using ENet;
using MHLab.AspNetCore.Enet.Handlers;
using Microsoft.Extensions.Logging;

namespace MHLab.AspNetCore.Enet.Sample.Handlers
{
    public class DisconnectionHandler : IDisconnectionHandler
    {
        private readonly ILogger<DisconnectionHandler> _logger;

        public DisconnectionHandler(ILogger<DisconnectionHandler> logger)
        {
            _logger = logger;
        }

        public void OnDisconnectedPeer(Peer peer)
        {
            _logger.Log(LogLevel.Debug, $"Client disconnected - ID: {peer.ID}, IP: {peer.IP}");
        }
    }
}
