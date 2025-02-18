// <copyright file="UFScopedBackgroundService.cs" company="Ultra Force Development">
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

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UltraForce.Library.Core.Asp.Services
{
  /// <summary>
  /// A background service helper class to execute a service in the background using its own
  /// service scope.
  /// <para>
  /// Note: do not forget to register the <c>TAsynchronousExecutableService</c> type at the service provider.
  /// </para>
  /// <para>
  /// The code is based on:
  /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0#consuming-a-scoped-service-in-a-background-task-1
  /// </para>
  /// </summary>
  /// <typeparam name="TAsynchronousExecutableService"></typeparam>
  [SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
  public class UFScopedBackgroundService<TAsynchronousExecutableService> : BackgroundService
    where TAsynchronousExecutableService : IUFAsynchronousExecutableService
  {
    #region private variables

    /// <summary>
    /// Logger for this class
    /// </summary>
    private readonly ILogger<UFScopedBackgroundService<TAsynchronousExecutableService>> m_logger;

    #endregion

    #region constructors & destructors

    /// <summary>
    /// Constructs an instance of <see cref="UFScopedBackgroundService{TBackgroundService}"/>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="logger"></param>
    public UFScopedBackgroundService(
      IServiceProvider services,
      ILogger<UFScopedBackgroundService<TAsynchronousExecutableService>> logger
    )
    {
      this.Services = services;
      this.m_logger = logger;
    }

    #endregion

    #region protected properties

    /// <summary>
    /// Reference to the services provider
    /// </summary>
    protected IServiceProvider Services { get; }

    #endregion

    #region BackgroundService

    /// <inheritdoc />
    protected override async Task ExecuteAsync(
      CancellationToken stoppingToken
    )
    {
      this.m_logger.LogInformation(
        $"Scoped Background Service for {typeof(TAsynchronousExecutableService).Name} has started"
      );
      using IServiceScope scope = this.Services.CreateScope();
      TAsynchronousExecutableService scopedProcessingService =
        scope.ServiceProvider.GetRequiredService<TAsynchronousExecutableService>();
      await scopedProcessingService.ExecuteAsync(stoppingToken);
      this.m_logger.LogInformation(
        $"Scoped Background Service for {typeof(TAsynchronousExecutableService).Name} has stopped"
      );
    }

    /// <inheritdoc />
    public override async Task StopAsync(
      CancellationToken stoppingToken
    )
    {
      this.m_logger.LogInformation(
        $"Scoped Background Service for {typeof(TAsynchronousExecutableService).Name} is stopping"
      );
      await base.StopAsync(stoppingToken);
    }

    #endregion
  }
}