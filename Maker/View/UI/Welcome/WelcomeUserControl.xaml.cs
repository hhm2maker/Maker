using Maker.Business;
using Maker.View.UI.UserControlDialog;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View.UI.Welcome
{
    /// <summary>
    /// WelcomeUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class WelcomeUserControl : MakerDialog
    {
        private NewMainWindow mw;
        public WelcomeUserControl(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;

            List<String> strs = FileBusiness.CreateInstance().GetDirectorysName(AppDomain.CurrentDomain.BaseDirectory + @"\Project");
            for (int i = 0; i < strs.Count; i++)
            {
                Border border = new Border();
                border.Margin = new Thickness(30,0,0,0);
                border.MouseEnter += StackPanel_MouseEnter;
                border.MouseLeave += StackPanel_MouseLeave;
                border.MouseLeftButtonDown += Border_MouseLeftButtonDown;
                border.CornerRadius = new CornerRadius(3);
                border.Background = new SolidColorBrush(Color.FromRgb(115, 120, 125));
                StackPanel sp = new StackPanel();
                sp.Margin = new Thickness(20); 
                border.Child = sp;
                sp.Orientation = Orientation.Vertical;
                TextBlock tb = new TextBlock();
                tb.Text = strs[i];
                tb.Foreground = new SolidColorBrush(Colors.White);
                tb.FontSize = 18;
                sp.Children.Add(tb);
                wpMain.Children.Add(border);
            }


        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mw.NewProject();
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = new SolidColorBrush(Color.FromRgb(184,191,198));
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = new SolidColorBrush(Color.FromRgb(115, 120, 125));
        }
    }
}
