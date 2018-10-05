using System;
using System.Collections.Generic;
using System.Linq;
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
        public void SetAttribute(int attribute,String strValue) {
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
            else {
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
    }
}
