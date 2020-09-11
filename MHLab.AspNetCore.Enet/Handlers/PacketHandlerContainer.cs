using ENet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MHLab.AspNetCore.Enet.Handlers
{
    public sealed class PacketHandlerContainer : IPacketHandlerContainer
    {
        private readonly List<IPacketHandler> _processors;

        public PacketHandlerContainer()
        {
            _processors = new List<IPacketHandler>();
        }

        public void AddHandler(IPacketHandler handler)
        {
            _processors.Add(handler);
        }

        public Task OnPacketReceived(Peer peer, byte channelId, Packet packet)
        {
            return Task.Run(() =>
            {
                for (var index = 0; index < _processors.Count; index++)
                {
                    var handler = _processors[index];
                    handler.OnPacketReceived(peer, channelId, packet);
                }

                packet.Dispose();
            });
        }
    }
}
