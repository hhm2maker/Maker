using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Maker.Business;
using Maker.Model;
using System.Windows.Forms;

namespace Maker.View.Control
{
    /// <summary>
    /// MainControlUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class MainControlUserControl : System.Windows.Controls.UserControl
    {
        public MainControlUserControl(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            fuc = new FrameUserControl(this);
            liveuc = new LiveUserControl(this);
            liveuc.CanDraw();
        }
        public NumberUserControl nuc = new NumberUserControl();
        public FrameUserControl fuc;//构造函数处初始化
        public LiveUserControl liveuc;

        public MainWindow mw;
        public List<Light> mLightList;

        private Mode mode = Mode.Number;
        /// <summary>
        ///  当前选择的内容
        /// </summary>
        public enum Mode
        {
            Number = 0,//输入
            Frame = 1,//逐帧
            Live = 2,//Live
        }

        private void ToUserControl(object sender, RoutedEventArgs e)
        {
            RefreshData();

            if (sender == tbNumberUserControl)
            {
                ToNumberUserControl();
                mode = Mode.Number;
            }
            else if (sender == tbFrameUserControl)
            {
                ToFrameUserControl();
                mode = Mode.Frame;
            }
            else if (sender == tbLiveUserControl)
            {
                ToLiveUserControl();
                mode = Mode.Live;
            }
        }

        public void LoadFileData(string filePath) {
            mLightList = FileBusiness.CreateInstance().ReadLightFile(filePath);
            mode = Mode.Number;
            ToNumberUserControl();
        }
        private void ToNumberUserControl()
        {
            mainDockPanel.Children.Clear();
            nuc.SetData(mLightList);
            mainDockPanel.Children.Add(nuc);
        }

        private void ToFrameUserControl()
        {
            mainDockPanel.Children.Clear();
            fuc.SetData(mLightList);
            mainDockPanel.Children.Add(fuc);
        }

        private void ToLiveUserControl()
        {
            mainDockPanel.Children.Clear();
            liveuc.SetData(mLightList);
            mainDockPanel.Children.Add(liveuc);
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshData()
        {
            if (mode == Mode.Number)
            {
                mLightList = nuc.GetData();
            }
            if (mode == Mode.Frame)
            {
                mLightList = fuc.GetData();
            }
            if (mode == Mode.Live)
            {
                mLightList = liveuc.GetData();
            }
        }
        /// <summary>
        /// 给编辑区设置数据
        /// </summary>
        public void SetDataToChildren()
        {
            if (mode == Mode.Number)
            {
                nuc.SetData(mLightList);
            }
            if (mode == Mode.Frame)
            {
                fuc.SetData(mLightList);
            }
            if (mode == Mode.Live)
            {
                liveuc.SetData(mLightList);
            }
        }

        private void mainDockPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            double minWidth = mainDockPanel.ActualWidth - fuc.svMain.ActualWidth - 30;
            double minHeight = mainDockPanel.ActualHeight;

            if (minWidth <= 0 || minHeight <= 0)
            {
                return;
            }
            else
            {
                double min = minWidth < minHeight ? minWidth : minHeight;
                fuc.mLaunchpad.Size = min;
            }
            //Console.WriteLine(mainDockPanel.ActualHeight+"---"+ mainDockPanel.ActualWidth);
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (mode == Mode.Frame)
            {
                if (e.Key == Key.Left)
                {
                    fuc.ToLastTime();
                }
                else if (e.Key == Key.Right)
                {
                    fuc.ToNextTime();
                }
            }
        }


        private void tbSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveLight();
        }

        private void SaveLight()
        {
            RefreshData();

            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.Filter = "灯光文件(*.light)|*.light|All files(*.*)|*.*";
            //saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            System.Windows.Forms.DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {
                FileBusiness.CreateInstance().WriteLightFile(saveFileDialog1.FileName, mLightList);
            }
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "灯光文件(*.light)|*.light|All files(*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mLightList = FileBusiness.CreateInstance().ReadLightFile(openFileDialog1.FileName);
                SetDataToChildren();
            }
        }

        private void tbSave_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
            FileBusiness.CreateInstance().WriteLightFile(mw.lightScriptFilePath, mLightList);
        }
    }
}
