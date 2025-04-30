// <copyright file="UFTableChildTagHelperBase.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid.Base;
using UltraForce.Library.Core.Asp.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

/// <summary>
/// This base class can be used for tag helpers that are placed with a
/// <see cref="UFTableTagHelperBase"/>. The class defines protected methods to get a reference
/// to the table, row and cell instance.
/// </summary>
/// <typeparam name="TTable">
/// Subclass of <see cref="UFTableTagHelperBase"/>.
/// </typeparam>
/// <typeparam name="TRow">
/// Any class (normally a subclass of <see cref="UFTableDataRowTagHelperBase{TTable}"/>).
/// </typeparam>
/// <typeparam name="TCell">
/// Any class (normally a subclass of <see cref="UFTableHeaderCellTagHelperBase{TTable,TTableRow}"/>).
/// </typeparam>
public class UFTableChildTagHelperBase<TTable, TRow, TCell> : TagHelper
  where TTable : UFTableTagHelperBase
  where TRow : class
  where TCell : class
{
  #region protected methods

  /// <summary>
  /// Returns the current table.
  /// </summary>
  /// <param name="context">
  /// Context of the tag helper. 
  /// </param>
  /// <returns></returns>
  protected TTable GetTable(
    TagHelperContext context
  )
  {
    return UFTagHelperTools.GetItem<TTable>(context, UFGridTagHelperBaseBase.Grid);
  }
  
  /// <summary>
  /// Returns the current row.
  /// </summary>
  /// <param name="context">
  /// Context of the tag helper. 
  /// </param>
  /// <returns></returns>
  protected TRow GetTableRow(
    TagHelperContext context
  )
  {
    return UFTagHelperTools.GetItem<TRow>(context, UFGridTagHelperBaseBase.Row);
  }
  
  /// <summary>
  /// Returns the current cell.
  /// </summary>
  /// <param name="context">
  /// Context of the tag helper. 
  /// </param>
  /// <returns></returns>
  protected TCell GetTableCell(
    TagHelperContext context
  )
  {
    return UFTagHelperTools.GetItem<TCell>(context, UFGridTagHelperBaseBase.Cell);
  }
  
  #endregion
}