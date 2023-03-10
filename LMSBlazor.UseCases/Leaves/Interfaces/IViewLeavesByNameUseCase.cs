using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Leaves.Interfaces
{
    public interface IViewLeavesByNameUseCase
    {
        Task<IEnumerable<Leave>> ExecuteAsync(string name = "");
    }
}