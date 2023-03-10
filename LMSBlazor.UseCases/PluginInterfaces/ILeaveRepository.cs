using LMSBlazor.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.UseCases.PluginInterfaces
{
    public interface ILeaveRepository
    {
        Task<IEnumerable<Leave>> GetLeavesByNameAsync(string name);
        Task AddLeaveAsync(Leave leave);
        Task UpdateLeaveAsync(Leave leave);
        Task<Leave> ViewLeaveByIdAsync(int leaveId);
    }
}
