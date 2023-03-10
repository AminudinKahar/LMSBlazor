using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.LeaveApplications.Interfaces
{
    public interface IViewLeaveApplicationByIdUseCase
    {
        Task<LeaveApplication?> ExecuteAsync(int applicationId);
    }
}