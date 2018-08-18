using Maker.View.Dialog.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ControlScriptDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ControlScriptDialog : Window
    {
        public InputUserControl iuc;
        public ControlScriptDialog(InputUserControl iuc)
        {
            InitializeComponent();
            this.iuc = iuc;
            Owner = iuc.mw;
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            String str = iuc.lightScriptDictionary[iuc.GetStepName()];
            String[] commands =  str.Split(Environment.NewLine.ToArray());
            foreach(String mStr in commands) {
                if (mStr.Trim().Equals(String.Empty)) {
                    continue;
                }
                if (mStr[0] == 9)
                {
                    lbMain.Items.Add(mStr.Substring(1));
                }
                else {
                    lbMain.Items.Add(mStr);
                }
            }
        }

        private void DeleteRangeList(object sender, RoutedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            lbMain.Items.RemoveAt(lbMain.SelectedIndex);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth > 140) {
                lbMain.Width = ActualWidth - 140;
            }
            if (ActualHeight > 110)
            {
                lbMain.Height = ActualHeight - 110;
            }
        }

        private void lbMain_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            int selectIndex = lbMain.SelectedIndex;
            if (lbMain.SelectedItem.ToString().Contains("Create.CreateLightGroup"))
            {
                Create_CreateLightGroupDialog dialog = new Create_CreateLightGroupDialog(iuc.mw, lbMain.SelectedItem.ToString());
                if (dialog.ShowDialog() == true)
                {
                    lbMain.Items[selectIndex] = dialog.result;
                }
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.HorizontalFlipping("))
            {
                MessageDialog dialog = new MessageDialog(iuc.mw, "HorizontalFlipping");
                dialog.ShowDialog();
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.VerticalFlipping("))
            {
                MessageDialog dialog = new MessageDialog(iuc.mw, "VerticalFlipping");
                dialog.ShowDialog();
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.Clockwise("))
            {
                MessageDialog dialog = new MessageDialog(iuc.mw, "Clockwise");
                dialog.ShowDialog();
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.AntiClockwise("))
            {
                MessageDialog dialog = new MessageDialog(iuc.mw, "AntiClockwise");
                dialog.ShowDialog();
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.Reversal("))
            {
                MessageDialog dialog = new MessageDialog(iuc.mw, "Reversal");
                dialog.ShowDialog();
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.RemoveBorder("))
            {
                MessageDialog dialog = new MessageDialog(iuc.mw, "RemoveTheBorder");
                dialog.ShowDialog();
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.ChangeTime("))
            {
                Edit_ChangeTime dialog = new Edit_ChangeTime(iuc.mw, lbMain.SelectedItem.ToString());
                if (dialog.ShowDialog() == true)
                {
                    lbMain.Items[selectIndex] = dialog.result;
                }
            }
            else if (lbMain.SelectedItem.ToString().Contains("new RangeGroup("))
            {
                Main_NewColorGroupDialog dialog = new Main_NewColorGroupDialog(iuc.mw, lbMain.SelectedItem.ToString(), 0);
                if (dialog.ShowDialog() == true)
                {
                    lbMain.Items[selectIndex] = dialog.result;
                }
            }
            else if (lbMain.SelectedItem.ToString().Contains("new ColorGroup("))
            {
                Main_NewColorGroupDialog dialog = new Main_NewColorGroupDialog(iuc.mw, lbMain.SelectedItem.ToString(),1);
                if (dialog.ShowDialog() == true)
                {
                    lbMain.Items[selectIndex] = dialog.result;
                }
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.CopyToTheEnd("))
            {
                Edit_OverlapDialog dialog = new Edit_OverlapDialog(iuc.mw, lbMain.SelectedItem.ToString(), 0);
                if (dialog.ShowDialog() == true)
                {
                    lbMain.Items[selectIndex] = dialog.result;
                }
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.CopyToTheFollow("))
            {
                Edit_OverlapDialog dialog = new Edit_OverlapDialog(iuc.mw, lbMain.SelectedItem.ToString(), 1);
                if (dialog.ShowDialog() == true)
                {
                    lbMain.Items[selectIndex] = dialog.result;
                }
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.AccelerationOrDeceleration("))
            {
                Edit_OverlapDialog dialog = new Edit_OverlapDialog(iuc.mw, lbMain.SelectedItem.ToString(), 2);
                if (dialog.ShowDialog() == true)
                {
                    lbMain.Items[selectIndex] = dialog.result;
                }
            }
            else if (lbMain.SelectedItem.ToString().Contains(".SetAttribute("))
            {
                LightGroup_SetAttributeDialog dialog = new LightGroup_SetAttributeDialog(iuc.mw, lbMain.SelectedItem.ToString());
                if (dialog.ShowDialog() == true)
                {
                    lbMain.Items[selectIndex] = dialog.result;
                }
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.SetEndTime("))
            {
                String str = lbMain.SelectedItem.ToString();
                Edit_SetEndTimeDialog dialog = new Edit_SetEndTimeDialog(iuc.mw, ref str);
                if (dialog.ShowDialog() == true)
                {
                    lbMain.Items[selectIndex] = dialog.result;
                }
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.Animation("))
            {
                String str = lbMain.SelectedItem.ToString();
                Edit_AnimationDisappearDialog dialog = new Edit_AnimationDisappearDialog(iuc.mw, ref str);
                if (dialog.ShowDialog() == true)
                {
                    lbMain.Items[selectIndex] = dialog.result;
                }
            }
            else if (lbMain.SelectedItem.ToString().Contains("Edit.Fold("))
            {
                String str = lbMain.SelectedItem.ToString();
                Edit_FoldDialog dialog = new Edit_FoldDialog(iuc.mw, ref str);
                if (dialog.ShowDialog() == true)
                {
                    lbMain.Items[selectIndex] = dialog.result;
                }
            }
            lbMain.SelectedIndex = selectIndex;
        }
    }
}
