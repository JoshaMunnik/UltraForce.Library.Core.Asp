// <copyright file="UFTagHelperTools.cs" company="Ultra Force Development">
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

using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UltraForce.Library.Core.Asp.Tools
{
  /// <summary>
  /// Support methods for <see cref="TagHelper"/>
  /// </summary>
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public static partial class UFTagHelperTools
  {
    #region private constants

    private static readonly string[] ValidUnits =
    {
      "cm", "mm", "in", "px", "pt", "pc", // Absolute length units
      "em", "ex", "ch", "rem", "vw", "vh", "vmin", "vmax", "%", // Relative length units
      "deg", "grad", "rad", "turn", // Angle units
      "s", "ms", // Time units
      "Hz", "kHz", // Frequency units
      "dpi", "dpcm", "dppx" // Resolution units
    };

    [GeneratedRegex(@"^-?\d*\.?\d+([a-zA-Z%]+)?$", RegexOptions.Compiled)]
    private static partial Regex UnitRegDef();

    private static readonly Regex UnitRegex = UnitRegDef();

    #endregion

    #region public methods

    /// <summary>
    /// Sets the content to the inner html of a tag. The content is only set if there is no current
    /// content and no other tag helper as adjusted the content already. 
    /// </summary>
    /// <param name="output">Output to update content of</param>
    /// <param name="factory">Factory that generates a tag to get inner content of</param>
    public static async Task SetContentToTagBuilderAsync(
      TagHelperOutput output,
      Func<TagBuilder> factory
    )
    {
      // do not update the content if another tag helper targeting this element has already done so.
      if (!output.IsContentModified)
      {
        // only replace if there is no child content
        TagHelperContent childContent = await output.GetChildContentAsync();
        if (childContent.IsEmptyOrWhiteSpace)
        {
          // build tag and copy it's inner html
          TagBuilder tagBuilder = factory();
          if (tagBuilder is { HasInnerHtml: true })
          {
            output.Content.SetHtmlContent(tagBuilder.InnerHtml);
          }
        }
        else
        {
          output.Content.SetHtmlContent(childContent);
        }
      }
    }

    /// <summary>
    /// Sets the content to a specific text. The text is only set if there is not another tag helper
    /// and the content is empty.
    /// </summary>
    /// <param name="output">Output to update</param>
    /// <param name="text">Text to set</param>
    public static async Task SetContentToText(
      TagHelperOutput output,
      string text
    )
    {
      // do not update the content if another tag helper targeting this element has already done so.
      if (!output.IsContentModified)
      {
        // only replace if there is no child content
        TagHelperContent childContent = await output.GetChildContentAsync();
        if (childContent.IsEmptyOrWhiteSpace)
        {
          output.Content.SetContent(text);
        }
        else
        {
          output.Content.SetHtmlContent(childContent);
        }
      }
    }

    /// <summary>
    /// Sets the content to a specific html. The html is only set if there is not another tag helper
    /// and the content is empty.
    /// </summary>
    /// <param name="output">Output to update</param>
    /// <param name="html">Html to set</param>
    public static async Task SetContentToHtmlAsync(
      TagHelperOutput output,
      string html
    )
    {
      // do not update the content if another tag helper targeting this element has already done so.
      if (!output.IsContentModified)
      {
        // only replace if there is no child content
        TagHelperContent childContent = await output.GetChildContentAsync();
        if (childContent.IsEmptyOrWhiteSpace)
        {
          output.Content.SetHtmlContent(html);
        }
        else
        {
          output.Content.SetHtmlContent(childContent);
        }
      }
    }

    /// <summary>
    /// Gets the label from a model expression, by building label with <see cref="IHtmlGenerator"/>
    /// and getting its inner html contents.
    /// </summary>
    /// <param name="generator">Generator to use</param>
    /// <param name="viewContext">View context to render in</param>
    /// <param name="expression">Expression to get label from</param>
    /// <param name="labelText">When non null, use this value instead of getting the label</param>
    /// <returns>the inner html or labelText if it was not empty</returns>
    public static string GetLabel(
      IHtmlGenerator generator,
      ViewContext viewContext,
      ModelExpression? expression,
      string labelText
    )
    {
      string label = labelText;
      if (!string.IsNullOrEmpty(label) || (expression == null))
      {
        return label;
      }
      TagBuilder tagBuilder = generator.GenerateLabel(
        viewContext,
        expression.ModelExplorer,
        expression.Name,
        labelText: null,
        htmlAttributes: null
      );
      label = UFCoreStringTools.GetString(tagBuilder.InnerHtml);
      return label;
    }

    /// <summary>
    /// Builds a tag helper attribute list from a dictionary.
    /// </summary>
    /// <param name="attributes"></param>
    /// <returns></returns>
    public static TagHelperAttributeList CreateAttributeList(
      Dictionary<string, string> attributes
    )
    {
      TagHelperAttributeList attributeList = new();
      foreach (KeyValuePair<string, string> attribute in attributes)
      {
        attributeList.Add(
          string.IsNullOrEmpty(attribute.Value)
            ? new TagHelperAttribute(attribute.Key)
            : new TagHelperAttribute(attribute.Key, attribute.Value)
        );
      }
      return attributeList;
    }

    /// <summary>
    /// Renders a tag helper that does not use any attributes to a string.
    /// </summary>
    /// <param name="childContent">Child content for the tag helper</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string RenderTagHelper<T>(
      string childContent
    ) where T : TagHelper, new()
    {
      DefaultTagHelperContent content = new();
      return RenderTagHelper<T>(new TagHelperAttributeList(), content.SetHtmlContent(childContent));
    }

    /// <summary>
    /// Renders a tag helper that does not use any attributes to a string.
    /// </summary>
    /// <param name="attributeList"></param>
    /// <param name="childContent">Optional child content for the tag helper</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string RenderTagHelper<T>(
      Dictionary<string, string> attributeList,
      string childContent
    ) where T : TagHelper, new()
    {
      DefaultTagHelperContent content = new();
      return RenderTagHelper<T>(attributeList, content.SetHtmlContent(childContent));
    }

    /// <summary>
    /// Renders a tag helper that does not use any attributes to a string.
    /// </summary>
    /// <param name="content">Optional child content for the tag helper</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string RenderTagHelper<T>(
      TagHelperContent? content = null
    ) where T : TagHelper, new()
    {
      return RenderTagHelper<T>(new TagHelperAttributeList(), content);
    }

    /// <summary>
    /// Renders a tag helper to a string.
    /// </summary>
    /// <param name="attributes"></param>
    /// <param name="childContent">Optional child content for the tag helper</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string RenderTagHelper<T>(
      Dictionary<string, string> attributes,
      TagHelperContent? childContent = null
    ) where T : TagHelper, new()
    {
      return RenderTagHelper<T>(CreateAttributeList(attributes), childContent);
    }

    /// <summary>
    /// Renders a tag helper to a string.
    /// 
    /// See:
    /// https://github.com/MissaouiChedy/RenderingTagHelperInsideAnother/blob/master/src/RenderingTagHelperInsideAnother/TagHelpers/WrapperTagHelper.cs
    /// </summary>
    /// <param name="attributeList"></param>
    /// <param name="childContent">Optional child content for the tag helper</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string RenderTagHelper<T>(
      TagHelperAttributeList attributeList,
      TagHelperContent? childContent = null
    ) where T : TagHelper, new()
    {
      T tagHelper = new();
      TagHelperOutput output = new(
        tagName: "div",
        attributes: attributeList,
        getChildContentAsync: (
            useCachedResult,
            encoder
          ) =>
          Task.Run<TagHelperContent>(() => childContent ?? new DefaultTagHelperContent())
      )
      {
        TagMode = TagMode.StartTagAndEndTag
      };
      output.Content.SetHtmlContent(childContent);
      TagHelperContext context = new(
        allAttributes: attributeList,
        items: new Dictionary<object, object>(),
        uniqueId: Guid.NewGuid().ToString()
      );
      tagHelper.Process(context, output);
      return RenderTagHelperOutput(output);
    }

    /// <summary>
    /// Renders the output of a tag helper to a string.
    ///
    /// See:
    /// https://github.com/MissaouiChedy/RenderingTagHelperInsideAnother/blob/master/src/RenderingTagHelperInsideAnother/TagHelpers/WrapperTagHelper.cs
    /// </summary>
    /// <param name="output"></param>
    /// <param name="encoder"></param>
    /// <returns></returns>
    public static string RenderTagHelperOutput(
      TagHelperOutput output,
      HtmlEncoder? encoder = null
    )
    {
      using StringWriter writer = new();
      output.WriteTo(writer, encoder ?? HtmlEncoder.Default);
      return writer.ToString();
    }

    /// <summary>
    /// Tries to find an attribute with a certain name.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static TagHelperAttribute? FindAttribute(
      TagHelperAttributeList list,
      string name
    )
    {
      return list.FirstOrDefault(tagHelperAttribute => tagHelperAttribute.Name == name);
    }

    /// <summary>
    /// Adds a value for an attribute. If the attribute exists, the value gets inserted before
    /// the attributes original value.
    /// </summary>
    /// <param name="attributes"></param>
    /// <param name="attribute"></param>
    /// <param name="value"></param>
    /// <param name="separator">
    /// Used when value is inserted before the original value. Default is a space.
    /// </param>
    public static void AddAttribute(
      TagHelperAttributeList attributes,
      string attribute,
      string value,
      string separator = " "
    )
    {
      string trimmedValue = value.Trim();
      if (string.IsNullOrEmpty(trimmedValue))
      {
        return;
      }
      TagHelperAttribute? existingAttribute = FindAttribute(attributes, attribute);
      if (existingAttribute != null)
      {
        trimmedValue = $"{trimmedValue}{separator}{existingAttribute.Value}";
      }
      attributes.SetAttribute(attribute, trimmedValue);
    }

    /// <summary>
    /// Shortcut to add css classes to an output. The method calls <see cref="AddAttribute"/> with
    /// the correct parameters.
    /// </summary>
    /// <param name="output">Output to get attributes from</param>
    /// <param name="classes">Additional css classes to add</param>
    public static void AddClasses(
      TagHelperOutput output,
      string classes
    )
    {
      AddAttribute(output.Attributes, "class", classes);
    }

    /// <summary>
    /// Checks if a value is a floating number with an optional unit like px, em, rem, %, etc.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsCssValue(
      string value
    )
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        return false;
      }
      Match match = UnitRegex.Match(value);
      if (!match.Success)
      {
        return false;
      }
      string unit = match.Groups[1].Value;
      return string.IsNullOrEmpty(unit) || Array.Exists(
        ValidUnits,
        validUnit => validUnit.Equals(unit, StringComparison.OrdinalIgnoreCase)
      );
    }

    /// <summary>
    /// Gets an item from the context and typecast it to a certain class type. 
    /// </summary>
    /// <param name="context">
    /// Context to get value from. 
    /// </param>
    /// <param name="key">
    /// Key to get value for. 
    /// </param>
    /// <param name="defaultValue">
    /// Default value to use if no value was found. If null, an exception is thrown.
    /// </param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if there is no value stored for the key and no default value was used.
    /// </exception>
    public static T GetItem<T>(
      TagHelperContext context,
      object key,
      T? defaultValue = null
    )
      where T : class
    {
      object? result = context.Items[key];
      if (result != null)
      {
        return (result as T)!;
      }
      if (defaultValue != null)
      {
        return defaultValue;
      }
      throw new InvalidOperationException($"Item with key {key} not found in context.");
    }

    #endregion
  }
}