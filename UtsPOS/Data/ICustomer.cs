using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtsPOS.Data
{
    public interface ICustomer
    {
        IEnumerable<Models.Customer> GetCustomers();
        Models.Customer GetCustomerById(int customerId);
        Models.Customer AddCustomer(Models.Customer customer);
        Models.Customer UpdateCustomer(Models.Customer customer);
        void DeleteCustomer(int customerId);        
    }
}