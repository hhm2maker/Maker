using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.View.Device
{
    public interface IPlay
    {
        /// <summary>
        /// 开始播放事件 - 进程中
        /// </summary>
        void StartPlayEvent();
        /// <summary>
        /// 结束播放事件 - 进程中
        /// </summary>
        void EndPlayEvent();
        /// <summary>
        /// 开始播放事件
        /// </summary>
        void PlayEvent();
        /// <summary>
        /// 停止播放事件
        /// </summary>
        void StopEvent();
        /// <summary>
        /// 暂停播放事件
        /// </summary>
        void PauseEvent(bool isPause);
    }
}
