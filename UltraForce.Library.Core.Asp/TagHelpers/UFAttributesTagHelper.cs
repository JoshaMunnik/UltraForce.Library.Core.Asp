// <copyright file="UFAttributesTagHelper.cs" company="Ultra Force Development">
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

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UltraForce.Library.Core.Asp.TagHelpers
{
  /// <summary>
  /// This tag helper adds support for <c>uf-attributes</c> attribute. The value is a list
  /// of tag helper attributes which get added to the attributes of the tag.
  /// </summary>
  /// <code>
  /// ...
  /// // uses c# 9.0 shorthand (for list and new)
  /// &lt;div uf-attributes="[new("data-test", "1"), new("data-parameter", "2")]"&gt;
  /// ...
  /// </code>
  [HtmlTargetElement(Attributes = "uf-attributes")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
  [SuppressMessage("ReSharper", "UnusedType.Global")]
  public class UFAttributesTagHelper : TagHelper
  {
    #region public properties
    
    /// <summary>
    /// Condition that will be set via the attribute
    /// </summary>
    [HtmlAttributeName("uf-attributes")]
    public IList<TagHelperAttribute>? Attributes { get; set; }
    
    #endregion
    
    #region public methods

    /// <inheritdoc />
    public override void Process(
      TagHelperContext context,
      TagHelperOutput output
    )
    {
      if (this.Attributes == null)
      {
        return;
      }
      foreach (TagHelperAttribute attribute in this.Attributes)
      {
        output.Attributes.Add(attribute);
      }
    }
    
    #endregion
  }
}