// <copyright file="UFStackItemTagHelper.cs" company="Ultra Force Development">
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
/// This tag should be used with <see cref="UFStackTagHelper"/>, each child within the stack should
/// be wrapped with this tag.
/// <para>
/// Renders:
/// <code>
/// &lt;div class="{GetStackItemClasses()}"&gt;
/// {children}
/// &lt;/div&gt;
/// </code>
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[HtmlTargetElement("uf-column", TagStructure = TagStructure.NormalOrSelfClosing)]
public class UFStackItemTagHelper(IUFTheme aTheme) : UFTagHelperWithTheme(aTheme), IUFStackItemProperties
{
  #region IUFStackItemProperties

  /// <inheritdoc />
  [HtmlAttributeName("no-interaction")]
  public bool NoInteraction { get; set; }
  
  #endregion
  
  #region overriden public methods

  /// <inheritdoc />
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    output.TagName = "div";
    output.TagMode = TagMode.StartTagAndEndTag;
    UFTagHelperTools.AddClasses(output, this.GetStackItemClasses());
  }

  #endregion
  
  #region overridable protected methods
  
  /// <summary>
  /// The default implementation calls <see cref="IUFTheme.GetStackItemClasses"/>.
  /// </summary>
  /// <returns></returns>
  protected virtual string GetStackItemClasses()
  {
    return this.Theme.GetStackItemClasses(this);
  }
  
  #endregion
}