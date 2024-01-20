using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;
using Scorpion.Models;

namespace Scorpion.Services
{
    public class SectionService
    {
        public static List<Section> GetItemList()
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            var output = cnn.Query<Section>("SELECT * FROM Section", new DynamicParameters());

            return output.ToList();
        }

        public static void SaveSection(Section item)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute("INSERT INTO Section (Name) VALUES (@Name)", item);
        }

        public static void UpdateSection(Section item)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute("UPDATE Section SET Name = @Name WHERE Id = @Id;", item);
        }

        public static void DeleteSection(Section item)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute("DELETE FROM Section WHERE Id = @Id", item);
        }
    }
}