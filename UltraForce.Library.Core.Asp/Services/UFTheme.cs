// <copyright file="UFTheme.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Mvc.ModelBinding;
using UltraForce.Library.Core.Asp.TagHelpers.Styling.Buttons;
using UltraForce.Library.Core.Asp.TagHelpers.Styling.Containers;
using UltraForce.Library.Core.Asp.TagHelpers.Styling.Forms;
using UltraForce.Library.Core.Asp.TagHelpers.Styling.Layout;
using UltraForce.Library.Core.Asp.TagHelpers.Styling.Table;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Enums;

namespace UltraForce.Library.Core.Asp.Services;

/// <summary>
/// <see cref="UFTheme"/> can be used as a base class for themes.
/// <para>
/// It returns empty strings for html and css classes methods. 
/// </para>
/// </summary>
public class UFTheme : IUFTheme
{
  #region IUFTheme

  /// <inheritdoc />
  public virtual string GetButtonClasses(IUFButtonProperties aProperties)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetButtonCaptionClasses(IUFButtonProperties aProperties)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetButtonIconHtml(IUFButtonProperties aProperties)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetContainerClasses(IUFContainerProperties aProperties)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTabsClasses(int aCount)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTabRadioClasses(IUFTabProperties aProperties)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTabLabelClasses(IUFTabProperties aProperties)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTabContentWrapperClasses(IUFTabProperties aProperties)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetStackClasses(
    IUFContainerProperties aProperties
  )
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetStackItemClasses(
    IUFStackItemProperties aProperties
  )
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetDataListClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetDataNameClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetDataValueClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTextWrapperClasses(IUFInputProperties aProperties, string aType)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTextLabelClasses(IUFInputProperties aProperties, string aType)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTextLabelSpanClasses(IUFInputProperties aProperties, string aType)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTextLabelDescriptionClasses(IUFInputProperties aProperties, string aType)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTextInputClasses(IUFInputProperties aProperties, string aType)
  {
    return "";
  }

  /// <summary>
  /// Returns the html for the validation feedback container. The default implementation returns:
  /// &lt;span class="{GetValidationFeedbackClasses()}" data-valmsg-for="{anId}" data-valmsg-replace="true"&gt;&lt;/span&gt;
  /// </summary>
  /// <param name="anId"></param>
  /// <returns></returns>
  public virtual string GetValidationFeedbackContainerHtml(string anId)
  {
    return
      $"<span class=\"{this.GetValidationFeedbackClasses()}\" data-valmsg-for=\"{anId}\" data-valmsg-replace=\"true\"></span>";
  }

  /// <summary>
  /// Renders the errors for an input field. The default implementation calls
  /// <see cref="UFMvcTools.GetErrors"/> using `br`-tag as separator.
  /// </summary>
  /// <param name="aModelState"></param>
  /// <param name="aName"></param>
  /// <returns></returns>
  public virtual string GetFieldErrorsHtml(ModelStateDictionary aModelState, string aName)
  {
    return UFMvcTools.GetErrors(aModelState, aName, "<br/>");
  }

  /// <inheritdoc />
  public virtual string GetFieldErrorsClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetCheckboxWrapperClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetCheckboxLabelClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetCheckboxLabelSpanClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetCheckboxLabelDescriptionClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetCheckboxInputClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetCheckboxExtraHtml()
  {
    return "";
  }

  /// <summary>
  /// The default implementation returns the value of <see cref="GetCheckboxWrapperClasses"/>.
  /// </summary>
  /// <returns></returns>
  public virtual string GetRadioWrapperClasses()
  {
    return this.GetCheckboxWrapperClasses();
  }

  /// <summary>
  /// The default implementation returns the value of <see cref="GetCheckboxLabelClasses"/>.
  /// </summary>
  /// <returns></returns>
  public virtual string GetRadioLabelClasses()
  {
    return this.GetCheckboxLabelClasses();
  }

  /// <summary>
  /// The default implementation returns the value of <see cref="GetCheckboxLabelSpanClasses"/>.
  /// </summary>
  /// <returns></returns>
  public virtual string GetRadioLabelSpanClasses()
  {
    return this.GetCheckboxLabelSpanClasses();
  }

  /// <summary>
  /// The default implementation returns the value of
  /// <see cref="GetCheckboxLabelDescriptionClasses"/>.
  /// </summary>
  /// <returns></returns>
  public virtual string GetRadioLabelDescriptionClasses()
  {
    return this.GetCheckboxLabelDescriptionClasses();
  }

  /// <summary>
  /// The default implementation returns the value of <see cref="GetCheckboxInputClasses"/>.
  /// </summary>
  /// <returns></returns>
  public virtual string GetRadioInputClasses()
  {
    return this.GetCheckboxInputClasses();
  }

  /// <summary>
  /// The default implementation returns the value of <see cref="GetCheckboxExtraHtml"/>.
  /// </summary>
  /// <returns></returns>
  public virtual string GetRadioExtraHtml()
  {
    return this.GetCheckboxExtraHtml();
  }

  /// <summary>
  /// The default implementation returns the value of <see cref="GetTextInputClasses"/>. 
  /// </summary>
  /// <returns></returns>
  public virtual string GetSelectClasses(IUFInputProperties aProperties)
  {
    return this.GetTextInputClasses(aProperties, "select");
  }

  /// <inheritdoc />
  public virtual string GetSpacerClasses(IUFSpacerProperties aProperties)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetColumnClasses(IUFFlexProperties aProperties)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetRowClasses(IUFFlexProperties aProperties)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTableClasses(IUFTableProperties aProperties)
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTableCellClasses(
    IUFCellProperties aProperties, UFTableCellType aType, IUFTableProperties aTableProperties,
    IUFTableRowProperties aRowProperties
  )
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTableHeaderButtonClasses(
    IUFCellProperties aProperties, IUFTableProperties aTableProperties,
    IUFTableRowProperties aRowProperties
  )
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetTableRowClasses(
    IUFTableRowProperties aProperties, IUFTableProperties aTableProperties
  )
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetFilterContainerClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetFilterInputClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetFilterButtonClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetSortAscendingClasses()
  {
    return "";
  }

  /// <inheritdoc />
  public virtual string GetSortDescendingClasses()
  {
    return "";
  }

  /// <summary>
  /// The default implementation returns true if the value does not end with:
  /// "px", "pt", "em", "rem" or "%".
  /// </summary>
  /// <param name="aValue"></param>
  /// <returns></returns>
  public virtual bool IsCssClass(string aValue)
  {
    return
      !aValue.EndsWith("px", StringComparison.OrdinalIgnoreCase) &&
      !aValue.EndsWith("pt", StringComparison.OrdinalIgnoreCase) &&
      !aValue.EndsWith("em", StringComparison.OrdinalIgnoreCase) &&
      !aValue.EndsWith("rem", StringComparison.OrdinalIgnoreCase) &&
      !aValue.EndsWith("%", StringComparison.OrdinalIgnoreCase);
  }

  #endregion

  #region overridable protected methods

  /// <summary>
  /// Returns css classes to use for the validation feedback container. The default implementation
  /// returns the value of <see cref="GetFieldErrorsClasses"/>. 
  /// </summary>
  /// <returns></returns>
  protected virtual string GetValidationFeedbackClasses()
  {
    return this.GetFieldErrorsClasses();
  }

  #endregion
}