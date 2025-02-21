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
  /// <param name="generator"></param>
  public UFModelExpressionRenderer(
    IHtmlGenerator generator
  )
  {
    this.HtmlGenerator = generator;
  }

  #endregion

  #region IUFModelExpressionRenderer

  /// <inheritdoc/>
  public async Task SetContentToNameAsync(
    TagHelperOutput output,
    ModelExpression expression,
    ViewContext viewContext
  )
  {
    await UFTagHelperTools.SetContentToTagBuilderAsync(
      output,
      () => this.HtmlGenerator.GenerateLabel(
        viewContext,
        expression.ModelExplorer,
        expression.Name,
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
  /// <para>
  /// If the value is an enum, the display name is used.
  /// </para>
  /// </summary>
  /// <param name="output"></param>
  /// <param name="expression"></param>
  /// <param name="viewContext"></param>
  public async Task SetContentToValueAsync(
    TagHelperOutput output,
    ModelExpression expression,
    ViewContext viewContext
  )
  {
    Type type = expression.Metadata.UnderlyingOrModelType;
    DefaultModelMetadata? metadata = expression.Metadata as DefaultModelMetadata;
    if ((type == typeof(DateTime)) || (type == typeof(DateTime?)))
    {
      await UFTagHelperTools.SetContentToHtmlAsync(
        output, expression.Model == null ? "-" : this.GetDateTimeHtml((DateTime)expression.Model)
      );
    }
    else if ((type == typeof(DateOnly)) || (type == typeof(DateOnly?)))
    {
      await UFTagHelperTools.SetContentToHtmlAsync(
        output, expression.Model == null ? "-" : this.GetDateHtml((DateOnly)expression.Model)
      );
    }
    else if ((type == typeof(TimeOnly)) || (type == typeof(TimeOnly?)))
    {
      await UFTagHelperTools.SetContentToHtmlAsync(
        output, expression.Model == null ? "-" : this.GetTimeHtml((TimeOnly)expression.Model)
      );
    }
    else if ((type == typeof(bool)) || (type == typeof(bool?)))
    {
      await UFTagHelperTools.SetContentToHtmlAsync(
        output,
        this.GetCheckBoxHtml((expression.Model != null) && (bool)expression.Model)
      );
    }
    else if (
      metadata?.Attributes.Attributes.FirstOrDefault(
        value => value is EmailAddressAttribute
      ) != null
    )
    {
      string email = (string)expression.Model;
      await UFTagHelperTools.SetContentToHtmlAsync(output, this.GetEmailLinkHtml(email));
    }
    else if (type.IsEnum)
    {
      await UFTagHelperTools.SetContentToHtmlAsync(
        output,
        expression.Model == null ? "-" : ((Enum)expression.Model).GetDisplayName()
      );
    }
    else
    {
      // set content by generating a textarea tag and use its inner html as content
      await UFTagHelperTools.SetContentToTagBuilderAsync(
        output,
        () => this.HtmlGenerator.GenerateTextArea(
          viewContext,
          expression.ModelExplorer,
          expression.Name,
          4,
          80,
          null
        )
      );
    }
  }

  /// <inheritdoc/>
  public IHtmlContentBuilder GetName(
    ModelExpression expression,
    ViewContext viewContext
  )
  {
    TagBuilder tagBuilder = this.HtmlGenerator.GenerateLabel(
      viewContext,
      expression.ModelExplorer,
      expression.Name,
      labelText: null,
      htmlAttributes: null
    );
    return tagBuilder.InnerHtml;
  }

  /// <inheritdoc/>
  public string GetValueAsText(
    ModelExpression expression,
    ViewContext viewContext
  )
  {
    Type type = expression.Metadata.UnderlyingOrModelType;
    DefaultModelMetadata? metadata = expression.Metadata as DefaultModelMetadata;
    if ((type == typeof(DateTime)) || (type == typeof(DateTime?)))
    {
      return expression.Model == null
        ? ""
        : ((DateTime)expression.Model).ToString("yyyy-MM-dd HH:mm:ss");
    }
    if ((type == typeof(DateOnly)) || (type == typeof(DateOnly?)))
    {
      return expression.Model == null
        ? ""
        : ((DateOnly)expression.Model).ToString("yyyy-MM-dd");
    }
    if ((type == typeof(TimeOnly)) || (type == typeof(TimeOnly?)))
    {
      return expression.Model == null
        ? ""
        : ((TimeOnly)expression.Model).ToString("HH:mm:ss");
    }
    if ((type == typeof(bool)) || (type == typeof(bool?)))
    {
      return expression.Model == null
        ? ""
        : (bool)expression.Model
          ? "true"
          : "false";
    }
    if (type.IsEnum)
    {
      return expression.Model == null ? "-" : ((Enum)expression.Model).GetDisplayDescription();
    }
    return expression.Model?.ToString() ?? "";
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
  /// <param name="isChecked"></param>
  /// <returns></returns>
  protected virtual string GetCheckBoxHtml(
    bool isChecked
  )
  {
    string checkedAttribute = isChecked ? " isChecked=\"isChecked\"" : "";
    return $"<input type=\"checkbox\" disabled=\"disabled\"{checkedAttribute}/>";
  }

  /// <summary>
  /// Generates an anchor tag using a mailto protocol.
  ///
  /// The default method just creates a basic input type without any styling. Subclasses
  /// can override if needed.
  /// </summary>
  /// <param name="email"></param>
  /// <returns></returns>
  protected virtual string GetEmailLinkHtml(
    string email
  )
  {
    return $"<a href=\"mailto:{email}\">{email}</a>";
  }

  /// <summary>
  /// Generates a html representation of a date and time. The default implementation returns the
  /// date/time in mysql format (yyyy-MM-dd HH:mm:ss) or '-' if the date is null.
  /// </summary>
  /// <param name="dateTime">Date/time or null if render no date and time</param>
  /// <returns></returns>
  protected virtual string GetDateTimeHtml(
    DateTime? dateTime
  )
  {
    return dateTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "-";
  }

  /// <summary>
  /// Generates a html representation of a date. The default implementation returns the
  /// date in mysql format (yyyy-MM-dd) or '-' if the date is null.
  /// </summary>
  /// <param name="date">Date or null if render no date</param>
  /// <returns></returns>
  protected virtual string GetDateHtml(
    DateOnly? date
  )
  {
    return date?.ToString("yyyy-MM-dd") ?? "-";
  }

  /// <summary>
  /// Generates a html representation of a time. The default implementation returns the
  /// time in mysql format (HH:mm:ss) or '-' if the time is null.
  /// </summary>
  /// <param name="time">Time or null if render no time</param>
  /// <returns></returns>
  protected virtual string GetTimeHtml(
    TimeOnly? time
  )
  {
    return time?.ToString("HH:mm:ss") ?? "-";
  }

  #endregion
}