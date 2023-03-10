using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Leaves.Interfaces
{
    public interface IAddLeaveUseCase
    {
        Task ExecuteAsync(Leave leave);
    }
}