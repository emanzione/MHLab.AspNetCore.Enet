using ENet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MHLab.AspNetCore.Enet.Handlers
{
    public sealed class DisconnectionHandlerContainer : IDisconnectionHandlerContainer
    {
        private readonly List<IDisconnectionHandler> _handlers;

        public DisconnectionHandlerContainer()
        {
            _handlers = new List<IDisconnectionHandler>();
        }

        public void AddHandler(IDisconnectionHandler handler)
        {
            _handlers.Add(handler);
        }

        public Task OnDisconnectedPeer(Peer peer)
        {
            return Task.Run(() =>
            {
                for (var index = 0; index < _handlers.Count; index++)
                {
                    var handler = _handlers[index];
                    handler.OnDisconnectedPeer(peer);
                }
            });
        }
    }
}
