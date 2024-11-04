// <copyright file="IUFInputProperties.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Forms;

/// <summary>
/// Properties for an input field within a web form.
/// </summary>
public interface IUFInputProperties
{
  /// <summary>
  /// When set, use this value for label instead of getting from the
  /// <see cref="InputTagHelper.For"/> property.
  /// <para>
  /// When <see cref="Wrap"/> is false, the property is only used with checkbox and radio elements.
  /// For other input elements the value is ignored.
  /// </para>
  /// </summary>
  string Label { get; set; }
  
  /// <summary>
  /// When true do not render a label element.
  /// </summary>
  bool NoLabel { get; set; }

  /// <summary>
  /// When true, wrap input element in a div. With false just render the input element.
  /// </summary>
  bool Wrap { get; set; }
  
  /// <summary>
  /// Additional text that is shown below the label. It is only used if <see cref="Wrap"/> is true.
  /// </summary>
  string Description { get; set; }
  
  /// <summary>
  /// When true show a multiline text input (textarea).
  /// </summary>
  bool Multiline { get; set; }
  
  /// <summary>
  /// When true do not set a width.
  /// </summary>
  bool NoWidth { get; set; }
}
