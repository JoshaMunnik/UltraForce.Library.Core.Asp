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
  #region constructors
  
  /// <summary>
  /// Constructs a new instance of <see cref="UFButtonProperties"/> using default values.
  /// </summary>
  public UFButtonProperties() { }

  /// <summary>
  /// Constructs a new instance of <see cref="UFButtonProperties"/> copying values from another
  /// instance.
  /// </summary>
  public UFButtonProperties(
    IUFButtonProperties aSource
  )
  {
    this.Type = aSource.Type;
    this.Size = aSource.Size;
    this.Icon = aSource.Icon;
    this.IconPosition = aSource.IconPosition;
    this.Disabled = aSource.Disabled;
    this.Static = aSource.Static;
    this.Submit = aSource.Submit;
    this.FullWidth = aSource.FullWidth;
    this.FullHeight = aSource.FullHeight;
    this.VerticalPadding = aSource.VerticalPadding;
    this.HorizontalPadding = aSource.HorizontalPadding;
    this.HorizontalContentPosition = aSource.HorizontalContentPosition;
    this.VerticalContentPosition = aSource.VerticalContentPosition;
  }
  
  #endregion
  
  #region IUFButtonProperties
  
  /// <inheritdoc />
  public UFButtonType Type { get; set; } = UFButtonType.Auto;

  /// <inheritdoc />
  public UFSize Size { get; set; } = UFSize.Normal;

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
  public int VerticalPadding { get; set; } = -1;

  /// <inheritdoc />
  public int HorizontalPadding { get; set; } = -1;

  /// <inheritdoc />
  public UFContentPosition HorizontalContentPosition { get; set; } = UFContentPosition.Center;
  
  /// <inheritdoc />
  public UFContentPosition VerticalContentPosition { get; set; } = UFContentPosition.Center;
  
  #endregion
}