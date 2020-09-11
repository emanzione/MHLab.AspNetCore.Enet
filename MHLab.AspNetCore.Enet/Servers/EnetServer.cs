using ENet;
using MHLab.AspNetCore.Enet.Handlers;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Host = ENet.Host;

namespace MHLab.AspNetCore.Enet.Servers
{
    public sealed class EnetServer : IHostedService
    {
        private readonly EnetConfiguration _configuration;

        private readonly ILogger<EnetServer> _logger;

        private readonly IConnectionHandlerContainer _connectionHandlers;
        private readonly IDisconnectionHandlerContainer _disconnectionHandlers;
        private readonly ITimeoutHandlerContainer _timeoutHandlers;
        private readonly IPacketHandlerContainer _packetHandlers;

        private bool _shouldRun;
        private Task _serverTask;

        public EnetServer(
            EnetConfiguration configuration,
            IServiceProvider serviceProvider,
            ILogger<EnetServer> logger,
            IConnectionHandlerContainer connectionHandlers,
            IDisconnectionHandlerContainer disconnectionHandlers,
            ITimeoutHandlerContainer timeoutHandlers,
            IPacketHandlerContainer packetHandlers
        )
        {
            _configuration = configuration;

            _logger = logger;

            _connectionHandlers = connectionHandlers;
            _disconnectionHandlers = disconnectionHandlers;
            _timeoutHandlers = timeoutHandlers;
            _packetHandlers = packetHandlers;

            _configuration.BindConnectionHandlers(_connectionHandlers, serviceProvider);
            _configuration.BindDisconnectionHandlers(_disconnectionHandlers, serviceProvider);
            _configuration.BindTimeoutHandlers(_timeoutHandlers, serviceProvider);
            _configuration.BindPacketHandlers(_packetHandlers, serviceProvider);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() =>
            {
                _shouldRun = false; 
                _logger.LogInformation("Enet server is shutting down...");
            });

            _serverTask = Task.Factory.StartNew(() =>
            {
                Library.Initialize();

                _shouldRun = true;

                using (Host server = new Host())
                {
                    Address address = new Address
                    {
                        Port = _configuration.Port
                    };

                    int pollingTimeout = _configuration.PollingTimeout;

                    server.Create(address, (int)_configuration.PeerLimit);

                    _logger.LogInformation($"Enet server listening at 0.0.0.0:{address.Port}");

                    Event netEvent;

                    while (_shouldRun)
                    {
                        bool polled = false;

                        while (!polled)
                        {
                            if (server.CheckEvents(out netEvent) <= 0)
                            {
                                if (server.Service(pollingTimeout, out netEvent) <= 0)
                                    break;

                                polled = true;
                            }

                            switch (netEvent.Type)
                            {
                                case EventType.None:
                                    break;

                                case EventType.Connect:
                                    _connectionHandlers.OnConnectedPeer(netEvent.Peer);
                                    break;

                                case EventType.Disconnect:
                                    _disconnectionHandlers.OnDisconnectedPeer(netEvent.Peer);
                                    break;

                                case EventType.Timeout:
                                    _timeoutHandlers.OnTimeoutPeer(netEvent.Peer);
                                    break;

                                case EventType.Receive:
                                    _packetHandlers.OnPacketReceived(netEvent.Peer, netEvent.ChannelID, netEvent.Packet);
                                    break;
                            }
                        }
                    }

                    server.Flush();
                }

                _logger.LogInformation("Enet server closed.");

                Library.Deinitialize();
            }, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);

            return _serverTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
