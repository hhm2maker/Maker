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
        public Dictionary<string, List<Light>> Hello()
        {
            Dictionary<string, List<Light>> mainLightGroup = new Dictionary<string, List<Light>>(); LightGroup Step1LightGroup = Step1(); mainLightGroup.Add("Step1", Step1LightGroup); LightGroup Step2LightGroup = Step2(); mainLightGroup.Add("Step2", Step2LightGroup); LightGroup Step3LightGroup = Step3(); mainLightGroup.Add("Step3", Step3LightGroup); LightGroup Step4LightGroup = Step4(); mainLightGroup.Add("Step4", Step4LightGroup); return mainLightGroup;
        }
        public LightGroup Step1()
        {
            CreateFromQuickOperationModel createFromQuickOperationModel = new CreateFromQuickOperationModel(0, new List<int>() { 11 }, 12, 12, new List<int>() { 5 }, 0, 10);
            LightGroup Step1LightGroup = Create.CreateLightGroup(createFromQuickOperationModel);
            ColorGroup MyStep1ColorGroup = new ColorGroup("73,74,75,76", ',', '-');
            Step1LightGroup.SetColor(MyStep1ColorGroup);
            Step1LightGroup.VerticalFlipping();
            Step1LightGroup.ChangeTime(LightGroup.MULTIPLICATION, 2);
            Step1LightGroup.SetStartTime(0);
            Step1LightGroup.SetEndTime(LightGroup.ALL, "12");
            Step1LightGroup.SetAllTime(12);
            Step1LightGroup.MatchTotalTimeLattice(12);
            Step1LightGroup.InterceptTime(0, 12);
            Step1LightGroup.Fold(LightGroup.VERTICAL, 0, 0);
            ColorGroup MyStep2ColorGroup = new ColorGroup("5,5,5,5,5,5,5,5,5,5", ',', '-');
            Step1LightGroup.ShapeColor(LightGroup.RADIALVERTICAL, MyStep2ColorGroup);
            Step1LightGroup.FillColor(5);
            ColorGroup MyStep3ColorGroup = new ColorGroup("5", ',', '-');
            Step1LightGroup.CopyToTheEnd(MyStep3ColorGroup);
            ColorGroup MyStep4ColorGroup = new ColorGroup("5", ',', '-');
            Step1LightGroup.CopyToTheFollow(MyStep4ColorGroup);
            ColorGroup MyStep5ColorGroup = new ColorGroup("100", ',', '-');
            Step1LightGroup.AccelerationOrDeceleration(MyStep5ColorGroup);
            Step1LightGroup.RemoveBorder();
            Step1LightGroup = Animation.Serpentine(Step1LightGroup, 0, 12);
            Step1LightGroup = Animation.Windmill(Step1LightGroup, 12);
            Step1LightGroup.ThirdParty("ChangeThePosition", "sample.dll", new List<String> { "11", "11" }); return Step1LightGroup;
        }
        public LightGroup Step2()
        {
            LightGroup Step2LightGroup = Step1();
            Step2LightGroup.SetAttribute(LightGroup.POSITION, "+10"); return Step2LightGroup;
        }
        public LightGroup Step3()
        {
            LightGroup Step3LightGroup = Create.CreateFromLightFile("0.light"); return Step3LightGroup;
        }
        public LightGroup Step4()
        {
            LightGroup Step4LightGroup = Create.Automatic(Create.RHOMBUSDIFFUSION, 11); return Step4LightGroup;
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

