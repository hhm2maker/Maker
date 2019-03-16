using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Language")]
    public class LanguageConfigModel
    {
        /// <summary>
        /// 当前版本
        /// </summary>
        [XmlElement("MyLanguage", IsNullable = false)]
        public string MyLanguage
        {
            get;
            set;
        } 
    }

   


}
