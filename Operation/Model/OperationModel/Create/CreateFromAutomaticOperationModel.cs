using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class CreateFromAutomaticOperationModel : CreateOperationModel
    {
        public enum AutomaticType
        {
            RhombusDiffusion = 0,
            Cross = 1,
            RandomFountain = 2
        };

        public AutomaticType MyAutomaticType
        {
            get;
            set;
        }
        private BaseAutomatic myBaseAutomatic;
        public BaseAutomatic MyBaseAutomatic
        {
            get
            {
                return myBaseAutomatic;
            }
            set {
                myBaseAutomatic = value;
                if (value is RhombusDiffusionAutomaticOperationModel)
                {
                    MyAutomaticType = AutomaticType.RhombusDiffusion;
                }
                if (value is CrossAutomaticOperationModel)
                {
                    MyAutomaticType = AutomaticType.Cross;
                }
                if (value is RandomFountainAutomaticOperationModel)
                {
                    MyAutomaticType = AutomaticType.RandomFountain;
                }
            }
        }


        public CreateFromAutomaticOperationModel()
        {

        }

        public CreateFromAutomaticOperationModel( BaseAutomatic baseAutomatic)
        {
            MyBaseAutomatic = baseAutomatic;
        }

        [Serializable]
        public class BaseAutomatic {

        }

        [Serializable]
        public class BaseOneNumberAutomatic : BaseAutomatic
        {
            public int Position
            {
                get;
                set;
            }

            public BaseOneNumberAutomatic()
            {
           
            }

            public BaseOneNumberAutomatic(int position)
            {
                Position = position;
            }
        }

        [Serializable]
        public class RhombusDiffusionAutomaticOperationModel : BaseOneNumberAutomatic
        {
            public RhombusDiffusionAutomaticOperationModel(int position) : base(position){ }
        }

        [Serializable]
        public class CrossAutomaticOperationModel : BaseOneNumberAutomatic
        {
            public CrossAutomaticOperationModel(int position) : base(position){}
        }

        [Serializable]
        public class RandomFountainAutomaticOperationModel : BaseAutomatic
        {
            public int Min
            {
                get;
                set;
            }

            public int Max
            {
                get;
                set;
            }

            public RandomFountainAutomaticOperationModel(int min, int max)
            {
                Min = min;
                Max = max;
            }
        }

        public override void SetXElement(XElement xEdit)
        {
            if (int.Parse(xEdit.Attribute("automaticType").Value) == 0)
            {
                MyBaseAutomatic = new RhombusDiffusionAutomaticOperationModel(int.Parse(xEdit.Attribute("position").Value));
            }
            else if (int.Parse(xEdit.Attribute("automaticType").Value) == 1)
            {
                MyBaseAutomatic = new CrossAutomaticOperationModel(int.Parse(xEdit.Attribute("position").Value));
            }
            else if (int.Parse(xEdit.Attribute("automaticType").Value) == 2)
            {
                MyBaseAutomatic = new RandomFountainAutomaticOperationModel(int.Parse(xEdit.Attribute("min").Value), int.Parse(xEdit.Attribute("max").Value));
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("CreateFromAutomatic");
            xVerticalFlipping.SetAttributeValue("automaticType", (int)MyAutomaticType);
            if (MyAutomaticType == AutomaticType.RhombusDiffusion
                || MyAutomaticType == AutomaticType.Cross)
            {
                xVerticalFlipping.SetAttributeValue("position", (MyBaseAutomatic as BaseOneNumberAutomatic).Position);
            }
            if (MyAutomaticType == AutomaticType.RandomFountain)
            {
                xVerticalFlipping.SetAttributeValue("max", (MyBaseAutomatic as RandomFountainAutomaticOperationModel).Max);
                xVerticalFlipping.SetAttributeValue("min", (MyBaseAutomatic as RandomFountainAutomaticOperationModel).Min);
            }

            return xVerticalFlipping;
        }
    }
}
