using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace LMSBlazor.Plugins.SqlServer
{
    public class LeaveEFCoreRepository : ILeaveRepository
    {
        private readonly IDbContextFactory<LMSDbContext> contextFactory;

        public LeaveEFCoreRepository(IDbContextFactory<LMSDbContext> contextFactory)
        {
            this.contextFactory=contextFactory;
        }

        public async Task AddLeaveAsync(Leave leave) 
        {
            using var db = this.contextFactory.CreateDbContext();
            db.Leaves.Add(leave);

            await db.SaveChangesAsync();

        }

        public async Task DeleteLeaveAsync(Leave leave)
        {
            using var db = this.contextFactory.CreateDbContext();
            var lv = await db.Leaves.FindAsync(leave.LeaveId);

            if (lv != null)
            {
                db.Leaves.Remove(lv);

                await db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Leave>> GetLeavesByNameAsync(string name)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.Leaves.Where(
                x => x.LeaveName.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();

        }

        public async Task UpdateLeaveAsync(Leave leave)
        {
            using var db = this.contextFactory.CreateDbContext();
            var lv = await db.Leaves.FindAsync(leave.LeaveId);
            if (lv != null)
            {
                lv.LeaveName = leave.LeaveName;

                await db.SaveChangesAsync();
            } 
        }

        public async Task<Leave> ViewLeaveByIdAsync(int leaveId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var lv = await db.Leaves.FindAsync(leaveId);
            if (lv != null) return lv;

            return new Leave();
        }
    }
}