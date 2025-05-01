// <copyright file="UFTableHeaderCellTagHelperBase.cs" company="Ultra Force Development">
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
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid.Base;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Constants;
using UltraForce.Library.Core.Asp.Types.Enums;
using UltraForce.Library.NetStandard.Extensions;
using UltraForce.Library.NetStandard.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

/// <summary>
/// Creates a table header cell. The cell can be self-closing or can contain content with a separate
/// closing tag.
/// <para>
/// The generated th element always uses an opening and closing tag.
/// </para>
/// <para>
/// Rendered html:
/// <code>
/// &lt;th class="{GetTableCellClasses()}}" [style="min-width: {MinWidth}; max-width: {MaxWidth}; box-sizing: content-box"]&gt;{children}&lt;/th&gt;
/// </code>
/// </para>
/// <para>
/// Rendered html for buttons (a div is used so that flex or grid styling can be used):
/// <code>
/// &lt;td class="{GetTableCellClasses()}}" [style="width: {MinWidth}; max-width: {MaxWidth}; box-sizing: content-box"]&gt;<br/>
///   &lt;div class="{GetTableHeaderButtonClasses()} &gt;<br/>
///     {children}<br/>
///   &lt;/div&gt;<br/>
/// &lt;/td&gt;
/// </code>
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
public abstract class UFTableHeaderCellTagHelperBase<TTable, TTableRow>(
  IUFModelExpressionRenderer modelExpressionRenderer
)
  : UFGridHeaderTagHelperBaseBase(modelExpressionRenderer)
  where TTable : UFTableTagHelperBase
  where TTableRow : UFTableHeaderRowTagHelperBase<TTable>
{
  #region public properties

  /// <summary>
  /// When not empty, set this value as min-width value via the style tag. 
  /// </summary>
  [HtmlAttributeName("min-width")]
  public string MinWidth { get; set; } = "";

  /// <summary>
  /// When not empty, set this value as max-width value via the style tag. 
  /// </summary>
  [HtmlAttributeName("max-width")]
  public string MaxWidth { get; set; } = "";

  #endregion

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
    await this.ProcessAsync(context, output, table, tableRow);
  }

  #endregion

  #region protected methods
  
  /// <summary>
  /// Executes the tag helper.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="output"></param>
  /// <param name="table">Table the cell is created inside in</param>
  /// <param name="tableRow">Row the cell is created inside in</param>
  protected virtual Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output,
    TTable table,
    TTableRow tableRow
  )
  {
    output.TagMode = TagMode.StartTagAndEndTag;
    output.TagName = "th";
    this.UpdateClasses(output, table, tableRow);
    UFSortTypeEnum sortType = this.GetSortType();
    if (tableRow == table.ProcessedFirstHeaderRow)
    {
      table.CellCount++;
    }
    if (
      (tableRow == table.ProcessedFirstHeaderRow) && table.Sorting &&
      (sortType != UFSortTypeEnum.None) 
    )
    {
      this.AddButtonWrapper(output, table, tableRow);
    }
    return Task.CompletedTask;
  }

  /// <summary>
  /// Returns the css classes for the cell.
  /// </summary>
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

  /// <summary>
  /// Returns the css classes for the buttons in the header.
  /// </summary>
  /// <param name="table"></param>
  /// <param name="tableRow"></param>
  /// <returns></returns>
  protected virtual string GetTableHeaderButtonClasses(
    TTable table,
    TTableRow tableRow
  )
  {
    return string.Empty;
  }

  #endregion

  #region private methods

  /// <summary>
  /// Adds css classes to the classes attribute and process the <see cref="MinWidth"/> property.
  /// </summary>
  /// <param name="output"></param>
  /// <param name="table"></param>
  /// <param name="tableRow"></param>
  private void UpdateClasses(
    TagHelperOutput output,
    TTable table,
    TTableRow tableRow
  )
  {
    string classValue = this.GetTableCellClasses(table, tableRow);
    UFTagHelperTools.AddClasses(output, classValue);
    string style = "";
    if (!string.IsNullOrEmpty(this.MinWidth))
    {
      style += " min-width: " + this.MinWidth + ";";
    }
    if (!string.IsNullOrEmpty(this.MaxWidth))
    {
      style += " max-width: " + this.MaxWidth + ";";
    }
    if (!string.IsNullOrEmpty(style))
    {
      style += " width: 1px; box-sizing: content-box;";
      output.Attributes.SetAttribute("style", style);
    }
  }

  /// <summary>
  /// Adds a wrapper for the content of a cell that is clickable for sorting.
  /// </summary>
  /// <param name="output"></param>
  /// <param name="table"></param>
  /// <param name="tableRow"></param>
  private void AddButtonWrapper(
    TagHelperOutput output,
    TTable table,
    TTableRow tableRow
  )
  {
    output.PreContent.AppendHtml(
      $"<button type=\"button\"" +
      $" class=\"{this.GetTableHeaderButtonClasses(table, tableRow)}\"" +
      $">"
    );
    output.PostContent.AppendHtml("</button>");
  }

  #endregion
}