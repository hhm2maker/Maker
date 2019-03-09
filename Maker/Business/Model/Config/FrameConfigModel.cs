using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("frame")]
    public class FrameConfigModel
    {
        public Style style { get; set; }
    }

    [XmlType("style")]
    public class Style
    {
        [XmlAttribute]
        public double size;

        [XmlAttribute]
        public double x;

        [XmlAttribute]
        public double y;
    }
}
