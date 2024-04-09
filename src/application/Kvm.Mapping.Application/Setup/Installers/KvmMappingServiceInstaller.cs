using Kvm.Mapping.Application.Http;
using Kvm.Mapping.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Kvm.Mapping.Application.Setup.Installers;

public static class KvmMappingServiceInstaller
{
    public static void InstallKvmMappingService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<HttpMessageHandlerWithRefreshToken>();

        services
            .AddRefitClient<IKvmMappingService>()
            .AddHttpMessageHandler<HttpMessageHandlerWithRefreshToken>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(configuration["Tms:Kvm:BaseUrl"]!));
    }
}
