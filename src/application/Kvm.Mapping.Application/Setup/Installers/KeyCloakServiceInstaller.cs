using Kvm.Mapping.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Kvm.Mapping.Application.Setup.Installers;

public static class KeyCloakServiceInstaller
{
    public static void InstallKeyCloakService(this IServiceCollection services, IConfiguration configuration) => services
        .AddRefitClient<IKeyCloakService>()
        .ConfigureHttpClient(client =>
        {
            client.BaseAddress = new Uri(configuration["KeyCloak:BaseUrl"]!);
        });
}
