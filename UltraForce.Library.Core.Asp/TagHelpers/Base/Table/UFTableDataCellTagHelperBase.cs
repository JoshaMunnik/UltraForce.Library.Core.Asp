// <copyright file="UFTableDataCellTagHelperBase.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid.Base;
using UltraForce.Library.Core.Asp.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

/// <summary>
/// Creates a table data cell. The cell can be self-closing or can contain content with a separate
/// closing tag.
/// <para>
/// The generated td element always uses an opening and closing tag.
/// </para>
/// <para>
/// When <see cref="UFGridItemTagHelperBaseBase.MinSize"/>,
/// <see cref="UFGridItemTagHelperBaseBase.MaxSize"/> or
/// <see cref="UFGridItemTagHelperBaseBase.Size"/> has been set and the table is not using grid
/// styling a style attribute is added to the td element.
/// </para>
/// <para>
/// Rendered html:
/// <code>
/// &lt;td class="{GetTableCellClasses()}" [style="width: {MinSize}; max-width: {MaxSize}; width: {Size}, box-sizing: content-box"]&gt;{children}&lt;/td&gt;
/// </code>
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
public abstract class UFTableDataCellTagHelperBase<TTable, TTableRow>(
  IUFModelExpressionRenderer modelExpressionRenderer
)
  : UFGridItemTagHelperBaseBase(modelExpressionRenderer)
  where TTable : UFTableTagHelperBase
  where TTableRow : UFTableDataRowTagHelperBase<TTable>
{
  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    await base.ProcessAsync(context, output);
    context.Items[UFGridTagHelperBaseBase.Cell] = this;
    TTable table = UFTagHelperTools.GetItem<TTable>(context, UFGridTagHelperBaseBase.Grid); 
    TTableRow tableRow = UFTagHelperTools.GetItem<TTableRow>(context, UFGridTagHelperBaseBase.Row);
    int cellIndex = table.CellIndex;
    table.CellIndex++;
    await this.ProcessAsync(context, output, table, tableRow, cellIndex);
  }

  #endregion

  #region protected methods

  /// <summary>
  /// Executes the tag helper.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="output"></param>
  /// <param name="table">Table the cell is created within</param>
  /// <param name="tableRow">Row the cell is created within</param>
  /// <param name="cellIndex">Index of cell in the row (0 based)</param>
  /// <returns></returns>
  protected virtual Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output,
    TTable table,
    TTableRow tableRow,
    int cellIndex
  )
  {
    output.TagMode = TagMode.StartTagAndEndTag;
    output.TagName = "td";
    if ((table.ProcessedFirstHeaderRow == null) && (table.ProcessedFirstDataRow == tableRow))
    {
      table.CellSizes.Add(this);
    }
    string classValue = this.GetTableCellClasses(table, tableRow);
    UFTagHelperTools.AddClasses(output, classValue);
    table.SetCellStyle(output, this);
    return Task.CompletedTask;
  }
   

  /// <summary>
  /// Returns the css classes for the cell.
  /// </summary>
  /// <param name="type"></param>
  /// <param name="table"></param>
  /// <param name="tableRow"></param>
  /// <returns></returns>
  protected virtual string GetTableCellClasses(
    TTable table,
    TTableRow tableRow
  )
  {
    return string.Empty;
  }

  #endregion
}