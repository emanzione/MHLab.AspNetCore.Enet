using ENet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MHLab.AspNetCore.Enet.Handlers
{
    public sealed class TimeoutHandlerContainer : ITimeoutHandlerContainer
    {
        private readonly List<ITimeoutHandler> _handlers;

        public TimeoutHandlerContainer()
        {
            _handlers = new List<ITimeoutHandler>();
        }

        public void AddHandler(ITimeoutHandler handler)
        {
            _handlers.Add(handler);
        }

        public Task OnTimeoutPeer(Peer peer)
        {
            return Task.Run(() =>
            {
                for (var index = 0; index < _handlers.Count; index++)
                {
                    var handler = _handlers[index];
                    handler.OnTimeoutPeer(peer);
                }
            });
        }
    }
}
