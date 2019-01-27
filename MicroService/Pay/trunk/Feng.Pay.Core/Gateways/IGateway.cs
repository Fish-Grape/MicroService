
using Feng.Pay.Core.Request;
using Feng.Pay.Core.Response;

namespace Feng.Pay.Core
{
    public interface IGateway
    {
        TResponse Execute<TModel, TResponse>(Request<TModel, TResponse> request) where TResponse : IResponse;
    }
}
