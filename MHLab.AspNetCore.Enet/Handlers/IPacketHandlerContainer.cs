using ENet;
using System.Threading.Tasks;

namespace MHLab.AspNetCore.Enet.Handlers
{
    public interface IPacketHandlerContainer
    {
        void AddHandler(IPacketHandler handler);
        Task OnPacketReceived(Peer peer, byte channelId, Packet packet);
    }
}
