using Maker.View.UIBusiness;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maker.View.UI.MyFile
{
    /// <summary>
    /// TabletPCFileManagerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TabletPCFileManagerWindow : Window
    {
        private BaseFileManager baseFileManager;
        private NewMainWindow mw;
        public TabletPCFileManagerWindow(NewMainWindow mw)
        {
            InitializeComponent();

            Owner = mw;
            this.mw = mw;
            AddContentUserControl("Light");
            AddContentUserControl("LightScript");
            AddContentUserControl("LimitlessLamp");
            AddContentUserControl("Page");

            baseFileManager = new BaseFileManager(mw);
        }

        public List<String> contentUserControls = new List<String>();

        public void AddContentUserControl(String uc)
        {
            TextBlock tb = new TextBlock();
            tb.Padding = new Thickness(10);
            tb.FontSize = 18;
            tb.Foreground = new SolidColorBrush(Color.FromRgb(169, 169, 169));

            tb.Text = (String)Application.Current.Resources[uc];

            tb.MouseLeftButtonDown += Tb_MouseLeftButtonDown;
            spContentTitle.Children.Add(tb);

            contentUserControls.Add(uc);
            //SetSpFilePosition(contentUserControls.Count - 1);
        }

        private void Tb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetSpFilePosition((((sender as TextBlock).Parent) as Panel).Children.IndexOf(sender as TextBlock));
        }

        public int filePosition = 0;
        public void SetSpFilePosition(int position)
        {
            (spContentTitle.Children[filePosition] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(169, 169, 169));
            (spContentTitle.Children[position] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            filePosition = position;

            lbFile.Items.Clear();

            foreach (var item in PositionToData(position))
            {
                lbFile.Items.Add(item);
            }

            tbExtension.Text = PositionToFileExtension(position);
           
            //spContent.Children.Clear();
            //spContent.Children.Add(contentUserControls[position]);
            foo();
            // .net 4.5
            async void foo()
            {
                await Task.Delay(50);

                double _p = 0.0;
                for (int i = 0; i < position; i++)
                {
                    _p += (spContentTitle.Children[i] as TextBlock).ActualWidth;
                }
                _p += ((spContentTitle.Children[position] as TextBlock).ActualWidth) / 2;

                double leftMargin = (spCenter.ActualWidth - spContentTitle.ActualWidth)/2;
                ThicknessAnimation animation2 = new ThicknessAnimation
                {
                    To = new Thickness(_p + leftMargin -25, 0, 0, 0),
                    Duration = TimeSpan.FromSeconds(0.5),
                };
                rFile.BeginAnimation(MarginProperty, animation2);
            }
        }

        private List<String> PositionToData(int position) {
            return baseFileManager.GetFile((BaseFileManager.FileType)position);
        }

        private String PositionToFileExtension(int position)
        {
            switch (position)
            {
                case 0:
                    return ".light";
                case 1:
                    return ".lightScript";
                case 2:
                    return ".limitlessLamp";
                case 3:
                    return ".lightPage";
            }
            return "";
        }

        private void lbStep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        TextBox tbNumber;
        TextBlock tbExtension;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            spRight.Children.Add(GeneralMainViewBusiness.CreateInstance().GetTopHintTextBlock("NewFileNameColon"));
            spRight.Children.Add(GeneralMainViewBusiness.CreateInstance().GetGrid(ref tbNumber,ref tbExtension));

            Button btn = GeneralMainViewBusiness.CreateInstance().GetButton("IntelligentFillIn", lbStep_SelectionChanged2);
            btn.HorizontalAlignment = HorizontalAlignment.Right;
            btn.Margin = new Thickness(0,20,0,0);
            spRight.Children.Add(btn);

            Button btn2 = GeneralMainViewBusiness.CreateInstance().GetButton("Ok", lbStep_SelectionChanged3);
            btn2.HorizontalAlignment = HorizontalAlignment.Right;
            btn2.Margin = new Thickness(0, 10, 0, 0);
            spRight.Children.Add(btn2);

            SetSpFilePosition(1);
        }

        private void lbStep_SelectionChanged2(object sender, RoutedEventArgs e)
        {
            List<String> files = PositionToData(filePosition);
            files.Reverse();

            String strTop = GetFileNameTop(files);

            if (strTop.Equals("Page1_1"))
            {
                tbNumber.Text = "Page1_1";
            }
            else {
                for (int i = 99; i > 0; i--)
                {
                    if (files.Contains(strTop+i + PositionToFileExtension(filePosition)))
                    {
                        tbNumber.Text = strTop + (i+1);
                        break;
                    }
                }
            }
        }

        private void lbStep_SelectionChanged3(object sender, RoutedEventArgs e)
        {
            BaseUserControl baseUserControl;
            if (filePosition == 0)
            {
                baseUserControl = mw.editUserControl.userControls[0];
            }
            else if (filePosition == 1)
            {
                baseUserControl = mw.editUserControl.userControls[3];
            }
            else if (filePosition == 2)
            {
                baseUserControl = mw.editUserControl.userControls[9];
            }
            else if (filePosition == 3)
            {
                baseUserControl = mw.editUserControl.userControls[8];
            }
            //else if (sender == miPage)
            //{
            //    baseUserControl = editUserControl.userControls[5];
            //}
            else
            {
                return;
            }
            baseUserControl.NewFileResult2(tbNumber.Text+tbExtension.Text);
            mw.editUserControl.IntoUserControl(tbNumber.Text + tbExtension.Text);
            Close();
        }

        private String GetFileNameTop(List<String> files)
        {
            if (files.Count == 0) {
                return "Page1_1";
            }

            int i = 10;

            for (; i > 0; i--){
                foreach (var file in files)
                {
                    if (file.Contains("Page" + i + "_"))
                    {
                        return "Page" + i + "_";
                    }
                }
            }
            if (i == 0)
            {
                return "Page1_1";
            }

            return "";
        }

    }
}
