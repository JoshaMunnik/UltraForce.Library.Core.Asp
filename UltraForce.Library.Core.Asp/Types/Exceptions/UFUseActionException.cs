// <copyright file="UFUseActionException.cs" company="Ultra Force Development">
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
using Microsoft.AspNetCore.Mvc;
using UltraForce.Library.Core.Asp.Filters;

namespace UltraForce.Library.Core.Asp.Types.Exceptions;

/// <summary>
/// This exception can be used within controllers to stop the executing of the current action and
/// perform some action result.
/// <para>
/// For this exception to function correctly, <see cref="UFUseActionExceptionFilter"/> has
/// to be installed as listener for controller exceptions. 
/// </para>
/// <para>
/// For example a redirect to some default page when some session data is no longer valid.
/// </para>
/// </summary>
/// <param name="result"></param>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class UFUseActionException(IActionResult result) : Exception
{
  #region public properties

  /// <summary>
  /// Action result to perform. 
  /// </summary>
  public IActionResult Result { get; } = result;

  #endregion
}
