// <copyright file="UFTableRowTagHelperBase.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.Types.Enums;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

/// <summary>
/// Creates a table row. Renders:
/// <code>
/// &lt;tr class="{GetTableRowClasses()}&gt;
///   {children}
/// &lt;/tr&gt;
/// </code> 
/// </summary>
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public abstract class UFTableRowTagHelperBase<TTable> : TagHelper
  where TTable : UFTableTagHelperBase
{
  #region public properties

  /// <summary>
  /// The type of row
  /// </summary>
  [HtmlAttributeName("type")]
  public UFTableRowTypeEnum Type { get; set; } = UFTableRowTypeEnum.Data;

  /// <summary>
  /// Keep the row at top or bottom if table gets sorted
  /// <para>
  /// Setting this value to <see cref="UFTableSortLocationEnum.Top"/> or
  /// <see cref="UFTableSortLocationEnum.Bottom"/> will create a `data-sort-location` attribute with
  /// either "top" or "bottom" as value.
  /// </para>
  /// </summary>
  [HtmlAttributeName("sort-location")]
  public UFTableSortLocationEnum SortLocation { get; set; } = UFTableSortLocationEnum.Middle;

  #endregion

  #region public methods

  /// <inheritdoc />
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    base.Process(context, output);
    context.Items[UFTableTagHelperBase.Row] = this;
    output.TagName = "tr";
    TTable table = (context.Items[UFTableTagHelperBase.Table] as TTable)!;
    if (table is { ProcessedFirstHeaderRow: null } && (this.Type == UFTableRowTypeEnum.Header))
    {
      table.ProcessedFirstHeaderRow = this;
    }
    if (table is { ProcessedFirstDataRow: null } && (this.Type == UFTableRowTypeEnum.Data))
    {
      table.ProcessedFirstDataRow = this;
      if (table is { ProcessedFirstHeaderRow: not null, SkipHeadBody: false })
      {
        output.PreElement.AppendHtml($"</thead><tbody class=\"{table.GetTableBodyClasses()}\">");
      }
    }
    switch (this.SortLocation)
    {
      case UFTableSortLocationEnum.Top:
        output.Attributes.SetAttribute(UFDataAttribute.SortLocation, "top");
        break;
      case UFTableSortLocationEnum.Bottom: 
        output.Attributes.SetAttribute(UFDataAttribute.SortLocation, "bottom");
        break;
    }
    if (this.Type == UFTableRowTypeEnum.Header)
    {
      output.Attributes.SetAttribute(UFDataAttribute.HeaderRow, "1");
    }
    UFTagHelperTools.AddClasses(output, this.GetTableRowClasses(table));
  }

  #endregion
  
  #region protected overridable methods

  /// <summary>
  /// Returns the classes for the table row.
  /// </summary>
  /// <param name="table"></param>
  /// <returns></returns>
  protected virtual string GetTableRowClasses(TTable table)
  {
    return string.Empty;
  }
  
  #endregion
}