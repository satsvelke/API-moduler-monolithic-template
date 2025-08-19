using System.Data;
using Dapper;
using Nucleus.Databases;
using Nucleus.Databases.Interfaces;
using Nucleus.Models;
using Nucleus.IRepository;

namespace Nucleus.Repository;

public class ValidationRepository : IValidationRepository
{
    private readonly IMainDatabaseContext mainDatabaseContext;

    public ValidationRepository(IMainDatabaseContext mainDatabaseContext)
    {
        this.mainDatabaseContext = mainDatabaseContext;
    }

    public async Task<IList<MessageElement>> Validate(DatbaseValidation datbaseValidation, CancellationToken cancellationToken)
    {

        ArgumentNullException.ThrowIfNull(datbaseValidation);

        ArgumentException.ThrowIfNullOrEmpty(datbaseValidation.StoredProcedure);

        using (var connection = mainDatabaseContext.CreateConnection(DatabaseKeys.Read))
        {
            var validations = await connection.QueryAsync<MessageElement>(new CommandDefinition(
                commandText: datbaseValidation.StoredProcedure
                , parameters: new { RequestPayload = datbaseValidation.RequestPayload }
                , commandType: CommandType.StoredProcedure
                , cancellationToken: cancellationToken
            )).ConfigureAwait(true);

            return validations.ToList();
        }
    }
}
