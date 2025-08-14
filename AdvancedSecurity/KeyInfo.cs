using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSecurity
{
    public class KeyInfo
    {
        public KeyInfo(byte[] key, byte[] iv)
        {
            Key = key;
            IV = iv;
        }

        public byte[] Key { get; }
        public byte[] IV { get; }
    }
}
