﻿using Maker.Business;
using Maker.Model;
using Maker.View.UI.Base;
using Maker.View.UI.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View.UI.Home
{
    /// <summary>
    /// HomeUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class LocalUserControl : BaseChildUserControl
    {
        private NewMainWindow mw;
        public LocalUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            Title = "Local";

        }

        private void InitProject()
        {
            List<String> strs = FileBusiness.CreateInstance().GetDirectorysName(AppDomain.CurrentDomain.BaseDirectory + @"\Project");
            //GeneralViewBusiness.SetStringsToListBox(lbProject, strs, projectConfigModel.Path);
            int width = (int)Math.Floor(((spCenter.Width - 75) / 4.0));
            int height = (int)(width / 4.0 * 3);
            for (int i = 0; i < strs.Count; i++)
            {
                Border border = new Border();
                border.Width = width;
                border.Height = height;
                border.Background = new SolidColorBrush(Colors.Transparent);
                border.MouseLeftButtonDown += Border_MouseLeftButtonDown;
                border.CornerRadius = new CornerRadius(3);
                border.BorderThickness = new Thickness(2);
                border.BorderBrush = new SolidColorBrush(Color.FromRgb(85,85,85));
                border.Margin = new Thickness(15, 15, 0, 0);
                if (i > strs.Count - 5)
                {
                    border.Margin = new Thickness(15, 15, 0, 15);
                }
                Grid grid = new Grid();
                border.Child = grid;
                TextBlock tb = new TextBlock();
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.Text = strs[i];
                tb.FontSize = 17;
                tb.Margin = new Thickness(15);
                tb.Foreground = new SolidColorBrush(Color.FromRgb(184, 191, 198));
                grid.Children.Add(tb);
                wpProject.Children.Add(border);
            }
            SetSpFilePosition(strs.IndexOf(mw.projectConfigModel.Path));
        }

        public int filePosition = -1;
        public void SetSpFilePosition(int position)
        {
            if (filePosition == position)
                return;

            if (filePosition != -1)
            {
                (wpProject.Children[filePosition] as Border).Background = new SolidColorBrush(Colors.Transparent);
                (wpProject.Children[filePosition] as Border).BorderBrush = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                (((wpProject.Children[filePosition] as Border).Child as Grid).Children[0] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(184, 191, 198));
            }

            (wpProject.Children[position] as Border).Background = new SolidColorBrush(Color.FromRgb(184, 191, 198));
            (wpProject.Children[position] as Border).BorderBrush = new SolidColorBrush(Colors.Transparent);
            (((wpProject.Children[position] as Border).Child as Grid).Children[0] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(85, 85, 85));

            filePosition = position;
            RefreshFile();
        }

        private void RefreshFile()
        {
            //TODO:
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetSpFilePosition(((sender as Border).Parent as Panel).Children.IndexOf(sender as Border));
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //iPicture.Source = new BitmapImage(new Uri(@"E:\Sharer\Maker\Maker\bin\Debug\Project\新建文件夹\img.png", UriKind.RelativeOrAbsolute));
        }

        bool isFirst = true;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (isFirst)
            {
                spCenter.Width = mw.ActualWidth / 4 + 630;
                InitProject();

                isFirst = false;
            }
        }

    }
}
