// <copyright file="UFDataAttribute.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.TagHelpers;
using UltraForce.Library.Core.Asp.Types.Enums;
using UltraForce.Library.NetStandard.Extensions;

namespace UltraForce.Library.Core.Asp.Types.Constants;

/// <summary>
/// This class defines all data attributes used by the typescript DOM library. The values
/// can be used in combination with <see cref="UFAttributesTagHelper"/>
/// </summary>
public static class UFDataAttribute
{
  /// <summary>
  /// Css classes to add to show an element
  /// </summary>
  public static TagHelperAttribute ShowClasses(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-show-classes", value);

  /// <summary>
  /// Css classes to add to hide an element
  /// </summary>
  public static TagHelperAttribute HideClasses(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-hide-classes", value);

  /// <summary>
  /// Value to use for the display style.
  /// </summary>
  public static TagHelperAttribute DisplayValue(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-display-value", value);

  /// <summary>
  /// Add paging support to a table or a grid.
  /// </summary>
  public static TagHelperAttribute Paging(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-paging", value);

  /// <summary>
  /// The number of items to show per page.
  /// </summary>
  public static TagHelperAttribute PageSize(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-page-size", value);

  /// <summary>
  /// Add sorting support to a table.
  /// </summary>
  public static TagHelperAttribute TableSorting(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-table-sorting", value);

  /// <summary>
  /// Add sorting support to a grid.
  /// </summary>
  public static TagHelperAttribute GridSorting(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-grid-sorting", value);

  /// <summary>
  /// Number of items per group
  /// </summary>
  public static TagHelperAttribute GroupSize(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-group-size", value);

  /// <summary>
  /// Added to an element that is used to control the sorting within a grid.
  /// </summary>
  public static TagHelperAttribute SortControl(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-sort-control", value);

  /// <summary>
  /// Add to an element within a grid control that should be used as the button.
  /// </summary>
  public static TagHelperAttribute SortButton(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-sort-button", value);

  /// <summary>
  /// Contains the key linking to a certain sort control.
  /// </summary>
  public static TagHelperAttribute SortKey(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-sort-key", value);

  /// <summary>
  /// Added to an element that contains child elements used to sort with.
  /// </summary>
  public static TagHelperAttribute ItemContainer(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-item-container", value);

  /// <summary>
  /// Added to elements used to sort with.
  /// </summary>
  public static TagHelperAttribute ItemGroup(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-item-group", value);

  /// <summary>
  /// Added to element that contains the sortable items.
  /// </summary>
  public static TagHelperAttribute GridBody(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-grid-body", value);

  /// <summary>
  /// Value to sort on.
  /// </summary>
  public static TagHelperAttribute SortValue(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-sort-value", value);

  /// <summary>
  /// Ignore the filter for this column or grid item.
  /// </summary>
  public static TagHelperAttribute NoFilter(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-no-filter", value);

  /// <summary>
  /// This attribute is added to rows that contain all data elements that don't match the filter.
  /// </summary>
  public static TagHelperAttribute RowNoMatch(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-row-no-match", value);

  /// <summary>
  /// This attribute is added to element that contain all data elements that don't match the filter.
  /// </summary>
  public static TagHelperAttribute FilterNoMatch(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-filter-no-match", value);

  /// <summary>
  /// Add filter support to a table.
  /// </summary>
  public static TagHelperAttribute FilterTable(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-filter-table", value);

  /// <summary>
  /// Add filter support to some container.
  /// </summary>
  public static TagHelperAttribute FilterInput(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-filter-input", value);

  /// <summary>
  /// The group a child within the container belongs to.
  /// </summary>
  public static TagHelperAttribute FilterGroup(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-filter-group", value);

  /// <summary>
  /// A container whose visibility depends on any of the children matching the filter.
  /// </summary>
  public static TagHelperAttribute FilterContainer(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-filter-container", value);

  /// <summary>
  /// Css classes to add the header element or grid control element.
  /// </summary>
  public static TagHelperAttribute SortAscending(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-sort-ascending", value);

  /// <summary>
  /// Css classes to add the header element or grid control element.
  /// </summary>
  public static TagHelperAttribute SortDescending(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-sort-descending", value);

  /// <summary>
  /// Used by various classes.
  /// </summary>
  public static TagHelperAttribute StorageId(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-storage-id", value);

  /// <summary>
  /// Points to a clickable element
  /// </summary>
  public static TagHelperAttribute SetFieldSelector(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-set-field-selector", value);

  /// <summary>
  /// Value to set if clickable element is clicked upon.
  /// </summary>
  public static TagHelperAttribute SetFieldValue(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-set-field-value", value);

  /// <summary>
  /// When set don't cache values with sorted tables.
  /// </summary>
  public static TagHelperAttribute NoCaching(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-no-caching", value);

  /// <summary>
  /// Image preview for file input.
  /// </summary>
  public static TagHelperAttribute ImagePreview(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-image-preview", value);

  /// <summary>
  /// Set content to width of image.
  /// </summary>
  public static TagHelperAttribute ImageWidth(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-image-width", value);

  /// <summary>
  /// Set content to height of image.
  /// </summary>
  public static TagHelperAttribute ImageHeight(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-image-height", value);

  /// <summary>
  /// Set content to name of image.
  /// </summary>
  public static TagHelperAttribute ImageName(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-image-name", value);

  /// <summary>
  /// Set content to size of image.
  /// </summary>
  public static TagHelperAttribute ImageSize(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-image-size", value);

  /// <summary>
  /// Set content to type of image.
  /// </summary>
  public static TagHelperAttribute ImageType(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-image-type", value);

  /// <summary>
  /// Show the content of the field if some condition is met.
  /// </summary>
  public static TagHelperAttribute ShowIfField(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-show-if-field", value);

  /// <summary>
  /// Hide the content of the field if some condition is met.
  /// </summary>
  public static TagHelperAttribute HideIfField(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-hide-if-field", value);

  /// <summary>
  /// Value to use.
  /// </summary>
  public static TagHelperAttribute FieldValue(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-field-value", value);

  /// <summary>
  /// Action to perform for some event
  /// </summary>
  public static TagHelperAttribute EventAction(
    UFEventActionEnum? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute(
    "data-uf-event-action" + (index > 0 ? "-" + index : ""), value?.GetDescription()
  );

  /// <summary>
  /// Events to perform action for
  /// </summary>
  public static TagHelperAttribute EventEvents(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute("data-uf-event-events" + (index > 0 ? "-" + index : ""), value);

  /// <summary>
  /// Css classes to add to show an element
  /// </summary>
  public static TagHelperAttribute EventTarget(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute("data-uf-event-target" + (index > 0 ? "-" + index : ""), value);

  /// <summary>
  /// Data to use for some of the event actions.
  /// </summary>
  public static TagHelperAttribute EventData(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute("data-uf-event-data" + (index > 0 ? "-" + index : ""), value);

  /// <summary>
  /// Attribute to use for the event action.
  /// </summary>
  public static TagHelperAttribute EventAttribute(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute(
    "data-uf-event-attribute" + (index > 0 ? "-" + index : ""), value
  );

  /// <summary>
  /// State to check for with the events.
  /// </summary>
  public static TagHelperAttribute EventState(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute("data-uf-event-state" + (index > 0 ? "-" + index : ""), value);

  /// <summary>
  /// Keyboard key to check for with the events.
  /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/key" />
  /// for possible values.
  /// </summary>
  public static TagHelperAttribute EventKey(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute("data-uf-event-key" + (index > 0 ? "-" + index : ""), value);

  /// <summary>
  /// Prevent default processing of the events.
  /// </summary>
  public static TagHelperAttribute EventPreventDefault(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute(
    "data-uf-event-prevent-default" + (index > 0 ? "-" + index : ""), value
  );

  /// <summary>
  /// Event that is a click action.
  /// </summary>
  public static TagHelperAttribute ClickAction(
    UFEventActionEnum? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute(
    "data-uf-click-action" + (index > 0 ? "-" + index : ""), value?.GetDescription()
  );

  /// <summary>
  /// Target to use for the click action.
  /// </summary>
  public static TagHelperAttribute ClickTarget(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute("data-uf-click-target" + (index > 0 ? "-" + index : ""), value);

  /// <summary>
  /// Data to use for the click action.
  /// </summary>
  public static TagHelperAttribute ClickData(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute("data-uf-click-data" + (index > 0 ? "-" + index : ""), value);

  /// <summary>
  /// Attribute to use for the load action.
  /// </summary>
  public static TagHelperAttribute ClickAttribute(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute("data-uf-load-attribute" + (index > 0 ? "-" + index : ""), value);

  /// <summary>
  /// Event that is a load action.
  /// </summary>
  public static TagHelperAttribute LoadAction(
    UFEventActionEnum? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute(
    "data-uf-load-action" + (index > 0 ? "-" + index : ""), value?.GetDescription()
  );

  /// <summary>
  /// Target to use for the load action.
  /// </summary>
  public static TagHelperAttribute LoadTarget(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute("data-uf-load-target" + (index > 0 ? "-" + index : ""), value);

  /// <summary>
  /// Data to use for the load action.
  /// </summary>
  public static TagHelperAttribute LoadData(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute("data-uf-load-data" + (index > 0 ? "-" + index : ""), value);

  /// <summary>
  /// Attribute to use for the load action.
  /// </summary>
  public static TagHelperAttribute LoadAttribute(
    object? value = null,
    int index = 0
  ) => UFTagHelperTools.Attribute("data-uf-load-attribute" + (index > 0 ? "-" + index : ""), value);

  /// <summary>
  /// Toggle the css class when the target is clicked upon.
  /// </summary>
  public static TagHelperAttribute ClickStylingSelector(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-click-styling-selector", value);

  /// <summary>
  /// Css classes to toggle.
  /// </summary>
  public static TagHelperAttribute ClickStylingClasses(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-click-styling-classes", value);

  /// <summary>
  /// Collaps details when target is clicked upon.
  /// </summary>
  public static TagHelperAttribute DetailsCollapse(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-details-collapse", value);

  /// <summary>
  /// Expands details when target is clicked upon.
  /// </summary>
  public static TagHelperAttribute DetailsExpand(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-details-expand", value);

  /// <summary>
  /// Shows a modal dialog and copies data attributes.
  /// </summary>
  public static TagHelperAttribute ShowDialog(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-show-dialog", value);

  /// <summary>
  /// Toggle element for some form field
  /// </summary>
  public static TagHelperAttribute ToggleType(
    UFToggleTypeEnum? value = null
  ) => UFTagHelperTools.Attribute("data-uf-toggle-type", value?.GetDescription());

  /// <summary>
  /// Property to toggle element for.
  /// </summary>
  public static TagHelperAttribute ToggleProperty(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-toggle-property", value);

  /// <summary>
  /// Selector to toggle element for.
  /// </summary>
  public static TagHelperAttribute ToggleSelector(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-toggle-selector", value);

  /// <summary>
  /// How to change the element.
  /// </summary>
  public static TagHelperAttribute ToggleChange(
    UFToggleChangeEnum? value = null
  ) => UFTagHelperTools.Attribute("data-uf-toggle-change", value?.GetDescription());

  /// <summary>
  /// How to update the required state.
  /// </summary>
  public static TagHelperAttribute ToggleRequired(
    UFToggleRequiredEnum? value = null
  ) => UFTagHelperTools.Attribute("data-uf-toggle-required", value?.GetDescription());

  /// <summary>
  /// Css classes to add or remove.
  /// </summary>
  public static TagHelperAttribute ToggleClasses(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-toggle-classes", value);

  /// <summary>
  /// When the condition is true.
  /// </summary>
  public static TagHelperAttribute ToggleCondition(
    UFToggleConditionEnum? value = null
  ) => UFTagHelperTools.Attribute("data-uf-toggle-condition", value?.GetDescription());

  /// <summary>
  /// How to compare.
  /// </summary>
  public static TagHelperAttribute ToggleCompare(
    UFToggleCompareEnum? value = null
  ) => UFTagHelperTools.Attribute("data-uf-toggle-compare", value?.GetDescription());

  /// <summary>
  /// Value to use.
  /// </summary>
  public static TagHelperAttribute ToggleValue(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-toggle-value", value);

  /// <summary>
  /// Values to use
  /// </summary>
  public static TagHelperAttribute ToggleValues(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-toggle-values", value);

  /// <summary>
  /// Target to toggle.
  /// </summary>
  public static TagHelperAttribute ToggleTarget(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-toggle-target", value);

  /// <summary>
  /// Manage submit buttons state.
  /// </summary>
  public static TagHelperAttribute ManageSubmit(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-manage-submit", value);

  /// <summary>
  /// Refreshes the page when the element is clicked upon.
  /// </summary>
  public static TagHelperAttribute PageRefresh(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-page-refresh", value);

  /// <summary>
  /// Converts a container to a floater.  
  /// </summary>
  public static TagHelperAttribute PopupContent(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-popup-content", value);

  /// <summary>
  /// Selector to toggle element for.
  /// </summary>
  public static TagHelperAttribute PopupPosition(
    UFPopupPositionEnum? value = null
  ) => UFTagHelperTools.Attribute("data-uf-popup-position", value?.GetDescription());

  /// <summary>
  /// Selector to toggle element for.
  /// </summary>
  public static TagHelperAttribute PopupHide(
    UFPopupHideEnum? value = null
  ) => UFTagHelperTools.Attribute("data-uf-popup-hide", value?.GetDescription());

  /// <summary>
  /// Selector to toggle element for.
  /// </summary>
  public static TagHelperAttribute PopupTransition(
    UFPopupTransitionEnum? value = null
  ) => UFTagHelperTools.Attribute("data-uf-popup-transition", value?.GetDescription());

  /// <summary>
  /// Selector to toggle element for.
  /// </summary>
  public static TagHelperAttribute PopupDeltaX(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-delta-x", value);

  /// <summary>
  /// Selector to toggle element for.
  /// </summary>
  public static TagHelperAttribute ToggleDeltaY(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-delta-y", value);

  /// <summary>
  /// Loads page from selector value.
  /// </summary>
  public static TagHelperAttribute SelectUrl(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-select-url", value);

  /// <summary>
  /// Shares hover state.
  /// </summary>
  public static TagHelperAttribute ShareHover(
    object? value = null
  ) => UFTagHelperTools.Attribute("data-uf-share-hover", value);
}