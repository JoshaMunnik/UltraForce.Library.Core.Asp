// <copyright file="UFStyledTagHelperBase.cs" company="Ultra Force Development">
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
using UltraForce.Library.Core.Asp.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base;

/// <summary>
/// A base class for simple tag helpers that render a styled tag.
/// <para>
/// It renders the following:
/// <code>
/// &lt;{tag} class="{GetClasses()}"&gt;
///   {children}
/// &lt;/{tag}&gt;
/// </code>
/// </para>
/// <para>Subclasses can override the <see cref="GetClasses"/> method to provide alternative custom
/// css classes.
/// </para>
/// </summary>
/// <param name="tag">tag to use</param>
/// <param name="mode">tag mode to use</param>
/// <param name="classes">css classes to use</param>
public abstract class UFStyledTagHelperBase(
  string tag,
  TagMode mode = TagMode.StartTagAndEndTag,
  string classes = ""
) : TagHelper
{
  #region public methods

  /// <inheritdoc />
  public override void Process(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    output.TagName = tag;
    output.TagMode = mode;
    UFTagHelperTools.AddClasses(output, this.GetClasses());
  }

  #endregion

  #region protected methods

  /// <summary>
  /// The default implementation returns the value of the classes variable passed in the
  /// constructor.
  /// <para>
  /// Subclasses can override this method if some logic is needed to determine the css classes.
  /// </para>
  /// </summary>
  /// <returns></returns>
  protected virtual string GetClasses()
  {
    return classes;
  }

  #endregion
}