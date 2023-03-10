using LMSBlazor.CoreBusiness;
using LMSBlazor.UseCases.LeaveApplications.Interfaces;
using LMSBlazor.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.UseCases.LeaveApplications
{
    public class SearchLeaveApplicationsUseCase : ISearchLeaveApplicationsUseCase
    {
        private readonly ILeaveApplicationRepository leaveApplicationRepository;

        public SearchLeaveApplicationsUseCase(ILeaveApplicationRepository leaveApplicationRepository)
        {
            this.leaveApplicationRepository=leaveApplicationRepository;
        }
        public async Task<IEnumerable<LeaveApplication>> ExecuteAsync(
            string employeeName,
            DateTime? dateFrom,
            DateTime? dateTo,
            ApprovalType? approvalType)
        {
            if (dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);

            return await this.leaveApplicationRepository.GetLeaveApplicationsAsync(
                employeeName,
                dateFrom,
                dateTo,
                approvalType);
        }
    }
}
