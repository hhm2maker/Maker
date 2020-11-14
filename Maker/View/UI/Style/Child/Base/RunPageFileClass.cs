using Maker.Model;
using Maker.View.Dialog;
using Maker.View.Style.Child;
using Maker.View.UI.Dialog.WindowDialog;
using Maker.View.UI.Utils;
using Maker.View.UIBusiness;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using static Maker.View.Style.Child.BaseStyle.RunModel;

namespace Maker.View.UI.Style.Child.Base
{
    public class RunPageFileClass
    {
        public BaseStyle Os
        {
            get;
            set;
        }

        public bool IsMultiple
        {
            get;
            set;
        }

        public List<String> Results
        {
            get;
            set;
        } = new List<string>();

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
        protected bool needClose = true;
            
        public void DrawRange(object sender, MouseButtonEventArgs e)
        {
            if (contextMenu == null)
            {
                contextMenu = new ContextMenu();
                contextMenu.Closed += ContextMenu_Closed;

                contextMenu.Style = (System.Windows.Style)StaticConstant.mw.Resources["myContextMenu"];
                
                RunCombo.ContextMenu = contextMenu;
            }

            string[] strs = RunCombo.Text.Split(',');
            Results.Clear();
            Results.AddRange(strs);
            contextMenu.Items.Clear();

            List<string> fileNames = Business.FileBusiness.CreateInstance().GetFilesName(StaticConstant.mw.LastProjectPath + @"\Play", new List<string>() { ".lightPage" });

            foreach (var item in fileNames)
            {
                MenuItem file = new MenuItem
                {
                    Header = item
                };

                if (Results.Contains(item))
                {
                    file.Icon = ResourcesUtils.Resources2BitMap("check.png");
                }
                else
                {
                    file.Icon = null;
                }
                file.Click += MenuItemList_Click;
                contextMenu.Items.Add(file);
            }

            needClose = true;
            contextMenu.IsOpen = true;
        }

        private void MenuItemList_Click(object sender, RoutedEventArgs e)
        {
            String file = (sender as MenuItem).Header.ToString();
            if (Results.Contains(file))
            {
                if (Results.Count == 0)
                {
                    contextMenu.IsOpen = false;
                    return;
                }
                Results.Remove(file);
            }
            else {
                if (!IsMultiple)
                {
                    Results.Clear();
                }
                Results.Add(file);
            }

            needClose = false;

            if (!IsMultiple)
            {
                RunCombo.Text = Results[0];
            }
            else
            {
                RunCombo.Text = "";

                for (int i = 0; i < Results.Count; i++)
                {
                    /*   if (lbPages.Items.Contains(dialog.lbMain.SelectedItems[i]))
                       {
                           continue;
                       }*/
                    if (i == 0)
                    {
                        RunCombo.Text += Results[i];
                    }
                    else { 
                        RunCombo.Text += ","+Results[i] ;
                    }
                }
            }

            contextMenu.IsOpen = false;
            Os.ToRefresh();
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
            if (needClose && Os!= null)
            {
                //RunCombo.Text = tb.Text.ToString();
                contextMenu.IsOpen = false;
                Os.ToRefresh();
            }
        }

    }
}
