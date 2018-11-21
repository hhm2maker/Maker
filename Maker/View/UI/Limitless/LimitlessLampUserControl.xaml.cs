using Maker.View.Control;
using System.Collections.Generic;
using System.Windows;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ChangeIntoMotionDialog.xaml 的交互逻辑
    /// </summary>
    public partial class LimitlessLampUserControl : BaseUserControl
    {
        public LimitlessLampUserControl(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;

            _fileExtension = ".limitlessLamp";
            _fileType = "LimitlessLamp";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddColumn(object sender, RoutedEventArgs e)
        {
            mLaunchpad.AddColumn();
        }
        private void RemoveColumn(object sender, RoutedEventArgs e)
        {
            mLaunchpad.RemoveColumn();
        }

        public List<int> NumberList {
            get;
            set;
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            NumberList = mLaunchpad.GetNumber();
            if (!NumberList.Contains(1))
            {
                //啥也没有
                //DialogResult = false;
            }
            else {
                //DialogResult = true;
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //DialogResult = false;
        }

        public void AA() {
            //ChangeIntoMotionDialog dialog = new ChangeIntoMotionDialog(mw);
            //if (dialog.ShowDialog() == true)
            //{
            //    StringBuilder builder = new StringBuilder();
            //    foreach (int number in dialog.NumberList)
            //    {
            //        builder.Append(number.ToString() + " ");
            //    }
            //    String stepName = GetUsableStepName();
            //    if (stepName == null)
            //    {
            //        new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
            //        return;
            //    }
            //    String commandLine = "\tLightGroup " + stepName + "LightGroup = Create.Animation(Translation,\""
            //        + builder.ToString().Trim() + "\");";

            //    lightScriptDictionary.Add(stepName, commandLine);
            //    //visibleDictionary.Add(stepName, true);
            //    containDictionary.Add(stepName, new List<string>() { stepName });
               
            //}
        }

        private void AddRow(object sender, RoutedEventArgs e)
        {
            mLaunchpad.AddRow();
        }
        private void RemoveRow(object sender, RoutedEventArgs e)
        {
            //mLaunchpad.RemoveRow();
        }
    }
}
