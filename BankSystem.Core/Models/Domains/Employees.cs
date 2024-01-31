using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Models.Domains
{
    public class Employees
    {
        [Key]
        [Required]
        public int EmpId { get; set; }
        [Required]
        public string EmpName { get; set; }
        [Range(5000,20000)]
        [Required]
        public int Salary { get; set; }
        [Required]
        [StringLength(14,ErrorMessage =  "SSN must be 14 number")]
        [RegularExpression(@"^[2-3]\d{13}$", ErrorMessage = "SSN must start with 2 or 3 and be 14 digits long")]
        public string SSN { get; set; }
        public bool IsDeleted { get; set; }
        public int GovernorateId { get; set; }
        public int VillageId { get; set; }
        public int RegionId { get; set; }

    }
}
