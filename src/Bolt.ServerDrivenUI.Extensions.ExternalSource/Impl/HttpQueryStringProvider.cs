using System.Text;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;

internal sealed class HttpQueryStringProvider(
        IRequestContextReader context,
        IRequestKeyNamesProvider requestKeyNamesProvider) 
    : IHttpQueryStringProvider
{
    private const char SectionsSeparator = ',';
    public IEnumerable<(string Key, string? Value)> Get()
    {
        var requestData = context.RequestData();
        var keyNames = requestKeyNamesProvider.Get();

        if (requestData.SectionNames.Length > 0)
        {
            yield return (keyNames.SectionNames, 
                string.Join(SectionsSeparator, requestData.SectionNames));
        }

        var mode = requestData.Mode == RequestMode.Default 
            ? RequestMode.Sections 
            : requestData.Mode; 

        yield return (keyNames.Mode, 
            mode.ToString());
    }
}
