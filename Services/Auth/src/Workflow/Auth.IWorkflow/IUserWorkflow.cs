using Auth.Dtos;

namespace Auth.IWorkflow;

public interface IUserWorkflow
{
    Task<UserLoginResponseDto?> Verify(UserLoginRequestDto request, CancellationToken cancellationToken);
}
