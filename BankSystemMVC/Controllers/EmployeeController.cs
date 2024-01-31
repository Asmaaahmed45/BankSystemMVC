using BankSystem.Core.Models.Domains;
using BankSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankSystem.Core.Repositories;
using BankSystemMVC.Filters;
using AspNetCore.Reporting;
using Microsoft.Extensions.Hosting;
using Microsoft.Reporting.NETCore;
using ClosedXML.Excel;

namespace BankSystemMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IBaseRepository<Employees> _ibaseRepository;
        private readonly IBaseRepository<Governorates> _GbaseRepository;
        private readonly IBaseRepository<Village> _VbaseRepository;
        private readonly IBaseRepository<Region> _RbaseRepository;
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IWebHostEnvironment HostEnvironment;
        public EmployeeController(IBaseRepository<Employees> ibaseRepository, 
            IBaseRepository<Governorates> gbaseRepository,
            IBaseRepository<Village> vbaseRepository,
            IBaseRepository<Region> rbaseRepository,
            IEmployeeRepository employeeRepository,
            IWebHostEnvironment HostEnvironment
            )
        {
            _ibaseRepository = ibaseRepository;
            _GbaseRepository = gbaseRepository;
            _VbaseRepository = vbaseRepository;
            _RbaseRepository = rbaseRepository;
            _EmployeeRepository = employeeRepository;
            this.HostEnvironment = HostEnvironment; 
        }
        public IActionResult Index()
        {
            return View();
        }
        [AdminAuth, Route("Admin/Report")]
        public IActionResult Report([FromQuery] int id)
        {
            var emp = _EmployeeRepository.Getemp(id);
            string mimetype = "application/pdf";
            int extention = 1;
            var path = $"{HostEnvironment.WebRootPath}\\Reports\\EmpReport.rdlc";
            var localReport = new AspNetCore.Reporting.LocalReport(path);
            var parameters = new Dictionary<string, string>
            {
                {"Name",emp.EmpName },
                {"Salary",emp.Salary.ToString() },
                {"SSN",emp.SSN },
                {"Governorate",emp.Governorate },
                {"Village",emp.Village},
                {"Region",emp.Region },
            };
            var ds = new List<EmpVM>();
            ds.Add(emp);
            localReport.AddDataSource("ds",ds);

            var res = localReport.Execute(RenderType.Pdf, extention, parameters, mimetype);

            return File(res.MainStream, "application/pdf", $"Report_{DateTime.Now}.pdf");
        }


        [AdminAuth,Route("/Employee/EmployeeDetails")]
        public IActionResult EmployeeDetails([FromQuery]int id) 
        { 
            var emp= _EmployeeRepository.Getemp(id);
            return View(emp);
        }
        [Route("/Employee/AllEmployees")]
        [UserEditEmpAuth]
        public IActionResult GetAllEmployees()
        {

            var employees = _EmployeeRepository.GetAllEmp();
            return View(employees);
        }
        public IActionResult GetAllGovernorates()
        {

            return Ok(_GbaseRepository.GetAll());
        }
        public IActionResult GetVillage(int GovernorateId)
        {
            return Ok(_VbaseRepository.GetById(x => x.GovernorateId == GovernorateId));
        }
        public IActionResult GetRegion(int VillageId)
        {
            return Ok(_RbaseRepository.GetById(x => x.VillageId == VillageId));
        }
        [UserAddEmpAuth]
        public IActionResult AddEmployee() { 
            return View();
        }
        [HttpGet,UserEditEmpAuth,Route("/Employee/EditEmployee")]
        public IActionResult EditEmployee([FromQuery]int id)
        {
            var employee = _ibaseRepository.GetById(id);
            if (employee != null)
            {
                var emp = _EmployeeRepository.Getemp(employee.EmpId);
                ViewBag.Governorate = emp.Governorate;
                ViewBag.Village = emp.Village;
                ViewBag.Region = emp.Region;
                return View(employee);
            }
            return RedirectToAction("GetAllEmployees");
        }
        [UserEditEmpAuth,Route("/Employee/DeleteEmployee")]
        public IActionResult DeleteEmployee([FromQuery] int id)
        {
             var emp= _ibaseRepository.GetById(id);
             emp.IsDeleted= true;
            _ibaseRepository.Update(emp);
            return RedirectToAction("GetAllEmployees");
        }
        [HttpPost,UserEditEmpAuth]
        public IActionResult EditEmployee(Employees employee)
        {
            _ibaseRepository.Update(employee);
             return RedirectToAction("GetAllEmployees");
        }
        [HttpPost,UserAddEmpAuth, Route("/Employee/AddEmployee")]
        public IActionResult AddEmployee(Employees employee)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "SomeThing Went Wrong,Please Try Again...";
                return View("AddEmployee");
            }
            var IsExist = _ibaseRepository.IsExist(x => x.SSN==employee.SSN);
            if (IsExist)
            {
                ViewBag.Message = "User " + employee.EmpName + " Is Already Exist..";
                return View("AddEmployee");
            }
            _ibaseRepository.Add(employee);
           
            ViewBag.Message = "Employee Added Successfully...";
            return View();
        }
    }
}

