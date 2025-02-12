// <copyright file="UFViewdataTagHelper.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.TagHelpers.Base;

namespace UltraForce.Library.Core.Asp.TagHelpers;

/// <summary>
/// This tag helper can be used to set or read data from <see cref="ViewContext.ViewData"/>.
/// <para>
/// When using the tag with a self closing tag, the content of this tag will be set to text stored
/// <see cref="ViewContext.ViewData"/> (without any surrounding tags).
/// </para>
/// <para>
/// When using the tag with and open and close tag with some content. The content will be stored in
/// the <see cref="ViewContext.ViewData"/>.
/// </para>
/// </summary>
[HtmlTargetElement("uf-viewdata", TagStructure = TagStructure.NormalOrSelfClosing)]
public class UFViewdataTagHelper : UFTagHelperWithViewContext
{
  #region public properties
  
  /// <summary>
  /// The key to use to read or write the data with.
  /// </summary>
  [HtmlAttributeName("key")]
  public string Key { get; set; } = string.Empty;
  
  #endregion
  
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
      output.Content.SetHtmlContent(this.ViewContext.ViewData[this.Key]?.ToString());
    }
    else
    {
      TagHelperContent? content = await output.GetChildContentAsync();
      this.ViewContext.ViewData[this.Key] = content.GetContent();
      output.SuppressOutput();
    }
  }

  #endregion
}