using Maker.Business;
using Maker.Model;
using Maker.View.Control;
using Maker.View.Device;
using Maker.View.LightScriptUserControl;
using Maker.View.UI;
using MakerUI.Device;
using Operation;
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

namespace Maker.View.Tool
{
    /// <summary>
    /// PavedLaunchpadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PavedUserControl : UserControl
    {
        private NewMainWindow mw;
        private List<Light> mLightList;
        private int pavedColumns;
        public PavedUserControl(NewMainWindow mw,List<Light> mLightList)
        {
            InitializeComponent();
            this.mw = mw;

            this.mLightList = mLightList;
            pavedColumns = mw.pavedConfigModel.Columns;
        }

        public PavedUserControl(NewMainWindow mw, List<Light> mLightList,int pavedColumns)
        {
            InitializeComponent();
            this.mw = mw;

            this.mLightList = mLightList;
            this.pavedColumns = pavedColumns;
        }

        private void WMain_Loaded(object sender, RoutedEventArgs e)
        {
            //透明度
            DoubleAnimation daOpacity = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            daOpacity.Completed += Animation_Completed;
            wMain.BeginAnimation(OpacityProperty, daOpacity);
        }
        public int Count;
        private void Animation_Completed(object sender, EventArgs e)
        {
            double d = wpMain.ActualWidth / pavedColumns;
            Dictionary<int, List<Light>> dil = LightBusiness.GetParagraphLightLightList(mLightList);
            Count = dil.Count;
            int max = mw.pavedConfigModel.Max;
            if (dil.Count > max) {
                for (int i = dil.Count - 1; i >= max ; i--) {
                    dil.Remove(dil.Last().Key);
                }
            }
            foreach (var item in dil)
            {
                LaunchpadPro pro = new LaunchpadPro();
                pro.SetLaunchpadBackground(new SolidColorBrush(Color.FromArgb(255, 83, 83, 83)));
                pro.SetData(item.Value);
                pro.Size = d;
                wpMain.Children.Add(pro);
            }
        }
    }
}
