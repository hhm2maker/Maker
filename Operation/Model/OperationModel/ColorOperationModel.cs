using Operation.Model.OperationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class ColorOperationModel : BaseNoOperationModel
    {
        public String HintString
        {
            get;
            set;
        }

        public List<int> Colors
        {
            get;
            set;
        } = new List<int>();

        public ColorOperationModel()
        {

        }

        public ColorOperationModel(List<int> colors)
        {
            Colors = colors;
        }

        public override void SetXElement(XElement xEdit)
        {
            if (xEdit.Attribute("colors") != null && !xEdit.Attribute("colors").Value.ToString().Equals(String.Empty))
            {
                String colors = xEdit.Attribute("colors").Value;
                String[] strsColor = colors.Split(' ');
                foreach (var item in strsColor)
                {
                    if (int.TryParse(item, out int color))
                    {
                        Colors.Add(color);
                    }
                }
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement(OperationName);
           
            StringBuilder sb = new StringBuilder();
            for (int count = 0; count < Colors.Count; count++)
            {
                if (count !=Colors.Count - 1)
                    sb.Append(Colors[count] + " ");
                else
                {
                    sb.Append(Colors[count]);
                }
            }
            xVerticalFlipping.SetAttributeValue("colors", sb.ToString());
            return xVerticalFlipping;
        }
    }
}
