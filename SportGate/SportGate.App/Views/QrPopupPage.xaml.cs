using SportGate.App.Helpers;
using SportGate.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Platform;

namespace SportGate.App.Views;

public partial class QrPopupPage : ContentPage
{
    private readonly string _qrText;
    private readonly IPrinterService _printerService;
    public QrPopupPage(string qrText)
    {
        InitializeComponent();
        _qrText = qrText;
        

        QrImage.Source = QrCodeHelper.GenerateQr(qrText);

        var services = IPlatformApplication.Current.Services;
        _printerService = services.GetRequiredService<IPrinterService>();

    }
    private async void Print_Clicked(object sender, EventArgs e)
    {
        try
        {
            await _printerService.PrintQrAsync(_qrText);

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    private async void Close_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}