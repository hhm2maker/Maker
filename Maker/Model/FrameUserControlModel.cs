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
        /// <summary>
        /// 当前时间节点
        /// </summary>
        private int nowTimePoint;
        public int NowTimePoint
        {
            get { return nowTimePoint; }
            set
            {
                nowTimePoint = value;
                RaisePropertyChanged(() => NowTimePoint);
                LoadFrame();
            }
        }

        private void LoadFrame()
        {
            if (NowTimePoint == 0)
            {
                CurrentFrame = 0;
                return;
            }

            CurrentFrame = LiTime[NowTimePoint - 1];

            List<Light> mLightList = new List<Light>();

            int[] x = NowData[LiTime[NowTimePoint - 1]];
            for (int i = 0; i < x.Count(); i++)
            {
                if (x[i] == 0)
                {
                    continue;
                }
                mLightList.Add(new Light(0, 144, i + 28, x[i]));
            }
            NowLightLight = mLightList;
        }

        /// <summary>
        /// 总时间节点(次数)
        /// </summary>
        private int allTimePoint;
        public int AllTimePoint
        {
            get { return allTimePoint; }
            set
            {
                allTimePoint = value;
                RaisePropertyChanged(() => AllTimePoint);
            }
        }

        /// <summary>
        /// 当前灯光
        /// </summary>
        private List<Light> nowLightLight;
        public List<Light> NowLightLight
        {
            get { return nowLightLight; }
            set
            {
                nowLightLight = value;
                RaisePropertyChanged(() => NowLightLight);
            }
        }

        /// <summary>
        /// 当前帧
        /// </summary>
        private int currentFrame;
        public int CurrentFrame
        {
            get { return currentFrame; }
            set
            {
                currentFrame = value;
                RaisePropertyChanged(() => CurrentFrame);
            }
        }

        /// <summary>
        /// 当前数据
        /// </summary>
        private Dictionary<int, int[]> nowData = new Dictionary<int, int[]>();
        public Dictionary<int, int[]> NowData
        {
            get { return nowData; }
            set
            {
                nowData = value;
                RaisePropertyChanged(() => NowData);
            }
        }

        /// <summary>
        /// 时间数组
        /// </summary>
        private List<int> liTime = new List<int>();
        public List<int> LiTime
        {
            get { return liTime; }
            set
            {
                liTime = value;
                RaisePropertyChanged(() => LiTime);
            }
        }
       
    }
}
