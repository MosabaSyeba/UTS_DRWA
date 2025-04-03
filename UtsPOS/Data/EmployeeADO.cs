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
    public class EmployeeADO : IEmployee
    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;

        public EmployeeADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Models.Employee> GetEmployees()
        {
            List<Models.Employee> employees = new();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Employees";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employees.Add(new Models.Employee
                    {
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        EmployeeName = reader["EmployeeName"].ToString(),
                        ContactNumber = reader["ContactNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        Position = reader["Position"].ToString()
                    });
                }
                return employees;
            }
        }

        public Models.Employee GetEmployeeById(int employeeId)
        {
            Models.Employee employee = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    employee = new Models.Employee
                    {
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        EmployeeName = reader["EmployeeName"].ToString(),
                        ContactNumber = reader["ContactNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        Position = reader["Position"].ToString()
                    };
                }
            }
            return employee;
        }

        public Models.Employee AddEmployee(Models.Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Employees (EmployeeName, ContactNumber, Email, Position) VALUES (@EmployeeName, @ContactNumber, @Email, @Position)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@ContactNumber", employee.ContactNumber);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                conn.Open();
                cmd.ExecuteNonQuery();
                return employee;
            }
        }

        public Models.Employee UpdateEmployee(Models.Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE Employees SET EmployeeName = @EmployeeName, ContactNumber = @ContactNumber, Email = @Email, Position = @Position WHERE EmployeeId = @EmployeeId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@ContactNumber", employee.ContactNumber);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                conn.Open();
                cmd.ExecuteNonQuery();
                return employee;
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}