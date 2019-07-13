using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Employee
    {
        [Key]
        public int AuthenticationID { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string AnnualSalary { get; set; } = string.Empty;

        public string MonthlySalary { get; set; } = string.Empty;

        public string EmployeeType { get; set; } = string.Empty;
    }
}
