using ENet;

namespace MHLab.AspNetCore.Enet.Handlers
{
    public interface IDisconnectionHandler
    {
        void OnDisconnectedPeer(Peer peer);
    }
}
