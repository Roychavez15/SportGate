using Microsoft.Extensions.Logging;

namespace SportGate.App
{
    //    public static class MauiProgram
    //    {
    //        public static MauiApp CreateMauiApp()
    //        {
    //            var builder = MauiApp.CreateBuilder();
    //            builder
    //                .UseMauiApp<App>()
    //                .ConfigureFonts(fonts =>
    //                {
    //                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
    //                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
    //                });

    //#if DEBUG
    //    		builder.Logging.AddDebug();
    //#endif

    //            return builder.Build();
    //        }
    //    }
    using Microsoft.Maui.Hosting;
    using CommunityToolkit.Maui;
    using SportGate.App.Services;
    using SportGate.App.ViewModels;
    using SportGate.App.Views;

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts => { });

            // Configura base URL aquí (cambia por tu endpoint)
            string apiBase = "https://tu-backend.example.com";

            builder.Services.AddSingleton(new ApiService(apiBase));

            // ViewModels
            builder.Services.AddSingleton<SellViewModel>();
            builder.Services.AddSingleton<HistoryViewModel>();
            builder.Services.AddSingleton<MainViewModel>();

            // Pages
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<SellPage>();
            builder.Services.AddTransient<HistoryPage>();
            builder.Services.AddTransient<QrPopupPage>();

            builder.Services.AddSingleton<INavigationService, NavigationService>();

            return builder.Build();
        }
    }
}