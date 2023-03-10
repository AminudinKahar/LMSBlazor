namespace LMSBlazor.UseCases.Common.Interface
{
    public interface IGenerateRefNumberUseCase
    {
        Task<string> ExecuteAsync(string initialRef, string currentYear, string currentMonth);
    }
}