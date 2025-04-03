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
    public class SaleItemADO : ISaleItem
    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;

        public SaleItemADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Models.SaleItem> GetSaleItems()
        {
            List<Models.SaleItem> saleItems = new();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM SaleItems";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    saleItems.Add(new Models.SaleItem
                    {
                        SaleItemsId = Convert.ToInt32(reader["SaleItemsId"]),
                        SaleId = Convert.ToInt32(reader["SaleId"]),
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Price = Convert.ToDecimal(reader["Price"])
                    });
                }
                return saleItems;
            }
        }

        public Models.SaleItem GetSaleItemById(int saleItemId)
        {
            Models.SaleItem saleItem = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM SaleItems WHERE SaleItemsId = @SaleItemsId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SaleItemsId", saleItemId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    saleItem = new Models.SaleItem
                    {
                        SaleItemsId = Convert.ToInt32(reader["SaleItemsId"]),
                        SaleId = Convert.ToInt32(reader["SaleId"]),
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Price = Convert.ToDecimal(reader["Price"])
                    };
                }
            }
            return saleItem;
        }

        public Models.SaleItem AddSaleItem(Models.SaleItem saleItem)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO SaleItems (SaleItemsId, ProductId, Quantity, Price) VALUES (@SaleItemsId, @ProductId, @Quantity, @Price)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SaleItemsId", saleItem.SaleId);
                cmd.Parameters.AddWithValue("@ProductId", saleItem.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", saleItem.Quantity);
                cmd.Parameters.AddWithValue("@Price", saleItem.Price);
                conn.Open();
                cmd.ExecuteNonQuery();
                return saleItem;
            }
        }

        public Models.SaleItem UpdateSaleItem(Models.SaleItem saleItem)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE SaleItems SET SaleItemsId = @SaleItemsId, ProductId = @ProductId, Quantity = @Quantity, Price = @Price WHERE SaleItemsId = @SaleItemsId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SaleItemsId", saleItem.SaleItemsId);
                cmd.Parameters.AddWithValue("@SaleId", saleItem.SaleId);
                cmd.Parameters.AddWithValue("@ProductId", saleItem.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", saleItem.Quantity);
                cmd.Parameters.AddWithValue("@Price", saleItem.Price);
                conn.Open();
                cmd.ExecuteNonQuery();
                return saleItem;
            }
        }

        public void DeleteSaleItem(int saleItemId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM SaleItems WHERE SaleItemsId = @SaleItemsId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SaleItemsId", saleItemId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}