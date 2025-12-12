using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Services
{
    using SportGate.App.Models;
    using System.Net.Http.Json;

    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(string baseUrl)
        {
            _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<List<EntryTypePrice>> GetEntryTypesAsync()
        => await GetJson<List<EntryTypePrice>>("/api/EntryTypePrice");

        public async Task<List<PersonCategoryPrice>> GetPersonCategoriesAsync()
        => await GetJson<List<PersonCategoryPrice>>("/api/PersonCategoryPrice");

        public async Task<TicketResponseDto?> CreateTicketAsync(CreateTicketRequest req)
        {
            var r = await _http.PostAsJsonAsync("/api/Tickets/create", req);
            if (!r.IsSuccessStatusCode) return null;
            return await r.Content.ReadFromJsonAsync<TicketResponseDto>();
        }

        public async Task<List<TicketResponseDto>> GetTodayTicketsAsync()
        => await GetJson<List<TicketResponseDto>>("/api/Tickets/today");

        private async Task<T?> GetJson<T>(string url)
        {
            try
            {
                var r = await _http.GetAsync(url);
                if (!r.IsSuccessStatusCode) return default;
                return await r.Content.ReadFromJsonAsync<T>() ?? default!;
            }
            catch
            {
                return default!;
            }
        }
    }
}