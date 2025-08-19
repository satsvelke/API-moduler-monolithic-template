using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Nucleus.Models;
using Nucleus.Utilities;

namespace Nucleus.Api.JwtConfig;

public class JwtToken : IJwtToken
{
    public async Task<string> CreateToken(JwtClaims jwtClaims)
    {
        return await Task.Run(() =>
                    {
                        ArgumentNullException.ThrowIfNull(jwtClaims);

                        ArgumentNullException.ThrowIfNull(jwtClaims.JwtSettings);

                        ArgumentException.ThrowIfNullOrWhiteSpace(jwtClaims.JwtSettings.Secret);

                        ArgumentException.ThrowIfNullOrWhiteSpace(jwtClaims.Email);

                        ArgumentException.ThrowIfNullOrWhiteSpace(jwtClaims.StaffId);

                        ArgumentException.ThrowIfNullOrWhiteSpace(jwtClaims.RoleName);

                        if (jwtClaims.RoleId == 0) throw new ArgumentException(nameof(jwtClaims.RoleId));
                        if (jwtClaims.CompanyId == 0) throw new ArgumentException(nameof(jwtClaims.CompanyId));


                        var key = Encoding.ASCII.GetBytes(jwtClaims.JwtSettings.Secret);

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[]
                            {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim("StaffId", jwtClaims.StaffId.Encrypt()),
                            new Claim("Email", jwtClaims.Email.Encrypt()),
                            new Claim("FirstName", jwtClaims?.FirstName!),
                            new Claim("LastName", jwtClaims?.LastName!),
                            new Claim("RoleName", jwtClaims!.RoleName.Encrypt()),
                            new Claim("RoleId", jwtClaims.RoleId.ToString(CultureInfo.InvariantCulture).Encrypt()),
                            new Claim("CompanyId", jwtClaims.CompanyId.ToString(CultureInfo.InvariantCulture).Encrypt()),
                            new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                            Expires = DateTime.UtcNow.AddMinutes(jwtClaims.JwtSettings.Expiry),
                            Issuer = jwtClaims.JwtSettings.Issuer,
                            Audience = jwtClaims.JwtSettings.Audience,

                            SigningCredentials = new SigningCredentials
                            (new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha512Signature)
                        };
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var jwtToken = tokenHandler.WriteToken(token);
                        return jwtToken!;
                    }).ConfigureAwait(false);
    }

    public async Task<bool> ValidateToken(JwtSettings jwtSettings, string token)
    {
        return await Task.Run(() =>
         {
             try
             {
                 ArgumentException.ThrowIfNullOrWhiteSpace(token);

                 var tokenHandler = new JwtSecurityTokenHandler();
                 TokenValidationParameters parameters = GetValidationParameters(jwtSettings);

                 SecurityToken? securityToken;

                 var principle = tokenHandler.ValidateToken(token, parameters, out securityToken);

                 return true;

             }
             catch (Exception)
             {
                 return false;
                 throw;
             }

         }).ConfigureAwait(false);
    }


    private static TokenValidationParameters GetValidationParameters(JwtSettings jwtSettings)
    {
        return new TokenValidationParameters()
        {
            ValidIssuer = jwtSettings?.Issuer,
            ValidAudience = jwtSettings?.Audience,
            IssuerSigningKey = new SymmetricSecurityKey
                                (Encoding.UTF8.GetBytes(jwtSettings?.Secret!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    }

    public async Task<JwtClaims?> GetClaims(JwtSettings jwtSettings, string token)
    {
        return await Task.Run(() =>
        {
            ArgumentException.ThrowIfNullOrEmpty(token);

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtToken = tokenHandler.ReadToken(token);

            var key = Encoding.ASCII.GetBytes(jwtSettings?.Secret!);

            var parameters = GetValidationParameters(jwtSettings!);

            SecurityToken securityToken;
            var principle = tokenHandler.ValidateToken(token, parameters, out securityToken);

            // Extract claims from the validated principle
            var claimsIdentity = principle.Identity as ClaimsIdentity;

            if (claimsIdentity is null) return null;

            // Map claims to JwtClaims class
            var jwtClaims = new JwtClaims
            {
                FirstName = claimsIdentity.FindFirst("FirstName")?.Value,
                LastName = claimsIdentity.FindFirst("LastName")?.Value,
                Email = claimsIdentity.FindFirst("Email")?.Value.Decrypt(),
                RoleName = claimsIdentity.FindFirst("RoleName")?.Value.Decrypt(),
                RoleId = Convert.ToInt32(claimsIdentity.FindFirst("RoleId")?.Value.Decrypt(), CultureInfo.InvariantCulture),
                StaffId = claimsIdentity.FindFirst("StaffId")?.Value.Decrypt(),
                CompanyId = Convert.ToInt64(claimsIdentity.FindFirst("CompanyId")?.Value.Decrypt(), CultureInfo.InvariantCulture),
                JwtSettings = jwtSettings
            };

            return jwtClaims;

        }).ConfigureAwait(true);
    }
}
