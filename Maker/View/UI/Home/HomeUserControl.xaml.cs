using Maker.Business;
using Maker.Model;
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
    public partial class HomeUserControl : UserControl
    {
        private NewMainWindow mw;
        public HomeUserControl(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;

            InitProject();
        }

        private void InitProject()
        {
            List<String> strs = FileBusiness.CreateInstance().GetDirectorysName(AppDomain.CurrentDomain.BaseDirectory + @"\Project");
            for (int i = 0; i < strs.Count; i++)
            {
                strs[i] = strs[i];
            }
            //GeneralViewBusiness.SetStringsToListBox(lbProject, strs, projectConfigModel.Path);

            for (int i = 0; i < strs.Count; i++)
            {
                Border border = new Border();
                border.Background = new SolidColorBrush(Colors.Transparent);
                border.MouseLeftButtonDown += Border_MouseLeftButtonDown;
                border.CornerRadius = new CornerRadius(3);
                border.BorderThickness = new Thickness(2);
                border.BorderBrush = new SolidColorBrush(Color.FromRgb(85,85,85));
                if (i == 0)
                {
                    border.Margin = new Thickness(15, 15, 15, 10);
                }
                else
                {
                    border.Margin = new Thickness(15, 0, 15, 10);
                }
                if (i == strs.Count - 1) {
                    border.Margin = new Thickness(15, 0, 15, 15);
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
                spProject.Children.Add(border);
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
                (spProject.Children[filePosition] as Border).Background = new SolidColorBrush(Colors.Transparent);
                (spProject.Children[filePosition] as Border).BorderBrush = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                (((spProject.Children[filePosition] as Border).Child as Grid).Children[0] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(184, 191, 198));
            }

            (spProject.Children[position] as Border).Background = new SolidColorBrush(Color.FromRgb(184, 191, 198));
            (spProject.Children[position] as Border).BorderBrush = new SolidColorBrush(Colors.Transparent);
            (((spProject.Children[position] as Border).Child as Grid).Children[0] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(85, 85, 85));

            filePosition = position;
            RefreshFile();
        }

        private void RefreshFile()
        {
            //TODO:
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetSpFilePosition(((sender as Border).Parent as StackPanel).Children.IndexOf(sender as Border));
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
                spCenter.Children.Add(new FastLaunchpadProUserControl(this));
                isFirst = false;
            }
        }

    }
}
