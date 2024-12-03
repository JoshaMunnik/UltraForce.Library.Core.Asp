// <copyright file="IUFFlexProperties.cs" company="Ultra Force Development">
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

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Layout;

/// <summary>
/// Properties for a flex container in a web page.
/// </summary>
public interface IUFFlexProperties
{
  /// <summary>
  /// How to distribute the items in the direction of the container.
  /// </summary>
  UFFlexJustifyContent JustifyContent { get; set; }

  /// <summary>
  /// How to align the items in the opposite direction. 
  /// </summary>
  UFFlexAlignItems AlignItems { get; set; }

  /// <summary>
  /// How to distribute the items in the opposite direction.
  /// </summary>
  UFFlexAlignContent AlignContent { get; set; }
  
  /// <summary>
  /// When true, the container is used to contain buttons. 
  /// </summary>
  bool Buttons { get; set; }

  /// <summary>
  /// Gap index, determining minimal space between children. When null, the theme default is used. 
  /// </summary>
  int? Gap { get; set; }

  /// <summary>
  /// When true use all available horizontal space in the parent.
  /// </summary>
  bool FullWidth { get; set; }

  /// <summary>
  /// When true use all available vertical space in the parent.
  /// </summary>
  bool FullHeight { get; set; }

  /// <summary>
  /// When true wrap children across multiple rows or columns when they don"t fit.
  /// </summary>
  bool Wrap { get; set; }
}