using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region Mapping Dependency

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            #endregion

            return services;
        }
    }
}
