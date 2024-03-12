using Bolt.MaySucceed;

namespace Bolt.ServerDrivenUI.Core;

public interface IRequestDataProvider
{
    MaySucceed<RequestData> Get();
}