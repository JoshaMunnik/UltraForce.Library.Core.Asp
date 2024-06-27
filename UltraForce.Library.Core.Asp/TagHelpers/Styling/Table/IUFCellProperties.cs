// <copyright file="IUFCellProperties.cs" company="Ultra Force Development">
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

namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Table;

public interface IUFCellProperties
{
  /// <summary>
  /// Horizontal position of content within the cell
  /// </summary>
  UFContentPosition Horizontal { get; set; }

  /// <summary>
  /// Vertical position of content within the cell
  /// </summary>
  UFContentPosition Vertical { get; set; }

  /// <summary>
  /// When false (default) the data in the cell can be found via the filter. When true an
  /// attribute with the name "data-no-filter" is added to the cell tag. This property is
  /// only processed when <see cref="Type"/> is <see cref="UFTableCellType.Data"/>.
  /// </summary>
  bool NoFilter { get; set; }

  /// <summary>
  /// When non empty, set this value as width value. This can either be a css class or a
  /// style definition. 
  /// </summary>
  string Width { get; set; }

  /// <summary>
  /// Size of text within the cell
  /// </summary>
  UFTableTextSize TextSize { get; set; }

  /// <summary>
  /// When true wrap content if cell is to small
  /// </summary>
  bool Wrap { get; set; }
}