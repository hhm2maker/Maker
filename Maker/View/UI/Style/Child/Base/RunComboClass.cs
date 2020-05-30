using Maker.View.Style.Child;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;

namespace Maker.View.UI.Style.Child.Base
{
    public class RunComboClass
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
        ComboBox comboBox;
        public void DrawRange(object sender, MouseButtonEventArgs e)
        {
            if (popup == null) {
                popup = new Popup
                {
                    PlacementTarget = TbMain,
                    Placement = PlacementMode.Bottom,
                    AllowsTransparency = true,
                    PopupAnimation = PopupAnimation.Fade,
                    StaysOpen = false
                };
                comboBox = BaseStyle.GetComboBoxStatic(Data, Combo_SelectionChanged);
                popup.Child = comboBox;
            }
            popup.HorizontalOffset = e.GetPosition(TbMain).X;
            comboBox.IsDropDownOpen = true;
            popup.IsOpen = true;
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RunCombo.Text = ((ComboBoxItem)(sender as ComboBox).SelectedItem).Content.ToString();
            popup.IsOpen = false;
            Os.ToRefresh();
        }
    }
}
