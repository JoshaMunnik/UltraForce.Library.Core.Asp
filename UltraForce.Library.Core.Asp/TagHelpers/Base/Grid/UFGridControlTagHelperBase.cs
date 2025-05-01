// <copyright file="UFGridControlTagHelperBase.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid.Base;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Constants;
using UltraForce.Library.Core.Asp.Types.Enums;
using UltraForce.Library.NetStandard.Extensions;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Grid;

/// <summary>
/// Base class for grid controls. It renders either a div or button depending on if the grid uses
/// sorting. 
/// </summary>
/// <param name="modelExpressionRenderer"></param>
/// <typeparam name="TGrid"></typeparam>
public class UFGridControlTagHelperBase<TGrid>(
  IUFModelExpressionRenderer modelExpressionRenderer
) : UFGridHeaderTagHelperBaseBase(modelExpressionRenderer)
where TGrid : UFGridTagHelperBase
{
  #region public methods
  
  /// <inheritdoc />
  public override async Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    await base.ProcessAsync(context, output);
    TGrid grid = UFTagHelperTools.GetItem<TGrid>(context, UFGridTagHelperBaseBase.Grid);
    int itemIndex = grid.GridControlCount;
    grid.GridControlCount++;
    await this.ProcessAsync(context, output, grid, itemIndex);
  }
  
  #endregion
  
  #region protected methods

  /// <summary>
  /// Processes the tag helper and sets the output.
  /// <para>
  /// The default implementation sets the tag and some attributes.
  /// </para>
  /// </summary>
  /// <param name="context"></param>
  /// <param name="output"></param>
  /// <param name="grid"></param>
  /// <param name="itemIndex">Index of control item</param>
  protected virtual Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output,
    TGrid grid,
    int itemIndex
  )
  {
    grid.GridItemSizes.Add(this);
    UFSortTypeEnum sortType = this.GetSortType();
    output.TagName = sortType == UFSortTypeEnum.None ? "div" : "button";
    output.TagMode = TagMode.StartTagAndEndTag;
    UFTagHelperTools.AddClasses(output, this.GetControlClasses(grid, sortType));
    output.Attributes.SetAttribute(UFDataAttribute.SortControl(sortType.GetDescription()));
    if (grid.Filter)
    {
      output.Attributes.SetAttribute(UFDataAttribute.NoFilter());
    }
    return Task.CompletedTask;
  } 

  /// <summary>
  /// Gets the css classes for the control element.
  /// </summary>
  /// <param name="grid"></param>
  /// <param name="sortType"></param>
  /// <returns></returns>
  protected virtual string GetControlClasses(
    TGrid grid,
    UFSortTypeEnum sortType
  )
  {
    return "";
  }
  
  #endregion
}