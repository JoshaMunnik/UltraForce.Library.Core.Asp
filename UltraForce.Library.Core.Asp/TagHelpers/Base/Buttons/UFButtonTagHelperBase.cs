// <copyright file="UFButtonTagHelperBase.cs" company="Ultra Force Development">
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
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Buttons;

/// <summary>
/// Renders a button or link using a button styling. When rendering a button the default type is
/// `button`; use the <see cref="Submit"/> property to change it to submit.
/// <para>
/// Renders:
/// <code>
/// &lt;{a|button|div} class="{GetButtonClasses()}" {href} {disabled} {target}&gt;
///   {GetBeforeCaptionHtml()}
///   &lt;span class="{GetButtonCaptionClasses()}"&gt;{children}&lt;/span&gt;
///   {GetAfterCaptionHtml()}
/// &lt;/{a|button|div}&gt;
/// </code>
/// </para>
/// <remarks>
/// Part of the code is based on the <see cref="AnchorTagHelper"/> implemenation.
/// </remarks>
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public abstract class UFButtonTagHelperBase(
  EndpointDataSource anEndpointDataSource,
  IHtmlGenerator aHtmlGenerator,
  IUFModelExpressionRenderer aModelExpressionRenderer
)
  : UFClickableTagHelperBase(anEndpointDataSource, aHtmlGenerator)
{
  #region public properties

  /// <summary>
  /// If the button is an anchor, a `div` tag is used instead of `a` tag. With buttons the
  /// `disabled` attribute is set.
  /// </summary>
  [HtmlAttributeName("disabled")]
  public bool Disabled { get; set; } = false;

  /// <summary>
  /// When <code>true</code> the button is rendered with a div element.
  /// </summary>
  [HtmlAttributeName("static")]
  public bool Static { get; set; } = false;

  /// <summary>
  /// When <code>true</code> the button type is set to submit. This property is only of use if
  /// the button is placed inside a form element.
  /// </summary>
  [HtmlAttributeName("submit")]
  public bool Submit { get; set; } = false;

  /// <summary>
  /// Value to set for the onclick attribute of the button. This property is ignored if
  /// <see cref="Clipboard"/> has been set.
  /// </summary>
  [HtmlAttributeName("on-click")]
  public string? OnClick { get; set; }

  /// <summary>
  /// When set, an onclick handler is added to copy the value to the clipboard. 
  /// </summary>
  public string? Clipboard { get; set; }

  /// <summary>
  /// When set, use it to get a name for. The <see cref="Name"/> property is ignored. 
  /// </summary>
  public ModelExpression? For { get; set; }

  /// <summary>
  /// When set, set a name attribute. This property is not used if <see cref="For"/> is set.
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// When set, set a value attribute.
  /// </summary>
  public string? Value { get; set; }

  #endregion

  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    await base.ProcessAsync(context, output);
    bool hasHref = this.ProcessHref(output);
    output.TagName = this.Static ? "div" : hasHref ? (this.Disabled ? "div" : "a") : "button";
    output.TagMode = TagMode.StartTagAndEndTag;
    if (this.Disabled)
    {
      if (!hasHref)
      {
        output.Attributes.SetAttribute("disabled", "disabled");
      }
    }
    if ((output.TagName == "button") && !output.Attributes.ContainsName("type"))
    {
      output.Attributes.SetAttribute("type", this.Submit ? "submit" : "button");
    }
    TagHelperContent? children = await output.GetChildContentAsync();
    if (children is { IsEmptyOrWhiteSpace: false })
    {
      string beforeHtml = this.GetBeforeCaptionHtml(true);
      string afterHtml = this.GetAfterCaptionHtml(true);
      output.PreContent.SetHtmlContent(
        $"{beforeHtml}<span class=\"{this.GetButtonCaptionClasses()}\">"
      );
      output.PostContent.SetHtmlContent($"</span>{afterHtml}");
    }
    else
    {
      string beforeHtml = this.GetBeforeCaptionHtml(false);
      string afterHtml = this.GetAfterCaptionHtml(false);
      output.PreContent.SetHtmlContent(beforeHtml + afterHtml);
    }
    if (this.Clipboard != null)
    {
      output.Attributes.SetAttribute(
        "onclick",
        $"navigator.clipboard.writeText('{this.Clipboard.Replace("'", "\\'")}')"
      );
    }
    else if (this.OnClick != null)
    {
      output.Attributes.SetAttribute("onclick", this.OnClick);
    }
    UFTagHelperTools.AddClasses(output, this.GetButtonClasses());
    if (this.For != null)
    {
      output.Attributes.SetAttribute(
        "name", this.ModelExpressionRenderer.GetName(this.For, this.ViewContext)
      );
    }
    else if (!string.IsNullOrEmpty(this.Name))
    {
      output.Attributes.SetAttribute("name", this.Name);
    }
    if (!string.IsNullOrEmpty(this.Value))
    {
      output.Attributes.SetAttribute("value", this.Value);
    }
  }

  #endregion

  #region protected properties

  /// <summary>
  /// </summary>
  protected IUFModelExpressionRenderer ModelExpressionRenderer { get; } = aModelExpressionRenderer;

  #endregion

  #region overridable protected methods

  /// <summary>
  /// The default implementation returns empty string.
  /// </summary>
  /// <param name="hasContent">True if there is content for the caption</param>
  /// <returns></returns>
  protected virtual string GetBeforeCaptionHtml(bool hasContent)
  {
    return string.Empty;
  }

  /// <summary>
  /// The default implementation returns empty string.
  /// </summary>
  /// <param name="hasContent">True if there is content for the caption</param>
  /// <returns></returns>
  protected virtual string GetAfterCaptionHtml(bool hasContent)
  {
    return string.Empty;
  }

  /// <summary>
  /// The default implementation returns empty string.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetButtonCaptionClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// The default implementation returns empty string.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetButtonClasses()
  {
    return string.Empty;
  }

  #endregion
}