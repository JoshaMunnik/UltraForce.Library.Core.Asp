using Microsoft.AspNetCore.Razor.TagHelpers;
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
/// Any class (normally a subclass of <see cref="UFTableRowTagHelperBase{TTable}"/>).
/// </typeparam>
/// <typeparam name="TCell">
/// Any class (normally a subclass of <see cref="UFTableCellTagHelperBase{TTable, TRow}"/>).
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
    return UFTagHelperTools.GetItem<TTable>(context, UFTableTagHelperBase.Table);
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
    return UFTagHelperTools.GetItem<TRow>(context, UFTableTagHelperBase.Table);
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
    return UFTagHelperTools.GetItem<TCell>(context, UFTableTagHelperBase.Table);
  }
  
  #endregion
}