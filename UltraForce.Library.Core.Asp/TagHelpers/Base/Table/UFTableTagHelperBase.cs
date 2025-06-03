// <copyright file="UFTableTagHelperBase.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid.Base;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Constants;
using UltraForce.Library.Core.Asp.Types.Interfaces;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

/// <summary>
/// Tag helper to render a table. The table assumes all header rows are the first rows in the table
/// and are grouped together. The tag helper will insert thead and tbody tags if there are header
/// and data rows. To skip this, call the constructor with `skipHeadBody` set to true.
/// <para>
/// If the styling of the table will use a grid styling, set the `useGrid` parameter to true. This
/// will add a style attribute setting the `grid-template-columns` property. The template will be
/// built from the sizes of the first row of header cells or the first row of data cells if there
/// are no header cells.
/// </para>
/// <para>
/// Table without filter and sorting:
/// <code>
/// &lt;table class="GetTableClasses()" [style="grid-template-columns: GetTemplateColumns()"]&gt;
///   {children}
/// &lt;/table&gt;
/// </code>
/// </para>
/// <para>
/// Table with a filter:
/// <code>
/// &lt;div class="GetTableContainerClasses()"&gt;
///   &lt;div class="GetFilterContainerClasses()"&gt;
///     &lt;input {id} class="GetFilterInputClasses()" placeholder="GetFilterPlaceholder()" data-uf-filter-table="{id}" /&gt;
///     &lt;button class="GetFilterButtonClasses()" data-uf-set-field-selector="#{id}"&gt;
///       GetFilterCaptionHtml()
///     &lt;/button&gt;
///   &lt;/div&gt;
///   &lt;table class="GetTableClasses()" [style="grid-template-columns: GetTemplateColumns()"]&gt;
///     {children}
///   &lt;/table&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Table with sorting:
/// <code>
/// &lt;table
///   &lt;table class="GetTableClasses()" [style="grid-template-columns: GetTemplateColumns()"]&gt;
///   data-uf-table-sorting="1"
///   data-uf-sort-ascending="GetSortAscendingClasses()"
///   data-uf-sort-descending="GetSortDescendingClasses()"
///   &gt;
///   {children}
/// &lt;/table&gt;
/// </code>
/// </para>
/// </summary>
/// <param name="skipHeadBody">
/// When true do not insert thead and tbody tags. The class will not call
/// <see cref="GetBodyClasses"/> and <see cref="GetHeadClasses"/>.
/// </param>
/// <param name="useGrid">
/// Set to true to indicate the table will be styled using `display: grid`. When true, the cell
/// instances will not set a style attribute with widths.
/// </param>
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public abstract class UFTableTagHelperBase(
  bool skipHeadBody = false,
  bool useGrid = false
) : UFGridTagHelperBaseBase
{
  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    this.ProcessedFirstHeaderRow = null;
    this.ProcessedFirstDataRow = null;
    this.CellSizes.Clear();
    this.CellIndex = 0;
    await base.ProcessAsync(context, output);
    output.TagName = "table";
    output.TagMode = TagMode.StartTagAndEndTag;
    // process children (these might set ProcessedFirstHeaderRow and ProcessedFirstDataRow)
    TagHelperContent? childContent = await output.GetChildContentAsync();
    output.Content.SetHtmlContent(childContent);
    UFTagHelperTools.AddClasses(output, this.GetTableClasses(this.CellSizes.Count));
    if (useGrid)
    {
      output.Attributes.SetAttribute(
        new TagHelperAttribute(
          "style", "grid-template-columns: " + this.GetTemplateColumns(this.CellSizes)
        )
      );
    }
    // skip adding thead and tbody tags?
    if (this.SkipHeadBody)
    {
      return;
    }
    // add thead tag if there was at least one header row
    if (this.ProcessedFirstHeaderRow != null)
    {
      output.PreContent.AppendHtml(
        $"<thead class=\"{this.GetHeadClasses(this.CellSizes.Count)}\">"
      );
      // if there are only header rows add also closing thead tag
      if (this.ProcessedFirstDataRow == null)
      {
        output.PostContent.AppendHtml("</thead>");
      }
    }
    // add closing tbody tag there was at leest one data row
    if (this.ProcessedFirstDataRow != null)
    {
      output.PostContent.AppendHtml("</tbody>");
      // no header rows, then add starting tbody tag as well
      if (this.ProcessedFirstHeaderRow == null)
      {
        output.PreContent.AppendHtml(
          $"<tbody class=\"{this.GetBodyClasses(this.CellSizes.Count)}\">"
        );
      }
    }
  }

  #endregion

  #region protected methods

  /// <summary>
  /// Returns the classes for the table. The number of cells is determined by counting the number
  /// of header cells in the first header row or the number of data cells in the first data row.
  /// </summary>
  /// <param name="cellCount">Number of cells</param>
  /// <returns></returns>
  protected virtual string GetTableClasses(
    int cellCount
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes for the element containing the table. This method is only used when
  /// <see cref="UFGridTagHelperBaseBase.Filter"/> is <c>true</c>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetTableContainerClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use with the tbody tag.
  /// </summary>
  /// <param name="cellCount">Number of cells</param>
  /// <returns></returns>
  protected virtual string GetBodyClasses(
    int cellCount
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use with the thead tag.
  /// </summary>
  /// <param name="cellCount">Number of cells</param>
  /// <returns></returns>
  protected virtual string GetHeadClasses(
    int cellCount
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Gets the template columns for the table. This method is only used when <see cref="useGrid"/>
  /// is true.
  /// <para>
  /// The default implementation calls <see cref="UFTagHelperTools.BuildGridTemplateSizes"/>
  /// </para>
  /// </summary>
  /// <param name="sizes"></param>
  /// <returns></returns>
  protected virtual string GetTemplateColumns(
    IList<IUFGridItemSize> sizes
  )
  {
    return UFTagHelperTools.BuildGridTemplateSizes(sizes);
  }

  #endregion

  #region internal properties

  /// <summary>
  /// Will be set to true by the first header row. Will be set by
  /// <see cref="UFTableDataRowTagHelperBase{TTable}"/>.
  /// </summary>
  internal object? ProcessedFirstHeaderRow { get; set; }

  /// <summary>
  /// Will be set to true by the first header row. Will be set by
  /// <see cref="UFTableDataRowTagHelperBase{TTable}"/>.
  /// </summary>
  internal object? ProcessedFirstDataRow { get; set; }

  /// <summary>
  /// True to not insert thead and tbody tags.
  /// </summary>
  internal bool SkipHeadBody { get; } = skipHeadBody;

  /// <summary>
  /// Updated by either header cells or data cells in the table.
  /// </summary>
  internal List<IUFGridItemSize> CellSizes { get; } = [];

  /// <summary>
  /// To keep track of the current cell.
  /// </summary>
  internal int CellIndex { get; set; } = 0;

  #endregion

  #region internal methods

  /// <summary>
  /// Gets the classes for the table body. Will be called by
  /// <see cref="UFTableDataRowTagHelperBase{TTable}"/>.
  /// </summary>
  /// <returns></returns>
  internal string GetTableBodyClasses() => this.GetBodyClasses(this.CellSizes.Count);

  /// <inheritdoc />
  internal override string GetContainerClasses() => this.GetTableContainerClasses();

  /// <inheritdoc />
  internal override TagHelperAttribute GetFilterAttribute(
    string value
  ) => UFDataAttribute.FilterTable(value);

  /// <inheritdoc />
  internal override TagHelperAttribute GetSortingAttribute(
    string value
  ) => UFDataAttribute.GridSorting(value);

  /// <summary>
  /// Sets the size of a cell via the style attribute.
  /// </summary>
  /// <param name="output"></param>
  /// <param name="size"></param>
  internal void SetCellStyle(
    TagHelperOutput output,
    IUFGridItemSize size
  )
  {
    string style = "";
    if (!string.IsNullOrEmpty(size.MinSize))
    {
      style += " min-width: " + size.MinSize + ";";
    }
    if (!string.IsNullOrEmpty(size.MaxSize))
    {
      style += " max-width: " + size.MaxSize + ";";
    }
    if (!string.IsNullOrEmpty(size.Size))
    {
      style += " width: " + size.Size + ";";
    }
    else if (!string.IsNullOrEmpty(style) && !useGrid)
    {
      style += " width: 1px;";
    }
    if (!string.IsNullOrEmpty(style))
    {
      style += " box-sizing: content-box;";
      output.Attributes.SetAttribute("style", style);
    }
  }

  #endregion
}
