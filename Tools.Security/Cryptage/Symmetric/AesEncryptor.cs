using System.Security.Cryptography;
using System.Text;

namespace Tools.Security.Cryptage.Symmetric
{
    public class AesEncryptor
    {
        private readonly byte[] _key;
        private readonly byte[] _vector;

        public byte[] Key
        {
            get
            {
                return _key;
            }
        }

        public byte[] Vector
        {
            get
            {
                return _vector;
            }
        }

        public AesEncryptor(SymmetricKeySizes keySize = SymmetricKeySizes.Size256)
        {
            Aes aes = Aes.Create();
            aes.KeySize = (int)keySize;

            aes.GenerateKey();
            aes.GenerateIV();

            _key = aes.Key;
            _vector = aes.IV;
        }

        public AesEncryptor(byte[] key, byte[] vector)
        {
            _key = key;
            _vector = vector;
        }

        public byte[] Encrypt(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            using(Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = Vector;

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

            Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = Vector;

            using (MemoryStream ms = new MemoryStream(value))
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader sw = new StreamReader(cs, Encoding.Unicode))
                    {
                        return sw.ReadToEnd();
                    }
                }
            }
        }

    }
}
