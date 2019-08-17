using Operation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Operation
{
    static class LightBusiness
    {
        /// <summary>
        /// 拆分ActionBean集合
        /// </summary>
        /// <param name="mActionBeanList">需要拆分的ActionBean集合</param>
        /// <returns>拆分后的ActionBean集合</returns>
        public static List<Light> Split(List<Light> mActionBeanList)
        {
            //将原动作排序(现在的输入将不会在跳转时排序)
            mActionBeanList = Copy(mActionBeanList);
            mActionBeanList = Sort(mActionBeanList); //排序
            List<Light> linShiActionBeanList = new List<Light>();
            List<Light> linShiActionBeanListZi = new List<Light>();
            List<int> linShiLiTime = new List<int>();
            int time = -1;
            //记录时间节点
            for (int i = 0; i < mActionBeanList.Count; i++)
            {
                if (mActionBeanList[i].Time != time)
                {
                    time = mActionBeanList[i].Time;
                    linShiLiTime.Add(time);
                }
            }
            for (int i = 28; i < 124; i++)
            {
                linShiActionBeanListZi.Clear();
                for (int j = 0; j < mActionBeanList.Count; j++)
                {

                    if (mActionBeanList[j].Position == i)
                    {
                        Light ab = new Light();
                        ab.Time = mActionBeanList[j].Time;
                        ab.Action = mActionBeanList[j].Action;
                        ab.Position = mActionBeanList[j].Position;
                        ab.Color = mActionBeanList[j].Color;
                        linShiActionBeanListZi.Add(ab);
                    }
                }
                Boolean bIsOpen = false;//是否打开
                Light LinshiActionBean = new Light();
                for (int j = 0; j < linShiLiTime.Count; j++)
                {
                    Boolean bIsContain = false;//该时间点是否有动作
                    for (int k = 0; k < linShiActionBeanListZi.Count; k++)
                    {
                        if (linShiActionBeanListZi[k].Time == linShiLiTime[j])
                        {
                            if (linShiActionBeanListZi[k].Action == 144)
                            {
                                bIsOpen = true;
                                linShiActionBeanList.Add(linShiActionBeanListZi[k]);
                                //如果包含了且是开 记录原数据(主要为记录颜色)
                                LinshiActionBean.Time = linShiActionBeanListZi[k].Time;
                                LinshiActionBean.Action = linShiActionBeanListZi[k].Action;
                                LinshiActionBean.Position = linShiActionBeanListZi[k].Position;
                                LinshiActionBean.Color = linShiActionBeanListZi[k].Color;
                            }
                            else if (linShiActionBeanListZi[k].Action == 128)
                            {
                                bIsOpen = false;
                                linShiActionBeanList.Add(linShiActionBeanListZi[k]);
                            }
                            bIsContain = true;
                            //break;
                        }
                    }
                    if (!bIsContain) //如果不包含
                    {
                        if (bIsOpen) //如果打开了 增加两条数据
                        {
                            //1
                            Light ab = new Light();
                            ab.Time = linShiLiTime[j];
                            ab.Action = 128;
                            ab.Position = i;
                            ab.Color = LinshiActionBean.Color;
                            linShiActionBeanList.Add(ab);
                            //2
                            Light ab2 = new Light();
                            ab2.Time = linShiLiTime[j];
                            ab2.Action = 144;
                            ab2.Position = i;
                            ab2.Color = LinshiActionBean.Color;
                            linShiActionBeanList.Add(ab2);
                        }
                    }
                }
            }
            linShiActionBeanList = Sort(linShiActionBeanList); //排序  
            return linShiActionBeanList;
        }

        /// <summary>
        /// 拆分ActionBean集合
        /// </summary>
        /// <param name="childLightList">需要拆分的ActionBean集合</param>
        /// <returns>拆分后的ActionBean集合</returns>
        public static List<Light> Split(List<Light> parentLightList,List<Light> childLightList)
        {
            //复制
            parentLightList = Copy(parentLightList);
            childLightList = Copy(childLightList);
            //将原动作排序(现在的输入将不会在跳转时排序)
            childLightList = Sort(childLightList); //排序
            parentLightList = Sort(childLightList); //排序
            List<Light> linShiActionBeanList = new List<Light>();
            List<Light> linShiActionBeanListZi = new List<Light>();
            List<int> linShiLiTime = new List<int>();
            int time = -1;
            //记录时间节点
            for (int i = 0; i < parentLightList.Count; i++)
            {
                if (parentLightList[i].Time != time)
                {
                    time = parentLightList[i].Time;
                    linShiLiTime.Add(time);
                }
            }
            for (int i = 28; i < 124; i++)
            {
                linShiActionBeanListZi.Clear();
                for (int j = 0; j < childLightList.Count; j++)
                {
                    if (childLightList[j].Position == i)
                    {
                        Light ab = new Light();
                        ab.Time = childLightList[j].Time;
                        ab.Action = childLightList[j].Action;
                        ab.Position = childLightList[j].Position;
                        ab.Color = childLightList[j].Color;
                        linShiActionBeanListZi.Add(ab);
                    }
                }
                Boolean bIsOpen = false;//是否打开
                Light LinshiActionBean = new Light();
                for (int j = 0; j < linShiLiTime.Count; j++)
                {
                    Boolean bIsContain = false;//该时间点是否有动作
                    for (int k = 0; k < linShiActionBeanListZi.Count; k++)
                    {
                        if (linShiActionBeanListZi[k].Time == linShiLiTime[j])
                        {
                            if (linShiActionBeanListZi[k].Action == 144)
                            {
                                bIsOpen = true;
                                linShiActionBeanList.Add(linShiActionBeanListZi[k]);
                                //如果包含了且是开 记录原数据(主要为记录颜色)
                                LinshiActionBean.Time = linShiActionBeanListZi[k].Time;
                                LinshiActionBean.Action = linShiActionBeanListZi[k].Action;
                                LinshiActionBean.Position = linShiActionBeanListZi[k].Position;
                                LinshiActionBean.Color = linShiActionBeanListZi[k].Color;
                            }
                            else if (linShiActionBeanListZi[k].Action == 128)
                            {
                                bIsOpen = false;
                                linShiActionBeanList.Add(linShiActionBeanListZi[k]);
                            }
                            bIsContain = true;
                            //break;
                        }
                    }
                    if (!bIsContain) //如果不包含
                    {
                        if (bIsOpen) //如果打开了 增加两条数据
                        {
                            //1
                            Light ab = new Light();
                            ab.Time = linShiLiTime[j];
                            ab.Action = 128;
                            ab.Position = i;
                            ab.Color = LinshiActionBean.Color;
                            linShiActionBeanList.Add(ab);
                            //2
                            Light ab2 = new Light();
                            ab2.Time = linShiLiTime[j];
                            ab2.Action = 144;
                            ab2.Position = i;
                            ab2.Color = LinshiActionBean.Color;
                            linShiActionBeanList.Add(ab2);
                        }
                    }
                }
            }
            linShiActionBeanList = Sort(linShiActionBeanList); //排序  
            return linShiActionBeanList;
        }
        /// <summary>
        /// 拼合ActionBean集合
        /// </summary>
        /// <param name="mActionBeanList">需要拼合的ActionBean集合</param>
        /// <returns>拼合后的ActionBean集合</returns>
        public static List<Light> Splice(List<Light> mActionBeanList)
        {
            //将原动作排序(现在的输入将不会在跳转时排序)
            mActionBeanList = Sort(mActionBeanList); //排序
            //去除自身重复项
            for (int i = mActionBeanList.Count - 1; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (mActionBeanList[i].Time == mActionBeanList[j].Time && mActionBeanList[i].Action == mActionBeanList[j].Action && mActionBeanList[i].Position == mActionBeanList[j].Position && mActionBeanList[i].Color == mActionBeanList[j].Color)
                    {
                        mActionBeanList.RemoveAt(j);
                        break;
                    }
                }
            }
            for (int i = 0; i < mActionBeanList.Count; i++)
            {
                if (mActionBeanList[i].Action == 128)
                {
                    for (int j = mActionBeanList.Count - 1; j >= i + 1; j--)
                    {
                        //如果动作是一开一关
                        if (mActionBeanList[i].Time == mActionBeanList[j].Time && mActionBeanList[j].Action == 144 && mActionBeanList[i].Position == mActionBeanList[j].Position)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                //如果上一个开的位置和颜色都和这个一样，就说明是需要拼接的
                                if (mActionBeanList[k].Action == 144 && mActionBeanList[k].Position == mActionBeanList[j].Position && mActionBeanList[k].Color == mActionBeanList[j].Color)
                                {
                                    mActionBeanList.RemoveAt(i);
                                    mActionBeanList.RemoveAt(j - 1);
                                    i--;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return mActionBeanList;
        }
        /// <summary>
        /// 普通排序
        /// </summary>
        /// <param name="mActionBeanList"></param>
        /// <returns></returns>
        public static List<Light> Sort(List<Light> mActionBeanList)
        {
            mActionBeanList = Copy(mActionBeanList);
            return mActionBeanList.OrderBy(t => t.Time).ThenBy(p => p.Position).ThenBy(a => a.Action).ToList();
        }
        /// <summary>
        /// 按对排序
        /// </summary>
        /// <param name="mActionBeanList"></param>
        /// <returns></returns>
        public static List<Light> SortCouple(List<Light> mActionBeanList)
        {
            return mActionBeanList.OrderBy(p => p.Position).ThenBy(t => t.Time).ThenBy(a => a.Action).ToList();
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="mActionBeanList"></param>
        /// <returns></returns>
        public static List<Light> Copy(List<Light> mActionBeanList)
        {
            List<Light> ll = new List<Light>();
            for (int i = 0; i < mActionBeanList.Count; i++)
            {
                ll.Add(new Light(mActionBeanList[i].Time, mActionBeanList[i].Action, mActionBeanList[i].Position, mActionBeanList[i].Color));
            }
            return ll;
        }
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="mActionBeanList"></param>
        /// <returns></returns>
        public static int GetMax(List<Light> mActionBeanList)
        {
            if (mActionBeanList.Count == 0)
                return -1;
            int max = mActionBeanList[0].Time;
            for (int i = 0; i < mActionBeanList.Count; i++)
            {
                if (max < mActionBeanList[i].Time)
                    max = mActionBeanList[i].Time;
            }
            return max;
        }
        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <param name="mActionBeanList"></param>
        /// <returns></returns>
        public static int GetMin(List<Light> mActionBeanList)
        {
            if (mActionBeanList.Count == 0)
                return -1;
            int min = mActionBeanList[0].Time;
            for (int i = 0; i < mActionBeanList.Count; i++)
            {
                if (min > mActionBeanList[i].Time)
                    min = mActionBeanList[i].Time;
            }
            return min;
        }
        /// <summary>
        /// 关闭动作颜色变为64
        /// </summary>
        /// <param name="mActionBeanList"></param>
        /// <returns></returns>
        public static List<Light> CloseColorTo64(List<Light> mActionBeanList)
        {
            List<Light> _lightList = Copy(mActionBeanList);
            foreach (Light l in _lightList)
            {
                if (l.Action == 128)
                {
                    l.Color = 64;
                }
            }
            return _lightList;
        }
        /// <summary>
        /// 去除非正常数字
        /// </summary>
        /// <param name="mActionBeanList"></param>
        /// <returns></returns>
        public static List<Light> RemoveNotLaunchpadNumbers(List<Light> mActionBeanList)
        {
            List<Light> _lightList = Copy(mActionBeanList);
            for (int i = _lightList.Count - 1; i >= 0; i--) {
                if (_lightList[i].Position < 0 || _lightList[i].Position > 100 || _lightList[i].Color < 1 || _lightList[i].Color > 128)
                {
                    _lightList.RemoveAt(i);
                }
            }
            return _lightList;
        }
        /// <summary>
        /// 把集合所有属性打印到控制台
        /// </summary>
        /// <param name="mActionBeanList"></param>
        public static void Print(List<Light> mActionBeanList) {
            foreach (Light l in mActionBeanList) {
                Console.WriteLine(l.Time+"---"+l.Action+"---"+l.Position+"---"+l.Color);
            }
        }
        /// <summary>
        /// 得到优化过的灯光组(去重：关闭之后马上开启，那么可以省掉关闭的动作，仅能在Maker中使用)
        /// </summary>
        /// <param name="mActionBeanList"></param>
        /// <returns></returns>
        public static List<Light> GetImprovePerformanceLightList(List<Light> mActionBeanList)
        {
            mActionBeanList = Sort(mActionBeanList);
            List<Light> lightList = new List<Light>();
            int notCanTime = -1;
            for (int i = 0; i < mActionBeanList.Count; i++) {
                if (mActionBeanList[i].Action == 144)
                {
                    lightList.Add(new Light(mActionBeanList[i].Time, mActionBeanList[i].Action, mActionBeanList[i].Position, mActionBeanList[i].Color));
                }
                else {
                    if (i == mActionBeanList.Count - 1)
                    {
                        lightList.Add(new Light(mActionBeanList[i].Time, mActionBeanList[i].Action, mActionBeanList[i].Position, mActionBeanList[i].Color));
                    }
                    if (mActionBeanList[i].Time == notCanTime) {
                        break;
                    }
                    else
                    {
                        for (int j = i; j < mActionBeanList.Count; j++)
                        {
                            if (mActionBeanList[i].Time != mActionBeanList[j].Time)
                            {
                                //时间不同停止遍历 - 没有重复，添加
                                lightList.Add(new Light(mActionBeanList[i].Time, mActionBeanList[i].Action, mActionBeanList[i].Position, mActionBeanList[i].Color));
                                break;
                            }
                            if (mActionBeanList[i].IsImprovePerformanceEquals(mActionBeanList[j]))
                            {
                                //满足条件的去除
                                notCanTime = mActionBeanList[i].Time;
                                break;
                            }
                        }
                    }
                }
            }
            //第二步，去掉同为开位置相同的灯光
            List<Light> lightList2 = new List<Light>();
            int position = 0;
            while (position != lightList.Count)
            {
                if (lightList[position].Action == 128)
                {
                    lightList2.Add(new Light(lightList[position].Time, lightList[position].Action, lightList[position].Position, lightList[position].Color));
                    position++;
                }
                else
                {
                    for (int j = position; j < lightList.Count; j++)
                    {
                        if (lightList[position].IsExceptForColorEquals(lightList[j]))
                        {
                            position = j;
                            break;
                        }
                        if (lightList[position].Position != lightList[position].Position)
                        {
                            lightList2.Add(new Light(lightList[position].Time, lightList[position].Action, lightList[position].Position, lightList[position].Color));
                            position = j;
                            break;
                        }
                    }
                }
            }
            return Sort(lightList2);
        }
        /// <summary>
        /// 得到灯光时间集合
        /// </summary>
        /// <param name="mActionBeanList"></param>
        /// <returns></returns>
        public static List<int> GetTimeList(List<Light> mActionBeanList) {
            List<int> timeList = new List<int>();
            for (int i = 0; i < mActionBeanList.Count; i++) {
                if (!timeList.Contains(mActionBeanList[i].Time))
                    timeList.Add(mActionBeanList[i].Time);
            }
            return timeList;
        }
        /// <summary>
        /// 得到分段灯光 - int 数组
        /// </summary>
        public static Dictionary<int, int[]> GetParagraphLightIntList(List<Light> mActionBeanList) {
            mActionBeanList = Sort(mActionBeanList);
            Dictionary<int, int[]> dic = new Dictionary<int, int[]>();
            int time = -1;
            for (int i = 0; i < mActionBeanList.Count; i++)
            {
                if (mActionBeanList[i].Time != time)
                {
                    time = mActionBeanList[i].Time;
                    int[] x = new int[100];
                    for (int j = 0; j < 100; j++)
                    {
                        x[j] = 0;
                    }
                    dic.Add(time, x);
                    if (mActionBeanList[i].Action == 144)
                    {
                        dic[time][mActionBeanList[i].Position - 28] = mActionBeanList[i].Color;
                    }
                    else if (mActionBeanList[i].Action == 128)
                    {
                        dic[time][mActionBeanList[i].Position - 28] = -1;//关闭为黑色
                    }
                }
                else
                {
                    if (mActionBeanList[i].Action == 144)
                    {
                        dic[time][mActionBeanList[i].Position - 28] = mActionBeanList[i].Color;
                    }
                    else if (mActionBeanList[i].Action == 128)
                    {
                        dic[time][mActionBeanList[i].Position - 28] = -1;//关闭为黑色
                    }
                }
            }
            return dic;
        }
        /// <summary>
        /// 得到分段灯光 - int 数组
        /// </summary>
        public static Dictionary<int, List<Light>> GetParagraphLightLightList(List<Light> mActionBeanList)
        {
            mActionBeanList = Sort(mActionBeanList);
            Dictionary<int, List<Light>> dic = new Dictionary<int, List<Light>>();
            int time = -1;
            for (int i = 0; i < mActionBeanList.Count; i++)
            {
                if (mActionBeanList[i].Time != time)
                {
                    time = mActionBeanList[i].Time;
                    dic.Add(time, new List<Light>());
                    dic[time].Add(mActionBeanList[i]);
                }
                else
                {
                    dic[time].Add(mActionBeanList[i]);
                }
            }
            return dic;
        }
        public static List<Light> LightGroupToListLight(LightGroup lightGroup) {
            List<Light> ll = new List<Light>();
            foreach (var item in lightGroup) {
                ll.Add(item);
            }
            return ll;
        }
        public static LightGroup LightGroupToListLight(List<Light> ll)
        {
            LightGroup lightGroup = new LightGroup();
            foreach (var item in ll)
            {
                lightGroup.Add(item);
            }
            return lightGroup;
        }

    }
}
