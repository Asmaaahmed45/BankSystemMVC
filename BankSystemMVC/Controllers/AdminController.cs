using BankSystem.Core.Models.Domains;
using BankSystem.Core.Repositories;
using BankSystem.EF.Repositories;
using BankSystemMVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BankSystemMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IBaseRepository<Employees> _ibaseRepository;

        public AdminController(IEmployeeRepository _employeeRepository, IBaseRepository<Employees> ibaseRepository)
        {
            _EmployeeRepository = _employeeRepository;
            _ibaseRepository = ibaseRepository;
        }
        [AdminAuth,Route("/Admin/AllEmployees")]
        public IActionResult GetAllEmployees()
        {

            var employees = _EmployeeRepository.GetAllEmp();
            return View("GetAllEmployees",employees);
        }
        [UserEditEmpAuth,HttpGet, Route("/Admin/EditEmployee")]
        public IActionResult EditEmployee([FromQuery] int id)
        {
            var employee = _ibaseRepository.GetById(id);
            if (employee != null)
            {
                return View(employee);
            }
            return RedirectToAction("GetAllEmployees");
        }
        [UserEditEmpAuth,Route("/Admin/DeleteEmployee")]
        public IActionResult DeleteEmployee([FromQuery] int id)
        {
            
            return RedirectToAction("GetAllEmployees");
        }
        [AdminAuth, Route("/Admin/EmployeeDetails")]
        public IActionResult EmployeeDetails([FromQuery] int id)
        {
            var emp = _EmployeeRepository.Getemp(id);
            return View(emp);
        }
    }
}
