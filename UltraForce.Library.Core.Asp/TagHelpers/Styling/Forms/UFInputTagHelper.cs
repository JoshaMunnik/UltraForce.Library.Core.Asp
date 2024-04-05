// <copyright file="UFInputTagHelper.cs" company="Ultra Force Development">
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
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.NetStandard.Annotations;
using UltraForce.Library.NetStandard.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Forms;

/// <summary>
/// Renders either a text input, a checkbox or a radio button. If no `id` is set and
/// <see cref="Wrap"/> is `true` the class will generate an id so the label can reference the
/// input element.
/// <para>
/// If there are no field errors, the field error block is not rendered.
/// </para>
/// <para>
/// Renders text input with wrapping and label:
/// <code>
/// &lt;div class="{Theme.GetTextWrapperClasses(type)}"&gt;
/// &lt;label class="{Theme.GetTextLabelClasses(type)}" for="{id}"&gt;
/// &lt;span class="{Theme.GetTextLabelSpanClasses(type)}"&gt;{label}&lt;/span&gt;
/// &lt;span class="{Theme.GetTextLabelDescriptionClasses(type)}"&gt;{description}&lt;/span&gt;
/// &lt;/label&gt;
/// &lt;input class="{Theme.GetTextInputClasses(type)}" id={} .../&gt;
/// {Theme.GetValidationFeedbackContainer(id)}
/// {RenderFieldErrors(...)}
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders text input with wrapping and no label:
/// <code>
/// &lt;div class="{Theme.GetTextWrapperClasses(type)}"&gt;
/// &lt;input class="{Theme.GetTextInputClasses(type)}" id={} .../&gt;
/// {Theme.GetValidationFeedbackContainer(id)}
/// &lt;div class="{Theme.GetFieldErrorsClasses()}"&gt;{Theme.GetFieldErrorsHtml()}&lt;/div&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders text input without wrapping:
/// <code>
/// &lt;input class="{Theme.GetTextInputClasses(type) .../&gt;
/// </code>
/// </para>
/// <para>
/// Renders multiline text input with wrapping and label:
/// <code>
/// &lt;div class="{Theme.GetTextWrapperClasses(type)}"&gt;
/// &lt;label class="{Theme.GetTextLabelClasses(type)}" for="{id}"&gt;
/// &lt;span class="{Theme.GetTextLabelSpanClasses(type)}"&gt;{label}&lt;/span&gt;
/// &lt;span class="{Theme.GetTextLabelDescriptionClasses(type)}"&gt;{description}&lt;/span&gt;
/// &lt;/label&gt;
/// &lt;textarea class="{Theme.GetTextInputClasses(type)}" id={} ..." &gt;{value}&lt;/textarea&gt;
/// {Theme.GetValidationFeedbackContainer(id)}
/// {RenderFieldErrors(...)}
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders multiline text input with wrapping and no label:
/// <code>
/// &lt;div class="{Theme.GetTextWrapperClasses(type)}"&gt;
/// &lt;textarea class="{Theme.GetTextInputClasses(type)}" id={} ..." &gt;{value}&lt;/textarea&gt;
/// {Theme.GetValidationFeedbackContainer(id)}
/// &lt;div class="{Theme.GetFieldErrorsClasses()}"&gt;{Theme.GetFieldErrorsHtml()}&lt;/div&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders multiline text input without wrapping:
/// <code>
/// &lt;textarea class="{Theme.GetTextInputClasses(type)}" id={} ..." &gt;{value}&lt;/textarea&gt;
/// </code>
/// </para>
/// <para>
/// Renders radio input with wrapping and a label:
/// <code>
/// &lt;div class="{Theme.GetRadioWrapperClasses()}"&gt;
/// &lt;label class="{Theme.GetRadioLabelClasses()}"&gt;
/// {Theme.GetRadioExtraHtml()}
/// &lt;span class="{Theme.GetRadioLabelSpanClasses()}&gt;{label}&lt;/span&gt;
/// &lt;span class="{Theme.GetRadioLabelDescriptionClasses()}"&gt;{description}&lt;/span&gt;
/// &lt;/label&gt;
/// {Theme.GetValidationFeedbackContainer(id)}
/// &lt;div class="{Theme.GetFieldErrorsClasses()}"&gt;{Theme.GetFieldErrorsHtml()}&lt;/div&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders radio input with wrapping but no label:
/// <code>
/// &lt;div class="{Theme.GetRadioWrapperClasses()}"&gt;
/// &lt;input type="radio" class="{Theme.GetRadioInputClasses()}" id={} .../&gt;
/// {Theme.GetRadioExtraHtml()}
/// {Theme.GetValidationFeedbackContainer(id)}
/// &lt;div class="{Theme.GetFieldErrorsClasses()}"&gt;{Theme.GetFieldErrorsHtml()}&lt;/div&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders radio input without wrapping but with a label:
/// <code>
/// &lt;label class="{Theme.GetRadioLabelClasses()}"&gt;
/// &lt;input type="radio" class="{Theme.GetRadioInputClasses()}" id={} .../&gt;
/// {Theme.GetRadioExtraHtml()}
/// &lt;span class="{Theme.GetRadioLabelSpanClasses()}&gt;{label}&lt;/span&gt;
/// &lt;span class="{Theme.GetRadioLabelDescriptionClasses()}"&gt;{description}&lt;/span&gt;
/// &lt;/label&gt;
/// </code>
/// </para>
/// <para>
/// Renders radio input without wrapping and without a label:
/// <code>
/// &lt;input type="radio" class="{Theme.GetRadioInputClasses()}" id={} .../&gt;
/// {Theme.GetRadioExtraHtml()}
/// </code>
/// </para>
/// <para>
/// Renders checkbox input with wrapping and a label:
/// <code>
/// &lt;div class="{Theme.GetCheckboxWrapperClasses()}"&gt;
/// &lt;label class="{Theme.GetCheckboxLabelClasses()}"&gt;
/// &lt;input type="checkbox" class="{Theme.GetCheckboxInputClasses()}" id={} .../&gt;
/// {Theme.GetCheckboxExtraHtml()}
/// &lt;span class="{Theme.GetCheckboxLabelSpanClasses()}&gt;{label}&lt;/span&gt;
/// &lt;span class="{Theme.GetCheckboxLabelDescriptionClasses()}"&gt;{description}&lt;/span&gt;
/// &lt;/label&gt;
/// {Theme.GetValidationFeedbackContainer(id)}
/// &lt;div class="{Theme.GetFieldErrorsClasses()}"&gt;{Theme.GetFieldErrorsHtml()}&lt;/div&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders checkbox input with wrapping but no label:
/// <code>
/// &lt;div class="{Theme.GetCheckboxWrapperClasses()}"&gt;
/// &lt;input type="checkbox" class="{Theme.GetCheckboxInputClasses()}" id={} .../&gt;
/// {Theme.GetCheckboxExtraHtml()}
/// {Theme.GetValidationFeedbackContainer(id)}
/// &lt;div class="{Theme.GetFieldErrorsClasses()}"&gt;{Theme.GetFieldErrorsHtml()}&lt;/div&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Renders checkbox input without wrapping but with a label:
/// <code>
/// &lt;label class="{Theme.GetCheckboxLabelClasses()}"&gt;
/// &lt;input type="checkbox" class="{Theme.GetCheckboxInputClasses()}" id={} ... /&gt;
/// {Theme.GetCheckboxExtraHtml()}
/// &lt;span class="{Theme.GetCheckboxLabelSpanClasses()}&gt;{label}&lt;/span&gt;
/// &lt;span class="{Theme.GetCheckboxLabelDescriptionClasses()}"&gt;{description}&lt;/span&gt;
/// &lt;/label&gt;
/// </code>
/// </para>
/// <para>
/// Renders checkbox input without wrapping and without a label:
/// <code>
/// &lt;input type="checkbox" class="{Theme.GetCheckboxInputClasses()}" id={} .../&gt;
/// {Theme.GetCheckboxExtraHtml()}
/// </code>
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[HtmlTargetElement("uf-input")]
public class UFInputTagHelper(IHtmlGenerator generator, IUFTheme aTheme)
  : InputTagHelper(generator), IUFInputProperties
{
  #region IUFInputProperties

  /// <inheritdoc />
  [HtmlAttributeName("label")]
  public string Label { get; set; } = "";

  /// <inheritdoc />
  [HtmlAttributeName("no-label")]
  public bool NoLabel { get; set; } = false;

  /// <inheritdoc />
  [HtmlAttributeName("wrap")]
  public bool Wrap { get; set; } = true;

  /// <inheritdoc />
  [HtmlAttributeName("description")]
  public string Description { get; set; } = "";

  /// <inheritdoc />
  [HtmlAttributeName("multiline")]
  public bool Multiline { get; set; } = false;

  /// <inheritdoc />
  [HtmlAttributeName("no-width")]
  public bool NoWidth { get; set; } = false;

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
    if (((this.For != null) || this.Wrap) && (output.Attributes["id"] == null))
    {
      output.Attributes.SetAttribute("id", Guid.NewGuid().ToString());
    }
    string id = (output.Attributes["id"] == null) ? "" : output.Attributes["id"].Value.ToString()!;
    string type = output.Attributes["type"]?.Value?.ToString()?.ToLowerInvariant() ?? "";
    string errorMessage = this.Theme.GetFieldErrorsHtml(
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
        if (this.Wrap)
        {
          this.RenderWrappedInput(output, id, label, type, errorMessage);
        }
        else
        {
          UFTagHelperTools.AddClasses(output, this.Theme.GetTextInputClasses(this, type));
        }
        break;
    }
  }

  #endregion

  #region protected properties

  protected IUFTheme Theme { get; } = aTheme;

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
      ? $"<div class=\"{this.Theme.GetFieldErrorsClasses()}\">{anErrorMessage}</div>"
      : "";
  }

  /// <summary>
  /// Wraps an element The elements gets wrapped in a div, a label and an error info block.
  /// </summary>
  /// <param name="anOutput">Output to wrap</param>
  /// <param name="anId">Id of input element</param>
  /// <param name="aLabel">Label text to use</param>
  /// <param name="anErrorMessage">Error message to show</param>
  private void RenderWrappedInput(
    TagHelperOutput anOutput, string anId, string aLabel, string aType, string anErrorMessage
  )
  {
    string errorMessage = this.GetErrorMessageHtml(anErrorMessage);
    UFTagHelperTools.AddClasses(
      anOutput, this.Theme.GetTextInputClasses(this, aType)
    );
    string description = this.GetDescription();
    string descriptionHtml = string.IsNullOrEmpty(description) || string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<span class=\"{this.Theme.GetTextLabelDescriptionClasses(this, aType)}\">" +
        $"{description}</span>";
    string labelHtml = string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<label class=\"{this.Theme.GetTextLabelClasses(this, aType)}\" for=\"{anId}\">" +
        $"<span class=\"{this.Theme.GetTextLabelSpanClasses(this, aType)}\">{aLabel}</span>" +
        descriptionHtml +
        "</label>";
    anOutput.PreElement.SetHtmlContent(
      $"<div class=\"{this.Theme.GetTextWrapperClasses(this, aType)}\">{labelHtml}"
    );
    anOutput.PostElement.SetHtmlContent(
      $"{this.Theme.GetValidationFeedbackContainerHtml(anId)}{errorMessage}</div>"
    );
  }

  private void RenderRadio(
    TagHelperOutput anOutput, string anId, string aLabel, string anErrorMessage
  )
  {
    string errorMessage = this.GetErrorMessageHtml(anErrorMessage);
    UFTagHelperTools.AddClasses(anOutput, this.Theme.GetRadioInputClasses());
    string pre = this.Wrap ? $"<div class=\"{this.Theme.GetRadioWrapperClasses()}\">" : "";
    string post = this.Wrap
      //? $"{this.GetValidationFeedbackContainer(anId)}{errorMessage}</div>"
      ? $"{errorMessage}</div>"
      : "";
    string preLabel = string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<label class=\"{this.Theme.GetRadioLabelClasses()}\"><span>";
    string description = this.GetDescription();
    string descriptionHtml = string.IsNullOrEmpty(description) || string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<span class=\"{this.Theme.GetRadioLabelDescriptionClasses()}\">" +
        $"{description}" +
        $"</span>";
    string postLabel = string.IsNullOrEmpty(aLabel)
      ? ""
      : $"</span>" +
        $"<span class=\"{this.Theme.GetRadioLabelSpanClasses()}\">{aLabel}</span>" +
        descriptionHtml +
        $"</label>";
    if (pre.Length + preLabel.Length > 0)
    {
      anOutput.PreElement.SetHtmlContent(pre + preLabel);
      anOutput.PostElement.SetHtmlContent(this.Theme.GetRadioExtraHtml() + postLabel + post);
    }
    else
    {
      anOutput.PostElement.SetHtmlContent(this.Theme.GetRadioExtraHtml());
    }
  }

  private void RenderCheckbox(
    TagHelperOutput anOutput, string anId, string aLabel, string anErrorMessage
  )
  {
    string errorMessage = this.GetErrorMessageHtml(anErrorMessage);
    UFTagHelperTools.AddClasses(anOutput, this.Theme.GetCheckboxInputClasses());
    string pre = this.Wrap ? $"<div class=\"{this.Theme.GetCheckboxWrapperClasses()}\">" : "";
    string post = this.Wrap
      //? $"{this.GetValidationFeedbackContainer(anId)}{errorMessage}</div>"
      ? $"{errorMessage}</div>"
      : "";
    string preLabel = string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<label class=\"{this.Theme.GetCheckboxLabelClasses()}\"><span>";
    string description = this.GetDescription();
    string descriptionHtml = string.IsNullOrEmpty(description) || string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<span class=\"{this.Theme.GetCheckboxLabelDescriptionClasses()}\">" +
        $"{description}" +
        $"</span>";
    string postLabel = string.IsNullOrEmpty(aLabel)
      ? ""
      : $"</span>" +
        $"<span class=\"{this.Theme.GetCheckboxLabelSpanClasses()}\">{aLabel}</span>" +
        descriptionHtml +
        $"</label>";
    if (pre.Length + preLabel.Length > 0)
    {
      anOutput.PreElement.SetHtmlContent(pre + preLabel);
      anOutput.PostElement.SetHtmlContent(this.Theme.GetCheckboxExtraHtml() + postLabel + post);
    }
    else
    {
      anOutput.PostElement.SetHtmlContent(this.Theme.GetCheckboxExtraHtml());
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