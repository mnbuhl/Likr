using System;
using System.Reflection;
using GreenPipes;
using Likr.Comments.Commands;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Likr.Posts.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<CommentCreatedConsumer>();
                configure.AddConsumer<CommentDeletedConsumer>();

                configure.UsingRabbitMq((context, cfg) =>
                {
                    var configuration = context.GetRequiredService<IConfiguration>();
                    string rabbitMqHost = configuration.GetValue<string>("RabbitMq");
                    cfg.Host(rabbitMqHost);
                    
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddMassTransitHostedService();
            
            return services;
        }
    }
}