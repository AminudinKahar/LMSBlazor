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
    public class ViewEmployeesByNameUseCase : IViewEmployeesByNameUseCase
    {
        private readonly IEmployeeRepository employeeRepository;

        public ViewEmployeesByNameUseCase(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository=employeeRepository;
        }
        public async Task<IEnumerable<Employee>> ExecuteAsync(string name = "")
        {
            return await employeeRepository.GetEmployeesByNameAsync(name);
        }
    }
}
