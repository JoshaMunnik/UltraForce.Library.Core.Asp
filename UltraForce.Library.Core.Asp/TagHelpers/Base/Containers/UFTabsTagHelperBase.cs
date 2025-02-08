// <copyright file="UFTabsTagHelperBase.cs" company="Ultra Force Development">
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
using Microsoft.AspNetCore.Razor.TagHelpers;
using UltraForce.Library.NetStandard.Tools;

namespace UltraForce.Library.Core.Asp.TagHelpers.Base.Containers;

/// <summary>
/// This tag helper is used to render a tab container. It expects its children to be instances
/// of <see cref="UFTabTagHelperBase"/>.
/// <para>
/// The class will render:
/// <code>
/// &lt;div class="{GetTabsClasses}"&gt;
///   {children}
/// &lt;/div&gt;
/// </code>
/// </para>
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public abstract class UFTabsTagHelperBase : TagHelper
{
  #region public constants
  
  /// <summary>
  /// The key that <see cref="UFTabTagHelperBase"/> can use to get the name of the radio button from the
  /// <see cref="TagHelperContext.Items"/>.
  /// </summary>
  public const string TabsRadioName = "uf_tabs_radio_name";
  
  #endregion
  
  #region public methods

  /// <inheritdoc />
  public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
  {
    await base.ProcessAsync(context, output);
    output.TagName = "div";
    output.TagMode = TagMode.StartTagAndEndTag;
    // use a guid to get a unique name the children can use as name for the radio button
    string id = Guid.NewGuid().ToString();
    context.Items[TabsRadioName] = id;
    // determine the number of tabs by counting the number of the times the name value is used.
    TagHelperContent? childContent = 
      output.IsContentModified ? output.Content : await output.GetChildContentAsync();
    string textContent = childContent.GetContent();
    int tabCount = UFStringTools.Count(id, textContent);
    output.Attributes.SetAttribute("class", this.GetTabsClasses(tabCount));
  }
  
  #endregion
  
  #region protected overridable methods

  /// <summary>
  /// The default implementation returns an empty string.
  /// </summary>
  /// <param name="aCount"></param>
  /// <returns></returns>
  protected virtual string GetTabsClasses(int aCount)
  {
    return string.Empty;
  }
  
  #endregion

}