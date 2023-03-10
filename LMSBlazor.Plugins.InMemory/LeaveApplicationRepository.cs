using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.Plugins.InMemory
{
    public class LeaveApplicationRepository : ILeaveApplicationRepository
    {
        private static int maxApplicationId = 0;
        private static int maxApplicationLogId = 0;
        private readonly IEmployeeRepository employeeRepository;
        private readonly ILeaveRepository leaveRepository;
        public List<LeaveApplicationLog> _leaveApplicationLogs = new List<LeaveApplicationLog>();
        public List<LeaveApplication> _leaveApplications = new List<LeaveApplication>();

        public LeaveApplicationRepository(IEmployeeRepository employeeRepository,
            ILeaveRepository leaveRepository)
        {
            this.employeeRepository=employeeRepository;
            this.leaveRepository=leaveRepository;
        }
        public async Task ApplyLeaveAsync(string lvNumber, Employee employee, Leave leave, double totalDays, string doneBy, DateTime dateFrom, DateTime dateTo, ApplyType dateFromApplyType, ApplyType dateToApplyType)
        {

            if (_leaveApplications.Count != 0)
                maxApplicationId = _leaveApplications.Max(x => x.LeaveApplicationId);

            // add leave application
            _leaveApplications.Add(new LeaveApplication
            {
                LeaveApplicationId = maxApplicationId + 1,
                LvNumber = lvNumber,
                EmployeeId = employee.EmployeeId,
                Employee = employee,
                LeaveId = leave.LeaveId,
                Leave = leave,
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

            if (_leaveApplicationLogs.Count != 0)
                maxApplicationLogId = _leaveApplicationLogs.Max(x => x.LeaveApplicationId);

            _leaveApplicationLogs.Add(new LeaveApplicationLog
            {
                LeaveApplicationId = maxApplicationLogId + 1,
                LvNumber = lvNumber,
                EmployeeId = employee.EmployeeId,
                Employee = employee,
                LeaveId = leave.LeaveId,
                Leave = leave,
                TotalDaysBefore = employee.TotalLeaveDays,
                TotalDaysAfter = employee.TotalLeaveDays,
                ApplicationDate = DateTime.Now,
                DoneBy = doneBy,
                ApprovalType = ApprovalType.Tunda
            });

            await Task.CompletedTask;
        }

        public async Task ApproveLeaveAsync(int applicationId)
        {
            // find applicationId  in _leaveApplication
            var la = _leaveApplications.FirstOrDefault(x => x.LeaveApplicationId == applicationId);

            if (la != null)
            {
                if (la.Leave != null && la.Employee != null)
                {
                    // update application status to approved
                    la.ApprovalType = ApprovalType.Lulus;

                    // add application logs
                    _leaveApplicationLogs.Add(new LeaveApplicationLog
                    {
                        LvNumber = la.LvNumber,
                        EmployeeId = la.Employee.EmployeeId,
                        Employee = la.Employee,
                        LeaveId = la.Leave.LeaveId,
                        Leave = la.Leave,
                        TotalDaysBefore = la.Employee.TotalLeaveDays,
                        TotalDaysAfter = la.Employee.TotalLeaveDays  - la.TotalDays,
                        ApplicationDate = DateTime.Now,
                        DoneBy = la.DoneBy,
                        ApprovalType = ApprovalType.Lulus
                    });
                }
                
            }

            await Task.CompletedTask;
        }

        public async Task DenyLeaveAsync(int applicationId)
        {
            /// find applicationId  in _leaveApplication
            var la = _leaveApplications.FirstOrDefault(x => x.LeaveApplicationId == applicationId);

            if (la != null)
            {
                if (la.Leave != null && la.Employee != null)
                {
                    // update application status to denied
                    la.ApprovalType = ApprovalType.Gagal;

                    // add application logs
                    _leaveApplicationLogs.Add(new LeaveApplicationLog
                    {
                        LvNumber = la.LvNumber,
                        EmployeeId = la.Employee.EmployeeId,
                        Employee = la.Employee,
                        LeaveId = la.Leave.LeaveId,
                        Leave = la.Leave,
                        TotalDaysBefore = la.Employee.TotalLeaveDays,
                        TotalDaysAfter = la.Employee.TotalLeaveDays,
                        ApplicationDate = DateTime.Now,
                        DoneBy = la.DoneBy,
                        ApprovalType = ApprovalType.Gagal
                    });
                }

            }

            await Task.CompletedTask;
        }

        public async Task<string> GenerateRefNumberAsync(string initialRef, string currentYear, string currentMonth)
        {
            string prefix = initialRef +"/"+ currentYear+"/";
            int x = 1;
            string refNum = prefix + "000";

            var maxRefNum = _leaveApplications
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
            return await Task.FromResult(maxRefNum);
        }

        public Task<double> GenerateTotalDaysAsync(DateTime dateFrom, bool? isHalfDayDateFrom, DateTime dateTo, bool? isHalfDayDateTo)
        {
            double totalDays = 0;

            if (isHalfDayDateFrom != null)
            {
                if (isHalfDayDateFrom == true && isHalfDayDateTo == false ||
                    isHalfDayDateFrom == false && isHalfDayDateTo == true)

                    totalDays = (dateTo - dateFrom).TotalDays + 1 - 0.5;

                if (isHalfDayDateFrom == true && isHalfDayDateTo == true)
                    totalDays = (dateTo - dateFrom).TotalDays  + 1 - 1;

                totalDays =  (dateTo - dateFrom).TotalDays  + 1;
            }
            else
            {
                totalDays = (dateTo - dateFrom).TotalDays  + 1;
            }

            return Task.FromResult(totalDays);
        }

        public async Task<IEnumerable<LeaveApplication>> GetLeaveApplicationsAsync(string employeeName, DateTime? dateFrom, DateTime? dateTo, ApprovalType? approvalType)
        {
            var employees = (await employeeRepository.GetEmployeesByNameAsync(string.Empty)).ToList();
            var leaves = (await leaveRepository.GetLeavesByNameAsync(string.Empty)).ToList();

            var query = from la in this._leaveApplications
                        join emp in employees on la.EmployeeId equals emp.EmployeeId
                        join l in leaves on la.LeaveId equals l.LeaveId
                        where
                            (string.IsNullOrWhiteSpace(employeeName) || emp.EmployeeName.ToLower().IndexOf(employeeName.ToLower()) >= 0)
                            &&
                            (!dateFrom.HasValue || la.ApplicationDate >= dateFrom.Value.Date) &&
                            (!dateTo.HasValue || la.ApplicationDate <= dateTo.Value.Date) &&
                            (!approvalType.HasValue || la.ApprovalType == approvalType)
                        select new LeaveApplication
                        {
                            Employee = emp,
                            LeaveApplicationId = la.LeaveApplicationId,
                            LvNumber =la.LvNumber,
                            EmployeeId = la.EmployeeId,
                            Leave = l,
                            LeaveId = la.LeaveId,
                            DateFrom = la.DateFrom,
                            DateTo = la.DateTo,
                            TotalDays = la.TotalDays,
                            ApplicationDate = la.ApplicationDate,
                            DoneBy = la.DoneBy,
                            ApprovalType = la.ApprovalType
                        };

            return query;
        }

        public async Task<LeaveApplication?> GetLeaveApplicationsByIdAsync(int applicationId)
        {
            var la = _leaveApplications.FirstOrDefault(x => x.LeaveApplicationId == applicationId);
            var newLa = new LeaveApplication();
            if (la != null)
            {
                newLa.LeaveApplicationId = applicationId;
                newLa.EmployeeId = la.EmployeeId;
                newLa.Employee = la.Employee;
                newLa.Leave = la.Leave;
                newLa.LeaveId = la.LeaveId;
                newLa.ApplicationDate = la.ApplicationDate;
                newLa.DoneBy = la.DoneBy;
                newLa.LvNumber = la.LvNumber;
                newLa.ApprovalType = la.ApprovalType;
                newLa.TotalDays = la.TotalDays;
                
            }
            return await Task.FromResult(newLa);
        }

        public async Task<IEnumerable<LeaveApplication>> GetLeaveApplicationsByLvNumberAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_leaveApplications);

            return await Task.FromResult(_leaveApplications.Where(x => x.LvNumber.Contains(name, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
