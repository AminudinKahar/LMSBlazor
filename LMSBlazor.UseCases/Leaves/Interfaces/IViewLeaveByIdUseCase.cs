using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Leaves.Interfaces
{
    public interface IViewLeaveByIdUseCase
    {
        Task<Leave> ExecuteAsync(int leaveId);
    }
}