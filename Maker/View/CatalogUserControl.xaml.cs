using Maker;
using Maker.View.LightWindow;
using MakerLight.View.Catalog;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View
{
    /// <summary>
    /// CatalogUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class CatalogUserControl : UserControl
    {
        private NewMainWindow mw;
        public CatalogUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            cluc = new CatalogLightUserControl(mw);
            fw = new FrameWindow(mw)
            {
                Width = mw.Width,
                Height = mw.Height
            };
            tbw = new TextBoxWindow(mw)
            {
                Width = mw.Width,
                Height = mw.Height
            };
            prw = new PianoRollWindow(mw)
            {
                Width = mw.Width,
                Height = mw.Height
            };
        }
        private Color darkColor = Color.FromRgb(40,40,40);
        private Color lightColour = Color.FromRgb(163, 163, 163);
        private Color darkColorSelect = Color.FromRgb(255, 255, 255);
        private Color lightColourSelect = Color.FromRgb(240, 240, 240);
        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            //如果点击选中项返回
            if (selectObject == sender)
                return;
            (sender as StackPanel).Background = new SolidColorBrush(Color.FromRgb( 221, 221, 221));
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            //如果点击选中项返回
            if (selectObject == sender)
                return;
            (sender as StackPanel).Background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
        }

        private Object selectObject;
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //如果点击选中项返回
            if (selectObject == sender)
                return;
            if (selectObject != null) {
                StackPanel spOld = selectObject as StackPanel;
                foreach (TextBlock tb in spOld.Children) {
                    if ((tb.Foreground as SolidColorBrush).Color == darkColorSelect)
                    {
                        tb.Foreground = new SolidColorBrush(darkColor);
                    }
                    else {
                        tb.Foreground = new SolidColorBrush(lightColour);
                    }
                }
                spOld.Background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
            }
            StackPanel spNew = sender as StackPanel;
            foreach (TextBlock tb in spNew.Children)
            {
                if ((tb.Foreground as SolidColorBrush).Color == darkColor)
                {
                    tb.Foreground = new SolidColorBrush(darkColorSelect);
                }
                else
                {
                    tb.Foreground = new SolidColorBrush(lightColourSelect);
                }
            }
            spNew.Background = new SolidColorBrush(Color.FromRgb(68, 119, 64));
            selectObject = sender;

            if (sender == spLight)
            {
                ToLightUserControl();
            }
        }
        private CatalogLightUserControl cluc;
        private void ToLightUserControl()
        {
          
        }

        private void ToAboutUserControl(object sender, MouseButtonEventArgs e)
        {
            mw.auc.Visibility = Visibility.Visible;
           DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));
            mw.auc.BeginAnimation(OpacityProperty, daV);
        }

        private void DaV_Completed(object sender, EventArgs e)
        {

        }
        private FrameWindow fw;
        private TextBoxWindow tbw;
        private PianoRollWindow prw;

     
        private void ToFrameWindow(object sender, RoutedEventArgs e)
        {
            if (!fw.IsActive)
            {
                fw.Show();
            }
            fw.Activate();
        }
        private void ToTextBoxWindow(object sender, RoutedEventArgs e)
        {
            if (!tbw.IsActive)
            {
                tbw.Show();
            }
            tbw.Activate();
        }
        private void ToPianoRollWindow(object sender, RoutedEventArgs e)
        {
            if (!prw.IsActive)
            {
                prw.Show();
            }
            prw.Activate();
        }
    }
}
