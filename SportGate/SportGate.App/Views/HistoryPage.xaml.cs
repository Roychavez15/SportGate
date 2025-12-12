using SportGate.App.Models;
using SportGate.App.ViewModels;

namespace SportGate.App.Views;

public partial class HistoryPage : ContentPage
{
    private readonly HistoryViewModel _vm;

    public HistoryPage(HistoryViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
        Loaded += HistoryPage_Loaded;
    }

    private async void HistoryPage_Loaded(object sender, EventArgs e)
    {
        await _vm.LoadAsync();
    }

    private async void TicketsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var sel = e.CurrentSelection.FirstOrDefault() as TicketResponseDto;
        if (sel == null) return;
        var page = new QrPopupPage(sel.ShortCode);
        await Navigation.PushModalAsync(page);
    }
}