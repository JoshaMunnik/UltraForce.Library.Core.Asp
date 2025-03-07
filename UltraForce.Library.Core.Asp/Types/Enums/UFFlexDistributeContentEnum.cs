// <copyright file="UFFlexAlignContentEnum.cs" company="Ultra Force Development">
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

namespace UltraForce.Library.Core.Asp.Types.Enums;

/// <summary>
/// How to distribute the children in the opposite direction of the main axis (column or row).
/// </summary>
public enum UFFlexDistributeContentEnum
{
  /// <summary>
  /// Place at start
  /// </summary>
  Start,

  /// <summary>
  /// Place at center
  /// </summary>
  Center,

  /// <summary>
  /// Place at end
  /// </summary>
  End,

  /// <summary>
  /// Stretch each child
  /// </summary>
  Stretch,

  /// <summary>
  /// Distribute space between children
  /// </summary>
  SpaceBetween,

  /// <summary>
  /// Distribute space around children
  /// </summary>
  SpaceAround,

  /// <summary>
  /// Distribute space evenly between children and the edge
  /// </summary>
  SpaceEvenly,

  /// <summary>
  /// Size each child equally. This only works in the direction of the main axis.
  /// </summary>
  SizeEvenly,
}