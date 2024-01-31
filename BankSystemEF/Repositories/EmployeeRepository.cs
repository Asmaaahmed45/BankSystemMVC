using BankSystem.Core.Models;
using BankSystem.Core.Models.Domains;
using BankSystem.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.EF.Repositories
{
    public class EmployeeRepository:BaseRepository<Employees>,IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }
        public EmpVM Getemp(int id)
        {
            var Employee = (from emp in _context.Employees
                            join gov in _context.Governorates
                            on emp.GovernorateId equals gov.Id
                            join vill in _context.Village
                            on emp.VillageId equals vill.Id
                            join reg in _context.Region
                            on emp.RegionId equals reg.Id
                            where emp.EmpId == id
                            select new EmpVM
                            {
                                EmpId = emp.EmpId,
                                EmpName = emp.EmpName,
                                Salary = emp.Salary,
                                SSN = emp.SSN,
                                Governorate = gov.Name,
                                Village = vill.Name,
                                Region = reg.Name
                            }).FirstOrDefault();
            return (Employee);
        }
        public IEnumerable<Employees> GetAllEmp()
        {
            var Emps = (from emp in _context.Employees
                        where emp.IsDeleted == false
                        select new Employees
                        {
                            EmpId = emp.EmpId,
                            EmpName = emp.EmpName,
                            Salary = emp.Salary,
                            SSN = emp.SSN,
                            GovernorateId = emp.GovernorateId,
                            RegionId = emp.RegionId,
                            VillageId = emp.VillageId,
                        }).ToList();
            return (Emps);
        }
    }
}
