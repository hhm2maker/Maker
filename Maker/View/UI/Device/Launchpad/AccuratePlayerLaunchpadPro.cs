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

namespace Maker.View.Device
{
    public class AccuratePlayerLaunchpadPro: PlayerLaunchpadPro
    {
        /// <summary>
        /// 笔刷列表
        /// </summary>
        private List<SolidColorBrush> brushList = new List<SolidColorBrush>();
        /// <summary>
        /// 灯光组 - 灯光列表
        /// </summary>
        public List<Light> lightList = new List<Light>();
        private IPlay iplay;
        /// <summary>
        /// 后台
        /// </summary>
        private BackgroundWorker worker = new BackgroundWorker();

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <param name="iplay"></param>
        public AccuratePlayerLaunchpadPro() : base()
        {
            //这里可以自定义笔刷数组
            brushList = StaticConstant.brushList;
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerSupportsCancellation = true;
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="iplay"></param>
        public AccuratePlayerLaunchpadPro(IPlay iplay) :base() {
            //这里可以自定义笔刷数组
            brushList = StaticConstant.brushList;
            this.iplay = iplay;
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerSupportsCancellation = true;
        }

        /// <summary>
        /// 播放最大时间
        /// </summary>
        private int MaxTime = 0;

        /// <summary>
        /// 播放当前时间
        /// </summary>
        private int NowTime = 0;

        /// <summary>
        /// 等待时间 => 根据BPM计算得出
        /// </summary>
        private TimeSpan wait ;

        /// <summary>
        /// 进度返回处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (NowTime == 0)
            {
                //开始播放事件 - 进程中
                if(iplay != null)
                    iplay.StartPlayEvent();
            }
            if (NowTime == MaxTime)
            {
                //结束播放事件 - 进程中
                if (iplay != null)
                    iplay.EndPlayEvent();
            }
            int i = 0;
            for (int l = i; l < lightList.Count; l++)
            {
                if (lightList[l].Time == NowTime)
                {
                    i = l+1;
                    if (lightList[l].Action == 128)
                    {
                        //停止播放=取消着色
                        SetButtonBackground(lightList[l].Position,closeBrush);
                    }
                    if (lightList[l].Action == 144)
                    {
                        //开始播放=开始着色 
                        SetButtonBackground(lightList[l].Position, brushList[lightList[l].Color]);
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
            //获取最大的时间
            MaxTime = LightBusiness.GetMax(lightList) ;
            for (; NowTime <= MaxTime; NowTime++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                worker.ReportProgress(NowTime);//返回进度
                Thread.Sleep(wait);
            }
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="mActionBeanList"></param>
        public override void SetData(List<Light> mActionBeanList)
        {
            //清空数据
            lightList.Clear();
            //获取数据
            lightList = LightBusiness.Sort(mActionBeanList);
        }

        /// <summary>
        /// 播放
        /// </summary>
        public override void Play() {
            NowTime = 0;
            //开始播放事件
            if (iplay != null)
                iplay.PlayEvent();
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public override void Stop() {
            NowTime = 0;
            //停止播放事件
            if (iplay != null)
                iplay.StopEvent();
            worker.CancelAsync();
            //清空
            foreach (var item in Children)
            {
                //停止播放=取消着色
                RoundedCornersPolygon rcp = item as RoundedCornersPolygon;
                if (rcp != null)
                    rcp.Fill = closeBrush;
                Ellipse e2 = item as Ellipse;
                if (e2 != null)
                    e2.Fill = closeBrush;
                Rectangle r = item as Rectangle;
                if (r != null)
                    r.Fill = closeBrush;
            }
        }

        /// <summary>
        /// 是否在暂停状态
        /// </summary>
        private Boolean bIsPause = false;

        /// <summary>
        /// 暂停/恢复
        /// </summary>
        public override void Pause() {
            if (bIsPause)
            {
                worker.RunWorkerAsync();
            }
            else
            {
                worker.CancelAsync();
            }
            //暂停播放事件
            if (iplay != null)
                iplay.PauseEvent(bIsPause);
            bIsPause = !bIsPause;
       }

        /// <summary>
        /// 设置等待时间 => 根据BPM计算得出
        /// </summary>
        public override void SetWait(TimeSpan wait) {
            this.wait = wait;
        }
    }
}
