// <copyright file="UFFontAwesomeTheme.cs" company="Ultra Force Development">
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

using Microsoft.IdentityModel.Tokens;
using UltraForce.Library.Core.Asp.TagHelpers.Styling.Buttons;

namespace UltraForce.Library.Core.Asp.Services;

/// <summary>
/// <see cref="UFFontAwesomeTheme"/> implements icon related methods for use with font awesome.
/// </summary>
public class UFFontAwesomeTheme : UFTheme
{
  #region UFTheme

  /// <inheritdoc />
  public override string GetButtonIconHtml(IUFButtonProperties aProperties)
  {
    string classValue = this.GetButtonIconCssClasses(aProperties);
    return string.IsNullOrEmpty(classValue) ? "" : $"<i class=\"{classValue}\"></i>";
  }
  
  #endregion
  
  #region protected support methods

  /// <summary>
  /// Gets the css classes for an icon. The default implementation assumes font awesome v6+ is used.
  /// If the icon is not empty, the method checks if the icon starts with "fa" and if not, it adds
  /// the required fa classes:<br/>
  /// `fa fa-{Icon}`
  /// </summary>
  /// <returns>Css classes or empty string if there is no icon</returns>
  protected virtual string GetButtonIconCssClasses(IUFButtonProperties aProperties)
  {
    string icon = aProperties.Icon ?? "";
    if (icon.IsNullOrEmpty())
    {
      return "";
    }
    if (icon.StartsWith("fa", StringComparison.OrdinalIgnoreCase))
    {
      return icon;
    }
    return $"fa fa-{icon}";
  }

  #endregion
}