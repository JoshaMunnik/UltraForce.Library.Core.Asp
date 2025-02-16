// <copyright file="UFClickableTagHelperBase.cs" company="Ultra Force Development">
// Ultra Force Library - Copyright (C) 2024 Ultra Force Development
// </copyright>
// <author>Josha Munnik</author>
// <email>josha@ultraforce.com</email>
// <license>
// The MIT License (MIT)
//
// Copyright (C) 2024 Ultra Force Development
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to 
// deal in the Software without restriction, including without limitation the 
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
// IN THE SOFTWARE.
// </license>

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base;

/// <summary>
/// <see cref="UFClickableTagHelperBase"/> can be used as base class for elements that can
/// be clicked upon and will jump to some other page.
/// <para>
/// It is a subclass of <see cref="AnchorTagHelper"/> adding some additional properties.
/// </para>
/// <para>
/// Subclasses can call <see cref="ProcessHref"/> to determine the href value and process the
/// additional properties.
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public abstract class UFClickableTagHelperBase : AnchorTagHelper
{
  #region constructors

  /// <summary>
  /// Constructs an instance <see cref="UFClickableTagHelperBase"/>. 
  /// </summary>
  /// <param name="anEndpointDataSource"></param>
  /// <param name="aHtmlGenerator">Used to generate an href value</param>
  protected UFClickableTagHelperBase(
    EndpointDataSource anEndpointDataSource,
    IHtmlGenerator aHtmlGenerator
  )
    : base(aHtmlGenerator)
  {
    this.EndpointDataSource = anEndpointDataSource;
  }

  #endregion

  #region public properties

  /// <summary>
  /// Use this value if no other route is set.
  /// </summary>
  [HtmlAttributeName("href")]
  public string? Href { get; set; }

  /// <summary>
  /// When true add the `target="_blank"` attribute.
  /// </summary>
  [HtmlAttributeName("new-tab")]
  public bool NewTab { get; set; } = false;

  /// <summary>
  /// This property can be used as a shorthand combining <see cref="AnchorTagHelper.Controller" />,
  /// <see cref="AnchorTagHelper.Action" /> and <see cref="AnchorTagHelper.RouteValues" /> values.
  /// It 'calls' a controller action by building a href from the value.
  /// <para>
  /// Use the following format:
  /// <code>
  /// [Controller.]Action[(RouteValue1,RouteValue2,...)]
  /// </code>
  /// </para>
  /// <para>
  /// Values between square brackets are optional.
  /// </para>
  /// <para>
  /// If the controller is not set the current controller is used.
  /// </para>
  /// <para>
  /// The text separating parameters is set by the <see cref="CallParameterSeparator"/>; the
  /// default value is a comma.
  /// </para>
  /// <para>
  /// If the action does not expect any parameters, do not use the round brackets! Else the
  /// code assumes there is one parameter that has an empty string as value.
  /// </para>
  /// </summary>
  [HtmlAttributeName("call")]
  public string? Call { get; set; }

  /// <summary>
  /// The separator used to separate parameters in the <see cref="Call"/> property. 
  /// </summary>
  [HtmlAttributeName("call-parameter-separator")]
  public string? CallParameterSeparator { get; set; } = ",";

  #endregion

  #region protected properties

  /// <summary>
  /// Routing information
  /// </summary>
  protected EndpointDataSource EndpointDataSource { get; }

  #endregion

  #region protected methods

  /// <summary>
  /// Checks if a href attribute is set; if not create one based on the <see cref="Href"/> property.
  /// </summary>
  /// <param name="anOutput"></param>
  /// <returns>True if a href value could be determined</returns>
  protected bool ProcessHref(
    TagHelperOutput anOutput
  )
  {
    // always process new tab
    if (this.NewTab)
    {
      anOutput.Attributes.SetAttribute("target", "_blank");
    }
    bool routeLink = this.Route != null;
    bool actionLink = this.Controller != null || this.Action != null;
    bool pageLink = this.Page != null || this.PageHandler != null;
    if (routeLink || actionLink || pageLink)
    {
      if (!string.IsNullOrWhiteSpace(this.Call))
      {
        throw new InvalidOperationException(
          "Call can not be set when Route or Controller or Action or Page is also set."
        );
      }
      return true;
    }
    if (!string.IsNullOrWhiteSpace(this.Href))
    {
      anOutput.Attributes.SetAttribute("href", this.Href);
      return true;
    }
    string href = this.ProcessCall();
    if (href != "")
    {
      anOutput.Attributes.SetAttribute("href", href);
      return true;
    }
    int hrefIndex = anOutput.Attributes.IndexOfName("href");
    if (hrefIndex >= 0)
    {
      anOutput.Attributes.RemoveAt(hrefIndex);
    }
    return false;
  }

  #endregion

  #region private methods

  /// <summary>
  /// Processes the <see cref="Call"/> property to determine the href value.
  /// </summary>
  /// <returns>Href value or empty string if no href could be determined</returns>
  private string ProcessCall()
  {
    if (string.IsNullOrWhiteSpace(this.Call))
    {
      return "";
    }
    string controller = this.GetControllerFromCall();
    if (controller == "")
    {
      return "";
    }
    string action = this.GetActionFromCall();
    List<string> parameters = this.GetParametersFromCall();
    RouteEndpoint? endPoint = this.FindRouteEndpoint(controller, action, parameters);
    if (endPoint == null)
    {
      return "";
    }
    Dictionary<string, string> routeValues = BuildRouteValues(parameters, endPoint);
    return this.BuildHref(action, controller, routeValues);
  }

  /// <summary>
  /// Builds a href from an action, controller and route values. If no href can be determined
  /// the method returns an empty string.
  /// </summary>
  /// <param name="anAction"></param>
  /// <param name="aController"></param>
  /// <param name="aRouteValues"></param>
  /// <returns></returns>
  private string BuildHref(
    string anAction,
    string aController,
    Dictionary<string, string> aRouteValues
  )
  {
    TagBuilder? tagBuilder = this.Generator.GenerateActionLink(
      this.ViewContext,
      linkText: string.Empty,
      actionName: anAction,
      controllerName: aController,
      protocol: "",
      hostname: "",
      fragment: "",
      routeValues: aRouteValues,
      htmlAttributes: null
    );
    return tagBuilder?.Attributes["href"] ?? "";
  }

  /// <summary>
  /// Builds the route values from the parameters and the endpoint.
  /// </summary>
  /// <param name="aParameters"></param>
  /// <param name="anEndPoint"></param>
  /// <returns></returns>
  private static Dictionary<string, string> BuildRouteValues(
    List<string> aParameters,
    RouteEndpoint anEndPoint
  )
  {
    Dictionary<string, string> routeValues = [];
    int delta = anEndPoint.RoutePattern.Parameters.Count - aParameters.Count;
    for (int index = 0; index < aParameters.Count; index++)
    {
      routeValues[anEndPoint.RoutePattern.Parameters[index + delta].Name] = aParameters[index];
    }
    return routeValues;
  }

  /// <summary>
  /// Tries to find a route endpoint that matches the controller, action and the number of
  /// parameters.
  /// </summary>
  /// <param name="aController"></param>
  /// <param name="anAction"></param>
  /// <param name="aParameters"></param>
  /// <returns></returns>
  private RouteEndpoint? FindRouteEndpoint(
    string aController,
    string anAction,
    List<string> aParameters
  )
  {
    RouteEndpoint? endPoint = this
      .EndpointDataSource.Endpoints
      .OfType<RouteEndpoint>()
      .FirstOrDefault(
        endPoint =>
          (endPoint.RoutePattern.RequiredValues["controller"]?.ToString() == aController) &&
          (endPoint.RoutePattern.RequiredValues["action"]?.ToString() == anAction) &&
          (
            (endPoint.RoutePattern.Parameters.Count == aParameters.Count) ||
            (
              (endPoint.RoutePattern.Parameters.Count == aParameters.Count + 2) &&
              (endPoint.RoutePattern.Parameters[0].Name == "controller") &&
              (endPoint.RoutePattern.Parameters[1].Name == "action")
            )
          )
      );
    return endPoint;
  }

  /// <summary>
  /// Extracts the parameters from the call property.
  /// </summary>
  /// <returns></returns>
  private List<string> GetParametersFromCall()
  {
    int parameterStart = this.Call!.IndexOf('(');
    if (parameterStart < 0)
    {
      return [];
    }
    int parameterEnd = this.Call!.LastIndexOf(')');
    return this.Call
      .Substring(parameterStart + 1, parameterEnd - parameterStart - 1)
      .Split(this.CallParameterSeparator)
      .ToList();
  }

  /// <summary>
  /// Extracts the controller from the call property. If none is found the method tries to get the
  /// current controller. 
  /// </summary>
  /// <returns>Controller name or empty string if none could be determined</returns>
  private string GetControllerFromCall()
  {
    int parameterStart = this.GetParameterStartInCall();
    int point = this.Call!.IndexOf('.');
    if ((point > parameterStart) || (point < 0))
    {
      return this.ViewContext.RouteData.Values["controller"]?.ToString() ?? "";
    }
    return this.Call![..point];
  }

  private int GetParameterStartInCall()
  {
    int parameterStart = this.Call!.IndexOf('(');
    if (parameterStart < 0)
    {
      parameterStart = this.Call!.Length;
    }
    return parameterStart;
  }

  /// <summary>
  /// Extracts the action from the call property.
  /// </summary>
  /// <returns></returns>
  private string GetActionFromCall()
  {
    int parameterStart = this.GetParameterStartInCall();
    int point = this.Call!.IndexOf('.');
    return (point > 0) && (point < parameterStart)
      ? this.Call.Substring(point + 1, parameterStart - point - 1)
      : this.Call[..parameterStart];
  }

  #endregion
}