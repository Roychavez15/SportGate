namespace SportGate.API.Helpers
{
    public static class ShortCodeGenerator
    {
        private static Random R = new Random();

        public static string Generate(int length = 6)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[R.Next(s.Length)]).ToArray());
        }
    }
}