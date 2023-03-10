using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.Plugins.InMemory
{
    public class EmployeeRepository : IEmployeeRepository
    {
        List<Employee> _employees = new List<Employee>();
        List<Employee_Leave> _employee_Leaves = new List<Employee_Leave>();

        public EmployeeRepository()
        {
            _employees = new List<Employee>()
            {
                new Employee { 
                    EmployeeId = 1, 
                    EmployeeName = "Aminudin Ab Kahar", 
                    EmployeeAge = 30, 
                    HireDate=DateTime.Now.AddYears(-1).AddDays(-12),
                    EmployeeEmail="amin@idwal.com.my",
                    EmployeePhoneNumber="011-33272978",
                    TotalLeaveDays = 0 
                },
                new Employee { 
                    EmployeeId = 2, 
                    EmployeeName = "Mohd Nor Bin Md Tan", 
                    EmployeeAge = 50, 
                    HireDate=DateTime.Now.AddYears(-20),
                    EmployeeEmail="mdNor@idwal.com.my", 
                    TotalLeaveDays = 0 
                }
            };

        }

        public Task AddEmployeeAsync(Employee employee)
        {
            if (_employees.Any(x => x.EmployeeName.Equals(employee.EmployeeName, StringComparison.OrdinalIgnoreCase))) return Task.CompletedTask;

            var maxId = _employees.Max(x => x.EmployeeId);
            employee.EmployeeId = maxId + 1;

            if (employee.Employee_Leaves!= null &&
                employee.Employee_Leaves.Count > 0)
            {
                foreach (var empLv in employee.Employee_Leaves)
                {
                    employee.TotalLeaveDays += empLv.DaysLeft;
                }
            }

            _employees.Add(employee);

            return Task.CompletedTask;
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {
            var emp = _employees.FirstOrDefault(x => x.EmployeeId == employeeId);
            var newEmp = new Employee();
            if (emp != null)
            {
                newEmp.EmployeeId = employeeId;
                newEmp.EmployeeName = emp.EmployeeName;
                newEmp.EmployeeAge = emp.EmployeeAge;
                newEmp.TotalLeaveDays = emp.TotalLeaveDays;
                newEmp.EmployeeEmail = emp.EmployeeEmail;
                newEmp.EmployeePhoneNumber = emp.EmployeePhoneNumber;
                newEmp.HireDate = emp.HireDate;
                newEmp.Employee_Leaves = new List<Employee_Leave>();
                if (emp.Employee_Leaves != null && emp.Employee_Leaves.Count > 0)
                {
                    foreach(var empLv in emp.Employee_Leaves)
                    {
                        var newEmpLv = new Employee_Leave
                        {
                            EmployeeId = empLv.EmployeeId,
                            Employee = emp,
                            LeaveId = empLv.LeaveId,
                            Leave = new Leave(),
                            DaysLeft = empLv.DaysLeft,
                        };
                        if (empLv.Leave != null)
                        {
                            newEmpLv.Leave.LeaveId = empLv.Leave.LeaveId;
                            newEmpLv.Leave.LeaveName = empLv.Leave.LeaveName;
                        }

                        newEmp.Employee_Leaves.Add(newEmpLv);
                    }
                }
            }
            return await Task.FromResult(newEmp);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_employees);

            return await Task.FromResult(_employees.Where(x => x.EmployeeName.Contains(name, StringComparison.OrdinalIgnoreCase)));
        }

        public Task UpdateEmployeeAsync(Employee employee)
        {
            //To prevent different produt from having the same name
            if (_employees.Any(x => x.EmployeeId != employee.EmployeeId &&
                    x.EmployeeName.ToLower() == employee.EmployeeName.ToLower())) return Task.CompletedTask;

            var emp = _employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
            if (emp != null)
            {
                emp.EmployeeName = employee.EmployeeName;
                emp.EmployeeAge = employee.EmployeeAge;
                emp.EmployeePhoneNumber = employee.EmployeePhoneNumber;
                emp.EmployeeEmail = employee.EmployeeEmail;
                emp.HireDate = employee.HireDate;
                if(employee.Employee_Leaves != null 
                    && employee.Employee_Leaves.Count > 0)
                {
                    emp.TotalLeaveDays = 0;

                    foreach (var empLv in employee.Employee_Leaves)
                    {
                        emp.TotalLeaveDays += empLv.DaysLeft;
                    }
                    emp.Employee_Leaves = employee.Employee_Leaves;
                }

            }

            return Task.CompletedTask;
        }
    }
}
