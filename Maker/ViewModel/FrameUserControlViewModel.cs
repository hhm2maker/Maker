using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maker.Model;
using Maker.View.LightUserControl;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight.Command;
using Maker.View.Dialog;
using System.Windows.Media.Imaging;
using Maker.View.Device;

namespace Maker.ViewModel
{
    public class FrameUserControlViewModel : ViewModelBase
    {
            /// <summary>
            /// 构造函数
            /// </summary>
            public FrameUserControlViewModel()
            {
                Welcome = new FrameUserControlModel() { };
            }


        private FrameUserControlModel model;
            /// <summary>
            /// 欢迎词属性
            /// </summary>
            public FrameUserControlModel Welcome
            {
                get { return model; }
                set { model = value; RaisePropertyChanged(() => Welcome); }
            }

        /// <summary>
        /// 命令(向左或向右)改变当前时间节点
        /// </summary>
        private RelayCommand<String> changeNowTimePointCmd;
        public RelayCommand<String> ChangeNowTimePointCmd
        {
            get
            {
                if (changeNowTimePointCmd == null) return new RelayCommand<String>((p) => ChangeNowTimePoint(p));
                return changeNowTimePointCmd;
            }
            set { changeNowTimePointCmd = value; }
        }

        /// <summary>
        /// (向左或向右)改变当前时间节点
        /// </summary>
        /// <param name="str">向左或向右</param>
        private void ChangeNowTimePoint(String str)
        {
            if (str.Equals("Left")) { 
            if (model.NowTimePoint <= 1) return;
                 model.NowTimePoint--;
            }
            else if (str.Equals("Right"))
            {
                if (model.NowTimePoint > model.NowData.Count - 1) return;
                model.NowTimePoint++;
            }
        }

        /// <summary>
        /// 命令添加或删除时间节点
        /// </summary>
        private RelayCommand<String> addOrDeleteTimePointCmd;
        public RelayCommand<String> AddOrDeleteTimePointCmd
        {
            get
            {
                if (addOrDeleteTimePointCmd == null) return new RelayCommand<String>((p) => AddOrDeleteTimePoint(p));
                return addOrDeleteTimePointCmd;
            }
            set { addOrDeleteTimePointCmd = value; }
        }

        /// <summary>
        /// 添加或删除时间节点
        /// </summary>
        /// <param name="str"></param>
        private void AddOrDeleteTimePoint(String str)
        {
            if (str.Equals("Add"))
            {
                GetNumberDialog dialog = new GetNumberDialog(StaticConstant.mw, "TheFrameOfTheNewNodeColon", false, model.LiTime, false);
                if (dialog.ShowDialog() == true)
                {
                    int[] x = new int[96];
                    for (int j = 0; j < 96; j++)
                    {
                        x[j] = 0;
                    }
                    InsertTimePoint(dialog.OneNumber, x);
                }
            }
            else if (str.Equals("AddStart"))
            {
                //如果已经有该时间点，报错
                if (model.LiTime.Contains(0))
                {
                    new MessageDialog(StaticConstant.mw, "TheFrameHasATimeNode").ShowDialog();
                }
                else
                {
                    int[] x = new int[96];
                    for (int i = 0; i < 96; i++)
                    {
                        x[i] = 0;
                    }
                    InsertTimePoint(0, x);
                }
            }
            else if (str.Equals("Delete"))
            {
                if (model.LiTime.Count == 0)
                    return;

                OkOrCancelDialog oocd = new OkOrCancelDialog(StaticConstant.mw, "WhetherToDeleteTheTimeNode");
                if (oocd.ShowDialog() == true)
                {
                    model.NowData.Remove(model.LiTime[model.NowTimePoint - 1]);
                    model.LiTime.RemoveAt(model.NowTimePoint - 1);
                    if (model.LiTime.Count == 0)
                    {
                        model.NowTimePoint = 0;
                    }
                    else
                    {
                        if (model.NowTimePoint == 1)
                        {
                            model.NowTimePoint = 1;
                        }
                        else
                        {
                            model.NowTimePoint--;
                        }
                    }
                    model.AllTimePoint -= 1;
                }
            }
            else {
                String strTime = str.Trim();
                if (strTime.Trim().Equals(""))
                {
                    return;
                }
                int time = 0;
                try
                {
                    if (strTime.Contains("+"))
                    {
                        //当前时间 +
                        time = int.Parse(strTime) + int.Parse(str.Substring(1));
                    }
                    else if (strTime.Contains("-"))
                    {
                        //当前时间 -
                        time = int.Parse(strTime) - int.Parse(str.Substring(1));
                    }
                    else
                    {
                        //当前时间
                        time = int.Parse(strTime);
                    }

                    if (time < 0)
                    {
                        new MessageDialog(StaticConstant.mw, "TheInputFormatIsIncorrect").ShowDialog();
                        return;
                    }

                }
                catch
                {
                    new MessageDialog(StaticConstant.mw, "TheInputFormatIsIncorrect").ShowDialog();
                    return;
                }
                //如果已经有该时间点，报错
                if (model.LiTime.Contains(time))
                {
                    new MessageDialog(StaticConstant.mw, "TheFrameHasATimeNode").ShowDialog();
                }
                else
                {
                    int[] x = new int[96];
                    for (int i = 0; i < 96; i++)
                    {
                        x[i] = 0;
                    }
                    InsertTimePoint(time, x);
                }
            }
        }

        /// <summary>
        /// 插入时间点
        /// </summary>
        /// <param name="time">插入时间</param>
        /// <param name="shape">插入形状</param>
        private void InsertTimePoint(int time, int[] shape)
        {
            model.NowData.Add(time, shape);
            if (model.LiTime.Count == 0)
            {
                model.LiTime.Insert(0, time);
                model.NowTimePoint = 1;
            }
            else
            {
                //如果比最大的小，比较大小插入合适的位置
                if (model.LiTime[model.LiTime.Count - 1] > time)
                {
                    for (int i = 0; i < model.LiTime.Count; i++)
                    {
                        if (model.LiTime[i] > time)
                        {
                            model.LiTime.Insert(i, time);
                            model.NowTimePoint = i + 1;
                            break;
                        }
                    }
                }
                //比最大的大，插入最后
                else
                {
                    model.LiTime.Add(time);
                    model.NowTimePoint = model.LiTime.Count;
                }
            }
            model.AllTimePoint += 1;
        }
    }
}
