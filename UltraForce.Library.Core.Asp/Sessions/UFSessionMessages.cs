// <copyright file="UFSessionMessages.cs" company="Ultra Force Development">
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

namespace UltraForce.Library.Core.Asp.Sessions
{
  /// <summary>
  /// <see cref="UFSessionMessages" /> can be used to store messages in a session or retrieve them
  /// to be show them inside a view.
  /// <para>
  /// To use this class, register this class as a scoped service.
  /// </para>
  /// <para>
  /// If there is no session because either the class fails to get a <see cref="HttpContext"/> or
  /// the <see cref="ISession"/>; messages will be not be stored and the get messages methods will return empty lists. 
  /// </para>
  /// </summary>
  public sealed class UFSessionMessages(IHttpContextAccessor anAccessor)
  {
    #region private constants

    /// <summary>
    /// Value that is added to all keys.
    /// </summary>
    private const string Prefix = "UFSessionMessages_";

    /// <summary>
    /// Normal type of messages
    /// </summary>
    private const string Normal = "Normal";

    /// <summary>
    /// Error type of messages
    /// </summary>
    private const string Error = "Error";

    /// <summary>
    /// Warning type of messages
    /// </summary>
    private const string Warning = "Warning";

    #endregion
    
    #region private variables

    /// <summary>
    /// The session to use (if any)
    /// </summary>
    private readonly UFSessionKeyedStorage? m_session = anAccessor.HttpContext?.Session != null 
      ? new UFSessionKeyedStorage(anAccessor.HttpContext.Session) 
      : null;
    
    #endregion
    
    #region IUFAdminSession

    /// <summary>
    /// Adds a message to show in page
    /// </summary>
    /// <param name="aMessage">Message to show</param>
    public void Add(string aMessage)
    {
      this.AddMessage(Normal, aMessage);
    }

    /// <summary>
    /// Adds an error message to show in page
    /// </summary>
    /// <param name="aMessage">Message to show</param>
    public void AddError(string aMessage)
    {
      this.AddMessage(Error, aMessage);
    }

    /// <summary>
    /// Adds a warning message to show in page
    /// </summary>
    /// <param name="aMessage">Message to show</param>
    public void AddWarning(string aMessage)
    {
      this.AddMessage(Warning, aMessage);
    }

    /// <summary>
    /// Clears all stored messages
    /// </summary>
    public void Clear()
    {
      this.ClearMessages(Normal);
      this.ClearMessages(Error);
      this.ClearMessages(Warning);
    }

    /// <summary>
    /// Gets all messages
    /// </summary>
    /// <returns>messages</returns>
    public IEnumerable<string> GetMessages()
    {
      return this.GetMessages(Normal);
    }

    /// <summary>
    /// Gets all error messages
    /// </summary>
    /// <returns>messages</returns>
    public IEnumerable<string> GetErrorMessages()
    {
      return this.GetMessages(Error);
    }

    /// <summary>
    /// Gets all warning messages
    /// </summary>
    /// <returns>messages</returns>
    public IEnumerable<string> GetWarningMessages()
    {
      return this.GetMessages(Warning);
    }

    /// <summary>
    /// Checks if there are any messages.
    /// </summary>
    /// <returns>True if there is at least one message</returns>
    public bool HasMessages()
    {
      return this.HasMessages(Normal) || this.HasMessages(Warning) || this.HasMessages(Error);
    }

    #endregion

    #region private methods

    /// <summary>
    /// Adds a message to the session.
    /// </summary>
    /// <param name="aType">Type of message</param>
    /// <param name="aMessage">Message to add</param>
    private void AddMessage(string aType, string aMessage)
    {
      if (this.m_session == null)
      {
        return;
      }
      try
      {
        string indexKey = GetIndexKey(aType);
        int currentIndex = this.m_session.GetInt(indexKey, 0);
        for (int index = 0; index < currentIndex; index++)
        {
          if (aMessage == this.m_session.GetString(GetItemKey(aType, index)))
          {
            return;
          }
        }
        this.m_session.SetString(GetItemKey(aType, currentIndex), aMessage);
        this.m_session.SetInt(indexKey, currentIndex + 1);
      }
      catch
      {
        // ignored
      }
    }

    /// <summary>
    /// Removes all messages for a certain type.
    /// </summary>
    /// <param name="aType">Type to remove message for</param>
    private void ClearMessages(string aType)
    {
      if (this.m_session == null)
      {
        return;
      }
      try
      {
        string indexKey = GetIndexKey(aType);
        int count = this.m_session.GetInt(indexKey, 0);
        for (int index = 0; index < count; index++)
        {
          this.m_session.DeleteKey(GetItemKey(aType, index));
        }
        this.m_session.DeleteKey(indexKey);
      }
      catch
      {
        // ignored
      }
    }

    /// <summary>
    /// Checks if there is at least one messages stored for a type.
    /// </summary>
    /// <param name="aType"></param>
    /// <returns></returns>
    private bool HasMessages(string aType)
    {
      if (this.m_session == null)
      {
        return false;
      }
      try
      {
        return this.m_session.HasKey(GetIndexKey(aType));
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// Gets all messages for a certain type
    /// </summary>
    /// <param name="aType">type</param>
    /// <returns>messages</returns>
    private IEnumerable<string> GetMessages(string aType)
    {
      if (this.m_session == null)
      {
        return [];
      }
      try
      {
        string indexKey = GetIndexKey(aType);
        int count = this.m_session.GetInt(indexKey, 0);
        List<string> result = new(count);
        for (int index = 0; index < count; index++)
        {
          result.Add(this.m_session.GetString(GetItemKey(aType, index)));
        }
        return result;
      }
      catch
      {
        return [];
      }
    }

    /// <summary>
    /// Gets storage key for the index and certain type.
    /// </summary>
    /// <param name="aType">Type to get index key for</param>
    /// <returns>index key</returns>
    private static string GetIndexKey(string aType)
    {
      return Prefix + aType + "Index";
    }

    /// <summary>
    /// Gets storage key for a message at a certain index and type.
    /// </summary>
    /// <param name="aType">Type to get key for</param>
    /// <param name="anIndex">Index to get key for</param>
    /// <returns>storage key</returns>
    private static string GetItemKey(string aType, int anIndex)
    {
      return Prefix + aType + anIndex;
    }

    #endregion
  }
}