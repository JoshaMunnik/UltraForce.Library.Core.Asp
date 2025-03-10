// <copyright file="UFBasicFlexTagHelperBase.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.TagHelpers.Base;
using UltraForce.Library.Core.Asp.Types.Enums;

namespace UltraForce.Library.Core.Asp.TagHelpers.Layout.Base;

/// <summary>
/// Abstract class for basic flex containers. It only offers properties to position the contents.
/// <para>
/// When using this tag helper, make sure to include the stylesheet in the html:
/// <code>
/// &lt;link rel="stylesheet" href="/_content/UltraForce.Library.Core.Asp/css/uf-styles.css"/&gt;
/// </code>
/// </para>
/// <para>
/// Renders:
/// <code>
/// &lt;div class="{GetClasses()}"&gt;
///   {children}
/// &lt;/div&gt;
/// </code>
/// </para>
/// </summary>
/// <param name="flexType"></param>
/// <param name="tag"></param>
public abstract class UFBasicFlexTagHelperBase(UFFlexTypeEnum flexType, string tag = "div") 
  : UFStyledTagHelperBase(tag)
{
  #region public properties

  /// <summary>
  /// How to align the items in the opposite direction. 
  /// </summary>
  public UFFlexAlignItemsEnum AlignCrossAxis { get; set; } = UFFlexAlignItemsEnum.Start;

  /// <summary>
  /// How to distribute the items in the opposite direction. This property is only of use
  /// when <see cref="Wrap"/> is true and there are multiple rows or columns.
  /// </summary>
  [HtmlAttributeName("distribute-cross-axis")]
  public UFFlexDistributeContentEnum DistributeCrossAxis { get; set; } = UFFlexDistributeContentEnum.Start;
  
  /// <summary>
  /// When true, reverse the order of the items.
  /// </summary>
  [HtmlAttributeName("reverse")]
  public bool Reverse { get; set; } = false;

  #endregion
  
  #region overridable protected methods

  /// <summary>
  /// The default implementation uses styles from 'uf-styles.css'. 
  /// </summary>
  /// <returns></returns>
  protected override string GetClasses()
  {
    string classes = "uf-flex";
    classes += flexType switch
    {
      UFFlexTypeEnum.Row => " uf-flex__row",
      UFFlexTypeEnum.Column => " uf-flex__column",
      _ => throw new ArgumentOutOfRangeException(nameof(flexType), flexType, null),
    };
    if (this.Reverse)
    {
      classes += flexType switch
      {
        UFFlexTypeEnum.Row => " uf-flex__row--is-reversed",
        UFFlexTypeEnum.Column => " uf-flex__column--is-reversed",
        _ => "",
      };
    }
    classes += this.AlignCrossAxis switch
    {
      UFFlexAlignItemsEnum.Center => " uf-flex--align-cross-center",
      UFFlexAlignItemsEnum.End => " uf-flex--align-cross-end",
      UFFlexAlignItemsEnum.Stretch => " uf-flex--align-cross-stretch",
      UFFlexAlignItemsEnum.Base => " uf-flex--align-cross-baseline",
      _ => " uf-flex--align-cross-start"
    };
    classes += this.DistributeCrossAxis switch
    {
      UFFlexDistributeContentEnum.End => " uf-flex--distribute-cross-end",
      UFFlexDistributeContentEnum.Center => " uf-flex--distribute-cross-center",
      UFFlexDistributeContentEnum.SpaceAround => " uf-flex--distribute-cross-around",
      UFFlexDistributeContentEnum.SpaceBetween => " uf-flex--distribute-cross-between",
      UFFlexDistributeContentEnum.SpaceEvenly => " uf-flex--distribute-cross-evenly",
      UFFlexDistributeContentEnum.Stretch => " uf-flex--distribute-cross-stretch",
      _ => " uf-flex--distribute-cross-start"
    };
    return classes;
  }

  #endregion
}