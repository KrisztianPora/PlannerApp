using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PlannerApp.Shared.Services;
using Blazor.FileReader;

namespace PlannerApp.Client
{
    public class Program
    {
        private const string URL = "https://localhost:9000";

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp =>
            {
                return new AuthenticationService(URL);
            });

            builder.Services.AddScoped(sp =>
            {
                return new PlansService(URL);
            });

            builder.Services.AddScoped(sp =>
            {
                return new ToDoItemsService(URL);
            });

            builder.Services.AddFileReaderService(options =>
            {
                options.UseWasmSharedBuffer = true;
            });

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, LocalAuthenticationStateProvider>();

            builder.Services.AddTransient(sp =>
                new HttpClient
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                });

            await builder.Build().RunAsync();
        }
    }
}
