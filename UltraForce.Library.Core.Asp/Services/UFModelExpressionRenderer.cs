// <copyright file="UFModelExpressionRenderer.cs" company="Ultra Force Development">
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

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.Core.Extensions;

namespace UltraForce.Library.Core.Asp.Services;

/// <summary>
/// An implementation of <see cref="IUFModelExpressionRenderer"/>.   
/// </summary>
public class UFModelExpressionRenderer : IUFModelExpressionRenderer
{
  #region constructors

  /// <summary>
  /// Constructs an instance of the <see cref="UFModelExpressionRenderer"/>.
  /// </summary>
  /// <param name="aGenerator"></param>
  public UFModelExpressionRenderer(IHtmlGenerator aGenerator)
  {
    this.HtmlGenerator = aGenerator;
  }

  #endregion

  #region IUFModelExpressionRenderer

  /// <inheritdoc/>
  public async Task SetContentToNameAsync(
    TagHelperOutput anOutput, ModelExpression anExpression, ViewContext aViewContext
  )
  {
    await UFTagHelperTools.SetContentToTagBuilderAsync(
      anOutput,
      () => this.HtmlGenerator.GenerateLabel(
        aViewContext,
        anExpression.ModelExplorer,
        anExpression.Name,
        labelText: null,
        htmlAttributes: null
      )
    );
  }

  /// <summary>
  /// Sets the content to the value of the <see cref="ModelExpression" />. It handles date
  /// and bool types. Date types are formatted to "yyyy-MM-dd HH:mm:ss". Bool types are shown
  /// as a checked or unchecked checkbox.
  /// <para>
  /// If the model expression uses the <see cref="EmailAddressAttribute"/> the value gets
  /// rendered using a mailto: link.
  /// </para> 
  /// </summary>
  /// <param name="anOutput"></param>
  /// <param name="anExpression"></param>
  /// <param name="aViewContext"></param>
  public async Task SetContentToValueAsync(
    TagHelperOutput anOutput, ModelExpression anExpression, ViewContext aViewContext
  )
  {
    Type type = anExpression.Metadata.UnderlyingOrModelType;
    DefaultModelMetadata? metadata = anExpression.Metadata as DefaultModelMetadata;
    if ((type == typeof(DateTime)) || (type == typeof(DateTime?)))
    {
      await UFTagHelperTools.SetContentToHtmlAsync(
        anOutput, anExpression.Model == null ? "-" : this.GetDateTimeHtml((DateTime)anExpression.Model)
      );
    }
    else if ((type == typeof(DateOnly)) || (type == typeof(DateOnly?)))
    {
      await UFTagHelperTools.SetContentToHtmlAsync(
        anOutput, anExpression.Model == null ? "-" : this.GetDateHtml((DateOnly)anExpression.Model)
      );
    }
    else if ((type == typeof(TimeOnly)) || (type == typeof(TimeOnly?)))
    {
      await UFTagHelperTools.SetContentToHtmlAsync(
        anOutput, anExpression.Model == null ? "-" : this.GetTimeHtml((TimeOnly)anExpression.Model)
      );
    }
    else if ((type == typeof(bool)) || (type == typeof(bool?)))
    {
      await UFTagHelperTools.SetContentToHtmlAsync(
        anOutput,
        this.GetCheckBoxHtml((anExpression.Model != null) && (bool)anExpression.Model)
      );
    }
    else if (
      metadata?.Attributes.Attributes.FirstOrDefault(
        value => value is EmailAddressAttribute
      ) != null
    )
    {
      string email = (string)anExpression.Model;
      await UFTagHelperTools.SetContentToHtmlAsync(anOutput, this.GetEmailLinkHtml(email));
    }
    else if (type.IsEnum)
    {
      await UFTagHelperTools.SetContentToHtmlAsync(
        anOutput, 
        anExpression.Model == null ? "-" : ((Enum) anExpression.Model).GetDisplayDescription()
      );
    }
    else
    {
      // set content by generating a textarea tag and use its inner html as content
      await UFTagHelperTools.SetContentToTagBuilderAsync(
        anOutput,
        () => this.HtmlGenerator.GenerateTextArea(
          aViewContext,
          anExpression.ModelExplorer,
          anExpression.Name,
          4,
          80,
          null
        )
      );
    }
  }
  

  /// <summary>
  /// Gets the name for an model expression.
  /// </summary>
  /// <param name="anExpression">expression to get name for</param>
  /// <param name="aViewContext"></param>
  /// <returns>html representation of name</returns>
  public IHtmlContentBuilder GetName(ModelExpression anExpression, ViewContext aViewContext)
  {
    TagBuilder tagBuilder = this.HtmlGenerator.GenerateLabel(
      aViewContext,
      anExpression.ModelExplorer,
      anExpression.Name,
      labelText: null,
      htmlAttributes: null
    );
    return tagBuilder.InnerHtml;
  }
  
  #endregion

  #region protected properties

  /// <summary>
  /// Html generator instance
  /// </summary>
  protected IHtmlGenerator HtmlGenerator { get; private set; }

  #endregion

  #region virtual protected methods

  /// <summary>
  /// Generates a input tag using checkbox type.
  ///
  /// The default method just creates a basic input type without any styling. Subclasses
  /// can override if needed.
  /// </summary>
  /// <param name="aChecked"></param>
  /// <returns></returns>
  protected virtual string GetCheckBoxHtml(bool aChecked)
  {
    string isChecked = aChecked ? " checked=\"checked\"" : "";
    return $"<input type=\"checkbox\" disabled=\"disabled\"{isChecked}/>";
  }

  /// <summary>
  /// Generates an anchor tag using a mailto protocol.
  ///
  /// The default method just creates a basic input type without any styling. Subclasses
  /// can override if needed.
  /// </summary>
  /// <param name="anEmail"></param>
  /// <returns></returns>
  protected virtual string GetEmailLinkHtml(string anEmail)
  {
    return $"<a href=\"mailto:{anEmail}\">{anEmail}</a>";
  }

  /// <summary>
  /// Generates a html representation of a date and time. The default implementation returns the
  /// date/time in mysql format (yyyy-MM-dd HH:mm:ss) or '-' if the date is null.
  /// </summary>
  /// <param name="aDateTime">Date/time or null if render no date and time</param>
  /// <returns></returns>
  protected virtual string GetDateTimeHtml(DateTime? aDateTime)
  {
    return aDateTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "-";
  }

  /// <summary>
  /// Generates a html representation of a date. The default implementation returns the
  /// date in mysql format (yyyy-MM-dd) or '-' if the date is null.
  /// </summary>
  /// <param name="aDate">Date or null if render no date</param>
  /// <returns></returns>
  protected virtual string GetDateHtml(DateOnly? aDate)
  {
    return aDate?.ToString("yyyy-MM-dd") ?? "-";
  }

  /// <summary>
  /// Generates a html representation of a time. The default implementation returns the
  /// time in mysql format (HH:mm:ss) or '-' if the time is null.
  /// </summary>
  /// <param name="aDate">Time or null if render no time</param>
  /// <returns></returns>
  protected virtual string GetTimeHtml(TimeOnly? aDate)
  {
    return aDate?.ToString("HH:mm:ss") ?? "-";
  }

  #endregion
}