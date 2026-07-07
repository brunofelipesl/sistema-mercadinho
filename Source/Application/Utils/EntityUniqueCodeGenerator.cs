namespace Source.Application.Utils
{
    public static class EntityUniqueCodeGenerator
    {
        public static string GenerateUniqueCode<T>()
        {
            char prefix = typeof(T).Name.ToUpper()[0];
            string randomString = GenerateRandomString(5);
            return $"{prefix}{randomString}";
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}