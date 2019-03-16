using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("ColorFile")]
    public class ColorFileModel
    {
        [XmlArray("Colors"), XmlArrayItem("Color")]
        public String[] Colors
        {
            get;
            set;
        }
    }
}
