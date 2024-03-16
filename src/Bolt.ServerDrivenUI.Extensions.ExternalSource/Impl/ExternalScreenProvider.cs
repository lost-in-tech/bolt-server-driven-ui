using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Bolt.Endeavor;
using Bolt.Polymorphic.Serializer.Json;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;

internal sealed class ExternalScreenProvider(IHttpClientFactory httpClientFactory,
    IHttpRequestMessageBuilder messageBuilder,
    IJsonSerializer jsonSerializer) : IExternalScreenProvider
{
    public async Task<MaySucceed<Screen>> Get(IRequestContextReader context, ExternalScreenRequest request, CancellationToken ct)
    {
        var client = httpClientFactory.CreateClient(request.ServiceName);

        using var msg = messageBuilder.Build(request.Method, request.Path, 
            request.QueryStrings,
            request.Headers);

        if (request.Content != null)
        {
            msg.Content =new StringContent(jsonSerializer.Serialize(request.Content), 
                Encoding.UTF8, 
                new MediaTypeHeaderValue("application/json"));
        }
        
        using var rsp = await client.SendAsync(msg, HttpCompletionOption.ResponseHeadersRead, ct);
        
        if (rsp.IsSuccessStatusCode)
        {
            await using var content = await rsp.Content.ReadAsStreamAsync(ct);
            
            var screen = await jsonSerializer.Deserialize<Screen>(content, ct);

            if (screen == null) throw new Exception("Failed to read screen object from http content");

            return screen;
        }

        if (rsp.StatusCode == HttpStatusCode.BadRequest)
        {
            await using var content = await rsp.Content.ReadAsStreamAsync(ct);
            var failure = await jsonSerializer.Deserialize<Failure>(content, ct);

            if (failure == null) throw new Exception("Failed to read screen object from http content");
            
            return failure;
        }

        return HttpFailure.New(rsp.StatusCode, $"Dependent Service {request.ServiceName} Failed");
    }
}