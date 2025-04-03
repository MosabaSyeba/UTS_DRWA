using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtsPOS.Models;

namespace UtsPOS.Data
{
    public interface IViewSalesAndProductWithCustomer
    {
        IEnumerable<ViewSalesAndProductWithCustomer> GetViewComplite();  
        ViewSalesAndProductWithCustomer GetViewCompliteById(int saleId); 
        void DeleteViewComplite(int saleId); 
    }
}