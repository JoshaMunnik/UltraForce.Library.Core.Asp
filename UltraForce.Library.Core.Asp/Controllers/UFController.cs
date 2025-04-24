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
using UltraForce.Library.Core.Asp.Sessions;
using UltraForce.Library.Core.Asp.Tools;
using UltraForce.Library.NetStandard.Net;

namespace UltraForce.Library.Core.Asp.Controllers
{
  /// <summary>
  /// This class extends <see cref="Controller"/> and adds some additional methods and properties.
  /// </summary>
  public class UFController : Controller
  {
    #region private variables

    private UFSessionKeyedStorage? m_sessionStorage = null;

    #endregion

    #region protected properties

    /// <summary>
    /// This property can be used to store data in the session using various types. The first
    /// time the property is accessed the instance is created. 
    /// </summary>
    protected UFSessionKeyedStorage SessionStorage =>
      this.m_sessionStorage ??= new UFSessionKeyedStorage(this.HttpContext.Session);

    #endregion

    #region protected methods

    /// <summary>
    /// Generate a bad request using 
    /// <see cref="UFExtendedErrorResponseModel{TEnum}" />.
    /// </summary>
    /// <typeparam name="TEnum">
    /// Enumeration type that defines extended error codes
    /// </typeparam>
    /// <param name="code">
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
    protected IActionResult BadRequest<TEnum>(
      TEnum code
    )
      where TEnum : struct, IConvertible
    {
      return base.BadRequest(
        new UFExtendedErrorResponseModel<TEnum>
        {
          Code = code,
          Name = code.ToString(CultureInfo.InvariantCulture)
        }
      );
    }

    /// <summary>
    /// Writes the json response first to the logger before sending it back
    /// to the server.
    /// </summary>
    /// <param name="logger">Logger to write to</param>
    /// <param name="data">Data to create Json for</param>
    /// <returns><see cref="JsonResult"/> instance</returns>
    protected JsonResult Json(
      ILogger logger,
      object data
    )
    {
      logger.LogInformation(
        $"{nameof(Json)} response = {{0}}",
        JsonSerializer.Serialize(data)
      );
      return this.Json(data);
    }

    /// <summary>
    /// A helper method that redirects to the index action of the current controller.
    /// </summary>
    /// <returns>A <see cref="RedirectResult" /></returns>
    protected IActionResult RedirectToIndex()
    {
      // ReSharper disable once Mvc.ActionNotResolved
      return this.RedirectToAction("Index");
    }

    /// <summary>
    /// A helper method that redirects to the index action of some controller.
    /// </summary>
    /// <param name="controller">
    /// Name of controller
    /// </param>
    /// <returns>A <see cref="RedirectResult" /></returns>
    protected IActionResult RedirectToIndex(
      string controller
    )
    {
      // ReSharper disable once Mvc.ActionNotResolved
      return this.RedirectToAction("Index", controller);
    }

    /// <summary>
    /// A helper method that redirects to the index action of some controller.
    /// </summary>
    /// <typeparam name="TController">
    /// Some controller type. The method uses <see cref="UFMvcTools.GetControllerName{T}"/> to
    /// get the controller name.
    /// </typeparam>
    /// <returns>A <see cref="RedirectResult" /></returns>
    protected IActionResult RedirectToIndex<TController>()
      where TController : Controller
    {
      // ReSharper disable once Mvc.ActionNotResolved
      return this.RedirectToAction("Index", UFMvcTools.GetControllerName<TController>());
    }

    /// <summary>
    /// A helper method that redirects to the index action of the current controller passing a
    /// single id parameter.
    /// </summary>
    /// <param name="id">
    /// The id to pass on to the index action.
    /// </param>
    /// <typeparam name="TId">
    /// Type of id.
    /// </typeparam>
    /// <returns>A <see cref="RedirectResult" /></returns>
    protected IActionResult RedirectToIndexWithId<TId>(
      TId id
    )
    {
      // ReSharper disable once Mvc.ActionNotResolved
      return this.RedirectToAction("Index", new { id });
    }

    /// <summary>
    /// A helper method that redirects to the index action of some controller passing a
    /// single id parameter.
    /// </summary>
    /// <typeparam name="TId">
    /// Type of id.
    /// </typeparam>
    /// <param name="controller">
    /// Name of controller
    /// </param>
    /// <param name="id">
    /// The id to pass on to the index action.
    /// </param>
    /// <returns>A <see cref="RedirectResult" /></returns>
    protected IActionResult RedirectToIndexWithId<TId>(
      string controller,
      TId id
    )
    {
      // ReSharper disable once Mvc.ActionNotResolved
      return this.RedirectToAction("Index", controller, new { id });
    }

    /// <summary>
    /// A helper method that redirects to the index action of some controller passing a
    /// single id parameter.
    /// </summary>
    /// <typeparam name="TId">
    /// Type of id.
    /// </typeparam>
    /// <typeparam name="TController">
    /// Some controller type. The method uses <see cref="UFMvcTools.GetControllerName{T}"/> to
    /// get the controller name.
    /// </typeparam>
    /// <param name="id">
    /// The id to pass on to the index action.
    /// </param>
    /// <returns>A <see cref="RedirectResult" /></returns>
    protected IActionResult RedirectToIndexWithId<TController, TId>(
      TId id
    )
      where TController : Controller
    {
      return this.RedirectToIndexWithId(UFMvcTools.GetControllerName<TController>(), new { id });
    }

    /// <summary>
    /// Redirect to an action of some controller.
    /// </summary>
    /// <param name="action">
    /// Action to redirect to.
    /// </param>
    /// <typeparam name="TController">
    /// Some controller type. The method uses <see cref="UFMvcTools.GetControllerName{T}"/> to
    /// get the controller name.
    /// </typeparam>
    /// <returns></returns>
    protected IActionResult RedirectToAction<TController>(
      string action
    )
      where TController : Controller
    {
      return this.RedirectToAction(action, UFMvcTools.GetControllerName<TController>());
    }

    /// <summary>
    /// Redirect to an action of some controller.
    /// </summary>
    /// <param name="action">
    /// Action to redirect to.
    /// </param>
    /// <param name="routeValues">
    /// Values to pass to the action.
    /// </param>
    /// <typeparam name="TController">
    /// Some controller type. The method uses <see cref="UFMvcTools.GetControllerName{T}"/> to
    /// get the controller name.
    /// </typeparam>
    /// <returns></returns>
    protected IActionResult RedirectToAction<TController>(
      string action,
      object routeValues
    )
      where TController : Controller
    {
      return this.RedirectToAction(
        action, UFMvcTools.GetControllerName<TController>(), routeValues
      );
    }

    /// <summary>
    /// Redirect to an action of some controller passing a single id parameter.
    /// </summary>
    /// <param name="action">
    /// Action to redirect to.
    /// </param>
    /// <param name="id">
    /// The id to pass on to the action.
    /// </param>
    /// <typeparam name="TId">
    /// Type of id.
    /// </typeparam>
    /// <typeparam name="TController">
    /// Some controller type. The method uses <see cref="UFMvcTools.GetControllerName{T}"/> to
    /// get the controller name.
    /// </typeparam>
    /// <returns></returns>
    protected IActionResult RedirectToActionWithId<TController, TId>(
      string action,
      TId id
    )
      where TController : Controller
    {
      return this.RedirectToAction(action, UFMvcTools.GetControllerName<TController>(), new { id });
    }

    /// <summary>
    /// Redirect to an action of some controller passing a single id parameter.
    /// </summary>
    /// <param name="action">
    /// Action to redirect to.
    /// </param>
    /// <param name="controller">
    /// Controller to redirect to.
    /// </param>
    /// <param name="id">
    /// The id to pass on to the action.
    /// </param>
    /// <typeparam name="TId">
    /// Type of id.
    /// </typeparam>
    /// <returns></returns>
    protected IActionResult RedirectToActionWithId<TId>(
      string action,
      string controller,
      TId id
    )
    {
      return this.RedirectToAction(action, controller, new { id });
    }

    #endregion
  }
}