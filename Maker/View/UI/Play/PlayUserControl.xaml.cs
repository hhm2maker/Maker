using Maker.Business;
using Maker.Business.Model;
using Maker.Model;
using Maker.View.UI.UserControlDialog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace Maker.View.UI
{
    /// <summary>
    /// PlayUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class PlayUserControl : BaseUserControl
    {
        private static NativeMethods.MidiInProc midiInProc;

        public PlayUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            _fileExtension = ".play";
            _fileType = "Play";
            mainView = gMain;

            HideControl();
        }
        private int tutorialPosition = 0;
        private void LoadKeyboards(object sender, RoutedEventArgs e)
        {
            XDocument _doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"Keyboard\keyboard.xml");
            XElement _root = _doc.Element("Root");


            foreach (XElement keyElement in _root.Elements("Key"))
            {
                KeyboardModel keyboardModel = new KeyboardModel();
                String strPosition = keyElement.Attribute("position").Value;
                if (strPosition.Equals(String.Empty))
                {
                    keyboardModel.Position = -1;
                }
                else
                {
                    if (int.TryParse(strPosition, out int result))
                    {
                        keyboardModel.Position = result;
                    }
                    else
                    {
                        keyboardModel.Position = -1;
                    }
                }
                String strDdKey = keyElement.Attribute("ddkey").Value;
                if (strDdKey.Equals(String.Empty))
                {
                    keyboardModel.DdKey = -1;
                }
                else
                {
                    if (int.TryParse(strDdKey, out int result))
                    {
                        keyboardModel.DdKey = result;
                    }
                    else
                    {
                        keyboardModel.DdKey = -1;
                    }
                }
                keyboardModel.SendKey = keyElement.Attribute("sendkey").Value;
                if (keyboardModel.Position != -1 && !keyboardModels.ContainsKey(keyboardModel.Position))
                    keyboardModels.Add(keyboardModel.Position, keyboardModel);
            }
            if (ip == null)
                return;
            ip.button1_Click();
        }

        private void LoadHint()
        {
            if (mw.hintModelDictionary.ContainsKey(1))
            {
                if (mw.hintModelDictionary[1].IsHint == false)
                {
                    //InstallUsbDriver();
                    return;
                }
            }
            HintDialog hintDialog = new HintDialog("安装固件", "您是否要安装固件？", BtnChangeLanguage_Ok_Click, BtnChangeLanguage_Cancel_Click, BtnChangeLanguage_NotHint_Click);
            mw.ShowMakerDialog(hintDialog);
        }

        private void InstallUsbDriver()
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\Attachments\novation-usb-driver-2.7.exe");
        }
        private void BtnChangeLanguage_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (mw.hintModelDictionary.ContainsKey(1))
            {
                if (mw.hintModelDictionary[1].IsHint == false)
                {
                    return;
                }
            }
            InstallUsbDriver();
            RemoveDialog();
        }

        private void BtnChangeLanguage_Cancel_Click(object sender, RoutedEventArgs e)
        {
            RemoveDialog();
        }

        private void BtnChangeLanguage_NotHint_Click(object sender, RoutedEventArgs e)
        {
            NotHint(1);
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


        private FileBusiness business = new FileBusiness();
        private static Dictionary<String, Dictionary<int, List<PageButtonModel>>> pages = new Dictionary<string, Dictionary<int, List<PageButtonModel>>>();
        /// <summary>
        /// 前面的int是位置，后面的是次数
        /// </summary>
        private static Dictionary<String, Dictionary<int, int>> positions = new Dictionary<string, Dictionary<int, int>>();
        private static Dictionary<String, List<Light>> lights = new Dictionary<String, List<Light>>();
        private static String nowPageName = String.Empty;
        private static Dictionary<Thread, List<object>> threads = new Dictionary<Thread, List<object>>();
        private static Dictionary<Thread, List<object>> threadsStop = new Dictionary<Thread, List<object>>();

        /// <summary>
        /// 读取xml文件
        /// </summary>
        public void LoadExeXml()
        {
            pages.Clear();
            positions.Clear();
            lights.Clear();

            DirectoryInfo d = new DirectoryInfo(mw.lastProjectPath);
            XDocument _doc = XDocument.Load(filePath);
            XElement _root = _doc.Element("Root");

            XElement _tutorial = _root.Element("Tutorial");
            String strTutorial = business.Base2String(_tutorial.Attribute("content").Value);
            List<int> mTutorialList = new List<int>();
            for (int i = 0; i < strTutorial.Length; i++)
            {
                mTutorialList.Add(strTutorial[i]);
            }
            List<Light> tutorialLights = business.ReadMidiContent(mTutorialList);
            mTeachingControl.InitTeaching(tutorialLights);

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
                        XElement elementDown = buttonElement.Element("Down");
                        model._down._lightName = elementDown.Attribute("lightname").Value;
                        model._down._goto = elementDown.Attribute("goto").Value;
                        model._down._bpm = elementDown.Attribute("bpm").Value;
                        XElement elementLoop = buttonElement.Element("Loop");
                        model._loop._lightName = elementLoop.Attribute("lightname").Value;
                        model._loop._goto = elementLoop.Attribute("goto").Value;
                        model._loop._bpm = elementLoop.Attribute("bpm").Value;
                        XElement elementUp = buttonElement.Element("Up");
                        model._up._lightName = elementUp.Attribute("lightname").Value;
                        model._up._goto = elementUp.Attribute("goto").Value;
                        model._up._bpm = elementUp.Attribute("bpm").Value;
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
                String str = business.Base2String(lightElement.Attribute("value").Value);

                List<int> mList = new List<int>();
                for (int i = 0; i < str.Length; i++)
                {
                    mList.Add(str[i]);
                }
                lights.Add(lightElement.Attribute("name").Value, business.ReadMidiContent(mList));
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
            public TextBox tbPosition;
            public String cb;
            private PlayUserControl pc;
            private IntPtr handle;
            private CDD dd;
            private Dictionary<int, KeyboardModel> keyboardModels;

            public InputPort() {

            }

            public InputPort( PlayUserControl pc, Dictionary<int, KeyboardModel> keyboardModels, int inputType)
            {
                midiInProc = new NativeMethods.MidiInProc(MidiProc);
                handle = IntPtr.Zero;
                this.keyboardModels = keyboardModels;
                this.inputType = inputType;
                this.pc = pc;
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
                    //dwParam1 = dwParam1 & 0xFFFF;
                    //uint l_dw1 = 0;
                    //l_dw1 = (dwParam1 >> 8) & 0xFF; //位置
                    //uint h_dw1 = 0;
                    //h_dw1 = dwParam1 & 0xFF;
                    //l2_dw1 = (dwParam1) & 0xFFFFFF;
                    //Console.WriteLine(Convert.ToString(wMsg, 16));
                    //Console.WriteLine(Convert.ToString(h_dw1, 16));
                    //Console.WriteLine(Convert.ToString(l_dw1, 16));
                    //Console.WriteLine(Convert.ToString(l2_dw1, 16));
                    //Console.WriteLine("-------------------------------");
                    uint position = ((dwParam1 & 0xFFFF) >> 8) & 0xFF;

                    if (dwParam1 > 32767)
                    {
                        StaticConstant.mw.Dispatcher.Invoke(
                         new Action(
                          delegate
                 {
                     StaticConstant.mw.SetButton((int)position);
                }
                ));
                  
                        KeyEvent((int)position, 0);

                        tbPosition.Dispatcher.Invoke(new Action(() => { tbPosition.Text = tbPosition.Text + position + " "; }));

                        if (pc.tutorialParagraphLightIntList != null)
                        {
                            if (cb.Contains("Pro") && (int)position == 91)
                            {
                                CloseLight();
                                pc.StartTutorial();
                                return;
                            }
                            if (cb.Contains("MK2") && (int)position == 104)
                            {
                                CloseLight();
                                pc.StartTutorial();
                                return;
                            }
                            if (pc.tutorialParagraphLightIntList[pc.tutorialPosition].Contains((int)position))
                            {
                                pc.tutorialParagraphLightIntList[pc.tutorialPosition].Remove((int)position);
                                MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(128 + (int)position * 0x100 + 64 * 0x10000 + passageway));
                            }

                            if (pc.tutorialParagraphLightIntList[pc.tutorialPosition].Count == 0)
                            {
                                pc.tutorialPosition++;
                                pc.tutorialPosition = pc.tutorialPosition % pc.tutorialParagraphLightIntList.Count;

                                for (int j = 0; j < pc.tutorialParagraphLightIntList[pc.tutorialPosition].Count; j++)
                                {
                                    if (pc.tutorialParagraphLightIntList[pc.tutorialPosition][j] >= 28 && pc.tutorialParagraphLightIntList[pc.tutorialPosition][j] <= 35)
                                    {
                                        //7f5bb5 7f - 颜色 - 128 5b - 位置 - 91 b5应该是
                                        //顶部灯光
                                        String str = "0x" + 3.ToString("X2").PadLeft(2, '0') + (pc.tutorialParagraphLightIntList[pc.tutorialPosition][j] + 63).ToString("X2").PadLeft(2, '0') + "b5";
                                        MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                                    }
                                    else
                                    {
                                        MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(144 + pc.tutorialParagraphLightIntList[pc.tutorialPosition][j] * 0x100 + 3 * 0x10000 + passageway));
                                    }
                                }
                            }
                        }
                        //----教程轨结束
                    }
                    else {
                        KeyEvent((int)position, 1);
                    }
                    if(pages.Count == 0)
                        return;
                    if (!pages[nowPageName].ContainsKey((int)position))
                        return;

                    if (dwParam1 > 32767)
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
                    else
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


            public int inputType = 0;//0浅度 1深度(驱动)
            private void KeyEvent(int position, int openOrClose)
            {
                //模拟键盘输入
                if (!keyboardModels.ContainsKey(position) )
                {
                    return;
                }
                if (inputType == 0 && openOrClose == 0)
                {
                    System.Windows.Forms.SendKeys.SendWait(keyboardModels[position].SendKey);
                }
                else if (inputType == 1)
                {
                    if (dd == null)
                        return;
                    if (openOrClose == 0)
                    {
                        int ddcode = keyboardModels[position].DdKey;                         //tab键位在DD键码表的3区第1个位置
                        dd.key(ddcode, 1);                                                  // 1=按下 2=放开          
                    }
                    if (openOrClose == 1)
                    {
                        int ddcode = keyboardModels[position].DdKey;
                        dd.key(ddcode, 2);
                    }
                }
            }

            public void button1_Click()
            {
                //可从注册表中直接获取
                //string dllfile = ReadDataFromReg();

                //LoadDllFile(dllfile);
                //return;
                if (dd == null)
                    LoadDllFile(AppDomain.CurrentDomain.BaseDirectory + @"Dll\Keyboard\DD85590.64.dll");
            }

            public void PlayIntLight(int[] intLight) {
                for (int i = 0; i < intLight.Length; i++) {
                    if (i ==0 && i <= 8)
                    {
                        //7f5bb5 7f - 颜色 - 128 5b - 位置 - 91 b5应该是
                        //顶部灯光
                        if (intLight[i] != 0)
                        {
                            String str = "0x" + intLight[i].ToString("X2").PadLeft(2, '0') + ((i+28) + 63).ToString("X2").PadLeft(2, '0') + "b5";
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
                        else {
                            MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(128 + (i + 28) * 0x100 + intLight[i] * 0x10000 + passageway));
                        }
                    }
                }
              
            }


            private void LoadDllFile(string dllfile)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(dllfile);
                if (!fi.Exists)
                {
                    MessageBox.Show("文件不存在");
                    return;
                }
                dd = new CDD();
                int ret = dd.Load(dllfile);
                if (ret == -2) { MessageBox.Show("装载库时发生错误"); return; }
                if (ret == -1) { MessageBox.Show("取函数地址时发生错误"); return; }
                if (ret == 0) { MessageBox.Show("非增强模块"); }
            }

            public void OpenLight() {
                if (pc.tutorialParagraphLightIntList != null)
                {
                    for (int j = 0; j < pc.tutorialParagraphLightIntList[pc.tutorialPosition].Count; j++)
                    {
                        if (pc.tutorialParagraphLightIntList[pc.tutorialPosition][j] >= 28 && pc.tutorialParagraphLightIntList[pc.tutorialPosition][j] <= 35)
                        {
                            //7f5bb5 7f - 颜色 - 128 5b - 位置 - 91 b5应该是
                            //顶部灯光
                            String str = "0x" + 3.ToString("X2").PadLeft(2, '0') + (pc.tutorialParagraphLightIntList[pc.tutorialPosition][j] + 63).ToString("X2").PadLeft(2, '0') + "b5";
                            MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                        }
                        else
                        {
                            MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(144 + pc.tutorialParagraphLightIntList[pc.tutorialPosition][j] * 0x100 + 3 * 0x10000 + passageway));
                        }
                    }
                }
            }
            public void CloseLight()
            {
                if (pc.tutorialParagraphLightIntList != null)
                {
                    for (int j = 0; j < pc.tutorialParagraphLightIntList[pc.tutorialPosition].Count; j++)
                    {
                        MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(128 + (int)pc.tutorialParagraphLightIntList[pc.tutorialPosition][j] * 0x100 + 64 * 0x10000 + passageway));
                    }
                }
            }
        }

        //private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    unreg_hotkey();
        //}


        public Dictionary<int, KeyboardModel> keyboardModels = new Dictionary<int, KeyboardModel>();

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
            Width = mw.ActualWidth * 0.9;
            Height = mw.gMost.ActualHeight;
           
            LoadHint();
        }

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

        private static void PlayToLaunchpad(object thread)
        {
            Thread _thread = thread as Thread;
            List<object> _objects = threads[_thread];
            //按下
            DownButtonModel downModel = pages[nowPageName][(int)_objects[0]][positions[nowPageName][(int)_objects[0]]]._down;
            if (!downModel._lightName.Equals(String.Empty) && lights.ContainsKey(downModel._lightName))
            {
                List<Light> downLight = lights[downModel._lightName];
                int maxTime = LightBusiness.GetMax(downLight);
                if (!Double.TryParse(downModel._bpm, out double downBpm))
                {
                    _thread.Abort();
                    threads.Remove(_thread);
                    return;
                }
                TimeSpan wait = TimeSpan.FromMilliseconds(1000 / downBpm);
                //int wait = Convert.ToInt32(1000 / Double.Parse(pages["1.xml"][0].Bpm));

                //翻页
                String page = pages[nowPageName][(int)_objects[0]][positions[nowPageName][(int)_objects[0]]]._down._goto;
                if (!page.Equals(String.Empty))
                {
                    nowPageName = page;
                }

                for (int i = 0; i <= maxTime; i++)
                {
                    for (int j = 0; j < downLight.Count; j++)
                    {
                        if (downLight[j].Time == i)
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
                            //Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(0x90 + (int.Parse(tbTestPosition.Text) * 0x100) + (60 * 0x10000) + cbPassageway.SelectedIndex)));
                            //Thread.Sleep(1000);
                            //Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(0x80 + (int.Parse(tbTestPosition.Text) * 0x100) + (60 * 0x10000) + cbPassageway.SelectedIndex)));
                        }
                    }
                    Thread.Sleep(wait);
                }
            }
            //循环
            LoopButtonModel loopModel = pages[nowPageName][(int)_objects[0]][positions[nowPageName][(int)_objects[0]]]._loop;
            if (!loopModel._lightName.Equals(String.Empty) && lights.ContainsKey(loopModel._lightName))
            {
                List<Light> loopLight = lights[loopModel._lightName];
                int maxTime = LightBusiness.GetMax(loopLight);
                if (!Double.TryParse(loopModel._bpm, out double loopBpm))
                {
                    _thread.Abort();
                    threads.Remove(_thread);
                    return;
                }
                TimeSpan wait = TimeSpan.FromMilliseconds(1000 / loopBpm);

                //翻页
                String page = pages[nowPageName][(int)_objects[0]][positions[nowPageName][(int)_objects[0]]]._down._goto;
                if (!page.Equals(String.Empty))
                {
                    nowPageName = page;
                }

                while (true)
                {
                    for (int i = 0; i <= maxTime; i++)
                    {
                        for (int j = 0; j < loopLight.Count; j++)
                        {
                            if (loopLight[j].Time == i)
                            {
                                Thread mThread = thread as Thread;
                                List<object> mObjects = threads[mThread];
                                if ((bool)mObjects[1] == false)
                                {
                                    mThread.Abort();
                                    threads.Remove(mThread);
                                    break;
                                }
                                if (loopLight[j].Position >= 28 && loopLight[j].Position <= 35)
                                {
                                    //顶部灯光
                                    if (loopLight[j].Action == 144)
                                    {
                                        String str = "0x" + loopLight[j].Color.ToString("X2").PadLeft(2, '0') + (loopLight[j].Position + 63).ToString("X2").PadLeft(2, '0') + "b5";
                                        MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                                    }
                                    else
                                    {
                                        String str = "0x" + (loopLight[j].Position + 63).ToString("X2").PadLeft(2, '0') + "b5";
                                        MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                                    }
                                }
                                else
                                {
                                    MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(loopLight[j].Action + loopLight[j].Position * 0x100 + loopLight[j].Color * 0x10000 + passageway));
                                }
                            }
                        }
                        Thread.Sleep(wait);
                    }
                }
            }
        }
        private static void StopToLaunchpad(object thread)
        {
            try
            {
                Thread _thread = thread as Thread;
                List<object> _objects = threadsStop[_thread];
                //抬起
                UpButtonModel upModel = pages[nowPageName][(int)_objects[0]][positions[nowPageName][(int)_objects[0]]]._up;
                if (!upModel._lightName.Equals(String.Empty) && lights.ContainsKey(upModel._lightName))
                {
                    List<Light> upLight = lights[upModel._lightName];
                    int maxTime = LightBusiness.GetMax(upLight);

                    if (!Double.TryParse(upModel._bpm, out double upBpm))
                    {
                        _thread.Abort();
                        threadsStop.Remove(_thread);
                        return;
                    }
                    TimeSpan wait = TimeSpan.FromMilliseconds(1000 / upBpm);

                    //翻页
                    String page = pages[nowPageName][(int)_objects[0]][positions[nowPageName][(int)_objects[0]]]._down._goto;
                    if (!page.Equals(String.Empty))
                    {
                        nowPageName = page;
                    }

                    for (int i = 0; i <= maxTime; i++)
                    {
                        for (int j = 0; j < upLight.Count; j++)
                        {
                            if (upLight[j].Time == i)
                            {
                                if (upLight[j].Position >= 28 && upLight[j].Position <= 35)
                                {
                                    //顶部灯光
                                    if (upLight[j].Action == 144)
                                    {
                                        String str = "0x" + upLight[j].Color.ToString("X2").PadLeft(2, '0') + (upLight[j].Position + 63).ToString("X2").PadLeft(2, '0') + "b5";
                                        MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                                    }
                                    else
                                    {
                                        String str = "0x" + (upLight[j].Position + 63).ToString("X2").PadLeft(2, '0') + "b5";
                                        MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                                    }
                                }
                                else
                                {
                                    MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(upLight[j].Action + upLight[j].Position * 0x100 + upLight[j].Color * 0x10000 + passageway));

                                }
                            }
                        }
                        Thread.Sleep(wait);
                    }
                    _thread.Abort();
                    threadsStop.Remove(_thread);
                }
            }
            catch { }
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

     

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mTeachingControl.SetSize(mTeachingControl.ActualHeight);
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
            for (int i = 0; i < str.Length; i++) {
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

        public int inputType = 0;
        private void cbIsDD_Checked(object sender, RoutedEventArgs e)
        {
            if (ip != null)
            {
                ip.inputType = 1;
            }
            inputType = 1;
        }

        private void cbIsDD_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ip != null)
                ip.inputType = 0;
            inputType = 0;

        }
        public List<List<int>> oldTutorialParagraphLightIntList;
        public List<List<int>> tutorialParagraphLightIntList;
        private void LoadTutorial(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "MIDI文件|*.mid;*.midi";
            //openFileDialog1.Filter = _fileExtension.Substring(1) + "文件(*" + _fileExtension + ")|*" + _fileExtension + "|All files(*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<Light> tutorialLightList = FileBusiness.CreateInstance().ReadMidiFile(openFileDialog1.FileName);

                oldTutorialParagraphLightIntList = LightBusiness.GetParagraphLightIntListList(tutorialLightList);
                if (oldTutorialParagraphLightIntList.Count == 0)
					tutorialParagraphLightIntList = null;
                StartTutorial();
            }
        }
        public void StartTutorial() {
            if (tutorialParagraphLightIntList == null)
                tutorialParagraphLightIntList = new List<List<int>>();
            tutorialPosition = 0;
            tutorialParagraphLightIntList.Clear();
            foreach (var item in oldTutorialParagraphLightIntList)
            {
                List<int> ints = new List<int>();
                for (int i = 0; i < item.Count; i++) {
                    ints.Add(item[i]);
                }
                tutorialParagraphLightIntList.Add(ints);
            }

            ip.OpenLight();
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

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mw.RemoveChildren();
        }





   
    }


}
