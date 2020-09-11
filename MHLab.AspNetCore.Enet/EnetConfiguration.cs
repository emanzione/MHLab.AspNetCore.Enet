using ENet;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using MHLab.AspNetCore.Enet.Handlers;

namespace MHLab.AspNetCore.Enet
{
    public sealed class EnetConfiguration
    {
        internal ushort Port = 7001;
        internal int PollingTimeout = 15;
        internal uint PeerLimit = Library.maxPeers;

        private readonly IServiceCollection _services;

        private readonly List<Type> _connectionHandlerTypes;
        private readonly List<Type> _disconnectionHandlerTypes;
        private readonly List<Type> _timeoutHandlerTypes;
        private readonly List<Type> _packetHandlerTypes;

        internal EnetConfiguration(IServiceCollection services)
        {
            _services = services;

            _connectionHandlerTypes = new List<Type>();
            _disconnectionHandlerTypes = new List<Type>();
            _timeoutHandlerTypes = new List<Type>();
            _packetHandlerTypes = new List<Type>();
        }

        public EnetConfiguration SetPort(ushort port)
        {
            Port = port;
            return this;
        }

        public EnetConfiguration SetPollingTimeout(int timeout)
        {
            PollingTimeout = timeout;
            return this;
        }

        public EnetConfiguration SetPeerLimit(uint peerLimit)
        {
            PeerLimit = Math.Min(peerLimit, Library.maxPeers);
            return this;
        }

        public EnetConfiguration AddConnectionHandler<THandler>() where THandler : class, IConnectionHandler
        {
            _connectionHandlerTypes.Add(typeof(THandler));
            _services.AddSingleton<THandler>();
            return this;
        }

        public EnetConfiguration AddDisconnectionHandler<THandler>() where THandler : class, IDisconnectionHandler
        {
            _disconnectionHandlerTypes.Add(typeof(THandler));
            _services.AddSingleton<THandler>();
            return this;
        }

        public EnetConfiguration AddTimeoutHandler<THandler>() where THandler : class, ITimeoutHandler
        {
            _timeoutHandlerTypes.Add(typeof(THandler));
            _services.AddSingleton<THandler>();
            return this;
        }

        public EnetConfiguration AddPacketHandler<THandler>() where THandler : class, IPacketHandler
        {
            _packetHandlerTypes.Add(typeof(THandler));
            _services.AddSingleton<THandler>();
            return this;
        }

        internal void BindConnectionHandlers(IConnectionHandlerContainer container, IServiceProvider provider)
        {
            foreach (var handlerType in _connectionHandlerTypes)
            {
                var handler = (IConnectionHandler)provider.GetService(handlerType);
                container.AddHandler(handler);
            }
        }

        internal void BindDisconnectionHandlers(IDisconnectionHandlerContainer container, IServiceProvider provider)
        {
            foreach (var handlerType in _disconnectionHandlerTypes)
            {
                var handler = (IDisconnectionHandler)provider.GetService(handlerType);
                container.AddHandler(handler);
            }
        }

        internal void BindTimeoutHandlers(ITimeoutHandlerContainer container, IServiceProvider provider)
        {
            foreach (var handlerType in _timeoutHandlerTypes)
            {
                var handler = (ITimeoutHandler)provider.GetService(handlerType);
                container.AddHandler(handler);
            }
        }

        internal void BindPacketHandlers(IPacketHandlerContainer container, IServiceProvider provider)
        {
            foreach (var handlerType in _packetHandlerTypes)
            {
                var handler = (IPacketHandler)provider.GetService(handlerType);
                container.AddHandler(handler);
            }
        }
    }
}
