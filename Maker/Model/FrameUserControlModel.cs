using GalaSoft.MvvmLight;
using Maker.View.LightUserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Model
{
        public class FrameUserControlModel : ObservableObject
        {
            private int nowTimePoint;
            /// <summary>
            /// 欢迎词
            /// </summary>
            public int NowTimePoint
            {
                get { return nowTimePoint; }
                set {
                if (int.TryParse(value.ToString(), out int result))
                {
                    nowTimePoint = result;
                }
                else {
                    nowTimePoint = 0;
                }
                    RaisePropertyChanged(() => NowTimePoint);
                }
            }
        }

}
