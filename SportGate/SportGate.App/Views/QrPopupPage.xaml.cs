namespace SportGate.App.Views;

public partial class QrPopupPage : ContentPage
{
    public QrPopupPage(string payload, string base64Png)
    {
        InitializeComponent();
        BarcodeView.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
        BarcodeView.Options = new ZXing.Common.EncodingOptions { Height = 250, Width = 250, Margin = 0 };
        BarcodeView.BarcodeValue = payload;
    }

    private async void Close_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}