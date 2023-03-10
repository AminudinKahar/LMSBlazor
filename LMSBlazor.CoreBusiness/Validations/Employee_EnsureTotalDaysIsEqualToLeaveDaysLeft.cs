using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.CoreBusiness.Validations
{
    public class Employee_EnsureTotalDaysIsEqualToLeaveDaysLeft : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var employee = validationContext.ObjectInstance as Employee;
            if (employee != null)
            {
                if (!ValidateTotalDays(employee))
                    return new ValidationResult(
                        $"Jumlah hari cuti mesti sama dengan jumlah cuti berbaki : {TotalDaysAccumulated(employee)} hari!",
                        new List<string> { validationContext.MemberName });;
            }

            return ValidationResult.Success;
        }

        private double TotalDaysAccumulated (Employee employee)
        {
            if (employee == null && employee?.Employee_Leaves == null) return 0;

            return employee.Employee_Leaves.Sum(x => x.DaysLeft);
        }

        private bool ValidateTotalDays(Employee employee)
        {
            if (employee.Employee_Leaves == null || employee.Employee_Leaves.Count <=0) return true;

            if (TotalDaysAccumulated(employee) != employee.TotalLeaveDays) return false;

            return true;
        }
    }
}
