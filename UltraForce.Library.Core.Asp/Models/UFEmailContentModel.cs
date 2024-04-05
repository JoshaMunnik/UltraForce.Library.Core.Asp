// <copyright file="UFEmailContentModel.cs" company="Ultra Force Development">
// Ultra Force Library - Copyright (C) 2018 Ultra Force Development
// </copyright>
// <author>Josha Munnik</author>
// <email>josha@ultraforce.com</email>
// <license>
// The MIT License (MIT)
//
// Copyright (C) 2018 Ultra Force Development
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

namespace UltraForce.Library.Core.Asp.Models
{
  /// <summary>
  /// This models is a simple class containing content for an email both in html and plain text form.
  /// </summary>
  public class UFEmailContentModel
  {
    #region constructors

    /// <summary>
    /// Create structure
    /// </summary>
    /// <param name="anHtml">initial html text</param>
    /// <param name="aText">initial plain text</param>
    public UFEmailContentModel(string? anHtml = null, string? aText = null)
    {
      this.Html = anHtml;
      this.Text = aText;
    }

    #endregion

    #region public properties

    /// <summary>
    /// Content in html form or null if no content exists
    /// </summary>
    public string? Html { get; set; }

    /// <summary>
    /// Content in plain text form or null if no content exists.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// This property is true if both <see cref="Html"/> and 
    /// <see cref="Text"/>
    /// </summary>
    public bool IsEmpty => string.IsNullOrWhiteSpace(this.Html) && string.IsNullOrWhiteSpace(this.Text);

    #endregion
  }
}