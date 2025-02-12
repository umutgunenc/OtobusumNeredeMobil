using OtobusumNerede.Api.Data;
using Quartz;

namespace OtobusumNerede.Api.Services.Quartz
{
    public class UpdateDuraklar: IJob
    {
        private readonly HttpClient _httpClient;
        private readonly OtobusumNeredeDbContext _dbContext;

        public UpdateDuraklar(HttpClient httpClient, OtobusumNeredeDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }
        public Task Execute(IJobExecutionContext context)
        {
            UpdataDatabase updataDatabase = new(_httpClient, _dbContext);
            Task task = updataDatabase.UpdateDuraklarAsync();
            return task;
        }
    }
}
