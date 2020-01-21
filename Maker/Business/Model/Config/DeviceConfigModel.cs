using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("Device")]
    public class DeviceConfigModel
    {

        [XmlArray("Devices"), XmlArrayItem("Device")]
        public List<Device> Devices
        {
            get;
            set;
        }

        [XmlType("Device")]
        public class Device
        {
            public Device() {

            }

            public Device(string deviceIn, string deviceOut, int channel) {
                DeviceIn = deviceIn;
                DeviceOut = deviceOut;
                Channel = channel;
            }

            [XmlAttribute("deviceIn")]
            public string DeviceIn
            {
                get;
                set;
            } = "";

            [XmlAttribute("deviceOut")]
            public string DeviceOut
            {
                get;
                set;
            } = "";

            [XmlAttribute("channel")]
            public int Channel
            {
                get;
                set;
            } = 0;
        }
    }

   


}
