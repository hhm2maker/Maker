using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model
{
    [XmlRoot("frame")]
    public class FrameModel
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
