// <copyright file="UFAuthorizeRolesAttribute.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Authorization;

namespace UltraForce.Library.Core.Asp.Annotations
{
  /// <summary>
  /// <see cref="UFAuthorizeRolesAttribute"/> can be used to specify a list of roles in a better
  /// manner.
  /// <para>
  /// Code based on: https://stackoverflow.com/a/24182340/968451
  /// </para>
  /// </summary>
  public class UFAuthorizeRolesAttribute : AuthorizeAttribute
  {
    /// <summary>
    /// Combines the roles into a single string using ',' as a separator.
    /// </summary>
    /// <param name="roles">Roles to combine</param>
    public UFAuthorizeRolesAttribute(params string[] roles)
    {
      this.Roles = string.Join(",", roles);
    }
  }
}