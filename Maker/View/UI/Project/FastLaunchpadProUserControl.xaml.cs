using Maker.Business;
using Maker.Model;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View.UI.Project
{
    /// <summary>
    /// FastLaunchpadProUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class FastLaunchpadProUserControl : UserControl
    {
        public FastLaunchpadProUserControl(UserControl uc,double size)
        {
            InitializeComponent();
            mLaunchpad.Size = size;
        }

        public void Play() {
            InitData();
        }

        List<Light> lightList; 
        private void InitData()
        {
            // .net 4.5
            lightList = Business.FileBusiness.CreateInstance().ReadMidiFileNoFormatTime(@"C:\Users\Administrator\Desktop\AAA.mid");

            foo();
            async void foo()
            {
                for (int i = 0; i < lightList.Count; i++) {
                    if (i > 20000)
                        return;

                    if (lightList[i].Time != 0) {
                        await Task.Delay((int)(lightList[i].Time * 1000 /96.0));
                    }
                    if (lightList[i].Action == 144)
                    {
                        mLaunchpad.SetButtonBackground(lightList[i].Position, StaticConstant.brushList[lightList[i].Color]);
                    }
                    else
                    {
                        mLaunchpad.SetButtonBackground(lightList[i].Position, StaticConstant.closeBrush);
                    }
                    Console.WriteLine(lightList[i].Position + "---" + lightList[i].Color);
                }
            }
        }
    }
}
