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
/// Renders with wrapping:<br/>
/// &lt;div class="{Theme.GetInputWrapperClasses()}"&gt;<br/>
/// &lt;label class="{Theme.GetInputLabelClasses()}" for="{id}"&gt;{label}&lt;/label&gt;<br/>
/// &lt;select class="{Theme.GetSelectClasses() id={} ..." &gt;<br/>
/// {children}<br/>
/// &lt;/select&gt;<br/>
/// {Theme.GetValidationFeedbackContainer(id)}<br/>
/// &lt;div class="{Theme.GetFieldErrorsClasses()}"&gt;{Theme.GetFieldErrorsHtml()}&lt;/div&gt;<br/>
/// &lt;/div&gt;
/// </para>
/// <para>
/// Renders without wrapping:<br/>
/// &lt;select class="{Theme.GetSelectClasses()}" &gt;<br/>
/// {children}<br/>
/// &lt;/select&gt;
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
    string errorMessage = this.Theme.GetFieldErrorsHtml(
      this.ViewContext.ModelState,
      output.Attributes["name"]?.Value?.ToString() ?? ""
    );
    if (this.Wrap)
    {
      this.WrapInput(output, id, label, errorMessage);
    }
    else
    {
      UFTagHelperTools.AddClasses(output, this.Theme.GetSelectClasses(this));
    }
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
      ? $"<div class=\"{this.Theme.GetFieldErrorsClasses()}\">{anErrorMessage}</div>"
      : "";
    UFTagHelperTools.AddClasses(anOutput, this.Theme.GetSelectClasses(this));
    string labelHtml = string.IsNullOrEmpty(aLabel)
      ? ""
      : $"<label class=\"{this.Theme.GetTextLabelClasses(this, "select")}\" for=\"{anId}\">{aLabel}</label>";
    anOutput.PreElement.SetHtmlContent(
      $"<div class=\"{this.Theme.GetTextWrapperClasses(this, "select")}\">{labelHtml}"
    );
    anOutput.PostElement.SetHtmlContent(
      $"{this.Theme.GetValidationFeedbackContainerHtml(anId)}{errorMessage}</div>"
    );
  }

  #endregion
}