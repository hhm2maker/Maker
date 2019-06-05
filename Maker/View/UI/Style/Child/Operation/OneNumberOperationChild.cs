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
            Slider slider = new Slider();
            slider.Background = new SolidColorBrush(Colors.Red);
            slider.Width = 100;
            slider.Height = 100;
            AddUIElement(slider);
            CreateDialog(200, 200);
            tbNumber = Get(1) as TextBox;
        
            

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
