using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Model
{
    public class Light
    {
        /// <summary>
        /// 时间
        /// </summary>
        public int Time
        {
            get;
            set;
        }
        /// <summary>
        /// 行为
        /// </summary>
        public int Action
        {
            get;
            set;
        }
        /// <summary>
        /// 位置
        /// </summary>
        public int Position
        {
            get;
            set;
        }
        /// <summary>
        /// 颜色
        /// </summary>
        public int Color
        {
            get;
            set;
        }
        /// <summary>
        /// 返回灯光对象
        /// </summary>
        /// <param name="Time">时间</param>
        /// <param name="Action">行为</param>
        /// <param name="Position">位置</param>
        /// <param name="Color">颜色</param>
        public Light(int Time, int Action, int Position, int Color)
        {
            this.Time = Time;
            this.Action = Action;
            this.Position = Position;
            this.Color = Color;
        }
        /// <summary>
        /// 返回无参对象
        /// </summary>
        public Light()
        {

        }

        //public bool IsEquals(object obj)
        //{
        //    if (obj == null)
        //    {
        //        return false;
        //    }
        //    if ((obj.GetType().Equals(this.GetType())) == false)
        //    {
        //        return false;
        //    }
        //    Light temp = null;
        //    temp = (Light)obj;
        //    return this.Time.Equals(temp.Time) && this.Action.Equals(temp.Action) && this.Position.Equals(temp.Position) && this.Color.Equals(temp.Color);
        //}
        /// <summary>
        /// 除了颜色其他都相同
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsExceptForColorEquals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if ((obj.GetType().Equals(this.GetType())) == false)
            {
                return false;
            }
            Light temp = null;
            temp = (Light)obj;
            return Time.Equals(temp.Time) && Action.Equals(temp.Action) && Position.Equals(temp.Position);
        }
     
        public bool IsImprovePerformanceEquals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if ((obj.GetType().Equals(this.GetType())) == false)
            {
                return false;
            }
            Light temp = null;
            temp = (Light)obj;
            return Time.Equals(temp.Time) && temp.Action == 144 && Position.Equals(temp.Position);
        }
    }
}
