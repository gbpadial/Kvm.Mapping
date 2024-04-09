using Kvm.Mapping.Application.Models;
using Refit;

namespace Kvm.Mapping.Application.Services;

public interface IKeyCloakService
{
    [Post("/openid-connect/token")]
    Task<KeyCloakLoginResponse> GetToken([Body(BodySerializationMethod.UrlEncoded)] KeyCloakLoginRequest request);
}
