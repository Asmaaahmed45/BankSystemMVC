using BankSystem.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Models
{
    public class AddUserVM
    {

        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string Password {  get; set; }
        [DataType(DataType.Password)]
        [Compare("Password" ,ErrorMessage ="Password Don't Match")]
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; }
    }
}
