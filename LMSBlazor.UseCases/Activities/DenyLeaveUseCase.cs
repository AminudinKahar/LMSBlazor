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
    public class DenyLeaveUseCase : IDenyLeaveUseCase
    {
        private readonly ILeaveApplicationRepository leaveApplicationRepository;

        public DenyLeaveUseCase(ILeaveApplicationRepository leaveApplicationRepository,
            IEmployeeRepository employeeRepository)
        {
            this.leaveApplicationRepository=leaveApplicationRepository;
        }

        public async Task ExecuteAsync(int applicationId)
        {
            await this.leaveApplicationRepository.DenyLeaveAsync(applicationId);
        }
    }
}
