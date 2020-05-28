using Maker.View.Style.Child;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;

namespace Maker.View.UI.Style.Child.Base
{
    public class ColorRunClass
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

        Popup popup;
        public void DrawRange(object sender, MouseButtonEventArgs e)
        {
            popup = new Popup();
            popup.PlacementTarget = TbMain;
            popup.Placement = PlacementMode.Bottom;
            popup.AllowsTransparency = true;
            popup.PopupAnimation = PopupAnimation.Fade;
            popup.StaysOpen = false;
            ColorPanel colorPanel = new ColorPanel();
            colorPanel.SelectionChanged += ColorPanel_SelectionChanged;
            popup.Child = colorPanel;
            popup.HorizontalOffset = e.GetPosition(TbMain).X;
            popup.IsOpen = true;
        }

        private void ColorPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RunCombo.Text = ((ListBox)sender).SelectedIndex.ToString();
            popup.IsOpen = false;
            Os.ToRefresh();
        }
    }
}
