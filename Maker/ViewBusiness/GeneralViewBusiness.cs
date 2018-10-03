using Maker.Model;
using Maker.View.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Maker.ViewBusiness
{
    public static class GeneralViewBusiness
    {
        /// <summary>
        /// 设置字符串数组到ListBox
        /// </summary>
        /// <param name="listbox"></param>
        /// <param name="strings"></param>
        public static void SetStringsToListBox(ListBox listbox,List<String> strings)
        {
            listbox.Items.Clear();
            foreach (String str in strings)
            {
                listbox.Items.Add(str);
            }
        }
        /// <summary>
        /// 设置字符串数组到ListBox
        /// </summary>
        /// <param name="listbox"></param>
        /// <param name="strings"></param>
        public static void SetStringsToListBox(ListBox listbox, List<String> strings,String selectString)
        {
            listbox.Items.Clear();
            foreach (String str in strings)
            {
                listbox.Items.Add(str);
                if (str.Equals(selectString)) {
                    listbox.SelectedIndex = listbox.Items.Count - 1;
                }
            }
        }
        /// <summary>
        /// 设置字符串数组和点击事件到MenuItem
        /// </summary>
        /// <param name="listbox"></param>
        /// <param name="strings"></param>
        public static void SetStringsAndClickEventToMenuItem(MenuItem menuItem, List<String> strings, RoutedEventHandler clickEvent,bool isClearOld)
        {
            if (isClearOld)
            {
                menuItem.Items.Clear();
            }
            foreach (String str in strings)
            {
                MenuItem mItem = new MenuItem
                {
                    Header = str
                };
                mItem.Click += clickEvent;
                menuItem.Items.Add(mItem);
            }
        }
        /// <summary>
        /// 设置字符串数组和点击事件到MenuItem
        /// </summary>
        /// <param name="listbox"></param>
        /// <param name="strings"></param>
        public static void SetStringsAndClickEventToMenuItem(MenuItem menuItem, List<String> strings, RoutedEventHandler clickEvent, bool isClearOld,int fontSize)
        {
            if (isClearOld)
            {
                menuItem.Items.Clear();
            }
            foreach (String str in strings)
            {
                MenuItem mItem = new MenuItem
                {
                    Header = str,
                    FontSize = fontSize
                };
                mItem.Click += clickEvent;
                menuItem.Items.Add(mItem);
            }
        }
        /// <summary>
        /// 设置窗口全屏 
        /// </summary>
        /// <param name="window"></param>
        public static void SetFullScreen(Window window)
        {
            Rect rc = SystemParameters.WorkArea;//获取工作区大小  
            window.Left = 0;//设置位置  
            window.Top = 0;
            window.Width = rc.Width;
            window.Height = rc.Height;
        }
        /// <summary>
        /// 设置窗口百分比
        /// </summary>
        /// <param name="window"></param>
        /// <param name="percentage"></param>
        public static void SetPercentageOfScreen(Window window,double percentage)
        {
            Rect rc = SystemParameters.WorkArea;//获取工作区大小  
            window.Left = rc.Width * (1 - percentage) /2;//设置位置  
            window.Top = rc.Height * (1 - percentage) / 2;
            window.Width = rc.Width * percentage;
            window.Height = rc.Height * percentage;
        }
        /// <summary>
        /// 设置Launchpad样式
        /// </summary>
        /// <param name="mLaunchpad"></param>
        /// <param name="deviceModel"></param>
        public static void SetLaunchpadStyle(LaunchpadPro mLaunchpad,DeviceModel deviceModel) {
            mLaunchpad.SetLaunchpadBackground(deviceModel.DeviceBackGround);
            mLaunchpad.SetSize(deviceModel.DeviceSize);
            if (deviceModel.IsMembrane)
            {
                mLaunchpad.AddMembrane();
            }
        }
    }
}
