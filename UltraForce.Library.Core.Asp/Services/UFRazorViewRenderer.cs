// <copyright file="UFRazorViewRenderer.cs" company="Ultra Force Development">
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using UltraForce.Library.Core.Asp.Models;

namespace UltraForce.Library.Core.Asp.Services
{
  /// <summary>
  /// A service to help renders a razor view to a string. Register this class as a Transient service
  /// with the service collection.
  /// </summary>
  /// <remarks>
  /// Based on code from:
  /// https://github.com/aspnet/Entropy/blob/master/samples/Mvc.RenderViewToString/RazorViewToStringRenderer.cs
  /// </remarks>
  public class UFRazorViewRenderer
  {
    #region private variables

    /// <summary>
    /// View engine to use
    /// </summary>
    private readonly IRazorViewEngine m_viewEngine;

    /// <summary>
    /// TempData provider to use
    /// </summary>
    private readonly ITempDataProvider m_tempDataProvider;

    /// <summary>
    /// Service provider to use
    /// </summary>
    private readonly IServiceProvider m_serviceProvider;

    #endregion

    #region constructors

    /// <summary>
    /// Constructs an instance of <see cref="UFRazorViewRenderer"/>
    /// </summary>
    /// <param name="viewEngine"></param>
    /// <param name="tempDataProvider"></param>
    /// <param name="serviceProvider"></param>
    public UFRazorViewRenderer(
      IRazorViewEngine viewEngine,
      ITempDataProvider tempDataProvider,
      IServiceProvider serviceProvider
    )
    {
      this.m_viewEngine = viewEngine;
      this.m_tempDataProvider = tempDataProvider;
      this.m_serviceProvider = serviceProvider;
    }

    #endregion

    #region public methods

    /// <summary>
    /// Renders a Razor view and its model data to a string. 
    /// <para>
    /// If the view can not be found the method with raise an exception.
    /// </para>
    /// </summary>
    /// <typeparam name="TModel">Type of model data</typeparam>
    /// <param name="viewName">Name of view</param>
    /// <param name="model">Model data</param>
    /// <returns>Rendered view</returns>
    public async Task<string?> RenderViewAsync<TModel>(
      string viewName,
      TModel model
    )
    {
      return await this.RenderViewAsync(viewName, model, true);
    }

    /// <summary>
    /// Tries to render both an html and plain text version of an email by appending
    /// 'Html'/'.Html' and 'Text'/'.Text' to the base view name.
    /// <c>baseViewName</c>.
    /// </summary>
    /// <remarks>
    /// If a view can not be found, the value for that property will be set to true.
    /// <para>
    /// If the HTML view can not be found, the method tries <c>baseViewName</c> itself as view name.
    /// </para>
    /// </remarks>
    /// <typeparam name="TModel">Type of model data</typeparam>
    /// <param name="baseViewName">Base view name</param>
    /// <param name="model">Model to render</param>
    /// <returns>A <see cref="UFEmailContentModel"/> instance</returns>
    public async Task<UFEmailContentModel> RenderEmailViewAsync<TModel>(
      string baseViewName,
      TModel model
    )
    {
      UFEmailContentModel result = new UFEmailContentModel
      {
        Html = await this.RenderViewAsync(baseViewName + "Html", model, false),
        Text = await this.RenderViewAsync(baseViewName + "Text", model, false)
      };
      result.Html ??= await this.RenderViewAsync(baseViewName + ".html", model, false);
      result.Text ??= await this.RenderViewAsync(baseViewName + ".text", model, false);
      // try base when base + "Html" could not be found
      result.Html ??= await this.RenderViewAsync(baseViewName, model, true);
      // remove /r
      if (result.Text != null)
      {
        result.Text = result.Text.Replace("\r", "");
      }
      return result;
    }

    #endregion

    #region private methods

    /// <summary>
    /// Renders a Razor view and its model data to a string.
    /// </summary>
    /// <typeparam name="TModel">Type of model data</typeparam>
    /// <param name="viewName">
    /// Name of view
    /// </param>
    /// <param name="model">
    /// Model data
    /// </param>
    /// <param name="exception">
    /// When true throw an exception if the view can not be found.
    /// </param>
    /// <returns>
    /// Rendered view or <c>null</c> if <c>exception</c> is <c>true</c>
    /// </returns>
    private async Task<string?> RenderViewAsync<TModel>(
      string viewName,
      TModel model,
      bool exception
    )
    {
      ActionContext actionContext = this.GetActionContext();
      IView? view = this.FindView(actionContext, viewName, exception);
      if (view == null)
      {
        return null;
      }
      await using StringWriter output = new();
      ViewContext viewContext = new(
        actionContext,
        view,
        new ViewDataDictionary<TModel>(
          new EmptyModelMetadataProvider(), new ModelStateDictionary()
        )
        {
          Model = model
        },
        new TempDataDictionary(actionContext.HttpContext, this.m_tempDataProvider),
        output,
        new HtmlHelperOptions()
      );
      await view.RenderAsync(viewContext);
      return output.ToString();
    }

    /// <summary>
    /// Tries to find a view.
    /// </summary>
    /// <param name="actionContext">
    /// Action context to use
    /// </param>
    /// <param name="viewName">
    /// View name
    /// </param>
    /// <param name="exception">
    /// When true throw an exception if the view can not be found.
    /// </param>
    /// <returns>
    /// View or <c>null</c> if <c>exception</c> is <c>true</c>
    /// </returns>
    private IView? FindView(
      ActionContext actionContext,
      string viewName,
      bool exception
    )
    {
      ViewEngineResult getViewResult = this.m_viewEngine.GetView(null, viewName, true);
      if (getViewResult.Success)
      {
        return getViewResult.View;
      }
      ViewEngineResult findViewResult =
        this.m_viewEngine.FindView(actionContext, viewName, true);
      if (findViewResult.Success)
      {
        return findViewResult.View;
      }
      if (!exception)
      {
        return null;
      }
      IEnumerable<string> searchedLocations =
        getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
      string errorMessage = string.Join(
        Environment.NewLine,
        new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }
          .Concat(searchedLocations)
      );
      throw new InvalidOperationException(errorMessage);
    }

    /// <summary>
    /// Gets an action context using the service provider
    /// </summary>
    /// <returns>ActionContext instance</returns>
    private ActionContext GetActionContext()
    {
      DefaultHttpContext httpContext = new()
      {
        RequestServices = this.m_serviceProvider
      };
      return new ActionContext(
        httpContext,
        new RouteData(),
        new ActionDescriptor()
      );
    }

    #endregion
  }
}