// <copyright file="UFCellTagHelper.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.TagHelpers.Base;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Enums;
using UltraForce.Library.NetStandard.Extensions;
using UltraForce.Library.NetStandard.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Table;

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
public abstract class UFCellTagHelper(IUFModelExpressionRenderer aModelExpressionRenderer)
  : UFTagHelperWithModelExpressionRenderer(aModelExpressionRenderer)
{
  #region public properties
  
  /// <summary>
  /// Type of cell.
  /// </summary>
  [HtmlAttributeName("type")]
  public UFTableCellType Type { get; set; } = UFTableCellType.Auto;

  /// <summary>
  /// When non empty, set this value as width value. This can either be a css class or a
  /// unit definition. The method <see cref="UFTagHelperTools.IsCssValue"/> is used to determine
  /// which.
  /// </summary>
  [HtmlAttributeName("width")]
  public string Width { get; set; } = "";

  /// <summary>
  /// Type of sorting (only of use with <see cref="UFTableCellType.Header"/>). If type is set to
  /// <see cref="UFTableSortType.Auto"/> (default) the method will try to determine the type
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
  public UFTableSortType SortType { get; set; } = UFTableSortType.Auto;
  
  /// <summary>
  /// When true the cell contents will not be cached with sorting. This is useful when the cell
  /// contents will be changed while using a sorted table.
  /// </summary>
  [HtmlAttributeName("no-caching")]
  public bool NoCaching { get; set; } = false;

  /// <summary>
  /// An expression to be evaluated against the current model. When set, the method will use the
  /// type to adjust the sort type (if <see cref="SortType"/> is set
  /// to <see cref="UFTableSortType.Auto"/>).
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
  /// when <see cref="Type"/> is <see cref="UFTableCellType.Data"/>.
  /// </summary>
  [HtmlAttributeName("no-filter")]
  public bool NoFilter { get; set; } = false;

  #endregion

  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(TagHelperContext aContext, TagHelperOutput anOutput)
  {
    await base.ProcessAsync(aContext, anOutput);
    UFTableTagHelper table = (aContext.Items[UFTableTagHelper.Table] as UFTableTagHelper)!;
    UFTableRowTagHelper tableRow = (aContext.Items[UFTableRowTagHelper.Row] as UFTableRowTagHelper)!;
    UFTableCellType type = this.Type == UFTableCellType.Auto 
      ? (tableRow.Type == UFTableRowType.Header ? UFTableCellType.Header : UFTableCellType.Data) 
      : this.Type;
    anOutput.TagMode = TagMode.StartTagAndEndTag;
    anOutput.TagName = type == UFTableCellType.Header ? "th" : "td";
    UFTableSortType sortType = await this.ProcessForAsync(anOutput, type);
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
      (tableRow == table.ProcessedFirstHeaderRow) && table.Sorting && (sortType != UFTableSortType.None)
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
    UFTableCellType aType, UFTableTagHelper aTable, UFTableRowTagHelper aTableRow
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
    UFTableTagHelper aTable, UFTableRowTagHelper aTableRow
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
  private async Task<UFTableSortType> ProcessForAsync(
    TagHelperOutput anOutput, UFTableCellType aType
  )
  {
    if (this.For == null)
    {
      return this.SortType == UFTableSortType.Auto ? UFTableSortType.None : this.SortType;
    }
    Type type = this.For!.Metadata.UnderlyingOrModelType;
    UFTableSortType sortType = this.SortType;
    if (sortType == UFTableSortType.Auto)
    {
      if ((type == typeof(DateTime)) || (type == typeof(DateTime?)))
      {
        sortType = UFTableSortType.Date;
      }
      else if (
        UFReflectionTools.IsNumeric(type) || (type == typeof(bool)) || (type == typeof(bool?))
      )
      {
        sortType = UFTableSortType.Number;
      }
      else
      {
        sortType = UFTableSortType.Text;
      }
    }
    if (aType == UFTableCellType.Header)
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
  private void UpdateClasses(TagHelperOutput anOutput, UFTableCellType aType, UFTableTagHelper aTable, UFTableRowTagHelper aTableRow)
  {
    string classValue = this.GetTableCellClasses(aType, aTable, aTableRow);
    if (!string.IsNullOrEmpty(this.Width))
    {
      if (UFTagHelperTools.IsCssValue(this.Width))
      {
        UFTagHelperTools.AddAttribute(
          anOutput.Attributes, "style", "width: " + this.Width
        );
      }
      else
      {
        classValue += " " + this.Width;
      }
    }
    UFTagHelperTools.AddClasses(anOutput, classValue);
  }

  /// <summary>
  /// Adds a <see cref="UFDataAttribute.NoFilter"/> attribute if the cell is a data cell and
  /// <see cref="NoFilter"/> is true.
  /// </summary>
  /// <param name="anOutput"></param>
  private void SetFilter(TagHelperOutput anOutput)
  {
    if ((this.Type == UFTableCellType.Data) && this.NoFilter)
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
    TagHelperOutput anOutput, UFTableSortType aSortType, UFTableCellType aType 
  )
  {
    if ((aType != UFTableCellType.Header) || (aSortType == UFTableSortType.None))
    {
      return;
    }
    switch (aSortType)
    {
      case UFTableSortType.Number:
      case UFTableSortType.Date:
      case UFTableSortType.Text:
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
  private void AddButtonWrapper(TagHelperOutput anOutput, UFTableTagHelper aTable, UFTableRowTagHelper aTableRow)
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