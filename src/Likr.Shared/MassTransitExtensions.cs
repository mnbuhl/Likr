using System;
using System.Reflection;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Likr.Shared
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumers(Assembly.GetEntryAssembly());
                
                configure.UsingRabbitMq((context, configurator) =>
                {
                    var configuration = context.GetRequiredService<IConfiguration>();
                    string serviceName = configuration.GetValue<string>("ServiceName");
                    string rabbitMqHost = configuration.GetValue<string>("RabbitMq");
                    
                    configurator.Host(rabbitMqHost);
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceName, false));
                    configurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
                });
                
            });

            services.AddMassTransitHostedService();
            
            return services;
        }
    }
}
