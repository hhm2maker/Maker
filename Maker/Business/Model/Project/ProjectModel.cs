using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Root")]
    public class ProjectModel
    {
        /// <summary>
        /// 透明度
        /// </summary>
        [XmlElement("Bpm", IsNullable = false)]
        public string Bpm
        {
            get;
            set;
        } = "";
    }

   


}
