using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OtobusumNerede.Shared.Apis;
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


        builder.Services.AddScoped(sp =>
            new HttpClient
            {
                BaseAddress = new Uri("https://api.ibb.gov.tr/iett/FiloDurum/")
            }
        );


        ConfigureRefit(builder.Services);

        await builder.Build().RunAsync();

        // refit Apis
        static void ConfigureRefit(IServiceCollection services)
        {
            string apiBaseUrl = "https://localhost:7048";

            services
                .AddRefitClient<IGetHatDurakApi>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

            services
                .AddRefitClient<IGetHatlarApi>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

            services
                .AddRefitClient<IGetSeferSaatleriApi>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

            //string hatOtobusUrl = "https://api.ibb.gov.tr/iett/FiloDurum/SeferGerceklesme";

            services
                .AddRefitClient<IGetHatOtobusApi>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

            services
                .AddRefitClient<IGetDuyurularApi>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

        }
    }
}