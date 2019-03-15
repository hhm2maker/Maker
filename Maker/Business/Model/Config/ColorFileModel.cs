using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace Maker.Business.Model.Config
{
    [XmlRoot("ColorFile")]
    public class ColorFileModel
    {
        [XmlArray("Colors"), XmlArrayItem("Color")]
        public Color[] Colors
        {
            get;
            set;
        }
    }

    [Serializable]
    public class Color
    {
        //XML创建时，生成当前类的属性 Name
        [XmlElement("Color", IsNullable = false)]
        public string Content;

        public Color()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Color(string parameter)
        {
            Content = parameter;
        }
    }



}
