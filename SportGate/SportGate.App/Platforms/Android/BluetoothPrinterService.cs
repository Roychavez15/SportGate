namespace SportGate.App.Platforms.Android
{

    using global::Android.Bluetooth;
    using Java.Util;
    using SportGate.App.Helpers;
    using SportGate.App.Models;
    using SportGate.App.Services;

    public class BluetoothPrinterService : IPrinterService
    {
        private readonly string _mac;

        public BluetoothPrinterService(string mac)
        {
            _mac = mac;
        }

        public async Task PrintTicketAsync(TicketResponseDto ticket)
        {
            var adapter = BluetoothAdapter.DefaultAdapter;
            var device = adapter.GetRemoteDevice(_mac);

            var socket = device.CreateRfcommSocketToServiceRecord(
                UUID.FromString("00001101-0000-1000-8000-00805F9B34FB"));

            socket.Connect();
            var stream = socket.OutputStream;

            // 1️⃣ TEXTO
            var esc = new EscPosBuilder()
                .Init()
                .Center()
                .Bold(true)
                .Text("LIGA SAN JUAN")
                .Bold(false)
                .Text("Ticket de Ingreso")
                .Feed()
                .Left()
                .Text($"Tipo: {ticket.EntryType}")
                .Text($"Personas: {ticket.PeopleCount}")
                .Text($"Fecha: {ticket.CreatedAt:dd/MM/yyyy HH:mm:ss}")
                .Text($"Total: {ticket.TotalAmount:C}")
                .Feed()
                .Build();

            await stream.WriteAsync(esc);

            // 2️⃣ CENTRAR QR (MUY IMPORTANTE)
            await stream.WriteAsync(new byte[] { 0x1B, 0x61, 0x01 }); // ESC a 1 (center)
            await stream.WriteAsync(new byte[] { 0x0A });           // feed antes

            // 3️⃣ QR REAL (helper)
            await stream.WriteAsync(QrToEscPosHelper.BuildQr(ticket.ShortCode));

            // ⬇️ AVANZAR PAPEL
            stream.Write(new byte[]
            {
                0x0A, 0x0A, 0x0A, 0x0A, 0x0A, 0x0A, 0x0A
            });

            await Task.Delay(200);

            // ✂️ CORTE
            stream.Write(new byte[] { 0x1D, 0x56, 0x00 });

            stream.Flush();
            stream.Close();
            socket.Close();
        }
        public async Task PrintQrAsync(string qrText)
        {
            var adapter = BluetoothAdapter.DefaultAdapter;
            var device = adapter.GetRemoteDevice(_mac);

            var socket = device.CreateRfcommSocketToServiceRecord(
                UUID.FromString("00001101-0000-1000-8000-00805F9B34FB"));

            socket.Connect();
            var stream = socket.OutputStream;

            // 1️⃣ INIT + TEXTO
            var esc = new EscPosBuilder()
                .Init()
                .Center()
                .Bold(true)
                .Text("REIMPRESIÓN QR")
                .Bold(false)
                .Feed()
                .Build();

            await stream.WriteAsync(esc);

            // 2️⃣ FORZAR CENTRADO + FEED
            await stream.WriteAsync(new byte[] { 0x1B, 0x61, 0x01 }); // ESC a 1 (center)
            await stream.WriteAsync(new byte[] { 0x0A, 0x0A });       // feeds ANTES

            // 3️⃣ QR GRANDE (helper)
            await stream.WriteAsync(QrToEscPosHelper.BuildQr(qrText));

            // 4️⃣ FEEDS SUFICIENTES DESPUÉS DEL QR
            await stream.WriteAsync(new byte[]
            {
                0x0A, 0x0A, 0x0A, 0x0A, 0x0A, 0x0A
            });

            // 5️⃣ ESPERAR A QUE TERMINE DE IMPRIMIR
            await Task.Delay(300);

            // 6️⃣ CORTE FINAL
            await stream.WriteAsync(new byte[] { 0x1D, 0x56, 0x00 });

            stream.Flush();
            stream.Close();
            socket.Close();
        }


    }

}
