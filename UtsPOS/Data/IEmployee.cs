using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtsPOS.Data
{
    public interface IEmployee
    {
        IEnumerable<Models.Employee> GetEmployees();
        Models.Employee GetEmployeeById(int employeeId);
        Models.Employee AddEmployee(Models.Employee employee);
        Models.Employee UpdateEmployee(Models.Employee employee);
        void DeleteEmployee(int employeeId);
    }
}