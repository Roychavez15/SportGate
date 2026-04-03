using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Services
{
    public interface IDialogService
    {
        Task ShowErrorAsync(string title, string message);
        Task ShowInfoAsync(string title, string message);
    }
}
