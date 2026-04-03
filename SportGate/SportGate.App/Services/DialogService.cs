namespace SportGate.App.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowErrorAsync(string title, string message)
            => Application.Current.MainPage
                .DisplayAlert(title, message, "OK");

        public Task ShowInfoAsync(string title, string message)
            => Application.Current.MainPage
                .DisplayAlert(title, message, "OK");
    }

}
