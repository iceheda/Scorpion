using Microsoft.Data.Sqlite;
using Scorpion.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Scorpion.Services
{
    public class ProductService
    {
        public static List<Product> GetListOfProducts()
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            var output = cnn.Query<Product>("SELECT * FROM Product", new DynamicParameters()).ToList();
            return output;
        }

        public static List<Product> GetProductListByWord(string str)//мб убрать нахуй? тупо сделаю в коде фильтрацию 
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            var output = cnn.Query<Product>($"SELECT * FROM Product WHERE NameOfProduct LIKE '%{str}%'", new DynamicParameters()).ToList();
            return output;
        }

        public static void SaveProduct(Product item)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute("INSERT INTO Product (NameOfProduct, Quantity, NotificationDay, DateOfIssue, ExpirationDate, Comment) values (@NameOfProduct, @Quantity, @NotificationDay, @DateOfIssue, @ExpirationDate, @Comment);",
                item);
        }

        public static void UpdateProduct(Product item)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute(
                "UPDATE Product SET NameOfProduct = @NameOfProduct, Quantity = @Quantity, NotificationDay = @NotificationDay, DateOfIssue = @DateOfIssue, ExpirationDate = @ExpirationDate, Comment = @Comment WHERE Id = @Id;",
                item);
        }

        public static void DeleteProduct(int id)
        {
            using var cnn = new SqliteConnection("Data Source=" + App.GetDatabasePath());
            cnn.Execute("DELETE FROM Product WHERE Id = " + id);
        }

    }
}