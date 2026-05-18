using Microsoft.Data.Sqlite;
using BlazorApp2.Models;

namespace BlazorApp2.Services
{
    public class ProductDbService
    {
        private readonly string _connectionString = "Data Source=Data/grocery.db";

        public List<Product> GetProductsByCategory(int categoryId)
        {
            var products = new List<Product>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                SELECT Id, Name, Description, Price, ImageUrl,
                       CategoryId, CategoryName,
                       ExpiryDate, IsOnSale, DiscountPercentage
                FROM Products
                WHERE CategoryId = $categoryId
            ";
            cmd.Parameters.AddWithValue("$categoryId", categoryId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    ImageUrl = reader.GetString(4),
                    CategoryId = reader.GetInt32(5),
                    CategoryName = reader.GetString(6),
                    ExpiryDate = DateTime.Parse(reader.GetString(7)),
                    IsOnSale = reader.GetInt32(8) == 1,
                    DiscountPercentage = reader.GetInt32(9)
                });
            }

            return products;
        }
        public List<Product> GetAllProducts()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Products";
            var reader = command.ExecuteReader();

            var products = new List<Product>();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    ImageUrl = reader.GetString(4),
                    CategoryId = reader.GetInt32(5),
                    CategoryName = reader.GetString(6),
                    ExpiryDate = DateTime.Parse(reader.GetString(7)),
                    IsOnSale = reader.GetInt32(8) == 1,
                    DiscountPercentage = reader.GetInt32(9)
                });
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                SELECT Id, Name, Description, Price, ImageUrl,
                       CategoryId, CategoryName,
                       ExpiryDate, IsOnSale, DiscountPercentage
                FROM Products
                WHERE Id = $id
            ";
            cmd.Parameters.AddWithValue("$id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    ImageUrl = reader.GetString(4),
                    CategoryId = reader.GetInt32(5),
                    CategoryName = reader.GetString(6),
                    ExpiryDate = DateTime.Parse(reader.GetString(7)),
                    IsOnSale = reader.GetInt32(8) == 1,
                    DiscountPercentage = reader.GetInt32(9)
                };
            }

            return null;
        }
    }
}
