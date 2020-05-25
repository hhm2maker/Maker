using Maker.Business;
using Maker.Business.Model;
using Maker.Business.Model.OperationModel;
using Maker.Model;
using Maker.View.UI.UserControlDialog;
using Operation;
using PlugLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using static Maker.View.UI.Play.LogCatUserControl;

namespace Maker.View.UI
{
    /// <summary>
    /// PlayUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class PlayUserControl : BaseUserControl
    {
        private static NativeMethods.MidiInProc midiInProc;

        private static Model nowModel = Model.Launchpad;
        public static Model NowModel
        {
            get
            {
                return nowModel;
            }
            set {
                nowModel = value;
            }
        }

        public enum Model : int
        {
            Normal,
            Launchpad,
        }

        public PlayUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            _fileExtension = ".play";
            _fileType = "Play";
            mainView = gMain;

            //HideControl();    
        }
        private int tutorialPosition = 0;

        private void LoadHint()
        {
            if (mw.hintModelDictionary.ContainsKey(1))
            {
                if (mw.hintModelDictionary[1].IsHint == false)
                {
                    return;
                }
            }
            HintDialog hintDialog = new HintDialog("安装固件", "您是否要安装固件？",
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    InstallUsbDriver();
                    RemoveDialog();
                },
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    RemoveDialog();
                },
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    NotHint(1);
                });
            mw.ShowMakerDialog(hintDialog);
        }

        private void InstallUsbDriver()
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\Attachments\novation-usb-driver-2.7.exe");
        }

        private void RemoveDialog()
        {
            mw.gMost.Children.RemoveAt(mw.gMost.Children.Count - 1);
            mw.gMost.Children.RemoveAt(mw.gMost.Children.Count - 1);
        }

        public void NotHint(int id)
        {
            if (mw.hintModelDictionary.ContainsKey(id))
                mw.hintModelDictionary[id].IsHint = false;
        }

        private static Dictionary<String, Dictionary<int, List<PageButtonModel>>> pages = new Dictionary<string, Dictionary<int, List<PageButtonModel>>>();
        /// <summary>
        /// 前面的int是位置，后面的是次数
        /// </summary>
        private static Dictionary<String, Dictionary<int, int>> positions = new Dictionary<string, Dictionary<int, int>>();
        private static Dictionary<String, List<Light>> lights = new Dictionary<String, List<Light>>();
        private static String nowPageName = String.Empty;
        private static Dictionary<Thread, List<object>> threads = new Dictionary<Thread, List<object>>();
        private static Dictionary<Thread, List<object>> threadsStop = new Dictionary<Thread, List<object>>();


        protected virtual void InitTeachingData(List<Light> tutorialLights)
        {

        }

        public BaseOperationModel XmlToModel(XElement xEdit)
        {
            BaseOperationModel baseOperationModel = null;
            if (xEdit.Name.ToString().Equals("LightFile"))
            {
                baseOperationModel = new LightFilePlayModel();
            }
            else if (xEdit.Name.ToString().Equals("GotoPage"))
            {
                baseOperationModel = new GotoPagePlayModel();
            }
            else if (xEdit.Name.ToString().Equals("AudioFile"))
            {
                baseOperationModel = new AudioFilePlayModel();
            }

            baseOperationModel.SetXElement(xEdit);
            return baseOperationModel;
        }

        static bool isLive = true;
        /// <summary>
        /// 读取xml文件
        /// </summary>
        public void LoadExeXml()
        {
            pages.Clear();
            positions.Clear();
            lights.Clear();

            DirectoryInfo d = new DirectoryInfo(mw.LastProjectPath);
            XDocument _doc = XDocument.Load(filePath);
            XElement _root = _doc.Element("Root");

            XElement _tutorial = _root.Element("Tutorial");
            String strTutorial = Business.FileBusiness.CreateInstance().Base2String(_tutorial.Attribute("content").Value);
            List<int> mTutorialList = new List<int>();
            for (int i = 0; i < strTutorial.Length; i++)
            {
                mTutorialList.Add(strTutorial[i]);
            }

            XElement _model = _root.Element("Model");
            if (_model.Value.Equals("0"))
            {
                isLive = true;
            }
            else
            {
                isLive = false;
            }

            List<Light> tutorialLights = Business.FileBusiness.CreateInstance().ReadMidiContent(mTutorialList);
            InitTeachingData(tutorialLights);

            XElement _pages = _root.Element("Pages");
            nowPageName = _pages.Attribute("first").Value;
            foreach (XElement pageElement in _pages.Elements("Page"))
            {
                Dictionary<int, List<PageButtonModel>> mDictionaryListButton = new Dictionary<int, List<PageButtonModel>>();
                Dictionary<int, int> mDictionaryPosition = new Dictionary<int, int>();
                foreach (XElement buttonsElement in pageElement.Elements("Buttons"))
                {
                    List<PageButtonModel> mListButton = new List<PageButtonModel>();
                    foreach (XElement buttonElement in buttonsElement.Elements("Button"))
                    {
                        PageButtonModel model = new PageButtonModel();
                        XElement xDown = buttonElement.Element("Down");
                        {
                            foreach (var xEdit in xDown.Elements())
                            {
                                model._down.OperationModels.Add(XmlToModel(xEdit));
                            }
                        }

                        XElement xLoop = buttonElement.Element("Loop");

                        {
                            foreach (var xEdit in xLoop.Elements())
                            {
                                model._loop.OperationModels.Add(XmlToModel(xEdit));
                            }
                        }

                        XElement xUp = buttonElement.Element("Up");
                        {
                            foreach (var xEdit in xUp.Elements())
                            {
                                model._up.OperationModels.Add(XmlToModel(xEdit));
                            }
                        }
                        mListButton.Add(model);
                    }
                    mDictionaryListButton.Add(int.Parse(buttonsElement.Attribute("position").Value), mListButton);
                    mDictionaryPosition.Add(int.Parse(buttonsElement.Attribute("position").Value), -1);
                }
                pages.Add(pageElement.Attribute("name").Value, mDictionaryListButton);
                positions.Add(pageElement.Attribute("name").Value, mDictionaryPosition);
            }

            XElement _lights = _root.Element("Lights");
            foreach (XElement lightElement in _lights.Elements("Light"))
            {
                //lights.Add(lightElement.Attribute("name").Value, business.LightStringToLightList(lightElement.Attribute("value").Value));
                String str = Business.FileBusiness.CreateInstance().Base2String(lightElement.Attribute("value").Value);

                List<int> mList = new List<int>();
                for (int i = 0; i < str.Length; i++)
                {
                    mList.Add(str[i]);
                }
                lights.Add(lightElement.Attribute("name").Value, Business.FileBusiness.CreateInstance().ReadMidiContent(mList));
                //格式化时间
                int time = 0;

                List<Light> mActionBeanList = lights[lightElement.Attribute("name").Value];
                for (int l = 0; l < mActionBeanList.Count; l++)
                {
                    if (mActionBeanList[l].Time == 0)
                    {
                        mActionBeanList[l].Time = time;
                    }
                    else
                    {
                        time += mActionBeanList[l].Time;
                        mActionBeanList[l].Time = time;
                    }
                }
            }
            //foreach (var item in lights)
            //{
            //    LightBusiness.Print(item.Value);
            //    Console.WriteLine("----------");
            //}
        }

        public InputPort ip;

        public class InputPort
        {
            public String cb;
            private PlayUserControl pc;
            private IntPtr handle;

            public InputPort()
            {

            }

            public InputPort(PlayUserControl pc)
            {
                midiInProc = new NativeMethods.MidiInProc(MidiProc);
                handle = IntPtr.Zero;
                this.pc = pc;
            }

            public void Load() {
                foreach (var item in pc.iInputAndOutputControls) {
                    item.OutputLight(FeedbackToConsole);
                }
                StaticConstant.mw.SetLog(LogTag.Plug, "载入了" + pc.iInputAndOutputControls.Count + "个输入输出插件", Level.Normal);
            }

            public static int InputCount
            {
                get { return NativeMethods.midiInGetNumDevs(); }
            }


            public bool Close()
            {
                bool result = NativeMethods.midiInClose(handle)
                    == NativeMethods.MMSYSERR_NOERROR;
                handle = IntPtr.Zero;
                return result;
            }


            public bool Open(int id)
            {
                return NativeMethods.midiInOpen(
                    out handle,   //HMIDIIN
                    id,           //id
                    midiInProc,   //CallBack
                    IntPtr.Zero,  //CallBack Instance
                    NativeMethods.CALLBACK_FUNCTION) == NativeMethods.MMSYSERR_NOERROR;//flag
            }


            public bool Start()
            {
                return NativeMethods.midiInStart(handle)
                    == NativeMethods.MMSYSERR_NOERROR;
            }


            public bool Stop()
            {
                return NativeMethods.midiInStop(handle)
                    == NativeMethods.MMSYSERR_NOERROR;
            }



            private void MidiProc(IntPtr hMidiIn,
                uint wMsg,
                IntPtr dwInstance,
                uint dwParam1,
                uint dwParam2)
            {
                // Receive messages here
                //Console.WriteLine("{0} {1} {2}", wMsg, dwParam1, dwParam2);

                if (wMsg == 963)
                {
                    uint _dwParam1 = dwParam1 & 0xFFFF;
                    uint l_dw1 = 0;
                    l_dw1 = (_dwParam1 >> 8) & 0xFF; //位置
                    uint h_dw1 = 0;
                    h_dw1 = _dwParam1 & 0xFF;
                    uint l2_dw1 = (_dwParam1) & 0xFFFFFF;
                    //Console.WriteLine(Convert.ToString(wMsg, 16));
                    //Console.WriteLine(Convert.ToString(h_dw1, 16));
                    //Console.WriteLine(Convert.ToString(l_dw1, 16));
                    //Console.WriteLine(Convert.ToString(l2_dw1, 16));
                    //Console.WriteLine("-------------------------------");

                    //uint position = ((dwParam1 & 0xFFFF) >> 8) & 0xFF;
                    uint position = l_dw1;


                    if (dwParam1 > 32767)
                    {
                        StaticConstant.mw.Dispatcher.Invoke(
                         new Action(
                          delegate
                 {
                     StaticConstant.mw.SetButton((int)position);
                 }
                ));
                        StaticConstant.mw.SetLog(LogTag.Input_Output, "按下了" + position, Level.Normal);

                        KeyEvent((int)position, InputAndOutputControlEnum.KeyModel.KeyDown);
                            //TODO 一键复原教程轨
                            //if (cb.Contains("Pro") && (int)position == 91)
                            //{
                            //    CloseLight();
                            //    pc.StartTutorial();
                            //    return;
                            //}
                            //if (cb.Contains("MK2") && (int)position == 104)
                            //{
                            //    CloseLight();
                            //    pc.StartTutorial();
                            //    return;
                            //}
                        //----教程轨结束
                    }
                    else
                    {
                        StaticConstant.mw.SetLog(LogTag.Input_Output, "抬起了" + position, Level.Normal);

                        KeyEvent((int)position, InputAndOutputControlEnum.KeyModel.KeyUp);
                    }

                    if (pages.Count == 0)
                        return;

                    if (!pages[nowPageName].ContainsKey((int)position))
                        return;

                    //顶灯的数值计算后和下面的键位会重叠
                    //5db5 - b5顶灯
                    //XX95 - 95其余的灯
                    //手动进入Live模式，发现9X是其余的灯
                    String number = Convert.ToString(l2_dw1, 16).Substring(Convert.ToString(l2_dw1, 16).Length - 2);
                    bool bNumber = number[0].Equals('9');
                    if (dwParam1 > 32767 && bNumber)
                    {
                        //打开
                        //Console.WriteLine("开："+position);
                        //System.Windows.Forms.MessageBox.Show(position.ToString());
                        positions[nowPageName][(int)position] = (positions[nowPageName][(int)position] + 1) % pages[nowPageName][(int)position].Count;
                        Thread newThread = new Thread(
                        new ParameterizedThreadStart(PlayToLaunchpad)
                      );
                        threads.Add(newThread, new List<object>() { (int)position, true });
                        newThread.Start(newThread);
                    }
                    else if (dwParam1 <= 32767 && bNumber)
                    {
                        //关闭
                        //Console.WriteLine("关：" + position);
                        foreach (var bigItem in threads)
                        {
                            List<object> _item = new List<object>();
                            _item = bigItem.Value;
                            int _position = (int)_item[0];
                            if (position == _position)
                            {
                                bool _isClose = (bool)_item[1];
                                //如果已经关闭
                                if (_isClose == false)
                                {
                                    continue;
                                }
                                else
                                {
                                    threads[bigItem.Key] = new List<Object>() { _position, false };
                                    break;
                                }
                            }
                        }
                        Thread newThread = new Thread(
                   new ParameterizedThreadStart(StopToLaunchpad)
                   );
                        threadsStop.Add(newThread, new List<object>() { (int)position, true });
                        newThread.Start(newThread);
                    }

                }
                else
                {
                    //Console.WriteLine(Convert.ToString(wMsg, 16));
                    //Console.WriteLine(Convert.ToString(dwParam1, 16));
                    //Console.WriteLine(Convert.ToString(dwParam2, 16));
                    //Console.WriteLine("-------------------------------");
                }
            }

            private void PlayToLaunchpad(object thread)
            {
                Thread _thread = thread as Thread;
                List<object> _objects = threads[_thread];
                //按下
                BaseButtonModel downModel = pages[nowPageName][(int)_objects[0]][positions[nowPageName][(int)_objects[0]]]._down;
                foreach (var item in downModel.OperationModels)
                {
                    if (item is AudioFilePlayModel)
                    {
                        //翻页
                        AudioFilePlayModel audioFilePlayModel = item as AudioFilePlayModel;
                        String audio = audioFilePlayModel.AudioName;
                        if (!audio.Equals(String.Empty) && File.Exists(StaticConstant.mw.LastProjectPath + "Audio/" + audio))
                        {
                            MediaPlayer player = new MediaPlayer();
                            medias.Add(player);
                            player.MediaEnded += Player_MediaEnded;
                            player.Volume = 1.0;
                            player.Open(new Uri(StaticConstant.mw.LastProjectPath + "Audio/" + audio, UriKind.RelativeOrAbsolute));
                            player.Play();
                            
                            //COM组件 Window Media Player 也是有延迟。
                            //WindowsMediaPlayer axWindowsMediaPlayer1 = new WindowsMediaPlayer();
                            //axWindowsMediaPlayer1.URL = StaticConstant.mw.LastProjectPath + "Audio/" + audio;
                            //axWindowsMediaPlayer1.controls.play();

                            //pc.PlayAudio(audio);
                        }
                    }
                    else if (item is LightFilePlayModel)
                    {
                        LightFilePlayModel lightFilePlayModel = item as LightFilePlayModel;
                        if (!lightFilePlayModel.FileName.Equals(String.Empty) && lights.ContainsKey(lightFilePlayModel.FileName))
                        {
                            Thread thread2 = new Thread(
                       new ParameterizedThreadStart(PlayLight)
                     );
                            thread2.Start(lightFilePlayModel);
                        }
                    }
                    else if (item is GotoPagePlayModel)
                    {
                        //翻页
                        GotoPagePlayModel gotoPagePlayModel = item as GotoPagePlayModel;
                        String page = gotoPagePlayModel.PageName;
                        if (!page.Equals(String.Empty))
                        {
                            nowPageName = page;
                        }
                    }
                }

                ////循环
                //LoopButtonModel loopModel = pages[nowPageName][(int)_objects[0]][positions[nowPageName][(int)_objects[0]]]._loop;
                //if (!loopModel._lightName.Equals(String.Empty) && lights.ContainsKey(loopModel._lightName))
                //{
                //    List<Light> loopLight = lights[loopModel._lightName];
                //    int maxTime = LightBusiness.GetMax(loopLight);
                //    if (!Double.TryParse(loopModel._bpm, out double loopBpm))
                //    {
                //        _thread.Abort();
                //        threads.Remove(_thread);
                //        return;
                //    }
                //    TimeSpan wait = TimeSpan.FromMilliseconds(1000 / loopBpm);

                //    //翻页
                //    String page = pages[nowPageName][(int)_objects[0]][positions[nowPageName][(int)_objects[0]]]._down._goto;
                //    if (!page.Equals(String.Empty))
                //    {
                //        nowPageName = page;
                //    }

                //    while (true)
                //    {
                //        for (int i = 0; i <= maxTime; i++)
                //        {
                //            for (int j = 0; j < loopLight.Count; j++)
                //            {
                //                if (loopLight[j].Time == i)
                //                {
                //                    Thread mThread = thread as Thread;
                //                    List<object> mObjects = threads[mThread];
                //                    if ((bool)mObjects[1] == false)
                //                    {
                //                        mThread.Abort();
                //                        threads.Remove(mThread);
                //                        break;
                //                    }
                //                    if (loopLight[j].Position >= 28 && loopLight[j].Position <= 35)
                //                    {
                //                        //顶部灯光
                //                        if (loopLight[j].Action == 144)
                //                        {
                //                            String str = "0x" + loopLight[j].Color.ToString("X2").PadLeft(2, '0') + (loopLight[j].Position + 63).ToString("X2").PadLeft(2, '0') + "b5";
                //                            MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                //                        }
                //                        else
                //                        {
                //                            String str = "0x" + (loopLight[j].Position + 63).ToString("X2").PadLeft(2, '0') + "b5";
                //                            MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                //                        }
                //                    }
                //                    else
                //                    {
                //                        MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(loopLight[j].Action + loopLight[j].Position * 0x100 + loopLight[j].Color * 0x10000 + passageway));
                //                    }
                //                }
                //            }
                //            Thread.Sleep(wait);
                //        }
                //    }
                //}
            }

            private void KeyEvent(int position, InputAndOutputControlEnum.KeyModel keyModel)
            {
                if (NowModel == Model.Launchpad)
                {
                    if (position < 28)
                    {
                        return;
                    }
                    List<Light> positions = new List<Light>();
                    positions.Add(new Light(0, 0, position - 28, 0));
                    Operation.FileBusiness.CreateInstance().ReplaceControl(positions, Operation.FileBusiness.CreateInstance().normalArr);
                    position = positions[0].Position;
                }

                foreach (var item in pc.iInputAndOutputControls)
                {
                    item.OnInput(position, keyModel);

                    StaticConstant.mw.SetLog(LogTag.Input_Output, "向插件发送了" + position, Level.Normal);
                }
            }

            /// <summary>
            /// 静态回调方法
            /// </summary>
            /// <param name="value"></param>
            private static void FeedbackToConsole(List<Light> lights)
            {
                StaticConstant.mw.SetLog(LogTag.Input_Output, "插件发送了灯光", Level.Normal);

                PlaySingleLight(lights);
            }

            public void PlayIntLight(int[] intLight)
            {
                for (int i = 0; i < intLight.Length; i++)
                {
                    if (i == 0 && i <= 8)
                    {
                        //7f5bb5 7f - 颜色 - 128 5b - 位置 - 91 b5应该是
                        //顶部灯光
                        if (intLight[i] != 0)
                        {
                            String str = "0x" + intLight[i].ToString("X2").PadLeft(2, '0') + ((i + 28) + 63).ToString("X2").PadLeft(2, '0') + "b5";
                            MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                        }
                        else
                        {
                            String str = "0x" + ((i + 28) + 63).ToString("X2").PadLeft(2, '0') + "b5";
                            MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                        }
                    }
                    else
                    {
                        if (intLight[i] != 0)
                        {
                            MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(144 + (i + 28) * 0x100 + intLight[i] * 0x10000 + passageway));
                        }
                        else
                        {
                            MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(128 + (i + 28) * 0x100 + intLight[i] * 0x10000 + passageway));
                        }
                    }
                }

            }
        }

        //private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    unreg_hotkey();
        //}



        private string ReadDataFromReg()
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\DD XOFT\\", false);
            if (key != null)
            {
                foreach (string vname in key.GetValueNames())
                {
                    if ("path" == vname.ToLower())
                    {
                        return key.GetValue(vname, "").ToString();
                    }
                }
            }
            return "";
        }

        private void Fun100()
        {
            MessageBox.Show("热键ID=100");
        }
        internal static class NativeMethods
        {
            internal const int MMSYSERR_NOERROR = 0;
            internal const int CALLBACK_FUNCTION = 0x00030000;


            internal delegate void MidiInProc(
                IntPtr hMidiIn,
                uint wMsg,
                IntPtr dwInstance,
                uint dwParam1,
                uint dwParam2);


            [DllImport("winmm.dll")]
            internal static extern int midiInGetNumDevs();


            [DllImport("winmm.dll")]
            internal static extern int midiInClose(
                IntPtr hMidiIn);


            [DllImport("winmm.dll")]
            internal static extern int midiInOpen(
                out IntPtr lphMidiIn,
                int uDeviceID,
                MidiInProc dwCallback,
                IntPtr dwCallbackInstance,
                int dwFlags);


            [DllImport("winmm.dll")]
            internal static extern int midiInStart(
                IntPtr hMidiIn);


            [DllImport("winmm.dll")]
            internal static extern int midiInStop(
                IntPtr hMidiIn);
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Height = mw.gMost.ActualHeight;

            LoadHint();

            LoadPlugs();
            
            if (ip != null) {
                ip.Load();
            }
        }

        public List<IInputAndOutputControl> iInputAndOutputControls = new List<IInputAndOutputControl>();

        protected override void LoadFileContent()
        {
            LoadExeXml();
        }


        public static IntPtr nowOutDeviceIntPtr = (IntPtr)(-1);
        public static int passageway = 0;
        private unsafe void btnImportRealPlayer_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)0x7f5cb5));
            //Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(0x7f + (91 * 0x100) + (60 * 0x10000) + 5)));
            //Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(0x70 + (91 * 0x100) + (60 * 0x10000) + 5)));
            //if (cbRealDevice.SelectedIndex == -1)
            //    return;
            //直接使用Thread类，以及其方法 
            //Thread threadA = new Thread(PlayToLaunchpad);
            //threadA.Start();
            a();
        }

        public void PlayAudio(String audio)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                MediaElement mediaElement = new MediaElement();
                mediaElement.Source = new Uri(StaticConstant.mw.LastProjectPath + @"Audio\" + audio, UriKind.RelativeOrAbsolute);
                mediaElement.LoadedBehavior = MediaState.Manual;
                mediaElement.MediaEnded += MediaElement_MediaEnded;
                //mediaElement.UnloadedBehavior = MediaState.Stop;
                //mediaElement.Stop();
                gMain.Children.Add(mediaElement);
                mediaElement.Play();
            }));
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (gMain.Children.Contains(sender as MediaElement))
            {
                gMain.Children.Remove(sender as MediaElement);
            }
        }

        /// <summary>
        /// 播放灯光
        /// </summary>
        private static void PlayLight(object thread)
        {
            LightFilePlayModel lightFilePlayModel = thread as LightFilePlayModel;
            List<Light> downLight = lights[lightFilePlayModel.FileName];

            int maxTime = Business.LightBusiness.GetMax(downLight);
            //if (!Double.TryParse(downModel._bpm, out double downBpm))
            //{
            //    _thread.Abort();
            //    threads.Remove(_thread);
            //    return;
            //}

            long _startTime = DateTime.Now.Ticks / 10000;

            TimeSpan wait = TimeSpan.FromMilliseconds(1000 / lightFilePlayModel.Bpm);
            for (int i = 0; i <= maxTime; i++)
            {
                for (int j = 0; j < downLight.Count; j++)
                {
                    if (downLight[j].Time == i)
                    {
                        if (isLive)
                        {
                            if (downLight[j].Position >= 28 && downLight[j].Position <= 35)
                            {
                                //7f5bb5 7f - 颜色 - 128 5b - 位置 - 91 b5应该是
                                //顶部灯光
                                if (downLight[j].Action == 144)
                                {
                                    String str = "0x" + downLight[j].Color.ToString("X2").PadLeft(2, '0') + (downLight[j].Position + 63).ToString("X2").PadLeft(2, '0') + "b5";
                                    //Console.WriteLine(str);
                                    //Console.WriteLine(Convert.ToInt64(str, 16));
                                    MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                                }
                                else
                                {
                                    String str = "0x" + (downLight[j].Position + 63).ToString("X2").PadLeft(2, '0') + "b5";
                                    MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                                }
                            }
                            else
                            {
                                MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(downLight[j].Action + downLight[j].Position * 0x100 + downLight[j].Color * 0x10000 + passageway));
                            }
                        }
                        else
                        {
                            //手动Live模式，强制通道为0
                            MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(downLight[j].Action + downLight[j].Position * 0x100 + downLight[j].Color * 0x10000 + 0));
                        }

                        //Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(0x90 + (int.Parse(tbTestPosition.Text) * 0x100) + (60 * 0x10000) + cbPassageway.SelectedIndex)));
                        //Thread.Sleep(1000);
                        //Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(0x80 + (int.Parse(tbTestPosition.Text) * 0x100) + (60 * 0x10000) + cbPassageway.SelectedIndex)));
                    }
                }

                if ((1000 / lightFilePlayModel.Bpm) - ((DateTime.Now.Ticks / 10000 - _startTime - (1000 / lightFilePlayModel.Bpm) * (i))) > 0)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds((1000 / lightFilePlayModel.Bpm) - ((DateTime.Now.Ticks / 10000 - _startTime - (1000 / lightFilePlayModel.Bpm) * (i)))));
                }
                else {
                    Thread.Sleep(wait);
                }

                //Thread.Sleep(TimeSpan.FromMilliseconds(DateTime.Now.Ticks / 10000 - _startTime - (1000 / lightFilePlayModel.Bpm) * i));
            }
        }

        static List<MediaPlayer> medias = new List<MediaPlayer>();


        private static void Player_MediaEnded(object sender, EventArgs e)
        {
            medias.Remove(sender as MediaPlayer);
        }

        private static void StopToLaunchpad(object thread)
        {
            Thread _thread = thread as Thread;
            List<object> _objects = threadsStop[_thread];
            //按下
            try
            {
                //TODO：可能-索引超出范围。必须为非负值并小于集合大小。
                BaseButtonModel downModel = pages[nowPageName][(int)_objects[0]][positions[nowPageName][(int)_objects[0]]]._up;
                foreach (var item in downModel.OperationModels)
                {
                    if (item is AudioFilePlayModel)
                    {
                        //翻页
                        AudioFilePlayModel audioFilePlayModel = item as AudioFilePlayModel;
                        String audio = audioFilePlayModel.AudioName;
                        if (!audio.Equals(String.Empty) && File.Exists(StaticConstant.mw.LastProjectPath + "Audio/" + audio))
                        {
                            MediaPlayer player = new MediaPlayer();
                            medias.Add(player);
                            player.MediaEnded += Player_MediaEnded;
                            player.Volume = 1;
                            player.Open(new Uri(StaticConstant.mw.LastProjectPath + "Audio/" + audio, UriKind.RelativeOrAbsolute));
                            player.Play();
                            //Dispatcher.BeginInvoke(new Action(delegate
                            //{
                            //    MediaElement mediaElement = new MediaElement();
                            //    mediaElement.Source = new Uri(StaticConstant.mw.LastProjectPath + "Audio/" + audio, UriKind.RelativeOrAbsolute);
                            //    //mediaElement.LoadedBehavior = MediaState.Manual;
                            //    mediaElement.Stop();
                            //    mediaElement.Play();
                            //}));
                        }
                    }
                    else if (item is LightFilePlayModel)
                    {
                        LightFilePlayModel lightFilePlayModel = item as LightFilePlayModel;
                        if (!lightFilePlayModel.FileName.Equals(String.Empty) && lights.ContainsKey(lightFilePlayModel.FileName))
                        {
                            Thread thread2 = new Thread(
                       new ParameterizedThreadStart(PlayLight)
                     );
                            thread2.Start(lightFilePlayModel);
                        }
                    }
                    else if (item is GotoPagePlayModel)
                    {
                        //翻页
                        GotoPagePlayModel gotoPagePlayModel = item as GotoPagePlayModel;
                        String page = gotoPagePlayModel.PageName;
                        if (!page.Equals(String.Empty))
                        {
                            nowPageName = page;
                        }
                    }
                }
            }
            catch {

            }
         
        }


        /// <summary>
        /// 播放灯光
        /// </summary>
        private static void PlaySingleLight(List<Light> ll)
        {
            List<Light> downLight = ll;
            for (int j = 0; j < downLight.Count; j++)
            {
                if (NowModel == Model.Launchpad)
                {
                    if (downLight[j].Position >= 28 && downLight[j].Position <= 35)
                    {
                        //7f5bb5 7f - 颜色 - 128 5b - 位置 - 91 b5应该是
                        //顶部灯光
                        if (downLight[j].Action == 144)
                        {
                            String str = "0x" + downLight[j].Color.ToString("X2").PadLeft(2, '0') + (downLight[j].Position + 63).ToString("X2").PadLeft(2, '0') + "b5";
                            //Console.WriteLine(str);
                            //Console.WriteLine(Convert.ToInt64(str, 16));
                            MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                        }
                        else
                        {
                            String str = "0x" + (downLight[j].Position + 63).ToString("X2").PadLeft(2, '0') + "b5";
                            MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                        }
                    }
                    else
                    {
                        MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(downLight[j].Action + downLight[j].Position * 0x100 + downLight[j].Color * 0x10000 + passageway));
                    }
                }
                else
                {
                    //手动Live模式，强制通道为0
                    MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(downLight[j].Action + downLight[j].Position * 0x100 + downLight[j].Color * 0x10000 + 0));
                }

                //Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(0x90 + (int.Parse(tbTestPosition.Text) * 0x100) + (60 * 0x10000) + cbPassageway.SelectedIndex)));
                //Thread.Sleep(1000);
                //Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(0x80 + (int.Parse(tbTestPosition.Text) * 0x100) + (60 * 0x10000) + cbPassageway.SelectedIndex)));
            }
        }


        public void CloseMidiOut()
        {
            try
            {
                MidiDeviceBusiness.midiOutClose(nowOutDeviceIntPtr);
            }
            catch { }
            finally
            {
                nowOutDeviceIntPtr = (IntPtr)(-1);
            }
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            if (toolBar.Template.FindName("OverflowGrid", toolBar) is FrameworkElement overflowGrid)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
            if (toolBar.Template.FindName("MainPanelBorder", toolBar) is FrameworkElement mainPanelBorder)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }

        public void CloseMidiConnect()
        {
            CloseMidiOut();
            if (ip != null)
            {
                ip.Stop();
                ip.Close();
            }
        }

        public void a()
        {
            //int moID = 1; // midi out device/port ID

            //PREVIOUS CODE
            //int moHdl; // midi out device/port handle
            //NEW CODE
            //IntPtr moHdl = IntPtr.Zero;
            IntPtr moHdl = nowOutDeviceIntPtr;
#if !true
    // SysEx via midiOutLongMsg works
    Chk (WinMM.midiOutOpen (out moHdl, moID, null, 0, 0)); // open midi out in non-stream mode
#else
            // SysEx via midiOutLongMsg fails
            //PREVIOUS CODE
            //Chk(WinMM.midiStreamOpen(out moHdl, ref moID, 1, null, 0, 0)); // open midi out in stream mode
            //NEW CODE
            //IntPtr instance = IntPtr.Zero;
            //Chk(WinMM.midiStreamOpen(out moHdl, ref moID, 1, null, instance, 0)); // open midi out in stream mode
            //Chk(WinMM.midiOutOpen(out moHdl, moID, null, (IntPtr)0, 0));// open midi out in stream mode
#endif
            //byte[] sx = { 240, 0, 32, 41, 2, 16, 20, 124, 1, 5, 72, 101, 108, 108, 111, 32, 2, 119, 111, 114, 108,100, 33, 247}; // GM On sysex
            String[] str = tbExport.Text.Split(',');
            byte[] sx = new byte[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                sx[i] = (byte)int.Parse(str[i]);
            }
            //byte[] sx = {240, 0 ,32, 41, 2 ,16 ,11 ,99 ,63 ,63 ,63 ,247 }; // GM On sysex
            //PREVIOUS CODE
            //int shdr = Marshal.SizeOf(typeof(MidiHdr)); // hdr size
            //NEW CODE
            int shdr = 0x40; // hdr size

            var mhdr = new MidiHdr(); // allocate managed hdr
            mhdr.bufferLength = mhdr.bytesRecorded = sx.Length; // length of message bytes
            mhdr.data = Marshal.AllocHGlobal(mhdr.bufferLength); // allocate native message bytes
            Marshal.Copy(sx, 0, mhdr.data, mhdr.bufferLength); // copy message bytes from managed to native memory
            IntPtr nhdr = Marshal.AllocHGlobal(shdr); // allocate native hdr
            Marshal.StructureToPtr(mhdr, nhdr, false); // copy managed hdr to native hdr

            Chk(WinMM.midiOutPrepareHeader(moHdl, nhdr, shdr)); // prepare native hdr
            Chk(WinMM.midiOutLongMsg(moHdl, nhdr, shdr)); // send native message bytes
            //Console.WriteLine(WinMM.midiOutPrepareHeader(moHdl, nhdr, shdr));
            //Console.WriteLine(WinMM.midiOutLongMsg(moHdl, nhdr, shdr));
        } // Test

        public static void Chk(int f)
        {
            if (0 == f) return;
            var sb = new StringBuilder(256); // MAXERRORLENGTH
            var s = 0 == WinMM.midiOutGetErrorText(f, sb, sb.Capacity) ? sb.ToString() : String.Format("MIDI Error {0}.", f);
            System.Diagnostics.Trace.WriteLine(s);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MidiHdr
        { // sending long MIDI messages requires a header
            public IntPtr data; // native pointer to message bytes, allocated on native heap
            public int bufferLength; // length of buffer 'data'
            public int bytesRecorded; // actual amount of data in buffer 'data'
            public int user; // custom user data
            public int flags; // information flags about buffer
            public IntPtr next; // reserved
            public int reserved; // reserved
            public int offset; // buffer offset on callback
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] reservedArray; // reserved
        } // struct MidiHdr

        internal sealed class WinMM
        { // native MIDI calls from WinMM.dll
            public delegate void CB(int hdl, int msg, int inst, int p1, int p2); // callback

            //PREVIOUS CODE
            //[DllImport("winmm.dll")]
            //public static extern int midiStreamOpen(out int hdl, ref int devID, int reserved, CB proc, int inst, int flags);
            //[DllImport("winmm.dll")]
            //public static extern int midiOutOpen(out int hdl, int devID, CB proc, int inst, int flags);
            //[DllImport("winmm.dll")]
            //public static extern int midiOutPrepareHeader(int hdl, IntPtr pHdr, int sHdr);
            //[DllImport("winmm.dll")]
            //public static extern int midiOutLongMsg(int hdl, IntPtr pHdr, int sHdr);
            //[DllImport("winmm.dll")]
            //public static extern int midiOutGetErrorText(int err, StringBuilder msg, int sMsg);

            //NEW CODE
            #region winmm declarations
            [DllImport("winmm.dll")]
            public static extern int midiOutPrepareHeader(IntPtr handle,
                IntPtr headerPtr, int sizeOfMidiHeader);
            [DllImport("winmm.dll")]
            public static extern int midiOutUnprepareHeader(IntPtr handle,
                IntPtr headerPtr, int sizeOfMidiHeader);
            [DllImport("winmm.dll")]
            public static extern int midiOutOpen(out IntPtr handle, int deviceID,
                CB proc, IntPtr instance, int flags);
            [DllImport("winmm.dll")]
            public static extern int midiOutGetErrorText(int errCode,
                StringBuilder message, int sizeOfMessage);
            [DllImport("winmm.dll")]
            public static extern int midiOutClose(IntPtr handle);
            [DllImport("winmm.dll")]
            public static extern int midiStreamOpen(out IntPtr handle, ref int deviceID, int reserved,
                CB proc, IntPtr instance, uint flag);
            [DllImport("winmm.dll")]
            public static extern int midiStreamClose(IntPtr handle);
            [DllImport("winmm.dll")]
            public static extern int midiStreamOut(IntPtr handle, IntPtr headerPtr, int sizeOfMidiHeader);
            [DllImport("winmm.dll")]
            public static extern int midiOutLongMsg(IntPtr handle,
                IntPtr headerPtr, int sizeOfMidiHeader);
            #endregion

        } // class WinMM
 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mw.LoadDevice();
            LoadPlugs();
        }

        private void LoadPlugs()
        {
            iInputAndOutputControls.Clear();
            foreach (var item in StaticConstant.mw.Plugs)
            {
                if (StaticConstant.mw.plugsConfigModel.Plugs[StaticConstant.mw.Plugs.IndexOf(item)].Enable) {
                    foreach (var control in item.GetControl())
                    {
                        if (control is IInputAndOutputControl)
                        {
                            iInputAndOutputControls.Add(control as IInputAndOutputControl);
                        }
                    }
                }
            }
        }

      

        private void cbModel_Checked(object sender, RoutedEventArgs e)
        {
            NowModel = Model.Launchpad;
        
        }

        private void cbModel_Unchecked(object sender, RoutedEventArgs e)
        {
            NowModel = Model.Normal;
        }
        //private void btnMustOpenMidi_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!int.TryParse(tbMustOpenNumber.Text, out int number)) {
        //        return;
        //    }
        //    CloseMidiOut();
        //    uint i = MidiDeviceBusiness.midiOutOpen(out nowOutDeviceIntPtr, (uint)number, (IntPtr)0, (IntPtr)0, 0);
        //    System.Windows.Forms.MessageBox.Show(i.ToString());

        //}
    }
}
