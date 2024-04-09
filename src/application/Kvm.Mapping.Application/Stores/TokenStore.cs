namespace Kvm.Mapping.Application.Stores;

public class TokenStore
{
    public string? AccessToken { get; set; }
    public DateTime ExpiresIn { get; set; }
}
