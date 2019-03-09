using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Paved")]
    public class PavedConfigModel
    {
        /// <summary>
        /// 平铺列数
        /// </summary>
        [XmlElement("Columns", IsNullable = false)]
        public int Columns
        {
            get;
            set;
        } = 5;

        /// <summary>
        /// 平铺最大个数
        /// </summary>
        [XmlElement("Max", IsNullable = false)]
        public int Max
        {
            get;
            set;
        } = 50;
    }

   


}
