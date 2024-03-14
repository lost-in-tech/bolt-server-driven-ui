using Bolt.Endeavor;

namespace Bolt.ServerDrivenUI.Core;

public interface IRequestDataProvider
{
    MaySucceed<RequestData> Get();
}