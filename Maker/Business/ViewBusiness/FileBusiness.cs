using Maker.Model;
using Maker.Utils;
using Maker.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Maker.Business.Currency;
using Maker.Business.Model.Config;
using Maker.View.LightScriptUserControl;
using Maker.View;

namespace Maker.Business
{
    public class FileBusiness
    {
        private FileBusiness()
        {

        }

        private static FileBusiness _fileBusiness = null;
        public static FileBusiness CreateInstance()
        {
            if (_fileBusiness == null)
            {
                _fileBusiness = new FileBusiness();
            }
            return _fileBusiness;
        }
        /// <summary>
        /// 读取Midi文件内容
        /// </summary>
        /// <param name="filePath">Midi文件的内容</param>
        public List<Light> ReadMidiContent(List<int> mData) {
            List<Light> mActionBeanList = new List<Light>();
            //当前记录什么位置
            int nowRecordPosition = 3;
            List<int> listTime = new List<int>();
            Light ab = new Light();
            for (int i = 0; i < mData.Count ; i++)
            {
                //记录时间
                if (nowRecordPosition == 3)
                {
                    //添加进时间数组
                    listTime.Add(mData[i]);
                    //如果最高位为0,结束读取时间差
                    if (mData[i] < 128)
                    {
                        int iTimeAll = 0;
                        for (int x = listTime.Count - 1; x >= 0; x--)
                        {
                            if (listTime[listTime.Count - x - 1] >= 128)
                            {
                                listTime[listTime.Count - x - 1] -= 128;
                            }
                            //Console.WriteLine((int)Math.Pow(128, x) + "---" + listTime[listTime.Count - x - 1]);
                            iTimeAll += (int)Math.Pow(127, x) * listTime[listTime.Count - x - 1];
                        }
                        ab = new Light();
                        ab.Time = iTimeAll;
                        listTime.Clear();
                        nowRecordPosition = 0;
                        continue;
                    }
                }
                if (nowRecordPosition == 0)
                {
                    ab.Action = mData[i];
                    nowRecordPosition = 1;
                    continue;
                }
                if (nowRecordPosition == 1)
                {
                    ab.Position = mData[i];
                    nowRecordPosition = 2;
                    continue;
                }
                if (nowRecordPosition == 2)
                {
                    ab.Color = mData[i];
                    nowRecordPosition = 3;
                    mActionBeanList.Add(ab);
                    continue;
                }
            }
            return mActionBeanList;
        }

        /// <summary>
        /// 读取Midi文件
        /// </summary>
        /// <param name="filePath">Midi文件的路径</param>
        public List<Light> ReadMidiFile(String filePath)
        {
            if (!File.Exists(filePath))
            {
                if (filePath.EndsWith(".mid"))
                {
                    filePath += "i";
                    if (!File.Exists(filePath))
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            List<Light> mActionBeanList = new List<Light>();//存放AB的集合
            List<int> mData = new List<int>();//文件字符集合
            List<String> mAction = new List<String>();
            //获取文件里所有的字节
            using (FileStream f = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                int i = 0;
                while ((i = f.ReadByte()) != -1)
                {
                    mData.Add(i);
                    //Console.WriteLine((int)i);

                    //Console.WriteLine(i.ToString("0X"));
                    //Console.WriteLine(string.Format("{0:X02}", i));
                }
            }
            List<int> listMainData = new List<int>();
            //剔除头部
            int iFilterHead = 22;
            while (true)
            {
                if (mData[iFilterHead] == 0 && mData[iFilterHead + 1] == 255)
                {
                    //Console.WriteLine("一个头信息");
                    iFilterHead += mData[iFilterHead + 3];
                    iFilterHead += 3;
                    iFilterHead++;
                }
                else
                {
                    break;
                }
            }
            //剔除尾部
            int iFilterFoot = 4;
            mData.RemoveRange(0, iFilterHead);
            mData.RemoveRange(mData.Count - iFilterFoot, iFilterFoot);
            mActionBeanList = ReadMidiContent(mData);
            //格式化时间
            int time = 0;
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

            for (int l = 0; l < mActionBeanList.Count; l++)
            {
                mActionBeanList[l].Position -= 28;
            }


            //CreateInstance().ReplaceControl(mActionBeanList, normalArr);
            return mActionBeanList;
        }


        /// <summary>
        /// 读取Midi文件
        /// </summary>
        /// <param name="filePath">Midi文件的路径</param>
        public List<Light> ReadMidiFileNoFormatTime(String filePath)
        {
            if (!File.Exists(filePath))
            {
                if (filePath.EndsWith(".mid"))
                {
                    filePath += "i";
                    if (!File.Exists(filePath))
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            List<Light> mActionBeanList = new List<Light>();//存放AB的集合
            List<int> mData = new List<int>();//文件字符集合
            List<String> mAction = new List<String>();
            //获取文件里所有的字节
            using (FileStream f = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                int i = 0;
                while ((i = f.ReadByte()) != -1)
                {
                    mData.Add(i);
                }
            }
            List<int> listMainData = new List<int>();
            //剔除头部
            int iFilterHead = 22;
            while (true)
            {
                if (mData[iFilterHead] == 0 && mData[iFilterHead + 1] == 255)
                {
                    //Console.WriteLine("一个头信息");
                    iFilterHead += mData[iFilterHead + 3];
                    iFilterHead += 3;
                    iFilterHead++;
                }
                else
                {
                    break;
                }
            }
            //剔除尾部
            int iFilterFoot = 4;
            mData.RemoveRange(0, iFilterHead);
            mData.RemoveRange(mData.Count - iFilterFoot, iFilterFoot);
            mActionBeanList = ReadMidiContent(mData);
            //不格式化时间
            //int time = 0;
            //for (int l = 0; l < mActionBeanList.Count; l++)
            //{
            //    if (mActionBeanList[l].Time == 0)
            //    {
            //        mActionBeanList[l].Time = time;
            //    }
            //    else
            //    {
            //        time += mActionBeanList[l].Time;
            //        mActionBeanList[l].Time = time;
            //    }
            //}

            for (int l = 0; l < mActionBeanList.Count; l++)
            {
                mActionBeanList[l].Position -= 28;
            }
            CreateInstance().ReplaceControl(mActionBeanList, normalArr);
            return mActionBeanList;
        }
        /// <summary>
        /// 写入Midi文件内容
        /// </summary>
        /// <param name="filePath">Midi文件的内容</param>
        public String WriteMidiContent(List<Light> lab)
        {
            ReplaceControl(lab, midiArr);
            lab = LightBusiness.Sort(lab);

            //还原时间
            int NowTime = 0;
            int jianTime = 0;
            for (int j = 0; j < lab.Count; j++)
            {
                if (lab[j].Time != NowTime)
                {
                    NowTime = lab[j].Time;
                    lab[j].Time -= jianTime;
                    jianTime = NowTime;
                }
                else
                {
                    lab[j].Time -= jianTime;
                }
            }
            StringBuilder Action = new StringBuilder();
            //144 128
            for (int j = 0; j < lab.Count; j++)
            {
                List<int> lI = GetListTime(lab[j].Time);

                for (int x = 0; x < lI.Count; x++)
                {
                    if (x != lI.Count - 1)
                    {
                        Action.Append((char)(lI[x] + 128));
                    }
                    else
                    {
                        Action.Append((char)(lI[x]));
                    }
                }
                //Action.Append((char)lab[j].Time);
                Action.Append((char)lab[j].Action);
                Action.Append((char)lab[j].Position);
                Action.Append((char)lab[j].Color);
            }
            return Action.ToString();
        }

        
        /// <summary>
        /// 写入Midi文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mActionBeanList"></param>
        public void WriteMidiFile(String filePath, String fileName,List<Light> mActionBeanList, bool isWriteToFile)
        {
            fileName = "";
            List<char> StartStr = new List<char>
            {
                (char)77,
                (char)84,
                (char)104,
                (char)100,
                (char)0,
                (char)0,
                (char)0,
                (char)6,
                (char)0,
                (char)0,
                (char)0,
                (char)1,
                (char)0,
                (char)60, //BPM
                (char)77,
                (char)84,
                (char)114,
                (char)107,
                (char)0,
                (char)0,
                (char)0,
                //(char)40 // 带 音轨速度
                (char)33
            };//文件头
              //StartStr.Add((char)0);
              //StartStr.Add((char)255);
              //StartStr.Add((char)3);
              //StartStr.Add((char)1);
              //StartStr.Add((char)0);

            //StartStr.Add((char)0); //00 FF 51 - 音轨速度
            //StartStr.Add((char)255);
            //StartStr.Add((char)81);
            //StartStr.Add((char)3);
            //StartStr.Add((char)Convert.ToInt64("a", 16));
            //StartStr.Add((char)Convert.ToInt64("2c", 16));
            //StartStr.Add((char)Convert.ToInt64("2a", 16));
          

            if (isWriteToFile)
            {
                StartStr.Add((char)0); //00 ff 03 - 音轨名称
                StartStr.Add((char)255);
                StartStr.Add((char)3);
                if (Encoding.Default.BodyName.ToLower().Equals("gb2312") || Encoding.Default.BodyName.ToLower().Equals("big5"))
                {
                    StartStr.Add((char)(fileName.Length * 2));// 汉字编码2个字节
                }
                else {
                    StartStr.Add((char)fileName.Length);
                }
            }
            List<char> StartStr2 = new List<char>
            {
                (char)0,
                (char)255,
                (char)88,
                (char)4,
                (char)4,
                (char)2,
                (char)36,
                (char)8,
                (char)0,
                (char)255,
                (char)88,
                (char)4,
                (char)4,
                (char)2,
                (char)36,
                (char)8
            };//文件头

            List<char> EndStr = new List<char>
            {
                (char)0,
                (char)255,
                (char)47,
                (char)0
            };//文件尾

            String Action = WriteMidiContent(LightBusiness.Copy(mActionBeanList));
            //18 - 21 位置的数字可以由两种方法得出 20 + 动作次数（开始结束都算）* 4 或是 文件大小 - 22
            String Zero = "";
            String Size;
            if (isWriteToFile)
            {
                if (Encoding.Default.BodyName.ToLower().Equals("gb2312") || Encoding.Default.BodyName.ToLower().Equals("big5"))
                {
                    Size = Convert.ToString(20 + fileName.Length * 2 + mActionBeanList.Count * 4, 2);
                }
                else
                {
                    Size = Convert.ToString(20 + fileName.Length + mActionBeanList.Count * 4, 2);
                }
            }
            else
            { 
                Size = Convert.ToString(20 + mActionBeanList.Count * 4, 2);
            }
            for (int j = 0; j < 32 - Size.Length; j++)
            {
                Zero += '0';
            }
            Size = Zero + Size;
            String one = Size.Substring(0, 8);
            String two = Size.Substring(8, 8);
            String three = Size.Substring(16, 8);
            String four = Size.Substring(24, 8);
            StartStr[18] = (char)Convert.ToInt32(one, 2);
            StartStr[19] = (char)Convert.ToInt32(two, 2);
            StartStr[20] = (char)Convert.ToInt32(three, 2);
            StartStr[21] = (char)Convert.ToInt32(four, 2);
            StringBuilder line = new StringBuilder();
            for (int j = 0; j < StartStr.Count; j++)
            {
                line.Append(StartStr[j]);
            }
            //获得文件路径
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            FileStream f = new FileStream(filePath, FileMode.OpenOrCreate);
            for (int j = 0; j < line.Length; j++)
            {
                f.WriteByte((byte)line[j]);
            }
            line.Clear();

            //gb2312
            Byte[] bytess = Encoding.Default.GetBytes(fileName);
            foreach (Byte b in bytess)
            {
                f.WriteByte(b);
            }

            for (int j = 0; j < StartStr2.Count; j++)
            {
                line.Append(StartStr2[j]);
            }

            line.Append(Action);

            for (int j = 0; j < EndStr.Count; j++)
            {
                line.Append(EndStr[j]);
            }

            for (int j = 0; j < line.Length; j++)
            {
                f.WriteByte((byte)line[j]);
            }
            f.Close();
        }
        /// <summary>
        /// 写入Midi文件 非ANSI
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mActionBeanList"></param>
        public void WriteMidiFile(String filePath, List<Light> mActionBeanList)
        {
            List<Light> lab = new List<Light>();
            for (int j = 0; j < mActionBeanList.Count; j++)
            {
                lab.Add(new Light(mActionBeanList[j].Time, mActionBeanList[j].Action, mActionBeanList[j].Position, mActionBeanList[j].Color));
            }
            lab = LightBusiness.Sort(lab);
            //还原时间
            int NowTime = 0;
            int jianTime = 0;
            for (int j = 0; j < lab.Count; j++)
            {
                if (lab[j].Time != NowTime)
                {
                    NowTime = lab[j].Time;
                    lab[j].Time -= jianTime;
                    jianTime = NowTime;
                }
                else
                {
                    lab[j].Time -= jianTime;
                }
            }
            List<char> mData = new List<char>();//文件字符
            List<char> StartStr = new List<char>();//文件头
            List<char> EndStr = new List<char>();//文件尾

            StartStr.Add((char)77);
            StartStr.Add((char)84);
            StartStr.Add((char)104);
            StartStr.Add((char)100);
            StartStr.Add((char)0);
            StartStr.Add((char)0);
            StartStr.Add((char)0);
            StartStr.Add((char)6);
            StartStr.Add((char)0);
            StartStr.Add((char)0);
            StartStr.Add((char)0);
            StartStr.Add((char)1);
            StartStr.Add((char)0);
            StartStr.Add((char)90);

            StartStr.Add((char)77);
            StartStr.Add((char)84);
            StartStr.Add((char)114);
            StartStr.Add((char)107);
            StartStr.Add((char)0);
            StartStr.Add((char)0);
            StartStr.Add((char)0);
            StartStr.Add((char)33);
            StartStr.Add((char)0);
            StartStr.Add((char)255);
            StartStr.Add((char)88);
            StartStr.Add((char)4);
            StartStr.Add((char)4);
            StartStr.Add((char)2);
            StartStr.Add((char)36);
            StartStr.Add((char)8);
            StartStr.Add((char)0);
            StartStr.Add((char)255);
            StartStr.Add((char)88);
            StartStr.Add((char)4);
            StartStr.Add((char)4);
            StartStr.Add((char)2);
            StartStr.Add((char)36);
            StartStr.Add((char)8);

            EndStr.Add((char)0);
            EndStr.Add((char)255);
            EndStr.Add((char)47);
            EndStr.Add((char)0);

            StringBuilder Action = new StringBuilder();
            //144 128
            for (int j = 0; j < lab.Count; j++)
            {
                List<int> lI = GetListTime(lab[j].Time);

                for (int x = 0; x < lI.Count; x++)
                {
                    if (x != lI.Count - 1)
                    {
                        Action.Append((char)(lI[x] + 128));
                    }
                    else
                    {
                        Action.Append((char)(lI[x]));
                    }
                }
                //Action.Append((char)lab[j].Time);
                Action.Append((char)lab[j].Action);
                Action.Append((char)lab[j].Position);
                Action.Append((char)lab[j].Color);
            }
            //18 - 21 位置的数字可以由两种方法得出 20 + 动作次数（开始结束都算）* 4 或是 文件大小 - 22
            String Zero = "";
            String Size;
            Size = System.Convert.ToString(20 + mActionBeanList.Count * 4, 2);
            for (int j = 0; j < 32 - Size.Length; j++)
            {
                Zero += '0';
            }
            Size = Zero + Size;
            String one = Size.Substring(0, 8);
            String two = Size.Substring(8, 8);
            String three = Size.Substring(16, 8);
            String four = Size.Substring(24, 8);
            StartStr[18] = (char)System.Convert.ToInt32(one, 2);
            StartStr[19] = (char)System.Convert.ToInt32(two, 2);
            StartStr[20] = (char)System.Convert.ToInt32(three, 2);
            StartStr[21] = (char)System.Convert.ToInt32(four, 2);
            StringBuilder line = new StringBuilder();
            for (int j = 0; j < StartStr.Count; j++)
            {
                line.Append(StartStr[j]);
            }
            line.Append(Action.ToString());
            for (int j = 0; j < EndStr.Count; j++)
            {
                line.Append(EndStr[j]);
            }
            //获得文件路径
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            FileStream f = new FileStream(filePath, FileMode.OpenOrCreate);
            for (int j = 0; j < line.Length; j++)
            {
                //Console.WriteLine((int)line[j]);
                f.WriteByte((byte)line[j]);
            }
            f.Close();
        }
        public List<int> GetListTime(int num)
        {
            List<int> listTime = new List<int>();
            int count = 0;
            while (num > Math.Pow(127, count + 1))
            {
                count++;
            }
            int nowNumber = num;

            for (int i = count; i >= 0; i--)
            {
                //Console.WriteLine(nowNumber / Math.Pow(128, i));
                listTime.Add((int)(nowNumber / Math.Pow(127, i)));
                nowNumber -= (int)((int)(nowNumber / Math.Pow(127, i)) * Math.Pow(127, i));
            }
            return listTime;
        }
        /// <summary>
        /// 复制文件夹中的所有内容
        /// </summary>
        /// <param name="sourceDirPath">源文件夹目录</param>
        /// <param name="saveDirPath">指定文件夹目录</param>
        public void CopyDirectory(string sourceDirPath, string saveDirPath)
        {
            try
            {
                if (!Directory.Exists(saveDirPath))
                {
                    Directory.CreateDirectory(saveDirPath);
                }
                string[] files = Directory.GetFiles(sourceDirPath);
                foreach (string file in files)
                {
                    string pFilePath = saveDirPath + "\\" + Path.GetFileName(file);
                    if (File.Exists(pFilePath))
                        continue;
                    File.Copy(file, pFilePath, true);
                }

                string[] dirs = Directory.GetDirectories(sourceDirPath);
                foreach (string dir in dirs)
                {
                    CopyDirectory(dir, saveDirPath + "\\" + Path.GetFileName(dir));
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 读取Light文件
        /// </summary>
        /// <param name="filePath">Light文件的路径</param>
        public List<Light> ReadLightFile(String filePath)
        {
            List<Light> mActionBeanList = new List<Light>();//存放AB的集合
            List<int> mData = new List<int>();//文件字符集合
            List<String> mAction = new List<String>();
            //获取文件里所有的字节
            using (FileStream f = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                int i = 0;
                while ((i = f.ReadByte()) != -1)
                {
                    mData.Add(i);
                }
            }
            mActionBeanList = ReadMidiContent(mData);

            //格式化时间
            int time = 0;
            for (int l = 0; l < mActionBeanList.Count; l++)
            {
                mActionBeanList[l].Position -= 28;
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

            ReplaceControl(mActionBeanList, normalArr);
            return mActionBeanList;
        }

        public List<Light> LightStringToLightList(String lightString) {
            List<Light> lightList = new List<Light>();
            if (lightString.Length != 0)
            {
                string[] strs = lightString.Split(';');
                foreach (string str in strs)
                {
                    string[] strss = str.Split(',');
                    if (strss.Length == 4)
                    {
                        lightList.Add(new Light(int.Parse(strss[0]), int.Parse(strss[1]), int.Parse(strss[2]), int.Parse(strss[3])));
                    }
                }
            }
            return lightList;
        }
        /// <summary>
        /// 写入Light灯光文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="mActionBeanList">需要写入的灯光数组</param>
        public void WriteLightFile(String filePath, List<Light> mActionBeanList)
        {
            //StringBuilder sb = new StringBuilder();
            //for (int j = 0; j < mActionBeanList.Count; j++)
            //{
            //    sb.Append(mActionBeanList[j].Time + "," + mActionBeanList[j].Action + "," + mActionBeanList[j].Position + "," + mActionBeanList[j].Color + ";");
            //    if (j != mActionBeanList.Count - 1)
            //    {
            //        sb.Append(Environment.NewLine);
            //    }
            //}
            File.Delete(filePath);
            String str = WriteMidiContent(mActionBeanList);
            if (str.Equals(String.Empty))
            {
                File.Create(filePath).Close();
            }
            else {
                FileStream f = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                for (int j = 0; j < str.Length; j++)
                {
                    f.WriteByte((byte)str[j]);
                }
                f.Close();
            }
        }

        /// <summary>
        /// 写入Unipad Light灯光文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="mActionBeanList">需要写入的灯光数组</param>
        public void WriteUnipadLightFile(String filePath,int bpm , List<Light> mActionBeanList)
        {
            File.Delete(filePath);
            mActionBeanList = LightBusiness.Sort(mActionBeanList);
            //还原时间
            int NowTime = 0;
            int jianTime = 0;
            for (int j = 0; j < mActionBeanList.Count; j++)
            {
                if (mActionBeanList[j].Time != NowTime)
                {
                    NowTime = mActionBeanList[j].Time;
                    mActionBeanList[j].Time -= jianTime;
                    jianTime = NowTime;
                }
                else
                {
                    mActionBeanList[j].Time -= jianTime;
                }
            }
           
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < mActionBeanList.Count; i++) {
                if (mActionBeanList[i].Time != 0) {
                    //d 1800
                    //Console.WriteLine(mActionBeanList[i].Time+"---"+(int)(mActionBeanList[i].Time / (bpm * 1.0) * 1000));
                    sb.Append("d "+ (int)(mActionBeanList[i].Time / (bpm * 1.0)  * 1000) + Environment.NewLine);
                }
                if (mActionBeanList[i].Action == 144)
                {
                    //o 1 1 a 3
                    sb.Append("o " + mActionBeanList[i].Position % 10 + " " + mActionBeanList[i].Position / 10 + " a " + mActionBeanList[i].Color + Environment.NewLine);
                }
                else {
                    //f 1 1
                    sb.Append("f " + mActionBeanList[i].Position % 10 + " " + mActionBeanList[i].Position / 10 + Environment.NewLine);
                }
            }

            if (sb.ToString().Equals(String.Empty))
            {
                File.Create(filePath).Close();
            }
            else
            {
                FileStream f = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                for (int j = 0; j < sb.Length; j++)
                {
                    f.WriteByte((byte)sb[j]);
                }
                f.Close();
            }
        }

        List<String> strsUnipadLightPosition = new List<string>()
        {
            ""
        };


        /// <summary>
        /// 获得文件夹下所有符合条件的文件名
        /// </summary>
        /// <param name="directoryPath">文件夹路径</param>
        /// <param name="extension">文件扩展名 如.txt,.mid</param>
        /// <returns></returns>
        public List<String> GetFilesName(String directoryPath, List<String> extension)
        {
            List<String> filesName = new List<string>();
            if (!Directory.Exists(directoryPath))
                return new List<string>();
            //遍历文件 
            DirectoryInfo TheFolder = new DirectoryInfo(directoryPath);
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                int nIndex = NextFile.Name.LastIndexOf('.');
                String filename = NextFile.Name;
                if (nIndex >= 0)
                {
                    filename = filename.Substring(nIndex);
                    if (extension.Contains(filename))
                    {
                        filesName.Add(NextFile.Name);
                    }
                }
            }
            return filesName;
        }
     
        public List<String> GetDirectorysName(String directoryPath)
        {
            List<String> directorysName = new List<string>();
            if (!Directory.Exists(directoryPath))
                return new List<string>();
            //遍历文件 
            DirectoryInfo TheFolder = new DirectoryInfo(directoryPath);
            foreach (DirectoryInfo NextDirectory in TheFolder.GetDirectories())
            {
                directorysName.Add(NextDirectory.Name);
            }
            return directorysName;
        }
        /// <summary>
        /// 读取颜色文件
        /// </summary>
        /// <param name="filePath">颜色文件的路径</param>
        public List<SolidColorBrush> ReadColorFile(String filePath)
        {
            String colorPath;
            if (filePath.Equals(String.Empty))
            {
                colorPath = System.Windows.Forms.Application.StartupPath + "\\Color\\color.color";
            }
            else
            {
                colorPath = filePath;
            }
            ColorFileModel colorListModel = new ColorFileModel();
            XmlSerializerBusiness.Load(ref colorListModel, filePath);
            List<SolidColorBrush> brushList = new List<SolidColorBrush>();

            foreach (String str in colorListModel.Colors)
            {
                brushList.Add(new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString(str)));
            }
            return brushList;
        }
        /// <summary>
        /// 读取范围文件
        /// </summary>
        /// <param name="filePath">颜色文件的路径</param>
        public Dictionary<string, List<int>> ReadRangeFile(String filePath)
        {
            Dictionary<string, List<int>> rangeDictionary = new Dictionary<string, List<int>>();
            FileStream f;
            f = new FileStream(filePath, FileMode.OpenOrCreate);
            StringBuilder builder = new StringBuilder();
            int i = 0;
            while ((i = f.ReadByte()) != -1)
            {
                builder.Append((char)i);
            }
            f.Close();
            String text = builder.ToString();
            text = text.Replace("\n", "");
            text = text.Replace("\r", "");
            String[] texts = text.Split(';');
            foreach (String str in texts)
            {
                if (str.Trim().Equals(String.Empty))
                    continue;
                String[] strs = str.Split(':');
                if (strs.Count() != 2)
                {
                    return null;
                }
                String[] numbers = strs[1].Split(',');
                List<int> numberList = new List<int>();
                foreach (String s in numbers)
                {
                    if (s.Trim().Equals(String.Empty))
                        continue;
                    numberList.Add(int.Parse(s));
                }
                rangeDictionary.Add(strs[0], numberList);
            }
            return rangeDictionary;
        }
        /// <summary>
        /// 检查Save文件
        /// </summary>
        /// <param name="saveFilePath">Save文件路径</param>
        /// <returns></returns>
        public String CheckSaveFile(String saveFilePath)
        {
            if (!File.Exists(saveFilePath)) {
                return "文件不存在";
            }
            FileStream f;
            f = new FileStream(saveFilePath, FileMode.OpenOrCreate);
            int i = 0;
            Boolean jilu = false;
            List<String> midipathList = new List<String>();
            String jiluStr = "";
            //MIDIext/Shapeofyou/1.mid;
            while ((i = f.ReadByte()) != -1)
            {
                if (i == '.')
                {
                    jilu = true;
                }
                else if (i == ';')
                {
                    jilu = false;
                    if (!jiluStr.Equals(""))
                    {
                        midipathList.Add(jiluStr);
                    }
                    jiluStr = "";
                    continue;
                }
                if (jilu) jiluStr += (char)i;
                //Console.WriteLine(i);
            }
            f.Close();
            String path2 = saveFilePath.Substring(0, saveFilePath.LastIndexOf("\\"));
            path2 = path2.Substring(0, path2.LastIndexOf("\\"));
            //获取文件夹名
            String filepath = saveFilePath.Substring(0, saveFilePath.LastIndexOf("\\"));
            filepath = filepath.Substring(filepath.LastIndexOf("\\"), filepath.Length - filepath.LastIndexOf("\\"));
            filepath = filepath.Substring(1, filepath.Length - 1);
            filepath = '\\' + filepath + '\\';
            //Console.WriteLine(filepath);

            int Error = 0;
            String ErrorStr = "";
            int inOtherFile = 0;
            String inOtherFileStr = "";

            for (int x = 0; x < midipathList.Count; x++)
            {
                midipathList[x] = midipathList[x].Substring(1, midipathList[x].Length - 1);
                midipathList[x] = midipathList[x].Substring(8, midipathList[x].Length - 8);
                midipathList[x] = midipathList[x].Replace("/", "\\");

                String Finalpath = path2 + midipathList[x];
                //校验是否有这个文件
                if (!File.Exists(Finalpath))
                {
                    Error++;
                    ErrorStr += "未找到文件：" + Finalpath + "\n";
                }
                if (Finalpath.IndexOf(filepath) == -1)
                {
                    inOtherFile++;
                    inOtherFileStr += "其他处文件：" + Finalpath + "\n";
                }
            }
            ErrorStr = "共发现 " + Error + " 处错误" + "\n" + ErrorStr;
            ErrorStr = ErrorStr + "共含有 " + inOtherFile + " 处其他文件夹文件" + "\n" + inOtherFileStr;
            return ErrorStr;
        }

        /// <summary>
        /// base64  to  string
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public String Base2String(String base64) {
            byte[] outputb = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(outputb);
        }

        /// <summary>
        /// string  to  base64
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public String String2Base(String str)
        {
            System.Text.Encoding encode = System.Text.Encoding.UTF8;
            byte[] bytedata = encode.GetBytes(str);
            return Convert.ToBase64String(bytedata, 0, bytedata.Length);
        }
        /// <summary>
        /// 加载虚拟设备文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DeviceModel LoadDeviceModel(String filePath) {
            DeviceModel deviceModel = new DeviceModel();
            ConfigBusiness config = new ConfigBusiness(filePath);
            deviceModel.DeviceType = config.Get("DeviceType").Trim();
            String strBg = config.Get("DeviceBackGround").Trim();
            deviceModel.DeviceBackGroundStr = strBg;
            if (strBg[0] == '#' || strBg.Length == 7)
            {
                System.Windows.Media.Color c = System.Windows.Media.Color.FromArgb(255, (byte)Convert.ToInt32(strBg.Substring(1, 2), 16), (byte)Convert.ToInt32(strBg.Substring(3, 2), 16), (byte)Convert.ToInt32(strBg.Substring(5, 2), 16));
                deviceModel.DeviceBackGround = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, c.R, c.G, c.B));
            }
            else {
                deviceModel.DeviceBackGround = new ImageBrush(new BitmapImage(new Uri(strBg, UriKind.Absolute)));
            }
            if (Double.TryParse(config.Get("DeviceSize").Trim(), out Double dSize))
            {
                deviceModel.DeviceSize = dSize;
            }
            else {
                deviceModel.DeviceSize = 600;
            }
            if (config.Get("IsMembrane").Trim().Equals("true"))
            {
                deviceModel.IsMembrane = true;
            }
            else {
                deviceModel.IsMembrane = false;
            }
            return deviceModel;
        }

        /// <summary>
        /// 固定位置交换/替换
        /// </summary>
        public void ReplaceControl(List<Light> lights, List<int> arr)
        {
            for (int k = 0; k < lights.Count; k++)
            {
                lights[k].Position = arr[lights[k].Position];
            }
        }

        /// <summary>
        /// MIDI位置数组
        /// </summary>
        public List<int> midiArr = new List<int>()
            {
                0,116,117,118,119,120,121,122,123,0,
                115,36,37,38,39,68,69,70,71,107,
                114,40,41,42,43,72,73,74,75,106,
                113,44,45,46,47,76,77,78,79,105,
                112,48,49,50,51,80,81,82,83,104,
                111,52,53,54,55,84,85,86,87,103,
                110,56,57,58,59,88,89,90,91,102,
                109,60,61,62,63,92,93,94,95,101,
                108,64,65,66,67,96,97,98,99,100,
                0,28,29,30,31,32,33,34,35,0,
              };

        /// <summary>
        /// 普通位置数组
        /// </summary>
        public List<int> normalArr = new List<int>()
            {
                91,92,93,94,95,96,97,98,
                11,12,13,14,21,22,23,24,
                 31,32,33,34,41,42,43,44,
                 51,52,53,54,61,62,63,64,
                 71,72,73,74,81,82,83,84,
                 15,16,17,18,25,26,27,28,
                 35,36,37,38,45,46,47,48,
                 55,56,57,58,65,66,67,68,
                 75,76,77,78,85,86,87,88,
                 89,79,69,59,49,39,29,19,
                 80,70,60,50,40,30,20,10,
                 1,2,3,4,5,6,7,8
              };

        public bool CheckFile(String fileName)
        {
            BaseUserControl baseUserControl = null;
            if (fileName.EndsWith(".mid"))
            {
                baseUserControl = StaticConstant.mw.editUserControl.userControls[0];
            }
            else
            {
                for (int i = 0; i < StaticConstant.mw.editUserControl.userControls.Count; i++)
                {
                    if (fileName.EndsWith(StaticConstant.mw.editUserControl.userControls[i]._fileExtension))
                    {
                        baseUserControl = StaticConstant.mw.editUserControl.userControls[i];
                        break;
                    }
                }
            }
            if (baseUserControl == null)
            {
                return false;
            }
            if (File.Exists(StaticConstant.mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName))
            {
                return true;
            }
            return false;
        }
    }
}