using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Activities.Interfaces
{
    public interface IApproveLeaveUseCase
    {
        Task ExecuteAsync(int applicationId);
    }
}