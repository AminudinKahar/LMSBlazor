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
    public class AddEmployeeUseCase : IAddEmployeeUseCase
    {
        private readonly IEmployeeRepository employeeRepository;

        public AddEmployeeUseCase(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository=employeeRepository;
        }
        public async Task ExecuteTask(Employee employee)
        {
            await this.employeeRepository.AddEmployeeAsync(employee);
        }
    }
}
