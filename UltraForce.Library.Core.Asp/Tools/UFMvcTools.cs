// <copyright file="UFMvcTools.cs" company="Ultra Force Development">
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

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using UltraForce.Library.Core.Extensions;
using UltraForce.Library.NetStandard.Annotations;
using UltraForce.Library.NetStandard.Extensions;
using UltraForce.Library.NetStandard.Tools;

namespace UltraForce.Library.Core.Asp.Tools
{
  /// <summary>
  /// <see cref="UFMvcTools"/> contain static support methods for MVC.
  /// </summary>
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public static class UFMvcTools
  {
    #region private constants

    /// <summary>
    /// Controller post used with controller class names.
    /// </summary>
    private const string Controller = "Controller";

    /// <summary>
    /// Controller post used with controller class names.
    /// </summary>
    private const string ViewComponent = "ViewComponent";

    #endregion

    #region public methods

    /// <summary>
    /// Checks if the name ends with 'Controller', if it does remove it.
    /// </summary>
    /// <param name="controller">Name of controller</param>
    /// <returns>Name of controller without 'Controller' at the end</returns>
    public static string GetControllerName(
      string controller
    )
    {
      return controller.EndsWith(UFMvcTools.Controller)
        ? controller[..^UFMvcTools.Controller.Length]
        : controller;
    }

    /// <summary>
    /// Checks if the type name ends with 'Controller', if it does remove it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>Name of type without 'Controller' at the end</returns>
    public static string GetControllerName<T>()
    {
      return GetControllerName(typeof(T).Name);
    }

    /// <summary>
    /// Checks if the name ends with 'ViewComponent', if it does remove it.
    /// </summary>
    /// <param name="component">Name of component</param>
    /// <returns>Name of controller without 'ViewComponent' at the end</returns>
    public static string GetViewComponentName(
      string component
    )
    {
      return component.EndsWith(UFMvcTools.ViewComponent)
        ? component[..^UFMvcTools.ViewComponent.Length]
        : component;
    }

    /// <summary>
    /// Returns a normalized value of a string.
    /// </summary>
    /// <param name="aValue">Value to normalize</param>
    /// <returns>Normalized value</returns>
    public static string Normalize(
      string aValue
    )
    {
      return aValue.ToLowerInvariant();
    }

    /// <summary>
    /// Replace macro texts with values.
    /// <para>
    /// The method replaces the following macro's: {copy}, {year} and {user}
    /// </para>
    /// </summary>
    /// <param name="text">text that might contain macro's</param>
    /// <param name="principal">value to get the username from</param>
    /// <returns>text with the macro's replaced</returns>
    public static string ReplaceMacros(
      string text,
      IPrincipal? principal = null
    )
    {
      string result = text
        .Replace("{copy}", "&copy;")
        .Replace("{year}", DateTime.Now.Year.ToString());
      if (principal is { Identity: not null })
      {
        result = result.Replace("{user}", principal.Identity.Name);
      }
      return result;
    }

    /// <summary>
    /// Adds an item at the start of a select list. Preserves the selected state.
    /// </summary>
    /// <param name="list">List to add item to</param>
    /// <param name="newItem">Item to insert at first position</param>
    /// <returns>New list with item inserted</returns>
    public static SelectList AddFirstItem(
      SelectList list,
      SelectListItem newItem
    )
    {
      List<SelectListItem> newList = list.ToList();
      newList.Insert(0, newItem);
      SelectListItem? selectedItem = newList.FirstOrDefault(item => item.Selected);
      string selectedItemValue = string.Empty;
      if (selectedItem != null)
      {
        selectedItemValue = selectedItem.Value;
      }
      return new SelectList(
        newList, nameof(SelectListItem.Value), nameof(SelectListItem.Text),
        selectedItemValue
      );
    }

    /// <summary>
    /// Creates a <see cref="SelectList"/> from an enum type. The methods tries to obtain the text
    /// from either the display name or the value from <see cref="UFDescriptionAttribute"/>
    /// attribute. If both are missing the value is used.
    /// </summary>
    /// <param name="emptyChoice">
    /// when not empty, add this string as first entry with an empty string for value
    /// </param>
    /// <param name="sort">true to sort the list of values</param>
    /// <typeparam name="T">Enum type</typeparam>
    /// <returns>Select list</returns>
    public static SelectList CreateListFromEnum<T>(
      string? emptyChoice = null,
      bool sort = true
    )
      where T : struct, Enum
    {
      List<SelectListItem> items = Enum
        .GetValues<T>()
        .Select(
          value => new SelectListItem(
            //value.GetDisplayDescription(), System.Convert.ToInt32(value).ToString()
            UFStringTools.SelectString(
              value.GetDisplayName(), value.GetDescription(), value.ToString()
            ),
            value.ToString()
          )
        )
        .ToList();
      if (sort)
      {
        items.Sort((
            item1,
            item2
          ) => string.Compare(item1.Text, item2.Text, StringComparison.Ordinal)
        );
      }
      SelectList list = new(
        items,
        nameof(SelectListItem.Value),
        nameof(SelectListItem.Text)
      );
      return string.IsNullOrEmpty(emptyChoice)
        ? list
        : AddFirstItem(list, new SelectListItem(emptyChoice, string.Empty));
    }

    /// <summary>
    /// Gets the errors for a field as a single string
    /// </summary>
    /// <param name="aModelState">Model state to get errors from</param>
    /// <param name="name">Field name to get errors for</param>
    /// <param name="divider">Value to place between errors</param>
    /// <param name="head">Value at start, only used if there are errors</param>
    /// <param name="tail">Value at end, only used if there are errors</param>
    /// <returns>
    /// A string containing all errors or <see cref="string.Empty" /> if there are no errors
    /// or no field with the specified name can be found
    /// </returns>
    public static string GetErrors(
      ModelStateDictionary aModelState,
      string name,
      string divider = ", ",
      string head = "",
      string tail = ""
    )
    {
      if (
        string.IsNullOrEmpty(name) || !aModelState.TryGetValue(name, out ModelStateEntry? value)
      )
      {
        return string.Empty;
      }
      ModelErrorCollection errors = value.Errors;
      if (errors.Count == 0)
      {
        return string.Empty;
      }
      return errors.Aggregate(
        head,
        (
          current,
          error
        ) => current + error.ErrorMessage
      ) + tail;
    }

    /// <summary>
    /// Validates an object using their validation attributes.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static (bool isValid, ICollection<ValidationResult> results) Validate(
      object value
    )
    {
      ICollection<ValidationResult> results = new List<ValidationResult>();
      bool isValid = Validator.TryValidateObject(
        value, new ValidationContext(value), results, true
      );
      return (isValid, results);
    }

    /// <summary>
    /// Gets the json name for a property in an object. The method checks if a property has
    /// a <see cref="JsonPropertyNameAttribute"/> and if so returns the name from the attribute.
    /// If no attribute can be found the property name is returned.
    /// </summary>
    /// <param name="value">Object to get property from</param>
    /// <param name="propertyName">Name of property</param>
    /// <returns>Json name or value of <c>aProperty</c></returns>
    /// <exception cref="Exception">
    /// When no property can be found for the specified name
    /// </exception>
    public static string GetJsonName(
      object value,
      string propertyName
    )
    {
      PropertyInfo? propertyInfo = value.GetType().GetProperty(propertyName);
      if (propertyInfo == null)
      {
        throw new Exception("Can not find property " + propertyName);
      }
      JsonPropertyNameAttribute? attribute =
        propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
      return attribute == null ? propertyName : attribute.Name;
    }

    #endregion
  }
}
