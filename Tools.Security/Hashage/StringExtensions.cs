using System.Security.Cryptography;
using System.Text;

namespace Tools.Security.Hashage
{
    public static class StringExtensions
    {
        public static byte[] Hash(this string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            byte[] valueAsByteArray = Encoding.Unicode.GetBytes(value);
            return SHA512.HashData(valueAsByteArray);
        }
    }
}
