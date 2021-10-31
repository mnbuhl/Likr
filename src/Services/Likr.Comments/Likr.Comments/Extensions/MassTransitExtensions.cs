using Likr.Comments.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Likr.Comments.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<LikeCreatedConsumer>();
                configure.AddConsumer<LikeDeletedConsumer>();
                configure.AddConsumer<CommentUserCreatedConsumer>();

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