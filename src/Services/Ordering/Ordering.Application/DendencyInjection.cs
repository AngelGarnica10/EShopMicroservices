﻿using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    public static class DendencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //});
            return services;
        }
    }
}
