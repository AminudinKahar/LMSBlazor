using LMSBlazor.CoreBusiness;
using LMSBlazor.WebApp.ViewModelsValidations;
using System.ComponentModel.DataAnnotations;

namespace LMSBlazor.WebApp.Shared.ViewModels
{
    public class ApplyLeaveViewModel
    {
        [Required(ErrorMessage = "No Rujukan diperlukan")]
        public string LvNumber { get; set; } = string.Empty;
        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Pekerja diperlukan")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; } = null;
        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Jenis Cuti diperlukan")]
        [ApplyLeave_EnsureLeaveAppointedToEmployee]
        public int LeaveId { get; set; }
        public Leave? Leave { get; set; } = null;
        [Required]
        [Range(minimum: 0.5, maximum: int.MaxValue, ErrorMessage = "Jumlah cuti mesti lebih dari 0 hari")]
        [ApplyLeave_EnsureEnoughTotalLeaveDaysLeft]
        public double DaysToApply { get; set; }
        [Required]
        public DateTime DateFrom { get; set; } = DateTime.Now;
        public ApplyType DateFromApplyType { get; set; }
        [Required]
        [ApplyLeave_EnsureDateToIsEqualOrMoreThanDateFrom]
        public DateTime DateTo { get; set; } = DateTime.Now;
        public ApplyType DateToApplyType { get; set; }
    }
}
