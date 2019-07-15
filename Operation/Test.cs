using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    using System;
    using System.Collections.Generic;
    using Operation;
    using Maker.Business.Model.OperationModel;
    public class Test
    {
        public static Dictionary<string, List<Light>> Hello()
        {
            Dictionary<string, List<Light>> mainLightGroup = new Dictionary<string, List<Light>>(); LightGroup Step1LightGroup = Step1(); mainLightGroup.Add("Step1", Step1LightGroup); return mainLightGroup;
        }
        public static LightGroup Step1()
        {
            CreateFromQuickOperationModel createFromQuickOperationModel = new CreateFromQuickOperationModel(0, new List<int>() { 11 }, 12, 12, new List<int>() { 5 }, 0, 10);
            LightGroup Step1LightGroup = Create.CreateLightGroup(createFromQuickOperationModel);
            LightBusiness.Print(Step1LightGroup);
            return Step1LightGroup;
        }
    }
}
    //   using System;
    //using System.Collections.Generic;
    //using Operation;
    //public class Test{
    //public Dictionary<string,List<Light>> Hello(){
    //Dictionary<string,List<Light>> mainLightGroup = new Dictionary<string,List<Light>>();LightGroup Step1LightGroup = Step1();mainLightGroup.Add("Step1LightGroup",Step1LightGroup);return mainLightGroup;}public LightGroup Step1(){PositionGroup Step1PositionGroup = new PositionGroup("36",' ','-');
    //	ColorGroup Step1ColorGroup = new ColorGroup("5",' ','-');
    //	LightGroup Step1LightGroup = new LightGroup();
    //    //LightGroup Step1LightGroup = Create.CreateLightGroup(0, Step1PositionGroup, 12, 12, Step1ColorGroup, Create.UP, Create.ALL);
    //    ColorGroup MyStep1ColorGroup = new ColorGroup("100 50",' ','-');
    //	Step1LightGroup.AccelerationOrDeceleration(MyStep1ColorGroup);
    //	ColorGroup MyStep2ColorGroup = new ColorGroup("5 5 5 5 5",' ','-');
    //	Step1LightGroup.ShapeColor(LightGroup.SQUARE, MyStep2ColorGroup); return Step1LightGroup;}}

