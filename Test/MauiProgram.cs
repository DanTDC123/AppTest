using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Test.Services;

namespace Test
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
			builder.Services.AddMudServices();
            builder.Services.AddScoped(sp=> new HttpClient { BaseAddress = new Uri("https://localhost:7288") });
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
			builder.Services.AddScoped<ITaskServices, TaskServices>();


#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

			return builder.Build();
        }
    }
}
