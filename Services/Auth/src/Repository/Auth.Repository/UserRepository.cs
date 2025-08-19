using System.Data;
using Auth.IRepository;
using Auth.Models;
using Dapper;
using Nucleus.Databases;
using Nucleus.Databases.Interfaces;

namespace Auth.Repository;

public class UserRepository : IUserRepository
{

    private readonly IMainDatabaseContext mainDatabaseContext;

    public UserRepository(IMainDatabaseContext mainDatabaseContext)
    {
        this.mainDatabaseContext = mainDatabaseContext;
    }

    public async Task<User?> VerifyUser(User userRequest, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userRequest);

        using (var connection = mainDatabaseContext.CreateConnection(DatabaseKeys.Read))
        {
            var user = await connection.QueryFirstOrDefaultAsync<User>(new CommandDefinition(
                commandText: "sp_user_verifyuser"
                , parameters: new { Email = userRequest.Email, Password = userRequest.Password }
                , commandType: CommandType.StoredProcedure
                , cancellationToken: cancellationToken
            ));

            return user;
        }
    }
}
