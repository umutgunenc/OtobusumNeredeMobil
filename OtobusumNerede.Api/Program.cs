using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using OtobusumNerede.Api.Data;
using OtobusumNerede.Api.Mapping;
using OtobusumNerede.Api.Services;
using OtobusumNerede.Api.Services.Classes;
using OtobusumNerede.Api.Services.Interfaces;
using OtobusumNerede.Api.Services.Quartz;
using OtobusumNerede.Shared.DTOs;
using Quartz;
using System.Diagnostics;
using System.Globalization;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //builder.Services.AddCors(options =>
        //{
        //    var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
        //    options.AddDefaultPolicy(policy =>
        //        policy.WithOrigins(allowedOrigins)
        //              .AllowAnyHeader()
        //              .AllowAnyMethod());
        //});

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });


        builder.Services.AddDbContext<OtobusumNeredeDbContext>(options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DatabaseConnection"),
                sqlOptions =>
                {
                    sqlOptions.CommandTimeout(600); // Komut zaman aşımı süresi (saniye cinsinden)
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, // Maksimum yeniden deneme sayısı
                        maxRetryDelay: TimeSpan.FromSeconds(30), // Yeniden denemeler arası maksimum bekleme süresi
                        errorNumbersToAdd: null // İsteğe bağlı: Belirli hata numaraları için yeniden deneme
                    );
                }
            );
        });

        // Quartz yapılandırması
        builder.Services.AddQuartz();

        // Quartz Service eklenmesi
        builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        builder.Services.AddScoped<UpdateHatlar>();
        builder.Services.AddHttpClient<UpdateHatlar>();
        builder.Services.AddHttpClient<UpdateDuraklar>();

        //  servisler
        builder.Services.AddScoped<IHatServices, HatServices>();        
        builder.Services.AddScoped<ISeferServices, SeferServices>();
        builder.Services.AddScoped<IHatDurakServices, HatDurakServices>();
        builder.Services.AddScoped<IHatOtobusServices, HatOtobusServices>();
        builder.Services.AddScoped<IIettDuyurularServices, IettDuyurularServices>();
        builder.Services.AddScoped<IHatRotaServices,HatRotaServices>();


        builder.Services.AddScoped<List<HatDto>>();
        builder.Services.AddScoped<List<SeferSaatiDto>>();
        builder.Services.AddScoped<List<HatDurakDto>>();
        builder.Services.AddScoped<List<HatOtobusDto>>();
        builder.Services.AddScoped<List<DuyurularDto>>();   

        // mapper
        builder.Services.AddAutoMapper(typeof(MappingProfile));


        // Api Controller
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseStaticFiles();


        var supportedCultures = new[] { new CultureInfo("en-US") };
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-US"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
        app.UseCors("AllowAll");


        app.MapControllers();


        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            await Tetikleyici.ScheduleJobs(services);
        }



        app.Run();
    }
}