using System.ComponentModel.DataAnnotations;

namespace LMSBlazor.CoreBusiness
{
    public class Leave
    {
        public int LeaveId { get; set; }
        [Required(ErrorMessage = "Perihal diperlukan")]
        [StringLength(150, ErrorMessage = "Perihal tidak boleh melebihi 150 aksara")]
        public string LeaveName { get; set; } = string.Empty;
        public List<Employee_Leave> Employee_Leaves { get; set; } = new List<Employee_Leave>();
    }
}