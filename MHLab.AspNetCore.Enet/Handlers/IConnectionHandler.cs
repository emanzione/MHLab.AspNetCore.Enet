using ENet;

namespace MHLab.AspNetCore.Enet.Handlers
{
    public interface IConnectionHandler
    {
        void OnConnectedPeer(Peer peer);
    }
}
