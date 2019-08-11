using Maker.Business.Model.OperationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Maker.View.UI.Style.Child
{
    public partial class SetAttributeOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "SetAttribute";
        private SetAttributeOperationModel setAttributeOperationModel;
        public SetAttributeOperationChild(SetAttributeOperationModel setAttributeOperationModel)
        {
            this.setAttributeOperationModel = setAttributeOperationModel;
            //构建对话框
            foreach (var item in setAttributeOperationModel.AttributeOperationModels)
            {
                ComboBox cb = GetComboBox(new List<string>{ "Time", "Position", "Color" }, null);
                if (item.attributeType.Equals(SetAttributeOperationModel.AttributeOperationModel.AttributeType.TIME))
                {
                    cb.SelectedIndex = 0;
                }
                else if (item.attributeType.Equals(SetAttributeOperationModel.AttributeOperationModel.AttributeType.POSITION))
                {
                    cb.SelectedIndex = 1;
                }
                else if (item.attributeType.Equals(SetAttributeOperationModel.AttributeOperationModel.AttributeType.COLOR))
                {
                    cb.SelectedIndex = 2;
                }
                TextBox tb = GetTexeBox(item.Value);
                tb.Width = 300;
                AddUIElement(GetDockPanel(new List<FrameworkElement> { cb, tb,GetImage("check.png",27) }));
            }

            AddUIElement(GetButton("Change", IvChange_Click));

            CreateDialog();

            AddTitleImage(new List<String>() { "add_white.png", "reduce.png" }, new List<System.Windows.Input.MouseButtonEventHandler>() { IvAdd_MouseLeftButtonDown, IvReduce_MouseLeftButtonDown });
        }

        private void IvChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IvAdd_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ComboBox cb = GetComboBox(new List<string> { "Time", "Position", "Color" }, null);
            TextBox tb = GetTexeBox("+0");
            tb.Width = 300;
            AddUIToDialog(GetDockPanel(new List<FrameworkElement> { cb, tb } ),UICount - 1);
        }

        private void IvReduce_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (lb.SelectedIndex == -1)
            //    return;
            //changeColorOperationModel.Colors.RemoveAt(lb.SelectedIndex);
            //lb.Items.RemoveAt(lb.SelectedIndex);
            //Refresh();
        }
    }
}
