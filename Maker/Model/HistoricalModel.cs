using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Model
{
    public class HistoricalModel
    {
        public String Path
        {
            get;
            set;
        }
        public HistoricalModel(String path)
        {
            Path = path;
        }
    }
}
