using ENet;
using System.Threading.Tasks;

namespace MHLab.AspNetCore.Enet.Handlers
{
    public interface IConnectionHandlerContainer
    {
        void AddHandler(IConnectionHandler handler);
        Task OnConnectedPeer(Peer peer);
    }
}
