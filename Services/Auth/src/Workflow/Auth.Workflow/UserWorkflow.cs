using System.Globalization;
using Auth.Dtos;
using Auth.IRepository;
using Auth.IWorkflow;
using Auth.Workflow.Mappers;
using Microsoft.Extensions.Options;
using Nucleus.Api.JwtConfig;
using Nucleus.Models;
using Nucleus.Utilities;

namespace Auth.Workflow;

public class UserWorkflow : IUserWorkflow
{
    private readonly IUserRepository userRepository;
    private readonly IJwtToken jwtToken;
    private readonly IOptions<JwtSettings> jwtSettings;

    public UserWorkflow(IUserRepository userRepository, IJwtToken jwtToken, IOptions<JwtSettings> jwtSettings)
    {
        this.userRepository = userRepository;
        this.jwtToken = jwtToken;
        this.jwtSettings = jwtSettings;
    }

    public async Task<UserLoginResponseDto?> Verify(UserLoginRequestDto request, CancellationToken cancellationToken)
    {

        ArgumentNullException.ThrowIfNull(request);

        var userRequest = new UserLoginMapper().ToUser(request);

        var userDetails = await userRepository.VerifyUser(userRequest, cancellationToken).ConfigureAwait(true);

        if (userDetails is null) return null;

        var token = await jwtToken.CreateToken(new JwtClaims()
        {
            Email = userDetails.Email,
            FirstName = userDetails.FirstName,
            LastName = userDetails.LastName,
            RoleName = userDetails.RoleName,
            RoleId = userDetails.RoleId,
            StaffId = userDetails.StaffId.ToString(CultureInfo.InvariantCulture),
            CompanyId = userDetails.CompanyId,
            JwtSettings = jwtSettings.Value
        });

        if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException(token);

        return new UserLoginResponseDto()
        {
            AccessToken = token,
            UserDetails = new UserLoginDetailsResponseDto()
            {
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                RoleName = userDetails.RoleName?.Encrypt(),
                RoleId = userDetails.RoleId.ToString(CultureInfo.InvariantCulture).Encrypt(),
                StaffId = userDetails.StaffId.ToString(CultureInfo.InvariantCulture).Encrypt(),
                CompanyId = userDetails.CompanyId.ToString(CultureInfo.InvariantCulture).Encrypt()
            }
        };
    }
}
