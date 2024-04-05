// <copyright file="UFTagHelperWithViewContext.cs" company="Ultra Force Development">
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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Services;

namespace UltraForce.Library.Core.Asp.TagHelpers.Lib;

/// <summary>
/// Base class for tag helpers. It exposes <see cref="ViewContext"/> property.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
public class UFTagHelperWithViewContext : UFTagHelperWithTheme
{
  #region constructors
  
  /// <summary>
  /// Constructs an instance of <see cref="UFTagHelperWithViewContext"/>
  /// </summary>
  /// <param name="aTheme"></param>
  protected UFTagHelperWithViewContext(IUFTheme aTheme) : base(aTheme)
  {
  }
  
  #endregion
  
  #region public properties

  /// <summary>
  /// View context (will be set externally by the razor engine)
  /// </summary>
  [HtmlAttributeNotBound]
  [ViewContext]
  public ViewContext ViewContext { get; set; } = null!;

  #endregion

}