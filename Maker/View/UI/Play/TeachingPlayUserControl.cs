using Maker.Model;
using Maker.View.Device;
using Maker.View.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Maker.View.UI.Play
{
    public class TeachingPlayUserControl : PlayUserControl
    {
        private TeachingControl teachingControl;
        private TeachingLaunchpadPro teachingLaunchpadPro;
        public TeachingPlayUserControl(NewMainWindow mw) : base(mw)
        {
            SizeChanged += UserControl_SizeChanged;
            //< play:TeachingControl Grid.Row = "6" x: Name = "mTeachingControl" ClipToBounds = "True" Visibility = "Collapsed" />
            //< device:TeachingLaunchpadPro x:Name = "mLaunchpad" Grid.Row = "6" />
            teachingControl = new TeachingControl();
            teachingControl.ClipToBounds = true;
            teachingControl.Visibility = System.Windows.Visibility.Collapsed;
            Grid.SetRow(teachingControl,6);
            gMost.Children.Add(teachingControl);

            teachingLaunchpadPro = new TeachingLaunchpadPro();
            Grid.SetRow(teachingLaunchpadPro, 6);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            teachingControl.SetSize(teachingControl.ActualHeight);
        }

        protected override void InitTeachingData(List<Light> tutorialLights)
        {
            teachingControl.InitTeaching(tutorialLights);
            teachingLaunchpadPro.SetTeachingData(tutorialLights);
        }
    }
}
