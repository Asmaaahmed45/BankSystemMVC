using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Models
{
    public class EmpVM
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public int Salary { get; set; }
        public string SSN { get; set; }
        public string Governorate { get; set;}
        public string Village { get; set;}
        public string Region { get; set;}
    }
}
