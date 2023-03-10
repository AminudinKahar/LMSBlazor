using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Employees.Interfaces
{
    public interface IViewEmployeeByIdUseCase
    {
        Task<Employee?> ExecuteAsync(int employeeId);
    }
}