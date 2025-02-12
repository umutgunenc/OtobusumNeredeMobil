using Quartz;


namespace OtobusumNerede.Api.Services.Quartz
{
    public static class Tetikleyici
    {

        public static async Task ScheduleJobs(IServiceProvider services)
        {
            var schedulerFactory = services.GetRequiredService<ISchedulerFactory>();
            var scheduler = await schedulerFactory.GetScheduler();


            await scheduler.Start(); // Scheduler başlatılıyor

            var jobUpdateHatlar = JobBuilder.Create<UpdateHatlar>().Build();

            var jobUpdateDuraklar = JobBuilder.Create<UpdateDuraklar>().Build();

            DateTime now = DateTime.Now;
            DateTime scheduledTime = new DateTime(now.Year, now.Month, now.Day, 03, 30, 0);

            var triggerHatlar = TriggerBuilder.Create()
                .StartAt(scheduledTime.ToUniversalTime())
                //.StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .RepeatForever()) 
                .Build();

            var triggerDuraklar = TriggerBuilder.Create()
                .StartAt(scheduledTime.ToUniversalTime())
                //.StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobUpdateHatlar, triggerHatlar);
            await scheduler.ScheduleJob(jobUpdateDuraklar, triggerDuraklar);

        }


    }
}
