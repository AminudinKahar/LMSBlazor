using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.Plugins.SqlServer
{
    public class LeaveApplicationEFCoreRepository : ILeaveApplicationRepository
    {
        private readonly IDbContextFactory<LMSDbContext> contextFactory;

        public LeaveApplicationEFCoreRepository(IDbContextFactory<LMSDbContext> contextFactory)
        {
            this.contextFactory=contextFactory;
        }
        public async Task ApplyLeaveAsync(string lvNumber, Employee employee, Leave leave, double totalDays, string doneBy, DateTime dateFrom, DateTime dateTo, ApplyType dateFromApplyType, ApplyType dateToApplyType)
        {
            using var db = this.contextFactory.CreateDbContext();

            // add leave application
            await db.LeaveApplications.AddAsync(new LeaveApplication
            {
                LvNumber = lvNumber,
                EmployeeId = employee.EmployeeId,
                LeaveId = leave.LeaveId,
                TotalDays = totalDays,
                ApplicationDate = DateTime.Now,
                DoneBy = doneBy,
                ApprovalType = ApprovalType.Tunda,
                DateFrom = dateFrom,
                DateTo = dateTo,
                DateFromApplyType = dateFromApplyType,
                DateToApplyType = dateToApplyType
            });
            // add leave application logs 


            await db.LeaveApplicationLogs.AddAsync(new LeaveApplicationLog
            {
                LvNumber = lvNumber,
                EmployeeId = employee.EmployeeId,
                LeaveId = leave.LeaveId,
                TotalDaysBefore = employee.TotalLeaveDays,
                TotalDaysAfter = employee.TotalLeaveDays,
                ApplicationDate = DateTime.Now,
                DoneBy = doneBy,
                ApprovalType = ApprovalType.Tunda
            });


            await db.SaveChangesAsync();
        }

        private void FlagLeaveUnchanged(Employee employee, LMSDbContext db)
        {
            if (employee?.Employee_Leaves != null &&
                            employee.Employee_Leaves.Count > 0)
            {
                foreach (var empLv in employee.Employee_Leaves)
                {
                    if (empLv.Leave != null)
                        db.Entry(empLv.Leave).State = EntityState.Unchanged;
                }
            }
        }

        public async Task ApproveLeaveAsync(int applicationId)
        {
            using var db = this.contextFactory.CreateDbContext();
            // find applicationId  in _leaveApplication
            var la = await db.LeaveApplications
                .Include(x => x.Employee)
                .Include(x => x.Leave)
                .FirstOrDefaultAsync(x => x.LeaveApplicationId == applicationId);

            if (la != null)
            {
                if (la.Leave != null && la.Employee != null)
                {
                    // update application status to approved
                    la.ApprovalType = ApprovalType.Lulus;

                    // add application logs
                    db.LeaveApplicationLogs.Add(new LeaveApplicationLog
                    {
                        LvNumber = la.LvNumber,
                        EmployeeId = la.Employee.EmployeeId,
                        LeaveId = la.Leave.LeaveId,
                        TotalDaysBefore = la.Employee.TotalLeaveDays,
                        TotalDaysAfter = la.Employee.TotalLeaveDays  - la.TotalDays,
                        ApplicationDate = DateTime.Now,
                        DoneBy = la.DoneBy,
                        ApprovalType = ApprovalType.Lulus
                    });

                    FlagLeaveUnchanged(la.Employee, db);
                }

                await db.SaveChangesAsync();
                
            }

        }

        public async Task DenyLeaveAsync(int applicationId)
        {
            using var db = this.contextFactory.CreateDbContext();
            /// find applicationId  in _leaveApplication
            var la = db.LeaveApplications
                .Include(x => x.Employee)
                .Include(x => x.Leave)
                .FirstOrDefault(x => x.LeaveApplicationId == applicationId);

            if (la != null)
            {
                if (la.Leave != null && la.Employee != null)
                {
                    // update application status to denied
                    la.ApprovalType = ApprovalType.Gagal;

                    // add application logs
                    db.LeaveApplicationLogs.Add(new LeaveApplicationLog
                    {
                        LvNumber = la.LvNumber,
                        EmployeeId = la.Employee.EmployeeId,
                        LeaveId = la.Leave.LeaveId,
                        TotalDaysBefore = la.Employee.TotalLeaveDays,
                        TotalDaysAfter = la.Employee.TotalLeaveDays,
                        ApplicationDate = DateTime.Now,
                        DoneBy = la.DoneBy,
                        ApprovalType = ApprovalType.Gagal
                    });

                    FlagLeaveUnchanged(la.Employee, db);
                }

                await db.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<LeaveApplication>> GetLeaveApplicationsAsync(string employeeName, DateTime? dateFrom, DateTime? dateTo, ApprovalType? approvalType)
        {
            using var db = this.contextFactory.CreateDbContext();
            var query = from la in db.LeaveApplications
                        join emp in db.Employees on la.EmployeeId equals emp.EmployeeId
                        join l in db.Leaves on la.LeaveId equals l.LeaveId
                        where
                            (string.IsNullOrWhiteSpace(employeeName) || emp.EmployeeName.ToLower().IndexOf(employeeName.ToLower()) >= 0)
                            &&
                            (!dateFrom.HasValue || la.ApplicationDate >= dateFrom.Value.Date) &&
                            (!dateTo.HasValue || la.ApplicationDate <= dateTo.Value.Date) &&
                            (!approvalType.HasValue || la.ApprovalType == approvalType)
                        select la;

            return await query.Include(x => x.Leave).Include(x => x.Employee).ToListAsync();
        }

        public async Task<LeaveApplication?> GetLeaveApplicationsByIdAsync(int applicationId)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.LeaveApplications
                .Include(x => x.Leave)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.LeaveApplicationId == applicationId);
            
        }

        public async Task<IEnumerable<LeaveApplication>> GetLeaveApplicationsByLvNumberAsync(string name)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.LeaveApplications.Where(x => x.LvNumber.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();
        }

        public Task<string> GenerateRefNumberAsync(string initialRef, string currentYear, string currentMonth)
        {
            using var db = this.contextFactory.CreateDbContext();
            string prefix = initialRef +"/"+ currentYear+"/";
            int x = 1;
            string refNum = prefix + "000";

            var maxRefNum = db.LeaveApplications
                .Max(x => x.LvNumber);
            if (maxRefNum == null)
            {
                maxRefNum = string.Format("{0:" + prefix + "000}", x);
            }
            else
            {
                x = int.Parse(maxRefNum.Substring(9));
                x++;
                refNum = string.Format("{0:" + prefix + "000}", x);
            }
            return Task.FromResult(maxRefNum);
        }

        public Task<double> GenerateTotalDaysAsync(DateTime dateFrom, bool? isHalfDayDateFrom, DateTime dateTo, bool? isHalfDayDateTo)
        {
            throw new NotImplementedException();
        }
    }
}
