using Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugLib
{
    public class InputAndOutputControlEnum
    {
        public enum KeyModel : int
        {
            KeyDown,
            KeyUp,
        }

        public delegate void SendLight(List<Light> light);
    }
}
