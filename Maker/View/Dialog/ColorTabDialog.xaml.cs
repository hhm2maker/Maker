using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ColorTabDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ColorTabDialog : Window
    {
        private MainWindow mw;
        public ColorTabDialog(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }
        //颜色
        private List<String> ColorList = new List<string>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColorList = ReadColorFile("");
            InitView();
        }
        private void InitView()
        {
            for (int k = 0; k < ColorList.Count; k++)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                TextBlock tb = new TextBlock
                {
                    Text = (k + 1).ToString(),
                    Width = 100
                };
                int one = Int32.Parse(ColorList[k].Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                int two = Int32.Parse(ColorList[k].Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                int three = Int32.Parse(ColorList[k].Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
                TextBlock tb2 = new TextBlock
                {
                    Text = one + "," + two + "," + three,
                    Width = 150
                };
                TextBlock tb3 = new TextBlock
                {
                    Text = ColorList[k],
                    Width = 150
                };
                sp.Children.Add(tb);
                sp.Children.Add(tb2);
                sp.Children.Add(tb3);
                lbMain.Items.Add(sp);
            }
        }
        private List<String> ReadColorFile(String path)
        {
            List<String> mList = new List<string>();
            String ColorPath;
            if (path.Equals(""))
            {
                ColorPath = mw.strColortabPath;
            }
            else
            {
                ColorPath = path;
            }
            FileStream f;
            f = new FileStream(ColorPath, FileMode.OpenOrCreate);
            int i = 0;
            String LingShi = "";
            while ((i = f.ReadByte()) != -1)
            {

                if (i == ';')
                {
                    LingShi = LingShi.Replace("\n", "");
                    LingShi = LingShi.Replace("\r", "");
                    mList.Add(LingShi);
                    LingShi = "";
                    continue;
                }
                LingShi += (char)i;
            }
            f.Close();
            if (mList.Count != 127)
            {
                if (System.Windows.Forms.MessageBox.Show("错误的颜色文件", "警告") == System.Windows.Forms.DialogResult.OK)
                {
                    Close();
                }
            }
            return mList;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //判断当前路径下内容是否和当前内容相同
            List<String> ls = ReadColorFile(mw.strColortabPath);
            bool isEquals = true;
            for(int i = 0;i<ls.Count;i++) {
                if (!ls[i].Equals(ColorList[i])) {
                    isEquals = false;
                    break;
                }
            }
            if (!isEquals)
           {
                if (System.Windows.Forms.MessageBox.Show("是否保存到当前颜色表", "提示", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    //获得文件路径
                    String localFilePath = mw.strColortabPath;
                    String line = "";
                    for (int i = 0; i < ColorList.Count; i++)
                    {
                        if (ColorList[i].Equals(""))
                        {
                            System.Windows.Forms.MessageBox.Show("错误码：1003。\n序号" + i + 1 + "未填写", "警告");
                            return;
                        }
                        line += ColorList[i] + ";" + System.Environment.NewLine;
                    }
                    FileStream f = new FileStream(localFilePath, FileMode.OpenOrCreate);
                    for (int i = 0; i < line.Length; i++)
                    {
                        f.WriteByte((byte)line[i]);
                    }
                    f.Close();
                }
            }
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void NewColorTab(object sender, RoutedEventArgs e)
        {
            foreach (Object o in lbMain.Items) {
                StackPanel sp = (StackPanel)o;
                TextBlock tb = (TextBlock)sp.Children[1];//十进制
                tb.Text = "";
                TextBlock tb2 = (TextBlock)sp.Children[2];//十六进制
                tb2.Text = "";
            }
            for (int i = 0; i < ColorList.Count; i++) {
                ColorList[i] = "";
            }
        }
        private void ChangeColor(object sender, MouseButtonEventArgs e)
        {
            if (lbMain.SelectedIndex == -1) {
                return;
            }
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            cd.AllowFullOpen = true;
            cd.FullOpen = true;
            cd.Color = System.Drawing.ColorTranslator.FromHtml(ColorList[lbMain.SelectedIndex]);
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StackPanel panel = (StackPanel)lbMain.SelectedItem;
                TextBlock tb = (TextBlock)panel.Children[1];
                tb.Text = cd.Color.R + "," + cd.Color.G + "," + cd.Color.B;//十进制
                TextBlock tb2 = (TextBlock)panel.Children[2];
                tb2.Text = "#" + (cd.Color.ToArgb().ToString("X8")).Substring(2).ToLower();//十六进制
                ColorList[lbMain.SelectedIndex] = "#" + (cd.Color.ToArgb().ToString("X8")).Substring(2).ToLower();//十六进制
            }
        }
        private void ImportColorTab(object sender, RoutedEventArgs e)
        {
            String ColorPath;
            if (string.IsNullOrWhiteSpace(mw.strColortabPath) || !File.Exists(mw.strColortabPath))
            {
                ColorPath = System.Windows.Forms.Application.StartupPath + "\\Color\\color.color";
            }
            else
            {
                ColorPath = mw.strColortabPath;
            }
            String fName;
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(ColorPath);  //注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "颜色文件|*.color";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fName = openFileDialog.FileName;
                ColorList =  ReadColorFile(fName);
                InitView();
            }
        }

        private void ExportColorTab(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            //设置文件类型
            saveFileDialog1.Filter = "颜色文件 | *.color";
            //设置默认文件类型显示顺序
            saveFileDialog1.FilterIndex = 2;
            //保存对话框是否记忆上次打开的目录
            saveFileDialog1.RestoreDirectory = true;
            //点了保存按钮进入
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //获得文件路径
                String localFilePath = saveFileDialog1.FileName;
                String line = "";
                for (int i = 0; i < ColorList.Count; i++)
                {
                    if (ColorList[i].Equals(""))
                    {
                        System.Windows.Forms.MessageBox.Show("错误码：1003。\n序号" + i + 1 + "未填写", "警告");
                        return;
                    }
                    line += ColorList[i] + ";" + System.Environment.NewLine;
                }
                FileStream f = new FileStream(localFilePath, FileMode.OpenOrCreate);
                for (int i = 0; i < line.Length; i++)
                {
                    f.WriteByte((byte)line[i]);
                }
                f.Close();
                //如果和原路径不相等询问是否替换颜色表
                if (!mw.strColortabPath.Equals(saveFileDialog1.FileName)) {
                    if (System.Windows.Forms.MessageBox.Show("是否替换颜色表路径", "提示", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load("Config/colortab.xml");
                        XmlNode colortabRoot = doc.DocumentElement;
                        XmlNode colortabPath = colortabRoot.SelectSingleNode("Path");
                        colortabPath.InnerText = saveFileDialog1.FileName;
                        doc.Save("Config/colortab.xml");
                        mw.strColortabPath = saveFileDialog1.FileName; 
                    }
                }
            }
        }
    }
}
