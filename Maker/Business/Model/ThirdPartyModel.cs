using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Model
{
    public class ThirdPartyModel
    {
        public String name
        {
            get;
            set;
        }
        public String entext
        {
            get;
            set;
        }
        public String zhtext
        {
            get;
            set;
        }
        public String view
        {
            get;
            set;
        }
        public String dll
        {
            get;
            set;
        }
        public ThirdPartyModel(String name, String entext, String zhtext, String view, String dll) {
            this.name = name;
            this.entext = entext;
            this.zhtext = zhtext;
            this.view = view;
            this.dll = dll;
        }

        public ThirdPartyModel() {

        }
    }
}
