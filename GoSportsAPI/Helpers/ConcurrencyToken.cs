namespace GoSportsAPI.Helpers
{
    public class ConcurrencyToken
    {
        public static string ToBase64(byte[] bytes) => Convert.ToBase64String(bytes);
        public static byte[] FromBase64(string s) => Convert.FromBase64String(s);
    }
}
