// <copyright file="UFModelExpressionTools.cs" company="Ultra Force Development">
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

using System.Collections;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace UltraForce.Library.Core.Asp.Tools;

/// <summary>
/// Support methods for <see cref="ModelExpression"/>.
/// </summary>
public static class UFModelExpressionTools
{
  /// <summary>
  /// Checks if a model expression represents an IEnumerable.
  /// </summary>
  /// <param name="modelExpression"></param>
  /// <returns></returns>
  public static bool IsEnumerable(
    ModelExpression modelExpression
  )
  {
    Type modelType = modelExpression.Model.GetType();
    return typeof(IEnumerable).IsAssignableFrom(modelType);
  }

  /// <summary>
  /// Checks if a model expression represents a generic IEnumerable.
  /// </summary>
  /// <param name="modelExpression"></param>
  /// <returns></returns>
  public static bool IsGenericEnumerable(
    ModelExpression modelExpression
  )
  {
    Type modelType = modelExpression.Model.GetType();
    return modelType.IsGenericType && modelType.GetGenericTypeDefinition() == typeof(IEnumerable<>);
  }

  /// <summary>
  /// Tries to get an attribute from the property in a model expression.
  /// </summary>
  /// <param name="modelExpression">Expression to check</param>
  /// <typeparam name="TAttribute">Attribute type to find</typeparam>
  /// <returns>Attribute instance or null if none can be found</returns>
  public static TAttribute? GetAttribute<TAttribute>(
    ModelExpression modelExpression
  )
    where TAttribute : Attribute
  {
    ModelMetadata? metadata = modelExpression.Metadata;
    if ((metadata?.ContainerType == null) || (metadata.PropertyName == null))
    {
      return null;
    }
    return metadata.ContainerType
      .GetProperty(metadata.PropertyName)?
      .GetCustomAttribute<TAttribute>();
  }
  
  /// <summary>
  /// Checks if a model expression represents an enum type.
  /// </summary>
  /// <param name="modelExpression">Model expression to check</param>
  /// <returns>True if type is an enum type</returns>
  public static bool IsEnumType(ModelExpression modelExpression)
  {
    return modelExpression.Metadata.ModelType.IsEnum;
  }

  /// <summary>
  /// Gets all enum values from a model expression.
  /// </summary>
  /// <param name="modelExpression">Model expression to get the values from</param>
  /// <returns>A list of enum values</returns>
  /// <exception cref="InvalidOperationException">
  /// If the model expression does not contain an enumeration type.
  /// </exception>
  public static Array GetEnumValues(ModelExpression modelExpression)
  {
    if (modelExpression.Metadata.ModelType.IsEnum)
    {
      return Enum.GetValues(modelExpression.Metadata.ModelType);
    }
    throw new InvalidOperationException("The model expression is not an enum type.");
  }
}