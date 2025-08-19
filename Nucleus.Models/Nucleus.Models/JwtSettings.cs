namespace Nucleus.Models;

public record JwtSettings
{
    public string? Secret { get; init; }
    public string? Audience { get; init; }
    public string? Issuer { get; init; }
    public int Expiry { get; init; }
}
