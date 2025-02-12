using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OtobusumNerede.Shared.UIServices;
using OtobusumNerede.Web;
using OtobusumNerede.Web.Apis;
using Refit;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// cache islemleri icin
builder.Services.AddBlazoredLocalStorage();

//TODO UI tarafýndan otobusKonnumlarina istek atilacak
//UI services
builder.Services.AddScoped<IUI_HatOtobusServices, UI_HatOtobusServices>();
builder.Services.AddHttpClient<UI_HatOtobusServices>();


ConfigureRefit(builder.Services);

await builder.Build().RunAsync();

// refit Apis
static void ConfigureRefit(IServiceCollection services)
{
    string apiBaseUrl = "https://localhost:7048";
    string apiUIBaseUrl = "https://localhost:7047";


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

    services
        .AddRefitClient<IGetHatOtobusApi>()
        .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

    services
        .AddRefitClient<IUIGetHatOtobusApi>()
        .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiUIBaseUrl));   
}