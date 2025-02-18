// <copyright file="UFNotConditionTagHelper.cs" company="Ultra Force Development">
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

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UltraForce.Library.Core.Asp.TagHelpers
{
  /// <summary>
  /// This tag helper adds support for not-condition attribute. When specified
  /// the tag and its children are only processed if the value is <c>false</c>.
  /// <para>
  /// This tag has the opposite effect of <see cref="UFConditionTagHelper"/>.
  /// </para>
  /// </summary>
  /// <remarks>
  /// Based on the example code of: 
  /// https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/authoring
  /// </remarks>
  [HtmlTargetElement(Attributes = "uf-not-condition")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
  [SuppressMessage("ReSharper", "UnusedType.Global")]
  public class UFNotConditionTagHelper : TagHelper
  {
    /// <summary>
    /// Condition that will be set via the attribute
    /// </summary>
    [HtmlAttributeName("uf-not-condition")]
    public bool NotCondition { get; set; }

    /// <inheritdoc />
    public override void Process(
      TagHelperContext context,
      TagHelperOutput output
    )
    {
      if (this.NotCondition)
      {
        output.SuppressOutput();
      }
    }
  }
}