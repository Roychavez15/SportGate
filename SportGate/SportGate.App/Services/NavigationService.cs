using SportGate.App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Services
{
    public class NavigationService : INavigationService
    {
        public async Task PushModalAsync(Page page)
        {
            await Shell.Current.Navigation.PushModalAsync(page);
        }

        public async Task PopModalAsync()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        public async Task NavigateToQrPopup(string shortCode)
        {
            await Shell.Current.Navigation.PushModalAsync(new QrPopupPage(shortCode));
        }
    }
}