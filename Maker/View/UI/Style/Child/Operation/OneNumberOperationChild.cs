using Maker.Business.Model.OperationModel;
using Maker.View.LightScriptUserControl;
using System.Collections.Generic;

namespace Maker.View.UI.Style.Child
{
    public partial class OneNumberOperationChild : OperationStyle
    {
        public override StyleType FunType { get; set; } = StyleType.Edit;

        private OneNumberOperationModel oneNumberOperationModel;
        public OneNumberOperationChild(OneNumberOperationModel oneNumberOperationModel, ScriptUserControl suc) : base(suc)
        {
            this.oneNumberOperationModel = oneNumberOperationModel;
            Title = oneNumberOperationModel.Identifier;
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            if (oneNumberOperationModel.MyNumberType == OneNumberOperationModel.NumberType.COLOR)
            {
                return new List<RunModel>
                {
                    new RunModel(oneNumberOperationModel.HintKeyword, oneNumberOperationModel.Number.ToString(),RunModel.RunType.Color)
                };
            }
            else if (oneNumberOperationModel.MyNumberType == OneNumberOperationModel.NumberType.POSITION)
            {
                return new List<RunModel>
                {
                    new RunModel(oneNumberOperationModel.HintKeyword, oneNumberOperationModel.Number.ToString(),RunModel.RunType.Position)
                };
            }
            else
            {
                return new List<RunModel>
                {
                    new RunModel(oneNumberOperationModel.HintKeyword, oneNumberOperationModel.Number.ToString(),RunModel.RunType.Normal)
                };
            }

        }

        protected override void RefreshView()
        {
            //Number
            string expression = runs[2].Text;
            int number = 0;
            try
            {
                System.Data.DataTable eval = new System.Data.DataTable();
                number = (int)eval.Compute(expression, "");
            }
            catch
            {
                number = oneNumberOperationModel.Number;
            }

            oneNumberOperationModel.Number = number;


            UpdateData();
        }


        //private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    int number = (int)(sender as Slider).Value;
        //    if (number == oneNumberOperationModel.Number)
        //        return;
        //    //真实数据同步
        //    oneNumberOperationModel.Number = number;

        //    tbNumber.Text = number.ToString();
        //    Refresh();
        //}

        //public override void Refresh()
        //{
        //    base.Refresh();
        //    int color = oneNumberOperationModel.Number;
       
        //    List<int> li = new List<int>();
        //    List<Light> ll = new List<Light>();

        //    for (int i = 0; i < MyData.Count; i++)
        //    {
        //        li.Add(MyData[i].Position);
        //        ll.Add(new Light(0, 144, MyData[i].Position, MyData[i].Color));
        //    }

        //    for (int i = 0; i < 100; i++)
        //    {
        //        if (!li.Contains(i))
        //        {
        //            ll.Add(new Light(0, 144, i, color));
        //        }
        //    }
        //    StaticConstant.mw.editUserControl.suc.mLaunchpad.SetData(ll);
        //}

        //public TextBox tbNumber;

        //public override bool ToSave()
        //{
        //    if (int.TryParse(tbNumber.Text, out int number))
        //    {
        //        oneNumberOperationModel.Number = number;
        //        return true;
        //    }
        //    else
        //    {
        //        tbNumber.Focus();
        //        return false;
        //    }
        //}


    }
}
