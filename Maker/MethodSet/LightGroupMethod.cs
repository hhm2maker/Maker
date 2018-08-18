using Maker.Business;
using Maker.Model;
using System;
using System.Collections.Generic;

namespace Maker.MethodSet
{
    public static class LightGroupMethod
    {
        /// <summary>
        /// 把整个灯光平移至某个时间格
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public static List<Light> SetStartTime(List<Light> lightGroup, int startTime) {
            List<Light> ll = LightBusiness.Copy(lightGroup);
            if (ll.Count == 0) {
                return ll;
            }
            ll = LightBusiness.Sort(ll);
            int addTime = startTime - ll[0].Time;
            for (int i = 0; i < ll.Count; i++) {
                ll[i].Time += addTime;
            }
            return ll;
        }
        /// <summary>
        /// 将所有的灯光对的持续时间控制在固定时间
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static List<Light> SetAllTime(List<Light> lightGroup, int time)
        {
            List<Light> ll = LightBusiness.Copy(lightGroup);
            ll = LightBusiness.Sort(ll);
                for (int i = 0; i < ll.Count; i++)
                {
                    //如果是开就去找关
                    if (ll[i].Action == 144)
                    {
                        for (int j = i + 1; j < ll.Count; j++)
                        {
                            if (ll[j].Action == 128 && ll[j].Position == ll[i].Position)
                            {
                                ll[j].Time = ll[i].Time + time ;
                                break;
                            }
                        }
                    }
                }
            return ll;
        }
        /// <summary>
        /// 获取颜色数组
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <returns></returns>
        public static List<int> GetColor(List<Light> lightGroup)
        {
            List<int> li = new List<int>();
            for (int i = 0; i < lightGroup.Count; i++)
            {
                if (!li.Contains(lightGroup[i].Color))
                {
                    li.Add(lightGroup[i].Color);
                }
            }
            return li;
        }
        /// <summary>
        /// 设置颜色(格式化)
        /// </summary>
        /// <param name="geshihua">新颜色集合</param>
        public static List<Light> SetColor(List<Light> lightGroup, List<int> geshihua)
        {
            List<Light> ll = LightBusiness.Copy(lightGroup);
            List<char> mColor = new List<char>();
            for (int j = 0; j < ll.Count; j++)
            {
                if (ll[j].Action == 144)
                {
                    if (!mColor.Contains((char)ll[j].Color))
                    {
                        mColor.Add((char)ll[j].Color);
                    }
                }
            }
            List<char> OldColorList = new List<char>();
            List<char> NewColorList = new List<char>();
            for (int i = 0; i < mColor.Count; i++)
            {
                OldColorList.Add(mColor[i]);
                NewColorList.Add(mColor[i]);
            }
            //获取一共有多少种老颜色
            int OldColorCount = mColor.Count;
            if (OldColorCount == 0)
            {
                return new List<Light>();
            }
            int chuCount = OldColorCount / geshihua.Count;
            int yuCount = OldColorCount % geshihua.Count;
            List<int> meigeyanseCount = new List<int>();//每个颜色含有的个数

            for (int i = 0; i < geshihua.Count; i++)
            {
                meigeyanseCount.Add(chuCount);
            }
            if (yuCount != 0)
            {
                for (int i = 0; i < yuCount; i++)
                {
                    meigeyanseCount[i]++;
                }
            }
            List<int> yansefanwei = new List<int>();
            for (int i = 0; i < geshihua.Count; i++)
            {
                int AllCount = 0;
                if (i != 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        AllCount += meigeyanseCount[j];
                    }
                }

                yansefanwei.Add(AllCount);
            }
            for (int i = 0; i < geshihua.Count; i++)
            {
                for (int j = yansefanwei[i]; j < yansefanwei[i] + meigeyanseCount[i]; j++)
                {
                    NewColorList[j] = (char)geshihua[i];
                }
            }
            //给原颜色排序
            OldColorList.Sort();
            for (int i = 0; i < ll.Count; i++)
            {
                for (int k = 0; k < ll.Count; k++)
                {
                    for (int l = 0; l < OldColorList.Count; l++)
                    {
                        if (ll[k].Action == 144 || ll[k].Action == 128)
                        {
                            if (ll[k].Color == OldColorList[l])
                            {
                                ll[k].Color = NewColorList[l];
                                break;
                            }
                        }
                    }
                }
            }
            return ll ;
        }
        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <param name="attribute"></param>
        /// <param name="mOperator">运算符：0等于，-1减，1加</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<Light> SetAttribute(List<Light> lightGroup, String attribute,int mOperator, int value)
        {
            List<Light> ll = LightBusiness.Copy(lightGroup);
            if (attribute.Equals("Time")) {
                if (mOperator == 0)
                {
                    foreach (Light l in ll)
                    {
                        l.Time = value;
                    }
                }
                else if (mOperator == -1)
                {
                    foreach (Light l in ll)
                    {
                        l.Time -= value;
                    }
                }
                else if (mOperator == 1)
                {
                    foreach (Light l in ll)
                    {
                        l.Time += value;
                    }
                }
            }
            else if(attribute.Equals("Action")) {
                if (mOperator == 0)
                {
                    foreach (Light l in ll)
                    {
                        l.Action = value;
                    }
                }
                else if (mOperator == -1)
                {
                    foreach (Light l in ll)
                    {
                        l.Action -= value;
                    }
                }
                else if (mOperator == 1)
                {
                    foreach (Light l in ll)
                    {
                        l.Action += value;
                    }
                }
            }
            else if (attribute.Equals("Position"))
            {
                if (mOperator == 0)
                {
                    foreach (Light l in ll)
                    {
                        l.Position = value;
                    }
                }
                else if (mOperator == -1)
                {
                    foreach (Light l in ll)
                    {
                        l.Position -= value;
                    }
                }
                else if (mOperator == 1)
                {
                    foreach (Light l in ll)
                    {
                        l.Position += value;
                    }
                }
            }
            else if (attribute.Equals("Color"))
            {
                if (mOperator == 0)
                {
                    foreach (Light l in ll)
                    {
                        l.Color = value;
                    }
                }
                else if (mOperator == -1)
                {
                    foreach (Light l in ll)
                    {
                        l.Color -= value;
                    }
                }
                else if (mOperator == 1)
                {
                    foreach (Light l in ll)
                    {
                        l.Color += value;
                    }
                }
            }
            return ll;
        }
    }
}
