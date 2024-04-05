// <copyright file="UFDutchIdentityErrorDescriber.cs" company="Ultra Force Development">
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

using Microsoft.AspNetCore.Identity;

namespace UltraForce.Library.Core.Asp.Services
{
  /// <summary>
  /// This class uses error messages in Dutch.
  /// </summary>
  public class UFDutchIdentityErrorDescriber : IdentityErrorDescriber
  {
    #region Public overriden methods

    /// <inheritdoc />
    public override IdentityError PasswordTooShort(int aLength)
    {
      return this.Replace(
        base.PasswordTooShort(aLength),
        $"Het wachtwoord moet tenminste {aLength} karakters bevatten."
      );
    }

    /// <inheritdoc />
    public override IdentityError PasswordMismatch()
    {
      return this.Replace(
        base.PasswordMismatch(),
        "De wachtwoorden zijn niet gelijk."
      );
    }

    /// <inheritdoc />
    public override IdentityError PasswordRequiresDigit()
    {
      return this.Replace(
        base.PasswordRequiresDigit(),
        "Het wachtwoord moet tenminste 1 getal bevatten."
      );
    }

    /// <inheritdoc />
    public override IdentityError PasswordRequiresLower()
    {
      return this.Replace(
        base.PasswordRequiresLower(),
        "Het wachtwoord moet tenminste 1 kleine letter bevatten."
      );
    }

    /// <inheritdoc />
    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
      return this.Replace(
        base.PasswordRequiresNonAlphanumeric(),
        "Het wachtwoord moet tenminste 1 karakter bevatten dat geen letter of getal is."
      );
    }

    /// <inheritdoc />
    public override IdentityError PasswordRequiresUniqueChars(int anUniqueChars)
    {
      return this.Replace(
        base.PasswordRequiresUniqueChars(anUniqueChars),
        $"Het wachtwoord moet tenminste {anUniqueChars} unieke karakters bevatten."
      );
    }

    /// <inheritdoc />
    public override IdentityError PasswordRequiresUpper()
    {
      return this.Replace(
        base.PasswordRequiresUpper(),
        "Het wachtwoord moet tenminste 1 hoofdletter bevatten."
      );
    }

    /// <inheritdoc />
    public override IdentityError DuplicateEmail(string email)
    {
      return this.Replace(
        base.DuplicateEmail(email),
        $"Het email adres `{email}` is al in gebruik."
      );
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Replaces the description part of a <see cref="IdentityError"/> and
    /// returns the instance.
    /// </summary>
    /// <param name="anError">Instance to update</param>
    /// <param name="aDescription">New description</param>
    /// <returns>value of anError</returns>
    private IdentityError Replace(IdentityError anError, string aDescription)
    {
      anError.Description = aDescription;
      return anError;
    }

    #endregion
  }
}