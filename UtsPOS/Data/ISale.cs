using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtsPOS.Data
{
    public interface ISale
    {
        IEnumerable<Models.Sale> GetSales();
        Models.Sale GetSaleById(int saleId);
        Models.Sale AddSale(Models.Sale sale);
        Models.Sale UpdateSale(Models.Sale sale);
        void DeleteSale(int saleId);        
    }
}