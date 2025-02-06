// <copyright file="UFButtonTagHelper.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.TagHelpers.Lib;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Enums;

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Buttons;

/// <summary>
/// Renders a button or link using a button styling. When rendering a button the default type is
/// `button`; use the <see cref="Submit"/> property to change it to submit.
/// <para>
/// If the <see cref="Type"/> is set to <see cref="UFButtonType.Auto"/> the class will
/// update it to one of the type values depending on the state of the button.
/// </para>
/// <para>
/// Setting the <see cref="Type"/> to <see cref="UFButtonType.Disabled"/> will style the button
/// as disabled; however so long <see cref="Disabled"/> is not set to true, the button is still
/// clickable.
/// </para>
/// <para>
/// Renders:
/// <code>
/// &lt;{a|button|div} class="{GetButtonCssClasses()}" {href} {disabled} {target}&gt;<br/>
///   {GetButtonIconHtml()}<br/>
///   &lt;span class="{GetButtonCaptionCssClasses()}"&gt;{children}&lt;/span&gt;<br/>
/// &lt;/{a|button|div}&gt;
/// </code>
/// </para>
/// <remarks>
/// Part of the code is based on the <see cref="AnchorTagHelper"/> implemenation.
/// </remarks>
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[HtmlTargetElement("uf-button")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class UFButtonTagHelper(
  EndpointDataSource anEndpointDataSource,
  IHtmlGenerator aHtmlGenerator,
  IUFModelExpressionRenderer aModelExpressionRenderer,
  IUFTheme aTheme
)
  : UFClickableTagHelperBase(anEndpointDataSource, aHtmlGenerator, aTheme), IUFButtonProperties
{
  #region IUFButtonProperties

  /// <inheritdoc />
  [HtmlAttributeName("color")]
  public UFButtonType Type { get; set; } = UFButtonType.Auto;

  /// <inheritdoc />
  [HtmlAttributeName("size")]
  public UFSize Size { get; set; } = UFSize.Normal;

  /// <inheritdoc />
  [HtmlAttributeName("icon")]
  public string? Icon { get; set; }

  /// <inheritdoc />
  [HtmlAttributeName("icon-position")]
  public UFButtonIconPosition IconPosition { get; set; } = UFButtonIconPosition.Auto;

  /// <inheritdoc />
  [HtmlAttributeName("disabled")]
  public bool Disabled { get; set; } = false;

  /// <inheritdoc />
  [HtmlAttributeName("static")]
  public bool Static { get; set; } = false;

  /// <inheritdoc />
  [HtmlAttributeName("submit")]
  public bool Submit { get; set; } = false;

  /// <inheritdoc />
  [HtmlAttributeName("full-width")]
  public bool FullWidth { get; set; } = false;

  /// <inheritdoc />
  [HtmlAttributeName("full-height")]
  public bool FullHeight { get; set; } = false;

  /// <inheritdoc />
  [HtmlAttributeName("vertical-padding")]
  public int VerticalPadding { get; set; } = -1;

  /// <inheritdoc />
  [HtmlAttributeName("horizontal-padding")]
  public int HorizontalPadding { get; set; } = -1;
  
  /// <inheritdoc />
  [HtmlAttributeName("horizontal-content-position")]
  public UFContentPosition HorizontalContentPosition { get; set; } = UFContentPosition.Center;
  
  /// <inheritdoc />
  [HtmlAttributeName("vertical-content-position")]
  public UFContentPosition VerticalContentPosition { get; set; } = UFContentPosition.Center;

  #endregion

  #region public properties

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
  public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
  {
    await base.ProcessAsync(context, output);
    bool hasHref = this.ProcessHref(output);
    output.TagName = this.Static ? "div" : hasHref ? (this.Disabled ? "div" : "a") : "button";
    output.TagMode = TagMode.StartTagAndEndTag;
    UFButtonProperties properties = new (this);
    if (this.Disabled)
    {
      if (!hasHref)
      {
        output.Attributes.SetAttribute("disabled", "disabled");
      }
      if (properties.Type == UFButtonType.Auto)
      {
        properties.Type = UFButtonType.Disabled;
      }
    }
    if (properties.Type == UFButtonType.Auto)
    {
      properties.Type = UFButtonType.Normal;
    }
    if ((output.TagName == "button") && !output.Attributes.ContainsName("type"))
    {
      output.Attributes.SetAttribute("type", this.Submit ? "submit" : "button");
    }
    string iconHtml = this.GetButtonIconHtml(properties);
    TagHelperContent? children = await output.GetChildContentAsync();
    if (children is { IsEmptyOrWhiteSpace: false })
    {
      output.PreContent.SetHtmlContent(
        $"{iconHtml}<span class=\"{this.GetButtonCaptionClasses(properties)}\">"
      );
      output.PostContent.SetHtmlContent("</span>");
    }
    else
    {
      output.PreContent.SetHtmlContent(iconHtml);
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
    UFTagHelperTools.AddAttribute(
      output.Attributes,
      "class",
      this.GetButtonClasses(properties)
    );
    if (this.For != null)
    {
      output.Attributes.SetAttribute(
        "name",
        this.ModelExpressionRenderer.GetName(this.For, this.ViewContext)
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
  /// The default implementation calls <see cref="IUFTheme.GetButtonIconHtml"/>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetButtonIconHtml(IUFButtonProperties aProperties)
  {
    return this.Theme.GetButtonIconHtml(aProperties);
  }

  /// <summary>
  /// The default implementation calls <see cref="IUFTheme.GetButtonCaptionClasses"/>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetButtonCaptionClasses(IUFButtonProperties aProperties)
  {
    return this.Theme.GetButtonCaptionClasses(aProperties);
  }

  /// <summary>
  /// The default implementation calls <see cref="IUFTheme.GetButtonClasses"/>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetButtonClasses(IUFButtonProperties aProperties)
  {
    return this.Theme.GetButtonClasses(aProperties);
  }

  #endregion
}