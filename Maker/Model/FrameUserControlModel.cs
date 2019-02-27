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
                    DeleteImgSource = "../../Resources/Image/delete_gray.png";
                }
                else
                {
                    DeleteImgSource = "../../Resources/Image/delete_blue.png";
                }
                if (nowTimePoint < 2)
                {
                    LeftImgSource = "../../Resources/Image/toleft_gray.png";
                }
                else
                {
                    LeftImgSource = "../../Resources/Image/toleft_blue.png";
                }
                if (nowTimePoint > liTime.Count-1)
                {
                    RightImgSource = "../../Resources/Image/toright_gray.png";
                }
                else
                {
                    RightImgSource = "../../Resources/Image/toright_blue.png";
                }
                RaisePropertyChanged(() => NowTimePoint);
                LoadFrame();
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
        /// 开始图片资源
        /// </summary>
        private String startImgSource = "../../Resources/Image/start_gray.png";
        public String StartImgSource
        {
            get { return startImgSource; }
            set
            {
                startImgSource = value;
                RaisePropertyChanged(() => StartImgSource);
            }
        }

        /// <summary>
        /// 向左图片资源
        /// </summary>
        private String leftImgSource = "../../Resources/Image/toleft_gray.png";
        public String LeftImgSource
        {
            get { return leftImgSource; }
            set
            {
                leftImgSource = value;
                RaisePropertyChanged(() => LeftImgSource);
            }
        }

        /// <summary>
        /// 向左图片资源
        /// </summary>
        private String rightImgSource = "../../Resources/Image/toright_gray.png";
        public String RightImgSource
        {
            get { return rightImgSource; }
            set
            {
                rightImgSource = value;
                RaisePropertyChanged(() => RightImgSource);
            }
        }

        /// <summary>
        /// 删除图片资源
        /// </summary>
        private String deleteImgSource = "../../Resources/Image/delete_gray.png";
        public String DeleteImgSource
        {
            get { return deleteImgSource; }
            set
            {
                deleteImgSource = value;
                RaisePropertyChanged(() => DeleteImgSource);
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
                    StartImgSource = "../../Resources/Image/start_gray.png";
                }
                else {
                    StartImgSource = "../../Resources/Image/start_blue.png";
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
