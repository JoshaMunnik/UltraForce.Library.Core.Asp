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

using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UltraForce.Library.Core.Asp.Tools
{
  /// <summary>
  /// Support methods for <see cref="TagHelper"/>
  /// </summary>
  public static class UFTagHelperTools
  {
    /// <summary>
    /// Sets the content to the inner html of a tag. The content is only set if there is no current
    /// content and no other tag helper as adjusted the content already. 
    /// </summary>
    /// <param name="anOutput">Output to update content of</param>
    /// <param name="aFactory">Factory that generates a tag to get inner content of</param>
    public static async Task SetContentToTagBuilderAsync(
      TagHelperOutput anOutput,
      Func<TagBuilder> aFactory
    )
    {
      // do not update the content if another tag helper targeting this element has already done so.
      if (!anOutput.IsContentModified)
      {
        // only replace if there is no child content
        TagHelperContent childContent = await anOutput.GetChildContentAsync();
        if (childContent.IsEmptyOrWhiteSpace)
        {
          // build tag and copy it's inner html
          TagBuilder tagBuilder = aFactory();
          if (tagBuilder is { HasInnerHtml: true })
          {
            anOutput.Content.SetHtmlContent(tagBuilder.InnerHtml);
          }
        }
        else
        {
          anOutput.Content.SetHtmlContent(childContent);
        }
      }
    }

    /// <summary>
    /// Sets the content to a specific text. The text is only set if there is not another tag helper
    /// and the content is empty.
    /// </summary>
    /// <param name="anOutput">Output to update</param>
    /// <param name="aText">Text to set</param>
    public static async Task SetContentToText(TagHelperOutput anOutput, string aText)
    {
      // do not update the content if another tag helper targeting this element has already done so.
      if (!anOutput.IsContentModified)
      {
        // only replace if there is no child content
        TagHelperContent childContent = await anOutput.GetChildContentAsync();
        if (childContent.IsEmptyOrWhiteSpace)
        {
          anOutput.Content.SetContent(aText);
        }
        else
        {
          anOutput.Content.SetHtmlContent(childContent);
        }
      }
    }

    /// <summary>
    /// Sets the content to a specific html. The html is only set if there is not another tag helper
    /// and the content is empty.
    /// </summary>
    /// <param name="anOutput">Output to update</param>
    /// <param name="anHtml">Html to set</param>
    public static async Task SetContentToHtmlAsync(TagHelperOutput anOutput, string anHtml)
    {
      // do not update the content if another tag helper targeting this element has already done so.
      if (!anOutput.IsContentModified)
      {
        // only replace if there is no child content
        TagHelperContent childContent = await anOutput.GetChildContentAsync();
        if (childContent.IsEmptyOrWhiteSpace)
        {
          anOutput.Content.SetHtmlContent(anHtml);
        }
        else
        {
          anOutput.Content.SetHtmlContent(childContent);
        }
      }
    }

    /// <summary>
    /// Gets the label from a model expression, by building label with <see cref="IHtmlGenerator"/>
    /// and getting its inner html contents.
    /// </summary>
    /// <param name="aGenerator">Generator to use</param>
    /// <param name="aViewContext">View context to render in</param>
    /// <param name="anExpression">Expression to get label from</param>
    /// <param name="aLabelText">When non null, use this value instead of getting the label</param>
    /// <returns>the inner html or aLabelText if it was not empty</returns>
    public static string GetLabel(
      IHtmlGenerator aGenerator,
      ViewContext aViewContext,
      ModelExpression? anExpression,
      string aLabelText
    )
    {
      string label = aLabelText;
      if (!string.IsNullOrEmpty(label) || (anExpression == null))
      {
        return label;
      }
      TagBuilder tagBuilder = aGenerator.GenerateLabel(
        aViewContext,
        anExpression.ModelExplorer,
        anExpression.Name,
        labelText: null,
        htmlAttributes: null
      );
      label = UFCoreStringTools.GetString(tagBuilder.InnerHtml);
      return label;
    }

    /// <summary>
    /// Builds a tag helper attribute list from a dictionary.
    /// </summary>
    /// <param name="anAttributes"></param>
    /// <returns></returns>
    public static TagHelperAttributeList CreateAttributeList(
      Dictionary<string, string> anAttributes
    )
    {
      TagHelperAttributeList attributeList = new();
      foreach (KeyValuePair<string, string> attribute in anAttributes)
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
    /// <param name="aContent">Child content for the tag helper</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string RenderTagHelper<T>(
      string aContent
    ) where T : TagHelper, new()
    {
      DefaultTagHelperContent content = new();
      return RenderTagHelper<T>(new TagHelperAttributeList(), content.SetHtmlContent(aContent));
    }

    /// <summary>
    /// Renders a tag helper that does not use any attributes to a string.
    /// </summary>
    /// <param name="anAttributeList"></param>
    /// <param name="aContent">Optional child content for the tag helper</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string RenderTagHelper<T>(
      Dictionary<string, string> anAttributeList,
      string aContent
    ) where T : TagHelper, new()
    {
      DefaultTagHelperContent content = new();
      return RenderTagHelper<T>(anAttributeList, content.SetHtmlContent(aContent));
    }

    /// <summary>
    /// Renders a tag helper that does not use any attributes to a string.
    /// </summary>
    /// <param name="aContent">Optional child content for the tag helper</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string RenderTagHelper<T>(
      TagHelperContent? aContent = null
    ) where T : TagHelper, new()
    {
      return RenderTagHelper<T>(new TagHelperAttributeList(), aContent);
    }

    /// <summary>
    /// Renders a tag helper to a string.
    /// </summary>
    /// <param name="anAttributes"></param>
    /// <param name="aContent">Optional child content for the tag helper</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string RenderTagHelper<T>(
      Dictionary<string, string> anAttributes,
      TagHelperContent? aContent = null
    ) where T : TagHelper, new()
    {
      return RenderTagHelper<T>(CreateAttributeList(anAttributes), aContent);
    }

    /// <summary>
    /// Renders a tag helper to a string.
    /// 
    /// See:
    /// https://github.com/MissaouiChedy/RenderingTagHelperInsideAnother/blob/master/src/RenderingTagHelperInsideAnother/TagHelpers/WrapperTagHelper.cs
    /// </summary>
    /// <param name="anAttributeList"></param>
    /// <param name="aContent">Optional child content for the tag helper</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string RenderTagHelper<T>(
      TagHelperAttributeList anAttributeList,
      TagHelperContent? aContent = null
    ) where T : TagHelper, new()
    {
      T tagHelper = new();
      TagHelperOutput output = new(
        tagName: "div",
        attributes: anAttributeList,
        getChildContentAsync: (useCachedResult, encoder) =>
          Task.Run<TagHelperContent>(() => aContent ?? new DefaultTagHelperContent())
      )
      {
        TagMode = TagMode.StartTagAndEndTag
      };
      output.Content.SetHtmlContent(aContent);
      TagHelperContext context = new(
        allAttributes: anAttributeList,
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
    /// <param name="anOutput"></param>
    /// <param name="anEncoder"></param>
    /// <returns></returns>
    public static string RenderTagHelperOutput(
      TagHelperOutput anOutput, HtmlEncoder? anEncoder = null
    )
    {
      using StringWriter writer = new();
      anOutput.WriteTo(writer, anEncoder ?? HtmlEncoder.Default);
      return writer.ToString();
    }

    /// <summary>
    /// Tries to find an attribute with a certain name.
    /// </summary>
    /// <param name="aList"></param>
    /// <param name="aName"></param>
    /// <returns></returns>
    public static TagHelperAttribute? FindAttribute(TagHelperAttributeList aList, string aName)
    {
      return aList.FirstOrDefault(tagHelperAttribute => tagHelperAttribute.Name == aName);
    }

    /// <summary>
    /// Adds a value for an attribute. If the attribute exists, the value gets added to its value.
    /// </summary>
    /// <param name="anAttributes"></param>
    /// <param name="anAttribute"></param>
    /// <param name="aValue"></param>
    /// <param name="aSeparator"></param>
    public static void AddAttribute(
      TagHelperAttributeList anAttributes, string anAttribute, string aValue, 
      string aSeparator = " "
    )
    {
      string value = aValue.Trim();
      if (string.IsNullOrEmpty(value))
      {
        return;
      }
      TagHelperAttribute? attribute = FindAttribute(anAttributes, anAttribute);
      if (attribute != null)
      {
        value = $"{attribute.Value}{aSeparator}{value}";
      }
      anAttributes.SetAttribute(anAttribute, value);
    }

    /// <summary>
    /// Shortcut to add css classes to an output. The method calls <see cref="AddAttribute"/> with
    /// the correct parameters.
    /// </summary>
    /// <param name="anOutput">Output to get attributes from</param>
    /// <param name="aClasses">Additional css classes to add</param>
    public static void AddClasses(TagHelperOutput anOutput, string aClasses)
    {
      AddAttribute(anOutput.Attributes, "class", aClasses);
    }
  }
}