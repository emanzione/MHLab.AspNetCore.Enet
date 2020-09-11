using ENet;

namespace MHLab.AspNetCore.Enet.Handlers
{
    public interface IPacketHandler
    {
        void OnPacketReceived(Peer peer, byte channelId, Packet packet);
    }
}
