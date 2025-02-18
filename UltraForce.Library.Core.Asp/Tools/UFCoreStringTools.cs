// <copyright file="UFCoreStringTools.cs" company="Ultra Force Development">
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

// <copyright file="UFStringTools.cs" company="Ultra Force Development">
// Ultra Force Library - Copyright (C) 2013 Ultra Force Development
// </copyright>
// <author>Josha Munnik</author>
// <email>josha@ultraforce.com</email>
// <summary>An utility class that adds extra string related methods.</summary>

using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace UltraForce.Library.Core.Asp.Tools
{
  /// <summary>
  /// An utility class that adds extra string related methods.
  /// </summary>
  public static class UFCoreStringTools
  {
    #region Private variables

    /// <summary>
    /// Used with UTF8 to ANSI.
    /// </summary>
    private static Encoding? s_encoder = null;

    #endregion

    #region Public methods

    /// <summary>
    /// Converts an utf8 string to ascii string. Characters that can not be 
    /// converted are removed.
    /// </summary>
    /// <param name="text">Text using UTF8 encoding</param>
    /// <returns>Text using ascii encoding</returns>
    public static string Utf8ToAscii(string text)
    {
      s_encoder ??= ASCIIEncoding.GetEncoding(
        "us-ascii",
        new EncoderReplacementFallback(string.Empty),
        new DecoderExceptionFallback()
      );
      return s_encoder.GetString(s_encoder.GetBytes(text));
    }

    /// <summary>
    /// Gets the html content as string.
    /// </summary>
    /// <param name="content">Content to convert to string</param>
    /// <returns>Content as string</returns>
    public static string GetString(IHtmlContent content)
    {
      using StringWriter writer = new();
      content.WriteTo(writer, HtmlEncoder.Default);
      return writer.ToString();
    }

    #endregion
  }
}