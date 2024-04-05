// <copyright file="UFController.cs" company="Ultra Force Development">
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

using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UltraForce.Library.NetStandard.Net;

namespace UltraForce.Library.Core.Asp.Controllers
{
  /// <summary>
  /// This class extends <see cref="Controller"/> and adds some additional
  /// methods.
  /// </summary>
  public class UFController : Controller
  {
    #region protected methods

    /// <summary>
    /// Generate a bad request using 
    /// <see cref="UFExtendedErrorResponseModel{TEnum}" />.
    /// </summary>
    /// <typeparam name="TEnum">
    /// Enumeration type that defines extended error codes
    /// </typeparam>
    /// <param name="aCode">
    /// Extended error code
    /// </param>
    /// <returns>
    /// Bad request with json object with the following format:
    /// <code>
    /// {
    ///   code /* int */,
    ///   name /* string */
    /// }
    /// </code>
    /// </returns>
    protected IActionResult BadRequest<TEnum>(TEnum aCode)
      where TEnum : struct, IConvertible
    {
      return base.BadRequest(new UFExtendedErrorResponseModel<TEnum> {
        Code = aCode,
        Name = aCode.ToString(CultureInfo.InvariantCulture)
      });
    }

    /// <summary>
    /// Writes the json response first to the logger before sending it back
    /// to the server.
    /// </summary>
    /// <param name="aLogger">Logger to write to</param>
    /// <param name="aData">Data to create Json for</param>
    /// <returns><see cref="JsonResult"/> instance</returns>
    protected JsonResult Json(ILogger aLogger, object aData)
    {
      aLogger.LogInformation(
        $"{nameof(Json)} response = {{0}}",
        JsonSerializer.Serialize(aData)
      );
      return this.Json(aData);
    }

    #endregion
  }
}