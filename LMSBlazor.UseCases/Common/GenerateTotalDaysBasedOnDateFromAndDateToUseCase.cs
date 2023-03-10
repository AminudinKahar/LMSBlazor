using LMSBlazor.UseCases.Common.Interface;
using LMSBlazor.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.UseCases.Common
{
    public class GenerateTotalDaysBasedOnDateFromAndDateTo : IGenerateTotalDaysBasedOnDateFromAndDateTo
    {
        private readonly ILeaveApplicationRepository leaveApplicationRepository;

        public GenerateTotalDaysBasedOnDateFromAndDateTo(ILeaveApplicationRepository leaveApplicationRepository)
        {
            this.leaveApplicationRepository=leaveApplicationRepository;
        }
        public async Task<double> ExecuteAsync(DateTime dateFrom, bool? isHalfDayDateFrom, DateTime dateTo, bool? isHalfDayDateTo)
        {
            dateTo = dateTo.AddDays(1);
            return await leaveApplicationRepository.GenerateTotalDaysAsync(dateFrom, isHalfDayDateFrom, dateTo, isHalfDayDateTo);
        }
    }
}
