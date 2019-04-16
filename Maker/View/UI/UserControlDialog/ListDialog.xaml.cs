using Maker.Business.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static Maker.Business.Model.ThirdPartySetupsModel;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class ListDialog : MakerDialog
    {
        private NewMainWindow mw;
        
        public ListDialog(NewMainWindow mw,List<String> strs, SelectionChangedEventHandler selectionChangedEventHandler, String hintText)
        {
            InitializeComponent();
            this.mw = mw;
            tbHint.Text = hintText;
            lbMain.SelectionChanged += selectionChangedEventHandler;

            for (int i = 0; i < strs.Count; i++) {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = strs[i];
                lbMain.Items.Add(listBoxItem);
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            mw.RemoveDialog();
        }

       
    }
}
