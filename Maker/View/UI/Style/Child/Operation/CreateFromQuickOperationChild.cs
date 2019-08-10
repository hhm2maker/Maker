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
        public override string Title { get; set; } = "FastGeneration";
        private CreateFromQuickOperationModel createFromQuickOperationModel;

        private TextBox tbTime, tbPosition, tbInterval, tbContinued,tbColor;
        private ComboBox cbType, cbAction;
        public CreateFromQuickOperationChild(CreateFromQuickOperationModel createFromQuickOperationModel)
        {
            this.createFromQuickOperationModel = createFromQuickOperationModel;
            //构建对话框
            tbTime = GetTexeBox(createFromQuickOperationModel.Time.ToString());
            tbTime.Width = 270;
            AddTitleAndControl("TimeColon", new List<FrameworkElement>() { tbTime, GetImage("calc.png",27) });

            StringBuilder sbPosition = new StringBuilder();
            foreach(var item in createFromQuickOperationModel.PositionList)
            {
                sbPosition.Append(item).Append(StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter);
            }
            tbPosition = GetTexeBox(sbPosition.ToString().Substring(0, sbPosition.ToString().Length-1));
            tbPosition.Width = 270;
            AddTitleAndControl("PositionColon", new List<FrameworkElement>() { tbPosition, GetImage("draw.png", 27), GetImage("more_white.png", 27) });

            tbInterval = GetTexeBox(createFromQuickOperationModel.Interval.ToString());
            AddTitleAndControl("IntervalColon", tbInterval);

            tbContinued = GetTexeBox(createFromQuickOperationModel.Continued.ToString());
            AddTitleAndControl("DurationColon", tbContinued);

            StringBuilder sbColor = new StringBuilder();
            foreach (var item in createFromQuickOperationModel.ColorList)
            {
                sbColor.Append(item).Append(StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter);
            }
            tbColor = GetTexeBox(sbPosition.ToString().Substring(0, sbColor.ToString().Length - 1));
            tbColor.Width = 270;
            AddTitleAndControl("ColorColon", new List<FrameworkElement>() { tbColor, GetImage("more_white.png", 27) });

            cbType = GetComboBox(new List<String>() { "Up", "Down","UpDown", "DownUp" ,"UpAndDown", "DownAndUp", "FreezeFrame" },null);
            cbType.SelectedIndex = createFromQuickOperationModel.Type;
            AddTitleAndControl("TypeColon", cbType);

            cbAction = GetComboBox(new List<String>() { "All", "Open", "Close"}, null);
            cbAction.SelectedIndex = createFromQuickOperationModel.Action - 10;
            AddTitleAndControl("ActionColon", cbAction);
          
            StackPanel dp = new StackPanel();
            dp.Orientation = Orientation.Vertical;
            dp.Margin = new Thickness(0, 10, 0, 10);

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
