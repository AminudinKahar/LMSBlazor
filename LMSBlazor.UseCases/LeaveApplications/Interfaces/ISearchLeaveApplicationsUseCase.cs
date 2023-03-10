using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.LeaveApplications.Interfaces
{
    public interface ISearchLeaveApplicationsUseCase
    {
        Task<IEnumerable<LeaveApplication>> ExecuteAsync(string employeeName, DateTime? dateFrom, DateTime? dateTo, ApprovalType? approvalType);
    }
}