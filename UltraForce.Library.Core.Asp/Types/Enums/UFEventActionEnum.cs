// <copyright file="UFEventActionEnum.cs" company="Ultra Force Development">
// Ultra Force Library - Copyright (C) 2025 Ultra Force Development
// </copyright>
// <author>Josha Munnik</author>
// <email>josha@ultraforce.com</email>
// <license>
// The MIT License (MIT)
//
// Copyright (C) 2025 Ultra Force Development
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

namespace UltraForce.Library.Core.Asp.Types.Enums;

/// <summary>
/// All possible actions that can be set for `data-uf-event-action`.
/// </summary>
public enum UFEventActionEnum
{
  /// <summary>
  /// Removes the target(s) from the DOM.
  /// </summary>
  [Description("remove-from-dom")]
  RemoveFromDom,
  
  /// <summary>
  /// Hides the target(s).
  /// </summary>
  [Description("hide")]
  Hide,
  
  /// <summary>
  /// Shows the target(s).
  /// </summary>
  [Description("show")]
  Show,
  
  /// <summary>
  /// Toggles visibility state of the target(s).
  /// </summary>
  [Description("toggle")]
  Toggle,
  
  /// <summary>
  /// Toggles the css class of the target(s).
  /// </summary>
  [Description("toggle-class")]
  ToggleClass,
  
  /// <summary>
  /// Removes from the class of the target(s).
  /// </summary>
  [Description("remove-from-class")]
  RemoveFromClass,
  
  /// <summary>
  /// Adds to the class of the target(s).
  /// </summary>
  [Description("add-to-class")]
  AddToClass,
  
  /// <summary>
  /// Shows the target(s) as a modal dialog.
  /// </summary>
  [Description("show-modal")]
  ShowModal,
  
  /// <summary>
  /// Shows the target(s) as a non-modal dialog.
  /// </summary>
  [Description("show-non-modal")]
  ShowNonModal,
  
  /// <summary>
  /// Closes the target(s).
  /// </summary>
  [Description("close")]
  Close,
  
  /// <summary>
  /// Sets the attribute at the target(s).
  /// </summary>
  [Description("set-attribute")]
  SetAttribute,
  
  /// <summary>
  /// Reloads the page.
  /// </summary>
  [Description("reload")]
  Reload,
  
  /// <summary>
  /// Sets the value of the target(s).
  /// </summary>
  [Description("set-value")]
  SetValue,
  
  /// <summary>
  /// Sets the innerText of the target(s).
  /// </summary>
  [Description("set-text")]
  SetText,
  
  /// <summary>
  /// Sets the innerHtml of
  /// </summary>
  [Description("set-html")]
  SetHtml,
}