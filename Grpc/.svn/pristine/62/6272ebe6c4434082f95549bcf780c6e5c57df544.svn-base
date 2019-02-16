using MagicOnion;
using System.Threading.Tasks;

namespace Feng.Grpc.Client
{
    public interface IGRpcConnection
    {
        TService GetRemoteService<TService>(string address, int port) where TService : IService<TService>;
        Task<TService> GetRemoteService<TService>(string serviceName) where TService : IService<TService>;
    }
}
