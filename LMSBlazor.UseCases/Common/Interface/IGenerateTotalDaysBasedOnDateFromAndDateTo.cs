namespace LMSBlazor.UseCases.Common.Interface
{
    public interface IGenerateTotalDaysBasedOnDateFromAndDateTo
    {
        Task<double> ExecuteAsync(DateTime dateFrom, bool? isHalfDayDateFrom, DateTime dateTo, bool? isHalfDayDateTo);
    }
}