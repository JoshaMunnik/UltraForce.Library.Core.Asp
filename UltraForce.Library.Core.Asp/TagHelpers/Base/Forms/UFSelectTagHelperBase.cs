// <copyright file="UFSelectTagHelperBase.cs" company="Ultra Force Development">
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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.NetStandard.Annotations;
using UltraForce.Library.NetStandard.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Forms;

/// <summary>
/// Base class to help with rendering a select element. If not `id` is set and
/// <see cref="NoWrap"/> is `false`, the code will generate an id so the label can reference the
/// select element.
/// <para>
/// If there are no field errors, the field error block is not rendered.
/// </para>
/// <para>
/// Renders with wrapping:
/// <code>
/// &lt;div class="{GetInputWrapperClasses()}" [id="{WrapId}"]&gt;<br/>
///   &lt;label class="{GetSelectLabelClasses()}" for="{id}" [id="{LabelId}"]&gt;
///     &lt;span class="{GetSelectLabelSpanClasses()}"&gt;
///      {GetLabelAsync(context,output)}
///     &lt;/span&gt;
///     &lt;span class="{GetSelectLabelDescriptionClasses()}"&gt;{GetDescription()}&lt;/span&gt;
///   &lt;/label&gt;
///   &lt;select class="{GetSelectClasses() id={} ..." &gt;<br/>
///     {children}<br/>
///   &lt;/select&gt;<br/>
///   {GetValidationFeedbackContainer(id)}<br/>
///   &lt;div class="{GetFieldErrorsClasses()}"&gt;{GetFieldErrorsHtml()}&lt;/div&gt;<br/>
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders without wrapping:
/// <code>
/// &lt;select class="{GetSelectClasses()}" &gt;<br/>
///   {children}<br/>
/// &lt;/select&gt;
/// </code>
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public abstract class UFSelectTagHelperBase(
  IHtmlGenerator generator
) : SelectTagHelper(generator)
{
  #region IUFInputProperties

  /// <summary>
  /// When set, use this value for label instead of getting from the
  /// <see cref="InputTagHelper.For"/> property.
  /// <para>
  /// When <see cref="NoWrap"/> is false, the property is only used with checkbox and radio elements.
  /// For other input elements the value is ignored.
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
  /// When true, do not the wrap input element in a div.
  /// </summary>
  [HtmlAttributeName("no-wrap")]
  public bool NoWrap { get; set; } = false;

  /// <summary>
  /// Additional text that is shown below the label. It is only used if <see cref="NoWrap"/> is
  /// not true.
  /// </summary>
  [HtmlAttributeName("description")]
  public string Description { get; set; } = "";

  /// <summary>
  /// When true do not include error block below the input element.
  /// </summary>
  [HtmlAttributeName("no-error")]
  public bool NoError { get; set; } = false;

  /// <summary>
  /// When set, set the id of the most outer wrapping element to this value.
  /// </summary>
  [HtmlAttributeName("wrap-id")]
  public string WrapId { get; set; } = "";

  /// <summary>
  /// When set, set the id of the label to this value.
  /// </summary>
  [HtmlAttributeName("label-id")]
  public string LabelId { get; set; } = "";

  #endregion

  #region public methods

  /// <inheritdoc />
  public override void Process(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    base.Process(context, output);
    output.TagName = "select";
    output.TagMode = TagMode.StartTagAndEndTag;
    string label = this.NoLabel
      ? ""
      : UFTagHelperTools.GetLabel(this.Generator, this.ViewContext, this.For, this.Label);
    if (((this.For != null) || !this.NoWrap) && (output.Attributes["id"] == null))
    {
      output.Attributes.SetAttribute("id", Guid.NewGuid().ToString());
    }
    string id = output.Attributes["id"]?.Value?.ToString() ?? "";
    string name = output.Attributes["name"]?.Value?.ToString() ?? id;
    string errorMessage = this.GetFieldErrorsHtml(
      this.ViewContext.ModelState, output.Attributes["name"]?.Value?.ToString() ?? ""
    );
    if (this.NoWrap)
    {
      UFTagHelperTools.AddClasses(output, this.GetSelectClasses());
    }
    else
    {
      this.WrapSelect(output, id, name, label, errorMessage);
    }
  }

  #endregion

  #region overridable protected methods

  /// <summary>
  /// Returns css classes for the select element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetSelectClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns css classes for the label element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetSelectLabelClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the span element inside the label.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetSelectLabelSpanClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the description span element inside the label.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetSelectLabelDescriptionClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns css classes for the wrapper element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetSelectWrapperClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the html for the validation feedback container.
  /// </summary>
  /// <param name="id">id of element or empty string if no id is set</param>
  /// <param name="name">name of field</param>
  /// <returns></returns>
  protected virtual string GetValidationFeedbackContainerHtml(
    string id, string name
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the css classes for the field errors block.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetFieldErrorsClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the html for the field errors block.
  /// </summary>
  /// <param name="states"></param>
  /// <param name="name"></param>
  /// <returns></returns>
  protected virtual string GetFieldErrorsHtml(
    ModelStateDictionary states,
    string name
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Gets a description string either from the <see cref="Description"/> property or from
  /// one of the known description providing attributes.
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

  #endregion

  #region private methods

  /// <summary>
  /// Renders the error block, shown below the select element.
  /// </summary>
  /// <param name="id"></param>
  /// <param name="name"></param>
  /// <param name="errorMessage"></param>
  /// <returns></returns>
  private string GetErrorBlock(
    string id,
    string name,
    string errorMessage
  )
  {
    if (this.NoError)
    {
      return "";
    }
    string errorMessageHtml = errorMessage != ""
      ? $"<div class=\"{this.GetFieldErrorsClasses()}\">{errorMessage}</div>"
      : "";
    return this.GetValidationFeedbackContainerHtml(id, name) + errorMessageHtml;
  }

  /// <summary>
  /// Wraps an element The elements gets wrapped in a div, a label and an error info block.
  /// </summary>
  /// <param name="output">Output to wrap</param>
  /// <param name="id">Id of input element</param>
  /// <param name="name">Name of input element</param>
  /// <param name="label">Label text to use</param>
  /// <param name="errorMessage">Error message to show</param>
  private void WrapSelect(
    TagHelperOutput output,
    string id,
    string name,
    string label,
    string errorMessage
  )
  {
    UFTagHelperTools.AddClasses(output, this.GetSelectClasses());
    string description = this.GetDescription();
    string descriptionHtml = string.IsNullOrEmpty(description) || string.IsNullOrEmpty(label)
      ? ""
      : $"<span class=\"{this.GetSelectLabelDescriptionClasses()}\">" +
      $"{description}</span>";
    string labelId = string.IsNullOrEmpty(this.LabelId)
      ? ""
      : $"id=\"{this.LabelId}\"";
    string labelHtml = string.IsNullOrEmpty(label)
      ? ""
      : $"<label class=\"{this.GetSelectLabelClasses()}\" for=\"{id}\" {labelId}>" +
      $"<span class=\"{this.GetSelectLabelSpanClasses()}\">{label}</span>" +
      descriptionHtml +
      "</label>";
    string wrapId = string.IsNullOrEmpty(this.WrapId)
      ? ""
      : $"id=\"{this.WrapId}\"";
    output.PreElement.AppendHtml(
      $"<div {wrapId} class=\"{this.GetSelectWrapperClasses()}\">{labelHtml}"
    );
    output.PostElement.AppendHtml(
      $"{this.GetErrorBlock(id, name, errorMessage)}</div>"
    );
  }

  #endregion
}
