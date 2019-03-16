using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("IsFirst")]
    public class IsFirstConfigModel
    {
        /// <summary>
        /// 是否是第一次
        /// </summary>
        [XmlElement("Value", IsNullable = false)]
        public bool Value
        {
            get;
            set;
        } = true; 
    }

   


}
