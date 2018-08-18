using Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.View.Device
{
    public class PlayerLaunchpadPro:LaunchpadPro
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="mActionBeanList"></param>
        public override void SetData(List<Light> mActionBeanList)
        { }

        /// <summary>
        /// 设置等待时间 - Double 
        /// </summary>
        public virtual void SetWait(Double wait)
        { }

        /// <summary>
        /// 设置等待时间 - TimeSpan
        /// </summary>
        public virtual void SetWait(TimeSpan wait)
        { }

        /// <summary>
        /// 播放
        /// </summary>
        public virtual void Play()
        { }

        /// <summary>
        /// 暂停
        /// </summary>
        public virtual void Pause()
        { }

        /// <summary>
        /// 停止
        /// </summary>
        public virtual void Stop()
        { }

        /// <summary>
        /// 设置大小
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public void SetSize(Double Width, Double Height)
        {
            this.Width = Width;
            this.Height = Height;
        }
    }
}
