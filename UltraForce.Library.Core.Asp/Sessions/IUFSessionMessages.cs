// <copyright file="IUFSessionMessages.cs" company="Ultra Force Development">
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
  /// Interface with properties that map to session data.
  /// </summary>
  public interface IUFSessionMessages
  {
    /// <summary>
    /// Adds a message to show in page
    /// </summary>
    /// <param name="aMessage">Message to show</param>
    void Add(string aMessage);

    /// <summary>
    /// Adds an error message to show in page
    /// </summary>
    /// <param name="aMessage">Message to show</param>
    void AddError(string aMessage);

    /// <summary>
    /// Adds a warning message to show in page
    /// </summary>
    /// <param name="aMessage">Message to show</param>
    void AddWarning(string aMessage);

    /// <summary>
    /// Clears all stored messages
    /// </summary>
    void Clear();

    /// <summary>
    /// Gets all messages
    /// </summary>
    /// <returns>messages</returns>
    IEnumerable<string> GetMessages();

    /// <summary>
    /// Gets all error messages
    /// </summary>
    /// <returns>messages</returns>
    IEnumerable<string> GetErrorMessages();

    /// <summary>
    /// Gets all warning messages
    /// </summary>
    /// <returns>messages</returns>
    IEnumerable<string> GetWarningMessages();

    /// <summary>
    /// Checks if there are any messages.
    /// </summary>
    /// <returns>True if there is at least one message</returns>
    bool HasMessages();
  }
}