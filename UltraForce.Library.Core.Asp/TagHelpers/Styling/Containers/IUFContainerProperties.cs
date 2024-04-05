// <copyright file="IUFContainerProperties.cs" company="Ultra Force Development">
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

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Containers;

/// <summary>
/// Properties for a container that can optionally position its child content.
/// </summary>
public interface IUFContainerProperties
{
  /// <summary>
  /// How to position the child horizontally within the container.
  /// </summary>
  UFContentPosition Horizontal { get; set; }

  /// <summary>
  /// How to position the child vertically within the container.
  /// </summary>
  UFContentPosition Vertical { get; set; }

  /// <summary>
  /// When true use the width of the parent, otherwise use the width of the content.
  /// </summary>
  bool FullWidth { get; set; }

  /// <summary>
  /// When true use the height of the parent, otherwise use the height of the content.
  /// </summary>
  bool FullHeight { get; set; }

  /// <summary>
  /// When true position the children in the container using <see cref="Horizontal"/> and
  /// <see cref="Vertical"/>.
  /// </summary>
  bool PositionChild { get; set; }

  /// <summary>
  /// Padding to add to the container.
  /// </summary>
  int Padding { get; set; }
}