﻿using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Blog")]
    public class BlogConfigModel
    {

        [XmlArray("Shortcuts"), XmlArrayItem("Shortcut")]
        public List<Shortcut> Shortcuts
        {
            get;
            set;
        }

        [XmlType("Shortcut")]
        public class Shortcut
        {
            public Shortcut() {

            }

            public Shortcut(string text, string url, string dll) {
                this.text = text;
                this.url = url;
                this.dll = dll;
            }

            [XmlAttribute("text")]
            public string text
            {
                get;
                set;
            } = "";

            [XmlAttribute("url")]
            public string url
            {
                get;
                set;
            } = "";

            [XmlAttribute("dll")]
            public string dll
            {
                get;
                set;
            } = "";
        }
    }

   


}
