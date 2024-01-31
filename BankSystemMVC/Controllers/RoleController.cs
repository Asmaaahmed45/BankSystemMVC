using BankSystem.Core.Models.Domains;
using BankSystem.Core.Repositories;
using BankSystemMVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BankSystemMVC.Controllers
{
    [AdminAuth]
    public class RoleController : Controller
    {
        private readonly IBaseRepository<Role> _roleRepository;
        public RoleController(IBaseRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/Role/AllRoles")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleRepository.GetAll();
            return View("AllRoles",roles);
        }
        public IActionResult AddRole(Role role)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "SomeThing Went Wrong,Please Try Again...";
                return View("AllRoles");
            }
            var IsExist = _roleRepository.IsExist(x => x.Name == role.Name);
            if (IsExist)
            {
                ViewBag.Message = "Role" + role.Name + " Is Already Exist..";
                return View("AllRoles");
            }
            _roleRepository.Add(role);
            ViewBag.Message = "Role Added Successfully...";
            return RedirectToAction(nameof(GetAllRoles));
        }
    }
}
