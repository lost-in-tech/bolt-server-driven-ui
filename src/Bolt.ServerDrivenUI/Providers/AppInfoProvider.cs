﻿using System.Reflection;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Providers;

internal sealed class AppInfoProvider : IAppInfoProvider
{
    private static readonly Lazy<AppInfo> Instance = new(() => new AppInfo
    {
        Name = AppDomain.CurrentDomain.FriendlyName.Replace(".", "-").ToLowerInvariant(),
        Version = Assembly.GetEntryAssembly()?.GetName().Version ?? new Version(1, 0, 0)
    });

    public AppInfo Get() => Instance.Value;
}