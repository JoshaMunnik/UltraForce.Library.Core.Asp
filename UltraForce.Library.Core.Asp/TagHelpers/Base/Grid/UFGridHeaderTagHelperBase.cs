// <copyright file="UFGridHeaderTagHelperBase.cs" company="Ultra Force Development">
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
/// Base class for the grid header tag helper. It can be used as a container for the grid controls
/// and apply additional styling.
/// </summary>
/// <typeparam name="TGrid"></typeparam>
public class UFGridHeaderTagHelperBase<TGrid> : TagHelper
where TGrid: UFGridTagHelperBase
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
    await this.ProcessAsync(context, output, grid);
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
  protected virtual Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output,
    TGrid grid
  )
  {
    output.TagName = "div";
    output.TagMode = TagMode.StartTagAndEndTag;
    UFTagHelperTools.AddClasses(output, this.GetHeaderClasses(grid));
    return Task.CompletedTask;
  }

  /// <summary>
  /// Returns the css classes to use for a header.
  /// </summary>
  /// <param name="grid">grid the header is used within</param>
  /// <returns></returns>
  protected virtual string GetHeaderClasses(
    TGrid grid
  )
  {
    return "";
  }
  
  #endregion
}