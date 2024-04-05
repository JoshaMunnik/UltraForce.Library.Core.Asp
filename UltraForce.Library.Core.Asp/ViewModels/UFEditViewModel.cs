// <copyright file="UFEditViewModel.cs" company="Ultra Force Development">
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

namespace UltraForce.Library.Core.Asp.ViewModels
{
  /// <summary>
  /// A base class for edit forms.
  /// </summary>
  /// <typeparam name="T">
  /// Type being edited, should be a class and  support a constructor without parameters.
  /// </typeparam>
  public abstract class UFEditViewModel<T> where T : class, new()
  {
    #region constructors

    /// <summary>
    /// Constructs an instance with a new value.
    /// </summary>
    protected UFEditViewModel()
    {
      this.Value = new T();
      this.NewValue = true;
    }

    /// <summary>
    /// Constructs an instance with an existing value.
    /// </summary>
    /// <param name="aValue"></param>
    protected UFEditViewModel(T aValue)
    {
      this.Value = aValue;
      this.NewValue = false;
    }

    #endregion

    #region public properties

    /// <summary>
    /// Value being edited
    /// </summary>
    public T Value { get; }

    /// <summary>
    /// True when editing a new value or false when editing an existing value
    /// </summary>
    public bool NewValue { get; set; }

    #endregion

    #region public methods

    /// <summary>
    /// Patches an existing value with data stored in <see cref="Value"/>.
    /// <para>
    /// Subclasses must implement this method.
    /// </para>
    /// </summary>
    /// <param name="aValue">Value to patch</param>
    /// <returns>Value of <c>aValue</c>, so call can be chained if needed</returns>
    public abstract T Patch(T aValue);

    #endregion
  }
}