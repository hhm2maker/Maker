using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
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

        public BaseAutomatic MyBaseAutomatic
        {
            get;
            set;
        }


        public CreateFromAutomaticOperationModel()
        {

        }

        public CreateFromAutomaticOperationModel( BaseAutomatic baseAutomatic)
        {
            MyBaseAutomatic = baseAutomatic;

            if (baseAutomatic is RhombusDiffusionAutomaticOperationModel) {
                MyAutomaticType = AutomaticType.RhombusDiffusion;
            }
            if (baseAutomatic is CrossAutomaticOperationModel)
            {
                MyAutomaticType = AutomaticType.Cross;
            }
            if (baseAutomatic is RandomFountainAutomaticOperationModel)
            {
                MyAutomaticType = AutomaticType.RandomFountain;
            }
        }

        public class BaseAutomatic {

        }

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

        public class RhombusDiffusionAutomaticOperationModel : BaseOneNumberAutomatic
        {
            public RhombusDiffusionAutomaticOperationModel(int position) : base(position){ }
           
        }

        public class CrossAutomaticOperationModel : BaseOneNumberAutomatic
        {
            public CrossAutomaticOperationModel(int position) : base(position){}
        }

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
