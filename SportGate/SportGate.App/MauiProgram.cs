namespace SportGate.App
{

    using CommunityToolkit.Maui;
    using Microsoft.Maui.Hosting;

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
            string apiBase = "http://181.39.104.93:5021";

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
            builder.Services.AddSingleton<IDialogService, DialogService>();

#if ANDROID
            builder.Services.AddSingleton<IPrinterService>(sp =>
                new SportGate.App.Platforms.Android.BluetoothPrinterService(
                    "DC:0D:30:DE:4F:98"));
            #endif
            return builder.Build();
        }
    }
}