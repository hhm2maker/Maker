using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Maker.Model
{
    public class DeviceModel
    {
        public String DeviceType {
            get;
            set;
        }
        public Brush DeviceBackGround
        {
            get;
            set;
        }
        public String DeviceBackGroundStr
        {
            get;
            set;
        }
        public Double DeviceSize
        {
            get;
            set;
        }
        public bool IsMembrane
        {
            get;
            set;
        }
        
    }
}
