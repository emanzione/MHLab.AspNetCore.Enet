using ENet;
using System.Threading.Tasks;

namespace MHLab.AspNetCore.Enet.Handlers
{
    public interface IDisconnectionHandlerContainer
    {
        void AddHandler(IDisconnectionHandler handler);
        Task OnDisconnectedPeer(Peer peer);
    }
}
