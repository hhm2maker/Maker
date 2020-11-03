using Maker.Model;
using Maker.View.Dialog;
using Maker.View.Style.Child;
using Maker.View.UI.Dialog.WindowDialog;
using Maker.View.UIBusiness;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Maker.View.UI.Style.Child.Base
{
    public class RunFileClass
    {
        public OperationStyle Os
        {
            get;
            set;
        }

        public List<String> Data
        {
            get;
            set;
        }

        public List<Run> Runs {
            get;
            set;
        }

        public Run RunCombo
        {
            get;
            set;
        }

        public TextBlock TbMain
        {
            get;
            set;
        }
        protected ContextMenu contextMenu;
        protected Popup popup;
        protected TextBox tb;
        protected bool needClose = true;

        public void DrawRange(object sender, MouseButtonEventArgs e)
        {
            if (contextMenu == null)
            {
                contextMenu = new ContextMenu();
                contextMenu.Closed += ContextMenu_Closed;

                MenuItem menuItemText = new MenuItem();
                tb = GeneralMainViewBusiness.GetTextBoxStatic();
                tb.IsEnabled = false;
                tb.Margin = new Thickness(0);
                menuItemText.Header = tb;
                contextMenu.Style = (System.Windows.Style)StaticConstant.mw.Resources["myContextMenu"];

                contextMenu.Items.Add(menuItemText);

                contextMenu.Items.Add(new Separator());

                MenuItem menuItemList = new MenuItem
                {
                    Header = (string)Application.Current.FindResource("File")
                };
                menuItemList.Click += MenuItemList_Click;
                contextMenu.Items.Add(menuItemList);


                RunCombo.ContextMenu = contextMenu;
            }

            needClose = true;
            tb.Text = RunCombo.Text;
            contextMenu.IsOpen = true;
        }

        private void MenuItemList_Click(object sender, RoutedEventArgs e)
        {
            ShowMyContentWindowDialog dialog = new ShowMyContentWindowDialog(Os.suc);
            if (dialog.ShowDialog() == true)
            {
                if (dialog.resultFileName.EndsWith(".lightScript"))
                {
                    if ((Os.suc.mw.LastProjectPath + @"\LightScript\" + dialog.resultFileName).Equals(Os.suc.filePath))
                    {
                        return;
                    }
                    ImportLibraryDialog _dialog = new ImportLibraryDialog(Os.suc.mw, Os.suc, Os.suc.mw.LastProjectPath + @"\LightScript\" + dialog.resultFileName, FinishEvent);
                    Os.suc.mw.ShowMakerDialog(_dialog);
                }
                else
                {
                    needClose = false;

                    RunCombo.Text = dialog.resultFileName;
                    contextMenu.IsOpen = false;
                    Os.ToRefresh();
                }
            }
        }

        private void FinishEvent(String fileName, String stepName)
        {
            needClose = false;

            RunCombo.Text = fileName;
            Runs[4].Text = stepName;
            contextMenu.IsOpen = false;
            Os.ToRefresh();
        }

        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            if (needClose)
            {
                RunCombo.Text = tb.Text.ToString();
                contextMenu.IsOpen = false;
                Os.ToRefresh();
            }
        }

    }
}
