using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Help")]
    public class HelpConfigModel
    {
        /// <summary>
        /// 当前版本
        /// </summary>
        [XmlElement("ExeFilePath", IsNullable = false)]
        public string ExeFilePath
        {
            get;
            set;
        } = "";
    }

   


}
