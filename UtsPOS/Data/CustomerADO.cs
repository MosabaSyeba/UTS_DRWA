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
    public class CustomerADO : ICustomer
    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;

        public CustomerADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Models.Customer> GetCustomers()
        {
            List<Models.Customer> customers = new();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Customers";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new Models.Customer
                    {
                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        ContactNumber = reader["ContactNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"].ToString()
                    });
                }
                return customers;
            }
        }

        public Models.Customer GetCustomerById(int customerId)
        {
            Models.Customer customer = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Customers WHERE CustomerId = @CustomerId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    customer = new Models.Customer
                    {
                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        ContactNumber = reader["ContactNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"].ToString()
                    };
                }
            }
            return customer;
        }

        public Models.Customer AddCustomer(Models.Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Customers (CustomerName, ContactNumber, Email, Address) VALUES (@CustomerName, @ContactNumber, @Email, @Address)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                cmd.Parameters.AddWithValue("@ContactNumber", customer.ContactNumber);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                conn.Open();
                cmd.ExecuteNonQuery();
                return customer;
            }
        }

        public Models.Customer UpdateCustomer(Models.Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE Customers SET CustomerName = @CustomerName, ContactNumber = @ContactNumber, Email = @Email, Address = @Address WHERE CustomerId = @CustomerId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                cmd.Parameters.AddWithValue("@ContactNumber", customer.ContactNumber);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                conn.Open();
                cmd.ExecuteNonQuery();
                return customer;
            }
        }

        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM Customers WHERE CustomerId = @CustomerId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}