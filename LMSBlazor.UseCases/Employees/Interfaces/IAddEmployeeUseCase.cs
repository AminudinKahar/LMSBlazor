using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Employees.Interfaces
{
    public interface IAddEmployeeUseCase
    {
        Task ExecuteTask(Employee employee);
    }
}