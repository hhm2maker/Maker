using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class ModelExportModel : BaseOperationModel
    {
        //0 - Live模式
        //1 - 普通模式
        public int Model
        {
            get;
            set;
        }


        public ModelExportModel()
        {

        }

        public ModelExportModel(int model) {
            Model = model;
        }

        public ModelExportModel(bool isLive)
        {
            Model = isLive ? 0 : 1;
        }

        public override void SetXElement(XElement xEdit)
        {
            if (int.TryParse(xEdit.Value, out int model)) {
                Model = model;
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("Model");
            xVerticalFlipping.SetValue(Model);

            return xVerticalFlipping;
        }
    }
}
