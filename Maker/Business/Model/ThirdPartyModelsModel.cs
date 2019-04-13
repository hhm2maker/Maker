using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model
{
    [XmlRoot("DetailedList")]
    public class ThirdPartyModelsModel
    {
        [XmlArray("Operations"), XmlArrayItem("Operation")]
        public List<ThirdPartyModel> ThirdPartyModels
        {
            get;
            set;
        }

        [XmlType("Operation")]
        public class ThirdPartyModel
        {
            [XmlAttribute]
            public String name
            {
                get;
                set;
            }

            [XmlAttribute]
            public String text
            {
                get;
                set;
            }

            [XmlAttribute]
            public String view
            {
                get;
                set;
            }

            [XmlAttribute]
            public String dll
            {
                get;
                set;
            }
            public ThirdPartyModel(String name, String text, String view, String dll)
            {
                this.name = name;
                this.text = text;
                this.view = view;
                this.dll = dll;
            }

            public ThirdPartyModel()
            {

            }
        }
    }
}
