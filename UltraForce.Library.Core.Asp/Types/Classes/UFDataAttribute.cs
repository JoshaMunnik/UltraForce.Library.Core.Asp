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

using UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

namespace UltraForce.Library.Core.Asp.Types.Classes;

/// <summary>
/// Constants mapping to various data attributes used by the Ultra Force DOM typescript library.
/// </summary>
public static class UFDataAttribute
{
  /// <summary>
  /// Used by <see cref="UFTableTagHelperBase"/>.
  /// </summary>
  public const string Filter = "data-uf-filter";

  /// <summary>
  /// Used by <see cref="UFTableTagHelperBase"/>.
  /// </summary>
  public const string Paging = "data-uf-paging";

  /// <summary>
  /// Used by <see cref="UFTableTagHelperBase"/>.
  /// </summary>
  public const string PageSize = "data-uf-page-size";

  /// <summary>
  /// Used by <see cref="UFTableTagHelperBase"/>.
  /// </summary>
  public const string Sorting = "data-uf-sorting";

  /// <summary>
  /// Used by <see cref="UFCellTagHelperBase"/>.
  /// </summary>
  public const string SortType = "data-uf-sort-type";

  /// <summary>
  /// Used by <see cref="UFCellTagHelperBase"/>.
  /// </summary>
  public const string SortValue = "data-uf-sort-value";

  /// <summary>
  /// Used by <see cref="UFCellTagHelperBase"/>.
  /// </summary>
  public const string NoFilter = "data-uf-no-filter";

  /// <summary>
  /// Used by <see cref="UFTableRowTagHelperBase"/>.
  /// </summary>
  public const string SortLocation = "data-uf-sort-location";

  /// <summary>
  /// Used by <see cref="UFTableTagHelperBase"/>.
  /// </summary>
  public const string FilterTable = "data-uf-filter-table";

  /// <summary>
  /// Used by <see cref="UFTableRowTagHelperBase"/>.
  /// </summary>
  public const string HeaderRow = "data-uf-header-row";

  /// <summary>
  /// Used by <see cref="UFTableTagHelperBase"/>.
  /// </summary>
  public const string SortAscending = "data-uf-sort-ascending";

  /// <summary>
  /// Used by <see cref="UFTableTagHelperBase"/>.
  /// </summary>
  public const string SortDescending = "data-uf-sort-descending";

  /// <summary>
  /// Used by various classes.
  /// </summary>
  public const string StorageId = "data-uf-storage-id";

  /// <summary>
  /// Points to a clickable element
  /// </summary>
  public const string SetFieldSelector = "data-uf-set-field-selector";

  /// <summary>
  /// Value to set if clickable element is clicked upon.
  /// </summary>
  public const string SetFieldValue = "data-uf-set-field-value";

  /// <summary>
  /// When set don't cache values with sorted tables.
  /// </summary>
  public const string NoCaching = "data-uf-no-caching";

  /// <summary>
  /// Image preview for file input.
  /// </summary>
  public const string ImagePreview = "data-uf-image-preview";

  /// <summary>
  /// Set content to width of image.
  /// </summary>
  public const string ImageWidth = "data-uf-image-width";

  /// <summary>
  /// Set content to height of image.
  /// </summary>
  public const string ImageHeight = "data-uf-image-height";

  /// <summary>
  /// Set content to name of image.
  /// </summary>
  public const string ImageName = "data-uf-image-name";

  /// <summary>
  /// Set content to size of image.
  /// </summary>
  public const string ImageSize = "data-uf-image-size";

  /// <summary>
  /// Set content to type of image.
  /// </summary>
  public const string ImageType = "data-uf-image-type";

  /// <summary>
  /// Show the content of the field if some condition is met.
  /// </summary>
  public const string ShowIfField = "data-uf-show-if-field";

  /// <summary>
  /// Hide the content of the field if some condition is met.
  /// </summary>
  public const string HideIfField = "data-uf-hide-if-field";

  /// <summary>
  /// Value to use.
  /// </summary>
  public const string FieldValue = "data-uf-field-value";

  /// <summary>
  /// Value to assign to the display style.
  /// </summary>
  public const string DisplayValue = "data-uf-display-value";
}