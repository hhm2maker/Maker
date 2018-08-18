using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Maker.Business
{
    class MidiDeviceBusiness
    {
        //检取描述指定媒介控制接口错误代码的字符串  
        [DllImport("winmm.dll", CharSet = CharSet.Ansi,
               BestFitMapping = false,
               ThrowOnUnmappableChar = true)]
        public static extern Int32 mciGetErrorString(Int32 errorCode, StringBuilder errorText, Int32 errorTextSize);

        //向指定的媒介控制接口设备发送一个字符串  
        [DllImport("winmm.dll", CharSet = CharSet.Ansi,
               BestFitMapping = false,
               ThrowOnUnmappableChar = true)]
        public static extern Int32 mciSendString(string command, string buffer, int bufferSize, IntPtr hwndCallback);

        //将指定的MIDI输入设备连接到输出设备  
        [DllImport("winmm.dll", CharSet = CharSet.Ansi,
               BestFitMapping = false,
               ThrowOnUnmappableChar = true)]
        public static extern UInt32 midiConnect(IntPtr hMidi, IntPtr hmo, IntPtr pReserved);

        //断开MIDI输入设备和输出设备的连接  
        [DllImport("winmm.dll", CharSet = CharSet.Ansi,
               BestFitMapping = false,
               ThrowOnUnmappableChar = true)]
        public static extern UInt32 midiDisconnect(IntPtr hMidi, IntPtr hmo, IntPtr pReserved);

        //关闭指定的音乐仪器数字接口的输入设备  
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern uint midiInClose(IntPtr hMidiIn);

        //查询指定的音乐仪器数字接口的输入设备，以确定其性能  
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern MMRESULT midiInGetDevCaps(UIntPtr uDeviceID, ref MIDIINCAPS caps, uint cbMidiInCaps);

        //查询指定的音乐仪器数字接口的输入设备，以确定其性能  
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern uint midiInGetNumDevs();

        //打开指定的音乐仪器数字接口的输入设备  
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern uint midiInOpen(IntPtr hMidiIn);

        //在给定的MIDI输入设备上输入，并将所有挂起的输入缓冲区标记为已执行的  
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern uint midiInReset(IntPtr hMidiIn);

        //启动在指定的音乐仪器数字接口的输入设备上的输入  
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern uint midiInStart(IntPtr hMidiIn);

        //关闭指定的音乐仪器数字接口的输出设备  
        [DllImport("winmm.dll")]
        public static extern uint midiOutClose(IntPtr hMidiOut);

        //查询指定的音乐仪器数字接口的输出设备，以确定其性能  
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern MMRESULT midiOutGetDevCaps(UIntPtr uDeviceID, ref MIDIOUTCAPS lpMidiOutCaps, uint cbMidiOutCaps);

        //检取有关MIDI输出设备指定采取的文本说明  
        [DllImport("winmm.dll")]
        public static extern uint midiOutGetErrorText(uint mmrError, StringBuilder pszText, uint cchText);

        //检取系统中存在的MIDI输出设备的数量  
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern uint midiOutGetNumDevs();

        //打开指定的MIDI输出设备进行回放  
        [DllImport("winmm.dll", CharSet = CharSet.Ansi,
               BestFitMapping = false,
               ThrowOnUnmappableChar = true)]
        public static extern uint midiOutOpen(out IntPtr lphMidiOut, uint uDeviceID, IntPtr dwCallback, IntPtr dwInstance, uint dwFlags);

        //向指定的MIDI输出设备发送一条短MIDI消息  
        [DllImport("winmm.dll", CharSet = CharSet.Ansi,
               BestFitMapping = false,
               ThrowOnUnmappableChar = true)]
        public static extern uint midiOutShortMsg(IntPtr hMidiOut, uint dwMsg);

        //关闭一个打开的MIDI流  
        [DllImport("winmm.dll", CharSet = CharSet.Ansi,
               BestFitMapping = false,
               ThrowOnUnmappableChar = true)]
        public extern static Int32 midiStreamClose(IntPtr hMidiStream);

        //为输出，打开一个MIDI流  
        [DllImport("winmm.dll", CharSet = CharSet.Ansi,
               BestFitMapping = false,
               ThrowOnUnmappableChar = true)]
        public extern static Int32 midiStreamOpen(ref IntPtr hMidiStream, ref Int32 puDeviceID, Int32 cMidi, IntPtr dwCallback, IntPtr dwInstance, Int32 fdwOpen);

        //暂停一个MIDI流的播放  
        [DllImport("winmm.dll", CharSet = CharSet.Ansi,
               BestFitMapping = false,
               ThrowOnUnmappableChar = true)]
        public static extern Int32 midiStreamPause(IntPtr hMidiStream);

        //关掉指定MIDI输出设备的所有MIDI通道  
        [DllImport("winmm.dll", CharSet = CharSet.Ansi,
               BestFitMapping = false,
               ThrowOnUnmappableChar = true)]
        public static extern Int32 midiStreamStop(IntPtr hMidiStream);

        //设置应用程序或驱动程序使用的最小定时器分辨率  
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern uint timeBeginPeriod(uint uMilliseconds);

        //清除应用程序或驱动程序使用的最小定时器分辨率  
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern uint timeEndPeriod(uint uMilliseconds);

        //查询定时器设备以确定其性能  
        //[DllImport("winmm.dll", SetLastError = true)]
        //public static extern UInt32 timeGetDevCaps(ref TimeCaps timeCaps, UInt32 sizeTimeCaps);

        //检取从WINDOWS开始已逝去的毫秒数  
        //[DllImport("winmm.dll", SetLastError = true)]
        //public static extern UInt32 timeGetSystemTime(ref MmTime mmTime, UInt32 sizeMmTime);

        //检取从WINDOWS开始已逝去的毫秒数，此函数比上一条函数开销小  
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern uint timeGetTime();

        //毁掉指定的定时器回调事件  
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern UInt32 timeKillEvent(UInt32 timerEventId);

        //设置一个定时器回调事件  
        //[DllImport("winmm.dll", SetLastError = true)]
        //public static extern UInt32 timeSetEvent(UInt32 msDelay, UInt32 msResolution, TimerEventHandler handler, ref UInt32 userCtx, UInt32 eventType);

        [DllImport("winmm.dll", SetLastError = true)]
        public static extern MMRESULT midiOutPrepareHeader(IntPtr hmo, MidiHdr lpMidiOutHdr, uint cbMidiOutHdr);

        public  enum MMRESULT : uint
        {
            MMSYSERR_NOERROR = 0,
            MMSYSERR_ERROR = 1,
            MMSYSERR_BADDEVICEID = 2,
            MMSYSERR_NOTENABLED = 3,
            MMSYSERR_ALLOCATED = 4,
            MMSYSERR_INVALHANDLE = 5,
            MMSYSERR_NODRIVER = 6,
            MMSYSERR_NOMEM = 7,
            MMSYSERR_NOTSUPPORTED = 8,
            MMSYSERR_BADERRNUM = 9,
            MMSYSERR_INVALFLAG = 10,
            MMSYSERR_INVALPARAM = 11,
            MMSYSERR_HANDLEBUSY = 12,
            MMSYSERR_INVALIDALIAS = 13,
            MMSYSERR_BADDB = 14,
            MMSYSERR_KEYNOTFOUND = 15,
            MMSYSERR_READERROR = 16,
            MMSYSERR_WRITEERROR = 17,
            MMSYSERR_DELETEERROR = 18,
            MMSYSERR_VALNOTFOUND = 19,
            MMSYSERR_NODRIVERCB = 20,
            WAVERR_BADFORMAT = 32,
            WAVERR_STILLPLAYING = 33,
            WAVERR_UNPREPARED = 34
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MIDIINCAPS
        {
            public ushort wMid;
            public ushort wPid;
            public uint vDriverVersion;     // MMVERSION
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;
            public uint dwSupport;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MIDIOUTCAPS
        {
            public ushort wMid;
            public ushort wPid;
            public uint vDriverVersion;     //MMVERSION
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;
            public ushort wTechnology;
            public ushort wVoices;
            public ushort wNotes;
            public ushort wChannelMask;
            public uint dwSupport;
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


        // values for wTechnology field of MIDIOUTCAPS structure
        private const ushort MOD_MIDIPORT = 1;     // output port
        private const ushort MOD_SYNTH = 2;        // generic internal synth
        private const ushort MOD_SQSYNTH = 3;      // square wave internal synth
        private const ushort MOD_FMSYNTH = 4;      // FM internal synth
        private const ushort MOD_MAPPER = 5;       // MIDI mapper
        private const ushort MOD_WAVETABLE = 6;    // hardware wavetable synth
        private const ushort MOD_SWSYNTH = 7;      // software synth

        // flags for dwSupport field of MIDIOUTCAPS structure
        private const uint MIDICAPS_VOLUME = 1;      // supports volume control
        private const uint MIDICAPS_LRVOLUME = 2;    // separate left-right volume control
        private const uint MIDICAPS_CACHE = 4;
        private const uint MIDICAPS_STREAM = 8;      // driver supports midiStreamOut directly
    }
}
