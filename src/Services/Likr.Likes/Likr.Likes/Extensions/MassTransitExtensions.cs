using Likr.Likes.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Likr.Likes.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            services.AddMassTransit(configure =>
            {
                //configure.AddConsumer<CommentCreatedConsumer>();
                //configure.AddConsumer<CommentDeletedConsumer>();
                configure.AddConsumer<PostCreatedConsumer>();
                configure.AddConsumer<PostDeletedConsumer>();
                
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