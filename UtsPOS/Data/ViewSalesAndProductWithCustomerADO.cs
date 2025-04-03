using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using UtsPOS.Models;

namespace UtsPOS.Data
{
    public class ViewSalesAndProductWithCustomerADO : IViewSalesAndProductWithCustomer
    {
        private readonly IConfiguration _configuration;
        private string connStr;

        public ViewSalesAndProductWithCustomerADO(IConfiguration configuration)
        {
            _configuration = configuration;
            this.connStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<ViewSalesAndProductWithCustomer> GetViewComplite()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"
SELECT        dbo.Sales.SaleDate, dbo.Sales.SaleId, dbo.Sales.TotalAmount, dbo.SaleItems.SaleItemsId, dbo.SaleItems.Quantity, dbo.SaleItems.Price, dbo.Products.ProductName, dbo.Products.Description, dbo.Categories.CategoryId, 
                         dbo.Categories.CategoryName, dbo.Customers.CustomerId AS CustomersId, dbo.Customers.CustomerName AS CustomersName, dbo.Customers.ContactNumber AS ContactNumbers, dbo.Customers.Email, 
                         dbo.Customers.Address, dbo.Products.ProductId AS ProductsId
FROM            dbo.Sales INNER JOIN
                         dbo.SaleItems ON dbo.Sales.SaleId = dbo.SaleItems.SaleId INNER JOIN
                         dbo.Products ON dbo.SaleItems.ProductId = dbo.Products.ProductId INNER JOIN
                         dbo.Categories ON dbo.Products.CategoryId = dbo.Categories.CategoryId INNER JOIN
                         dbo.Customers ON dbo.Sales.CustomerId = dbo.Customers.CustomerId";

                SqlCommand cmd = new SqlCommand(strSql, conn);
                List<ViewSalesAndProductWithCustomer> salesList = new List<ViewSalesAndProductWithCustomer>();

                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ViewSalesAndProductWithCustomer saleDetail = new ViewSalesAndProductWithCustomer()
                        {
                            CustomerId = Convert.ToInt32(dr["CustomersId"]),
                            CustomerName = dr["CustomersName"].ToString(),
                            ContactNumber = dr["ContactNumbers"].ToString(),
                            Email = dr["Email"].ToString(),
                            Address = dr["Address"].ToString(),
                            SaleId = Convert.ToInt32(dr["SaleId"]),
                            SaleDate = Convert.ToDateTime(dr["SaleDate"]),
                            TotalAmount = Convert.ToDecimal(dr["TotalAmount"]),
                            SaleItemId = Convert.ToInt32(dr["SaleItemsId"]),
                            Quantity = Convert.ToInt32(dr["Quantity"]),
                            SalePrice = Convert.ToDecimal(dr["Price"]),
                            ProductId = Convert.ToInt32(dr["ProductsId"]),
                            ProductName = dr["ProductName"].ToString(),
                            CategoryId = Convert.ToInt32(dr["CategoryId"]),
                            CategoryName = dr["CategoryName"].ToString()
                        };
                        salesList.Add(saleDetail);
                    }
                    return salesList;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public ViewSalesAndProductWithCustomer GetViewCompliteById(int saleId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"
SELECT        dbo.Sales.SaleDate, dbo.Sales.SaleId, dbo.Sales.TotalAmount, dbo.SaleItems.SaleItemsId, dbo.SaleItems.Quantity, dbo.SaleItems.Price, dbo.Products.ProductName, dbo.Products.Description, dbo.Categories.CategoryId, 
                         dbo.Categories.CategoryName, dbo.Customers.CustomerId AS CustomersId, dbo.Customers.CustomerName AS CustomersName, dbo.Customers.ContactNumber AS ContactNumbers, dbo.Customers.Email, 
                         dbo.Customers.Address, dbo.Products.ProductId AS ProductsId
FROM            dbo.Sales INNER JOIN
                         dbo.SaleItems ON dbo.Sales.SaleId = dbo.SaleItems.SaleId INNER JOIN
                         dbo.Products ON dbo.SaleItems.ProductId = dbo.Products.ProductId INNER JOIN
                         dbo.Categories ON dbo.Products.CategoryId = dbo.Categories.CategoryId INNER JOIN
                         dbo.Customers ON dbo.Sales.CustomerId = dbo.Customers.CustomerId
                    WHERE   dbo.Sales.SaleId = @SaleId";

                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@SaleId", saleId);
                ViewSalesAndProductWithCustomer viewComplite = null;

                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        viewComplite = new ViewSalesAndProductWithCustomer()
                        {
                            CustomerId = Convert.ToInt32(dr["CustomersId"]),
                            CustomerName = dr["CustomersName"].ToString(),
                            ContactNumber = dr["ContactNumbers"].ToString(),
                            Email = dr["Email"].ToString(),
                            Address = dr["Address"].ToString(),
                            SaleId = Convert.ToInt32(dr["SaleId"]),
                            SaleDate = Convert.ToDateTime(dr["SaleDate"]),
                            TotalAmount = Convert.ToDecimal(dr["TotalAmount"]),
                            SaleItemId = Convert.ToInt32(dr["SaleItemsId"]),
                            Quantity = Convert.ToInt32(dr["Quantity"]),
                            SalePrice = Convert.ToDecimal(dr["Price"]),
                            ProductId = Convert.ToInt32(dr["ProductsId"]),
                            ProductName = dr["ProductName"].ToString(),
                            CategoryId = Convert.ToInt32(dr["CategoryId"]),
                            CategoryName = dr["CategoryName"].ToString()
                        };
                    }
                    return viewComplite;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeleteViewComplite(int saleId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = "DELETE FROM Sales WHERE SaleId = @SaleId";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@SaleId", saleId);

                try
                {
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Sale not found");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
