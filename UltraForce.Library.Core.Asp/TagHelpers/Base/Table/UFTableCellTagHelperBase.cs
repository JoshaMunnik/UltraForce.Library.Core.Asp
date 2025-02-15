// <copyright file="UFCellTagHelperBase.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Constants;
using UltraForce.Library.Core.Asp.Types.Enums;
using UltraForce.Library.NetStandard.Extensions;
using UltraForce.Library.NetStandard.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

/// <summary>
/// Creates a table cell. The cell can be self-closing or can contain content with a separate
/// closing tag.
/// <para>
/// The generated td or th element always uses an opening and closing tag.
/// </para>
/// <para>
/// Rendered html for header:
/// <code>
/// &lt;th class="{GetTableCellClasses()}}" [style="width: {Width}"]&gt;{children}&lt;/th&gt;
/// </code>
/// </para>
/// <para>
/// Rendered html for data:
/// <code>
/// &lt;td class="{GetTableCellClasses()}}" [style="width: {Width}"]&gt;{children}&lt;/td&gt;
/// </code>
/// </para>
/// <para>
/// Rendered html for buttons (a div is used so that flex or grid styling can be used):
/// <code>
/// &lt;td class="{GetTableCellClasses()}}" [style="width: {Width}"]&gt;<br/>
///   &lt;div class="{GetTableHeaderButtonClasses()} &gt;<br/>
///     {children}<br/>
///   &lt;/div&gt;<br/>
/// &lt;/td&gt;
/// </code>
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
public abstract class UFTableCellTagHelperBase<TTable, TTableRow>(
  IUFModelExpressionRenderer aModelExpressionRenderer
)
  : UFTagHelperWithModelExpressionRenderer(aModelExpressionRenderer)
  where TTable : UFTableTagHelperBase
  where TTableRow : UFTableRowTagHelperBase<TTable>
{
  #region public properties

  /// <summary>
  /// Type of cell.
  /// </summary>
  [HtmlAttributeName("type")]
  public UFTableCellTypeEnum Type { get; set; } = UFTableCellTypeEnum.Auto;

  /// <summary>
  /// When not empty, set this value as width value via the style tag. 
  /// </summary>
  [HtmlAttributeName("width")]
  public string Width { get; set; } = "";

  /// <summary>
  /// When not empty, set this value as max-width value via the style tag. 
  /// </summary>
  [HtmlAttributeName("max-width")]
  public string MaxWidth { get; set; } = "";

  /// <summary>
  /// Type of sorting (only of use with <see cref="UFTableCellTypeEnum.Header"/>). If type is set to
  /// <see cref="UFTableSortTypeEnum.Auto"/> (default) the method will try to determine the type
  /// from <see cref="For"/>.
  /// <para>
  /// </para>
  /// <para>
  /// An attribute with the name "data-sort-type" will be added to the cell. This attribute
  /// contains one of the following values:
  /// - "text"
  /// - "number"
  /// - "date" 
  /// </para>
  /// </summary>
  [HtmlAttributeName("sort-type")]
  public UFTableSortTypeEnum SortType { get; set; } = UFTableSortTypeEnum.Auto;

  /// <summary>
  /// When true the cell contents will not be cached with sorting. This is useful when the cell
  /// contents will be changed while using a sorted table.
  /// </summary>
  [HtmlAttributeName("no-caching")]
  public bool NoCaching { get; set; } = false;

  /// <summary>
  /// An expression to be evaluated against the current model. When set, the method will use the
  /// type to adjust the sort type (if <see cref="SortType"/> is set
  /// to <see cref="UFTableSortTypeEnum.Auto"/>).
  /// <para>
  /// Date values are formatted using mysql format (so there is no confusion on month and
  /// day positions):
  /// "yyyy-mm-dd hh:mm:ss"
  /// </para>
  /// <para>
  /// If the type of this value is a `bool`, an attribute `data-sort-value` is set with either
  /// `0` or `1`.
  /// </para>
  /// <para>
  /// For data cells, the value is used as content for the cell.
  /// </para>
  /// <para>
  /// For header cells, the name is used as content for the cell.
  /// </para>
  /// </summary>
  [HtmlAttributeName("for")]
  public ModelExpression? For { get; set; } = null;

  /// <summary>
  /// When true data in the cell can be found via the filter. When false an attribute with
  /// the name "data-no-filter" is added to the cell tag. This property is only processed
  /// when <see cref="Type"/> is <see cref="UFTableCellTypeEnum.Data"/>.
  /// </summary>
  [HtmlAttributeName("no-filter")]
  public bool NoFilter { get; set; } = false;

  #endregion

  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(
    TagHelperContext aContext,
    TagHelperOutput anOutput
  )
  {
    await base.ProcessAsync(aContext, anOutput);
    TTable table = (aContext.Items[UFTableTagHelperBase.Table] as TTable)!;
    TTableRow tableRow = (aContext.Items[UFTableRowTagHelperBase<TTable>.Row] as TTableRow)!;
    UFTableCellTypeEnum type = this.Type == UFTableCellTypeEnum.Auto
      ? (tableRow.Type == UFTableRowTypeEnum.Header
        ? UFTableCellTypeEnum.Header
        : UFTableCellTypeEnum.Data)
      : this.Type;
    anOutput.TagMode = TagMode.StartTagAndEndTag;
    anOutput.TagName = type == UFTableCellTypeEnum.Header ? "th" : "td";
    UFTableSortTypeEnum sortType = await this.ProcessForAsync(anOutput, type);
    if (table.Sorting)
    {
      this.SetDataSortType(anOutput, sortType, type);
    }
    if (table.Filter)
    {
      this.SetFilter(anOutput);
    }
    this.UpdateClasses(anOutput, type, table, tableRow);
    if (
      (tableRow == table.ProcessedFirstHeaderRow) && table.Sorting &&
      (sortType != UFTableSortTypeEnum.None)
    )
    {
      this.AddButtonWrapper(anOutput, table, tableRow);
    }
    if (this.NoCaching)
    {
      anOutput.Attributes.SetAttribute(UFDataAttribute.NoCaching, "1");
    }
  }

  #endregion

  #region protected methods

  /// <summary>
  /// Returns the css classes for the cell.
  /// </summary>
  /// <param name="aType"></param>
  /// <param name="aTable"></param>
  /// <param name="aTableRow"></param>
  /// <returns></returns>
  protected virtual string GetTableCellClasses(
    UFTableCellTypeEnum aType,
    TTable aTable,
    TTableRow aTableRow
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// Returns the css classes for the buttons in the header.
  /// </summary>
  /// <param name="aTable"></param>
  /// <param name="aTableRow"></param>
  /// <returns></returns>
  protected virtual string GetTableHeaderButtonClasses(
    TTable aTable,
    TTableRow aTableRow
  )
  {
    return string.Empty;
  }

  #endregion

  #region private methods

  /// <summary>
  /// Processes the <see cref="For"/> property. It determines the sort type and sets the
  /// content (if it has not been altered) to either the name or value.
  /// </summary>
  /// <param name="anOutput">Output to update</param>
  /// <param name="aType"></param>
  /// <returns>Sort type</returns>
  private async Task<UFTableSortTypeEnum> ProcessForAsync(
    TagHelperOutput anOutput,
    UFTableCellTypeEnum aType
  )
  {
    if (this.For == null)
    {
      return this.SortType == UFTableSortTypeEnum.Auto ? UFTableSortTypeEnum.None : this.SortType;
    }
    Type type = this.For!.Metadata.UnderlyingOrModelType;
    UFTableSortTypeEnum sortType = this.SortType;
    if (sortType == UFTableSortTypeEnum.Auto)
    {
      if ((type == typeof(DateTime)) || (type == typeof(DateTime?)))
      {
        sortType = UFTableSortTypeEnum.Date;
      }
      else if (
        UFReflectionTools.IsNumeric(type) || (type == typeof(bool)) || (type == typeof(bool?))
      )
      {
        sortType = UFTableSortTypeEnum.Number;
      }
      else
      {
        sortType = UFTableSortTypeEnum.Text;
      }
    }
    if (aType == UFTableCellTypeEnum.Header)
    {
      await this.ModelExpressionRenderer.SetContentToNameAsync(
        anOutput, this.For, this.ViewContext
      );
    }
    else
    {
      await this.ModelExpressionRenderer.SetContentToValueAsync(
        anOutput, this.For, this.ViewContext
      );
      if (type == typeof(bool))
      {
        bool value = (bool)this.For.Model;
        anOutput.Attributes.SetAttribute(UFDataAttribute.SortValue, value ? "1" : "0");
      }
      else if (type == typeof(bool?))
      {
        bool? value = (bool?)this.For.Model;
        anOutput.Attributes.SetAttribute(
          UFDataAttribute.SortValue, (value != null) && value.Value ? "1" : "0"
        );
      }
      if (!anOutput.Attributes.ContainsName("title") && (this.For.Model != null))
      {
        anOutput.Attributes.SetAttribute(
          "title", this.ModelExpressionRenderer.GetValueAsText(this.For, this.ViewContext)
        );
      }
    }
    return sortType;
  }

  /// <summary>
  /// Adds css classes to the classes attribute and process the <see cref="Width"/> property.
  /// </summary>
  /// <param name="anOutput"></param>
  /// <param name="aType"></param>
  /// <param name="aTable"></param>
  /// <param name="aTableRow"></param>
  private void UpdateClasses(
    TagHelperOutput anOutput,
    UFTableCellTypeEnum aType,
    TTable aTable,
    TTableRow aTableRow
  )
  {
    string classValue = this.GetTableCellClasses(aType, aTable, aTableRow);
    UFTagHelperTools.AddClasses(anOutput, classValue);
    string style = "";
    if (!string.IsNullOrEmpty(this.Width))
    {
      style = " width: " + this.Width + ";";
    }
    if (!string.IsNullOrEmpty(this.Width))
    {
      style = " max-width: " + this.Width + ";";
    }
    if (!string.IsNullOrEmpty(style))
    {
      anOutput.Attributes.SetAttribute("style", style);
    }
  }

  /// <summary>
  /// Adds a <see cref="UFDataAttribute.Filter"/> attribute if the cell is a data cell and
  /// <see cref="NoFilter"/> is true.
  /// </summary>
  /// <param name="anOutput"></param>
  private void SetFilter(
    TagHelperOutput anOutput
  )
  {
    if ((this.Type == UFTableCellTypeEnum.Data) && this.NoFilter)
    {
      anOutput.Attributes.SetAttribute(UFDataAttribute.NoFilter, "1");
    }
  }

  /// <summary>
  /// Sets the `data-sort-type` attribute based on the sort type.
  /// </summary>
  /// <param name="anOutput"></param>
  /// <param name="aSortType"></param>
  /// <param name="aType"></param>
  private void SetDataSortType(
    TagHelperOutput anOutput,
    UFTableSortTypeEnum aSortType,
    UFTableCellTypeEnum aType
  )
  {
    if ((aType != UFTableCellTypeEnum.Header) || (aSortType == UFTableSortTypeEnum.None))
    {
      return;
    }
    switch (aSortType)
    {
      case UFTableSortTypeEnum.Number:
      case UFTableSortTypeEnum.Date:
      case UFTableSortTypeEnum.Text:
        anOutput.Attributes.SetAttribute(
          UFDataAttribute.SortType, aSortType.GetDescription()
        );
        break;
    }
  }

  /// <summary>
  /// Adds a wrapper for the content of a cell that is clickable for sorting.
  /// </summary>
  /// <param name="anOutput"></param>
  /// <param name="aTable"></param>
  /// <param name="aTableRow"></param>
  private void AddButtonWrapper(
    TagHelperOutput anOutput,
    TTable aTable,
    TTableRow aTableRow
  )
  {
    anOutput.PreContent.SetHtmlContent(
      $"<button" +
      $" class=\"{this.GetTableHeaderButtonClasses(aTable, aTableRow)}\"" +
      $">"
    );
    anOutput.PostContent.SetHtmlContent("</button>");
  }

  #endregion
}