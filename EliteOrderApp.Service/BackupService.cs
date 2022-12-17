using EliteOrderApp.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace EliteOrderApp.Service
{
    public class BackupService
    {
        private readonly string _connectionString = "";
        private readonly string _backupFolderFullPath = "";
        private readonly string[] _systemDatabaseNames = { "master", "tempdb", "model", "msdb" };
        public BackupService(string connectionString, string backupFolderFullPath)
        {
            _connectionString = connectionString;
            _backupFolderFullPath = backupFolderFullPath;
        }

        public void BackupAllUserDatabases()
        {
            foreach (string databaseName in GetAllUserDatabases())
            {
                BackupDatabase(databaseName);
            }
        }

        public void BackupDatabase(string databaseName)
        {
            using var connection = new SqlConnection(_connectionString);
            var filePath = BuildBackupPathWithFilename(databaseName);
            var server = new Server(new ServerConnection(connection));
            var dbBackup = new Backup()
            {
                Action = BackupActionType.Database,
                Database = databaseName,
                
            };
            dbBackup.Devices.AddDevice(filePath, DeviceType.File);
            dbBackup.Initialize = true;
            dbBackup.SqlBackupAsync(server);
        }
        public async Task RestoreDatabase(string databaseName)
        {
            await using var connection = new SqlConnection(_connectionString);
            var server = new Server(new ServerConnection(connection));
            var dbRestore = new Restore()
            {
                Database = databaseName,
                Action = RestoreActionType.Database,
                NoRecovery = false,
                ReplaceDatabase = true,
            };
            server.KillAllProcesses(databaseName);
            var filePath = BuildBackupPathWithFilename(databaseName);
            dbRestore.Devices.AddDevice(filePath, DeviceType.File);
            dbRestore.SqlRestoreAsync(server);
            
        }
        private IEnumerable<string> GetAllUserDatabases()
        {
            var databases = new List<string>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var databasesTable = connection.GetSchema("Databases");

            connection.Close();

            foreach (DataRow row in databasesTable.Rows)
            {
                string databaseName = row["database_name"].ToString();

                if (_systemDatabaseNames.Contains(databaseName))
                    continue;

                databases.Add(databaseName);
            }

            return databases;
        }

        private string BuildBackupPathWithFilename(string databaseName)
        {
            var filename = $"{databaseName}.bak";
            return Path.Combine(_backupFolderFullPath, filename);
        }

    }
}
