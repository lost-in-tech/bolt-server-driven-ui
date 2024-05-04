namespace Bolt.ServerDrivenUI.Core.Elements;

public interface IUiAction
{
}

public abstract class ActionBase : IUiAction
{
    public string Name { get; set; } = string.Empty;
}

public class UiAction<T> : IUiAction
{
    public string? Name { get; set; }
    public T? Data { get; set; }
}

public class UiAction : IUiAction
{
    public string? Name { get; set; }
    public Dictionary<string,string>? Data { get; set; }
}