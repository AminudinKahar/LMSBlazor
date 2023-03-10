using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.Employees.Interfaces;
using LMSBlazor.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.UseCases.Employees
{
    public class EditEmployeeUseCase : IEditEmployeeUseCase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EditEmployeeUseCase(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository=employeeRepository;
        }
        public async Task ExecuteAsync(Employee employee)
        {
            await this.employeeRepository.UpdateEmployeeAsync(employee);
        }

    }
}
