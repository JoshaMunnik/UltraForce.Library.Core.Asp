// <copyright file="UFButtonProperties.cs" company="Ultra Force Development">
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

using UltraForce.Library.Core.Asp.Types.Enums;

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Buttons;

/// <summary>
/// A simple implementation of the <see cref="IUFButtonProperties"/>. Assigning default values
/// to each property. 
/// </summary>
public class UFButtonProperties : IUFButtonProperties
{
  /// <inheritdoc />
  public UFButtonColor Color { get; set; } = UFButtonColor.Auto;

  /// <inheritdoc />
  public UFButtonSize Size { get; set; } = UFButtonSize.Normal;

  /// <inheritdoc />
  public UFButtonVariant Variant { get; set; } = UFButtonVariant.Auto;

  /// <inheritdoc />
  public string? Icon { get; set; } = null;

  /// <inheritdoc />
  public UFButtonIconPosition IconPosition { get; set; } = UFButtonIconPosition.Auto;

  /// <inheritdoc />
  public bool Disabled { get; set; } = false;

  /// <inheritdoc />
  public bool Static { get; set; } = false;

  /// <inheritdoc />
  public bool Submit { get; set; } = false;

  /// <inheritdoc />
  public bool FullWidth { get; set; } = false;

  /// <inheritdoc />
  public bool FullHeight { get; set; } = false;

  /// <inheritdoc />
  public int VerticalPadding { get; set; } = 0;

  /// <inheritdoc />
  public int HorizontalPadding { get; set; } = 0;

  /// <inheritdoc />
  public UFContentPosition HorizontalContentPosition { get; set; } = UFContentPosition.Center;
  
  /// <inheritdoc />
  public UFContentPosition VerticalContentPosition { get; set; } = UFContentPosition.Center;
}