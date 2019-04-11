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
        
        public String text
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
        public ThirdPartyModel(String name, String text, String view, String dll) {
            this.name = name;
            this.text = text;
            this.view = view;
            this.dll = dll;
        }

        public ThirdPartyModel() {

        }
    }
}
