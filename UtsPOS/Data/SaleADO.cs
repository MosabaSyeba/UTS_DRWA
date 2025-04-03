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
    public class SaleADO : ISale
    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;

        public SaleADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Models.Sale> GetSales()
        {
            List<Models.Sale> sales = new();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Sales";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sales.Add(new Models.Sale
                    {
                        SaleId = Convert.ToInt32(reader["SaleId"]),
                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                        SaleDate = Convert.ToDateTime(reader["SaleDate"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"])
                    });
                }
                return sales;
            }
        }

        public Models.Sale GetSaleById(int saleId)
        {
            Models.Sale sale = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Sales WHERE SaleId = @SaleId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SaleId", saleId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    sale = new Models.Sale
                    {
                        SaleId = Convert.ToInt32(reader["SaleId"]),
                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                        SaleDate = Convert.ToDateTime(reader["SaleDate"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"])
                    };
                }
            }
            return sale;
        }

        public Models.Sale AddSale(Models.Sale sale)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Sales (CustomerId, SaleDate, TotalAmount) VALUES (@CustomerId, @SaleDate, @TotalAmount)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerId", sale.CustomerId);
                cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate);
                cmd.Parameters.AddWithValue("@TotalAmount", sale.TotalAmount);
                conn.Open();
                cmd.ExecuteNonQuery();
                return sale;
            }
        }

        public Models.Sale UpdateSale(Models.Sale sale)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE Sales SET CustomerId = @CustomerId, SaleDate = @SaleDate, TotalAmount = @TotalAmount WHERE SaleId = @SaleId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SaleId", sale.SaleId);
                cmd.Parameters.AddWithValue("@CustomerId", sale.CustomerId);
                cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate);
                cmd.Parameters.AddWithValue("@TotalAmount", sale.TotalAmount);
                conn.Open();
                cmd.ExecuteNonQuery();
                return sale;
            }
        }

        public void DeleteSale(int saleId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM Sales WHERE SaleId = @SaleId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SaleId", saleId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}