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
    public class ViewLeavesByNameUseCase : IViewLeavesByNameUseCase
    {
        private readonly ILeaveRepository leaveRepository;

        public ViewLeavesByNameUseCase(ILeaveRepository leaveRepository)
        {
            this.leaveRepository=leaveRepository;
        }
        public async Task<IEnumerable<Leave>> ExecuteAsync(string name = "")
        {
            return await leaveRepository.GetLeavesByNameAsync(name);
        }
    }
}
