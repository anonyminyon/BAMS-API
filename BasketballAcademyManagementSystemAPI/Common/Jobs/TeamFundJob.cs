using BasketballAcademyManagementSystemAPI.Application.Services;
using Quartz;

namespace BasketballAcademyManagementSystemAPI.Common.Jobs
{
    public class TeamFundJob : IJob
    {
        private readonly ITeamFundService _teamFundService;

        public TeamFundJob(ITeamFundService teamFundService)
        {
            _teamFundService = teamFundService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($" [TeamFundJob] Đã chạy lúc: {DateTime.Now}");
            //truyền vào false để kiểm tra điều kiện ngày 1 hàng tháng mới tạo teamfund
            await _teamFundService.AutoCreateTeamFundsAsync(false);
            
        }
    }
}
