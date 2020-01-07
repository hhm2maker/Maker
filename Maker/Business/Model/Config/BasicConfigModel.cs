using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Basic")]
    public class BasicConfigModel
    {
        /// <summary>
        /// 范围列表显示数字
        /// </summary>
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
    }

   


}
