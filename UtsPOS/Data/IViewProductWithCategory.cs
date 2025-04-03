using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtsPOS.Models;

namespace UtsPOS.Data
{
    public interface IViewProductWithCategory
    {
        IEnumerable<ViewProductAndCategory> GetProductCategory();
        ViewProductAndCategory GetProductCategoryById(int productId);
        void DeleteProductCategory(int productId);
    }
}