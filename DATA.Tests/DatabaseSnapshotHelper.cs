using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Tests
{
    using System.Data.SqlClient;

    public class DatabaseSnapshotHelper
    {
        private readonly string _connectionString;

        public DatabaseSnapshotHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateSnapshot(string snapshotName, string logicalName, string snapshotFilePath)
        {
            var createSnapshotSql = $@"
CREATE DATABASE {snapshotName} ON
(
    NAME = {logicalName},
    FILENAME = '{snapshotFilePath}'
) AS SNAPSHOT OF TestDB;";

            ExecuteNonQuery(createSnapshotSql);
        }

        public void RestoreFromSnapshot(string databaseName, string snapshotName)
        {
            var restoreFromSnapshotSql = $@"
USE master;
ALTER DATABASE {databaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE {databaseName} FROM DATABASE_SNAPSHOT = '{snapshotName}';
ALTER DATABASE {databaseName} SET MULTI_USER;";

            ExecuteNonQuery(restoreFromSnapshotSql);
        }

        public void DropSnapshot(string snapshotName)
        {
            var dropSnapshotSql = $"DROP DATABASE {snapshotName};";

            ExecuteNonQuery(dropSnapshotSql);
        }

        private void ExecuteNonQuery(string commandText)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand(commandText, connection);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

}
