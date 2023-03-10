using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.CoreBusiness
{
    public class Employee_Leave
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int LeaveId { get; set; }
        public Leave? Leave { get; set; }
        public double DaysLeft { get; set; }
    }
}
