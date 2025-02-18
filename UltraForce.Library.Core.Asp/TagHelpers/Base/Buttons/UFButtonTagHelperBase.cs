// <copyright file="UFButtonTagHelperBase.cs" company="Ultra Force Development">
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
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using UltraForce.Library.Core.Asp.Services;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Buttons;

/// <summary>
/// Base class for buttons. 
/// <para>
/// Renders:
/// <code>
/// &lt;{a|button|div} class="{GetButtonClasses()}" {href} {disabled} {target}&gt;
///   {GetBeforeCaptionHtml()}
///   &lt;span class="{GetButtonCaptionClasses()}"&gt;{children}&lt;/span&gt;
///   {GetAfterCaptionHtml()}
/// &lt;/{a|button|div}&gt;
/// </code>
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public abstract class UFButtonTagHelperBase(
  EndpointDataSource endpointDataSource,
  IHtmlGenerator htmlGenerator,
  IUFModelExpressionRenderer modelExpressionRenderer
)
  : UFBaseButtonTagHelperBase(endpointDataSource, htmlGenerator, modelExpressionRenderer)
{
  #region protected methods

  /// <inheritdoc />
  protected sealed override string GetBeforeCaptionHtml(
    TagHelperContext context,
    TagHelperOutput output,
    bool hasCaption,
    bool isStatic
  )
  {
    return this.GetBeforeCaptionHtml(hasCaption, isStatic);
  }

  /// <inheritdoc />
  protected sealed override string GetAfterCaptionHtml(
    TagHelperContext context,
    TagHelperOutput output,
    bool hasCaption,
    bool isStatic
  )
  {
    return this.GetAfterCaptionHtml(hasCaption, isStatic);
  }

  /// <inheritdoc />
  protected sealed override string GetButtonCaptionClasses(
    TagHelperContext context,
    TagHelperOutput output,
    bool isStatic
  )
  {
    return this.GetButtonCaptionClasses(isStatic);
  }

  /// <inheritdoc />
  protected sealed override string GetButtonClasses(
    TagHelperContext context,
    TagHelperOutput output,
    bool hasCaption,
    bool isStatic
  )
  {
    return this.GetButtonClasses(hasCaption, isStatic);
  }

  /// <summary>
  /// The default implementation returns empty string.
  /// </summary>
  /// <param name="hasCaption">True if there is content for the caption</param>
  /// <param name="isStatic">True if the button will be rendered with a <c>div</c> tag</param>
  /// <returns>string containing html formatting</returns>
  protected virtual string GetBeforeCaptionHtml(
    bool hasCaption,
    bool isStatic
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// The default implementation returns empty string.
  /// </summary>
  /// <param name="hasCaption">True if there is content for the caption</param>
  /// <param name="isStatic">True if the button will be rendered with a <c>div</c> tag</param>
  /// <returns>string containing html formatting</returns>
  protected virtual string GetAfterCaptionHtml(
    bool hasCaption,
    bool isStatic
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// The default implementation returns empty string.
  /// </summary>
  /// <param name="isStatic">True if the button will be rendered with a <c>div</c> tag</param>
  /// <returns>css classes</returns>
  protected virtual string GetButtonCaptionClasses(
    bool isStatic
  )
  {
    return string.Empty;
  }

  /// <summary>
  /// The default implementation returns empty string.
  /// </summary>
  /// <param name="hasCaption">True if there is content for the caption</param>
  /// <param name="isStatic">True if the button will be rendered with a <c>div</c> tag</param>
  /// <returns>css classes</returns>
  protected virtual string GetButtonClasses(
    bool hasCaption,
    bool isStatic
  )
  {
    return string.Empty;
  }

  #endregion
}