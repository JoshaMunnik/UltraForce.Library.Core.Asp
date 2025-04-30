// <copyright file="UFGridTagHelperBase.cs" company="Ultra Force Development">
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
/// Base class for grids.
/// </summary>
public class UFGridTagHelperBase : UFGridTagHelperBaseBase
{
  #region public properties
  
  /// <summary>
  /// When true, any <see cref="UFGridGroupTagHelperBase{TGrid}"/> within the grid will render itself.
  /// Else only the children within each group are rendered. 
  /// </summary>
  public bool RenderGroups { get; set; } = false;

  /// <summary>
  /// The number of items per group. Required if the items are not grouped and there are no controls
  /// to determine the number of items from.
  /// </summary>
  public int? ItemCount { get; set; } = null;
  
  #endregion
  
  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    await base.ProcessAsync(context, output);
    output.TagName = "div";
    output.TagMode = TagMode.StartTagAndEndTag;
    UFTagHelperTools.AddClasses(output, this.GetGridClasses());
    // process the child content explicitly
    TagHelperContent? childContent = await output.GetChildContentAsync();
    if (childContent != null) {
      output.Content.SetHtmlContent(childContent);
    }
    if (!this.Sorting)
    {
      return;
    }
    if (this.GridControlCount == 0)
    {
      throw new Exception("Sorting requires grid controls.");
    }
    output.Attributes.Add(UFDataAttribute.GridSorting());
    if (this.ItemCount != null)
    {
      output.Attributes.Add(UFDataAttribute.GroupSize(this.ItemCount));
    }
  }

  #endregion
  
  #region protected methods
  
  /// <summary>
  /// Returns the css classes to use for the grid container. The grid container is only used
  /// if <see cref="UFGridTagHelperBaseBase.Filter"/> is <c>true</c>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetGridContainerClasses()
  {
    return "";
  }

  /// <summary>
  /// Classes for the grid itself.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetGridClasses()
  {
    return "";
  }
  
  #endregion
  
  #region internal methods
  
  internal override TagHelperAttribute GetFilterAttribute(
    string value
  ) => UFDataAttribute.FilterInput(value);

  internal override TagHelperAttribute GetSortingAttribute(
    string value
  ) => UFDataAttribute.GridSorting(value);

  internal override string GetContainerClasses() => this.GetGridContainerClasses();

  #endregion
  
  #region internal properties
  
  /// <summary>
  /// Updated by controls inside the grid.
  /// </summary>
  internal int GridControlCount { get; set; }
  
  /// <summary>
  /// Updated by groups inside the grid.
  /// </summary>
  internal int GridGroupIndex { get; set; }
  
  /// <summary>
  /// Updated by items inside the grid.
  /// </summary>
  internal int GridItemIndex { get; set; }
  
  #endregion
}