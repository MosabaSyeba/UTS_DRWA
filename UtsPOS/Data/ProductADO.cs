using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using UtsPos.Models;

namespace UtsPOS.Data
{
    public class ProductADO : IProduct
    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;

        public ProductADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Models.Product> GetProducts()
        {
            List<Models.Product> products = new();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Models.Product
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = reader["ProductName"].ToString(),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        Description = reader["Description"].ToString()
                    });
                }
                return products;
            }
        }

        public Models.Product GetProductById(int productId)
        {
            Models.Product product = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Products WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    product = new Models.Product
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = reader["ProductName"].ToString(),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        Description = reader["Description"].ToString()
                    };
                }
            }
            return product;
        }

        public Models.Product AddProduct(Models.Product product)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Products (ProductName, CategoryId, Price, StockQuantity, Description) VALUES (@ProductName, @CategoryId, @Price, @StockQuantity, @Description)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                conn.Open();
                cmd.ExecuteNonQuery();
                return product;
            }
        }

        public Models.Product UpdateProduct(Models.Product product)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE Products SET ProductName = @ProductName, CategoryId = @CategoryId, Price = @Price, StockQuantity = @StockQuantity, Description = @Description WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                conn.Open();
                cmd.ExecuteNonQuery();
                return product;
            }
        }

        public void DeleteProduct(int productId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM Products WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}