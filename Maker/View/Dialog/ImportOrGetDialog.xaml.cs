using Maker.Business;
using Maker.Model;
using Maker.View.Control;
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
using System.Windows.Shapes;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ImportOrGetDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ImportOrGetDialog : Window
    {
        private MainWindow mw;
        private String filepath;
        private int type;
        /// <summary>
        /// 当前名称
        /// </summary>
        public String UsableStepName {
            get;
            set;
        }
        public List<String> importList = new List<String>();
        public List<String> getList = new List<String>();

        public ImportOrGetDialog(MainWindow mw,String filepath,int type)
        {
            InitializeComponent();
            this.mw = mw;
            this.filepath = filepath;
            this.type = type;
            Owner = mw;
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbHelp.Text = "这两种方法都有各自的优缺点。" + Environment.NewLine +
                "引用代码块清爽，但是必须把引用的文件置于语句文件同一文件夹下。" + Environment.NewLine +
                "导入数据会使代码块杂乱、会使运行速度变慢和语句文件变大，但是导入后可与原文件分离。";

            UsableStepName = mw.iuc.GetUsableStepName();
            //引用文本框内容
            StringBuilder _builder = new StringBuilder();
            if (type == 0) {
                _builder.Append("\tLightGroup " + UsableStepName + "LightGroup = Create.FromMidiFile(\"Resource^" + System.IO.Path.GetFileNameWithoutExtension(filepath) + "\");" );
            }
            else {
                _builder.Append("\tLightGroup " + UsableStepName + "LightGroup = Create.FromLightFile(\"Resource^" + System.IO.Path.GetFileNameWithoutExtension(filepath)+"\");" );
            }
            tbImport.Text = _builder.ToString();
            importList.Add(UsableStepName);

              //导入数据文本框内容
              StringBuilder builder = new StringBuilder();
            builder.Append("\tLightGroup " + UsableStepName + "LightGroup = new LightGroup();" + Environment.NewLine);
            getList.Add(UsableStepName);
            FileBusiness business = new FileBusiness();

            List<Light> mLl = null;
            if (type == 0)
            {
                mLl= business.ReadMidiFile(filepath); 
            }
            else
            {
                mLl = business.ReadLightFile(filepath);
            }
            int i = 1;
            foreach (Light l in mLl)
            {
                builder.Append("\tLight light" + i + " = new LightGroup(" + l.Time + "," + l.Action + "," + l.Position + "," + l.Color + ");" + Environment.NewLine);
                builder.Append("\t" + UsableStepName + "LightGroup.Add(" + "light" + i + ");" + Environment.NewLine);
                i++;
                getList.Add("light"+i);
            }
            tbGet.Text = builder.ToString();
        }
    }
}
