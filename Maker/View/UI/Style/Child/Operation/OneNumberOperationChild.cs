using Maker.Business.Model.OperationModel;
using Maker.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maker.View.UI.Style.Child
{
    public partial class OneNumberOperationChild : OperationStyle
    {
        private OneNumberOperationModel oneNumberOperationModel;
        private Slider slider;
        public OneNumberOperationChild(OneNumberOperationModel oneNumberOperationModel)
        {
            this.oneNumberOperationModel = oneNumberOperationModel;
            Title = oneNumberOperationModel.Identifier;
            //构建对话框
         
            //AddTextBox();

            DockPanel grid = new DockPanel();

            tbNumber = GetTexeBox(oneNumberOperationModel.Number.ToString());

            if (oneNumberOperationModel.MyNumberType == OneNumberOperationModel.NumberType.COLOR)
            {
                //tbNumber.Width = 200;
                slider = new Slider();
                slider.Width = 200;
                slider.VerticalAlignment = VerticalAlignment.Center;
                slider.Minimum = 1;
                slider.Maximum = 127;
                slider.Value = oneNumberOperationModel.Number;
                slider.LargeChange = 5;
                slider.SmallChange = 1;
                slider.ValueChanged += Slider_ValueChanged;
                slider.Margin = new Thickness(0, 0, 20, 0);
                grid.Children.Add(slider);
            }
            else if (oneNumberOperationModel.MyNumberType == OneNumberOperationModel.NumberType.POSITION)
            {
                //tbNumber.Width = 200;
                slider = new Slider();
                slider.Width = 200;
                slider.VerticalAlignment = VerticalAlignment.Center;
                slider.Minimum = 0;
                slider.Maximum = 99;
                slider.Value = oneNumberOperationModel.Number;
                slider.LargeChange = 5;
                slider.SmallChange = 1;
                slider.ValueChanged += Slider_ValueChanged;
                slider.Margin = new Thickness(0, 0, 20, 0);
                grid.Children.Add(slider);
            }
            else if (oneNumberOperationModel.MyNumberType == OneNumberOperationModel.NumberType.OTHER)
            {
               
            }
          
            tbNumber.LostFocus += TbNumber_LostFocus;

            //grid.Margin = new Thickness(0, 10, 0, 0);
            grid.Children.Add(tbNumber);

            //AddUIElement(grid);
            AddTitleAndControl(oneNumberOperationModel.HintKeyword, new List<FrameworkElement>() { grid });
            CreateDialog();
        }

        private void TbNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse((sender as TextBox).Text, out int num))
            {
                if (num == oneNumberOperationModel.Number)
                    return;
                if (slider != null)
                {
                    slider.Value = num;
                }
                else {
                    oneNumberOperationModel.Number = num;
                }
            }
            else {
                tbNumber.Text = oneNumberOperationModel.Number.ToString();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int number = (int)(sender as Slider).Value;
            if (number == oneNumberOperationModel.Number)
                return;
            //真实数据同步
            oneNumberOperationModel.Number = number;

            tbNumber.Text = number.ToString();
            Refresh();
        }

        public override void Refresh()
        {
            base.Refresh();
            int color = oneNumberOperationModel.Number;
            //Operation.LightGroup lg = OperationUtils.MakerLightToOperationLight(mDa);
            //lg.FillColor((int)(obj[0] as Slider).Value);
            //StaticConstant.mw.projectUserControl.suc.mLaunchpad.SetData(OperationUtils.OperationLightToMakerLight(lg));
            List<int> li = new List<int>();
            List<Light> ll = new List<Light>();
         

            for (int i = 0; i < MyData.Count; i++)
            {
                li.Add(MyData[i].Position);
                ll.Add(new Light(0, 144, MyData[i].Position, MyData[i].Color));
            }
            for (int i = 0; i < 100; i++)
            {
                if (!li.Contains(i))
                {
                    ll.Add(new Light(0, 144, i, color));
                }
            }
            StaticConstant.mw.projectUserControl.suc.mLaunchpad.SetData(ll);
        }



        public TextBox tbNumber;

        public override bool ToSave()
        {
            if (int.TryParse(tbNumber.Text, out int number))
            {
                oneNumberOperationModel.Number = number;
                return true;
            }
            else
            {
                tbNumber.Focus();
                return false;
            }
        }


    }
}
