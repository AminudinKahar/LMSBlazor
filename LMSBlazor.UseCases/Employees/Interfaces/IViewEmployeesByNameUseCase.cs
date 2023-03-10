using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Employees.Interfaces
{
    public interface IViewEmployeesByNameUseCase
    {
        Task<IEnumerable<Employee>> ExecuteAsync(string name = "");
    }
}