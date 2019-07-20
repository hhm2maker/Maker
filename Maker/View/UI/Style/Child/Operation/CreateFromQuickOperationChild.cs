using Maker.Business;
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
    public partial class CreateFromQuickOperationChild : OperationStyle
    {
        private CreateFromQuickOperationModel reateFromQuickOperationModel;

        private ListBox lb;
        public CreateFromQuickOperationChild(CreateFromQuickOperationModel reateFromQuickOperationModel)
        {
            this.reateFromQuickOperationModel = reateFromQuickOperationModel;
            //构建对话框
            StackPanel dp = new StackPanel();
            dp.Orientation = Orientation.Vertical;
            dp.Margin = new Thickness(0, 10, 0, 10);
            TextBlock tb = new TextBlock();
            tb.FontSize = 16;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.SetResourceReference(TextBlock.TextProperty, "FastGeneration");
            dp.Children.Add(tb);

            Button btnChange = new Button();
            btnChange.SetResourceReference(ContentProperty, "Change");
            btnChange.MouseLeftButtonDown += IvReduce_MouseLeftButtonDown;
            dp.Children.Add(btnChange);

            AddUIElement(dp);

            CreateDialog();
        }

      

        private void IvReduce_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // StaticConstant.mw.projectUserControl.suc.mLaunchpad.SetData(nowLl);
        }



        public override bool ToSave() {
            return true;
        }
    }
}
