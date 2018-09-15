using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Yow.Api.Configurations
{
    public static class AutoMapper {
        internal static void RegisterAutoMapper (IServiceCollection services) {
            services.AddAutoMapper ();
        }
    }
}