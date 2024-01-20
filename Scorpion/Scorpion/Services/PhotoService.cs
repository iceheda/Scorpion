using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Scorpion.Models;

namespace Scorpion.Services
{
    public class PhotoService
    {
        public static List<Photo> GetImagesByArticleId(int id)
        {
            var result = new List<Photo>();

            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            using var command = new SqliteCommand("", cnn);
            cnn.Open();

            command.CommandText = "SELECT * FROM Photo WHERE ArticleId = " + id;
            var reader = command.ExecuteReader();

            while (reader.Read())
                result.Add(new Photo
                {
                    Id = reader.GetInt32(0),
                    PhotoBlob = (byte[])reader["PhotoBlob"],
                    ArticleId = reader.GetInt32(2)
                });
            //var output = cnn.Query<object>("SELECT * FROM Photo WHERE ArticleId = " + id, new DynamicParameters()).ToList();
            //result = output.Select(x => x.);
            cnn.Close();

            return result;
        }

        public static Task SavePhoto(Photo item)
        {
            try
            {
                using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());

                if (cnn.State != ConnectionState.Open)
                    cnn.Open();

                var query = "INSERT INTO Photo (PhotoBlob, ArticleId) values (@p1, @p2)";
                using var command = cnn.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add("p1", SqliteType.Blob).Value = item.PhotoBlob;
                command.Parameters.Add("p2", SqliteType.Integer).Value = item.ArticleId;

                command.ExecuteNonQuery();
            }
            catch
            {
                
            }
            return Task.CompletedTask;
        }

        public static void DeletePhoto(Photo item)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute("DELETE FROM Photo WHERE Id = @Id", item);
        }


        public static Photo GetFirstArticleImage(int id)
        {
            var result = new Photo();

            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            using var command = new SqliteCommand("", cnn);
            cnn.Open();

            command.CommandText = $"SELECT * FROM Photo WHERE ArticleId={id} ORDER BY ArticleId ASC LIMIT 1";
            var reader = command.ExecuteReader();

            while (reader.Read())
                result = new Photo
                {
                    Id = reader.GetInt32(0),
                    PhotoBlob = (byte[])reader["PhotoBlob"],
                    ArticleId = reader.GetInt32(2)
                };
            //var output = cnn.Query<object>("SELECT * FROM Photo WHERE ArticleId = " + id, new DynamicParameters()).ToList();
            //result = output.Select(x => x.);
            cnn.Close();

            return result;
        }
    }
}