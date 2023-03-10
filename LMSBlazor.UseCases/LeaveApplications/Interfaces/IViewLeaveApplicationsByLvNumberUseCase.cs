using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.LeaveApplications.Interfaces
{
    public interface IViewLeaveApplicationsByLvNumberUseCase
    {
        Task<IEnumerable<LeaveApplication>> ExecuteAsync(string name = "");
    }
}