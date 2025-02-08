// <copyright file="UFStackTagHelper.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Enums;

namespace UltraForce.Library.Core.Asp.TagHelpers.Layout;

/// <summary>
/// Places children on top of each other. Each child should be wrapped with a
/// <see cref="UFStackItemTagHelper"/>.
/// <para>
/// When using this tag helper, make sure to include the stylesheet in the html:
/// <code>
/// &lt;link rel="stylesheet" href="/_content/UltraForce.Library.Core.Asp/css/uf-styles.css"/&gt;
/// </code>
/// </para>
/// <para>
/// Renders:
/// <code>
/// &lt;div class="{GetStackClasses()}"&gt;
///   {children}
/// &lt;/div&gt;
/// </code>
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[HtmlTargetElement("uf-stack", TagStructure = TagStructure.NormalOrSelfClosing)]
public class UFStackTagHelper : TagHelper
{
  #region public properties

  /// <summary>
  /// How to position the child horizontally within the container.
  /// </summary>
  [HtmlAttributeName("horizontal")]
  public UFContentPositionEnum Horizontal { get; set; } = UFContentPositionEnum.None;

  /// <summary>
  /// How to position the child vertically within the container.
  /// </summary>
  [HtmlAttributeName("vertical")]
  public UFContentPositionEnum Vertical { get; set; } = UFContentPositionEnum.None;

  /// <summary>
  /// When true use the width of the parent, otherwise use the width of the content.
  /// </summary>
  [HtmlAttributeName("full-width")]
  public bool FullWidth { get; set; } = false;

  /// <summary>
  /// When true use the height of the parent, otherwise use the height of the content.
  /// </summary>
  [HtmlAttributeName("full-height")]
  public bool FullHeight { get; set; } = false;

  #endregion
  
  #region overriden public methods

  /// <inheritdoc />
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    output.TagName = "div";
    output.TagMode = TagMode.StartTagAndEndTag;
    UFTagHelperTools.AddClasses(output, this.GetStackClasses());
  }

  #endregion
  
  #region overridable protected methods
  
  /// <summary>
  /// The default implementation uses styles from 'uf-styles.css'. 
  /// </summary>
  /// <returns></returns>
  protected virtual string GetStackClasses()
  {
    string classes = "uf-stack__container";
    classes += this.Horizontal switch
    {
      UFContentPositionEnum.Start => " uf-stack__container--position-child-horizontal-at-start",
      UFContentPositionEnum.Center => " uf-stack__container--position-child-horizontal-at-center",
      UFContentPositionEnum.End => " uf-stack__container--position-child-horizontal-at-end",
      UFContentPositionEnum.Stretch => " uf-stack__container--stretch-child-horizontal",
      _ => "",
    };
    classes += this.Vertical switch
    {
      UFContentPositionEnum.Start => " uf-stack__container--position-child-vertical-at-start",
      UFContentPositionEnum.Center => " uf-stack__container--position-child-vertical-at-center",
      UFContentPositionEnum.End => " uf-stack__container--position-child-vertical-at-end",
      UFContentPositionEnum.Stretch => " uf-stack__container--stretch-child-vertical",
      _ => "",
    };
    if (this.FullWidth)
    {
      classes += " uf-stack__container--has-full-width";
    }
    if (this.FullHeight)
    {
      classes += " uf-stack__container--has-full-height";
    }
    return classes;
  }
  
  #endregion
}