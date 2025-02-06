// <copyright file="UFSelectTagHelper.cs" company="Ultra Force Development">
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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Forms;

/// <summary>
/// Base class to help with rendering a select element. If not `id` is set and
/// <see cref="Wrap"/> is `true`, the code will generate an id so the label can reference the
/// select element.
/// <para>
/// If there are no field errors, the field error block is not rendered.
/// </para>
/// <para>
/// Renders with wrapping:
/// <code>
/// &lt;div class="{GetInputWrapperClasses()}"&gt;<br/>
///   &lt;label class="{GetInputLabelClasses()}" for="{id}"&gt;{label}&lt;/label&gt;<br/>
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
[HtmlTargetElement("uf-select", TagStructure = TagStructure.NormalOrSelfClosing)]
public class UFSelectTagHelper(IHtmlGenerator aGenerator, IUFTheme aTheme)
  : SelectTagHelper(aGenerator), IUFInputProperties
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
  
  /// <summary>
  /// This property has no effect on this tag helper.
  /// </summary>
  [HtmlAttributeNotBound]
  public bool Multiline { get; set; } = false;
  
  /// <inheritdoc />
  [HtmlAttributeName("no-width")]
  public bool NoWidth { get; set; } = false;

  #endregion

  #region protected properties
  
  /// <summary>
  /// Theme to use for styling.
  /// </summary>
  protected IUFTheme Theme { get; } = aTheme;

  #endregion
    
  #region public methods

  /// <inheritdoc />
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    base.Process(context, output);
    output.TagName = "select";
    output.TagMode = TagMode.StartTagAndEndTag;
    string label = this.NoLabel 
      ? "" 
      :  UFTagHelperTools.GetLabel(this.Generator, this.ViewContext, this.For, this.Label);
    if (((this.For != null) || this.Wrap) && (output.Attributes["id"] == null))
    {
      output.Attributes.SetAttribute("id", Guid.NewGuid().ToString());
    }
    string id = (output.Attributes["id"] == null) ? "" : output.Attributes["id"].Value.ToString()!;
    string errorMessage = this.GetFieldErrorsHtml(
      this.ViewContext.ModelState,
      output.Attributes["name"]?.Value?.ToString() ?? ""
    );
    if (this.Wrap)
    {
      this.WrapInput(output, id, label, errorMessage);
    }
    else
    {
      UFTagHelperTools.AddClasses(output, this.GetSelectClasses());
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
    return this.Theme.GetSelectClasses(this);
  }
  
  /// <summary>
  /// Returns css classes for the label element.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetTextLabelClasses(string aType)
  {
    return this.Theme.GetTextLabelClasses(this, aType);
  }
  
  
  /// <summary>
  /// Returns css classes for the wrapper element.
  /// </summary>
  /// <param name="aType"></param>
  /// <returns></returns>
  protected virtual string GetTextWrapperClasses(string aType)
  {
    return this.Theme.GetTextWrapperClasses(this, aType);
  }
  
  /// <summary>
  /// Returns the html for the validation feedback container.
  /// </summary>
  /// <param name="anId"></param>
  /// <returns></returns>
  protected virtual string GetValidationFeedbackContainerHtml(string anId)
  {
    return this.Theme.GetValidationFeedbackContainerHtml(anId);
  }
  
  /// <summary>
  /// Returns the css classes for the field errors block.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetFieldErrorsClasses()
  {
    return this.Theme.GetFieldErrorsClasses();
  }

  /// <summary>
  /// Returns the html for the field errors block.
  /// </summary>
  /// <param name="states"></param>
  /// <param name="name"></param>
  /// <returns></returns>
  protected virtual string GetFieldErrorsHtml(ModelStateDictionary states, string name)
  {
    return this.Theme.GetFieldErrorsHtml(states, name);
  }
  
  #endregion

  #region private methods

  /// <summary>
  /// Wraps an element The elements gets wrapped in a div, a label and an error info block.
  /// </summary>
  /// <param name="anOutput">Output to wrap</param>
  /// <param name="anId">Id of input element</param>
  /// <param name="aLabel">Label text to use</param>
  /// <param name="anErrorMessage">Error message to show</param>
  private void WrapInput(
    TagHelperOutput anOutput, string anId, string aLabel, string anErrorMessage
  )
  {
    string errorMessage = anErrorMessage != ""
      ? $"<div class=\"{this.GetFieldErrorsClasses()}\">{anErrorMessage}</div>"
      : "";
    UFTagHelperTools.AddClasses(anOutput, this.GetSelectClasses());
    string labelHtml = string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<label class=\"{this.GetTextLabelClasses("select")}\" for=\"{anId}\">{aLabel}</label>";
    anOutput.PreElement.SetHtmlContent(
      $"<div class=\"{this.GetTextWrapperClasses("select")}\">{labelHtml}"
    );
    anOutput.PostElement.SetHtmlContent(
      $"{this.GetValidationFeedbackContainerHtml(anId)}{errorMessage}</div>"
    );
  }

  #endregion
}