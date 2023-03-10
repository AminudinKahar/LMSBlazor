using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Activities.Interfaces
{
    public interface IDenyLeaveUseCase
    {
        Task ExecuteAsync(int applicationId);
    }
}