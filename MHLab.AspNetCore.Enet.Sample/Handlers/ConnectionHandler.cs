using ENet;
using MHLab.AspNetCore.Enet.Handlers;
using Microsoft.Extensions.Logging;

namespace MHLab.AspNetCore.Enet.Sample.Handlers
{
    public class ConnectionHandler : IConnectionHandler
    {
        private readonly ILogger<ConnectionHandler> _logger;

        public ConnectionHandler(ILogger<ConnectionHandler> logger)
        {
            _logger = logger;
        }

        public void OnConnectedPeer(Peer peer)
        {
            _logger.LogDebug($"Client connected - ID: {peer.ID}, IP: {peer.IP}");
        }
    }
}
