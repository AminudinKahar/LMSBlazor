using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.LeaveApplications.Interfaces;
using LMSBlazor.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.UseCases.LeaveApplications
{
    public class ViewLeaveApplicationByIdUseCase : IViewLeaveApplicationByIdUseCase
    {
        private readonly ILeaveApplicationRepository leaveApplicationRepository;

        public ViewLeaveApplicationByIdUseCase(ILeaveApplicationRepository leaveApplicationRepository)
        {
            this.leaveApplicationRepository=leaveApplicationRepository;
        }
        public async Task<LeaveApplication?> ExecuteAsync(int applicationId)
        {
            return await leaveApplicationRepository.GetLeaveApplicationsByIdAsync(applicationId);
        }
    }
}
