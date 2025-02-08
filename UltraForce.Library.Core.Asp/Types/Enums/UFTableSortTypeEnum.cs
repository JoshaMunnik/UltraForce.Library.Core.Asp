// <copyright file="UFTableSortTypeEnum.cs" company="Ultra Force Development">
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

using System.ComponentModel;
using UltraForce.Library.Core.Asp.TagHelpers.Base.Table;

namespace UltraForce.Library.Core.Asp.Types.Enums;

/// <summary>
/// Determines how a column is sorted in a table.
/// </summary>
public enum UFTableSortTypeEnum
{
  /// <summary>
  /// Determine type from <see cref="UFCellTagHelperBase.For"/> (if any), else defaults to
  /// text
  /// </summary>
  Auto,

  /// <summary>
  /// Sort values as text
  /// </summary>
  [Description("text")]
  Text,

  /// <summary>
  /// Sort values as number
  /// </summary>
  [Description("number")]
  Number,

  /// <summary>
  /// Sort values as dates
  /// </summary>
  [Description("date")]
  Date,

  /// <summary>
  /// No sorting
  /// </summary>
  None
}