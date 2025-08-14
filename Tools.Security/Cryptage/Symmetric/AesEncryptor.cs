using System.Security.Cryptography;
using System.Text;

namespace Tools.Security.Cryptage.Symmetric
{
    public class AesEncryptor
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public byte[] Key
        {
            get
            {
                return _key;
            }
        }

        public byte[] IV
        {
            get
            {
                return _iv;
            }
        }

        public AesEncryptor(SymmetricKeySizes keySize = SymmetricKeySizes.Size256)
        {
            Aes aes = Aes.Create();
            aes.KeySize = (int)keySize;

            aes.GenerateKey();
            aes.GenerateIV();

            _key = aes.Key;
            _iv = aes.IV;
        }

        public AesEncryptor(byte[] key, byte[] vector)
        {
            _key = key;
            _iv = vector;
        }

        public byte[] Encrypt(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            using(Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs, Encoding.Unicode))
                        {
                            sw.Write(value);
                        }
                        return ms.ToArray();
                    }
                }
            }
        }

        public string Decrypt(byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (MemoryStream ms = new MemoryStream(value))
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs, Encoding.Unicode))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
