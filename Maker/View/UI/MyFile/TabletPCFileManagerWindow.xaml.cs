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
        public TabletPCFileManagerWindow(NewMainWindow mw)
        {
            InitializeComponent();

            Owner = mw;
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

            switch (position)
            {
                case 0:
                    tbExtension.Text = ".light";
                    break;
                case 1:
                    tbExtension.Text = ".lightScript";
                    break;
                case 2:
                    tbExtension.Text = ".limitlessLamp";
                    break;
                case 3:
                    tbExtension.Text = ".lightPage";
                    break;
            }
           
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

        private void lbStep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        TextBox tbNumber;
        TextBlock tbExtension;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            spRight.Children.Add(GeneralMainViewBusiness.CreateInstance().GetTopHintTextBlock("NewFileNameColon"));
            spRight.Children.Add(GeneralMainViewBusiness.CreateInstance().GetGrid(ref tbNumber,ref tbExtension));
            spRight.Children.Add(GeneralMainViewBusiness.CreateInstance().GetButton("Ok",null));

            
            SetSpFilePosition(1);

            foreach (var item in baseFileManager.GetFile(BaseFileManager.FileType.LightScript))
            {
                lbFile.Items.Add(item);
            }
        }
    }
}
