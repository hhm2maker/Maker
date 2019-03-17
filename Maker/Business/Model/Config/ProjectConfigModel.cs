using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Project")]
    public class ProjectConfigModel
    {
        /// <summary>
        /// 当前路径
        /// </summary>
        [XmlElement("Path", IsNullable = false)]
        public string Path
        {
            get;
            set;
        }
    }

   


}
