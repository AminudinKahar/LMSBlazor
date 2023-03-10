using LMSBlazor.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.UseCases.PluginInterfaces
{
    public interface ILeaveApplicationRepository
    {
        Task ApplyLeaveAsync(string lvNumber, Employee employee, Leave leave, double totalDays, string doneBy, DateTime dateFrom, DateTime dateTo, ApplyType dateFromApplyType, ApplyType dateToApplyType);
        Task ApproveLeaveAsync(int applicationId);
        Task DenyLeaveAsync(int applicationId);
        Task<IEnumerable<LeaveApplication>> GetLeaveApplicationsByLvNumberAsync(string name);
        Task<LeaveApplication?> GetLeaveApplicationsByIdAsync(int applicationId);
        Task<IEnumerable<LeaveApplication>> GetLeaveApplicationsAsync(string employeeName, DateTime? dateFrom, DateTime? dateTo, ApprovalType? approvalType);
        Task<string> GenerateRefNumberAsync(string initialRef, string currentYear, string currentMonth);
        Task<double> GenerateTotalDaysAsync(DateTime dateFrom, bool? isHalfDayDateFrom, DateTime dateTo, bool? isHalfDayDateTo);
    }
}
