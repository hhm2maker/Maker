using Maker.View.Style.Child;
using Maker.View.UIBusiness;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;

namespace Maker.View.UI.Style.Child.Base
{
    public class RunColorClass : RunContentClass
    {

        public override UIElement GetContent()
        {
            ColorPanel colorPanel = new ColorPanel(10);
            colorPanel.SelectionChanged += ColorPanel_SelectionChanged;
            return colorPanel;
        }

        private void ColorPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            needClose = false;

            RunCombo.Text = ((ListBox)sender).SelectedIndex.ToString();
            contextMenu.IsOpen = false;
            Os.ToRefresh();
        }
    }
}
