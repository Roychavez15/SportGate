namespace SportGate.App.Services
{
    using System.Text;

    public class EscPosBuilder
    {
        private readonly List<byte> _buffer = new();

        public EscPosBuilder Init()
        {
            _buffer.AddRange(new byte[] { 0x1B, 0x40 }); // ESC @
            return this;
        }

        public EscPosBuilder Center()
        {
            _buffer.AddRange(new byte[] { 0x1B, 0x61, 0x01 });
            return this;
        }

        public EscPosBuilder Left()
        {
            _buffer.AddRange(new byte[] { 0x1B, 0x61, 0x00 });
            return this;
        }

        public EscPosBuilder Bold(bool on)
        {
            _buffer.AddRange(new byte[] { 0x1B, 0x45, (byte)(on ? 1 : 0) });
            return this;
        }

        public EscPosBuilder Text(string text)
        {
            _buffer.AddRange(Encoding.UTF8.GetBytes(text + "\n"));
            return this;
        }

        public EscPosBuilder Feed(int lines = 1)
        {
            for (int i = 0; i < lines; i++)
                _buffer.Add(0x0A);
            return this;
        }

        public EscPosBuilder Cut()
        {
            _buffer.AddRange(new byte[] { 0x1D, 0x56, 0x00 });
            return this;
        }

        public byte[] Build() => _buffer.ToArray();
    }

}
