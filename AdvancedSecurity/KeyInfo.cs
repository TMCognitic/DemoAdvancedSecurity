using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSecurity
{
    public class KeyInfo
    {
        public KeyInfo(byte[] key, byte[] vector)
        {
            Key = key;
            Vector = vector;
        }

        public byte[] Key { get; }
        public byte[] Vector { get; }
    }
}
