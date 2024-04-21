// <copyright file="UFSessionKeyedStorage.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Http;
using UltraForce.Library.NetStandard.Storage;

namespace UltraForce.Library.Core.Asp.Sessions
{
  /// <summary>
  /// This class maps a <see cref="UFKeyedStorage" /> to a <see cref="ISession"/>.
  /// </summary>
  public class UFSessionKeyedStorage(ISession aSession) : UFKeyedStorage
  {
    #region Private variables

    /// <summary>
    /// Session mapped to.
    /// </summary>
    private readonly ISession m_session = aSession;

    #endregion

    #region UFKeyedStorage

    /// <inheritdoc />
    public override int GetInt(string aKey, int aDefault)
    {
      return this.m_session.GetInt32(aKey) ?? aDefault;
    }

    /// <inheritdoc />
    public override void SetInt(string aKey, int aValue)
    {
      this.m_session.SetInt32(aKey, aValue);
    }

    /// <inheritdoc />
    public override string GetString(string aKey, string aDefault)
    {
      return this.HasKey(aKey)
        ? this.m_session.GetString(aKey) ?? aDefault
        : aDefault;
    }

    /// <inheritdoc />
    public override void SetString(string aKey, string aValue)
    {
      this.m_session.SetString(aKey, aValue);
    }

    /// <inheritdoc />
    public override void DeleteKey(string aKey)
    {
      this.m_session.Remove(aKey);
    }

    /// <inheritdoc />
    public override void DeleteAll()
    {
      this.m_session.Clear();
    }

    /// <inheritdoc />
    public override bool HasKey(string aKey)
    {
      return this.m_session.Keys.Any(key => key == aKey);
    }

    #endregion
  }
}