using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.LeaveApplications.Interfaces;
using LMSBlazor.UseCases.PluginInterfaces;

namespace LMSBlazor.UseCases.LeaveApplications
{
    public class ViewLeaveApplicationsByLvNumberUseCase : IViewLeaveApplicationsByLvNumberUseCase
    {
        private readonly ILeaveApplicationRepository leaveApplicationRepository;

        public ViewLeaveApplicationsByLvNumberUseCase(ILeaveApplicationRepository leaveApplicationRepository)
        {
            this.leaveApplicationRepository=leaveApplicationRepository;
        }
        public async Task<IEnumerable<LeaveApplication>> ExecuteAsync(string name = "")
        {
            return await leaveApplicationRepository.GetLeaveApplicationsByLvNumberAsync(name);
        }
    }
}
