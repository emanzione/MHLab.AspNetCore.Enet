using ENet;

namespace MHLab.AspNetCore.Enet.Handlers
{
    public interface ITimeoutHandler
    {
        void OnTimeoutPeer(Peer peer);
    }
}
