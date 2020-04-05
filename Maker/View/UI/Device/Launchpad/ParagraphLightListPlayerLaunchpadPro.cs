using Maker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Maker.Business;
using Maker.View.Utils;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;
using Operation;
using MakerUI.Device;

namespace Maker.View.Device
{
    public class ParagraphLightListPlayerLaunchpadPro : PlayerLaunchpadPro
    {
        /// <summary>
        /// 时间集合
        /// </summary>
        private List<int> timeList = new List<int>();

        /// <summary>
        /// 时间段落字典
        /// </summary>
        private Dictionary<int, List<Light>> timeDictionary = new Dictionary<int, List<Light>>();

        /// <summary>
        /// 播放接口实现类
        /// </summary>
        private IPlay iplay;

        /// <summary>
        /// 后台
        /// </summary>
        private BackgroundWorker worker = new BackgroundWorker();

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="iplay"></param>
        public ParagraphLightListPlayerLaunchpadPro(IPlay iplay) : base()
        {
            //这里可以自定义笔刷数组
            brushList = StaticConstant.brushList;
            this.iplay = iplay;
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerSupportsCancellation = true;
        }

        /// <summary>
        /// 当前节点
        /// </summary>
        private int NowTimePosition;

        /// <summary>
        /// Bpm
        /// </summary>
        private Double dWait;

        /// <summary>
        /// 进度返回处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pce"></param>
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs pce)
        {
            int number = pce.ProgressPercentage;

            if (number == -1)
            {
                //清空
                ClearAllColorExceptMembrane();
                iplay.EndPlayEvent();

                return;
            }
            if (number == 0)
            {
                //开始播放事件 - 进程中
                iplay.StartPlayEvent();
            }
            if (number == timeList.Count - 1)
            {
                //结束播放事件 - 进程中
                iplay.EndPlayEvent();
            }

            List<Light> x = timeDictionary[timeList[number]];
            for (int i = 0; i < x.Count(); i++)
            {
                //RoundedCornersPolygon rcp = lfe[x[i]] as RoundedCornersPolygon;
                if (x[i].Action == 128)
                {
                    if (GetButton(x[i].Position) is RoundedCornersPolygon rcp)
                    {
                        rcp.Fill = closeBrush;
                    }
                    if (GetButton(x[i].Position) is Ellipse e)
                    {
                        e.Fill = closeBrush;
                    }
                    if (GetButton(x[i].Position) is Rectangle r)
                    {
                        r.Fill = closeBrush;
                    }
                }
                else
                {
                    if (GetButton(x[i].Position) is RoundedCornersPolygon rcp)
                    {
                        rcp.Fill = StaticConstant.brushList[x[i].Color];
                    }
                    if (GetButton(x[i].Position) is Ellipse e)
                    {
                        e.Fill = StaticConstant.brushList[x[i].Color];
                    }
                    if (GetButton(x[i].Position) is Rectangle r)
                    {
                        r.Fill = StaticConstant.brushList[x[i].Color];
                    }
                }
            }
         
        }

        /// <summary>
        /// 业务逻辑处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            long _startTime = DateTime.Now.Ticks / 10000;

            for (; NowTimePosition < timeList.Count; NowTimePosition++)
            {
                if (worker.CancellationPending)
                {
                    if (!bIsPause)
                    {
                        worker.ReportProgress(-1);//返回进度
                    }
                    e.Cancel = true;
                    break;
                }
                if (!isFirst)
                {
                    //Console.WriteLine(NowTimePosition + "---" + TimeSpan.FromMilliseconds(1000 / dWait * (timeList[NowTimePosition] - timeList[NowTimePosition - 1])));
                    double d = DateTime.Now.Ticks / 10000 - _startTime - (timeList[NowTimePosition] - timeList[0]) / dWait * 1000.0;
                    if (d > 0 && (timeList[NowTimePosition] - timeList[NowTimePosition - 1]) / dWait * 1000.0 - d > 0)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds((timeList[NowTimePosition] - timeList[NowTimePosition - 1]) / dWait * 1000.0 - d));
                    }
                    else {
                        if (NowTimePosition != 0) {
                            Thread.Sleep(TimeSpan.FromMilliseconds((timeList[NowTimePosition] - timeList[NowTimePosition - 1]) / dWait * 1000.0));
                        }
                    }

                    //if (NowTimePosition == 545)
                    //{
                    //    Console.WriteLine(d);
                    //}

                    //long _nowTime = DateTime.Now.Ticks / 10000;
                    //double d = timeList[NowTimePosition] / dWait * 1000;
                    //double d2 = timeList[NowTimePosition-1] / dWait * 1000;dw

                    //Console.WriteLine(1000.0 / dWait * (timeList[NowTimePosition] - timeList[NowTimePosition - 1]) + _nowTime - _startTime - timeList[NowTimePosition - 1] * dWait / 1000);
                    //Thread.Sleep(TimeSpan.FromMilliseconds(1000.0 / dWait * (timeList[NowTimePosition] - timeList[NowTimePosition - 1]) + _nowTime - _startTime - 1000.0 / dWait * (timeList[NowTimePosition-1])));
                    //_startTime = _nowTime;

                    //Dispatcher.Invoke(new Action(() => {
                    //    Console.WriteLine(mediaElement.Position);
                    //}));
                }                                                                          
                else {
                    isFirst = false;
                }
                //else {
                //    Thread.Sleep(TimeSpan.FromMilliseconds(1000 / dWait * (timeList[NowTimePosition])));
                //}
                 worker.ReportProgress(NowTimePosition);//返回进度
            }
        }

        bool isFirst = true;

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="mActionBeanList"></param>
        public override void SetData(List<Light> mActionBeanList)
        {
            //清空数据
            timeList.Clear();
            timeDictionary.Clear();
            //获取数据
            timeList = Business.LightBusiness.GetTimeList(mActionBeanList);
            timeDictionary = Business.LightBusiness.GetParagraphLightLightList(mActionBeanList);
        }

        private int myTTT;

        /// <summary>
        /// 播放
        /// </summary>
        public override void Play()
        {
            isFirst = true;
            myTTT = timeList.IndexOf(SmallTime);
            NowTimePosition = myTTT;

            //NowTimePosition = 0;
            //开始播放事件
            iplay.PlayEvent();
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public override void Stop()
        {
            //停止播放事件
            iplay.StopEvent();

            worker.CancelAsync();
            NowTimePosition = 0;
        }


        /// <summary>
        /// 是否在暂停状态
        /// </summary>
        private Boolean bIsPause = false;

        /// <summary>
        /// 暂停/恢复
        /// </summary>
        public override void Pause()
        {
            if (bIsPause)
            {
                worker.RunWorkerAsync();
            }
            else
            {
                worker.CancelAsync();
            }
            //暂停播放事件
            iplay.PauseEvent(bIsPause);
            bIsPause = !bIsPause;
        }

        /// <summary>
        /// 设置BPM
        /// </summary>
        public override void SetWait(double dWait)
        {
            this.dWait = dWait;
        }
    }
}
