using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.Activities.Interfaces;
using LMSBlazor.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.UseCases.Activities
{
    public class ApproveLeaveUseCase : IApproveLeaveUseCase
    {
        private readonly ILeaveApplicationRepository leaveApplicationRepository;
        private readonly IEmployeeRepository employeeRepository;

        public ApproveLeaveUseCase(ILeaveApplicationRepository leaveApplicationRepository,
            IEmployeeRepository employeeRepository)
        {
            this.leaveApplicationRepository=leaveApplicationRepository;
            this.employeeRepository=employeeRepository;
        }

        public async Task ExecuteAsync(int applicationId)
        {
            // find application by id
            var la = await this.leaveApplicationRepository.GetLeaveApplicationsByIdAsync(applicationId);

            if (la != null)
            {
                if (la.Employee != null && la.Leave != null)
                {
                    // insert a record in the transaction table
                    await this.leaveApplicationRepository.ApproveLeaveAsync(applicationId);

                    // decrease the totalDays
                    la.Employee.TotalLeaveDays -= la.TotalDays;

                    // decrease the daysLeft in Employee_Leaves
                    foreach (var emp in la.Employee.Employee_Leaves)
                    {
                        if (emp.LeaveId != la.Leave.LeaveId) continue;
                        emp.DaysLeft -= la.TotalDays;
                    }

                    await this.employeeRepository.UpdateEmployeeAsync(la.Employee);
                }
                
            }
            
        }
    }
}
