// <copyright file="UFButtonStyle.cs" company="Ultra Force Development">
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
/// Styles that might set multiple properties to a predefined value.
/// </summary>
public enum UFButtonStyle
{
  /// <summary>
  /// Do not set any property.
  /// </summary>
  Custom,

  /// <summary>
  /// Sets the icon to a visual representing sending something (envelope)
  /// </summary>
  Send,

  /// <summary>
  /// Set the icon to a visual representing saving something (floppy disk)
  /// </summary>
  Save,

  /// <summary>
  /// Sets the icon to a visual representing going back (left arrow) and use outline variant.
  /// </summary>
  Back,

  /// <summary>
  /// Sets the icon to a visual representing creating something (new file)
  /// </summary>
  Create,

  /// <summary>
  /// Sets the icon to a visual representing editing something (pencil)
  /// </summary>
  Edit,

  /// <summary>
  /// Sets the icon to a visual representing showing details (info)
  /// </summary>
  Details,

  /// <summary>
  /// Sets the icon to a visual representing deleting something (trash)
  /// </summary>
  Delete,
  
  /// <summary>
  /// Sets the icon to a visual representing a list (list)
  /// </summary>
  List,
  
  /// <summary>
  /// Sets the icon to a visual representing copying something (copy)
  /// </summary>
  Copy,
  
  /// <summary>
  /// Sets the icon to a visual representing a group of users (copy)
  /// </summary>
  Users,
  
  /// <summary>
  /// Sets the icon to a visual representing adding (plus)
  /// </summary>
  Add,
  
  /// <summary>
  /// Sets the icon to a visual representing adding (minus)
  /// </summary>
  Remove
}