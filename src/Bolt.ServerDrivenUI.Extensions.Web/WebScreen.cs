using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.Web;

public record WebScreen : Screen
{
    public required HttpResponseInstruction ResponseInstruction { get; init; }
}

public record HttpResponseInstruction
{
    public int HttpStatusCode { get; init; }
    public string? RedirectUrl { get; init; }
}