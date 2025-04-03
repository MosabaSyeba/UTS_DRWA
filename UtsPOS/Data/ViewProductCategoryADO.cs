using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using UtsPOS.Models;

namespace UtsPOS.Data
{
    public class ViewProductCategoryADO : IViewProductWithCategory

    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;

        public ViewProductCategoryADO(IConfiguration configuration)
        {
            _configuration = configuration;
            this.connStr = configuration.GetConnectionString("DefaultConnection");
        }

        
        public void DeleteProductCategory(int productId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"DELETE FROM Products WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(strSql, conn);

                try
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Product not found");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public ViewProductAndCategory GetProductCategoryById(int productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ViewProductAndCategory> GetProductCategory()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"SELECT Categories.CategoryId, Categories.CategoryName, Products.ProductId, Products.ProductName, Products.Price, Products.StockQuantity, Products.Description
                                  FROM Categories INNER JOIN
                                  Products ON Categories.CategoryId = Products.CategoryId";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                List<ViewProductAndCategory> products = new List<ViewProductAndCategory>();

                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ViewProductAndCategory product = new ViewProductAndCategory();
                        product.ProductId = Convert.ToInt32(dr["ProductId"]);
                        product.ProductName = dr["ProductName"].ToString();
                        product.Description = dr["Description"].ToString();
                        product.Price = Convert.ToDecimal(dr["Price"]);
                        product.StockQuantity = Convert.ToInt32(dr["StockQuantity"]);
                        product.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                        product.CategoryName = dr["CategoryName"].ToString();
                        products.Add(product);
                    }
                    return products;
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
    }
}
