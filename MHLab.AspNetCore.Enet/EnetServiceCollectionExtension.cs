using MHLab.AspNetCore.Enet.Handlers;
using MHLab.AspNetCore.Enet.Servers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MHLab.AspNetCore.Enet
{
    public static class EnetServiceCollectionExtension
    {
        public static IServiceCollection AddEnet(this IServiceCollection services, Action<EnetConfiguration> configurator)
        {
            services.AddSingleton<IConnectionHandlerContainer, ConnectionHandlerContainer>();
            services.AddSingleton<IDisconnectionHandlerContainer, DisconnectionHandlerContainer>();
            services.AddSingleton<ITimeoutHandlerContainer, TimeoutHandlerContainer>();
            services.AddSingleton<IPacketHandlerContainer, PacketHandlerContainer>();

            var configuration = new EnetConfiguration(services);
            configurator.Invoke(configuration);

            services.AddSingleton(configuration);
            services.AddHostedService<EnetServer>();

            return services;
        }
    }
}
