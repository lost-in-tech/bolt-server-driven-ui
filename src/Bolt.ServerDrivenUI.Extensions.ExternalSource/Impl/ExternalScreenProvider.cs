using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Bolt.Endeavor;
using Bolt.Polymorphic.Serializer.Json;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;

internal sealed class ExternalScreenProvider(IHttpClientWrap httpClientWrap,
    IHttpRequestMessageBuilder messageBuilder,
    IJsonSerializer jsonSerializer,
    IRequestKeyNamesProvider keyNamesProvider) 
    : IExternalScreenProvider
{
    public async Task<MaySucceed<Screen>> Get(
        IRequestContextReader context, 
        ExternalScreenRequest request, 
        CancellationToken ct)
    {
        using var msg = messageBuilder.Build(new()
            {
                Method = request.Method,
                Path = request.Path,
                QueryStrings = AppendSectionsQs(context, 
                                    request.QueryStrings, 
                                    request.ForSections),
                Headers = request.Headers
            });

        if (request.Content != null)
        {
            msg.Content =new StringContent(jsonSerializer.Serialize(request.Content), 
                Encoding.UTF8, 
                new MediaTypeHeaderValue("application/json"));
        }
        
        using var rsp = await httpClientWrap.SendAsync(request.ServiceName, msg,ct);
        
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

    private IEnumerable<(string Key, string? Value)> AppendSectionsQs(IRequestContextReader context, 
        IEnumerable<(string Key, string? Value)>? query,
        SectionInfo[] sections)
    {
        var sectionKeyName = keyNamesProvider.Get().SectionNames;
        var newSectionNamesToAppend = BuildSectionsQueryValues(context, sections).ToArray();
        var sectionKeyExists = false;

        if (query != null)
        {
            foreach (var item in query)
            {
                if (newSectionNamesToAppend.Length > 0)
                {
                    if (string.Equals(item.Key, sectionKeyName, StringComparison.OrdinalIgnoreCase))
                    {
                        sectionKeyExists = true;

                        yield return (item.Key, $"{item.Value},{string.Join(",", newSectionNamesToAppend)}");

                        continue;
                    }
                }

                yield return item;
            }
        }

        if (!sectionKeyExists && newSectionNamesToAppend.Length > 0)
        {
            yield return (keyNamesProvider.Get().SectionNames, string.Join(",", newSectionNamesToAppend));
        }
    }

    private IEnumerable<string> BuildSectionsQueryValues(IRequestContextReader context, SectionInfo[] sections)
    {
        var requestData = context.RequestData();

        var requestedSectionNames = requestData.SectionNames;

        if (requestedSectionNames.Length > 0)
        {
            foreach (var requestedSectionName in requestedSectionNames)
            {
                SectionInfo? section = sections.FirstOrDefault(x => string.Equals(x.Name,
                    requestedSectionName,
                    StringComparison.OrdinalIgnoreCase));

                if (section.HasValue) yield return section.Value.Name;
            }
        }
        else
        {
            foreach (var sectionInfo in sections)
            {
                yield return sectionInfo.Name;
            }
        }
    }
}