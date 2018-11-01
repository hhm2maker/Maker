using Maker.Business;
using Maker.Business.Model;
using Maker.Model;
using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Maker.View
{
    /// <summary>
    /// PlayUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class PlayUserControl : UserControl
    {
        private static NativeMethods.MidiInProc midiInProc;
        private NewMainWindow mw;
        public PlayUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            LoadKeyboards();
        }

        private void LoadKeyboards()
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
                if(keyboardModel.Position != -1 && !keyboardModels.ContainsKey(keyboardModel.Position))
                  keyboardModels.Add(keyboardModel.Position, keyboardModel);
            }
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
            XDocument _doc = XDocument.Load(mw.lastProjectPath + @"\" + d.Name + ".makerpl");
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
        public unsafe void StartMidiIn()
        {
            //直接使用Thread类，以及其方法 
            //Thread threadA = new Thread();
            //threadA.Start();
            cbRealDeviceIn.Items.Clear();
            for (int j = 0; j < MidiDeviceBusiness.midiInGetNumDevs(); j++)
            {
                MidiDeviceBusiness.MIDIINCAPS caps = new MidiDeviceBusiness.MIDIINCAPS();
                MidiDeviceBusiness.midiInGetDevCaps(new UIntPtr(new IntPtr(j).ToPointer()), ref caps, Convert.ToUInt32(Marshal.SizeOf(typeof(MidiDeviceBusiness.MIDIINCAPS))));
                //midiOutOpen(out IntPtr mOut, (uint)j, (IntPtr)0, (IntPtr)0, 0);
                //Console.WriteLine(caps.szPname + "----");
                cbRealDeviceIn.Items.Add(caps.szPname);
            }
            if (cbRealDeviceIn.Items.Count > 0) {
                cbRealDeviceIn.SelectedIndex = 0;
            }
        }
        InputPort ip;
        public void Check()
        {
            if (ip != null)
            {
                ip.Stop();
                ip.Close();
            }
            if (cbRealDeviceIn.SelectedIndex != -1)
            {
                //Console.WriteLine("Hello");
                ip = new InputPort(keyboardModels,inputType,canOpenOrClose);
                //Console.WriteLine("devices-sum:{0}", InputPort.InputCount);
                ip.Open(cbRealDeviceIn.SelectedIndex);
                ip.Start();
            }
            //Console.WriteLine("Bye~");
        }
        public class InputPort
        {
            private IntPtr handle;
            private CDD dd = new CDD();
            private  Dictionary<int, KeyboardModel> keyboardModels ;
            public InputPort(Dictionary<int, KeyboardModel> keyboardModels ,int inputType,int canOpenOrClose)
            {
                midiInProc = new NativeMethods.MidiInProc(MidiProc);
                handle = IntPtr.Zero;
                this.keyboardModels = keyboardModels;
                this.inputType = inputType;
                this.canOpenOrClose = canOpenOrClose;
                button1_Click();
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
                  
                    if (!pages[nowPageName].ContainsKey((int)position))
                        return;
                    if (dwParam1 > 16383)
                    {
                        KeyEvent((int)position,0);
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
                        KeyEvent((int)position, 1);
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
            public int canOpenOrClose = 0;//随着LPD按下抬起来开关(仅限深度模式) 0浅度 1深度(驱动)
            private void KeyEvent(int position,int openOrClose)
            {
                //模拟键盘输入
                if (!keyboardModels.ContainsKey(position)){
                    return;
                }
                if (inputType == 0)
                {
                    System.Windows.Forms.SendKeys.SendWait("{"+ keyboardModels[position].SendKey+ "}");
                }
                else if (inputType == 1)
                {
                    if (canOpenOrClose == 0)
                    {
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
                    else {
                        int ddcode = keyboardModels[position].DdKey;                         
                        dd.key(ddcode, 1);
                        dd.key(ddcode, 2);                           
                    }
                }
            }

            private void button1_Click()
            {
                //可从注册表中直接获取
                //string dllfile = ReadDataFromReg();

                //LoadDllFile(dllfile);
                //return;

                LoadDllFile(AppDomain.CurrentDomain.BaseDirectory + @"\DD85590.64.dll");
            }


            private void LoadDllFile(string dllfile)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(dllfile);
                if (!fi.Exists)
                {
                    MessageBox.Show("文件不存在");
                    return;
                }

                int ret = dd.Load(dllfile);
                if (ret == -2) { MessageBox.Show("装载库时发生错误"); return; }
                if (ret == -1) { MessageBox.Show("取函数地址时发生错误"); return; }
                if (ret == 0) { MessageBox.Show("非增强模块"); }

                return;
            }
            //[DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
            //public static extern void keybd_event(System.Windows.Forms.Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
            //private void button1_Click()
            //{
            //    keybd_event(System.Windows.Forms.Keys.Q, 0, 0, 0);
            //}
        }

        //private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    unreg_hotkey();
        //}


        private Dictionary<int,KeyboardModel> keyboardModels = new Dictionary<int,KeyboardModel>();

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

        private bool isFirst = true;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (isFirst) {
                InitData();
                isFirst = false;
            }
        }
        private void InitData()
        {
            for (int i = 1; i <= 16; i++)
            {
                cbPassageway.Items.Add("ch." + i);
            }
            cbPassageway.SelectedIndex = 0;
        }

        private static IntPtr nowOutDeviceIntPtr = (IntPtr)(-1);
        private bool isSearchChangeSelect = false;
        private void btnSearchEquipment_Click(object sender, RoutedEventArgs e)
        {
            isSearchChangeSelect = true;
            CloseMidiOut();
            cbRealDevice.Items.Clear();
            for (int j = 0; j < MidiDeviceBusiness.midiOutGetNumDevs(); j++)
            {
                MidiDeviceBusiness.MIDIOUTCAPS caps = new MidiDeviceBusiness.MIDIOUTCAPS();
                MidiDeviceBusiness.midiOutGetDevCaps(new UIntPtr((uint)j), ref caps, Convert.ToUInt32(Marshal.SizeOf(typeof(MidiDeviceBusiness.MIDIOUTCAPS))));
                //midiOutOpen(out IntPtr mOut, (uint)j, (IntPtr)0, (IntPtr)0, 0);
                String Pname = caps.szPname;
                if (cbOnlySearchForLaunchpad.IsChecked == true)
                {
                    if (Pname.Contains("Launchpad"))
                    {
                        cbRealDevice.Items.Add(Pname);
                        if (isFirst)
                        {
                            //MidiDeviceBusiness.midiOutOpen(out nowOutDeviceIntPtr, (uint)j, (IntPtr)0, (IntPtr)0, 0);
                            isFirst = false;
                        }
                    }
                }
                else
                {
                    cbRealDevice.Items.Add(Pname);
                    if (isFirst)
                    {
                        //MidiDeviceBusiness.midiOutOpen(out nowOutDeviceIntPtr, (uint)j, (IntPtr)0, (IntPtr)0, 0);
                        isFirst = false;
                    }
                }
            }
            if (cbRealDevice.Items.Count > 0)
            {
                //isSearchChangeSelect = true;
                cbRealDevice.SelectedIndex = 0;
            }
        }
        static int passageway = 0;
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
                            if (downLight[j].Position >= 28 && downLight[j].Position <= 35) {
                                //7f5bb5 7f - 颜色 - 128 5b - 位置 - 91 b5应该是
                                //顶部灯光
                                if (downLight[j].Action == 144)
                                {
                                    String str = "0x" + downLight[j].Color.ToString("X2").PadLeft(2, '0') + (downLight[j].Position + 63).ToString("X2").PadLeft(2, '0') + "b5";
                                    //Console.WriteLine(str);
                                    //Console.WriteLine(Convert.ToInt64(str, 16));
                                    MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                                }
                                else {
                                    String str = "0x" + (downLight[j].Position + 63).ToString("X2").PadLeft(2, '0') + "b5";
                                    MidiDeviceBusiness.midiOutShortMsg(nowOutDeviceIntPtr, (uint)(Convert.ToInt64(str, 16)));
                                }
                            }
                            else {
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
        private void CbRealDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isSearchChangeSelect)
            {
                isSearchChangeSelect = false;
                return;
            }
            CloseMidiOut();
            if (cbRealDevice.SelectedIndex == -1)
                return;
            MidiDeviceBusiness.midiOutOpen(out nowOutDeviceIntPtr, (uint)cbRealDevice.SelectedIndex, (IntPtr)0, (IntPtr)0, 0);
        }

        private void CloseMidiOut()
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
            if (ip != null) { 
            ip.Stop();
            ip.Close();
            }
        }

        private void cbPassageway_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            passageway = cbPassageway.SelectedIndex;
        }

        private void btnSearchEquipmentIn_Click(object sender, RoutedEventArgs e)
        {
            StartMidiIn();
        }

        private void cbRealDeviceIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Check();
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
            IntPtr instance = IntPtr.Zero;
            //Chk(WinMM.midiStreamOpen(out moHdl, ref moID, 1, null, instance, 0)); // open midi out in stream mode
            //Chk(WinMM.midiOutOpen(out moHdl, moID, null, (IntPtr)0, 0));// open midi out in stream mode
#endif
            //byte[] sx = { 0xF0, 0x7E, 0x7F, 0x09, 0x01, 0xF7 }; // GM On sysex
            //byte[] sx = { 0xF0, 0x00, 0x20, 0x29, 0x02, 0x18, 0x0E, 0x7F }; // GM On sysex
            byte[] sx = { 0xF0, 0x00, 0x20, 0x29, 0x02, 0x18, 0x0B, 0x0B,0x3f, 0x3f, 0x3f,0xF7 }; // GM On sysex
            //byte[] sx = { 240, 0, 32, 41, 2, 4, 20, 124, 1, 5, 72, 101, 108, 108, 111, 32, 2, 119, 111, 114, 108,100, 33, 247 }; // GM On sysex

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
      
            //Chk(WinMM.midiOutPrepareHeader(moHdl, nhdr, shdr)); // prepare native hdr
            //Chk(WinMM.midiOutLongMsg(moHdl, nhdr, shdr)); // send native message bytes
            Console.WriteLine(WinMM.midiOutPrepareHeader(moHdl, nhdr, shdr));
            Console.WriteLine(WinMM.midiOutLongMsg(moHdl, nhdr, shdr));
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

        private int inputType = 0;
        private int canOpenOrClose = 0;
        private void cbIsDD_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == cbIsDD) {
                if (ip != null)
                    ip.inputType = 1;
                inputType = 1;
            }
            if (sender == cbCanOpenOrClose)
            {
                if (ip != null)
                    ip.canOpenOrClose = 1;
                canOpenOrClose = 1;
            }
        }

        private void cbIsDD_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender == cbIsDD)
            {
                if (ip != null)
                    ip.inputType = 0;
                inputType = 0;
            }
            if (sender == cbCanOpenOrClose)
            {
                if (ip != null)
                    ip.canOpenOrClose = 1;
                canOpenOrClose = 0;
            }
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
