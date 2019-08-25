using Maker.Business;
using Maker.Model;
using Maker.View.Control;
using Maker.View.Device;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Maker.View.LightUserControl;
namespace Maker.View.Tool
{
    /// <summary>
    /// PavedLaunchpadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShowPianoRollUserControl : UserControl
    {
        private NewMainWindow mw;
        public ShowPianoRollUserControl(NewMainWindow mw,List<Light> mLightList)
        {
            InitializeComponent();
            this.mw = mw;

            pianoroll.SetData(mLightList);
        }
        

        private void btnPaved_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnPaved.IsEnabled = false;
            DoubleAnimation daHeight = new DoubleAnimation();
                daHeight.From = 1;
                daHeight.To = 0;
                daHeight.Duration = TimeSpan.FromSeconds(0.2);

            daHeight.Completed += Board_Completed;
            wMain.BeginAnimation(OpacityProperty, daHeight);
        }

        private void Board_Completed(object sender, EventArgs e)
        {
            mw.editUserControl.RemoveTool();
        }

        private void wMain_Loaded(object sender, RoutedEventArgs e)
        {
            //Width = mw.ActualWidth * 0.8;
            Height = mw.ActualHeight * 0.8;
        }

    }
}
