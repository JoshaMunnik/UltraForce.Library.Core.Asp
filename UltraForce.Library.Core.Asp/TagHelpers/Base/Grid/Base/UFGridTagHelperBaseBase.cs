// <copyright file="UFGridTagHelperBaseBase.cs" company="Ultra Force Development">
// Ultra Force Library - Copyright (C) 2025 Ultra Force Development
// </copyright>
// <author>Josha Munnik</author>
// <email>josha@ultraforce.com</email>
// <license>
// The MIT License (MIT)
//
// Copyright (C) 2025 Ultra Force Development
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
using UltraForce.Library.Core.Asp.Types.Constants;
using UltraForce.Library.NetStandard.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Grid.Base;

/// <summary>
/// Base class for grid and table classes. 
/// </summary>
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public abstract class UFGridTagHelperBaseBase : TagHelper
{
  #region internal constants

  /// <summary>
  /// The key that children can use to access this (the table or grid) instance.
  /// </summary>
  internal const string Grid = "uf_grid";

  /// <summary>
  /// The key that children can use to access the row instance.
  /// </summary>
  internal const string Row = "uf_row";

  /// <summary>
  /// The key that children can use to access the cell instance.
  /// </summary>
  internal const string Cell = "uf_cell";

  #endregion

  #region public properties

  /// <summary>
  /// When true add an input box that is used to filter items with.
  /// </summary>
  [HtmlAttributeName("filter")]
  public bool Filter { get; set; } = false;

  /// <summary>
  /// When true add a `data-uf-paging` attribute with the value "1".
  /// </summary>
  [HtmlAttributeName("paging")]
  public bool Paging { get; set; } = false;

  /// <summary>
  /// Number of items per page (only used if <see cref="Paging"/> is true). It will set
  /// `data-uf-page-size` attribute. 
  /// </summary>
  [HtmlAttributeName("page-size")]
  public int PageSize { get; set; } = 15;

  /// <summary>
  /// When true add sorting support. 
  /// </summary>
  [HtmlAttributeName("sorting")]
  public bool Sorting { get; set; } = false;

  /// <summary>
  /// Assign a value to preserve the sorting and paging state of this element between sessions. When
  /// set the state is stored in a local storage of the browser. 
  /// </summary>
  [HtmlAttributeName("storage-id")]
  public string StorageId { get; set; } = "";

  #endregion

  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    context.Items[Grid] = this;
    string id = output.Attributes["id"]?.Value?.ToString() ?? UFHtmlTools.NewDomId();
    if (this.Filter)
    {
      this.AddFilter(output, id);
    }
    if (this.Paging)
    {
      output.Attributes.SetAttribute(UFDataAttribute.Paging("1"));
      output.Attributes.SetAttribute(UFDataAttribute.PageSize(this.PageSize.ToString()));
    }
    if (this.Sorting)
    {
      this.AddSorting(output);
    }
    if (!string.IsNullOrWhiteSpace(this.StorageId))
    {
      output.Attributes.SetAttribute(UFDataAttribute.StorageId(this.StorageId));
    }
    if (this.Filter || this.Paging || this.Sorting)
    {
      output.Attributes.SetAttribute("id", id);
    }
  }

  #endregion

  #region protected methods

  /// <summary>
  /// Returns the classes for the filter input.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetFilterInputClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the text to be used as the place-holder for the filter input.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetFilterPlaceholder()
  {
    return "filter...";
  }

  /// <summary>
  /// Returns the classes for the filter button.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetFilterButtonClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the html to use for the filter button caption (the child of the button tag).
  /// <para>
  /// The default implementation returns "clear".
  /// </para>
  /// </summary>
  /// <returns></returns>
  protected virtual string GetFilterCaptionHtml()
  {
    return "clear";
  }

  /// <summary>
  /// Returns the classes for the filter container.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetFilterContainerClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes for the sort ascending icon.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetSortAscendingClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes for the sort descending icon.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetSortDescendingClasses()
  {
    return string.Empty;
  }

  #endregion

  #region internal methods

  internal abstract TagHelperAttribute GetFilterAttribute(
    string value
  );

  internal abstract TagHelperAttribute GetSortingAttribute(
    string value
  );

  internal abstract string GetContainerClasses();

  #endregion

  #region private methods

  private void AddFilter(
    TagHelperOutput output,
    string tableId
  )
  {
    string inputId = UFHtmlTools.NewDomId();
    TagHelperAttribute filterAttribute = this.GetFilterAttribute($"#{tableId}");
    string input =
      $"<input id=\"{inputId}\"" +
      $" class=\"{this.GetFilterInputClasses()}\"" +
      $" placeholder=\"{this.GetFilterPlaceholder()}\"" +
      $" type=\"text\" {filterAttribute.Name}=\"{filterAttribute.Value}\"" +
      $" autocomplete=\"off\"" +
      $"/>";
    string button =
      $"<button" +
      $" class=\"{this.GetFilterButtonClasses()}\"" +
      $" {UFDataAttribute.SetFieldSelector().Name}=\"#{inputId}\"" +
      $">" +
      this.GetFilterCaptionHtml() +
      "</button>";
    output.PreElement.AppendHtml(
      $"<div class=\"{this.GetContainerClasses()}\">" +
      $"<div class=\"{this.GetFilterContainerClasses()}\">{input}{button}</div>"
    );
    output.PostElement.AppendHtml("</div>");
  }

  private void AddSorting(
    TagHelperOutput output
  )
  {
    output.Attributes.SetAttribute(this.GetSortingAttribute("1"));
    output.Attributes.SetAttribute(
      UFDataAttribute.SortAscending(this.GetSortAscendingClasses())
    );
    output.Attributes.SetAttribute(
      UFDataAttribute.SortDescending(this.GetSortDescendingClasses())
    );
  }

  #endregion
}