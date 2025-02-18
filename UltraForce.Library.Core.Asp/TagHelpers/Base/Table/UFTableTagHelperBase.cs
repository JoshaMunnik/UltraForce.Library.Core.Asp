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
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Constants;
using UltraForce.Library.NetStandard.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

/// <summary>
/// Tag helper to render a table. The table assumes all header rows are the first rows in the table
/// and are grouped together. The tag helper will insert thead and tbody tags if there are header
/// and data rows. To skip this, call the constructor with `skipHeadBody` set to true.
/// <para>
/// Table without filter and sorting:
/// <code>
/// &lt;table class="GetTableClasses()"&gt;
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
///   &lt;table class="GetTableClasses()"&gt;
///     {children}
///   &lt;/table&gt;
/// &lt;/div&gt;
/// </code>
/// </para>
/// <para>
/// Table with sorting:
/// <code>
/// &lt;table
///   class="GetTableClasses()"
///   data-uf-sorting="1"
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
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public abstract class UFTableTagHelperBase(bool skipHeadBody = false) : TagHelper
{
  #region internal constants

  /// <summary>
  /// The key that children can use to access this (the table) instance.
  /// </summary>
  internal const string Table = "uf_table";

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
  /// When true add a `data-filter` attribute with the value "1" to the table.
  /// </summary>
  [HtmlAttributeName("filter")]
  public bool Filter { get; set; } = false;

  /// <summary>
  /// When true add a `data-paging` attribute with the value "1" to the table.
  /// </summary>
  [HtmlAttributeName("paging")]
  public bool Paging { get; set; } = false;

  /// <summary>
  /// Number of rows per page (only used if <see cref="Paging"/> is true). It will set
  /// `data-page-size` attribute on the table. 
  /// </summary>
  [HtmlAttributeName("page-size")]
  public int PageSize { get; set; } = 15;

  /// <summary>
  /// When true add `data-sorting` attribute with the value "1" to the table. 
  /// </summary>
  [HtmlAttributeName("sorting")]
  public bool Sorting { get; set; } = false;

  /// <summary>
  /// Assign a value to preserve the sorting and paging state of the table between sessions. When
  /// set the state is stored in a local storage of the browser. 
  /// </summary>
  public string StorageId { get; set; } = "";
  
  #endregion

  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
  {
    this.ProcessedFirstHeaderRow = null;
    this.ProcessedFirstDataRow = null;
    context.Items[Table] = this;
    output.TagName = "table";
    string id = output.Attributes["id"]?.Value?.ToString() ?? UFHtmlTools.NewDomId();
    if (this.Filter)
    {
      this.AddFilter(output, id);
    }
    if (this.Paging)
    {
      output.Attributes.SetAttribute(UFDataAttribute.Paging, "1");
      output.Attributes.SetAttribute(UFDataAttribute.PageSize, this.PageSize.ToString());
    }
    if (this.Sorting)
    {
      this.AddSorting(output);
    }
    if ((this.Paging || this.Sorting) && !string.IsNullOrWhiteSpace(this.StorageId))
    {
      output.Attributes.SetAttribute(UFDataAttribute.StorageId, this.StorageId);
    }
    if (this.Filter || this.Paging || this.Sorting)
    {
      output.Attributes.SetAttribute("id", id);
    }
    UFTagHelperTools.AddClasses(output, this.GetTableClasses());
    // process children (these might set ProcessedFirstHeaderRow and ProcessedFirstDataRow)
    TagHelperContent? childContent = await output.GetChildContentAsync();
    output.Content.SetHtmlContent(childContent);
    // skip adding thead and tbody tags?
    if (this.SkipHeadBody)
    {
      return;
    }
    // add thead tag if there was at least one header row
    if (this.ProcessedFirstHeaderRow != null)
    {
      output.PreContent.AppendHtml($"<thead class=\"{this.GetHeadClasses()}\">");
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
        output.PreContent.AppendHtml($"<tbody class=\"{this.GetBodyClasses()}\">");
      }
    }
  }

  #endregion
  
  #region protected overridable methods
  
  /// <summary>
  /// Returns the classes for the table. 
  /// </summary>
  /// <returns></returns>
  protected virtual string GetTableClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes for the element containing the table. This method is only used when
  /// <see cref="Filter"/> is <c>true</c>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetTableContainerClasses()
  {
    return string.Empty;
  }
  
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

  /// <summary>
  /// Returns the classes to use with the tbody tag. 
  /// </summary>
  /// <returns></returns>
  protected virtual string GetBodyClasses()
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the classes to use with the thead tag. 
  /// </summary>
  /// <returns></returns>
  protected virtual string GetHeadClasses()
  {
    return string.Empty;
  }
  
  #endregion
  
  #region internal properties

  /// <summary>
  /// Will be set to true by the first header row. Will be set by
  /// <see cref="UFTableRowTagHelperBase{TTable}"/>.
  /// </summary>
  internal object? ProcessedFirstHeaderRow { get; set; }
  
  /// <summary>
  /// Will be set to true by the first header row. Will be set by
  /// <see cref="UFTableRowTagHelperBase{TTable}"/>.
  /// </summary>
  internal object? ProcessedFirstDataRow { get; set; }

  /// <summary>
  /// True to not insert thead and tbody tags.
  /// </summary>
  internal bool SkipHeadBody { get;  } = skipHeadBody;
  
  #endregion
  
  #region internal methods
  
  /// <summary>
  /// Gets the classes for the table body. Will be called by
  /// <see cref="UFTableRowTagHelperBase{TTable}"/>.
  /// </summary>
  /// <returns></returns>
  internal string GetTableBodyClasses() => this.GetBodyClasses();
  
  #endregion

  #region private methods

  private void AddFilter(TagHelperOutput output, string tableId)
  {
    output.Attributes.SetAttribute(UFDataAttribute.Filter, "1");
    string inputId = UFHtmlTools.NewDomId();
    string input =
      $"<input id=\"{inputId}\"" +
      $" class=\"{this.GetFilterInputClasses()}\"" +
      $" placeholder=\"{this.GetFilterPlaceholder()}\"" +
      $" type=\"text\" {UFDataAttribute.FilterTable}=\"#{tableId}\"" +
      $" autocomplete=\"off\"" +
      $"/>";
    string button =
      $"<button" +
      $" class=\"{this.GetFilterButtonClasses()}\"" +
      $" {UFDataAttribute.SetFieldSelector}=\"#{inputId}\"" +
      $">" +
      this.GetFilterCaptionHtml() +
      "</button>";
    output.PreElement.AppendHtml(
      $"<div class=\"{this.GetTableContainerClasses()}\">" +
      $"<div class=\"{this.GetFilterContainerClasses()}\">{input}{button}</div>"
    );
    output.PostElement.AppendHtml("</div>");
  }

  private void AddSorting(TagHelperOutput output)
  {
    output.Attributes.SetAttribute(UFDataAttribute.Sorting, "1");
    output.Attributes.SetAttribute(
      UFDataAttribute.SortAscending, this.GetSortAscendingClasses()
    );
    output.Attributes.SetAttribute(
      UFDataAttribute.SortDescending, this.GetSortDescendingClasses()
    );
  }

  #endregion
}