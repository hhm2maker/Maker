using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    public class LightGroup : List<Light>
    {
        public static int TIME = 0;
        public static int ACTION = 1;
        public static int POSITION = 2;
        public static int COLOR = 3;

        public static int NORMAL = 10;
        public static int ADD = 11;
        public static int REMOVE = 12;
        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public void SetAttribute(int attribute, String strValue)
        {
            int mOperator = 0;
            int value = 0;
            if (strValue[0] == '+')
            {
                mOperator = 11;
                value = int.Parse(strValue.Substring(1));
            }
            else if (strValue[0] == '-')
            {
                mOperator = 12;
                value = int.Parse(strValue.Substring(1));
            }
            else
            {
                mOperator = 10;
                value = int.Parse(strValue.Substring(0));
            }
            if (attribute == TIME)
            {
                if (mOperator == 10)
                {
                    foreach (Light l in this)
                    {
                        l.Time = value;
                    }
                }
                else if (mOperator == 12)
                {
                    foreach (Light l in this)
                    {
                        l.Time -= value;
                    }
                }
                else if (mOperator == 11)
                {
                    foreach (Light l in this)
                    {
                        l.Time += value;
                    }
                }
            }
            else if (attribute == ACTION)
            {
                if (mOperator == 10)
                {
                    foreach (Light l in this)
                    {
                        l.Action = value;
                    }
                }
                else if (mOperator == 12)
                {
                    foreach (Light l in this)
                    {
                        l.Action -= value;
                    }
                }
                else if (mOperator == 11)
                {
                    foreach (Light l in this)
                    {
                        l.Action += value;
                    }
                }
            }
            else if (attribute == POSITION)
            {
                if (mOperator == 10)
                {
                    foreach (Light l in this)
                    {
                        l.Position = value;
                    }
                }
                else if (mOperator == 12)
                {
                    foreach (Light l in this)
                    {
                        l.Position -= value;
                    }
                }
                else if (mOperator == 11)
                {
                    foreach (Light l in this)
                    {
                        l.Position += value;
                    }
                }
            }
            else if (attribute == COLOR)
            {
                if (mOperator == 10)
                {
                    foreach (Light l in this)
                    {
                        l.Color = value;
                    }
                }
                else if (mOperator == 12)
                {
                    foreach (Light l in this)
                    {
                        l.Color -= value;
                    }
                }
                else if (mOperator == 11)
                {
                    foreach (Light l in this)
                    {
                        l.Color += value;
                    }
                }
            }
        }

        /// <summary>
        /// 固定位置交换/替换
        /// </summary>
        public void ReplaceControl(List<int> arr)
        {
            for (int k = 0; k < Count; k++)
            {
                this[k].Position = arr[this[k].Position];
            }
        }

        /// <summary>
        /// 水平翻转位置数组
        /// </summary>
        private List<int> horizontalFlippingArr = new List<int>()
            {
                90,91,92,93,94,95,96,97,98,99,
                80,81,82,83,84,85,86,87,88,89,
                70,71,72,73,74,75,76,77,78,79,
                60,61,62,63,64,65,66,67,68,69,
                50,51,52,53,54,55,56,57,58,59,
                40,41,42,43,44,45,46,47,48,49,
                30,31,32,33,34,35,36,37,38,39,
                20,21,22,23,24,25,26,27,28,29,
                10,11,12,13,14,15,16,17,18,19,
                0,1,2,3,4,5,6,7,8,9,
              };

        /// <summary>
        /// 水平翻转
        /// </summary>
        public void HorizontalFlipping()
        {
            ReplaceControl(horizontalFlippingArr);
        }

        /// <summary>
        /// 垂直翻转位置数组
        /// </summary>
        private List<int> verticalFlippingArr = new List<int>()
            {
                9,8,7,6,5,4,3,2,1,0,
                19,18,17,16,15,14,13,12,11,10,
                29,28,27,26,25,24,23,22,21,20,
                39,38,37,36,35,34,33,32,31,30,
                49,48,47,46,45,44,43,42,41,40,
                59,58,57,56,55,54,53,52,51,50,
                69,68,67,66,65,64,63,62,61,60,
                79,78,77,76,75,74,73,72,71,70,
                89,88,87,86,85,84,83,82,81,80,
                99,98,97,96,95,94,93,92,91,90,
             };

        /// <summary>
        /// 垂直翻转
        /// </summary>
        public void VerticalFlipping()
        {
            //for (int k = 0; k < Count; k++)
            //{
            //    if (this[k].Position % 10 > 4)
            //    {
            //        this[k].Position = this[k].Position - (9 - this[k].Position % 10);
            //    }
            //    else
            //    {
            //        this[k].Position = this[k].Position + (5 - this[k].Position % 10) * 2 -1;
            //    }
            //}
            ReplaceControl(verticalFlippingArr);
        }


        /// <summary>
        /// 顺时针翻转位置数组
        /// </summary>
        private List<int> clockwiseArr = new List<int>()
            {
                90,80,70,60,50,40,30,20,10,0,
				91,81,71,61,51,41,31,21,11,1,
				92,82,72,62,52,42,32,22,12,2,
				93,83,73,63,53,43,33,23,13,3,
				94,84,74,64,54,44,34,24,14,4,
				95,85,75,65,55,45,35,25,15,5,
				96,86,76,66,56,46,36,26,16,6,
				97,87,77,67,57,47,37,27,17,7,
                98,88,78,68,58,48,38,28,18,8,
				99,89,79,69,59,49,39,29,19,9,
             };

        /// <summary>
        /// 顺时针
        /// </summary>
        public void Clockwise()
        {
            ReplaceControl(clockwiseArr);
        }

        /// <summary>
        /// 逆时针翻转位置数组
        /// </summary>
        private List<int> antiClockwiseArr = new List<int>()
            {
                9,19,29,39,49,59,69,79,89,99,
                8,18,28,38,48,58,68,78,88,98,
                7,17,27,37,47,57,67,77,87,97,
                6,16,26,36,46,56,66,76,86,96,
                5,15,25,35,45,55,65,75,85,95,
                4,14,24,34,44,54,64,74,84,94,
                3,13,23,33,43,53,63,73,83,93,
                2,12,22,32,42,52,62,72,82,92,
                1,11,21,31,41,51,61,71,81,91,
                0,10,20,30,40,50,60,70,80,90,
             };

        /// <summary>
        /// 逆时针
        /// </summary>
        public void AntiClockwise()
        {
            ReplaceControl(antiClockwiseArr);
        }

        /// <summary>
        ///左下角斜线反转数组
        /// </summary>
        private List<int> lowerLeftSlashFlippingArr = new List<int>()
            {
                0,10,20,30,40,50,60,70,80,90,
                1,11,21,31,41,51,61,71,81,91,
                2,12,22,32,42,52,62,72,82,92,
                3,13,23,33,43,53,63,73,83,93,
                4,14,24,34,44,54,64,74,84,94,
                5,15,25,35,45,55,65,75,85,95,
                6,16,26,36,46,56,66,76,86,96,
                7,17,27,37,47,57,67,77,87,97,
                8,18,28,38,48,58,68,78,88,98,
                9,19,29,39,49,59,69,79,89,99,
             };

        /// <summary>
        /// 左下角斜线反转
        /// </summary>
        public void LowerLeftSlashFlipping()
        {
            ReplaceControl(lowerLeftSlashFlippingArr);
        }

        /// <summary>
        ///右下角斜线反转数组
        /// </summary>
        private List<int> lowerRightSlashFlippingArr = new List<int>()
            {
                99,89,79,69,59,49,39,29,19,9,
                98,88,78,68,58,48,38,28,18,8,
                97,87,77,67,57,47,37,27,17,7,
                96,86,76,66,56,46,36,26,16,6,
                95,85,75,65,55,45,35,25,15,5,
                94,84,74,64,54,44,34,24,14,4,
                93,83,73,63,53,43,33,23,13,3,
                92,82,72,62,52,42,32,22,12,2,
                91,81,71,61,51,41,31,21,11,1,
                90,80,70,60,50,40,30,20,10,0,
             };

        /// <summary>
        /// 右下角斜线反转
        /// </summary>
        public void LowerRightSlashFlipping()
        {
            ReplaceControl(lowerRightSlashFlippingArr);
        }
      
        public static int VERTICAL = 20;
        public static int HORIZONTAL = 21;
        /// <summary>
        /// 对折
        /// </summary>
        /// <param name="orientation">方向</param>
        /// <param name="startPosition"></param>
        /// <param name="span"></param>
        public void Fold(int orientation, int startPosition, int span)
        {
            if (startPosition <= 0 || startPosition >= 10)
                return;
            List<List<int>> mList = new List<List<int>>();
            if (orientation == HORIZONTAL)
            {
                mList.AddRange(IntCollection.HorizontalIntList.ToList());
            }
            else if (orientation == VERTICAL)
            {
                mList.AddRange(IntCollection.VerticalIntList.ToList());
            }
            else
            {
                return;
            }
            int _Min = startPosition - span < 0 ? startPosition : 100;
            int _Max = startPosition + span > 9 ? 10 - startPosition : 100;

            int mMin = _Min < _Max ? _Min : _Max;
            int min, max;
            if (_Min != 100 && _Max != 100 || mMin == 100)
            {
                min = startPosition - span < 0 ? 0 : startPosition - span;
                max = startPosition + span > 9 ? 9 : startPosition + span;
            }
            else
            {
                min = startPosition - mMin;
                max = startPosition + mMin - 1;
            }
            List<int> li1 = new List<int>();
            for (int i = startPosition - 1; i >= min; i--)
            {
                li1.Add(i);
            }
            List<int> li2 = new List<int>();
            for (int i = startPosition; i <= max; i++)
            {
                li2.Add(i);
            }
            List<int> before = new List<int>();
            foreach (int i in li1)
            {
                before.AddRange(mList[i].ToList());
            }
            List<int> after = new List<int>();
            foreach (int i in li2)
            {
                after.AddRange(mList[i].ToList());
            }
            foreach (Light l in this)
            {
                int p = before.IndexOf(l.Position);
                if (p != -1)
                {
                    if (after.Count > p)
                        l.Position = after[p];
                    continue;
                }
                int p2 = after.IndexOf(l.Position);
                if (p2 != -1)
                {
                    if (before.Count > p2)
                        l.Position = before[p2];
                    //continue;无用
                }
            }
            RemoveIncorrectlyData(this);
            return;
        }
        /// <summary>
        /// 去掉无用的数据
        /// </summary>
        /// <param name="mLl"></param>
        private static void RemoveIncorrectlyData(List<Light> mLl)
        {
            for (int x = mLl.Count - 1; x >= 0; x--)
            {
                if (mLl[x].Position == -1)
                {
                    mLl.RemoveAt(x);
                }
            }
        }

      
     
        /// <summary>
        /// 反转
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <returns></returns>
        public void Reversal()
        {
            int max = LightBusiness.GetMax(this);
            int min = LightBusiness.GetMin(this);
            if (max == -1 && min == -1 && this.Count / 2 != 0)
                return;
            //两两组合
            List<Light> ll = LightBusiness.SortCouple(this);
            Clear();
            AddRange(ll);
            for (int i = 0; i < Count; i++)
            {
                //调整时间
                this[i].Time = max - this[i].Time + min;
            }
            for (int i = 0; i < Count; i++)
            {
                if (i / 2 == 1)
                {
                    Light l = this[i];
                    this[i] = this[i - 1];
                    this[i - 1] = l;
                }
            }
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Action == 144)
                {
                    this[i].Action = 128;
                }
                else if (this[i].Action == 128)
                {
                    this[i].Action = 144;
                }
            }
        }

        /// <summary>
        ///边框数组
        /// </summary>
        private List<int> borderArr = new List<int>()
            {
               0,1,2,3,4,5,6,7,8,9,
               10,20,30,40,50,60,70,80,90,
               91,92,93,94,95,96,97,98,99,
               89,79,69,59,49,39,29,19,
             };

        /// <summary>
        /// 去除边框灯光
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <returns></returns>
        public void RemoveBorder()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (borderArr.Contains(this[i].Position)){
                    Remove(this[i]);
                }
            }
        }

        public static int MULTIPLICATION = 30;
        public static int DIVISION = 31;
        /// <summary>
        /// 改变时间
        /// </summary>
        /// <param name="mOperator">乘或除</param>
        /// <param name="multiple"></param>
        public void ChangeTime(int mOperator, Double multiple)
        {
            if (mOperator == MULTIPLICATION)
            {
                for (int i = 0; i < Count; i++)
                {
                    this[i].Time = Convert.ToInt32(this[i].Time * multiple);
                }
            }
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    this[i].Time = Convert.ToInt32(this[i].Time / multiple);
                }
            }
        }
        /// <summary>
        /// 匹配时间
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public void MatchTotalTimeLattice(int v)
        {
            List<Light> ll = LightBusiness.Sort(this);
            Clear();
            AddRange(ll);
            int max = LightBusiness.GetMax(this);
            double d = (double)v / max;
            for (int i = 0; i < Count; i++)
            {
                int result = (int)Math.Round(this[i].Time * d, MidpointRounding.AwayFromZero);
                if (result > v)
                    result--;
                this[i].Time = result;
            }
        }

        /// <summary>
        /// 截取时间内的灯光
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void InterceptTime(int min, int max)
        {
            List<Light> ll = LightBusiness.SortCouple(this);
            Clear();
            AddRange(ll);
            int _max;
            if (max == -1)
                _max = LightBusiness.GetMax(this);
            else
                _max = max;
            int _min;
            if (min == -1)
                _min = LightBusiness.GetMin(this);
            else
                _min = min;
            List<Light> listLight = new List<Light>();
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Time >= min && this[i].Time <= max)
                {
                    listLight.Add(new Light(this[i].Time, this[i].Action, this[i].Position, this[i].Color));
                }
                else if (this[i].Time < min && this[i].Action == 144)
                {
                    if (this[i + 1].Time > min && this[i + 1].Action == 128 && this[i + 1].Time < max)
                    {
                        listLight.Add(new Light(min, this[i].Action, this[i].Position, this[i].Color));
                    }
                }
                else if (this[i].Time > max && this[i].Action == 128)
                {
                    if (i == 0)
                        continue;
                    if (this[i - 1].Time < max && this[i - 1].Action == 144 && this[i - 1].Time > min)
                    {
                        listLight.Add(new Light(max, this[i].Action, this[i].Position, this[i].Color));
                    }
                }
            }
            Clear();
            AddRange(listLight);
        }

        /// <summary>
        /// 用指定颜色填充空白区域
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public void FillColor(int v)
        {
            List<Light> ll = LightBusiness.Sort(this);
            Clear();
            AddRange(ll);
       
            int max = LightBusiness.GetMax(this);
            List<Light> mLl = new List<Light>();
            for (int i = 0; i < 100; i++)
            {
                int nowTime = 0;
                for (int j = 0; j < Count; j++)
                {
                    if (this[j].Position == i)
                    {
                        //如果是开始
                        if (this[j].Action == 144)
                        {
                            //时间大于nowTime
                            if (this[j].Time > nowTime)
                            {
                                //填充一组
                                mLl.Add(new Light(nowTime, 144, i, v));
                                mLl.Add(new Light(this[j].Time, 128, i, 64));
                            }
                        }
                        if (this[j].Action == 128)
                        {
                            nowTime = this[j].Time;
                        }
                    }
                }
                if (nowTime < max)
                {
                    mLl.Add(new Light(nowTime, 144, i, v));
                    mLl.Add(new Light(max, 128, i, 64));
                }
            }
            AddRange(mLl.ToList());
        }
        
        /// <summary>
        /// 设置颜色(格式化)
        /// </summary>
        /// <param name="geshihua">新颜色集合</param>
        public void SetColor(List<int> geshihua)
        {
            List<Light> ll = LightBusiness.Copy(this);
            Clear();

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
                return;
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
            //for (int i = 0; i < ll.Count; i++)
            //{
                for (int k = 0; k < ll.Count; k++)
                {
                    for (int l = 0; l < OldColorList.Count; l++)
                    {
                        //if (ll[k].Action == 144 || ll[k].Action == 128)
                        //{
                            if (ll[k].Color == OldColorList[l])
                            {
                                ll[k].Color = NewColorList[l];
                                break;
                            }
                        //}
                    }
                }
            //}
            AddRange(ll);
        }
        /// <summary>
        /// 根据次数变换颜色
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <param name="colorList"></param>
        /// <returns></returns>
        public void ColorWithCount(List<int> colorList)
        {
            List<Light> ll = LightBusiness.SortCouple(this);
            Clear();
            AddRange(ll);
            int i = 0;
            int nowPosition = -1;
            int colorCount = colorList.Count;
            foreach (Light l in this)
            {
                if (nowPosition == -1)
                    nowPosition = l.Position;
                if (l.Position == nowPosition)
                {
                    if (l.Action == 144)
                    {
                        l.Color = colorList[i];
                        i = (i + 1) % colorCount;
                    }
                }
                else
                {
                    i = 0;
                    nowPosition = l.Position;
                    if (l.Action == 144)
                    {
                        l.Color = colorList[i];
                        i = (i + 1) % colorCount;
                    }
                }
            }
        }
        /// <summary>
        /// 把整个灯光平移至某个时间格
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public void SetStartTime(int startTime)
        {
            List<Light> ll = LightGroupMethod.SetStartTime(this, startTime);
            Clear();
            AddRange(ll);
        }
        /// <summary>
        /// 复制到最后
        /// </summary>
        /// <param name="colorList"></param>
        public void CopyToTheEnd(RangeGroup colorList)
        {
            //就是复制自己
            if (colorList.Count == 0)
            {
                //获取最后的时间
                List<Light> ll = LightBusiness.Sort(this);
                Clear();
                AddRange(ll);
                int time = this[Count - 1].Time;

                List<Light> _ll = LightGroupMethod.SetStartTime(this, time);
                for (int i = 0; i < ll.Count; i++)
                {
                    Add(ll[i]);
                }
            }
            else
            {
                //得到原灯光
                List<Light> _ll = LightBusiness.Copy(this);
                for (int j = 0; j < colorList.Count; j++)
                {
                    //获取最后的时间
                    List<Light> ll = LightBusiness.Sort(this);
                    Clear();
                    AddRange(ll);

                    int time = this[Count - 1].Time;
                    List<Light> mLl = LightGroupMethod.SetStartTime(_ll, time);
                    for (int i = 0; i < mLl.Count; i++)
                    {
                        //if (mLl[i].Action == 144)
                        mLl[i].Color = colorList[j];
                        this.Add(mLl[i]);
                    }
                }
            }
        }
        public static int ALL = 40;
        public static int END = 41;
        public static int ALLANDEND = 42;
        public void SetEndTime(int v1, string v2)
        {
            List<Light> ll = LightBusiness.Copy(this);
            Clear();
            AddRange(ll);
            if (v1 == ALL)
            {
                if (v2.Contains("+"))
                {
                    if (!int.TryParse(v2.Substring(1), out int number))
                    {
                        return;
                    }
                    foreach (Light l in this)
                    {
                        if (l.Action == 128)
                            l.Time += number;
                    }
                    return;
                }
                else if (v2.Contains("-"))
                {
                    if (!int.TryParse(v2.Substring(1), out int number))
                    {
                        return;
                    }
                    foreach (Light l in this)
                    {
                        if (l.Action == 128)
                            l.Time -= number;
                    }
                    return;
                }
                else
                {
                    if (!int.TryParse(v2, out int number))
                    {
                        return;
                    }
                    foreach (Light l in this)
                    {
                        if (l.Action == 128)
                            l.Time = number;
                    }
                    return;
                }
            }
            else if (v1 == END)
            {
                int time = -1;
                foreach (Light l in this)
                {
                    if (l.Time > time && l.Action == 128)
                    {
                        time = l.Time;
                    }
                }
                if (v2.Contains("+"))
                {
                    if (!int.TryParse(v2.Substring(1), out int number))
                    {
                        return;
                    }
                    foreach (Light l in this)
                    {
                        if (l.Time == time && l.Action == 128)
                            l.Time += number;
                    }
                    return;
                }
                else if (v2.Contains("-"))
                {
                    if (!int.TryParse(v2.Substring(1), out int number))
                    {
                        return;
                    }
                    foreach (Light l in this)
                    {
                        if (l.Time == time && l.Action == 128)
                            l.Time -= number;
                    }
                    return;
                }
                else
                {
                    if (!int.TryParse(v2, out int number))
                    {
                        return;
                    }
                    foreach (Light l in this)
                    {
                        if (l.Time == time && l.Action == 128)
                            l.Time = number;
                    }
                    return;
                }
            }
            else if (v1 == ALLANDEND)
            {
                List<Light> mLl = new List<Light>();
                int position = -1;
                int time = -1;
                for (int i = 28; i <= 123; i++)
                {
                    position = -1;
                    time = -1;
                    for (int j = 0; j < Count; j++)
                    {
                        if (this[j].Position == i && this[j].Action == 128)
                        {
                            if (this[j].Time > time)
                            {
                                position = j;
                                time = this[j].Time;
                            }
                        }
                    }
                    if (position > -1)
                        mLl.Add(this[position]);
                }
                if (v2.Contains("+"))
                {
                    if (!int.TryParse(v2.Substring(1), out int number))
                    {
                        return;
                    }
                    foreach (Light l in mLl)
                    {
                        l.Time += number;
                    }
                    Clear();
                    AddRange(mLl);
                    return;
                }
                else if (v2.Contains("-"))
                {
                    if (!int.TryParse(v2.Substring(1), out int number))
                    {
                        return;
                    }
                    foreach (Light l in mLl)
                    {
                        l.Time -= number;
                    }
                    Clear();
                    AddRange(mLl);
                    return;
                }
                else
                {
                    if (!int.TryParse(v2, out int number))
                    {
                        return;
                    }
                    foreach (Light l in mLl)
                    {
                        l.Time = number;
                    }
                    Clear();
                    AddRange(mLl);
                    return;
                }
            }
        }
        /// <summary>
        /// 将所有的灯光对的持续时间控制在固定时间
        /// </summary>
        /// <param name="time"></param>
        public void SetAllTime(int time)
        {
            List<Light> ll = LightBusiness.Copy(this);
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
                            ll[j].Time = ll[i].Time + time;
                            break;
                        }
                    }
                }
            }
            Clear();
            AddRange(ll);
        }
        /// <summary>
        /// 跟随复制到最后
        /// </summary>
        /// <param name="colorList"></param>
        public void CopyToTheFollow(List<int> colorList)
        {
            List<Light> ll = LightBusiness.SortCouple(this);
            List<Light> _lightGroup = LightBusiness.Copy(this);
            if (colorList.Count == 0)
            {
                for (int i = 0; i < ll.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        int width = ll[i + 1].Time - ll[i].Time;
                        _lightGroup.Add(new Light(ll[i].Time + width, ll[i].Action, ll[i].Position, ll[i].Color));
                        _lightGroup.Add(new Light(ll[i + 1].Time + width, ll[i + 1].Action, ll[i + 1].Position, ll[i + 1].Color));
                    }
                }
            }
            else
            {
                for (int i = 0; i < ll.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        int width = ll[i + 1].Time - ll[i].Time;
                        for (int j = 0; j < colorList.Count; j++)
                        {
                            _lightGroup.Add(new Light(ll[i].Time + (j + 1) * width, ll[i].Action, ll[i].Position, colorList[j]));
                            _lightGroup.Add(new Light(ll[i + 1].Time + (j + 1) * width, ll[i + 1].Action, ll[i + 1].Position, colorList[j]));
                        }
                    }
                }
            }
            Clear();
            AddRange(_lightGroup);
        }

        /// <summary>
        /// 加速或减速运动
        /// </summary>
        /// <param name="rangeList"></param>
        public void AccelerationOrDeceleration(List<int> rangeList)
        {
            List<Light> _lightGroup = LightBusiness.Sort(this);
            List<Light> lightGroup = new List<Light>();
            for (int i = 0; i < rangeList.Count; i++)
            {
                lightGroup = LightBusiness.Sort(lightGroup);
                int time = 0;
                if (lightGroup.Count != 0)
                {
                    time = lightGroup[lightGroup.Count - 1].Time;
                }
                List<Light> mLightGroup = new List<Light>();
                for (int j = 0; j < _lightGroup.Count; j++)
                {
                    mLightGroup.Add(new Light(time + (int)(_lightGroup[j].Time * (rangeList[i] / 100.0)), _lightGroup[j].Action, _lightGroup[j].Position, _lightGroup[j].Color));
                }
                for (int k = 0; k < mLightGroup.Count; k++)
                {
                    lightGroup.Add(new Light(mLightGroup[k].Time, mLightGroup[k].Action, mLightGroup[k].Position, mLightGroup[k].Color));
                }
            }
            Clear();
            AddRange(lightGroup);
        }

        public static int SQUARE = 50;
        public static int RADIALVERTICAL = 51;
        public static int RADIALHORIZONTAL = 52;
        /// <summary>
        /// 按形状填充颜色
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="v"></param>
        public void ShapeColor(int _type, List<int> v)
        {
            List<Light> lightGroup = LightBusiness.Copy(this);
            //方形
            if (_type == SQUARE)
            {
                if (v.Count != 5)
                {
                    return;
                }

                List<List<int>> lli = new List<List<int>>();
                lli.Add(new List<int>() { 44, 45, 54, 55 });
                lli.Add(new List<int>() { 33, 34, 35, 36, 43, 46, 53, 56, 63, 64, 65, 66 });
                lli.Add(new List<int>() { 22,23,24,25,26,27,32,37,42,47,52,57,62,67,72,73,74,75,76,77 });
                lli.Add(new List<int>() { 11,12,13,14,15,16,17,18,21,28,31,38,41,48,51,58,61,68,71,78,81,82,83,84,85,86,87,88 });
             
                lli.Add(borderArr);
                if (v[0] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[0].Contains(l.Position))
                        {
                            l.Color = v[0];
                        }
                    }
                }
                if (v[1] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[1].Contains(l.Position))
                        {
                            l.Color = v[1];
                        }
                    }
                }
                if (v[2] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[2].Contains(l.Position))
                        {
                            l.Color = v[2];
                        }
                    }
                }
                if (v[3] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[3].Contains(l.Position))
                        {
                            l.Color = v[3];
                        }
                    }
                }
                if (v[4] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[4].Contains(l.Position))
                        {
                            l.Color = v[4];
                        }
                    }
                }
            }
            //垂直径向
            if (_type == RADIALVERTICAL)
            {
                if (v.Count != 10)
                {
                    return;
                }
                List<List<int>> lli = new List<List<int>>();
                lli.Add(new List<int>() { 90,91,92,93,94,95,96,97,98,99 });
                lli.Add(new List<int>() { 80,81,82,83,84,85,86,87,88,89 });
                lli.Add(new List<int>() { 70,71,72,73,74,75,76,77,78,79});
                lli.Add(new List<int>() { 60,61,62,63,64,65,66,67,68,69 });
                lli.Add(new List<int>() { 50,51,52,53,54,55,56,57,58,59 });
                lli.Add(new List<int>() { 40,41,42,43,44,45,46,47,48,49 });
                lli.Add(new List<int>() { 30,31,32,33,34,35,36,37,38,39 });
                lli.Add(new List<int>() { 20,21,22,23,24,25,26,27,28,29 });
                lli.Add(new List<int>() { 10,11,12,13,14,15,16,17,18,19 });
                lli.Add(new List<int>() { 0,1,2,3,4,5,6,7,8,9 });
                if (v[0] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[0].Contains(l.Position))
                        {
                            l.Color = v[0];
                        }
                    }
                }
                if (v[1] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[1].Contains(l.Position))
                        {
                            l.Color = v[1];
                        }
                    }
                }
                if (v[2] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[2].Contains(l.Position))
                        {
                            l.Color = v[2];
                        }
                    }
                }
                if (v[3] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[3].Contains(l.Position))
                        {
                            l.Color = v[3];
                        }
                    }
                }
                if (v[4] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[4].Contains(l.Position))
                        {
                            l.Color = v[4];
                        }
                    }
                }
                if (v[5] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[5].Contains(l.Position))
                        {
                            l.Color = v[5];
                        }
                    }
                }
                if (v[6] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[6].Contains(l.Position))
                        {
                            l.Color = v[6];
                        }
                    }
                }
                if (v[7] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[7].Contains(l.Position))
                        {
                            l.Color = v[7];
                        }
                    }
                }
                if (v[8] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[8].Contains(l.Position))
                        {
                            l.Color = v[8];
                        }
                    }
                }
                if (v[9] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[9].Contains(l.Position))
                        {
                            l.Color = v[9];
                        }
                    }
                }
            }
            //水平径向
            if (_type == RADIALHORIZONTAL)
            {
                if (v.Count != 10)
                {
                    return;
                }
                List<List<int>> lli = new List<List<int>>();
                lli.Add(new List<int>() { 90,80,70,60,50,40,30,20,10,0 });
                lli.Add(new List<int>() { 91,81,71,61,51,41,31,21,11,1 });
                lli.Add(new List<int>() { 92,82,72,62,52,42,32,22,12,2 });
                lli.Add(new List<int>() { 93,83,73,63,53,43,33,23,13,3 });
                lli.Add(new List<int>() { 94,84,74,64,54,44,34,24,14,4});
                lli.Add(new List<int>() { 95,85,75,65,55,45,35,25,15,5 });
                lli.Add(new List<int>() { 96,86,76,66,56,46,36,26,16,6 });
                lli.Add(new List<int>() { 97,87,77,67,57,47,37,27,17,7 });
                lli.Add(new List<int>() { 98,88,78,68,58,48,38,28,18,8 });
                lli.Add(new List<int>() { 99,89,79,69,59,49,39,29,19,9 });
                if (v[0] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[0].Contains(l.Position))
                        {
                            l.Color = v[0];
                        }
                    }
                }
                if (v[1] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[1].Contains(l.Position))
                        {
                            l.Color = v[1];
                        }
                    }
                }
                if (v[2] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[2].Contains(l.Position))
                        {
                            l.Color = v[2];
                        }
                    }
                }
                if (v[3] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[3].Contains(l.Position))
                        {
                            l.Color = v[3];
                        }
                    }
                }
                if (v[4] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[4].Contains(l.Position))
                        {
                            l.Color = v[4];
                        }
                    }
                }
                if (v[5] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[5].Contains(l.Position))
                        {
                            l.Color = v[5];
                        }
                    }
                }
                if (v[6] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[6].Contains(l.Position))
                        {
                            l.Color = v[6];
                        }
                    }
                }
                if (v[7] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[7].Contains(l.Position))
                        {
                            l.Color = v[7];
                        }
                    }
                }
                if (v[8] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[8].Contains(l.Position))
                        {
                            l.Color = v[8];
                        }
                    }
                }
                if (v[9] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[9].Contains(l.Position))
                        {
                            l.Color = v[9];
                        }
                    }
                }
            }
            Clear();
            AddRange(lightGroup);
        }

        public static int INTERSECTION = 61;
        public static int COMPLEMENT = 62;

        public void CollectionOperation(int type, LightGroup lightGroup)
        {
            List<Light> ll = LightBusiness.Copy(this);

            //集合操作
            List<Light> mainBig = new List<Light>();
            mainBig.AddRange(ll);
            mainBig.AddRange(lightGroup);
            mainBig = LightBusiness.Splice(mainBig);

            List<Light> big = LightBusiness.Split(mainBig, ll);
            List<Light> small = LightBusiness.Split(mainBig, lightGroup);

            List<Light> result = new List<Light>();
            if (type == INTERSECTION)
            {
                for (int i = 0; i < big.Count; i++)
                {
                    for (int j = small.Count - 1; j >= 0; j--)
                    {
                        if (big[i].IsExceptForColorEquals(small[j]))
                        {
                            result.Add(big[i]);
                            small.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
            else if (type == COMPLEMENT)
            {
                for (int i = 0; i < big.Count; i++)
                {
                    bool isContain = false;
                    for (int j = 0; j < small.Count; j++)
                    {
                        if (big[i].IsExceptForColorEquals(small[j]))
                        {
                            isContain = true;
                            break;
                        }
                    }
                    if (!isContain)
                    {
                        result.Add(big[i]);
                    }
                }
            }
            Clear();
            AddRange(result);
        }

        /// <summary>
        /// 第三方
        /// </summary>
        /// <param name="dllFileName"></param>
        /// <param name="parameters"></param>
        public void ThirdParty(String thirdPartyName, String dllFileName, List<String> parameters)
        {
            if (!dllFileName.Equals(String.Empty))
            {
                //Type type = ass.GetType("HorizontalFlipping.HorizontalFlipping");//程序集下所有的类
                //Executable废弃该文件夹

                //这种方式可以释放文件资源
                //LoadFile 无法释放文件
                byte[] fileData = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"Operation\Dll\" + dllFileName + ".dll");
                Assembly ass = Assembly.Load(fileData);
                Type[] types = ass.GetTypes();
                Type type = null;
                foreach (Type t in types)
                {
                    if (t.ToString().Contains("." + thirdPartyName))
                    {
                        type = t;
                        break;
                    }
                }
              
                if (type == null)
                    return ;
 
                //判断是否继承于IGetOperationResult类
                Type _type = type.GetInterface("Operation.IGetOperationResult");
                if (_type == null)
                    return ;
                Object o = Activator.CreateInstance(type);
                MethodInfo mi = o.GetType().GetMethod("GetOperationResult");
                List<Light> lol = new List<Light>();
                foreach (Light l in this)
                {
                    lol.Add(new Operation.Light(l.Time, l.Action, l.Position, l.Color));
                }

                lol = (List<Operation.Light>)mi.Invoke(o, new Object[] { lol, parameters });

                Clear();
                AddRange(lol.ToArray());
            }
        }
    }
}