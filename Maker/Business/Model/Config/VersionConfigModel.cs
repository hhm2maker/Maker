using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Version")]
    public class VersionConfigModel
    {
        /// <summary>
        /// 当前版本
        /// </summary>
        [XmlElement("NowVersion", IsNullable = false)]
        public string NowVersion
        {
            get;
            set;
        } 
    }

   


}
