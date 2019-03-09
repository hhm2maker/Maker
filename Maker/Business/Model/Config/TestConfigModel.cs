using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Test")]
    public class TestConfigModel
    {
        /// <summary>
        /// 透明度
        /// </summary>
        [XmlElement("Opacity", IsNullable = false)]
        public int Opacity
        {
            get;
            set;
        } = 100;
    }

   


}
