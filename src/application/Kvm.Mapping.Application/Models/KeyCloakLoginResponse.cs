using System.Text.Json.Serialization;

namespace Kvm.Mapping.Application.Models;

public class KeyCloakLoginResponse
{
    public KeyCloakLoginResponse(string accessToken, int expiresIn)
    {
        AccessToken = accessToken;
        ExpiresIn = expiresIn;
    }

    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}
