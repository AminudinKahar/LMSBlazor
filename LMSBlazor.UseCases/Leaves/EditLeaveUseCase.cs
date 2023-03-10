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
    public class EditLeaveUseCase : IEditLeaveUseCase
    {
        private readonly ILeaveRepository leaveRepository;

        public EditLeaveUseCase(ILeaveRepository leaveRepository)
        {
            this.leaveRepository=leaveRepository;
        }
        public async Task ExecuteAsync(Leave leave)
        {
            await this.leaveRepository.UpdateLeaveAsync(leave);
        }
    }
}
