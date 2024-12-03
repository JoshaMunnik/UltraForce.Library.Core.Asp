// <copyright file="IUFButtonProperties.cs" company="Ultra Force Development">
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
/// The properties for a button.
/// </summary>
public interface IUFButtonProperties
{
  /// <summary>
  /// Color scheme to use for the button. When <see cref="UFButtonColor.Auto"/> is used, the
  /// styling will decide the color based on the button state.
  /// </summary>
  public UFButtonColor Color { get; set; }

  /// <summary>
  /// Size of the button.
  /// </summary>
  public UFButtonSize Size { get; set; }

  /// <summary>
  /// Style to draw the button in.
  /// </summary>
  public UFButtonVariant Variant { get; set; }

  /// <summary>
  /// Optional font awesome icon to show in the button. Use the name without the <code>fa-</code>
  /// prefix.
  /// </summary>
  public string? Icon { get; set; }

  /// <summary>
  /// Position of the icon in the button. Only of use if <see cref="Icon"/> has been set. When
  /// <see cref="UFButtonIconPosition.Auto"/> is used, the styling will decide the position. 
  /// </summary>
  public UFButtonIconPosition IconPosition { get; set; }

  /// <summary>
  /// If the button is an anchor, a `div` tag is used instead of `a` tag. With buttons the
  /// `disabled` attribute is set.
  /// </summary>
  public bool Disabled { get; set; }

  /// <summary>
  /// When <code>true</code> the button is rendered with a div element.
  /// </summary>
  public bool Static { get; set; }
  
  /// <summary>
  /// When <code>true</code> the button type is set to submit. This property is only of use if
  /// the button is placed inside a form element.
  /// </summary>
  public bool Submit { get; set; }
  
  /// <summary>
  /// When true stretch the button to the full width of the parent element.
  /// </summary>
  public bool FullWidth { get; set; }
  
  /// <summary>
  /// When true stretch the button to the full height of the parent element.
  /// </summary>
  public bool FullHeight { get; set; }
  
  /// <summary>
  /// Additional vertical padding to add to the content within the button.
  /// </summary>
  public int VerticalPadding { get; set; }
  
  /// <summary>
  /// Additional horizontal padding to add to the content within the button.
  /// </summary>
  public int HorizontalPadding { get; set; }
  
  /// <summary>
  /// The horizontal position of the caption and icon within the button.
  /// </summary>
  public UFContentPosition HorizontalContentPosition { get; set; }
  
  /// <summary>
  /// The vertical position of the caption and icon within the button.
  /// </summary>
  public UFContentPosition VerticalContentPosition { get; set; }
}