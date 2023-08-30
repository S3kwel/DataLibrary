using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Tests
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private readonly DatabaseSnapshotHelper _snapshotHelper;
        private const string SnapshotName = "TestDB_Snapshot";
        private const string LogicalName = "TestDB_Data";
        private const string SnapshotFilePath = "C:\\snapshots\\";

        public DatabaseFixture()
        {
            var connectionString = "Your SQL Server Connection String Here";
            _snapshotHelper = new DatabaseSnapshotHelper(connectionString);
        }

        public async Task InitializeAsync()
        {
            await Task.Run(() =>
            {
                // Create the database snapshot before tests run
                _snapshotHelper.CreateSnapshot(SnapshotName, LogicalName, SnapshotFilePath);
            });
        }

        public async Task DisposeAsync()
        {
            await Task.Run(() =>
            {
                // Restore and then drop the snapshot after tests run
                _snapshotHelper.RestoreFromSnapshot("TestDB", SnapshotName);
                _snapshotHelper.DropSnapshot(SnapshotName);
            });
        }
    }

}
