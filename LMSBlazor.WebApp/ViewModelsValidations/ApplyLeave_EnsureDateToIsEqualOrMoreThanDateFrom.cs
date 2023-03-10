using LMSBlazor.WebApp.Shared.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace LMSBlazor.WebApp.ViewModelsValidations
{
    public class ApplyLeave_EnsureDateToIsEqualOrMoreThanDateFrom : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var applyLeaveViewModel = validationContext.ObjectInstance as ApplyLeaveViewModel;

            if (applyLeaveViewModel != null)
            {
                if (applyLeaveViewModel.DateFrom > applyLeaveViewModel.DateTo)
                {
                    return new ValidationResult("Tarikh Dari tidak boleh kurang dari Tarikh Hingga");
                }

            }
            return ValidationResult.Success;
        }
    }
}
