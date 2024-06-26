// <copyright file="IUFTheme.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.Types.Enums;

namespace UltraForce.Library.Core.Asp.Services;

/// <summary>
/// <see cref="IUFTheme" /> generates css classes and html for the different components.
/// </summary>
public interface IUFTheme
{
  #region button styling

  /// <summary>
  /// Gets the css classes to use for the button element.
  /// </summary>
  /// <param name="aProperties">The button property values</param>
  /// <returns>One or more css classes to style the button.</returns>
  string GetButtonClasses(IUFButtonProperties aProperties);

  /// <summary>
  /// Gets the css classes to use for the caption element within a button.
  /// </summary>
  /// <param name="aProperties">The button property values</param>
  /// <returns>One or more css classes to style the caption.</returns>
  string GetButtonCaptionClasses(IUFButtonProperties aProperties);

  /// <summary>
  /// Gets the html for an icon.
  /// </summary>
  /// <returns>Html snippet or empty string if there is no icon</returns>
  string GetButtonIconHtml(IUFButtonProperties aProperties);

  #endregion

  #region container styling

  /// <summary>
  /// Gets the css classes to use for the container element.
  /// </summary>
  /// <param name="aProperties">The container property values</param>
  /// <returns>One or more css classes to style the container.</returns>
  string GetContainerClasses(IUFContainerProperties aProperties);

  /// <summary>
  /// Gets the css classes to use for the tabs container element.
  /// </summary>
  /// <param name="aCount">Number of tabs the container contains</param>
  /// <returns></returns>
  string GetTabsClasses(int aCount);

  /// <summary>
  /// Gets the css classes to use for the radio element with a tab.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <returns></returns>
  string GetTabRadioClasses(IUFTabProperties aProperties);

  /// <summary>
  /// Gets the css classes to use for the label element with a tab.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <returns></returns>
  string GetTabLabelClasses(IUFTabProperties aProperties);

  /// <summary>
  /// Gets the css classes to use for the content wrapper element with a tab.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <returns></returns>
  string GetTabContentWrapperClasses(IUFTabProperties aProperties);

  #endregion

  #region data list styling

  /// <summary>
  /// Gets the css classes to use for the data list element.
  /// </summary>
  /// <returns>One or more css classes to style the data list.</returns>
  string GetDataListClasses();

  /// <summary>
  /// Gets the css classes to use for the data name element.
  /// </summary>
  /// <returns>One or more css classes to style the data name.</returns>
  string GetDataNameClasses();

  /// <summary>
  /// Gets the css classes to use for the data value element.
  /// </summary>
  /// <returns>One or more css classes to style the data value.</returns>
  string GetDataValueClasses();

  #endregion

  #region form styling

  /// <summary>
  /// Returns the css classes to use with the wrapper element for text input and select elements.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <param name="aType">Type the input is created for</param>
  /// <returns></returns>
  string GetTextWrapperClasses(IUFInputProperties aProperties, string aType);

  /// <summary>
  /// Returns the css classes to use with the label for text input and select elements.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <param name="aType">Type the input is created for</param>
  /// <returns></returns>
  string GetTextLabelClasses(IUFInputProperties aProperties, string aType);

  /// <summary>
  /// Returns the css classes to use with the span within the label for text input and select
  /// elements.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <param name="aType">Type the input is created for</param>
  /// <returns></returns>
  string GetTextLabelSpanClasses(IUFInputProperties aProperties, string aType);

  /// <summary>
  /// Returns the css classes to use with the description within label for text input and select
  /// elements.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <param name="aType">Type the input is created for</param>
  /// <returns></returns>
  string GetTextLabelDescriptionClasses(IUFInputProperties aProperties, string aType);

  /// <summary>
  /// Returns the css classes to use with the text input element.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <param name="aType">Type the input is created for</param>
  /// <returns></returns>
  string GetTextInputClasses(IUFInputProperties aProperties, string aType);

  /// <summary>
  /// Returns the html for the validation feedback container (used by javascript validation). 
  /// </summary>
  /// <param name="anId"></param>
  /// <returns></returns>
  string GetValidationFeedbackContainerHtml(string anId);

  /// <summary>
  /// Gets the errors for an input field as an html block. The errors will be wrapped in a
  /// container, see <see cref="GetFieldErrorsClasses()"/>.
  /// </summary>
  /// <param name="aModelState"></param>
  /// <param name="aName"></param>
  /// <returns></returns>
  string GetFieldErrorsHtml(ModelStateDictionary aModelState, string aName);

  /// <summary>
  /// Returns the css classes to use for the wrapper of the errors.
  /// </summary>
  /// <returns></returns>
  string GetFieldErrorsClasses();

  /// <summary>
  /// Returns the css classes to use for the wrapper of a checkbox element.
  /// </summary>
  /// <returns></returns>
  string GetCheckboxWrapperClasses();

  /// <summary>
  /// Returns the css classes to use for the label of a checkbox element. 
  /// </summary>
  /// <returns></returns>
  string GetCheckboxLabelClasses();

  /// <summary>
  /// Returns the css classes to use for the text within the checkbox label element.
  /// </summary>
  /// <returns></returns>
  string GetCheckboxLabelSpanClasses();

  /// <summary>
  /// Returns the css classes to use for the description within the checkbox label element.
  /// </summary>
  /// <returns></returns>
  string GetCheckboxLabelDescriptionClasses();

  /// <summary>
  /// Returns the css classes to use for the checkbox input element.
  /// </summary>
  /// <returns></returns>
  string GetCheckboxInputClasses();

  /// <summary>
  /// Gets the extra html to render after the checkbox input element. It can be used when
  /// using custom graphics.
  /// </summary>
  /// <returns></returns>
  string GetCheckboxExtraHtml();

  /// <summary>
  /// Returns the css classes to use for the wrapper element of a radio element.
  /// </summary>
  /// <returns></returns>
  string GetRadioWrapperClasses();

  /// <summary>
  /// Returns the css classes to use for the label of a radio element. 
  /// </summary>
  /// <returns></returns>
  string GetRadioLabelClasses();

  /// <summary>
  /// Returns the css classes to use for the text within the radio label element.
  /// </summary>
  /// <returns></returns>
  string GetRadioLabelSpanClasses();

  /// <summary>
  /// Returns the css classes to use for the description within the radio label element.
  /// </summary>
  /// <returns></returns>
  string GetRadioLabelDescriptionClasses();

  /// <summary>
  /// Returns the css classes to use for the radio input element. 
  /// </summary>
  /// <returns></returns>
  string GetRadioInputClasses();

  /// <summary>
  /// Gets the extra html to render after the radio input element. It can be used when
  /// using custom graphics.
  /// </summary>
  /// <returns></returns>
  string GetRadioExtraHtml();

  /// <summary>
  /// Returns the css classes to use for the select element. 
  /// </summary>
  /// <returns></returns>
  string GetSelectClasses(IUFInputProperties aProperties);

  #endregion

  #region layout

  /// <summary>
  /// Returns the css classes to use for the spacer element.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <returns></returns>
  string GetSpacerClasses(IUFSpacerProperties aProperties);

  /// <summary>
  /// Returns the css classes to use for the column element.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <returns></returns>
  string GetColumnClasses(IUFFlexProperties aProperties);

  /// <summary>
  /// Returns the css classes to use for the row element.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <returns></returns>
  string GetRowClasses(IUFFlexProperties aProperties);

  #endregion

  #region tables

  /// <summary>
  /// Returns the css classes to use for the table element.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <returns></returns>
  string GetTableClasses(IUFTableProperties aProperties);

  /// <summary>
  /// Returns the css classes to use for the table cell (td/th) element.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <param name="aType"></param>
  /// <param name="aTableProperties"></param>
  /// <param name="aRowProperties"></param>
  /// <returns></returns>
  string GetTableCellClasses(
    IUFCellProperties aProperties, UFTableCellType aType, IUFTableProperties aTableProperties,
    IUFTableRowProperties aRowProperties
  );

  /// <summary>
  /// Returns the css classes to use for the button element within a table header cell that is
  /// added when the table is sortable. 
  /// </summary>
  /// <param name="aProperties"></param>
  /// <param name="aTableProperties"></param>
  /// <param name="aRowProperties"></param>
  /// <returns></returns>
  string GetTableHeaderButtonClasses(
    IUFCellProperties aProperties, IUFTableProperties aTableProperties,
    IUFTableRowProperties aRowProperties
  );

  /// <summary>
  /// Returns the css classes to use for the table row (tr) element.
  /// </summary>
  /// <param name="aProperties"></param>
  /// <param name="aTableProperties"></param>
  /// <returns></returns>
  string GetTableRowClasses(IUFTableRowProperties aProperties, IUFTableProperties aTableProperties);

  /// <summary>
  /// Returns the css classes to use for the filter container element shown above the table if
  /// <see cref="UFTableTagHelper.Filter"/> is true.
  /// </summary>
  /// <returns></returns>
  string GetFilterContainerClasses();

  /// <summary>
  /// Returns the css classes to use for input element within the filter container.
  /// </summary>
  /// <returns></returns>
  string GetFilterInputClasses();

  /// <summary>
  /// Returns the css classes to use for button element within the filter container.
  /// </summary>
  /// <returns></returns>
  string GetFilterButtonClasses();

  /// <summary>
  /// Returns the css classes to use for a table header element when the rows are sorted in
  /// an ascending order for that column.
  /// <para>
  /// The css classes are added to the result from <see cref="GetTableRowClasses"/>.
  /// </para>
  /// </summary>
  /// <returns></returns>
  string GetSortAscendingClasses();

  /// <summary>
  /// Returns the css classes to use for a table header element when the rows are sorted in
  /// an descending order for that column.
  /// <para>
  /// The css classes are added to the result from <see cref="GetTableRowClasses"/>.
  /// </para>
  /// </summary>
  /// <returns></returns>
  string GetSortDescendingClasses();

  #endregion

  #region support methods

  /// <summary>
  /// Checks if a value is a css class or unit value (to be set within the style attribute).
  /// </summary>
  /// <param name="aValue"></param>
  /// <returns></returns>
  bool IsCssClass(string aValue);

  #endregion
}