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

    public class SellViewModel : BaseViewModel
    {
        private readonly ApiService _api;

        public ObservableCollection<EntryTypePrice> EntryTypes { get; } = new();
        public ObservableCollection<PersonCategoryPrice> Categories { get; } = new();

        private EntryTypePrice? _selectedType;

        public EntryTypePrice? SelectedType
        {
            get => _selectedType;
            set
            {
                Set(ref _selectedType, value);
                OnSelectedTypeChanged();
            }
        }

        public Dictionary<int, int> SelectedCounts { get; } = new();

        private decimal _total;
        public decimal Total { get => _total; set => Set(ref _total, value); }

        public DelegateCommand GenerateTicketCommand { get; }

        public SellViewModel(ApiService api)
        {
            _api = api;
            GenerateTicketCommand = new DelegateCommand(async _ => await GenerateTicketAsync());
        }

        public async Task InitializeAsync()
        {
            var types = await _api.GetEntryTypesAsync();
            EntryTypes.Clear();
            foreach (var t in types) EntryTypes.Add(t);

            var cats = await _api.GetPersonCategoriesAsync();
            Categories.Clear();
            foreach (var c in cats) Categories.Add(c);
        }

        private void OnSelectedTypeChanged()
        {
            SelectedCounts.Clear();
            Total = 0;
            // initialize counts to 0
            foreach (var c in Categories) SelectedCounts[c.Id] = 0;
        }

        public void SetCategoryCount(int categoryId, int qty)
        {
            SelectedCounts[categoryId] = qty;
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            if (SelectedType == null) { Total = 0; return; }
            decimal t = 0m;
            if (SelectedType.RequiresBaseFee) t += SelectedType.BaseFee;
            foreach (var kv in SelectedCounts)
            {
                var cat = Categories.FirstOrDefault(x => x.Id == kv.Key);
                if (cat != null) t += cat.Price * kv.Value;
            }
            Total = t;
        }

        private async Task GenerateTicketAsync()
        {
            if (SelectedType == null) return;

            // validation: if not allow multiple, ensure exactly one person selected
            if (!SelectedType.AllowMultiplePeople)
            {
                var sum = SelectedCounts.Values.Sum();
                if (sum != 1) { /* show error via UI binding */ return; }
            }

            var req = new CreateTicketRequest { EntryTypeCode = SelectedType.Code };
            foreach (var kv in SelectedCounts)
            {
                if (kv.Value <= 0) continue;
                req.People.Add(new CreateTicketPersonDto { PersonCategoryId = kv.Key, Quantity = kv.Value });
            }

            var res = await _api.CreateTicketAsync(req);
            if (res != null)
            {
                // raise event or navigate to popup: simplest is to store last ticket
                LastCreatedTicket = res;
            }
        }

        private TicketResponseDto? _lastCreated;
        public TicketResponseDto? LastCreatedTicket { get => _lastCreated; set => Set(ref _lastCreated, value); }
    }
}