using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Leaves.Interfaces
{
    public interface IDeleteLeaveUseCase
    {
        Task ExecuteAsync(Leave leaveId);
    }
}