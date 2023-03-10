using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Leaves.Interfaces
{
    public interface IEditLeaveUseCase
    {
        Task ExecuteAsync(Leave leave);
    }
}