using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Basic")]
    public class BasicConfigModel
    {
        [XmlElement("Model", IsNullable = false)]
        public ModelType Model
        {
            get;
            set;
        }

        public enum ModelType
        {
            PC = 0,
            TabletPC = 1,
        }

        [XmlElement("UseCache", IsNullable = false)]
        public bool UseCache
        {
            get;
            set;
        } = true;
    }

   


}
