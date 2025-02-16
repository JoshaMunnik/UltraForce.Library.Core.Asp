// <copyright file="IUFViewDataService.cs" company="Ultra Force Development">
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

using UltraForce.Library.Core.Asp.TagHelpers;

namespace UltraForce.Library.Core.Asp.Services;

/// <summary>
/// Defines an interface for a service that provides data for a view which is shared between
/// all (partial) views and controllers using typed properties.
/// <para>
/// Register this service in the DI container as a singleton.
/// </para>
/// </summary>
public interface IUFViewDataService
{
  #region properties
  
  /// <summary>
  /// This property is used to store the title of the page. The <see cref="UFDataTitleTagHelper"/>
  /// uses this property to set or read the title.
  /// </summary>
  string Title { get; set; }
  
  #endregion
  
  #region methods

  /// <summary>
  /// Clears all data.
  /// </summary>
  void Clear();
  
  #endregion
}
