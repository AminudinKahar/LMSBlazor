using LMSBlazor.WebApp.Shared.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace LMSBlazor.WebApp.ViewModelsValidations
{
    public class ApplyLeave_EnsureEnoughTotalLeaveDaysLeft : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var applyLeaveViewModel = validationContext.ObjectInstance as ApplyLeaveViewModel;

            if (applyLeaveViewModel != null)
            {
                if (applyLeaveViewModel.Employee != null)
                {
                    if (applyLeaveViewModel.Employee.Employee_Leaves != null && applyLeaveViewModel.Employee.Employee_Leaves.Count > 0)
                    {
                        foreach (var leave in applyLeaveViewModel.Employee.Employee_Leaves)
                        {
                            if (leave.LeaveId == applyLeaveViewModel.LeaveId)
                            {
                                if (leave.DaysLeft < applyLeaveViewModel.DaysToApply)
                                    return new ValidationResult($"Jumlah cuti bagi ({applyLeaveViewModel.Leave?.LeaveName}) tidak mencukupi : {leave.DaysLeft} hari!");
                            }
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
