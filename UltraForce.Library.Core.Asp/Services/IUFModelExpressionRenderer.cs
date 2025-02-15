// <copyright file="IUFModelExpressionRenderer.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UltraForce.Library.Core.Asp.Services;

/// <summary>
/// Defines a renderer for <see cref="ModelExpression"/>s. 
/// </summary>
public interface IUFModelExpressionRenderer
{
  /// <summary>
  /// Sets the content to the (display) name of the <see cref="ModelExpression"/>. The code is
  /// based on the code from <see cref="LabelTagHelper"/>.
  /// </summary>
  /// <param name="anOutput">Output to update</param>
  /// <param name="anExpression">Model expression to get name from</param>
  /// <param name="aViewContext">View context to use</param>
  Task SetContentToNameAsync(
    TagHelperOutput anOutput, ModelExpression anExpression, ViewContext aViewContext
  );

  /// <summary>
  /// Sets the content to the value of the <see cref="ModelExpression" />. 
  /// </summary>
  /// <param name="anOutput">Output to update</param>
  /// <param name="anExpression">Expression to get value from</param>
  /// <param name="aViewContext">View context to use</param>
  Task SetContentToValueAsync(
    TagHelperOutput anOutput, ModelExpression anExpression, ViewContext aViewContext
  );

  /// <summary>
  /// Gets the name for a model expression.
  /// </summary>
  /// <param name="anExpression">expression to get name for</param>
  /// <param name="aViewContext"></param>
  /// <returns>html representation of name</returns>
  IHtmlContentBuilder GetName(ModelExpression anExpression, ViewContext aViewContext);
  

  /// <summary>
  /// Gets the value as a text string.
  /// </summary>
  /// <param name="anExpression">expression to get value for</param>
  /// <param name="aViewContext"></param>
  /// <returns>text representation of value</returns>
  string GetValueAsText(ModelExpression anExpression, ViewContext aViewContext);
}