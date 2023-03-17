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
    public class DeleteEmployeeUseCase : IDeleteEmployeeUseCase
    {
        private readonly IEmployeeRepository employeeRepository;

        public DeleteEmployeeUseCase(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository=employeeRepository;
        }
        public async Task ExecuteAsync(Employee employee)
        {
            await this.employeeRepository.DeleteEmployeeAsync(employee);
        }
    }
}
