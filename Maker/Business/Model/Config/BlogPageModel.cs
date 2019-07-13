using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("BlogPage")]
    public class BlogPageModel
    {
        /// <summary>
        /// 当前版本
        /// </summary>
        [XmlElement("Dll", IsNullable = false)]
        public Dll dll
        {
            get;
            set;
        }

        [XmlType("Dll")]
        public class Dll
        {

            [XmlAttribute("url")]
            public string url
            {
                get;
                set;
            }

            [XmlAttribute("name")]
            public string name
            {
                get;
                set;
            }
        }

        [XmlArray("Buttons"), XmlArrayItem("Button")]
        public List<Button> Buttons
        {
            get;
            set;
        }

        [XmlType("Button")]
        public class Button
        {

            [XmlAttribute("hint")]
            public string hint
            {
                get;
                set;
            }

            [XmlAttribute("text")]
            public string text
            {
                get;
                set;
            }

            [XmlAttribute("details")]
            public string details
            {
                get;
                set;
            }

            [XmlArray("Parameters"), XmlArrayItem("Parameter")]
            public List<string> Parameters
            {
                get;
                set;
            }
        }
    }

   


}
