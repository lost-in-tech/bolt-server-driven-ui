using Bolt.MaySucceed;

namespace Ensemble.Core;

public interface IRequestDataProvider
{
    MaySucceed<RequestData> Get();
}