using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGate.App.Helpers
{
    public static class QrCodeHelper
    {
        public static ImageSource GenerateQr(string text)
        {
            var generator = new QRCodeGenerator();
            var data = generator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

            var png = new PngByteQRCode(data);
            var bytes = png.GetGraphic(10);

            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }
    }
}
