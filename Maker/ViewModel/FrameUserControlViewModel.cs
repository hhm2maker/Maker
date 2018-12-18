using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maker.Model;
using Maker.View.LightUserControl;

namespace Maker.ViewModel
{
    public class FrameUserControlViewModel : ViewModelBase
    {
            /// <summary>
            /// 构造函数
            /// </summary>
            public FrameUserControlViewModel()
            {
                Welcome = new FrameUserControlModel() { NowTimePoint = 0 };
            }


        private FrameUserControlModel welcome;
            /// <summary>
            /// 欢迎词属性
            /// </summary>
            public FrameUserControlModel Welcome
            {
                get { return welcome; }
                set { welcome = value; RaisePropertyChanged(() => Welcome); }
            }

        public int NowTimePoint
        {
            get { return Welcome.NowTimePoint; }
            set { Welcome.NowTimePoint = value; }
        }

    }
}
