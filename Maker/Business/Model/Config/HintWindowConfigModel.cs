using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("HintWindow")]
    public class HintWindowConfigModel
    {
        [XmlElement("IsFirst", IsNullable = false)]
        public bool IsFirst
        {
            get;
            set;
        }

        [XmlElement("Top", IsNullable = false)]
        public int Top
        {
            get;
            set;
        }

        [XmlElement("Left", IsNullable = false)]
        public int Left
        {
            get;
            set;
        }

        [XmlElement("Width", IsNullable = false)]
        public int Width
        {
            get;
            set;
        }

        [XmlElement("Height", IsNullable = false)]
        public int Height
        {
            get;
            set;
        }

        [XmlElement("Position", IsNullable = false)]
        public int Position
        {
            get;
            set;
        }
    }
}
