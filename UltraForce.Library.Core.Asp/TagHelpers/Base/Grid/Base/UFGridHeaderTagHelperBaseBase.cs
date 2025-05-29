// <copyright file="UFGridHeaderTagHelperBaseBase.cs" company="Ultra Force Development">
// Ultra Force Library - Copyright (C) 2025 Ultra Force Development
// </copyright>
// <author>Josha Munnik</author>
// <email>josha@ultraforce.com</email>
// <license>
// The MIT License (MIT)
//
// Copyright (C) 2025 Ultra Force Development
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

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.Types.Constants;
using UltraForce.Library.Core.Asp.Types.Enums;
using UltraForce.Library.Core.Asp.Types.Interfaces;
using UltraForce.Library.NetStandard.Extensions;
using UltraForce.Library.NetStandard.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Grid.Base;

/// <summary>
/// Base class for header items within a grid or table.
/// </summary>
/// <param name="modelExpressionRenderer"></param>
public abstract class UFGridHeaderTagHelperBaseBase(
  IUFModelExpressionRenderer modelExpressionRenderer
)
  : UFTagHelperWithModelExpressionRenderer(modelExpressionRenderer), IUFGridItemSize
{
  #region public properties

  /// <summary>
  /// Type of sorting. If type is set to <see cref="UFSortTypeEnum.Auto"/> (default) the
  /// method will try to determine the type from <see cref="For"/>.
  /// </summary>
  [HtmlAttributeName("sort-type")]
  public UFSortTypeEnum SortType { get; set; } = UFSortTypeEnum.Auto;

  /// <summary>
  /// An expression to be evaluated against the current model. When set, the method will use the
  /// type to adjust the sort type (if <see cref="SortType"/> is set
  /// to <see cref="UFSortTypeEnum.Auto"/>).
  /// <para>
  /// When set, the name is used as content for the header.
  /// </para>
  /// </summary>
  [HtmlAttributeName("for")]
  public ModelExpression? For { get; set; } = null;

  /// <inheritdoc />
  [HtmlAttributeName("item-size")]
  public UFGridItemSizeEnum UFGridItemSize { get; set; } = UFGridItemSizeEnum.Auto;

  /// <inheritdoc />
  [HtmlAttributeName("min-size")]
  public string? MinSize { get; set; } = null;

  /// <inheritdoc />
  [HtmlAttributeName("max-size")]
  public string? MaxSize { get; set; } = null;

  /// <inheritdoc />
  [HtmlAttributeName("size")]
  public string? Size { get; set; } = null;

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
    await this.ProcessForAsync(output);
  }

  #endregion

  #region protected methods

  /// <summary>
  /// Gets the sort type. Handles <see cref="UFSortTypeEnum.Auto"/> and <see cref="For"/>.
  /// </summary>
  /// <returns></returns>
  protected UFSortTypeEnum GetSortType()
  {
    if (this.For == null)
    {
      return this.SortType == UFSortTypeEnum.Auto ? UFSortTypeEnum.None : this.SortType;
    }
    Type type = this.For!.Metadata.UnderlyingOrModelType;
    UFSortTypeEnum sortType = this.SortType;
    if (sortType == UFSortTypeEnum.Auto)
    {
      if (
        (type == typeof(DateTime)) ||
        (type == typeof(DateTime?)) ||
        (type == typeof(DateOnly)) ||
        (type == typeof(DateOnly?))
      )
      {
        sortType = UFSortTypeEnum.Date;
      }
      else if (
        UFReflectionTools.IsNumeric(type) || (type == typeof(bool)) || (type == typeof(bool?))
      )
      {
        sortType = UFSortTypeEnum.Number;
      }
      else
      {
        sortType = UFSortTypeEnum.Text;
      }
    }
    return sortType;
  }

  #endregion

  #region private methods

  /// <summary>
  /// Processes the <see cref="For"/> property. Set the content to the name of the property
  /// if there is an expression.
  /// </summary>
  /// <param name="output">Output to update</param>
  /// <returns>Sort type</returns>
  private async Task ProcessForAsync(
    TagHelperOutput output
  )
  {
    if (this.For == null)
    {
      return;
    }
    await this.ModelExpressionRenderer.SetContentToNameAsync(
      output, this.For, this.ViewContext
    );
  }

  #endregion
}
