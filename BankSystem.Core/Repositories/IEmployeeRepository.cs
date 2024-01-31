using BankSystem.Core.Models.Domains;
using BankSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Repositories
{
    public interface IEmployeeRepository
    {
        EmpVM Getemp(int id);
        IEnumerable<Employees> GetAllEmp();
    }
}
