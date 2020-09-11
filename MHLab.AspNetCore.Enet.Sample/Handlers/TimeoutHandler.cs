using ENet;
using MHLab.AspNetCore.Enet.Handlers;
using Microsoft.Extensions.Logging;

namespace MHLab.AspNetCore.Enet.Sample.Handlers
{
    public sealed class TimeoutHandler : ITimeoutHandler
    {
        private readonly ILogger<TimeoutHandler> _logger;

        public TimeoutHandler(ILogger<TimeoutHandler> logger)
        {
            _logger = logger;
        }

        public void OnTimeoutPeer(Peer peer)
        {
            _logger.Log(LogLevel.Debug, $"Client timeout - ID: {peer.ID}, IP: {peer.IP}");
        }
    }
}
