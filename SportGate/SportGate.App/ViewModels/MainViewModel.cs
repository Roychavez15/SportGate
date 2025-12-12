using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public SellViewModel SellVM { get; }
        public HistoryViewModel HistoryVM { get; }

        public MainViewModel(SellViewModel sell, HistoryViewModel history)
        {
            SellVM = sell;
            HistoryVM = history;
        }
    }
}