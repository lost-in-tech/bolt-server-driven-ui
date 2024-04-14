using Microsoft.AspNetCore.Builder;

namespace Bolt.ServerDrivenUI.Extensions.Web.Endpoints;

public interface IEndpointMapper
{
    void Map(WebApplication app);
}