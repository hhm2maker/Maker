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

            [XmlAttribute("path")]
            public string Path
            {
                get;
                set;
            } = "";

            [XmlAttribute("enable")]
            public bool Enable
            {
                get;
                set;
            } = false;
        }
    }

   


}
