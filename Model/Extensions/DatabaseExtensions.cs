using System.Data;
using System.Data.Common;

namespace Model.Extensions;

public static class DatabaseExtensions
{
    public static void CheckAndOpenDatabase(this DbConnection connection)
    {
        if (connection.State != ConnectionState.Open)
            connection.Open();
    }
}