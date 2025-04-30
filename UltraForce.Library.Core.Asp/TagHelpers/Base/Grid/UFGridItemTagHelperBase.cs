// <copyright file="UFGridItemTagHelperBase.cs" company="Ultra Force Development">
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

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Grid;

/// <summary>
/// Base class for grid items. It defines attributes depending on if the grid uses sorting and
/// filtering and if the grid contains <see cref="UFGridGroupTagHelperBase{TGrid}"/>.
/// </summary>
/// <param name="modelExpressionRenderer"></param>
/// <typeparam name="TGrid"></typeparam>
public class UFGridItemTagHelperBase<TGrid>(
  IUFModelExpressionRenderer modelExpressionRenderer
) : UFGridItemTagHelperBaseBase(modelExpressionRenderer)
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
    output.TagName = "div";
    output.TagMode = TagMode.StartTagAndEndTag;
    UFTagHelperTools.AddClasses(output, this.GetItemClasses(grid));
    int groupIndex = grid.GridGroupIndex;
    int itemIndex = grid.GridItemIndex;
    grid.GridItemIndex++;
    if (!grid.RenderGroups && (groupIndex > 0))
    {
      output.Attributes.SetAttribute(UFDataAttribute.ItemGroup(groupIndex));
    }
    if (!grid.Filter)
    {
      return;
    }
    if (groupIndex > 0)
    {
      if (!grid.RenderGroups) {
        output.Attributes.SetAttribute(UFDataAttribute.FilterGroup(groupIndex));
      }
      return;
    }
    int groupSize = grid.ItemCount ?? grid.GridControlCount;
    if (groupSize > 0)
    {
      output.Attributes.SetAttribute(UFDataAttribute.FilterGroup(itemIndex / groupSize));
    }
  }
  
  #endregion
  
  #region protected methods

  /// <summary>
  /// Gets the css classes for the control element.
  /// </summary>
  /// <param name="grid"></param>
  /// <returns></returns>
  protected virtual string GetItemClasses(
    TGrid grid
  )
  {
    return "";
  }
  
  #endregion
}
