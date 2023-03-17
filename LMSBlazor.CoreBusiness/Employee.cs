using LMSBlazor.CoreBusiness.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.CoreBusiness
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Nama diperlukan")]
        [StringLength(150, ErrorMessage = "Nama idak boleh melebihi 150 aksara")]
        public string EmployeeName { get; set; } = string.Empty;
        //[Range(18, int.MaxValue, ErrorMessage = "Umur mesti sama atau melebihi 18 tahun")]
        public int EmployeeAge { get; set; }
        [Phone(ErrorMessage = "Nombor Telefon tidak sah")]
        public string EmployeePhoneNumber { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Emel Tidak sah")]
        public string EmployeeEmail { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "Jumlah Cuti mesti sama atau melebihi 0 hari")]
        public double TotalLeaveDays { get; set; }
        [Required]
        public DateTime HireDate { get; set; }
        [Employee_EnsureTotalDaysIsEqualToLeaveDaysLeft]
        public List<Employee_Leave> Employee_Leaves { get; set; } = new List<Employee_Leave>();

        public void AddLeave(Leave leave)
        {
            if (!this.Employee_Leaves.Any(x => x.Leave != null &&
            x.Leave.LeaveName.Equals(leave.LeaveName)))
            {
                this.Employee_Leaves.Add(new Employee_Leave
                {
                    LeaveId = leave.LeaveId,
                    Leave = leave,
                    DaysLeft = 1,
                    EmployeeId = this.EmployeeId,
                    Employee = this
                });

                this.TotalLeaveDays++;
                
            }
        }

    }
}
