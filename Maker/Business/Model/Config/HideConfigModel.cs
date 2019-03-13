using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Hide")]
    public class HideConfigModel
    {
        /// <summary>
        /// 范围列表显示数字
        /// </summary>
        [XmlElement("RangeListNumber", IsNullable = false)]
        public bool RangeListNumber
        {
            get;
            set;
        } = true;
    }

   


}
