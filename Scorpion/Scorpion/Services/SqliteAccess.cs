using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Scorpion.Services
{
    public class SqliteAccess
    {
        public static void CreateDb()
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Open();

            var command = cnn.CreateCommand();
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS [Section]
                (
                [Id]    INTEGER NOT NULL UNIQUE,
                [Name]  TEXT NOT NULL,
                PRIMARY KEY([Id] AUTOINCREMENT)
                );

            CREATE TABLE IF NOT EXISTS [Subsection] 
                (
                [Id]    INTEGER NOT NULL UNIQUE,
                [Name]  TEXT NOT NULL,
                [SectionId] INTEGER NOT NULL,
                PRIMARY KEY([Id] AUTOINCREMENT),
                FOREIGN KEY([SectionId]) REFERENCES [Section]([Id]) ON DELETE CASCADE
                );

            CREATE TABLE IF NOT EXISTS [Article] 
                (
                [Id]    INTEGER NOT NULL UNIQUE,
                [Name]  TEXT NOT NULL,
                [ShortDescription]  TEXT NOT NULL,
                [FullDescription]   TEXT NOT NULL,
                [SubsectionId] INTEGER NOT NULL,
                PRIMARY KEY([Id] AUTOINCREMENT),
                FOREIGN KEY([SubsectionId]) REFERENCES [Subsection]([Id]) ON DELETE CASCADE
                );

            CREATE TABLE IF NOT EXISTS [Photo] 
                (
                [Id]    INTEGER NOT NULL UNIQUE, 
                [Photo] BLOB NOT NULL,
                [ArticleId] INTEGER NOT NULL,
                PRIMARY KEY([Id] AUTOINCREMENT),
                FOREIGN KEY([ArticleId]) REFERENCES [Article]([Id]) ON DELETE CASCADE
                ); 

	        CREATE TABLE IF NOT EXISTS [Product] 
		        (
		        [Id]    INTEGER NOT NULL UNIQUE,
		        [NameOfProduct] TEXT NOT NULL,
		        [Quantity]      INTEGER NOT NULL DEFAULT 1,
		        [DateOfIssue]   TEXT NOT NULL,
		        [ExpirationDate]        TEXT NOT NULL,
		        [NotificationDay]       INTEGER NOT NULL,
		        [Comment]     TEXT,
		        PRIMARY KEY([Id] AUTOINCREMENT)
		        ) ";
            command.ExecuteNonQuery();

            cnn.Close();
        }
        public static async Task CheckAccessToStorage()
        {
            try
            {
                while (true)
                {
                    //var status = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>(); пермищионсы древние. удалить после теста эту говну
                    var statusReadStorage = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                    var statusWriteStorage = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

                    if (statusWriteStorage != PermissionStatus.Granted || statusReadStorage != PermissionStatus.Granted)
                    {
                        statusReadStorage = await Permissions.RequestAsync<Permissions.StorageRead>();
                        statusWriteStorage = await Permissions.RequestAsync<Permissions.StorageWrite>();
                    }

                    if (statusReadStorage == PermissionStatus.Granted && statusWriteStorage == PermissionStatus.Granted) 
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Application.Current.Quit();
            }
        }
        public static void FillDb()
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute("insert into Section (Name) values ('Раздел')");
            cnn.Execute("insert into Subsection (Name, SectionId) values ('Подраздел', '1')");
            cnn.Execute(
                "insert into Article (Name, ShortDescription, FullDescription, SubsectionId) values ('Название статьи', 'Кратко о статье', 'Полностью статья', '1')");
        }
    }
}