using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Blog")]
    public class BlogContentModel
    {
        /// <summary>
        /// 当前版本
        /// </summary>
        [XmlElement("Author", IsNullable = false)]
        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// 当前版本
        /// </summary>
        [XmlElement("HeadPortrait", IsNullable = false)]
        public string HeadPortrait
        {
            get;
            set;
        }

        /// <summary>
        /// 当前版本
        /// </summary>
        [XmlElement("Contact", IsNullable = false)]
        public string Contact
        {
            get;
            set;
        }

        /// <summary>
        /// 当前版本
        /// </summary>
        [XmlElement("Introduce", IsNullable = false)]
        public string Introduce
        {
            get;
            set;
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

            //[XmlAttribute("parameter")]
            //public string parameter
            //{
            //    get;
            //    set;
            //}

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
