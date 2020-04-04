using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Plugs")]
    public class PlugsConfigModel
    {

        [XmlArray("MyPlugs"), XmlArrayItem("Plug")]
        public List<Plug> Plugs
        {
            get;
            set;
        }

        [XmlType("Plug")]
        public class Plug
        {
            public Plug() {

            }

            public Plug(string path) {
                Path = path;
            }

            [XmlAttribute("path")]
            public string Path
            {
                get;
                set;
            } = "";

          
        }
    }

   


}
