using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Employees.Interfaces
{
    public interface IDeleteEmployeeUseCase
    {
        Task ExecuteAsync(Employee employee);
    }
}