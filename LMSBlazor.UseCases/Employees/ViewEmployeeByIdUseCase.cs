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
    public class ViewEmployeeByIdUseCase : IViewEmployeeByIdUseCase
    {
        private readonly IEmployeeRepository employeeRepository;

        public ViewEmployeeByIdUseCase(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository=employeeRepository;
        }
        public async Task<Employee?> ExecuteAsync(int employeeId)
        {
            return await employeeRepository.GetEmployeeByIdAsync(employeeId);
        }
    }
}
