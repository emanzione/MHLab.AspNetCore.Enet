using ENet;
using System.Threading.Tasks;

namespace MHLab.AspNetCore.Enet.Handlers
{
    public interface ITimeoutHandlerContainer
    {
        void AddHandler(ITimeoutHandler handler);
        Task OnTimeoutPeer(Peer peer);
    }
}
