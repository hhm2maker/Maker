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

namespace Maker.View.UI.Style.Child
{
    [Serializable]
    public partial class ColorOperationChild : OperationStyle
    {
        private ColorOperationModel changeColorOperationModel;
        public ColorOperationChild(ColorOperationModel changeColorOperationModel)
        {
            this.changeColorOperationModel = changeColorOperationModel;
            //构建对话框
            AddTopHintTextBlock(changeColorOperationModel.HintString);
            AddTextBox();
            //CreateDialog();

            for (int i = 0; i < changeColorOperationModel.Colors.Count; i++)
            {

                Grid grid = new Grid();
                //grid.HorizontalAlignment = HorizontalAlignment.Stretch;
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
                slider.LargeChange = 5;
                slider.SmallChange = 1;
                //slider.ValueChanged += Slider_ValueChanged;
                slider.Margin = new Thickness(0, 0, 20, 0);
                grid.Children.Add(slider);

                TextBox tbNumber = new TextBox();
                tbNumber.FontSize = 16;
                tbNumber.Width = 50;
                //tbNumber.LostFocus += TbNumber_LostFocus;
                tbNumber.Background = null;
                tbNumber.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                tbNumber.Text = changeColorOperationModel.Colors[i].ToString();

                grid.Margin = new Thickness(0, 10, 0, 0);
                if (slider != null)
                {
                    Grid.SetColumn(tbNumber, 0);
                }
                grid.Children.Add(tbNumber);
                Grid.SetColumn(tbNumber, 1);

                AddUIElement(grid);
            }

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
