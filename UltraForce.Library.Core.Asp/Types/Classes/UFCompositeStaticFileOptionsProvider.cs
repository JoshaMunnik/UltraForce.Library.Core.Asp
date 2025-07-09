// <copyright file="UFCompositeStaticFileOptionsProvider.cs" company="Ultra Force Development">
// Ultra Force Library - Copyright (C) 2025 Ultra Force Development
// </copyright>
// <author>Josha Munnik</author>
// <email>josha@ultraforce.com</email>
// <license>
// The MIT License (MIT)
//
// Copyright (C) 2025 Ultra Force Development
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

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace UltraForce.Library.Core.Asp.Types.Classes;

/// <summary>
/// This class provides a composite file provider that combines multiple static file options and
/// reroutes to one of the static file providers if the request path matches the start of the path.
/// <para>
/// Code based on:
/// <see href="https://stackoverflow.com/a/61925004/968451" />
/// </para>
/// </summary>
public class UFCompositeStaticFileOptionsProvider : IFileProvider
{
  #region private variables

  /// <summary>
  /// Original file provider that serves the web root files.
  /// </summary>
  private readonly IFileProvider m_webRootFileProvider;

  /// <summary>
  /// Options of additional static file providers that can be used to serve files.
  /// </summary>
  private readonly IEnumerable<StaticFileOptions> m_staticFileOptions;

  #endregion

  #region constructors

  /// <summary>
  /// Constructs a new instance of the <see cref="UFCompositeStaticFileOptionsProvider"/> class.
  /// </summary>
  /// <param name="webRootFileProvider">Current registered file provider</param>
  /// <param name="staticFileOptions">Additional registered static file providers</param>
  public UFCompositeStaticFileOptionsProvider(
    IFileProvider webRootFileProvider,
    params StaticFileOptions[] staticFileOptions
  ) : this(webRootFileProvider, (IEnumerable<StaticFileOptions>)staticFileOptions)
  {
  }

  /// <summary>
  /// Constructs a new instance of the <see cref="UFCompositeStaticFileOptionsProvider"/> class.
  /// <para>
  /// Note that the code does not register the static file provider specified via the option, it
  /// assumes this has been done by the caller.
  /// </para>
  /// </summary>
  /// <param name="webRootFileProvider">Current registered file provider</param>
  /// <param name="staticFileOptions">Additional registered static file providers</param>
  /// <exception cref="ArgumentNullException"></exception>
  public UFCompositeStaticFileOptionsProvider(
    IFileProvider webRootFileProvider,
    IEnumerable<StaticFileOptions> staticFileOptions
  )
  {
    this.m_webRootFileProvider = webRootFileProvider ??
      throw new ArgumentNullException(nameof(webRootFileProvider));
    this.m_staticFileOptions = staticFileOptions;
  }

  #endregion

  #region IFileProvider

  /// <inheritdoc />
  public IDirectoryContents GetDirectoryContents(
    string subpath
  )
  {
    IFileProvider provider = this.GetFileProvider(subpath, out string outPath);
    return provider.GetDirectoryContents(outPath);
  }

  /// <inheritdoc />
  public IFileInfo GetFileInfo(
    string subpath
  )
  {
    IFileProvider provider = this.GetFileProvider(subpath, out string outPath);
    return provider.GetFileInfo(outPath);
  }

  /// <inheritdoc />
  public IChangeToken Watch(
    string filter
  )
  {
    IFileProvider provider = this.GetFileProvider(filter, out string outPath);
    return provider.Watch(outPath);
  }

  #endregion

  #region private methods

  /// <summary>
  /// Tries to find the correct file provider based on the path.
  /// </summary>
  /// <param name="path"></param>
  /// <param name="outPath">path relative to the file provider</param>
  /// <returns>Provider to use</returns>
  private IFileProvider GetFileProvider(
    string path,
    out string outPath
  )
  {
    outPath = path;
    foreach (StaticFileOptions staticFileOption in this.m_staticFileOptions)
    {
      if (
        (staticFileOption.FileProvider == null) ||
        !path.StartsWith(staticFileOption.RequestPath, StringComparison.Ordinal)
      )
      {
        continue;
      }
      outPath = path.Substring(
        staticFileOption.RequestPath.Value?.Length ?? 0,
        path.Length - staticFileOption.RequestPath.Value?.Length ?? 0
      );
      return staticFileOption.FileProvider;
    }
    return this.m_webRootFileProvider;
  }

  #endregion
}
