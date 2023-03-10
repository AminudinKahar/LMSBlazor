using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.Plugins.SqlServer
{
    public class EmployeeEFCoreRepository : IEmployeeRepository
    {
        private readonly IDbContextFactory<LMSDbContext> contextFactory;

        public EmployeeEFCoreRepository(IDbContextFactory<LMSDbContext> contextFactory)
        {
            this.contextFactory=contextFactory;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.Employees.Add(employee);
            FlagLeaveUnchanged(employee, db);

            await db.SaveChangesAsync();
        }

        private void FlagLeaveUnchanged(Employee employee, LMSDbContext db)
        {
            if (employee?.Employee_Leaves != null &&
                            employee.Employee_Leaves.Count > 0)
            {
                foreach (var empLv in employee.Employee_Leaves)
                {
                    if (empLv.Leave != null)
                        db.Entry(empLv.Leave).State = EntityState.Unchanged;
                }
            }
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.Employees.Include(x => x.Employee_Leaves)
                .ThenInclude(x => x.Leave).FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByNameAsync(string name)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.Employees.Include(x => x.Employee_Leaves)
                .ThenInclude(x => x.Leave)
                .Where(
                x => x.EmployeeName.ToLower().IndexOf(name.ToLower()) >= 0)
                .ToListAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            using var db = this.contextFactory.CreateDbContext();
            //To prevent different produt from having the same name
            var emp = await db.Employees.Include(x => x.Employee_Leaves)
                .FirstOrDefaultAsync(x => x.EmployeeId == employee.EmployeeId);

            if (emp != null)
            {
                emp.EmployeeName = employee.EmployeeName;
                emp.EmployeeAge = employee.EmployeeAge;
                emp.EmployeePhoneNumber = employee.EmployeePhoneNumber;
                emp.EmployeeEmail = employee.EmployeeEmail;
                emp.HireDate = employee.HireDate;
                emp.TotalLeaveDays = employee.TotalLeaveDays;
                emp.Employee_Leaves = employee.Employee_Leaves;

                FlagLeaveUnchanged(employee, db);

                await db.SaveChangesAsync();

            }
        }
    }
}
