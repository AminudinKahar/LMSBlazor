using LMSBlazor.UseCases.Common.Interface;
using LMSBlazor.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.UseCases.Common
{
    public class GenerateRefNumberUseCase : IGenerateRefNumberUseCase
    {
        private readonly ILeaveApplicationRepository leaveApplicationRepository;

        public GenerateRefNumberUseCase(ILeaveApplicationRepository leaveApplicationRepository)
        {
            this.leaveApplicationRepository=leaveApplicationRepository;
        }
        public async Task<string> ExecuteAsync(string initialRef, string currentYear, string currentMonth)
        {
            return await leaveApplicationRepository.GenerateRefNumberAsync(initialRef, currentYear, currentMonth);
        }
    }
}
