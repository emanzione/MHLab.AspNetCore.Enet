using ENet;
using MHLab.AspNetCore.Enet.Handlers;
using Microsoft.Extensions.Logging;

namespace MHLab.AspNetCore.Enet.Sample.Handlers
{
    public sealed class PacketHandler : IPacketHandler
    {
        private readonly ILogger<PacketHandler> _logger;

        public PacketHandler(ILogger<PacketHandler> logger)
        {
            _logger = logger;
        }

        public void OnPacketReceived(Peer peer, byte channelId, Packet packet)
        {
            _logger.Log(LogLevel.Debug, $"Packet received from - ID: {peer.ID}, IP: {peer.IP}, Channel ID: {channelId}, Data length: {packet.Length}");
        }
    }
}
