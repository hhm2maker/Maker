using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Lock
{
    public interface IBindesh
    {
        string encode(string str);
        string decode(string str);
    }

    public class EncryptionDecryption : IBindesh
    {
        public string encode(string str)
        {
            string htext = "";

            for (int i = 0; i < str.Length; i++)
            {
                htext = htext + (char)(str[i] + 5 - 1 * 2);
            }
            return htext;
        }

        public string decode(string str)
        {
            string dtext = "";

            for (int i = 0; i < str.Length; i++)
            {
                dtext = dtext + (char)(str[i] - 5 + 1 * 2);
            }
            return dtext;
        }
    }
}
