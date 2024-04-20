using System.Net;
using System.Text;
using Bolt.Endeavor;
using Bolt.Polymorphic.Serializer.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Bolt.ServerDrivenUI.Extensions.Web;

public class ScreenViewResult(WebScreen viewModel) : ActionResult
{
    public override async Task ExecuteResultAsync(ActionContext context)
    {
        var serializer = context.HttpContext.RequestServices.GetRequiredService<IJsonSerializer>();
        var response = context.HttpContext.Response;
        
        response.ContentType = "application/json";
        
        await response.WriteAsync(serializer.Serialize(viewModel), Encoding.UTF8);
    }
}