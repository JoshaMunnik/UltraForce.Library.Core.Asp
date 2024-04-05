// <copyright file="UFDutchDefaultModelBindingMessageProvider.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace UltraForce.Library.Core.Asp.Localization
{
  /// <summary>
  /// This class replaced error message in 
  /// <see cref="DefaultModelBindingMessageProvider"/>
  /// with Dutch variants.
  /// </summary>
  public static class UFDutchDefaultModelBindingMessageProvider
  {
    /// <summary>
    /// Replaces the error messages with Dutch versions.
    /// </summary>
    /// <param name="aProvider">Provider to update</param>
    public static void Apply(DefaultModelBindingMessageProvider aProvider)
    {
      aProvider.SetValueIsInvalidAccessor(
        s => $"Ongeldige waarde voor {s}."
      );
      aProvider.SetValueMustBeANumberAccessor(
        s => $"De waarde voor {s} moet een getal zijn."
      );
      aProvider.SetValueMustNotBeNullAccessor(
        s => $"De waarde voor {s} mag niet leeg zijn."
      );
      aProvider.SetMissingBindRequiredValueAccessor(
        s => $"{s} is nodig en mag niet leeg zijn."
      );
      aProvider.SetAttemptedValueIsInvalidAccessor(
        (s, v) => $"{s} bevat een ongeldige waarde `{v}`."
      );
      aProvider.SetUnknownValueIsInvalidAccessor(
        s => $"{s} bevat een onbekende ongeldige waarde."
      );
      string test = aProvider.ValueMustNotBeNullAccessor("Test");
    }
  }
}