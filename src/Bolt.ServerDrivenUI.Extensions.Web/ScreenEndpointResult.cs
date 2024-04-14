using System.Text;
using Bolt.Endeavor;
using Bolt.Polymorphic.Serializer.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Bolt.ServerDrivenUI.Extensions.Web;

public class ScreenEndpointResult(MaySucceed<Bolt.ServerDrivenUI.Core.Screen> viewModel) : IResult
{
    public  async Task ExecuteAsync(HttpContext context)
    {
        var serializer = context.RequestServices.GetRequiredService<IJsonSerializer>();
        var response = context.Response;
        
        response.ContentType = "application/json";
        
        if(viewModel.IsSucceed)
        {
            await response.WriteAsync(serializer.Serialize(viewModel.Value), Encoding.UTF8);
            return;
        }

        response.StatusCode = viewModel.Failure.StatusCode;
        await response.WriteAsync(serializer.Serialize(viewModel.Failure), Encoding.UTF8);
    }
}