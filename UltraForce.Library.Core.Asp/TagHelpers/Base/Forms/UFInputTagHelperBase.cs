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
/// Renders text input with wrapping and label:
/// <code>
/// &lt;div class="{GetTextInputWrapperClasses(type)}"&gt;
///   &lt;label class="{GetTextInputLabelClasses(type)}" for="{id}"&gt;
///     &lt;span class="{GetTextInputLabelSpanClasses(type)}"&gt;{label}&lt;/span&gt;
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
///     &lt;span class="{GetTextInputLabelSpanClasses(type)}"&gt;{label}&lt;/span&gt;
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
///     &lt;span class="{GetRadioLabelSpanClasses()}&gt;{label}&lt;/span&gt;
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
///   &lt;input type="radio" class="{GetRadioInputClasses()}" id={} .../&gt;
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
///   &lt;input type="radio" class="{GetRadioInputClasses()}" id={} .../&gt;
///   {GetRadioExtraHtml()}
///   &lt;span class="{GetRadioLabelSpanClasses()}&gt;{label}&lt;/span&gt;
///   &lt;span class="{GetRadioLabelDescriptionClasses()}"&gt;{description}&lt;/span&gt;
/// &lt;/label&gt;
/// </code>
/// </para>
/// <para>
/// Renders radio input without wrapping and without a label:
/// <code>
/// &lt;input type="radio" class="{GetRadioInputClasses()}" id={} .../&gt;
/// {GetRadioExtraHtml()}
/// </code>
/// </para>
/// <para>
/// Renders checkbox input with wrapping and a label:
/// <code>
/// &lt;div class="{GetCheckboxWrapperClasses()}"&gt;
///   &lt;label class="{GetCheckboxLabelClasses()}"&gt;
///     &lt;input type="checkbox" class="{GetCheckboxInputClasses()}" id={} .../&gt;
///    {GetCheckboxExtraHtml()}
///    &lt;span class="{GetCheckboxLabelSpanClasses()}&gt;{label}&lt;/span&gt;
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
///   &lt;input type="checkbox" class="{GetCheckboxInputClasses()}" id={} .../&gt;
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
///   &lt;span class="{GetCheckboxLabelSpanClasses()}&gt;{label}&lt;/span&gt;
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
[HtmlTargetElement("uf-input")]
public abstract class UFInputTagHelperBase(IHtmlGenerator generator)
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
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    output.TagName = this.Multiline ? "textarea" : "input";
    output.TagMode = this.Multiline ? TagMode.StartTagAndEndTag : TagMode.SelfClosing;
    if (this.For != null)
    {
      base.Process(context, output);
    }
    else
    {
      output.Attributes.SetAttribute("type", this.InputTypeName);
      output.Attributes.SetAttribute("name", this.Name);
      output.Attributes.SetAttribute("value", this.Value);
    }
    if (this.Multiline)
    {
      output.Content.SetContent(output.Attributes["value"]?.Value?.ToString() ?? "");
    }
    string label = this.NoLabel
      ? ""
      : UFTagHelperTools.GetLabel(this.Generator, this.ViewContext, this.For, this.Label);
    if (((this.For != null) || !this.NoWrap) && (output.Attributes["id"] == null))
    {
      output.Attributes.SetAttribute("id", Guid.NewGuid().ToString());
    }
    string id = (output.Attributes["id"] == null) ? "" : output.Attributes["id"].Value.ToString()!;
    string type = output.Attributes["type"]?.Value?.ToString()?.ToLowerInvariant() ?? "";
    string errorMessage = this.GetFieldErrorsHtml(
      this.ViewContext.ModelState,
      output.Attributes["name"]?.Value?.ToString() ?? ""
    );
    switch (type)
    {
      case "checkbox":
        this.RenderCheckbox(output, id, label, errorMessage);
        break;
      case "radio":
        this.RenderRadio(output, id, label, errorMessage);
        break;
      default:
        if (this.NoWrap)
        {
          UFTagHelperTools.AddClasses(output, this.GetTextInputClasses(type));
        }
        else
        {
          this.RenderWrappedInput(output, id, label, type, errorMessage);
        }
        break;
    }
  }

  #endregion
  
  #region overridable protected methods
  
  /// <summary>
  /// Returns the classes to use for the input element.
  /// </summary>
  /// <param name="aType"></param>
  /// <returns></returns>
  protected virtual string GetTextInputClasses(string aType)
  {
    return string.Empty;
  }
  
  /// <summary>
  /// Returns the classes to use for the label element.
  /// </summary>
  /// <param name="aType"></param>
  /// <returns></returns>
  protected virtual string GetTextInputLabelClasses(string aType)
  {
    return string.Empty;
  }
  
  /// <summary>
  /// Returns the classes to use for the span element inside the label.
  /// </summary>
  /// <param name="aType"></param>
  /// <returns></returns>
  protected virtual string GetTextInputLabelSpanClasses(string aType)
  {
    return string.Empty;
  }

  /// <summary>
  /// Gets html code inserted before the input element.
  /// </summary>
  /// <param name="aType"></param>
  /// <returns></returns>
  protected virtual string GetTextInputPreHtml(
    string aType
  )
  {
    return string.Empty;
  }
  
  /// <summary>
  /// Gets html code inserted after the input element.
  /// </summary>
  /// <param name="aType"></param>
  /// <returns></returns>
  protected virtual string GetTextInputPostHtml(
    string aType
  )
  {
    return string.Empty;
  }
  
  /// <summary>
  /// Returns the classes to use for the description span element inside the label.
  /// </summary>
  /// <param name="aType"></param>
  /// <returns></returns>
  protected virtual string GetTextInputLabelDescriptionClasses(string aType)
  {
    return string.Empty;
  }
  
  /// <summary>
  /// Returns the classes to use for the wrapper div element.
  /// </summary>
  /// <param name="aType"></param>
  /// <returns></returns>
  protected virtual string GetTextInputWrapperClasses(string aType)
  {
    return string.Empty;
  }
  
  /// <summary>
  /// Returns the classes to use for the validation feedback container.
  /// </summary>
  /// <param name="anId"></param>
  /// <returns></returns>
  protected virtual string GetValidationFeedbackContainerHtml(string anId)
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
  protected virtual string GetFieldErrorsHtml(ModelStateDictionary states, string name)
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
  /// <param name="anErrorMessage"></param>
  /// <returns></returns>
  private string GetErrorMessageHtml(string anErrorMessage)
  {
    return anErrorMessage != ""
      ? $"<div class=\"{this.GetFieldErrorsClasses()}\">{anErrorMessage}</div>"
      : "";
  }

  /// <summary>
  /// Wraps an element The elements gets wrapped in a div, a label and an error info block.
  /// </summary>
  /// <param name="anOutput">Output to wrap</param>
  /// <param name="anId">Id of input element</param>
  /// <param name="aLabel">Label text to use</param>
  /// <param name="aType">Input type</param>
  /// <param name="anErrorMessage">Error message to show</param>
  private void RenderWrappedInput(
    TagHelperOutput anOutput, string anId, string aLabel, string aType, string anErrorMessage
  )
  {
    string errorMessage = this.GetErrorMessageHtml(anErrorMessage);
    UFTagHelperTools.AddClasses(
      anOutput, this.GetTextInputClasses(aType)
    );
    string description = this.GetDescription();
    string descriptionHtml = string.IsNullOrEmpty(description) || string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<span class=\"{this.GetTextInputLabelDescriptionClasses(aType)}\">" +
        $"{description}</span>";
    string labelHtml = string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<label class=\"{this.GetTextInputLabelClasses(aType)}\" for=\"{anId}\">" +
        $"<span class=\"{this.GetTextInputLabelSpanClasses(aType)}\">{aLabel}</span>" +
        descriptionHtml +
        "</label>";
    string preHtml = this.GetTextInputPreHtml(aType);
    string postHtml = this.GetTextInputPostHtml(aType);
    anOutput.PreElement.SetHtmlContent(
      $"<div class=\"{this.GetTextInputWrapperClasses(aType)}\">{labelHtml}{preHtml}"
    );
    anOutput.PostElement.SetHtmlContent(
      $"{postHtml}{this.GetValidationFeedbackContainerHtml(anId)}{errorMessage}</div>"
    );
  }

  private void RenderRadio(
    TagHelperOutput anOutput, string anId, string aLabel, string anErrorMessage
  )
  {
    string errorMessage = this.GetErrorMessageHtml(anErrorMessage);
    UFTagHelperTools.AddClasses(anOutput, this.GetRadioInputClasses());
    string pre = this.NoWrap ? $"<div class=\"{this.GetRadioWrapperClasses()}\">" : "";
    string post = this.NoWrap
      //? $"{this.GetValidationFeedbackContainer(anId)}{errorMessage}</div>"
      ? $"{errorMessage}</div>"
      : "";
    string preLabel = string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<label class=\"{this.GetRadioLabelClasses()}\"><span>";
    string description = this.GetDescription();
    string descriptionHtml = string.IsNullOrEmpty(description) || string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<span class=\"{this.GetRadioLabelDescriptionClasses()}\">" +
        $"{description}" +
        $"</span>";
    string postLabel = string.IsNullOrEmpty(aLabel)
      ? ""
      : $"</span>" +
        $"<span class=\"{this.GetRadioLabelSpanClasses()}\">{aLabel}</span>" +
        descriptionHtml +
        $"</label>";
    if (pre.Length + preLabel.Length > 0)
    {
      anOutput.PreElement.SetHtmlContent(pre + preLabel);
      anOutput.PostElement.SetHtmlContent(this.GetRadioExtraHtml() + postLabel + post);
    }
    else
    {
      anOutput.PostElement.SetHtmlContent(this.GetRadioExtraHtml());
    }
  }

  private void RenderCheckbox(
    TagHelperOutput anOutput, string anId, string aLabel, string anErrorMessage
  )
  {
    string errorMessage = this.GetErrorMessageHtml(anErrorMessage);
    UFTagHelperTools.AddClasses(anOutput, this.GetCheckboxInputClasses());
    string pre = this.NoWrap ? $"<div class=\"{this.GetCheckboxWrapperClasses()}\">" : "";
    string post = this.NoWrap
      //? $"{this.GetValidationFeedbackContainer(anId)}{errorMessage}</div>"
      ? $"{errorMessage}</div>"
      : "";
    string preLabel = string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<label class=\"{this.GetCheckboxLabelClasses()}\"><span>";
    string description = this.GetDescription();
    string descriptionHtml = string.IsNullOrEmpty(description) || string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<span class=\"{this.GetCheckboxLabelDescriptionClasses()}\">" +
        $"{description}" +
        $"</span>";
    string postLabel = string.IsNullOrEmpty(aLabel)
      ? ""
      : $"</span>" +
        $"<span class=\"{this.GetCheckboxLabelSpanClasses()}\">{aLabel}</span>" +
        descriptionHtml +
        $"</label>";
    if (pre.Length + preLabel.Length > 0)
    {
      anOutput.PreElement.SetHtmlContent(pre + preLabel);
      anOutput.PostElement.SetHtmlContent(this.GetCheckboxExtraHtml() + postLabel + post);
    }
    else
    {
      anOutput.PostElement.SetHtmlContent(this.GetCheckboxExtraHtml());
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