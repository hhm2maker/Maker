using Maker.View.LightScriptUserControl;
using Maker.ViewBusiness;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Maker.View.UI.Dialog.WindowDialog
{
    /// <summary>
    /// ShowMyContentWindowDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ShowMyContentWindowDialog : Window
    {
        private ScriptUserControl suc;

        public String resultFileName;

        public ShowMyContentWindowDialog(ScriptUserControl suc)
        {
            InitializeComponent();

            Owner = suc.mw;

            this.suc = suc;
            InitMyContent(0);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
              InitMyContent(((sender as Border).Parent as Panel).Children.IndexOf((sender as Border)));
        }

        public void InitMyContent(int position)
        {
            //获取最新的我的内容
            if (position == oldPosition)
                return;
            GeneralViewBusiness.SetStringsAndClickEventToListBox(miChildMycontent, GetMyContent(position, suc.filePath), null, true, 16);

            if (oldPosition != -1) {
                Border bOld = spTitle.Children[oldPosition] as Border;
                bOld.Background = new SolidColorBrush(Colors.Transparent);
                bOld.BorderThickness = new Thickness(0, 0, 0, 2);
            }

            Border bNew = spTitle.Children[position] as Border;
            bNew.Background = new SolidColorBrush(Color.FromRgb(43,43,43));
            bNew.BorderThickness = new Thickness(2, 2, 2, 0);

            oldPosition = position;
        }

        int oldPosition = -1;
        /// <summary>
        /// 获取我的文件列表
        /// </summary>
        /// <returns></returns>
        public List<String> GetMyContent(int position ,String exceptStr)
        {
            List<String> contents = new List<String>();
            if (position == 0)
            {
                DirectoryInfo folder = new DirectoryInfo(suc.mw.LastProjectPath + @"\Light");
                foreach (FileInfo file in folder.GetFiles("*.light"))
                {
                    if (!file.Name.Equals(exceptStr))
                        contents.Add(System.IO.Path.GetFileName(file.FullName));
                }
                foreach (FileInfo file in folder.GetFiles("*.mid"))
                {
                    if (!file.Name.Equals(exceptStr))
                        contents.Add(System.IO.Path.GetFileName(file.FullName));
                }
            }
            if (position == 1)
            {
                DirectoryInfo folder = new DirectoryInfo(suc.mw.LastProjectPath + @"\LightScript");
                foreach (FileInfo file in folder.GetFiles("*.lightScript"))
                {
                    if (!file.Name.Equals(exceptStr))
                        contents.Add(System.IO.Path.GetFileName(file.FullName));
                }
            }
            if (position == 2)
            {
                DirectoryInfo folder = new DirectoryInfo(suc.mw.LastProjectPath + @"\LimitlessLamp");
                foreach (FileInfo file in folder.GetFiles("*.LimitlessLamp"))
                {
                    if (!file.Name.Equals(exceptStr))
                        contents.Add(System.IO.Path.GetFileName(file.FullName));
                }
            }
            return contents;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (miChildMycontent.SelectedIndex == -1)
                return;

            resultFileName = (miChildMycontent.SelectedItem as ListBoxItem).Content as String;
            DialogResult = true;
            Close();
        }
    }
}
