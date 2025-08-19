using Auth.Models;

namespace Auth.IRepository;

public interface IUserRepository
{
    public Task<User?> VerifyUser(User userRequest, CancellationToken cancellationToken);
}
