using LMSBlazor.WebApp.Shared.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace LMSBlazor.WebApp.ViewModelsValidations
{
    public class ApplyLeave_EnsureLeaveAppointedToEmployee : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var applyLeaveViewModel = validationContext.ObjectInstance as ApplyLeaveViewModel;
            if (applyLeaveViewModel != null)
            {
                // check if leave is exist or not
                if (applyLeaveViewModel.LeaveId == 0)
                {
                    return new ValidationResult("Jenis cuti ini tidak berdaftar.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
