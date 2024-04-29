using Bolt.Common.Extensions;
using Bolt.IocScanner.Attributes;

namespace Bookshop.ServerDrivenUI.Api.Features.Shared;

public interface IAppUrlBuilder
{
    string Home();
    string Details(string isbn, string? title = null);
    string List(string? category = null);
}

[AutoBind(LifeCycle.Singleton)]
internal sealed class AppUrlBuilder : IAppUrlBuilder
{
    public string Home()
    {
        return "/";
    }

    public string Details(string isbn, string? title = null)
    {
        return $"/books/{(title.IsEmpty() ? "x" : title.ToSlug())}/isbn-{isbn}";
    }

    public string List(string? category = null)
    {
        return $"/books/{category}";
    }
}