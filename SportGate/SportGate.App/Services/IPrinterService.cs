using SportGate.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Services
{
    public interface IPrinterService
    {
        Task PrintTicketAsync(TicketResponseDto ticket);
        Task PrintQrAsync(string qrText);

    }
}
