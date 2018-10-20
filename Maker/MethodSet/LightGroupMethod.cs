using Maker.Business;
using Maker.Model;
using System;
using System.Collections.Generic;

namespace Maker.MethodSet
{
    public static class LightGroupMethod
    {
      
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
