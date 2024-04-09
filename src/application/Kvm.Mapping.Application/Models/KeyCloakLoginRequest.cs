using System.Text.Json.Serialization;

namespace Kvm.Mapping.Application.Models;

public class KeyCloakLoginRequest
{
    public KeyCloakLoginRequest(string clientId, string clientSecret)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
    }

    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }

    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; set; }

    [JsonPropertyName("grant_type")]
    public string GrantType { get; } = "client_credentials";

    public FormUrlEncodedContent AsFormUrlEncodedContent()
    {
        var contentList = new List<KeyValuePair<string, string>>
        {
            new("client_id", ClientId),
            new("client_secret", ClientSecret),
            new("grant_type", GrantType)
        };

        return new(contentList);
    }
}
