// <copyright file="UFTableHeaderRowTagHelperBase.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid.Base;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Table.Base;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Constants;

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
public abstract class UFTableHeaderRowTagHelperBase<TTable> : UFTableRowTagHelperBase
  where TTable : UFTableTagHelperBase
{
  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
  {
    await base.ProcessAsync(context, output);
    TTable table = UFTagHelperTools.GetItem<TTable>(context, UFGridTagHelperBaseBase.Grid);
    await this.ProcessAsync(context, output, table);
  }

  #endregion

  #region protected methods

  /// <summary>
  /// Executes the tag helper.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="output"></param>
  /// <param name="table">Table the row is created inside in</param>
  protected virtual Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output,
    TTable table
  )
  {
    table.CellIndex = 0;
    if (table is { ProcessedFirstHeaderRow: null })
    {
      table.ProcessedFirstHeaderRow = this;
    }
    UFTagHelperTools.AddClasses(output, this.GetTableRowClasses(table));
    output.Attributes.SetAttribute(UFDataAttribute.HeaderRow());
    return Task.CompletedTask;
  }

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
