using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;
using Scorpion.Models;

namespace Scorpion.Services
{
    public class ArticleService
    {
        public static List<Article> GetSomeArticleList(int id)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            var output = cnn.Query<Article>("SELECT * FROM Article WHERE SubsectionId = " + id, new DynamicParameters()).ToList();
            return output;
        }

        public static List<Article> GetArticleListByWord(string str)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            var output = cnn.Query<Article>("SELECT * FROM Article WHERE FullDescription LIKE '%" + str + "%'", new DynamicParameters()).ToList();
            return output;
        }

        public static void SaveArticle(Article item)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute(
                "insert into Article (Name, ShortDescription, FullDescription, SubsectionId) values (@Name, @ShortDescription, @FullDescription, @SubsectionId) ",
                item);
        }

        public static void UpdateArticle(Article item)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute(
                "UPDATE Article SET Name = @Name, ShortDescription = @ShortDescription, FullDescription = @FullDescription WHERE Id = @Id;",
                item);
        }

        public static void DeleteArticle(Article item)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute("DELETE FROM Article WHERE Id = @Id", item);
        }

        public static List<ArticleWithPhoto> GetArticlesWithPhoto(int id)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            var output = cnn.Query<Article>("SELECT * FROM Article WHERE SubsectionId = " + id, new DynamicParameters()).ToList();
            var list = new List<ArticleWithPhoto>();
            foreach (var item in output)
            {
                list.Add(new ArticleWithPhoto { Photos = PhotoService.GetImagesByArticleId(item.Id), Article = item });
            }
            return list;
        }
    }
}