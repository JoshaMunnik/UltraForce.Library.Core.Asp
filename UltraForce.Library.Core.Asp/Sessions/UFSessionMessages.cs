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

namespace UltraForce.Library.Core.Asp.Sessions
{
  /// <summary>
  /// <see cref="UFSessionMessages" /> implements <see cref="IUFSessionMessages"/> using
  /// <see cref="UFSessionMiddleware"/> to store the messages.
  /// </summary>
  public sealed class UFSessionMessages : IUFSessionMessages
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

    #region IUFAdminSession

    /// <inheritdoc />
    public void Add(string aMessage)
    {
      AddMessage(Normal, aMessage);
    }

    /// <inheritdoc />
    public void AddError(string aMessage)
    {
      AddMessage(Error, aMessage);
    }

    /// <inheritdoc />
    public void AddWarning(string aMessage)
    {
      AddMessage(Warning, aMessage);
    }

    /// <inheritdoc />
    public void Clear()
    {
      ClearMessages(Normal);
      ClearMessages(Error);
      ClearMessages(Warning);
    }

    /// <inheritdoc />
    public IEnumerable<string> GetMessages()
    {
      return GetMessages(Normal);
    }

    /// <inheritdoc />
    public IEnumerable<string> GetErrorMessages()
    {
      return GetMessages(Error);
    }

    /// <inheritdoc />
    public IEnumerable<string> GetWarningMessages()
    {
      return GetMessages(Warning);
    }

    /// <inheritdoc />
    public bool HasMessages()
    {
      return HasMessages(Normal) || HasMessages(Warning) || HasMessages(Error);
    }
    
    #endregion

    #region private methods

    /// <summary>
    /// Adds a message to the session.
    /// </summary>
    /// <param name="aType">Type of message</param>
    /// <param name="aMessage">Message to add</param>
    private static void AddMessage(string aType, string aMessage)
    {
      string indexKey = GetIndexKey(aType);
      int currentIndex = UFSessionMiddleware.Instance.GetInt(indexKey, 0);
      for (int index = 0; index < currentIndex; index++)
      {
        if (aMessage == UFSessionMiddleware.Instance.GetString(GetItemKey(aType, index)))
        {
          return;
        }
      }
      UFSessionMiddleware.Instance.SetString(GetItemKey(aType, currentIndex), aMessage);
      UFSessionMiddleware.Instance.SetInt(indexKey, currentIndex + 1);
    }

    /// <summary>
    /// Removes all messages for a certain type.
    /// </summary>
    /// <param name="aType">Type to remove message for</param>
    private static void ClearMessages(string aType)
    {
      string indexKey = GetIndexKey(aType);
      int count = UFSessionMiddleware.Instance.GetInt(indexKey, 0);
      for (int index = 0; index < count; index++)
      {
        UFSessionMiddleware.Instance.DeleteKey(GetItemKey(aType, index));
      }
      UFSessionMiddleware.Instance.DeleteKey(indexKey);
    }

    /// <summary>
    /// Checks if there is at least one messages stored for a type.
    /// </summary>
    /// <param name="aType"></param>
    /// <returns></returns>
    private static bool HasMessages(string aType)
    {
      return UFSessionMiddleware.Instance.HasKey(GetIndexKey(aType));
    }

    /// <summary>
    /// Gets all messages for a certain type
    /// </summary>
    /// <param name="aType">type</param>
    /// <returns>messages</returns>
    private static IEnumerable<string> GetMessages(string aType)
    {
      string indexKey = GetIndexKey(aType);
      int count = UFSessionMiddleware.Instance.GetInt(indexKey, 0);
      List<string> result = new(count);
      for (int index = 0; index < count; index++)
      {
        result.Add(UFSessionMiddleware.Instance.GetString(GetItemKey(aType, index)));
      }
      return result;
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