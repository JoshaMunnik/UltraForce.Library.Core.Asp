// <copyright file="UFTableTagHelper.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.TagHelpers.Lib;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Enums;

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Table;

/// <summary>
/// Creates a table row.
/// </summary>
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
[HtmlTargetElement("uf-table-row", TagStructure = TagStructure.NormalOrSelfClosing)]
public class UFTableRowTagHelper(IUFTheme aTheme) 
  : UFTagHelperWithTheme(aTheme), IUFTableRowProperties
{
  #region public constants
  
  /// <summary>
  /// The key that children can use to access this instance.
  /// </summary>
  public const string Row = "uf_row";
  
  #endregion
  
  #region public properties

  /// <summary>
  /// The type of row
  /// </summary>
  [HtmlAttributeName("type")]
  public UFTableRowType Type { get; set; } = UFTableRowType.Data;

  /// <summary>
  /// When true show rows with alternating background colors
  /// </summary>
  [HtmlAttributeName("alternate")]
  public bool Alternate { get; set; } = true;

  /// <summary>
  /// When true show different background on hover
  /// </summary>
  [HtmlAttributeName("hover")]
  public bool Hover { get; set; } = true;

  /// <summary>
  /// Keep the row at top or bottom if table gets sorted
  /// <para>
  /// Setting this value to <see cref="UFTableSortLocation.Top"/> or
  /// <see cref="UFTableSortLocation.Bottom"/> will create a `data-sort-location` attribute with
  /// either "top" or "bottom" as value.
  /// </para>
  /// </summary>
  [HtmlAttributeName("sort-location")]
  public UFTableSortLocation SortLocation { get; set; } = UFTableSortLocation.Middle;

  #endregion

  #region public methods

  /// <inheritdoc />
  public override void Process(TagHelperContext context, TagHelperOutput anOutput)
  {
    base.Process(context, anOutput);
    anOutput.TagName = "tr";
    UFTableTagHelper table = (context.Items[UFTableTagHelper.Table] as UFTableTagHelper)!;
    if (table is { ProcessedFirstHeaderRow: null } && (this.Type == UFTableRowType.Header))
    {
      table.ProcessedFirstHeaderRow = this;
    }
    context.Items[Row] = this;
    switch (this.SortLocation)
    {
      case UFTableSortLocation.Top:
        anOutput.Attributes.SetAttribute(UFDataAttribute.SortLocation, "top");
        break;
      case UFTableSortLocation.Bottom: 
        anOutput.Attributes.SetAttribute(UFDataAttribute.SortLocation, "bottom");
        break;
    }
    if (this.Type == UFTableRowType.Header)
    {
      anOutput.Attributes.SetAttribute(UFDataAttribute.HeaderRow, "1");
    }
    UFTagHelperTools.AddClasses(anOutput, this.Theme.GetTableRowClasses(this, table));
  }

  #endregion
}