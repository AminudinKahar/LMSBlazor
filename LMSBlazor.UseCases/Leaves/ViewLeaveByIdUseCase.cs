using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.Leaves.Interfaces;
using LMSBlazor.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.UseCases.Leaves
{
    public class ViewLeaveByIdUseCase : IViewLeaveByIdUseCase
    {
        private readonly ILeaveRepository leaveRepository;

        public ViewLeaveByIdUseCase(ILeaveRepository leaveRepository)
        {
            this.leaveRepository=leaveRepository;
        }
        public async Task<Leave> ExecuteAsync(int leaveId)
        {
            return await this.leaveRepository.ViewLeaveByIdAsync(leaveId);
        }
    }
}
