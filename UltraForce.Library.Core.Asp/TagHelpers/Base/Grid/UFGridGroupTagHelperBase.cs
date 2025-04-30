// <copyright file="UFGridGroupTagHelperBase.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.TagHelpers.Base.Grid.Base;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Constants;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Grid;

/// <summary>
/// This tag helper should be used to group items within a grid when the grid uses sorting and/or
/// filtering.
/// <para>
/// The <see cref="UFGridTagHelperBase.RenderGroups"/> determines if the group renders a tag or
/// if only the children are rendered.
/// </para> 
/// </summary>
public class UFGridGroupTagHelperBase<TGrid> : TagHelper
  where TGrid : UFGridTagHelperBase
{
  #region public methods

  /// <inheritdoc />
  public override void Process(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    base.Process(context, output);
    TGrid? grid = UFTagHelperTools.GetItem<TGrid>(context, UFGridTagHelperBaseBase.Grid);
    context.Items[UFGridTagHelperBaseBase.Row] = this;
    grid.GridGroupIndex++;
    if (!grid.RenderGroups)
    {
      output.TagName = null;
      return;
    }
    output.TagName = "div";
    output.TagMode = TagMode.StartTagAndEndTag;
    UFTagHelperTools.AddClasses(output, this.GetGroupClasses(grid));
    if (grid.Sorting)
    {
      output.Attributes.Add(UFDataAttribute.ItemContainer());
    }
    if (grid.Filter)
    {
      output.Attributes.Add(UFDataAttribute.FilterContainer());
    }
  }
  
  #endregion
  
  #region protected methods
  
  /// <summary>
  /// Returns the css classes to use for the group container. This method will only be called if
  /// <see cref="UFGridTagHelperBase.RenderGroups"/> is <c>true</c>.
  /// <para>
  /// The default implementation returns an empty string.
  /// </para>
  /// </summary>
  /// <param name="grid"></param>
  /// <returns></returns>
  protected virtual string GetGroupClasses(TGrid grid)
  {
    return "";
  }
  
  #endregion
}