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

namespace Maker.View.UI.Edit
{
    /// <summary>
    /// EditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditWindow : Window
    {
        private NewMainWindow mw;
        public EditWindow(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;

            Owner = mw;

            tbFileName.Text = mw.needControlFileName.Substring(0, mw.needControlFileName.IndexOf('.'));

            using(StreamReader sr = new StreamReader(mw.LastProjectPath+@"LightScript\"+ mw.needControlFileName, Encoding.UTF8)){
                StringBuilder sb = new StringBuilder();
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line).Append(Environment.NewLine);
                }
                tbContent.Text = sb.ToString();
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            String filePath = mw.LastProjectPath + @"LightScript\" + tbFileName.Text + ".lightScript";
            if (File.Exists(filePath))
            {
                if (MessageBox.Show("是否要覆盖此文件", "提示",MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    WriteContent(filePath);
                }
            }
            else
            {
                WriteContent(filePath);
            }
            mw.InitFile();
        }

        private void WriteContent(String filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create)) {
                using (StreamWriter sw = new StreamWriter(fs)) {
                    //开始写入
                    sw.Write(tbContent.Text);
                    //清空缓冲区
                    //sw.Flush();
                    //关闭流
                    //sw.Close();
                    //fs.Close();
                }
            }

            if ((tbFileName.Text + ".lightScript").Equals(mw.editUserControl.FileName))
            {
                mw.editUserControl.IntoUserControl(tbFileName.Text+".lightScript",false);
                //(mw.editUserControl.gMain.Children[0] as BaseUserControl).LoadFile(filePath);
            }
        }
    }
}
