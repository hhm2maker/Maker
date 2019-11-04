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

namespace Maker.View.Device
{
    public class ParagraphLightListPlayerLaunchpadPro3 : PlayerLaunchpadPro
    {
        /// <summary>
        /// 笔刷列表
        /// </summary>
        private List<SolidColorBrush> brushList = new List<SolidColorBrush>();

        /// <summary>
        /// 时间集合
        /// </summary>
        private List<int> timeList = new List<int>();

        /// <summary>
        /// 时间段落字典
        /// </summary>
        private Dictionary<int, TimeSpan> timeDictionary = new Dictionary<int, TimeSpan>();

        private Dictionary<int, List<Light>> _timeDictionary = new Dictionary<int, List<Light>>();

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
        public ParagraphLightListPlayerLaunchpadPro3(IPlay iplay) : base()
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

            List<Light> x = _timeDictionary[timeList[number]];
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

                //Console.WriteLine(NowTimePosition + "---" + TimeSpan.FromMilliseconds(1000 / dWait * (timeList[NowTimePosition] - timeList[NowTimePosition - 1])));
                //Thread.Sleep(timeDictionary[timeList[NowTimePosition]]);

                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000 / dWait * (timeList[NowTimePosition] - timeList[NowTimePosition - 1])));
                }

                //Dispatcher.Invoke(new Action(() => {
                //    Console.WriteLine(mediaElement.Position);
                //}));

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
            dWait = 90;
            //清空数据
            timeList.Clear();
            timeDictionary.Clear();
            _timeDictionary.Clear();
            //获取数据
            timeList = LightBusiness.GetTimeList(mActionBeanList);
            _timeDictionary = LightBusiness.GetParagraphLightLightList(mActionBeanList);
            for (int i = 0; i < timeList.Count; i++)
            {
                if (i == timeList.Count - 1)
                    continue;
                if (i == 0)
                {
                    timeDictionary.Add(timeList[i], TimeSpan.FromMilliseconds(0));
                }
                else
                {
                    //timeDictionary.Add(timeList[i], TimeSpan.FromMilliseconds(1000 / dWait * (timeList[i] - timeList[i-1])));
                    timeDictionary.Add(timeList[i], TimeSpan.FromMilliseconds(1000 / dWait * (timeList[i] - timeList[0])));
                }

            }
        }

        private int myTTT;

        /// <summary>
        /// 播放
        /// </summary>
        public override void Play()
        {
            myTTT = timeList.IndexOf(SmallTime);
            NowTimePosition = myTTT;

            //NowTimePosition = 0;
            //开始播放事件
            //iplay.PlayEvent();
            //worker.RunWorkerAsync();

            //for (int i = NowTimePosition; i < timeList.Count; i++)
            //{
            //if (i == timeList.Count - 1)
            //    continue;
            //if (i == 0)
            //{
            //    timeDictionary.Add(timeList[i], TimeSpan.FromMilliseconds(0));
            //}
            //else
            //{
            //    timeDictionary.Add(timeList[i], TimeSpan.FromMilliseconds(1000 / dWait * (timeList[i + 1] - timeList[i])));
            //}
            int i = NowTimePosition;
            while (i <= timeList.Count - 1)
            {
                foo();
                // .net 4.5
                async void foo()
                {
                    Console.WriteLine(timeList.Count + "---" + i);
                    if (timeDictionary.ContainsKey(timeList[i]) && _timeDictionary.ContainsKey(timeList[i]))
                    {
                        await Task.Delay(timeDictionary[timeList[i]]);

                        try
                        {
                            List<Light> x = _timeDictionary[timeList[i]];
                            for (int z = 0; z < x.Count(); z++)
                            {
                                //RoundedCornersPolygon rcp = lfe[x[i]] as RoundedCornersPolygon;
                                if (x[z].Action == 128)
                                {
                                    GetButton(x[z].Position).Fill = closeBrush;
                                }
                                else
                                {
                                    GetButton(x[z].Position).Fill = StaticConstant.brushList[x[z].Color];
                                }
                            }
                        }
                        catch
                        {

                        }
                        i++;
                    }
                    //Thread thread = new Thread(new ParameterizedThreadStart(Method1));
                    //thread.Start(i);
                }
                //}
            }

        }

        void Method1(object my)
        {

            //       while (true)
            //       {
            //           Console.WriteLine(DateTime.Now.ToString()
            //+ "_" +
            //Thread.CurrentThread.ManagedThreadId.ToString());
            //           Thread.CurrentThread.Join(100);//阻止设定时间
            //       }

            //Thread.Sleep(timeDictionary[timeList[(int)my]]);

            foo();
            // .net 4.5
            async void foo()
            {
                await Task.Delay(timeDictionary[timeList[(int)my]]);

                List<Light> x = _timeDictionary[timeList[(int)my]];
                for (int i = 0; i < x.Count(); i++)
                {
                    //RoundedCornersPolygon rcp = lfe[x[i]] as RoundedCornersPolygon;
                    if (x[i].Action == 128)
                    {
                        GetButton(x[i].Position).Fill = closeBrush;
                    }
                    else
                    {
                        GetButton(x[i].Position).Fill = StaticConstant.brushList[x[i].Color];
                    }
                }
            }

            //Dispatcher.Invoke(new Action(() =>
            //{
            //    List<Light> x = _timeDictionary[timeList[(int)my]];
            //    for (int i = 0; i < x.Count(); i++)
            //    {
            //        //RoundedCornersPolygon rcp = lfe[x[i]] as RoundedCornersPolygon;
            //        if (x[i].Action == 128)
            //        {
            //            GetButton(x[i].Position).Fill = closeBrush;
            //        }
            //        else
            //        {
            //            GetButton(x[i].Position).Fill = StaticConstant.brushList[x[i].Color];
            //        }
            //    }
            //}));

            //new Thread(() =>
            //{

            //}).Start();

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
            return;

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
