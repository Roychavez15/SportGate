using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Helpers
{
    using System.Text;

    public static class QrToEscPosHelper
    {
        public static byte[] BuildQr(string data)
        {
            var bytes = new List<byte>();

            // Modelo QR
            bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x04, 0x00, 0x31, 0x41, 0x32, 0x00 });

            // Tamaño
            bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x43, 0x0A });

            // Nivel corrección
            bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x45, 0x33 });

            var qrData = Encoding.UTF8.GetBytes(data);
            int len = qrData.Length + 3;

            bytes.AddRange(new byte[]
            {
            0x1D, 0x28, 0x6B,
            (byte)(len % 256),
            (byte)(len / 256),
            0x31, 0x50, 0x30
            });

            bytes.AddRange(qrData);

            // Imprimir
            bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x51, 0x30 });

            return bytes.ToArray();
        }

    }

}
