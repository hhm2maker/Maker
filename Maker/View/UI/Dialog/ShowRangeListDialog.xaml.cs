using Maker.View.LightScriptUserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ShowRangeListDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ShowRangeListDialog : Window
    {
        public ScriptUserControl iuc;
        public ShowRangeListDialog(ScriptUserControl iuc)
        {
            InitializeComponent();
            this.iuc = iuc;
            Owner = iuc.mw;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left = 0;
            //if (iuc.mw.bIsRangeListNumber) {
            //    cbShowNumber.IsChecked = true;
            //}
            //else
            //{ 
            //    cbShowNumber.IsChecked = false;
            //}
            //RefrushData();
        }
        public void RefrushData() {
            lbMain.Items.Clear();
            if (cbShowNumber.IsChecked == true)
            {
                foreach (var item in iuc.rangeDictionary)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(item.Key + "：");
                    foreach (int i in item.Value)
                    {
                        builder.Append(i + " ");
                    }
                    lbMain.Items.Add(builder.ToString().Trim());
                }
            }
            else
            {
                foreach (var item in iuc.rangeDictionary)
                {
                    lbMain.Items.Add(item.Key);

                }
            }
        }

        private void cbShowNumber_Checked(object sender, RoutedEventArgs e)
        {
            if (iuc != null)
            {
                lbMain.Items.Clear();

                foreach (var item in iuc.rangeDictionary)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(item.Key + "：");
                    foreach (int i in item.Value)
                    {
                        builder.Append(i + " ");
                    }
                    lbMain.Items.Add(builder.ToString());
                }

                //if (iuc.mw_.bIsRangeListNumber)
                //    return;
                //XmlDocument doc = new XmlDocument();
                //doc.Load("Config/hide.xml");
                //XmlNode hideRoot = doc.DocumentElement;
                //XmlNode hideRangeListNumber = hideRoot.SelectSingleNode("RangeListNumber");
                //hideRangeListNumber.InnerText = "true";
                //iuc.mw_.bIsRangeListNumber = true;
                //doc.Save("Config/hide.xml");
            }
        }
        private void cbShowNumber_Unchecked(object sender, RoutedEventArgs e)
        {
            //if (iuc != null)
            //{
            //    lbMain.Items.Clear();
            //    foreach (var item in iuc.rangeDictionary)
            //    {
            //        lbMain.Items.Add(item.Key);
            //    }

            //    if (!iuc.mw_.bIsRangeListNumber)
            //        return;
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load("Config/hide.xml");
            //    XmlNode hideRoot = doc.DocumentElement;
            //    XmlNode hideRangeListNumber = hideRoot.SelectSingleNode("RangeListNumber");
            //    hideRangeListNumber.InnerText = "false";
            //    iuc.mw_.bIsRangeListNumber = false;
            //    doc.Save("Config/hide.xml");
            //}
        }
        private void NewRangeList(object sender, RoutedEventArgs e)
        {
            AddNewRangeListDialog dialog = new AddNewRangeListDialog(this,0,"");
            if (dialog.ShowDialog() == true) {
                iuc.rangeDictionary.Add(dialog.RangeName,dialog.MultipleNumber);
                RefrushData();
            }
        }
        private void EditRangeList(object sender, RoutedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            String[] str = iuc.rangeDictionary.Keys.ToArray();
            AddNewRangeListDialog dialog = new AddNewRangeListDialog(this, 1, str[lbMain.SelectedIndex]);
            if (dialog.ShowDialog() == true)
            {
                iuc.rangeDictionary[dialog.RangeName] = dialog.MultipleNumber;
                RefrushData();
            }
        }
        private void DeleteRangeList(object sender, RoutedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            String[] str = iuc.rangeDictionary.Keys.ToArray();
            iuc.rangeDictionary.Remove(str[lbMain.SelectedIndex]);
            lbMain.Items.RemoveAt(lbMain.SelectedIndex);
        }

        private void lbMain_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            DialogResult = true;
        }

        private void CopyRangeListContent(object sender, RoutedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            String[] str = iuc.rangeDictionary.Keys.ToArray();
            List<int> list = iuc.rangeDictionary[str[lbMain.SelectedIndex]].ToList();
            StringBuilder builder = new StringBuilder();
            foreach (int i in list) {
                builder.Append(i+" ");
            }
            Clipboard.SetDataObject(builder.ToString().Trim());
        }
        
    }
}
