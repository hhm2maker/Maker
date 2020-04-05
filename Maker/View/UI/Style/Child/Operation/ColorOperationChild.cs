using Maker.Business;
using Maker.Business.Model.OperationModel;
using Maker.Model;
using Operation;
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

            Title = changeColorOperationModel.HintString;
            //构建对话框
         
            lb = new ListBox();
            lb.Padding = new Thickness(-5,0,-5,0);
            lb.Background = new SolidColorBrush(Colors.Transparent);
            lb.BorderBrush = new SolidColorBrush(Colors.Transparent);
            lb.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            for (int i = 0; i < changeColorOperationModel.Colors.Count; i++)
            {
                Add(i);
            }
            AddUIElement(lb);

            CreateDialog();
            AddTitleImage(new List<String>() { "add_white.png", "reduce.png" }, new List<System.Windows.Input.MouseButtonEventHandler>() { IvAdd_MouseLeftButtonDown , IvReduce_MouseLeftButtonDown });
        }

        private void IvAdd_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            changeColorOperationModel.Colors.Add(5);
            Add(changeColorOperationModel.Colors.Count-1);
            Refresh();
        }

        private void IvReduce_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lb.SelectedIndex == -1)
                return;
            changeColorOperationModel.Colors.RemoveAt(lb.SelectedIndex);
            lb.Items.RemoveAt(lb.SelectedIndex);
            Refresh();
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

            if (number == changeColorOperationModel.Colors[position])
                return;

            //真实数据同步
            changeColorOperationModel.Colors[position] = number;
            ((slider.Parent as Grid).Children[1] as TextBox).Text = number.ToString();
            Refresh();
        }

        private List<char> mColor;
        List<char> OldColorList = new List<char>();
        List<char> NewColorList = new List<char>();

        protected override void InitData()
        {
            List<Light> ll = Business.LightBusiness.Copy(NowData);
            mColor = new List<char>();
            for (int j = 0; j < ll.Count; j++)
            {
                if (ll[j].Action == 144)
                {
                    if (!mColor.Contains((char)ll[j].Color))
                    {
                        mColor.Add((char)ll[j].Color);
                    }
                }
            }
            for (int i = 0; i < mColor.Count; i++)
            {
                OldColorList.Add(mColor[i]);
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            List<Light> nowLl = Business.LightBusiness.Copy(MyData); 

            List<int> geshihua = changeColorOperationModel.Colors;
            NewColorList.AddRange(OldColorList.ToArray());

            //获取一共有多少种老颜色
            int OldColorCount = mColor.Count;
            if (OldColorCount == 0)
            {
                return;
            }
            int chuCount = OldColorCount / geshihua.Count;
            int yuCount = OldColorCount % geshihua.Count;
            List<int> meigeyanseCount = new List<int>();//每个颜色含有的个数

            for (int i = 0; i < geshihua.Count; i++)
            {
                meigeyanseCount.Add(chuCount);
            }
            if (yuCount != 0)
            {
                for (int i = 0; i < yuCount; i++)
                {
                    meigeyanseCount[i]++;
                }
            }
            List<int> yansefanwei = new List<int>();
            for (int i = 0; i < geshihua.Count; i++)
            {
                int AllCount = 0;
                if (i != 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        AllCount += meigeyanseCount[j];
                    }
                }
                yansefanwei.Add(AllCount);
            }
            for (int i = 0; i < geshihua.Count; i++)
            {
                for (int j = yansefanwei[i]; j < yansefanwei[i] + meigeyanseCount[i]; j++)
                {
                    NewColorList[j] = (char)geshihua[i];
                }
            }
            //给原颜色排序
            OldColorList.Sort();

            for (int k = 0; k < nowLl.Count; k++)
            {
                for (int l = 0; l < OldColorList.Count; l++)
                {
                    if (nowLl[k].Color == OldColorList[l])
                    {
                        nowLl[k].Color = NewColorList[l];
                        break;
                    }
                }
            }

            StaticConstant.mw.editUserControl.suc.mLaunchpad.SetData(nowLl);
        }

        private void Add(int i) {
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

        private void CbOperation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        public ComboBox cbOperation;

        public override bool ToSave() {
            //if (tbColors.Text.Equals(String.Empty))
            //{
            //    tbColors.Focus();
            //    return false;
            //}
            //List<int> colors = new List<int>();
            //String[] strColors = tbColors.Text.Split(StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter);
            //foreach(var item in strColors) {
            //    if (int.TryParse(item, out int color))
            //    {
            //        colors.Add(color);
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //changeColorOperationModel.Colors = colors;
            return true;
        }
    }
}
