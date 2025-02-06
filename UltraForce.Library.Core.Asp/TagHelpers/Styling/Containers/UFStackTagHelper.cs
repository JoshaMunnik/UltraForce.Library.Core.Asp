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
using UltraForce.Library.Core.Asp.Services;
using UltraForce.Library.Core.Asp.TagHelpers.Lib;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Asp.Types.Enums;

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Containers;

/// <summary>
/// Places children on top of each other. Each child should be wrapped with a
/// <see cref="UFStackItemTagHelper"/>.
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
public class UFStackTagHelper(IUFTheme aTheme) : UFTagHelperWithTheme(aTheme), IUFContainerProperties
{
  #region IUFContainerProperties

  /// <inheritdoc />
  [HtmlAttributeName("horizontal")]
  public UFContentPosition Horizontal { get; set; } = UFContentPosition.Stretch;

  /// <inheritdoc />
  [HtmlAttributeName("vertical")]
  public UFContentPosition Vertical { get; set; } = UFContentPosition.Stretch;

  /// <inheritdoc />
  [HtmlAttributeName("full-width")]
  public bool FullWidth { get; set; } = false;

  /// <inheritdoc />
  [HtmlAttributeName("full-height")]
  public bool FullHeight { get; set; } = false;

  /// <inheritdoc />
  [HtmlAttributeName("position-child")]
  public bool PositionChild { get; set; } = false;

  /// <inheritdoc />
  [HtmlAttributeName("padding")]
  public int Padding { get; set; } = 0;

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
  /// The default implementation calls <see cref="IUFTheme.GetStackClasses"/>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetStackClasses()
  {
    return this.Theme.GetStackClasses(this);
  }
  
  #endregion
}