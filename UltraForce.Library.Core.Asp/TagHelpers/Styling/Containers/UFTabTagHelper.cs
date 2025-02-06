// <copyright file="UFTabTagHelper.cs" company="Ultra Force Development">
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
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Lib;

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Containers;

/// <summary>
/// This tag helper is used to render a tab container. It expects to be used as a child of a
/// <see cref="UFTabsTagHelper"/>.
/// <para>
/// The class renders the following html:
/// <code>
/// &lt;input type="radio" name=".." id="{internal}" class="{GetRadioClasses()}" [checked] /&gt;
/// &lt;label for="{internal}" class="{GetLabelClasses()}"&gt;{Caption}&lt;/label&gt;
/// &lt;div class="{GetContentWrapperClasses()}"&gt;
///   {children}
/// &lt;/div&gt;
/// </code>
/// </para>
/// <remarks>
/// The css classes should use '+' with the selectors.
/// </remarks>
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[HtmlTargetElement("uf-tab")]
public class UFTabTagHelper(IUFTheme aTheme) : UFTagHelperWithTheme(aTheme), IUFTabProperties
{
  #region public properties

  /// <summary>
  /// Caption of the tag. Can be html formatted.
  /// </summary>
  [HtmlAttributeName("caption")]
  public string Caption { get; set; } = "";

  /// <summary>
  /// Initial state of the tab. If true, the tab will be selected. 
  /// </summary>
  [HtmlAttributeName("selected")]
  public bool Selected { get; set; } = false;

  #endregion

  #region public methods

  /// <inheritdoc />
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    base.Process(context, output);
    output.TagName = "div";
    output.TagMode = TagMode.StartTagAndEndTag;
    string id = Guid.NewGuid().ToString();
    string name = context.Items[UFTabsTagHelper.TabsRadioName].ToString()!;
    output.PreElement.AppendHtml(
      this.RenderRadio(name, id) +
      this.RenderLabel(id) +
      $"<div class=\"{this.GetContentWrapperClasses()}\">"
    );
    output.PostElement.AppendHtml("</div>");
  }

  #endregion
  
  #region protected overridable methods
  
  /// <summary>
  /// The default implementation returns the result from <see cref="IUFTheme.GetTabRadioClasses"/>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetRadioClasses()
  {
    return this.Theme.GetTabRadioClasses(this);
  }
  
  /// <summary>
  /// The default implementation returns the result from <see cref="IUFTheme.GetTabLabelClasses"/>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetLabelClasses()
  {
    return this.Theme.GetTabLabelClasses(this);
  }
  
  /// <summary>
  /// The default implementation returns the result from
  /// <see cref="IUFTheme.GetTabContentWrapperClasses"/>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetContentWrapperClasses()
  {
    return this.Theme.GetTabContentWrapperClasses(this);
  }
  
  #endregion

  #region private methods

  /// <summary>
  /// Renders a radio input element, used to select the tab.
  /// </summary>
  /// <param name="aName"></param>
  /// <param name="anId"></param>
  /// <returns></returns>
  private string RenderRadio(string aName, string anId)
  {
    return
      $"<input" +
      $" type=\"radio\"" +
      $" name=\"{aName}\"" +
      $" id=\"{anId}\"" +
      $" class=\"{this.GetRadioClasses()}\"" +
      $" {(this.Selected ? "checked" : "")}" +
      " />";
  }

  /// <summary>
  /// Renders a label element, used to display the caption of the tab.
  /// </summary>
  /// <param name="anId"></param>
  /// <returns></returns>
  private string RenderLabel(string anId)
  {
    return
      $"<label for=\"{anId}\"" +
      $" class=\"{this.GetLabelClasses()}\"" +
      $">{this.Caption}</label>";
  }

  #endregion
}