using Kvm.Mapping.Application.Services;
using Kvm.Mapping.Application.Stores;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace Kvm.Mapping.Application.Http;

public class HttpMessageHandlerWithRefreshToken : DelegatingHandler
{
    private readonly TokenStore _tokenStore;
    private readonly IConfiguration _configuration;
    private readonly IKeyCloakService _keyCloakService;

    public HttpMessageHandlerWithRefreshToken(
        TokenStore tokenStore,
        IConfiguration configuration,
        IKeyCloakService keyCloakService)
    {
        _tokenStore = tokenStore;
        _configuration = configuration;
        _keyCloakService = keyCloakService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (IsAccessTokenExpired())
        {
            await GetAccessTokenAsync();
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStore.AccessToken);
        request.Headers.Add("Ocp-Apim-Subscription-Key", _configuration["Tms:ApimSubscription"]);

        return await base.SendAsync(request, cancellationToken);
    }

    private bool IsAccessTokenExpired() => _tokenStore.ExpiresIn <= DateTime.UtcNow;

    private async Task GetAccessTokenAsync()
    {
        string user = _configuration["KeyCloak:User"]!;
        string secret = _configuration["KeyCloak:Secret"]!;

        var keyCloakResponse = await _keyCloakService.GetToken(new(user, secret));
        _tokenStore.AccessToken = keyCloakResponse.AccessToken;
        _tokenStore.ExpiresIn = DateTime.UtcNow.AddSeconds(keyCloakResponse.ExpiresIn);
    }
}
