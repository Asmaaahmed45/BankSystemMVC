using BankSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace BankSystemMVC.Filters
{
    public class UserEditEmpAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var serializedData = context.HttpContext.Session.GetString("data");
            if (serializedData is null)
            {
                context.Result = new RedirectToActionResult(actionName: "Login", controllerName: "User", routeValues: null);

            }
            var loginVM = JsonConvert.DeserializeObject<LoginVM>(serializedData);
            if (loginVM.RoleId == 3)
            {
                List<string> RouteURL = new List<string>
                {
                    "/Employee/AllEmployees",
                    "/Employee/EditEmployee",
                    "/Employee/DeleteEmployee",
                };
            }
            else
            {

                context.Result = new RedirectToActionResult(actionName: "AccessDenied", controllerName: "User", routeValues: null);
            }
        }
    }
}
