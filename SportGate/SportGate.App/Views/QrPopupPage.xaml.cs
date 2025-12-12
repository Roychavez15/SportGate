using SportGate.App.Helpers;

namespace SportGate.App.Views;

public partial class QrPopupPage : ContentPage
{
    public QrPopupPage(string qrText)
    {
        InitializeComponent();
        QrImage.Source = QrCodeHelper.GenerateQr(qrText);
    }

    private async void Close_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}