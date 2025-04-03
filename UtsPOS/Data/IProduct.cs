using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtsPOS.Data
{
    public interface IProduct
    {
        IEnumerable<Models.Product> GetProducts();
        Models.Product GetProductById(int productId);
        Models.Product AddProduct(Models.Product product);
        Models.Product UpdateProduct(Models.Product product);
        void DeleteProduct(int productId);       
    }
}