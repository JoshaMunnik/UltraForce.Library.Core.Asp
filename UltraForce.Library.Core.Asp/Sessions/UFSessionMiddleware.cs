// <copyright file="UFSessionMiddleware.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Http;
using UltraForce.Library.Core.Asp.Web;
using UltraForce.Library.NetStandard.Storage;

namespace UltraForce.Library.Core.Asp.Sessions
{
  /// <summary>
  /// <see cref="UFSessionMiddleware" /> is <see cref="UFKeyedStorage"/> implementation mapping
  /// to <see cref="HttpContext.Session"/>.
  /// <para>
  /// Use <see cref="Instance"/> to access the singleton instance.
  /// </para>
  /// <para>
  /// To use <see cref="UFSessionMiddleware" />, import <see cref="UFWebExtensions" /> in
  /// <c>Startup.cs</c> or <c>Program.cs</c> and register it at
  /// <see cref="Microsoft.AspNetCore.Builder.IApplicationBuilder"/> as
  /// middleware: `builder.UseUFSession();` 
  /// </para>
  /// <para>
  /// <c>UseUFSession()</c> will make a call to <c>UseSession()</c>.
  /// </para>
  /// </summary>
  public class UFSessionMiddleware : UFKeyedStorage
  {
    #region Private variables

    /// <summary>
    /// Current active http context
    /// </summary>
    private HttpContext? m_context;

    /// <summary>
    /// Next middleware
    /// </summary>
    private readonly RequestDelegate m_next;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructs an instance of the UFSession middleware.
    /// </summary>
    /// <param name="aNext">Next middleware</param>
    public UFSessionMiddleware(RequestDelegate aNext)
    {
      Instance = this;
      this.m_next = aNext;
    }

    #endregion

    #region Public methods

    /// <summary>
    /// This method is called by the http rendering pipeline.
    /// </summary>
    /// <param name="aContext">Current <see cref="HttpContext"/></param>
    /// <returns>result from next middleware</returns>
    public Task Invoke(HttpContext aContext)
    {
      this.m_context = aContext;
      return this.m_next(aContext);
    }

    #endregion

    #region Public overrides

    /// <inheritdoc />
    public override int GetInt(string aKey, int aDefault)
    {
      return this.m_context?.Session.GetInt32(aKey) ?? aDefault;
    }

    /// <inheritdoc />
    public override void SetInt(string aKey, int aValue)
    {
      this.m_context?.Session.SetInt32(aKey, aValue);
    }

    #endregion

    #region UFKeyedStorage

    /// <inheritdoc />
    public override string GetString(string aKey, string aDefault)
    {
      return this.HasKey(aKey)
        ? this.m_context?.Session.GetString(aKey) ?? aDefault
        : aDefault;
    }

    /// <inheritdoc />
    public override void SetString(string aKey, string aValue)
    {
      this.m_context?.Session.SetString(aKey, aValue);
    }

    /// <inheritdoc />
    public override void DeleteKey(string aKey)
    {
      this.m_context?.Session.Remove(aKey);
    }

    /// <inheritdoc />
    public override void DeleteAll()
    {
      this.m_context?.Session.Clear();
    }

    /// <inheritdoc />
    public override bool HasKey(string aKey)
    {
      return this.m_context?.Session.Keys.Any(key => key == aKey) ?? false;
    }

    #endregion

    #region Public properties

    /// <summary>
    /// Reference to singleton instance.
    /// </summary>
    public static UFSessionMiddleware Instance { get; private set; } = null!;

    #endregion
  }
}