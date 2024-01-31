using BankSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BankSystemMVC.Filters
{
    public class AdminAuthAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var serializedData= context.HttpContext.Session.GetString("data");
            if (serializedData is null)
            {
                context.Result = new RedirectToActionResult(actionName: "Login", controllerName: "User",routeValues:null);

            }
            var loginVM = JsonConvert.DeserializeObject<LoginVM>(serializedData);
            if (loginVM.RoleId == 1)
            {
                List<string> RouteURL = new List<string>
                {
                    "/Role/AllRoles",
                    "/User/AddUser",
                    "Role/GetAllRoles",
                    "/Admin/AllEmployees",
                    "/Admin/EmployeeDetails",

                };
            }
            else
            {
               
                context.Result = new RedirectToActionResult(actionName: "AccessDenied", controllerName: "User", routeValues: null);
            }
        }
    }
}
