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
        [XmlElement("ShortcutName", IsNullable = false)]
        public string ShortcutName
        {
            get;
            set;
        }


        /// <summary>
        /// 当前版本
        /// </summary>
        [XmlElement("BaseUrl", IsNullable = false)]
        public string BaseUrl
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

        
        [XmlArray("Pages"), XmlArrayItem("Page")]
        public List<Page> Pages
        {
            get;
            set;
        }

        [XmlType("Page")]
        public class Page
        {

            [XmlAttribute("type")]
            public string type
            {
                get;
                set;
            }

            [XmlAttribute("url")]
            public string url
            {
                get;
                set;
            }
        }

        [XmlArray("Contacts"), XmlArrayItem("Contact")]
        public List<Contact> Contacts
        {
            get;
            set;
        }

        [XmlType("Contact")]
        public class Contact
        {

            [XmlAttribute("type")]
            public string type
            {
                get;
                set;
            }

            [XmlAttribute("content")]
            public string content
            {
                get;
                set;
            }
        }
    }

   


}
