using Maker.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Maker.Business.MidiDeviceBusiness;

namespace Maker.View
{
    public partial class PlayUserControl
    {
        
    

    
    }
    public class InputPort
    {
        private NativeMethods.MidiOutProc midiInProc;
        private IntPtr handle;


        public InputPort()
        {
            midiInProc = new NativeMethods.MidiOutProc(MidiProc);
            handle = IntPtr.Zero;
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
            return NativeMethods.midiOutOpen(
                out handle,   //HMIDIIN
                id,			  //id
                midiInProc,   //CallBack
                IntPtr.Zero,  //CallBack Instance
                NativeMethods.CALLBACK_FUNCTION)  //flag
                    == NativeMethods.MMSYSERR_NOERROR;
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


            //if (wMsg == 963)
            //{
            //    dwParam1 = dwParam1 & 0xFFFF;
            //    uint h_dw1 = 0;
            //    uint l_dw1 = 0;
            //    h_dw1 = dwParam1 & 0xFF;
            //    l_dw1 = (dwParam1 >> 8) & 0xFF;




            //    Console.WriteLine(Convert.ToString(wMsg, 16));
            //    Console.WriteLine(Convert.ToString(h_dw1, 16));
            //    Console.WriteLine(Convert.ToString(l_dw1, 16));
            //    Console.WriteLine(Convert.ToString(dwParam2, 16));
            //    Console.WriteLine("-------------------------------");
            //}
            //else
            //{
            //    Console.WriteLine(Convert.ToString(wMsg, 16));
            //    Console.WriteLine(Convert.ToString(dwParam1, 16));
            //    Console.WriteLine(Convert.ToString(dwParam2, 16));
            //    Console.WriteLine("-------------------------------");
            //}
        }


        public static unsafe void Hello()
        {
            Console.WriteLine("Hello");
            Console.WriteLine("devices-sum:{0}", InputPort.InputCount);
            //ip.Open(1);

            for (int j = 0; j < MidiDeviceBusiness.midiOutGetNumDevs(); j++)
            {
                MIDIOUTCAPS caps = new MIDIOUTCAPS();
                MidiDeviceBusiness.midiOutGetDevCaps(new UIntPtr(new IntPtr(j).ToPointer()), ref caps, Convert.ToUInt32(System.Runtime.InteropServices.Marshal.SizeOf(typeof(MIDIOUTCAPS)) * 8));
                MidiDeviceBusiness.midiOutOpen(out IntPtr mOut, (uint)j, (IntPtr)0, (IntPtr)0, 0);
                Console.WriteLine(mOut);
                Console.WriteLine(caps.szPname);
                Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(mOut, (uint)(0x90 + (61 * 0x100) + (60 * 0x10000) + 0)));
                Thread.Sleep(1000);
                Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(mOut, (uint)(0x90 + (61 * 0x100) + (60 * 0x10000) + 1)));
                Thread.Sleep(1000);
                Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(mOut, (uint)(0x90 + (61 * 0x100) + (60 * 0x10000) + 2)));
                Thread.Sleep(1000);
                Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(mOut, (uint)(0x90 + (61 * 0x100) + (60 * 0x10000) + 3)));
                Thread.Sleep(1000);
                Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(mOut, (uint)(0x90 + (61 * 0x100) + (60 * 0x10000) + 4)));
                Thread.Sleep(1000);
                Console.WriteLine(MidiDeviceBusiness.midiOutShortMsg(mOut, (uint)(0x90 + (61 * 0x100) + (60 * 0x10000) + 5)));
            }


            //ip.Start();
            //try
            //{
            //    //while (true)
            //    //{
            //    //    Thread.Sleep(1);
            //    //}
            //}
            //catch (Exception e)
            //{


            //}
            //finally
            //{
            //    ip.Stop();
            //    ip.Close();
            //    Console.WriteLine("Bye~");
            //}



        }
    }
    internal static class NativeMethods
    {
        internal const int MMSYSERR_NOERROR = 0;
        internal const int CALLBACK_FUNCTION = 0x00030000;


        internal delegate void MidiOutProc(
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
        internal static extern int midiOutOpen(
            out IntPtr lphMidiIn,
            int uDeviceID,
            MidiOutProc dwCallback,
            IntPtr dwCallbackInstance,
            int dwFlags);


        [DllImport("winmm.dll")]
        internal static extern int midiInStart(
            IntPtr hMidiIn);


        [DllImport("winmm.dll")]
        internal static extern int midiInStop(
            IntPtr hMidiIn);
    }
}
