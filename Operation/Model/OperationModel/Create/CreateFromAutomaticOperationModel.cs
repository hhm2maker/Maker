using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
