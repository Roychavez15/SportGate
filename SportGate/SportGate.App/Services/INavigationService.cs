using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Services
{
    public interface INavigationService
    {
        Task PushModalAsync(Page page);

        Task PopModalAsync();

        Task NavigateToQrPopup(string shortCode);
    }
}