using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OtobusumNerede.Shared.Apis;
using OtobusumNerede.Shared.UIServices.Services;
using OtobusumNerede.Shared.UIServices.Services.Interfaces;
using OtobusumNerede.SharedComponents;
using Refit;



internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // cache islemleri icin
        builder.Services.AddBlazoredLocalStorage();

        builder.Services.AddBlazorBootstrap();

        builder.Services.AddHttpClient<IHatOtobusServices, HatOtobusServices>();


        ConfigureRefit(builder.Services);

        await builder.Build().RunAsync();

        // refit Apis
        static void ConfigureRefit(IServiceCollection services)
        {
            //string apiBaseUrl = "https://localhost:7048";
            string apiBaseUrl = "https://otobusumnerede.runasp.net/";

            services
                .AddRefitClient<IGetHatDurakApi>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

            services
                .AddRefitClient<IGetHatlarApi>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

            services
                .AddRefitClient<IGetSeferSaatleriApi>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));


            services
                .AddRefitClient<IGetHatOtobusApi>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

            services
                .AddRefitClient<IGetDuyurularApi>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

        }
    }
}