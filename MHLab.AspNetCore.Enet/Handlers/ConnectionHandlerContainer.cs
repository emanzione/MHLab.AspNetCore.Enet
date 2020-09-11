using ENet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MHLab.AspNetCore.Enet.Handlers
{
    public sealed class ConnectionHandlerContainer : IConnectionHandlerContainer
    {
        private readonly List<IConnectionHandler> _handlers;

        public ConnectionHandlerContainer()
        {
            _handlers = new List<IConnectionHandler>();
        }

        public void AddHandler(IConnectionHandler handler)
        {
            _handlers.Add(handler);
        }

        public Task OnConnectedPeer(Peer peer)
        {
            return Task.Run(() =>
            {
                for (var index = 0; index < _handlers.Count; index++)
                {
                    var handler = _handlers[index];
                    handler.OnConnectedPeer(peer);
                }
            });
        }
    }
}
