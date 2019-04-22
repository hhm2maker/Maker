using Maker.Business;
using Maker.Model;
using Maker.View.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Maker.View.Device
{
    public class TeachingLaunchpadPro : LaunchpadPro
    {

        public TeachingLaunchpadPro() : base()
        {
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerSupportsCancellation = true;

            MouseLeftButtonDown += TeachingLaunchpadPro_MouseLeftButtonDown;
        }

        private void TeachingLaunchpadPro_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Play();
        }

        private List<Light> teachingList = new List<Light>();

        /// <summary>
        /// 后台
        /// </summary>
        private BackgroundWorker worker = new BackgroundWorker();

        public void SetTeachingData(List<Light> teachingList)
        {
            for (int i = 0; i < teachingList.Count; i++)
            {
                teachingList[i].Position -= 28;
            }
            this.teachingList = LightBusiness.Sort(teachingList);
           
            FileBusiness.CreateInstance().ReplaceControl(this.teachingList, FileBusiness.CreateInstance().normalArr);
            //LightBusiness.Print(teachingList);
        }

        public void StartPlay() {
            if (teachingList == null || teachingList.Count == 0)
                return;
          
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
        private TimeSpan wait = TimeSpan.FromMilliseconds(1000 / Double.Parse("96"));

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
                //iplay.StartPlayEvent();
            }
            if (NowTime == MaxTime)
            {
                //结束播放事件 - 进程中
                //iplay.EndPlayEvent();
            }

            int i = 0;
            for (int l = i; l < teachingList.Count; l++)
            {
                if (teachingList[l].Time == NowTime)
                {
                    i = l + 1;
                    if (teachingList[l].Action == 128)
                    {
                        //停止播放=取消着色
                        if (GetButton(teachingList[l].Position ) is RoundedCornersPolygon rcp)
                            rcp.Fill = closeBrush;
                        if (GetButton(teachingList[l].Position ) is Ellipse e2)
                            e2.Fill = closeBrush;
                        if (GetButton(teachingList[l].Position ) is Rectangle r)
                            r.Fill = closeBrush;
                    }
                    if (teachingList[l].Action == 144)
                    {
                        Ellipse e3 = new Ellipse();
                     
                        //e3.Width = _circularWidth * 2.5;
                        //e3.Height = _circularWidth * 2.5;
                        e3.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                        e3.StrokeThickness = 2;
                        //SetLeft(e3, GetLeft(GetButton(teachingList[l].Position)));
                        //SetTop(e3, GetTop(GetButton(teachingList[l].Position)));
                        Children.Add(e3);

                        Storyboard storyboard = new Storyboard();

                        var s = new Storyboard();

                        DoubleAnimation doubleAnimation = new DoubleAnimation();
                        doubleAnimation.From = _circularWidth * 3.0;
                        doubleAnimation.To = 0;
                        doubleAnimation.Duration = TimeSpan.FromSeconds(1);
                        //Storyboard.SetTarget(doubleAnimation, e3);
                        Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(WidthProperty));

                        DoubleAnimation doubleAnimation2 = new DoubleAnimation();
                        doubleAnimation2.From = _circularWidth * 3.0;
                        doubleAnimation2.To = 0;
                        doubleAnimation2.Duration = TimeSpan.FromSeconds(1);
                        //Storyboard.SetTarget(doubleAnimation, e3);
                        Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath(HeightProperty));

                        Double d = GetLeft(GetButton(teachingList[l].Position));
                        DoubleAnimation doubleAnimation3 = new DoubleAnimation();
                        doubleAnimation3.From = d - _circularWidth / 1.5;
                        doubleAnimation3.To = d + _circularWidth / 1.5;
                        doubleAnimation3.Duration = TimeSpan.FromSeconds(1);
                        //Storyboard.SetTarget(doubleAnimation, e3);
                        Storyboard.SetTargetProperty(doubleAnimation3, new PropertyPath(LeftProperty));

                        Double d2 = GetTop(GetButton(teachingList[l].Position));
                        DoubleAnimation doubleAnimation4 = new DoubleAnimation();
                        doubleAnimation4.From = d2 - _circularWidth / 1.5;
                        doubleAnimation4.To = d2 + _circularWidth / 1.5;
                        doubleAnimation4.Duration = TimeSpan.FromSeconds(1);
                        //Storyboard.SetTarget(doubleAnimation, e3);
                        Storyboard.SetTargetProperty(doubleAnimation4, new PropertyPath(TopProperty));

                        s.Children.Add(doubleAnimation);
                        s.Children.Add(doubleAnimation2);
                        s.Children.Add(doubleAnimation3);
                        s.Children.Add(doubleAnimation4);
                        s.Begin(e3);


                        //开始播放=开始着色 
                        //if (GetButton(teachingList[l].Position ) is RoundedCornersPolygon rcp)
                        //    rcp.Fill = StaticConstant.brushList[teachingList[l].Color];
                        //if (GetButton(teachingList[l].Position ) is Ellipse e2)
                        //    e2.Fill = StaticConstant.brushList[teachingList[l].Color];
                        //if (GetButton(teachingList[l].Position) is Rectangle r)
                        //    r.Fill = StaticConstant.brushList[teachingList[l].Color];
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
            MaxTime = LightBusiness.GetMax(teachingList);
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
            teachingList.Clear();
            //获取数据
        }


        /// <summary>
        /// 播放
        /// </summary>
        public void Play()
        {
            NowTime = 0;
            //开始播放事件
            //iplay.PlayEvent();
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public  void Stop()
        {
            NowTime = 0;
            //停止播放事件
            //iplay.StopEvent();
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
        public  void Pause()
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
            //iplay.PauseEvent(bIsPause);
            bIsPause = !bIsPause;
        }

        /// <summary>
        /// 设置等待时间 => 根据BPM计算得出
        /// </summary>
        public void SetWait(TimeSpan wait)
        {
            this.wait = wait;
        }

    }
}
