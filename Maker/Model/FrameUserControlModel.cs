using GalaSoft.MvvmLight;
using Maker.View.LightUserControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Maker.Model.FramePointModel;

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
                if (nowTimePoint == 0)
                {
                    CanDelete = false; 
                }
                else
                {
                    CanDelete = true;
                }
                if (nowTimePoint < 2)
                {
                    CanLeft = false;
                }
                else
                {
                    CanLeft = true;
                }
                if (nowTimePoint > liTime.Count-1)
                {
                    CanRight = false;
                }
                else
                {
                    CanRight = true;
                }
                RaisePropertyChanged(() => NowTimePoint);
                LoadFrame();
            }
        }

        /// <summary>
        /// 时间节点是否可以向左
        /// </summary>
        private bool canLeft;
        public bool CanLeft
        {
            get { return canLeft; }
            set
            {
                canLeft = value;
                RaisePropertyChanged(() => CanLeft);
            }
        }

        /// <summary>
        /// 时间节点是否可以向右
        /// </summary>
        private bool canRight;
        public bool CanRight
        {
            get { return canRight; }
            set
            {
                canRight = value;
                RaisePropertyChanged(() => CanRight);
            }
        }

        /// <summary>
        /// 是否可以开始
        /// </summary>
        private bool canStart;
        public bool CanStart
        {
            get { return canStart; }
            set
            {
                canStart = value;
                RaisePropertyChanged(() => CanStart);
            }
        }

        /// <summary>
        /// 是否可以删除
        /// </summary>
        private bool canDelete;
        public bool CanDelete
        {
            get { return canDelete; }
            set
            {
                canDelete = value;
                RaisePropertyChanged(() => CanDelete);
            }
        }

        public void LoadFrame()
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

            if (!points.ContainsKey(nowTimePoint))
            {
                Texts = new List<Text>();
                ListBoxData = new ObservableCollection<dynamic>();
            }
            else
            {
                Texts = points[nowTimePoint].Texts;

                ListBoxData = new ObservableCollection<dynamic>();
                for (int i = 0; i < Texts.Count; i++) {
                    (ListBoxData as ObservableCollection<dynamic>).Add(Texts[i].Value);
                }
            }
            if(StaticConstant.mw.playuc.ip != null)
                StaticConstant.mw.playuc.ip.PlayIntLight(NowData[LiTime[NowTimePoint - 1]]);
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
        /// 当前选中位置
        /// </summary>
        private List<int> selects = new List<int>();
        public List<int> Selects
        {
            get { return selects; }
            set
            {
                selects = value;
                RaisePropertyChanged(() => Selects);
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
                if (liTime.Contains(0))
                {
                    CanStart = false;
                }
                else {
                    CanStart = true;
                }
                RaisePropertyChanged(() => LiTime);
            }
        }

       
        private Dictionary<int, FramePointModel> points = new Dictionary<int, FramePointModel>();
        public Dictionary<int, FramePointModel> Points
        {
            get { return points; }
            set
            {
                points = value;

                if (!points.ContainsKey(nowTimePoint))
                {
                    Texts = new List<Text>();
                    ListBoxData = new ObservableCollection<dynamic>();
                }
                else
                {
                    Texts = points[nowTimePoint].Texts;

                    ListBoxData = new ObservableCollection<dynamic>();
                    for (int i = 0; i < Texts.Count; i++)
                    {
                        (ListBoxData as ObservableCollection<dynamic>).Add(Texts[i].Value);
                    }
                }
            }
        }

        /// <summary>
        /// 文本数组
        /// </summary>
        private List<Text> texts ;
        public List<Text> Texts
        {
            get { return texts; }
            set
            {
                texts = value;
                RaisePropertyChanged(() => Texts);
            }
        }

        private IEnumerable listBoxData;
        /// <summary>
        /// LisBox数据模板
        /// </summary>
        public IEnumerable ListBoxData
        {
            get { return listBoxData; }
            set {
                listBoxData = value;
                RaisePropertyChanged(() => ListBoxData);
            }
        }

        /// <summary>
        /// 自定义添加时间节点图片资源
        /// </summary>
        private String diyAddImgSource = "../../Resources/Image/add_gray.png";
        public String DiyAddImgSource
        {
            get { return diyAddImgSource; }
            set
            {
                diyAddImgSource = value;
                RaisePropertyChanged(() => DiyAddImgSource);
            }
        }

        /// <summary>
        /// 当前自定义字符串
        /// </summary>
        private String diyAddStr = String.Empty;
        public String DiyAddStr
        {
            get { return diyAddStr; }
            set
            {
                diyAddStr = value;

                String strTime = diyAddStr.Trim();
                if (strTime.Trim().Equals(""))
                {
                    DiyAddImgSource = "../../Resources/Image/add_gray.png";
                    return;
                }
                int time = 0;
                try
                {
                    if (strTime.Contains("+"))
                    {
                        //当前时间 +
                        time = int.Parse(strTime) + int.Parse(diyAddStr.Substring(1));
                    }
                    else if (strTime.Contains("-"))
                    {
                        //当前时间 -
                        time = int.Parse(strTime) - int.Parse(diyAddStr.Substring(1));
                    }
                    else
                    {
                        //当前时间
                        time = int.Parse(strTime);
                    }

                    if (time < 0)
                    {
                        DiyAddImgSource = "../../Resources/Image/add_gray.png";
                        return;
                    }
                }
                catch
                {
                    DiyAddImgSource = "../../Resources/Image/add_gray.png";
                    return;
                }
                //如果已经有该时间点，报错
                if (LiTime.Contains(time))
                {
                    DiyAddImgSource = "../../Resources/Image/add_gray.png";
                }
                else
                {
                    DiyAddImgSource = "../../Resources/Image/add_blue.png";
                }
                RaisePropertyChanged(() => DiyAddStr);
            }
        }
    }
}
