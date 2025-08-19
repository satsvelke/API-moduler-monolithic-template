namespace Nucleus.Databases.Interfaces;

using System.Data;
using MySql.Data.MySqlClient;

public interface IMainDatabaseContext
{
     IDbConnection CreateConnection(string databaseKey);
}
