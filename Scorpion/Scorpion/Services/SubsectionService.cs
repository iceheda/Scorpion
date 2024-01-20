using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;
using Scorpion.Models;

namespace Scorpion.Services
{
    public class SubsectionService
    {
        public static List<Subsection> GetSomeSubsectionList(int id)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            var output = cnn
                .Query<Subsection>("SELECT * FROM Subsection WHERE SectionId = " + id, new DynamicParameters())
                .ToList();

            return output;
        }

        public static void SaveSubsection(Subsection item)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute("insert into Subsection (Name, SectionId) values (@Name, @SectionId) ", item);
        }

        public static void UpdateSubsection(Subsection item)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute("UPDATE Subsection SET Name = @Name WHERE Id = @Id;", item);
        }

        public static void DeleteSubsection(Subsection item)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute("DELETE FROM Subsection WHERE Id = @Id", item);
        }
    }
}