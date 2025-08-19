using Nucleus.Models;

namespace Nucleus.Api.JwtConfig;

public interface IJwtToken
{
    Task<string> CreateToken(JwtClaims jwtClaims);

    Task<bool> ValidateToken(JwtSettings jwtSettings, string token);

    Task<JwtClaims?> GetClaims(JwtSettings jwtSettings, string token);
}
