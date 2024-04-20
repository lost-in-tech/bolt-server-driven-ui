using System.Text;
using Bolt.Endeavor;
using Bolt.Polymorphic.Serializer.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Bolt.ServerDrivenUI.Extensions.Web;

public class ScreenEndpointResult(WebScreen viewModel) : IResult
{
    public  async Task ExecuteAsync(HttpContext context)
    {
        var serializer = context.RequestServices.GetRequiredService<IJsonSerializer>();
        var response = context.Response;
        
        response.ContentType = "application/json";
        await response.WriteAsync(serializer.Serialize(viewModel), Encoding.UTF8);
    }
}