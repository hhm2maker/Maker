using Maker.Model;
using Maker.View.Style.Child;
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
    public class RunContentClass
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
            if (contextMenu == null) {
                contextMenu = new ContextMenu();
                contextMenu.Closed += ContextMenu_Closed;

                MenuItem menuItemText = new MenuItem();
                tb = GeneralMainViewBusiness.GetTextBoxStatic();
                tb.Margin = new Thickness(0);
                menuItemText.Header = tb;
                contextMenu.Style = (System.Windows.Style)StaticConstant.mw.Resources["myContextMenu"];

                contextMenu.Items.Add(menuItemText);

                contextMenu.Items.Add(new Separator());

                MenuItem menuItemList = new MenuItem
                {
                    Header = "列表"
                };
                foreach (var item in (Os as OperationStyle).suc.rangeDictionary)
                {
                    MenuItem _menuItem = new MenuItem();
                    _menuItem.Header = item.Key;
                    _menuItem.Click += _menuItem_Click;
                    menuItemList.Items.Add(_menuItem);
                }
                contextMenu.Items.Add(menuItemList);

                UIElement uIElement = GetContent();
                if (uIElement != null) {
                    contextMenu.Items.Add(new Separator());
                    MenuItem menuItem = new MenuItem();
                    menuItem.Header = uIElement;
                    contextMenu.Items.Add(menuItem);
                }

                RunCombo.ContextMenu = contextMenu;
            }

            needClose = true;
            tb.Text = RunCombo.Text;
            contextMenu.IsOpen = true;

            //popup = new Popup();
            //popup.PlacementTarget = TbMain;
            //popup.Placement = PlacementMode.Bottom;
            //popup.AllowsTransparency = true;
            //popup.PopupAnimation = PopupAnimation.Fade;
            //popup.StaysOpen = false;
            //StackPanel sp = new StackPanel();
            //sp.Orientation = Orientation.Vertical;
            //TextBox tb = GeneralMainViewBusiness.GetTextBoxStatic();
            //tb.LostFocus += Tb_LostFocus;
            //sp.Children.Add(tb);
            //UIElement uIElement = GetContent();
            //if (GetContent() != null) {
            //    sp.Children.Add(uIElement);
            //}

            //popup.Child = sp;
            //popup.HorizontalOffset = e.GetPosition(TbMain).X;
            //popup.IsOpen = true;
        }

        private void _menuItem_Click(object sender, RoutedEventArgs e)
        {
            needClose = false;

            RunCombo.Text = ((MenuItem)sender).Header.ToString();
            contextMenu.IsOpen = false;
            Os.ToRefresh();
        }

        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            if (needClose) {
                RunCombo.Text = tb.Text.ToString();
                contextMenu.IsOpen = false;
                Os.ToRefresh();
            }
        }

        public virtual UIElement GetContent() {
            return null;
        }
    }
}
