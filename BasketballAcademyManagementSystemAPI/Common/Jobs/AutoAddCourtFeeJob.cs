using BasketballAcademyManagementSystemAPI.Application.Services;
using Quartz;

namespace BasketballAcademyManagementSystemAPI.Common.Jobs
{
    public class AutoAddCourtFeeJob : IJob
    {
        private readonly ITeamFundService _teamFundService;

        public AutoAddCourtFeeJob(ITeamFundService teamFundService)
        {
            _teamFundService = teamFundService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($" [AutoAddCourtFeeJob] Đã chạy lúc: {DateTime.Now}");
            await _teamFundService.AutoAddExpenditureCourtForTeamFundsAsync();
        }
    }

}

