using LMSBlazor.CoreBusiness;

namespace LMSBlazor.UseCases.Activities.Interfaces
{
    public interface IApplyLeaveUseCase
    {
        Task ExecuteAsync(string lvNumber, Employee employee, Leave leave, double totalDaysApplied, string doneBy, DateTime dateFrom, DateTime dateTo, ApplyType dateFromHalfDay, ApplyType dateToHalfDay);
    }
}