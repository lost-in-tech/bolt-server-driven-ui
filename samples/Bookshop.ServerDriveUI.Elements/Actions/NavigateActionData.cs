using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements.Actions;

public class NavigateAction : UiAction<NavigateActionData>
{
}

public record NavigateActionData
{
    public required string Url { get; init; }
    public bool IsExternal { get; init; }
}