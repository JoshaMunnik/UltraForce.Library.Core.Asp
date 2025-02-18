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
/// Renders a button or link using a button styling. This class is the base class for all button
/// tag helpers. 
/// <para>
/// When rendering a button the default type is `button`; use the <see cref="Submit"/> property to
/// change it to submit.
/// </para>
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
public abstract class UFBaseButtonTagHelperBase(
  EndpointDataSource endpointDataSource,
  IHtmlGenerator htmlGenerator,
  IUFModelExpressionRenderer modelExpressionRenderer
)
  : UFClickableTagHelperBase(endpointDataSource, htmlGenerator)
{
  #region public properties

  /// <summary>
  /// If the button is an anchor, a `div` tag is used instead of `a` tag. With buttons the
  /// `disabled` attribute is set.
  /// </summary>
  [HtmlAttributeName("disabled")]
  public bool Disabled { get; set; } = false;

  /// <summary>
  /// When <c>true</c> the button is rendered with a div element.
  /// </summary>
  [HtmlAttributeName("static")]
  public bool Static { get; set; } = false;

  /// <summary>
  /// When <c>true</c> the button type is set to submit. This property is only of use if
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
    bool isStatic = this.Static || (this.Disabled && hasHref);
    output.TagName = isStatic ? "div" : hasHref ? "a" : "button";
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
      string beforeHtml = this.GetBeforeCaptionHtml(context, output, true, isStatic);
      string afterHtml = this.GetAfterCaptionHtml(context, output, true, isStatic);
      output.PreContent.SetHtmlContent(
        $"{beforeHtml}<span class=\"{this.GetButtonCaptionClasses(context, output, isStatic)}\">"
      );
      output.PostContent.SetHtmlContent($"</span>{afterHtml}");
      UFTagHelperTools.AddClasses(output, this.GetButtonClasses(context, output, true, isStatic));
    }
    else
    {
      string beforeHtml = this.GetBeforeCaptionHtml(context, output, false, isStatic);
      string afterHtml = this.GetAfterCaptionHtml(context, output, false, isStatic);
      output.PreContent.SetHtmlContent(beforeHtml + afterHtml);
      UFTagHelperTools.AddClasses(
        output, this.GetButtonClasses(context, output, false, isStatic)
      );
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
  protected IUFModelExpressionRenderer ModelExpressionRenderer { get; } = modelExpressionRenderer;

  #endregion

  #region internal methods

  /// <summary>
  /// Gets the html before the caption.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="output"></param>
  /// <param name="hasCaption">True if the tag has any content</param>
  /// <param name="isStatic">True if the tag is rendered as a div element</param>
  /// <returns></returns>
  protected abstract string GetBeforeCaptionHtml(
    TagHelperContext context,
    TagHelperOutput output,
    bool hasCaption,
    bool isStatic
  );

  /// <summary>
  /// Gets the html after the caption.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="output"></param>
  /// <param name="hasCaption">True if the tag has any content</param>
  /// <param name="isStatic">True if the tag is rendered as a div element</param>
  /// <returns></returns>
  protected abstract string GetAfterCaptionHtml(
    TagHelperContext context,
    TagHelperOutput output,
    bool hasCaption,
    bool isStatic
  );

  /// <summary>
  /// Gets the css classes to use for the caption span. 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="output"></param>
  /// <param name="isStatic">True if the tag is rendered as a div element</param>
  /// <returns></returns>
  protected abstract string GetButtonCaptionClasses(
    TagHelperContext context,
    TagHelperOutput output,
    bool isStatic
  );

  /// <summary>
  /// Gets the css classes to use for the button.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="output"></param>
  /// <param name="hasCaption">True if the tag has any content</param>
  /// <param name="isStatic">True if the tag is rendered as a div element</param>
  /// <returns></returns>
  protected abstract string GetButtonClasses(
    TagHelperContext context,
    TagHelperOutput output,
    bool hasCaption,
    bool isStatic
  );

  #endregion
}