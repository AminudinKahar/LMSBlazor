using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Employees.Interfaces
{
    public interface IEditEmployeeUseCase
    {
        Task ExecuteAsync(Employee employee);
    }
}