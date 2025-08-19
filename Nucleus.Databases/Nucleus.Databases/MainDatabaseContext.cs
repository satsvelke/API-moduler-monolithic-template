using Nucleus.Databases.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Nucleus.Databases;


public class MainDatabaseContext : IMainDatabaseContext
{
    private readonly IConfiguration configuration;
    private readonly string? readConnectionString;
    private readonly string? writeConnectionString;


    public MainDatabaseContext(IConfiguration configuration)
    {
        this.configuration = configuration;
        readConnectionString = configuration.GetConnectionString("MainReadConnectionString");
        writeConnectionString = configuration.GetConnectionString("MainWriteConnectionString");
    }


    public IDbConnection CreateConnection(string databaseKey)
    {
        if (string.IsNullOrWhiteSpace(databaseKey)) throw new ArgumentNullException(nameof(databaseKey));

        var connectionString = databaseKey == DatabaseKeys.Write
        ? writeConnectionString : databaseKey == DatabaseKeys.Read
        ? readConnectionString : string.Empty;

        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        return new MySqlConnection(connectionString);
    }
}
