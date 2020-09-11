# MHLab.AspNetCore.Enet

![Build](https://github.com/manhunterita/MHLab.AspNetCore.Enet/workflows/Build/badge.svg)
[![Nuget](https://img.shields.io/nuget/v/MHLab.AspNetCore.Enet)](https://www.nuget.org/packages/MHLab.AspNetCore.Enet/)

An extension that allows you to spin an Enet server in your ASP.NET Core application. It's based on the [Stanislav Denisov](https://github.com/nxrighthere)'s awesome work: [ENet-CSharp](https://github.com/nxrighthere/ENet-CSharp).

It's easy to set:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Your other code...

    services.AddEnet((configuration) =>
    {
        configuration
            /* Set the port your Enet Server will listen to. */
            .SetPort(7001)
            /* Set the polling time, in milliseconds. */
            .SetPollingTimeout(15);

        /* Add your connection handlers to compose the pipeline. The order matters. */
        configuration
            .AddConnectionHandler<MyConnectionHandler1>()
            .AddConnectionHandler<MyConnectionHandlerN>();

        /* Add your disconnection handlers to compose the pipeline. The order matters. */
        configuration
            .AddDisconnectionHandler<MyDisconnectionHandler1>()
            .AddDisconnectionHandler<MyDisconnectionHandlerN>();

        /* Add your timeout handlers to compose the pipeline. The order matters. */
        configuration
            .AddTimeoutHandler<MyTimeoutHandler1>()
            .AddTimeoutHandler<MyTimeoutHandlerN>();

        /* Add your packet received handlers to compose the pipeline. The order matters. */
        configuration
            .AddPacketHandler<MyPacketHandler1>()
            .AddPacketHandler<MyPacketHandlerN>();
    });

    // Your other code...
}
```

## Sample

You can find the usage sample in the `MHLab.AspNetCore.Enet.Sample` folder. It's the classic ASP.NET Core Web API template with the addition of the Enet Server.

## Connection Handlers

A connection handler is triggered when a new client connection is established. You can build your connection handlers like this:

```csharp
using ENet;
using MHLab.AspNetCore.Enet.Handlers;
using Microsoft.Extensions.Logging;

public class ConnectionHandler : IConnectionHandler
{
    private readonly ILogger<ConnectionHandler> _logger;

    public ConnectionHandler(ILogger<ConnectionHandler> logger)
    {
        /* Here in the constructor you can take the full advantage of the 
        Microsoft Dependency Injection framework. */
        _logger = logger;
    }

    public void OnConnectedPeer(Peer peer)
    {
        _logger.LogDebug($"Client connected - ID: {peer.ID}, IP: {peer.IP}");
    }
}
```

## Disconnection Handlers

A disconnection handler is triggered when an already connected client disconnects gracefully. You can build your disconnection handlers like this:

```csharp
using ENet;
using MHLab.AspNetCore.Enet.Handlers;
using Microsoft.Extensions.Logging;

public class DisconnectionHandler : IDisconnectionHandler
{
    private readonly ILogger<DisconnectionHandler> _logger;

    public DisconnectionHandler(ILogger<DisconnectionHandler> logger)
    {
        /* Here in the constructor you can take the full advantage of the 
        Microsoft Dependency Injection framework. */
        _logger = logger;
    }

    public void OnDisconnectedPeer(Peer peer)
    {
        _logger.Log(LogLevel.Debug, $"Client disconnected - ID: {peer.ID}, IP: {peer.IP}");
    }
}
```

## Timeout Handlers

A timeout handler is triggered when an already connected client disconnects ungracefully. You can build your timeout handlers like this:

```csharp
using ENet;
using MHLab.AspNetCore.Enet.Handlers;
using Microsoft.Extensions.Logging;

public class TimeoutHandler : ITimeoutHandler
{
    private readonly ILogger<TimeoutHandler> _logger;

    public TimeoutHandler(ILogger<TimeoutHandler> logger)
    {
        /* Here in the constructor you can take the full advantage of the 
        Microsoft Dependency Injection framework. */
        _logger = logger;
    }

    public void OnTimeoutPeer(Peer peer)
    {
        _logger.Log(LogLevel.Debug, $"Client timeout - ID: {peer.ID}, IP: {peer.IP}");
    }
}
```

## Packet Handlers

A packet handler is triggered when a message is received from a connected client. You can build your packet handlers like this:

```csharp
using ENet;
using MHLab.AspNetCore.Enet.Handlers;
using Microsoft.Extensions.Logging;

public class PacketHandler : IPacketHandler
{
    private readonly ILogger<PacketHandler> _logger;

    public PacketHandler(ILogger<PacketHandler> logger)
    {
        /* Here in the constructor you can take the full advantage of the 
        Microsoft Dependency Injection framework. */
        _logger = logger;
    }

    public void OnPacketReceived(Peer peer, byte channelId, Packet packet)
    {
        _logger.Log(LogLevel.Debug, $"Packet received from - ID: {peer.ID}, IP: {peer.IP}, Channel ID: {channelId}, Data length: {packet.Length}");
    }
}
```
