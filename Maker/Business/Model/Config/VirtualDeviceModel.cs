using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("VirtualDevice")]
    public class VirtualDeviceModel
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        [XmlElement("DeviceType", IsNullable = false)]
        public DeviceType MyDeviceType
        {
            get;
            set;
        }

        public enum DeviceType
        {
            LaunchpadPro = 0,
        }

        /// <summary>
        /// 设备背景 - 可以是颜色#000000/#00000000,或是图片
        /// </summary>
        [XmlElement("DeviceBackGround", IsNullable = false)]
        public string DeviceBackGround
        {
            get;
            set;
        }

        /// <summary>
        /// 是否贴膜
        /// </summary>
        [XmlElement("IsMembrane", IsNullable = false)]
        public bool IsMembrane
        {
            get;
            set;
        } = true;
    }

   


}
