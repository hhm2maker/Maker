using Maker.Business.Model.OperationModel;
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
    public partial class OneNumberOperationChild : OperationStyle
    {
        private OneNumberOperationModel oneNumberOperationModel;
        public OneNumberOperationChild(OneNumberOperationModel oneNumberOperationModel)
        {
            this.oneNumberOperationModel = oneNumberOperationModel;
            //构建对话框
            AddTopHintTextBlock(oneNumberOperationModel.HintKeyword);
            AddTextBox();
            
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;

            tbNumber = new TextBox();
            tbNumber.FontSize = 16;
            tbNumber.Width = 50;
            tbNumber.Background = null;
            tbNumber.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            sp.Margin = new Thickness(0, 10, 0, 0);

            if (oneNumberOperationModel.MyNumberType == OneNumberOperationModel.NumberType.COLOR)
            {
                Slider slider = new Slider();
                slider.Width = 150;
                slider.VerticalAlignment = VerticalAlignment.Center;
                slider.Minimum = 1;
                slider.Value = oneNumberOperationModel.Number;
                slider.Maximum = 255;
                sp.Children.Add(slider);

            }
            else if (oneNumberOperationModel.MyNumberType == OneNumberOperationModel.NumberType.POSITION)
            {
                Slider slider = new Slider();
                slider.Width = 150;
                slider.VerticalAlignment = VerticalAlignment.Center;
                slider.Minimum = 0;
                slider.Maximum = 99;
                slider.Value = oneNumberOperationModel.Number;

                sp.Children.Add(slider);

            }
            else if (oneNumberOperationModel.MyNumberType == OneNumberOperationModel.NumberType.OTHER)
            {
                tbNumber.Width = 200;
            }

            sp.Children.Add(tbNumber);

            AddUIElement(sp);
            CreateDialog(200, 200);

            tbNumber.Text = oneNumberOperationModel.Number.ToString();
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
