﻿using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI.Core;

public interface IScreenComposer<in TRequest>
{
    Task<MaySucceed<Screen>> Compose(TRequest request, CancellationToken ct);
}

public record Screen
{
    public Dictionary<string, ScreenLayout>? Layouts { get; init; }
    public required IEnumerable<ScreenSection> Sections { get; init; }
    public Dictionary<string, object>? ContextData { get; init; }
    public IEnumerable<IMetaData>? MetaData { get; init; }
}

public record ScreenLayout
{
    public string? VersionId { get; init; }
    public bool? NotModified { get; init; }
    public required IElement Element { get; init; }
}

public record ScreenSection
{
    public required string Name { get; init; }
    public required IElement Element { get; init; }
}