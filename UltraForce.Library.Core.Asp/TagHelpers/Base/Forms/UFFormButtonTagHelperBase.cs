// <copyright file="UFFormButtonTagHelperBase.cs" company="Ultra Force Development">
// Ultra Force Library - Copyright (C) 2025 Ultra Force Development
// </copyright>
// <author>Josha Munnik</author>
// <email>josha@ultraforce.com</email>
// <license>
// The MIT License (MIT)
//
// Copyright (C) 2025 Ultra Force Development
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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.NetStandard.Annotations;
using UltraForce.Library.NetStandard.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Forms;

/// <summary>
/// Renders a button using form field styling.
/// <para>
/// Renders button with wrapping and label, similar to other form field elements:
/// <code>
/// &lt;div class="{GetButtonWrapperClasses()}"&gt;
///   &lt;label class="{GetButtonLabelClasses()}" for="{id}"&gt;
///     &lt;span class="{GetButtonLabelSpanClasses()}"&gt;
///      {GetLabelAsync(context,output)}
///     &lt;/span&gt;
///     &lt;span class="{GetButtonLabelDescriptionClasses()}"&gt;{GetDescription()}&lt;/span&gt;
///   &lt;/label&gt;
///     &lt;button class="{GetButtonClasses()}" ...&gt;
///     ...children... or value from "For" property
///     &lt;/button&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public abstract class UFFormButtonTagHelperBase(
  IHtmlGenerator generator,
  IUFModelExpressionRenderer modelExpressionRenderer
)
  : UFTagHelperWithModelExpressionRenderer(modelExpressionRenderer)
{
  #region public properties

  /// <summary>
  /// When set, use this value for label instead of getting from the
  /// <see cref="InputTagHelper.For"/> property.
  /// <para>
  /// When <see cref="NoWrap"/> is false, this property is not used.
  /// </para>
  /// </summary>
  [HtmlAttributeName("label")]
  public string Label { get; set; } = "";

  /// <summary>
  /// When true do not render a label element.
  /// </summary>
  [HtmlAttributeName("no-label")]
  public bool NoLabel { get; set; } = false;

  /// <summary>
  /// When true, do not the wrap button element in a div. 
  /// </summary>
  [HtmlAttributeName("no-wrap")]
  public bool NoWrap { get; set; } = false;

  /// <summary>
  /// An expression to be evaluated against the current model. The contents of the button is
  /// set to the value of this expression. The label is set to the name of this expression.
  /// If there is a description attribute on the property, it is used as the description.
  /// <para>
  /// Date values are formatted using mysql format (so there is no confusion on month and
  /// day positions):
  /// "yyyy-mm-dd hh:mm:ss"
  /// </para>
  /// </summary>
  [HtmlAttributeName("for")]
  public ModelExpression? For { get; set; } = null;

  /// <summary>
  /// Additional text that is shown below the label. It is only used if <see cref="NoWrap"/> is
  /// not true and there is a label being shown. Leave empty to use the description from the
  /// <see cref="For"/> property.
  /// </summary>
  [HtmlAttributeName("description")]
  public string Description { get; set; } = "";

  #endregion

  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    output.TagName = "button";
    output.TagMode = TagMode.StartTagAndEndTag;
    if (this.For != null)
    {
      await this.ModelExpressionRenderer.SetContentToValueAsync(
        output, this.For, this.ViewContext
      );
    }
    string label = await this.GetLabelHtmlAsync(context, output);
    if (this.NoWrap)
    {
      UFTagHelperTools.AddClasses(output, this.GetButtonClasses());
    }
    else
    {
      this.RenderWrappedButton(output, label);
    }
  }

  #endregion

  #region overridable protected methods

  /// <summary>
  /// Returns the classes to use for the button.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetButtonClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the label element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetButtonLabelClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the label html. The default implementation checks if <see cref="Label"/> has a value,
  /// if it does return that value.
  /// If <see cref="Label"/> is empty, try to determine the value from the
  /// <see cref="InputTagHelper.For"/> property.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="output"></param>
  /// <returns></returns>
  protected virtual async Task<string> GetLabelHtmlAsync(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    if (this.NoLabel)
    {
      return "";
    }
    if (!string.IsNullOrEmpty(this.Label))
    {
      return this.Label;
    }
    return UFTagHelperTools.GetLabel(generator, this.ViewContext, this.For, "");
  }
  
  /// <summary>
  /// Gets a description string either from the <see cref="Description"/> property or from
  /// one of the known description providing attributes if <see cref="For"/> has been set.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetDescription()
  {
    if (!string.IsNullOrEmpty(this.Description))
    {
      return this.Description;
    }
    PropertyInfo? propertyInfo = this.For?.Metadata.ContainerMetadata?.ModelType.GetProperty(
      this.For?.Metadata.PropertyName ?? ""
    );
    if (propertyInfo == null)
    {
      return "";
    }
    return
      UFAttributeTools.Find<DisplayAttribute>(propertyInfo)?.Description ??
      UFAttributeTools.Find<UFDescriptionAttribute>(propertyInfo)?.Description ??
      UFAttributeTools.Find<DescriptionAttribute>(propertyInfo)?.Description ??
      "";
  }

  /// <summary>
  /// Returns the classes to use for the span element inside the label.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetButtonLabelSpanClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the description span element inside the label.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetButtonLabelDescriptionClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the wrapper div element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetButtonWrapperClasses()
  {
    return string.Empty;
  }

  #endregion

  #region private methods

  /// <summary>
  /// Wraps an element The elements gets wrapped in a div, a label and an error info block.
  /// </summary>
  /// <param name="output">Output to wrap</param>
  /// <param name="label">Label text to use</param>
  private void RenderWrappedButton(
    TagHelperOutput output,
    string label
  )
  {
    UFTagHelperTools.AddClasses(
      output, this.GetButtonClasses()
    );
    string description = this.GetDescription();
    string descriptionHtml = string.IsNullOrEmpty(description) || string.IsNullOrEmpty(label)
      ? ""
      : $"<span class=\"{this.GetButtonLabelDescriptionClasses()}\">" +
      $"{description}</span>";
    string labelHtml = string.IsNullOrEmpty(label)
      ? ""
      : $"<label class=\"{this.GetButtonLabelClasses()}\">" +
      $"<span class=\"{this.GetButtonLabelSpanClasses()}\">{label}</span>" +
      descriptionHtml +
      "</label>";
    output.PreElement.AppendHtml(
      $"<div class=\"{this.GetButtonWrapperClasses()}\">{labelHtml}"
    );
    output.PostElement.AppendHtml(
      "</div>"
    );
  }

  #endregion
}