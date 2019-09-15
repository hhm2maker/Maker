using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Utils
{
    class MediaFileTimeUtil
    {
        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);

        [DllImport("winmm.dll")]
        public static extern int mciSendString(string m_strCmd, StringBuilder m_strReceive, int m_v1, int m_v2);

        public static string GetAsfTime(string filePath)
        {
            StringBuilder shortpath = new StringBuilder(80);
            GetShortPathName(filePath, shortpath, shortpath.Capacity);
            string name = shortpath.ToString();
            StringBuilder buf = new StringBuilder(80);
            mciSendString("close all", buf, buf.Capacity, 0);
            mciSendString("open " + name + " alias media", buf, buf.Capacity, 0);
            mciSendString("status media length", buf, buf.Capacity, 0);
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)Convert.ToDouble(buf.ToString().Trim()));
            //return ts.ToString();
            return buf.ToString().Trim();
        }

        public static string GetAsfTime(string filePath, double dBpm)
        {
            return (long.Parse(GetAsfTime(filePath)) * dBpm / 1000).ToString();
        }
    }
}
