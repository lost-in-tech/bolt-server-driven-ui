using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Bolt.ServerDrivenUI.Extensions.Web.Endpoints;

public static class WebApplicationExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        var assembly = Assembly.GetEntryAssembly();

        if(assembly == null) return;
        
        var mappers = assembly.GetExportedTypes().Where(x => x.IsClass && x.IsAssignableTo(typeof(IEndpointMapper)));

        foreach (var mapperType in mappers)
        {
            if (Activator.CreateInstance(mapperType) is IEndpointMapper instance)
            {
                instance.Map(app);
            }
        }
    }
}