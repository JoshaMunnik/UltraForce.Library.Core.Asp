// <copyright file="UFAppExtensions.cs" company="Ultra Force Development">
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using UltraForce.Library.Core.Asp.Types.Classes;

namespace UltraForce.Library.Core.Asp.Extensions;

/// <summary>
/// Extensions for <see cref="IApplicationBuilder"/> to be used in <c>Program.cs</c>
/// </summary>
public static class UFAppExtensions
{
  #region public methods

  /// <summary>
  /// Add <see cref="PhysicalFileProvider"/> for every area that has a <c>wwwroot</c> folder.
  /// <para>
  /// To access files in the <c>wwwroot</c> folder of an area (for example in
  /// the <c>css</c> subfolder), use the following URL (both Areas and wwwroot are
  /// not specified in the URL):
  /// <c>https://.../AreaName/css/...</c>
  /// </para>
  /// <para>
  /// Code based on:
  /// <see href="https://stackoverflow.com/a/56919679/968451" />
  /// </para>
  /// </summary>
  /// <param name="app"></param>
  /// <param name="environment">
  /// When set, replace the <see cref="IWebHostEnvironment.WebRootFileProvider" /> with an instance
  /// of <see cref="UFCompositeStaticFileOptionsProvider"/> adding the static providers for
  /// each area.
  /// </param>
  public static void UseAreaStaticFiles(
    this IApplicationBuilder app,
    IWebHostEnvironment? environment = null
  )
  {
    IEnumerable<string> areas = GetAreasWithWebRoot();
    List<StaticFileOptions> staticFileOptions = [];
    foreach (string area in areas)
    {
      // area points to the wwwroot folder of the area; so get the parent folder of wwwroot
      string parentPath = Path.GetDirectoryName(area)!;
      // act like the folder is a file and get the name of only the parent folder
      string folderBeforeLast = Path.GetFileName(parentPath);
      StaticFileOptions option = new()
      {
        FileProvider = new PhysicalFileProvider(area),
        RequestPath = "/" + folderBeforeLast,
      };
      staticFileOptions.Add(option);
      app.UseStaticFiles(option);
    }
    if (environment != null)
    {
      environment.WebRootFileProvider = new UFCompositeStaticFileOptionsProvider(
        environment.WebRootFileProvider, staticFileOptions
      );
    }
  }

  /// <summary>
  /// Adds support for serving static files with the .avif extension.
  /// </summary>
  /// <param name="app"></param>
  public static void UseStaticAvifFiles(
    this IApplicationBuilder app
  )
  {
    FileExtensionContentTypeProvider extensionProvider = new();
    extensionProvider.Mappings.Add(".avif", "image/avif");
    app.UseStaticFiles(
      new StaticFileOptions
      {
        ContentTypeProvider = extensionProvider,
        ServeUnknownFileTypes = true
      }
    );
  }

  #endregion

  #region private methods

  /// <summary>
  /// Gets all areas that have a wwwroot folder.
  /// </summary>
  /// <returns>Full server paths to the wwwroot folder</returns>
  private static IEnumerable<string> GetAreasWithWebRoot()
  {
    string currentDirectory = Directory.GetCurrentDirectory();
    DirectoryInfo areasDirectory = new(Path.Combine(currentDirectory, "Areas"));
    IEnumerable<DirectoryInfo> areaDirectories = areasDirectory.EnumerateDirectories();
    foreach (DirectoryInfo area in areaDirectories)
    {
      DirectoryInfo? wwwroot = area.EnumerateDirectories("wwwroot").FirstOrDefault();
      if (wwwroot != null)
      {
        yield return wwwroot.FullName;
      }
    }
  }

  #endregion
}
