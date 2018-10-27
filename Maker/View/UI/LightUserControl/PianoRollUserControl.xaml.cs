using Maker.Business;
using Maker.Model;
using Maker.Utils;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Maker.View.LightUserControl
{
    /// <summary>
    /// PianoRollWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PianoRollUserControl : BaseLightUserControl
    {
        public PianoRollUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            //mainView = gMain;
            //HideControl();
            mPianoRoll.CanDraw();
        }
    }
}