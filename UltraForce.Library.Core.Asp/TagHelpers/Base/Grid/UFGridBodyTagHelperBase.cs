// <copyright file="UFGridBodyTagHelperBase.cs" company="Ultra Force Development">
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
/// Base class for the grid body tag helper.
/// </summary>
/// <typeparam name="TGrid"></typeparam>
public class UFGridBodyTagHelperBase<TGrid> : TagHelper
where TGrid: UFGridTagHelperBase
{
  #region public methods

  /// <inheritdoc />
  public override void Process(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    base.Process(context, output);
    TGrid grid = UFTagHelperTools.GetItem<TGrid>(context, UFGridTagHelperBaseBase.Grid);
    output.TagName = "div";
    output.TagMode = TagMode.StartTagAndEndTag;
    UFTagHelperTools.AddClasses(output, this.GetBodyClasses(grid));
    output.Attributes.SetAttribute(UFDataAttribute.GridBody());
  }

  #endregion
  
  #region protected methods

  /// <summary>
  /// Returns the css classes to use for a body.
  /// </summary>
  /// <param name="grid">grid the body is used within</param>
  /// <returns></returns>
  protected virtual string GetBodyClasses(
    TGrid grid
  )
  {
    return "";
  }
  
  #endregion
}