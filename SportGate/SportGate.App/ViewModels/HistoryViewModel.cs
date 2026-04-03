using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.ViewModels
{
    using SportGate.App.Helpers;
    using SportGate.App.Models;
    using SportGate.App.Services;
    using System.Collections.ObjectModel;

    public class HistoryViewModel : BaseViewModel
    {
        private readonly ApiService _api;
        public ObservableCollection<TicketResponseDto> Tickets { get; } = new();

        public DelegateCommand RefreshCommand { get; }
        private decimal _dayTotal;
        public decimal DayTotal
        {
            get => _dayTotal;
            set => Set(ref _dayTotal, value);
        }
        public HistoryViewModel(ApiService api)
        {
            _api = api;
            RefreshCommand = new DelegateCommand(async _ => await LoadAsync());
        }

        public async Task LoadAsync()
        {
            var list = await _api.GetTodayTicketsAsync();
            Tickets.Clear();
            foreach (var t in list.OrderByDescending(x => x.Id)) Tickets.Add(t);
            DayTotal = Tickets.Sum(x => x.TotalAmount);
        }
    }
}