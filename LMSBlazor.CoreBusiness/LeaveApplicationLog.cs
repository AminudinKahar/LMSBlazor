using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.CoreBusiness
{
    public class LeaveApplicationLog
    {
        [Key]
        public int LeaveApplicationId { get; set; }
        public string LvNumber { get; set; } = string.Empty;
        [Required]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        [Required]
        public double TotalDaysBefore { get; set; }
        [Required]
        public int LeaveId { get; set; }
        public Leave? Leave { get; set; }
        [Required]
        public double TotalDaysAfter { get; set; }
        [Required]
        public DateTime ApplicationDate { get; set; }
        [Required]
        public string DoneBy { get; set; } = string.Empty;
        public ApprovalType ApprovalType  { get; set; }

    }
}
