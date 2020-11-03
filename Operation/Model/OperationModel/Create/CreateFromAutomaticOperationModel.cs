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
            RandomFountain = 2,
            BilateralDiffusion = 3
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
                if (value is BilateralDiffusionAutomaticOperationModel)
                {
                    MyAutomaticType = AutomaticType.BilateralDiffusion;
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

            public int Continued
            {
                get;
                set;
            }

            public BaseOneNumberAutomatic()
            {
           
            }

            public BaseOneNumberAutomatic(int position, int continued)
            {
                Position = position;
                Continued = continued;
            }
        }

        [Serializable]
        public class RhombusDiffusionAutomaticOperationModel : BaseOneNumberAutomatic
        {
            public RhombusDiffusionAutomaticOperationModel(int position, int continued) : base(position, continued) { }
        }

        [Serializable]
        public class CrossAutomaticOperationModel : BaseOneNumberAutomatic
        {
            public CrossAutomaticOperationModel(int position, int continued) : base(position,continued){}
        }

        [Serializable]
        public class BilateralDiffusionAutomaticOperationModel : BaseOneNumberAutomatic
        {
            public BilateralDiffusionAutomaticOperationModel(int position, int continued) : base(position, continued) { }
        }

        [Serializable]
        public class RandomFountainAutomaticOperationModel : BaseAutomatic
        {
            public List<int> Position
            {
                get;
                set;
            }

      
            public RandomFountainAutomaticOperationModel(List<int> position)
            {
                Position = position;
            }
        }

        public override void SetXElement(XElement xEdit)
        {
            if (int.Parse(xEdit.Attribute("automaticType").Value) == 0)
            {
                MyBaseAutomatic = new RhombusDiffusionAutomaticOperationModel(int.Parse(xEdit.Attribute("position").Value), int.Parse(xEdit.Attribute("continued").Value));
            }
            else if (int.Parse(xEdit.Attribute("automaticType").Value) == 1)
            {
                if (xEdit.Attribute("continued") == null)
                {
                    MyBaseAutomatic = new CrossAutomaticOperationModel(int.Parse(xEdit.Attribute("position").Value), 1);
                }
                else
                {
                    MyBaseAutomatic = new CrossAutomaticOperationModel(int.Parse(xEdit.Attribute("position").Value), int.Parse(xEdit.Attribute("continued").Value));
                }
            }
            else if (int.Parse(xEdit.Attribute("automaticType").Value) == 2)
            {
                List<int> positions = new List<int>();
                for (int i = 0; i < xEdit.Attribute("position").Value.Length; i++)
                {
                    positions.Add(xEdit.Attribute("position").Value[i] - 33);
                }
                MyBaseAutomatic = new RandomFountainAutomaticOperationModel(positions);
            }
            else if(int.Parse(xEdit.Attribute("automaticType").Value) == 3)
            {
                MyBaseAutomatic = new BilateralDiffusionAutomaticOperationModel(int.Parse(xEdit.Attribute("position").Value), int.Parse(xEdit.Attribute("continued").Value));
            }
        }

        public override XElement GetXElement()
        {
            XElement xVerticalFlipping = new XElement("CreateFromAutomatic");
            xVerticalFlipping.SetAttributeValue("automaticType", (int)MyAutomaticType);
            if (MyAutomaticType == AutomaticType.RhombusDiffusion
                || MyAutomaticType == AutomaticType.Cross
                || MyAutomaticType == AutomaticType.BilateralDiffusion)
            {
                xVerticalFlipping.SetAttributeValue("position", (MyBaseAutomatic as BaseOneNumberAutomatic).Position);
                xVerticalFlipping.SetAttributeValue("continued", (MyBaseAutomatic as BaseOneNumberAutomatic).Continued);
            }
            if (MyAutomaticType == AutomaticType.RandomFountain)
            {
                StringBuilder sbPositions = new StringBuilder();
                List<int> PositionList = (MyBaseAutomatic as RandomFountainAutomaticOperationModel).Position;
                for (int i = 0; i < PositionList.Count; i++)
                {
                    sbPositions.Append((char)(PositionList[i] + 33));
                }
                xVerticalFlipping.SetAttributeValue("position", sbPositions.ToString());
            }

            return xVerticalFlipping;
        }
    }
}
