using BankSystem.Core.Models;
using BankSystem.Core.Models.Domains;
using BankSystem.Core.Repositories;
using BankSystem.EF.Repositories;
using BankSystemMVC.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Data;
using System.Security.Policy;

namespace BankSystemMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IBaseRepository<User> _ibaseRepository;
        private readonly IBaseRepository<Role> _roleRepository;
        public UserController(IBaseRepository<User> ibaseRepository, IBaseRepository<Role> roleRepository)
        {
            _ibaseRepository = ibaseRepository;
            _roleRepository = roleRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetRoles()
        {
            return Ok( _roleRepository.GetAll());
        }
        [AdminAuth,Route("User/AddUser")]
        public IActionResult AddUser()
        {
            return View();
        }
       
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
        [HttpPost]
        public IActionResult Login(LoginVM loginVM) 
        { 
            if(!ModelState.IsValid)
            {
                ViewBag.Message = "SomeThing Went Wrong,Please Try Again...";
                return View("login");
            }
            var user1 = _ibaseRepository.GetByName(x=>x.UserName==loginVM.UserName);
            loginVM.RoleId = user1.RoleId;
            bool verified = BCrypt.Net.BCrypt.Verify(loginVM.Password, user1.Password);
            if (user1 == null || verified==false)
            {
                ViewBag.Message = "UserName or Password Is Wrong,Please Try Again..";
                return View("Login");
            }
            HttpContext.Session.SetString("data", Newtonsoft.Json.JsonConvert.SerializeObject(loginVM));    
            return View("Home");
        }
        [HttpPost]
        [AdminAuth, Route("User/AddUser")]
        public IActionResult AddUser(AddUserVM addUserVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "SomeThing Went Wrong,Please Try Again...";
                return View("AddUser");
            }
            var userfound = _ibaseRepository.GetByName(x => x.UserName == addUserVM.UserName);
            if (userfound!=null&&userfound.RoleId==addUserVM.RoleId&&userfound.Email==addUserVM.Email)
            {
                ViewBag.Message = "User " + addUserVM.UserName + " Is Already Exist..";
                return View("AddUser");
            }
          User user = new User
          {
                UserName = addUserVM.UserName,
                Email = addUserVM.Email,
                PhoneNumber = addUserVM.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(addUserVM.Password),
                RoleId = addUserVM.RoleId,
          };
            _ibaseRepository.Add(user);
            ViewBag.Message = "User Added Successfully...";
            return View("Home");
        }
    }
}
