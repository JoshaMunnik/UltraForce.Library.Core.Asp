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
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid.Base;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Constants;

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
public abstract class UFTableTagHelperBase(bool skipHeadBody = false) : UFGridTagHelperBaseBase
{
  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
  {
    this.ProcessedFirstHeaderRow = null;
    this.ProcessedFirstDataRow = null;
    await base.ProcessAsync(context, output);
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
  
  #region protected methods
  
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
  internal bool SkipHeadBody { get;  } = skipHeadBody;
  
  #endregion
  
  #region internal methods
  
  /// <summary>
  /// Gets the classes for the table body. Will be called by
  /// <see cref="UFTableDataRowTagHelperBase{TTable}"/>.
  /// </summary>
  /// <returns></returns>
  internal string GetTableBodyClasses() => this.GetBodyClasses();

  /// <inheritdoc />
  internal override string GetContainerClasses() => this.GetTableContainerClasses();

  /// <inheritdoc />
  internal override TagHelperAttribute GetFilterAttribute(
    string value
  ) => UFDataAttribute.FilterTable(value);

  /// <inheritdoc />
  internal override TagHelperAttribute GetSortingAttribute(
    string value
  ) => UFDataAttribute.TableSorting(value);

  #endregion
}