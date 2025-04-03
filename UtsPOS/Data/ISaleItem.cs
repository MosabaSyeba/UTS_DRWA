using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtsPOS.Data
{
    public interface ISaleItem
    {
        IEnumerable<Models.SaleItem> GetSaleItems();
        Models.SaleItem GetSaleItemById(int saleItemId);
        Models.SaleItem AddSaleItem(Models.SaleItem saleItem);
        Models.SaleItem UpdateSaleItem(Models.SaleItem saleItem);
        void DeleteSaleItem(int saleItemId);        
    }
}