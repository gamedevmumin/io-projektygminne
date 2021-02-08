using Microsoft.Extensions.DependencyInjection;
using System;

namespace VotingSystem.CronJobs
{
    public static class IServiceCollectionCronJobExtensions
    {
        public static IServiceCollection AddCronJob<T>(
            this IServiceCollection services,
            Action<CronServiceOptions<T>> configAction) where T : CronServiceBase
        {
            var options = new CronServiceOptions<T>();
            configAction.Invoke(options);

            services.AddSingleton(options);
            services.AddHostedService<T>();

            return services;
        }
    }
}
