using Maker.Business.Model.OperationModel;
using Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Maker.View.UI.Style.Child
{
    [Serializable]
    public partial class ColorOperationChild : OperationStyle
    {
        private ColorOperationModel changeColorOperationModel;

        private ListBox lb;
        public ColorOperationChild(ColorOperationModel changeColorOperationModel)
        {
            this.changeColorOperationModel = changeColorOperationModel;
            //构建对话框
            AddTopHintTextBlock(changeColorOperationModel.HintString);
            AddTextBox();

            StackPanel sp = new StackPanel();
            sp.Margin = new Thickness(0, 10, 0, 10);
            sp.Orientation = Orientation.Horizontal;
            sp.HorizontalAlignment = HorizontalAlignment.Right;
            Image ivAdd = new Image
            {
                Width = 20,
                Height = 20,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/add_white.png", UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.Fill
            };
            RenderOptions.SetBitmapScalingMode(ivAdd, BitmapScalingMode.Fant);
            sp.Children.Add(ivAdd);
            Image ivReduce = new Image
            {
                Width = 20,
                Height = 20,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/reduce.png", UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.Fill
            };
            RenderOptions.SetBitmapScalingMode(ivReduce, BitmapScalingMode.Fant);
            ivReduce.Margin = new Thickness(10,0,0,0);
            sp.Children.Add(ivReduce);
           
            AddUIElement(sp);

            lb = new ListBox();
            lb.Background = new SolidColorBrush(Colors.Transparent);
            lb.BorderBrush = new SolidColorBrush(Colors.Transparent);
            lb.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            for (int i = 0; i < changeColorOperationModel.Colors.Count; i++)
            {
                Grid grid = new Grid();
                grid.HorizontalAlignment = HorizontalAlignment.Stretch;
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(columnDefinition);
                ColumnDefinition columnDefinition2 = new ColumnDefinition();
                columnDefinition2.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(columnDefinition2);

                Slider slider = new Slider();
                // slider.Width = 150;
                slider.VerticalAlignment = VerticalAlignment.Center;
                slider.Minimum = 1;
                slider.Maximum = 127;
                slider.Value = changeColorOperationModel.Colors[i];
                slider.LargeChange = 0;
                slider.SmallChange = 0;
                slider.ValueChanged += Slider_ValueChanged;
                slider.Margin = new Thickness(0, 0, 20, 0);
                grid.Children.Add(slider);

                TextBox tbNumber = new TextBox();
                tbNumber.FontSize = 16;
                tbNumber.Width = 50;
                tbNumber.LostFocus += TbNumber_LostFocus;
                tbNumber.Background = null;
                tbNumber.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                tbNumber.Text = changeColorOperationModel.Colors[i].ToString();

                //grid.Margin = new Thickness(0, 10, 0, 10);
                if (slider != null)
                {
                    Grid.SetColumn(tbNumber, 0);
                }
                grid.Children.Add(tbNumber);
                Grid.SetColumn(tbNumber, 1);

                lb.Items.Add(grid);
            }
            AddUIElement(lb);

            CreateDialog();

            tbColors = Get(1) as TextBox;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < changeColorOperationModel.Colors.Count; i++)
            {
                if (i != changeColorOperationModel.Colors.Count - 1)
                {
                    sb.Append(changeColorOperationModel.Colors[i] + StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter.ToString());
                }
                else
                {
                    sb.Append(changeColorOperationModel.Colors[i]);
                }
            }

            tbColors.Text = sb.ToString();
        }
        private void TbNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            int position = lb.Items.IndexOf((tb.Parent as Grid));

            if (int.TryParse(tb.Text, out int num))
            {
                if (num == changeColorOperationModel.Colors[position])
                    return;
                ((tb.Parent as Grid).Children[0] as Slider).Value = num;
            }
            else
            {
                tb.Text = changeColorOperationModel.Colors[position].ToString();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            int position = lb.Items.IndexOf((slider.Parent as Grid));

            int number = (int)slider.Value;
            ((slider.Parent as Grid).Children[1] as TextBox).Text = number.ToString();
            Refresh();
        }

        public override void Refresh()
        {
            int color = (int)obj[0];
            if (color == oneNumberOperationModel.Number)
                return;

            if (MyData == null)
            {
                StaticConstant.mw.projectUserControl.suc.Test(StaticConstant.mw.projectUserControl.suc.GetStepName(), StaticConstant.mw.projectUserControl.suc.sw.lbCatalog.SelectedIndex);

                List<int> times = LightBusiness.GetTimeList(NowData);
                int position = Convert.ToInt32(StaticConstant.mw.projectUserControl.suc.tbTimePointCountLeft.Text) - 1;
                MyData = new List<Light>();
                for (int i = 0; i < NowData.Count; i++)
                {
                    if (NowData[i].Time == times[position])
                    {
                        MyData.Add(new Light(NowData[i].Time, NowData[i].Action, NowData[i].Position, NowData[i].Color));
                    }
                }
            }


            List<int> li = new List<int>();
            List<Light> ll = new List<Light>();


            //真实数据同步
            oneNumberOperationModel.Number = color;


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

        private void CbOperation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        public TextBox tbColors;
        public ComboBox cbOperation;

        public override bool ToSave() {
            if (tbColors.Text.Equals(String.Empty))
            {
                tbColors.Focus();
                return false;
            }
            List<int> colors = new List<int>();
            String[] strColors = tbColors.Text.Split(StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter);
            foreach(var item in strColors) {
                if (int.TryParse(item, out int color))
                {
                    colors.Add(color);
                }
                else
                {
                    return false;
                }
            }
            changeColorOperationModel.Colors = colors;
            return true;
        }
    }
}
