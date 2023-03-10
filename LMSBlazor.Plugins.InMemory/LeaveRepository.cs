using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.PluginInterfaces;

namespace LMSBlazor.Plugins.InMemory
{
    public class LeaveRepository : ILeaveRepository
    {
        private List<Leave> _leaves;

        public LeaveRepository()
        {
            _leaves = new List<Leave>()
            {
                new Leave { LeaveId = 1, LeaveName = "Cuti Tahunan"},
                new Leave { LeaveId = 2, LeaveName = "Cuti Sakit"},
                new Leave { LeaveId = 3, LeaveName = "Cuti Bersalin (Perempuan)"},
                new Leave { LeaveId = 4, LeaveName = "Cuti Bersalin (Lelaki)"},
            };
        }

        public Task AddLeaveAsync(Leave leave)
        {
            if (_leaves.Any(x => x.LeaveName.Equals(leave.LeaveName, StringComparison.OrdinalIgnoreCase))) return Task.CompletedTask;

            var maxId = _leaves.Max(x => x.LeaveId);
            leave.LeaveId = maxId + 1;
            _leaves.Add(leave);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Leave>> GetLeavesByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_leaves);

            return _leaves.Where(x => x.LeaveName.Contains(name,StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public Task UpdateLeaveAsync(Leave leave)
        {
            //if (_leaves.Any(x => x.LeaveId != leave.LeaveId &&
            //    x.LeaveName.Equals(leave.LeaveName, StringComparison.OrdinalIgnoreCase)))
            //    return Task.CompletedTask;

            var lv = _leaves.FirstOrDefault(x => x.LeaveId == leave.LeaveId);
            if (lv != null)
            {
                lv.LeaveName = leave.LeaveName;
            }

            return Task.CompletedTask;
        }

        public async Task<Leave> ViewLeaveByIdAsync(int leaveId)
        {
            var lv = _leaves.First(x => x.LeaveId == leaveId);

            var newLv = new Leave
            {
                LeaveId = leaveId,
                LeaveName = lv.LeaveName
            };

            return await Task.FromResult(newLv);
        }
    }
}