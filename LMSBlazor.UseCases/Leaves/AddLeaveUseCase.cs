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
    public class AddLeaveUseCase : IAddLeaveUseCase
    {
        private readonly ILeaveRepository leaveRepository;

        public AddLeaveUseCase(ILeaveRepository leaveRepository)
        {
            this.leaveRepository=leaveRepository;
        }
        public async Task ExecuteAsync(Leave leave)
        {
            await this.leaveRepository.AddLeaveAsync(leave);
        }
    }
}
