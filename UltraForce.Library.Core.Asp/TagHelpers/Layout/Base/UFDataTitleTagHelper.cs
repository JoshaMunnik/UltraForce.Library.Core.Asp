// <copyright file="UFDataTagHelperBase.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.TagHelpers.Base;

namespace UltraForce.Library.Core.Asp.TagHelpers.Layout.Base;

/// <summary>
/// This base class can be used to store or retrieve data.
/// <para>
/// When using the tag with a self-closing tag, the content of this tag will be set to the result
/// from <see cref="GetDataAsync"/>.
/// </para>
/// <para>
/// When using the tag with and open and close tag with some content. The content will be used
/// as a parameter for <see cref="SetDataAsync"/> and the tag helper will not render anything.
/// </para>
/// </summary>
public abstract class UFDataTagHelperBase : UFTagHelperWithViewContext
{
  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(
    TagHelperContext context,
    TagHelperOutput output
  )
  {
    if (output.TagMode == TagMode.SelfClosing)
    {
      output.TagName = string.Empty;
      output.Content.SetHtmlContent(await this.GetDataAsync());
    }
    else
    {
      TagHelperContent? content = await output.GetChildContentAsync();
      await this.SetDataAsync(content.GetContent());
      output.SuppressOutput();
    }
  }

  #endregion
  
  #region protected methods

  /// <summary>
  /// Returns the stored data to be used as content for the tag.
  /// </summary>
  /// <returns>Stored text</returns>
  protected abstract Task<string> GetDataAsync();
  
  /// <summary>
  /// Stores the text so it can be used later to create the content for the tag.
  /// </summary>
  /// <param name="text">Text to store</param>
  /// <returns></returns>
  protected abstract Task SetDataAsync(string text);

  #endregion
}