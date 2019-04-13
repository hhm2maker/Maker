using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model
{
    [XmlRoot("Setup")]
    public class ThirdPartySetupsModel
    {

        [XmlArray("Operations"), XmlArrayItem("Operation")]
        public List<ThirdPartySetupModel> ThirdPartySetupModels
        {
            get;
            set;
        }

        [XmlType("Operation")]
        public class ThirdPartySetupModel
        {

            [XmlElement("Name", IsNullable = false)]
            public string Name
            {
                get;
                set;
            }

            [XmlElement("Text", IsNullable = false)]
            public string Text
            {
                get;
                set;
            }

            [XmlElement("View", IsNullable = false)]
            public string View
            {
                get;
                set;
            }

            [XmlElement("Dll", IsNullable = false)]
            public string Dll
            {
                get;
                set;
            }
        }

    }

   


}
