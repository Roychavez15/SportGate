using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Services
{

    using SportGate.App.Helpers;
    using SportGate.App.Models;
    using System.Net.Sockets;

    public class NetworkPrinterService : IPrinterService
    {
        private readonly string _ip;
        private readonly int _port;

        public NetworkPrinterService(string ip, int port = 9100)
        {
            _ip = ip;
            _port = port;
        }

        public async Task PrintTicketAsync(TicketResponseDto ticket)
        {
            var esc = new EscPosBuilder()
                .Init()
                .Center()
                .Bold(true)
                .Text("LIGA SAN JUAN")
                .Bold(false)
                .Text("Ticket de Ingreso")
                .Feed()
                .Left()
                .Text($"Código: {ticket.entryTypePrice.Description}")
                .Text($"Personas: {ticket.PeopleCount}")
                .Text($"Fecha: {ticket.CreatedAt:dd/MM/yyyy HH:mm}")
                .Text($"Total: {ticket.TotalAmount:C}")
                .Feed()
                .Build();

            var qr = QrToEscPosHelper.BuildQr(ticket.ShortCode);

            using var client = new TcpClient();
            await client.ConnectAsync(_ip, _port);
            using var stream = client.GetStream();

            await stream.WriteAsync(esc);
            await stream.WriteAsync(qr);
            await stream.WriteAsync(new byte[] { 0x0A, 0x0A, 0x1D, 0x56, 0x00 });
        }
        public async Task PrintQrAsync(string qrText)
        {

        }
    }

}
