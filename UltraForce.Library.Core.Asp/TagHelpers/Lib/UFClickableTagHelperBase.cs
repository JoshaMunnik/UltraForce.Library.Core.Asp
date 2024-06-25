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

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;

namespace UltraForce.Library.Core.Asp.TagHelpers.Lib;

/// <summary>
/// <see cref="UFClickableTagHelperBase"/> can be used as base class for elements that can
/// be clicked upon and will jump to some other page.
/// <para>
/// It defines various properties to create an url from.
/// </para>
/// <para>
/// Subclasses can call the <see cref="GetHref"/> to get the href value.
/// </para>
/// </summary>
public class UFClickableTagHelperBase : UFTagHelperWithViewContext
{
  #region private variables
  
  /// <summary>
  /// See <see cref="RouteValues"/>
  /// </summary>
  private IDictionary<string, string>? m_routeValues = null;
  
  #endregion
  
  #region constructors

  /// <summary>
  /// Constructs an instance <see cref="UFClickableTagHelperBase"/>. 
  /// </summary>
  /// <param name="aHtmlGenerator">Used to generate an href value</param>
  /// <param name="aTheme"></param>
  protected UFClickableTagHelperBase(IHtmlGenerator aHtmlGenerator, IUFTheme aTheme) : base(aTheme)
  {
    this.HtmlGenerator = aHtmlGenerator;
  }

  #endregion

  #region public properties
  
  /// <summary>
  /// When set generate an url and use anchor tag instead of a button tag.
  /// </summary>
  [HtmlAttributeName("action")]
  public string? Action { get; set; }

  /// <summary>
  /// When set generate an url and use anchor tag instead of a button tag.
  /// </summary>
  [HtmlAttributeName("controller")]
  public string? Controller { get; set; }

  /// <summary>
  /// When set, use a anchor tag instead of a button tag.
  /// </summary>
  [HtmlAttributeName("href")]
  public string? Href { get; set; }

  /// <summary>
  /// Only used if <see cref="Href"/> contains a value. When true open the link in a new tab.
  /// </summary>
  [HtmlAttributeName("new-tab")]
  public bool NewTab { get; set; } = false;

  /// <summary>
  /// Additional parameters for the route. Only used if <see cref="Action"/> and/or
  /// <see cref="Controller"/> are used.
  /// <para>
  /// For the attribute name add the name of the parameter to `route-`.<br/>
  /// Example: `route-id="123"`
  /// </para>
  /// </summary>
  [HtmlAttributeName("all-route-data", DictionaryAttributePrefix = "route-")]
  public IDictionary<string, string> RouteValues
  {
    get =>
      this.m_routeValues ??= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    set => this.m_routeValues = value;
  }

  #endregion
  
  #region protected properties

  /// <summary>
  /// Gets the <see cref="IHtmlGenerator"/> used to generate
  /// the <see cref="AnchorTagHelper"/>'s output.
  /// </summary>
  protected IHtmlGenerator HtmlGenerator { get; }

  #endregion

  #region protected methods

  /// <summary>
  /// Determines the href value via <see cref="GetHref"/> and sets the href attribute and target
  /// attribute if <see cref="NewTab"/> is true.
  /// </summary>
  /// <param name="anOutput"></param>
  /// <returns>True if a href value could be determined</returns>
  protected bool ProcessHref(TagHelperOutput anOutput)
  {
    string? href = this.GetHref(anOutput);
    if (string.IsNullOrEmpty(href))
    {
      return false;
    }
    anOutput.Attributes.SetAttribute("href", href);
    if (this.NewTab)
    {
      anOutput.Attributes.SetAttribute("target", "_blank");
    }
    return true;
  }

  /// <summary>
  /// Gets the href. If a controller and/or action is set build the url from these, else check
  /// the href attribute.
  /// </summary>
  /// <param name="anOutput"></param>
  /// <returns></returns>
  protected string? GetHref(TagHelperOutput anOutput)
  {
    if (string.IsNullOrEmpty(this.Action) && string.IsNullOrEmpty(this.Controller))
    {
      return this.Href ?? anOutput.Attributes["href"]?.Value?.ToString();
    }
    TagBuilder? tagBuilder = this.HtmlGenerator.GenerateActionLink(
      this.ViewContext,
      linkText: string.Empty,
      actionName: this.Action,
      controllerName: this.Controller,
      protocol: "",
      hostname: "",
      fragment: "",
      routeValues: this.RouteValues,
      htmlAttributes: null
    );
    return tagBuilder?.Attributes["href"];
  }

  #endregion
}