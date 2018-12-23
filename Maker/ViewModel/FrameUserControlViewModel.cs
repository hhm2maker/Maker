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


        private FrameUserControlModel welcome;
            /// <summary>
            /// 欢迎词属性
            /// </summary>
            public FrameUserControlModel Welcome
            {
                get { return welcome; }
                set { welcome = value; RaisePropertyChanged(() => Welcome); }
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
            if (welcome.NowTimePoint <= 1) return;
                 welcome.NowTimePoint--;
            }
            else if (str.Equals("Right"))
            {
                if (welcome.NowTimePoint > welcome.NowData.Count - 1) return;
                welcome.NowTimePoint++;
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
                GetNumberDialog dialog = new GetNumberDialog(StaticConstant.mw, "TheFrameOfTheNewNodeColon", false, welcome.LiTime, false);
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
                if (welcome.LiTime.Contains(0))
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
                if (welcome.LiTime.Count == 0)
                    return;

                OkOrCancelDialog oocd = new OkOrCancelDialog(StaticConstant.mw, "WhetherToDeleteTheTimeNode");
                if (oocd.ShowDialog() == true)
                {
                    welcome.NowData.Remove(welcome.LiTime[welcome.NowTimePoint - 1]);
                    welcome.LiTime.RemoveAt(welcome.NowTimePoint - 1);
                    if (welcome.LiTime.Count == 0)
                    {
                        welcome.NowTimePoint = 0;
                    }
                    else
                    {
                        if (welcome.NowTimePoint == 1)
                        {
                            welcome.NowTimePoint = 1;
                        }
                        else
                        {
                            welcome.NowTimePoint--;
                        }
                    }
                    welcome.AllTimePoint -= 1;
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
                if (welcome.LiTime.Contains(time))
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
            welcome.NowData.Add(time, shape);
            if (welcome.LiTime.Count == 0)
            {
                welcome.LiTime.Insert(0, time);
                welcome.NowTimePoint = 1;
            }
            else
            {
                //如果比最大的小，比较大小插入合适的位置
                if (welcome.LiTime[welcome.LiTime.Count - 1] > time)
                {
                    for (int i = 0; i < welcome.LiTime.Count; i++)
                    {
                        if (welcome.LiTime[i] > time)
                        {
                            welcome.LiTime.Insert(i, time);
                            welcome.NowTimePoint = i + 1;
                            break;
                        }
                    }
                }
                //比最大的大，插入最后
                else
                {
                    welcome.LiTime.Add(time);
                    welcome.NowTimePoint = welcome.LiTime.Count;
                }
            }
            welcome.AllTimePoint += 1;
        }
    }
}
