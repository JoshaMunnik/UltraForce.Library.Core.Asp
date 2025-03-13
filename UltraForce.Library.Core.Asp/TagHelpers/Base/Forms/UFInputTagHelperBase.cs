// <copyright file="UFInputTagHelperBase.cs" company="Ultra Force Development">
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
/// Renders either a text input, a checkbox or a radio button. If no `id` is set and
/// <see cref="NoWrap"/> is `true` the class will generate an id so the label can reference the
/// input element.
/// <para>
/// If there are no field errors, the field error block is not rendered.
/// </para>
/// <para>
/// Since the pseudo selectors ::after and ::before can not be used no input and textarea elements,
/// it is possible to override <see cref="GetTextInputPreHtml"/> and
/// <see cref="GetTextInputPostHtml"/> to add extra html before and after the input element.
/// </para>
/// <para>
/// To set a label, there are three options:
/// <ul>
///   <li>Set the <see cref="Label"/> property/attribute</li>
///   <li>Add one or more child elements/texts to the tag (they will be moved to the label)</li>
///   <li>Set the <see cref="InputTagHelper.For"/> property</li>
/// </ul>
/// </para>
/// <para>
/// Renders text input with wrapping and label:
/// <code>
/// &lt;div class="{GetTextInputWrapperClasses(type)}"&gt;
///   &lt;label class="{GetTextInputLabelClasses(type)}" for="{id}"&gt;
///     &lt;span class="{GetTextInputLabelSpanClasses(type)}"&gt;
///      {GetLabelAsync(context,output)}
///     &lt;/span&gt;
///     &lt;span class="{GetTextInputLabelDescriptionClasses(type)}"&gt;{description}&lt;/span&gt;
///   &lt;/label&gt;
///   {GetTextInputPreHtml()}
///     &lt;input class="{GetTextInputClasses(type)}" id={} .../&gt;
///   {GetTextInputPostHtml()}
///   {GetValidationFeedbackContainer(id)}
///   {RenderFieldErrors(...)}
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders text input with wrapping and no label:
/// <code>
/// &lt;div class="{GetTextInputWrapperClasses(type)}"&gt;
///   {GetTextInputPreHtml()}
///     &lt;input class="{GetTextInputClasses(type)}" id={} .../&gt;
///   {GetTextInputPostHtml()}
///   {GetValidationFeedbackContainer(id)}
///   &lt;div class="{GetFieldErrorsClasses()}"&gt;{GetFieldErrorsHtml()}&lt;/div&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders text input without wrapping:
/// <code>
/// &lt;input class="{GetTextInputClasses(type) .../&gt;
/// </code>
/// </para>
/// <para>
/// Renders multiline text input with wrapping and label:
/// <code>
/// &lt;div class="{GetTextInputWrapperClasses(type)}"&gt;
///   &lt;label class="{GetTextInputLabelClasses(type)}" for="{id}"&gt;
///     &lt;span class="{GetTextInputLabelSpanClasses(type)}"&gt;
///      {GetLabelAsync(context,output)}
///     &lt;/span&gt;
///     &lt;span class="{GetTextInputLabelDescriptionClasses(type)}"&gt;{description}&lt;/span&gt;
///   &lt;/label&gt;
///   {GetTextInputPreHtml()}
///     &lt;textarea class="{GetTextInputClasses(type)}" id={} ..." &gt;{value}&lt;/textarea&gt;
///   {GetTextInputPostHtml()}
///   {GetValidationFeedbackContainer(id)}
///   {RenderFieldErrors(...)}
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders multiline text input with wrapping and no label:
/// <code>
/// &lt;div class="{GetTextInputWrapperClasses(type)}"&gt;
///   {GetTextInputPreHtml(type)}
///     &lt;textarea class="{GetTextInputClasses(type)}" id={} ..." &gt;{value}&lt;/textarea&gt;
///   {GetTextInputPostHtml()}
///   {GetValidationFeedbackContainer(id)}
///   &lt;div class="{GetFieldErrorsClasses()}"&gt;{GetFieldErrorsHtml()}&lt;/div&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders multiline text input without wrapping:
/// <code>
/// &lt;textarea class="{GetTextInputClasses(type)}" id={} ..." &gt;{value}&lt;/textarea&gt;
/// </code>
/// </para>
/// <para>
/// Renders radio input with wrapping and a label:
/// <code>
/// &lt;div class="{GetRadioWrapperClasses()}"&gt;
///   &lt;label class="{GetRadioLabelClasses()}"&gt;
///     {GetRadioExtraHtml()}
///     &lt;input type="radio" class="{GetRadioInputClasses()}" .../&gt;
///     &lt;span class="{GetRadioLabelSpanClasses()}"&gt;
///      {GetLabelAsync(context,output)}
///     &lt;/span&gt;
///     &lt;span class="{GetRadioLabelDescriptionClasses()}"&gt;{description}&lt;/span&gt;
///   &lt;/label&gt;
///   {GetValidationFeedbackContainer(id)}
///   &lt;div class="{GetFieldErrorsClasses()}"&gt;{GetFieldErrorsHtml()}&lt;/div&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders radio input with wrapping but no label:
/// <code>
/// &lt;div class="{GetRadioWrapperClasses()}"&gt;
///   &lt;input type="radio" class="{GetRadioInputClasses()}" .../&gt;
///   {GetRadioExtraHtml()}
///   {GetValidationFeedbackContainer(id)}
///   &lt;div class="{GetFieldErrorsClasses()}"&gt;{GetFieldErrorsHtml()}&lt;/div&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders radio input without wrapping but with a label:
/// <code>
/// &lt;label class="{GetRadioLabelClasses()}"&gt;
///   &lt;input type="radio" class="{GetRadioInputClasses()}" .../&gt;
///   {GetRadioExtraHtml()}
///   &lt;span class="{GetRadiiLabelSpanClasses()}"&gt;
///    {GetLabelAsync(context,output)}
///   &lt;/span&gt;
///   &lt;span class="{GetRadioLabelDescriptionClasses()}"&gt;{description}&lt;/span&gt;
/// &lt;/label&gt;
/// </code>
/// </para>
/// <para>
/// Renders radio input without wrapping and without a label:
/// <code>
/// &lt;input type="radio" class="{GetRadioInputClasses()}" .../&gt;
/// {GetRadioExtraHtml()}
/// </code>
/// </para>
/// <para>
/// Renders checkbox input with wrapping and a label:
/// <code>
/// &lt;div class="{GetCheckboxWrapperClasses()}"&gt;
///   &lt;label class="{GetCheckboxLabelClasses()}"&gt;
///     &lt;input type="checkbox" class="{GetCheckboxInputClasses()}" .../&gt;
///     {GetCheckboxExtraHtml()}
///     &lt;span class="{GetCheckboxLabelSpanClasses()}"&gt;
///      {GetLabelAsync(context,output)}
///     &lt;/span&gt;
///    &lt;span class="{GetCheckboxLabelDescriptionClasses()}"&gt;{description}&lt;/span&gt;
///   &lt;/label&gt;
///   {GetValidationFeedbackContainer(id)}
///   &lt;div class="{GetFieldErrorsClasses()}"&gt;{GetFieldErrorsHtml()}&lt;/div&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders checkbox input with wrapping but no label:
/// <code>
/// &lt;div class="{GetCheckboxWrapperClasses()}"&gt;
///   &lt;input type="checkbox" class="{GetCheckboxInputClasses()}" .../&gt;
///   {GetCheckboxExtraHtml()}
///   {GetValidationFeedbackContainer(id)}
///   &lt;div class="{GetFieldErrorsClasses()}"&gt;{GetFieldErrorsHtml()}&lt;/div&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders checkbox input without wrapping but with a label:
/// <code>
/// &lt;label class="{GetCheckboxLabelClasses()}"&gt;
///   &lt;input type="checkbox" class="{GetCheckboxInputClasses()}" id={} ... /&gt;
///   {GetCheckboxExtraHtml()}
///   &lt;span class="{GetCheckboxLabelSpanClasses()}"&gt;
///    {GetLabelAsync(context,output)}
///   &lt;/span&gt;
///   &lt;span class="{GetCheckboxLabelDescriptionClasses()}"&gt;{description}&lt;/span&gt;
/// &lt;/label&gt;
/// </code>
/// </para>
/// <para>
/// Renders checkbox input without wrapping and without a label:
/// <code>
/// &lt;input type="checkbox" class="{GetCheckboxInputClasses()}" id={} .../&gt;
/// {GetCheckboxExtraHtml()}
/// </code>
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public abstract class UFInputTagHelperBase(
  IHtmlGenerator generator
)
  : InputTagHelper(generator)
{
  #region public properties

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
  /// When true show a multiline text input (textarea).
  /// </summary>
  [HtmlAttributeName("multiline")]
  public bool Multiline { get; set; } = false;

  #endregion

  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    output.TagName = this.Multiline ? "textarea" : "input";
    output.TagMode = this.Multiline ? TagMode.StartTagAndEndTag : TagMode.SelfClosing;
    if (this.For != null)
    {
      base.Process(context, output);
    }
    else
    {
      if (this.InputTypeName != null)
      {
        output.Attributes.SetAttribute("type", this.InputTypeName);
      }
      if (this.Name != null)
      {
        output.Attributes.SetAttribute("name", this.Name);
      }
      if (this.Value != null)
      {
        output.Attributes.SetAttribute("value", this.Value);
      }
    }
    if (this.Multiline)
    {
      output.Content.SetContent(output.Attributes["value"]?.Value?.ToString() ?? "");
    }
    string label = await this.GetLabelHtmlAsync(context, output);
    if (((this.For != null) || !this.NoWrap) && (output.Attributes["id"] == null))
    {
      output.Attributes.SetAttribute("id", Guid.NewGuid().ToString());
    }
    string id = output.Attributes["id"]?.Value?.ToString() ?? "";
    string type = output.Attributes["type"]?.Value?.ToString()?.ToLowerInvariant() ?? "";
    string name = output.Attributes["name"]?.Value?.ToString() ?? id;
    string errorMessage = this.GetFieldErrorsHtml(
      this.ViewContext.ModelState,
      output.Attributes["name"]?.Value?.ToString() ?? ""
    );
    switch (type)
    {
      case "checkbox":
        this.RenderCheckbox(output, id, name, label, errorMessage);
        break;
      case "radio":
        this.RenderRadio(output, id, name, label, errorMessage);
        break;
      default:
        if (this.NoWrap)
        {
          UFTagHelperTools.AddClasses(output, this.GetTextInputClasses(type));
        }
        else
        {
          this.RenderWrappedInput(output, id, name, label, type, errorMessage);
        }
        break;
    }
  }

  #endregion

  #region overridable protected methods

  /// <summary>
  /// Returns the classes to use for the input element.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  protected virtual string GetTextInputClasses(
    string type
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the label element.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  protected virtual string GetTextInputLabelClasses(
    string type
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the label html. The default implementation checks if <see cref="Label"/> has a value,
  /// if it does return that value.
  /// Else check if the tag has any children and return that content.
  /// If there are no children and <see cref="Label"/> is empty, try to determine the value from
  /// the <see cref="InputTagHelper.For"/> property.
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
    string content = (await output.GetChildContentAsync()).GetContent();
    if (!string.IsNullOrEmpty(content))
    {
      output.Content.SetHtmlContent(string.Empty);
      return content;
    }
    return UFTagHelperTools.GetLabel(this.Generator, this.ViewContext, this.For, "");
  }

  /// <summary>
  /// Returns the classes to use for the span element inside the label.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  protected virtual string GetTextInputLabelSpanClasses(
    string type
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Gets html code inserted before the input element.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  protected virtual string GetTextInputPreHtml(
    string type
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Gets html code inserted after the input element.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  protected virtual string GetTextInputPostHtml(
    string type
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the description span element inside the label.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  protected virtual string GetTextInputLabelDescriptionClasses(
    string type
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the wrapper div element.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  protected virtual string GetTextInputWrapperClasses(
    string type
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the validation feedback container.
  /// </summary>
  /// <param name="id">id of element or empty string if no id is used</param>
  /// <param name="name">name of form field</param>
  /// <returns></returns>
  protected virtual string GetValidationFeedbackContainerHtml(
    string id,
    string name
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the field errors div element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetFieldErrorsClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the html to use for the field errors.
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
  /// Returns the classes to use for the radio input element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetRadioInputClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the radio wrapper div element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetRadioWrapperClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the radio label element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetRadioLabelClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the span element inside the radio label.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetRadioLabelSpanClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the description span element inside the radio label.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetRadioLabelDescriptionClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the html to use for the radio extra content.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetRadioExtraHtml()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the checkbox input element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetCheckboxInputClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the checkbox wrapper div element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetCheckboxWrapperClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the checkbox label element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetCheckboxLabelClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the span element inside the checkbox label.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetCheckboxLabelSpanClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use for the description span element inside the checkbox label.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetCheckboxLabelDescriptionClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the html to use for the checkbox extra content.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetCheckboxExtraHtml()
  {
    return string.Empty;
  }

  #endregion

  #region private methods

  /// <summary>
  /// Gets the html block showing an error message (if any).
  /// </summary>
  /// <param name="errorMessage"></param>
  /// <returns></returns>
  private string GetErrorMessageHtml(
    string errorMessage
  )
  {
    return errorMessage != ""
      ? $"<div class=\"{this.GetFieldErrorsClasses()}\">{errorMessage}</div>"
      : "";
  }

  /// <summary>
  /// Wraps an element The elements gets wrapped in a div, a label and an error info block.
  /// </summary>
  /// <param name="output">Output to wrap</param>
  /// <param name="id">Id of input element</param>
  /// <param name="name"></param>
  /// <param name="label">Label text to use</param>
  /// <param name="type">Input type</param>
  /// <param name="errorMessage">Error message to show</param>
  private void RenderWrappedInput(
    TagHelperOutput output,
    string id,
    string name,
    string label,
    string type,
    string errorMessage
  )
  {
    string errorMessageHtml = this.GetErrorMessageHtml(errorMessage);
    UFTagHelperTools.AddClasses(
      output, this.GetTextInputClasses(type)
    );
    string description = this.GetDescription();
    string descriptionHtml = string.IsNullOrEmpty(description) || string.IsNullOrEmpty(label)
      ? ""
      : $"<span class=\"{this.GetTextInputLabelDescriptionClasses(type)}\">" +
      $"{description}</span>";
    string labelHtml = string.IsNullOrEmpty(label)
      ? ""
      : $"<label class=\"{this.GetTextInputLabelClasses(type)}\" for=\"{id}\">" +
      $"<span class=\"{this.GetTextInputLabelSpanClasses(type)}\">{label}</span>" +
      descriptionHtml +
      "</label>";
    string preHtml = this.GetTextInputPreHtml(type);
    string postHtml = this.GetTextInputPostHtml(type);
    output.PreElement.AppendHtml(
      $"<div class=\"{this.GetTextInputWrapperClasses(type)}\">{labelHtml}{preHtml}"
    );
    output.PostElement.AppendHtml(
      $"{postHtml}{this.GetValidationFeedbackContainerHtml(id, name)}{errorMessageHtml}</div>"
    );
  }

  private void RenderRadio(
    TagHelperOutput anOutput,
    string id,
    string name,
    string label,
    string errorMessage
  )
  {
    string errorMessageHtml = this.GetErrorMessageHtml(errorMessage);
    UFTagHelperTools.AddClasses(anOutput, this.GetRadioInputClasses());
    string pre = !this.NoWrap ? $"<div class=\"{this.GetRadioWrapperClasses()}\">" : "";
    string post = !this.NoWrap
      ? $"{this.GetValidationFeedbackContainerHtml(id, name)}{errorMessageHtml}</div>"
      //? $"{errorMessage}</div>"
      : "";
    string preLabel = string.IsNullOrEmpty(label)
      ? ""
      : $"<label class=\"{this.GetRadioLabelClasses()}\">";
    string description = this.GetDescription();
    string descriptionHtml = string.IsNullOrEmpty(description) || string.IsNullOrEmpty(label)
      ? ""
      : $"<span class=\"{this.GetRadioLabelDescriptionClasses()}\">" +
      $"{description}" +
      "</span>";
    string postLabel = string.IsNullOrEmpty(label)
      ? ""
      : $"<span class=\"{this.GetRadioLabelSpanClasses()}\">{label}</span>" +
      descriptionHtml +
      "</label>";
    if (pre.Length + preLabel.Length > 0)
    {
      anOutput.PreElement.AppendHtml(pre + preLabel);
      anOutput.PostElement.AppendHtml(this.GetRadioExtraHtml() + postLabel + post);
    }
    else
    {
      anOutput.PostElement.AppendHtml(this.GetRadioExtraHtml());
    }
  }

  private void RenderCheckbox(
    TagHelperOutput anOutput,
    string id,
    string name,
    string label,
    string errorMessage
  )
  {
    string errorMessageHtml = this.GetErrorMessageHtml(errorMessage);
    UFTagHelperTools.AddClasses(anOutput, this.GetCheckboxInputClasses());
    string pre = !this.NoWrap ? $"<div class=\"{this.GetCheckboxWrapperClasses()}\">" : "";
    string post = !this.NoWrap
      ? $"{this.GetValidationFeedbackContainerHtml(id, name)}{errorMessageHtml}</div>"
      //? $"{errorMessage}</div>"
      : "";
    string preLabel = string.IsNullOrEmpty(label)
      ? ""
      : $"<label class=\"{this.GetCheckboxLabelClasses()}\">";
    string description = this.GetDescription();
    string descriptionHtml = string.IsNullOrEmpty(description) || string.IsNullOrEmpty(label)
      ? ""
      : $"<span class=\"{this.GetCheckboxLabelDescriptionClasses()}\">" +
      $"{description}" +
      "</span>";
    string postLabel = string.IsNullOrEmpty(label)
      ? ""
      : $"<span class=\"{this.GetCheckboxLabelSpanClasses()}\">{label}</span>" +
      descriptionHtml +
      "</label>";
    if (pre.Length + preLabel.Length > 0)
    {
      anOutput.PreElement.AppendHtml(pre + preLabel);
      anOutput.PostElement.AppendHtml(this.GetCheckboxExtraHtml() + postLabel + post);
    }
    else
    {
      anOutput.PostElement.AppendHtml(this.GetCheckboxExtraHtml());
    }
  }

  /// <summary>
  /// Gets a description string either from the property or from one of the known description
  /// providing attributes.
  /// </summary>
  /// <returns></returns>
  private string GetDescription()
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
}