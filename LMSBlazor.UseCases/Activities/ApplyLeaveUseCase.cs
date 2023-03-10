using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.Activities.Interfaces;
using LMSBlazor.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.UseCases.Activities
{
    public class ApplyLeaveUseCase : IApplyLeaveUseCase
    {
        private readonly ILeaveApplicationRepository LeaveApplicationRepository;

        public ApplyLeaveUseCase(ILeaveApplicationRepository LeaveApplicationRepository)
        {
            this.LeaveApplicationRepository=LeaveApplicationRepository;
        }
        public async Task ExecuteAsync(string lvNumber,Employee employee,Leave leave,double totalDaysApplied,string doneBy, DateTime dateFrom, DateTime dateTo, ApplyType dateFromApplyType, ApplyType dateToApplyType)
        {
            await this.LeaveApplicationRepository.ApplyLeaveAsync(lvNumber,employee,leave,totalDaysApplied,doneBy, dateFrom,dateTo, dateFromApplyType,dateToApplyType);

        }
    }
}
