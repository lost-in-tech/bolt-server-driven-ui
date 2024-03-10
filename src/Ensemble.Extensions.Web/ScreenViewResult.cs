using System.Net;
using System.Text;
using Bolt.MaySucceed;
using Bolt.Polymorphic.Serializer.Json;
using Ensemble.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Ensemble.Extensions.Web;

public class ScreenViewResult(MaySucceed<Screen> viewModel) : ActionResult
{
    public override async Task ExecuteResultAsync(ActionContext context)
    {
        var serializer = context.HttpContext.RequestServices.GetRequiredService<IJsonSerializer>();
        var response = context.HttpContext.Response;
        
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