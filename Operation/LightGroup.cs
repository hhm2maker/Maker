using Maker.Model;
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
        /// 水平翻转位置数组
        /// </summary>
        List<int> horizontalFlippingArr = new List<int>()
            {
               116,117,118,119,120,121,122,123,64,65,66,67,60,61,62,63,56,57,58,59,52,53,54,55,48,49,50,51,44,45,46,47,40,41,42,43,36,37,38,39,96,97,98,99,92,93,94,
                95,88,89,90,91,84,85,86,87,80,81,82,83,76,77,78,79,72,73,74,75,68,69,70,71,107,106,105,104,103,102,101,100,115,114,113,112,111,110,109,108,28,29,30,
                31,32,33,34,35
              };
        /// <summary>
        /// 水平翻转
        /// </summary>
        public void HorizontalFlipping()
        {
            for (int k = 0; k < Count; k++)
            {
                this[k].Position = horizontalFlippingArr[this[k].Position - 28];
            }
        }

        /// <summary>
        /// 垂直翻转
        /// </summary>
        public void VerticalFlipping()
        {
            #region
            for (int k = 0; k < Count; k++)
            {
                //左下
                if (this[k].Position == 36) { this[k].Position = 71; continue; }
                if (this[k].Position == 37) { this[k].Position = 70; continue; }
                if (this[k].Position == 38) { this[k].Position = 69; continue; }
                if (this[k].Position == 39) { this[k].Position = 68; continue; }

                if (this[k].Position == 40) { this[k].Position = 75; continue; }
                if (this[k].Position == 41) { this[k].Position = 74; continue; }
                if (this[k].Position == 42) { this[k].Position = 73; continue; }
                if (this[k].Position == 43) { this[k].Position = 72; continue; }

                if (this[k].Position == 44) { this[k].Position = 79; continue; }
                if (this[k].Position == 45) { this[k].Position = 78; continue; }
                if (this[k].Position == 46) { this[k].Position = 77; continue; }
                if (this[k].Position == 47) { this[k].Position = 76; continue; }

                if (this[k].Position == 48) { this[k].Position = 83; continue; }
                if (this[k].Position == 49) { this[k].Position = 82; continue; }
                if (this[k].Position == 50) { this[k].Position = 81; continue; }
                if (this[k].Position == 51) { this[k].Position = 80; continue; }

                //左上
                if (this[k].Position == 52) { this[k].Position = 87; continue; }
                if (this[k].Position == 53) { this[k].Position = 86; continue; }
                if (this[k].Position == 54) { this[k].Position = 85; continue; }
                if (this[k].Position == 55) { this[k].Position = 84; continue; }

                if (this[k].Position == 56) { this[k].Position = 91; continue; }
                if (this[k].Position == 57) { this[k].Position = 90; continue; }
                if (this[k].Position == 58) { this[k].Position = 89; continue; }
                if (this[k].Position == 59) { this[k].Position = 88; continue; }

                if (this[k].Position == 60) { this[k].Position = 95; continue; }
                if (this[k].Position == 61) { this[k].Position = 94; continue; }
                if (this[k].Position == 62) { this[k].Position = 93; continue; }
                if (this[k].Position == 63) { this[k].Position = 92; continue; }

                if (this[k].Position == 64) { this[k].Position = 99; continue; }
                if (this[k].Position == 65) { this[k].Position = 98; continue; }
                if (this[k].Position == 66) { this[k].Position = 97; continue; }
                if (this[k].Position == 67) { this[k].Position = 96; continue; }

                //右下
                if (this[k].Position == 68) { this[k].Position = 39; continue; }
                if (this[k].Position == 69) { this[k].Position = 38; continue; }
                if (this[k].Position == 70) { this[k].Position = 37; continue; }
                if (this[k].Position == 71) { this[k].Position = 36; continue; }

                if (this[k].Position == 72) { this[k].Position = 43; continue; }
                if (this[k].Position == 73) { this[k].Position = 42; continue; }
                if (this[k].Position == 74) { this[k].Position = 41; continue; }
                if (this[k].Position == 75) { this[k].Position = 40; continue; }

                if (this[k].Position == 76) { this[k].Position = 47; continue; }
                if (this[k].Position == 77) { this[k].Position = 46; continue; }
                if (this[k].Position == 78) { this[k].Position = 45; continue; }
                if (this[k].Position == 79) { this[k].Position = 44; continue; }

                if (this[k].Position == 80) { this[k].Position = 51; continue; }
                if (this[k].Position == 81) { this[k].Position = 50; continue; }
                if (this[k].Position == 82) { this[k].Position = 49; continue; }
                if (this[k].Position == 83) { this[k].Position = 48; continue; }

                //右上
                if (this[k].Position == 84) { this[k].Position = 55; continue; }
                if (this[k].Position == 85) { this[k].Position = 54; continue; }
                if (this[k].Position == 86) { this[k].Position = 53; continue; }
                if (this[k].Position == 87) { this[k].Position = 52; continue; }

                if (this[k].Position == 88) { this[k].Position = 59; continue; }
                if (this[k].Position == 89) { this[k].Position = 58; continue; }
                if (this[k].Position == 90) { this[k].Position = 57; continue; }
                if (this[k].Position == 91) { this[k].Position = 56; continue; }

                if (this[k].Position == 92) { this[k].Position = 63; continue; }
                if (this[k].Position == 93) { this[k].Position = 62; continue; }
                if (this[k].Position == 94) { this[k].Position = 61; continue; }
                if (this[k].Position == 95) { this[k].Position = 60; continue; }

                if (this[k].Position == 96) { this[k].Position = 67; continue; }
                if (this[k].Position == 97) { this[k].Position = 66; continue; }
                if (this[k].Position == 98) { this[k].Position = 65; continue; }
                if (this[k].Position == 99) { this[k].Position = 64; continue; }

                //右圆钮
                if (this[k].Position == 100) { this[k].Position = 108; continue; }
                if (this[k].Position == 101) { this[k].Position = 109; continue; }
                if (this[k].Position == 102) { this[k].Position = 110; continue; }
                if (this[k].Position == 103) { this[k].Position = 111; continue; }
                if (this[k].Position == 104) { this[k].Position = 112; continue; }
                if (this[k].Position == 105) { this[k].Position = 113; continue; }
                if (this[k].Position == 106) { this[k].Position = 114; continue; }
                if (this[k].Position == 107) { this[k].Position = 115; continue; }

                //左圆钮
                if (this[k].Position == 108) { this[k].Position = 100; continue; }
                if (this[k].Position == 109) { this[k].Position = 101; continue; }
                if (this[k].Position == 110) { this[k].Position = 102; continue; }
                if (this[k].Position == 111) { this[k].Position = 103; continue; }
                if (this[k].Position == 112) { this[k].Position = 104; continue; }
                if (this[k].Position == 113) { this[k].Position = 105; continue; }
                if (this[k].Position == 114) { this[k].Position = 106; continue; }
                if (this[k].Position == 115) { this[k].Position = 107; continue; }

                //下圆钮
                if (this[k].Position == 116) { this[k].Position = 123; continue; }
                if (this[k].Position == 117) { this[k].Position = 122; continue; }
                if (this[k].Position == 118) { this[k].Position = 121; continue; }
                if (this[k].Position == 119) { this[k].Position = 120; continue; }
                if (this[k].Position == 120) { this[k].Position = 119; continue; }
                if (this[k].Position == 121) { this[k].Position = 118; continue; }
                if (this[k].Position == 122) { this[k].Position = 117; continue; }
                if (this[k].Position == 123) { this[k].Position = 116; continue; }

                //上圆钮
                if (this[k].Position == 28) { this[k].Position = 35; continue; }
                if (this[k].Position == 29) { this[k].Position = 34; continue; }
                if (this[k].Position == 30) { this[k].Position = 33; continue; }
                if (this[k].Position == 31) { this[k].Position = 32; continue; }
                if (this[k].Position == 32) { this[k].Position = 31; continue; }
                if (this[k].Position == 33) { this[k].Position = 30; continue; }
                if (this[k].Position == 34) { this[k].Position = 29; continue; }
                if (this[k].Position == 35) { this[k].Position = 28; continue; }
            }
            #endregion
        }

        /// <summary>
        /// 右下角斜线反转
        /// </summary>
        public void LowerRightSlashFlipping()
        {
            #region
            for (int k = 0; k < Count; k++)
            {
                //左下
                if (this[k].Position == 36) { this[k].Position = 99; continue; }
                if (this[k].Position == 37) { this[k].Position = 95; continue; }
                if (this[k].Position == 38) { this[k].Position = 91; continue; }
                if (this[k].Position == 39) { this[k].Position = 87; continue; }

                if (this[k].Position == 40) { this[k].Position = 98; continue; }
                if (this[k].Position == 41) { this[k].Position = 94; continue; }
                if (this[k].Position == 42) { this[k].Position = 90; continue; }
                if (this[k].Position == 43) { this[k].Position = 86; continue; }

                if (this[k].Position == 44) { this[k].Position = 97; continue; }
                if (this[k].Position == 45) { this[k].Position = 93; continue; }
                if (this[k].Position == 46) { this[k].Position = 89; continue; }
                if (this[k].Position == 47) { this[k].Position = 85; continue; }

                if (this[k].Position == 48) { this[k].Position = 96; continue; }
                if (this[k].Position == 49) { this[k].Position = 92; continue; }
                if (this[k].Position == 50) { this[k].Position = 88; continue; }
                if (this[k].Position == 51) { this[k].Position = 84; continue; }

                //左上
                if (this[k].Position == 52) { this[k].Position = 67; continue; }
                if (this[k].Position == 53) { this[k].Position = 63; continue; }
                if (this[k].Position == 54) { this[k].Position = 59; continue; }
                if (this[k].Position == 55) { continue; }

                if (this[k].Position == 56) { this[k].Position = 66; continue; }
                if (this[k].Position == 57) { this[k].Position = 62; continue; }
                if (this[k].Position == 58) { continue; }
                if (this[k].Position == 59) { this[k].Position = 54; continue; }

                if (this[k].Position == 60) { this[k].Position = 65; continue; }
                if (this[k].Position == 61) { continue; }
                if (this[k].Position == 62) { this[k].Position = 57; continue; }
                if (this[k].Position == 63) { this[k].Position = 53; continue; }

                if (this[k].Position == 64) { continue; }
                if (this[k].Position == 65) { this[k].Position = 60; continue; }
                if (this[k].Position == 66) { this[k].Position = 56; continue; }
                if (this[k].Position == 67) { this[k].Position = 52; continue; }

                //右下
                if (this[k].Position == 68) { this[k].Position = 83; continue; }
                if (this[k].Position == 69) { this[k].Position = 79; continue; }
                if (this[k].Position == 70) { this[k].Position = 75; continue; }
                if (this[k].Position == 71) { continue; }

                if (this[k].Position == 72) { this[k].Position = 82; continue; }
                if (this[k].Position == 73) { this[k].Position = 78; continue; }
                if (this[k].Position == 74) { continue; }
                if (this[k].Position == 75) { this[k].Position = 70; continue; }

                if (this[k].Position == 76) { this[k].Position = 81; continue; }
                if (this[k].Position == 77) { this[k].Position = 46; continue; }
                if (this[k].Position == 78) { continue; }
                if (this[k].Position == 79) { this[k].Position = 69; continue; }

                if (this[k].Position == 80) { continue; }
                if (this[k].Position == 81) { this[k].Position = 76; continue; }
                if (this[k].Position == 82) { this[k].Position = 72; continue; }
                if (this[k].Position == 83) { this[k].Position = 68; continue; }

                //右上
                if (this[k].Position == 84) { this[k].Position = 51; continue; }
                if (this[k].Position == 85) { this[k].Position = 47; continue; }
                if (this[k].Position == 86) { this[k].Position = 43; continue; }
                if (this[k].Position == 87) { this[k].Position = 39; continue; }

                if (this[k].Position == 88) { this[k].Position = 50; continue; }
                if (this[k].Position == 89) { this[k].Position = 46; continue; }
                if (this[k].Position == 90) { this[k].Position = 42; continue; }
                if (this[k].Position == 91) { this[k].Position = 38; continue; }

                if (this[k].Position == 92) { this[k].Position = 49; continue; }
                if (this[k].Position == 93) { this[k].Position = 45; continue; }
                if (this[k].Position == 94) { this[k].Position = 41; continue; }
                if (this[k].Position == 95) { this[k].Position = 37; continue; }

                if (this[k].Position == 96) { this[k].Position = 48; continue; }
                if (this[k].Position == 97) { this[k].Position = 44; continue; }
                if (this[k].Position == 98) { this[k].Position = 40; continue; }
                if (this[k].Position == 99) { this[k].Position = 36; continue; }

                //右圆钮
                if (this[k].Position == 100) { this[k].Position = 116; continue; }
                if (this[k].Position == 101) { this[k].Position = 117; continue; }
                if (this[k].Position == 102) { this[k].Position = 118; continue; }
                if (this[k].Position == 103) { this[k].Position = 119; continue; }
                if (this[k].Position == 104) { this[k].Position = 120; continue; }
                if (this[k].Position == 105) { this[k].Position = 121; continue; }
                if (this[k].Position == 106) { this[k].Position = 122; continue; }
                if (this[k].Position == 107) { this[k].Position = 123; continue; }

                //左圆钮
                if (this[k].Position == 108) { this[k].Position = 28; continue; }
                if (this[k].Position == 109) { this[k].Position = 29; continue; }
                if (this[k].Position == 110) { this[k].Position = 30; continue; }
                if (this[k].Position == 111) { this[k].Position = 31; continue; }
                if (this[k].Position == 112) { this[k].Position = 32; continue; }
                if (this[k].Position == 113) { this[k].Position = 33; continue; }
                if (this[k].Position == 114) { this[k].Position = 34; continue; }
                if (this[k].Position == 115) { this[k].Position = 35; continue; }

                //下圆钮
                if (this[k].Position == 116) { this[k].Position = 100; continue; }
                if (this[k].Position == 117) { this[k].Position = 101; continue; }
                if (this[k].Position == 118) { this[k].Position = 102; continue; }
                if (this[k].Position == 119) { this[k].Position = 103; continue; }
                if (this[k].Position == 120) { this[k].Position = 104; continue; }
                if (this[k].Position == 121) { this[k].Position = 105; continue; }
                if (this[k].Position == 122) { this[k].Position = 106; continue; }
                if (this[k].Position == 123) { this[k].Position = 107; continue; }

                //上圆钮
                if (this[k].Position == 28) { this[k].Position = 108; continue; }
                if (this[k].Position == 29) { this[k].Position = 109; continue; }
                if (this[k].Position == 30) { this[k].Position = 110; continue; }
                if (this[k].Position == 31) { this[k].Position = 111; continue; }
                if (this[k].Position == 32) { this[k].Position = 112; continue; }
                if (this[k].Position == 33) { this[k].Position = 113; continue; }
                if (this[k].Position == 34) { this[k].Position = 114; continue; }
                if (this[k].Position == 35) { this[k].Position = 115; continue; }
            }
            #endregion
        }
        /// <summary>
        /// 左下角斜线反转
        /// </summary>
        public void LowerLeftSlashFlipping()
        {
            #region
            for (int k = 0; k < this.Count; k++)
            {
                //左下
                if (this[k].Position == 36) { continue; }
                if (this[k].Position == 37) { this[k].Position = 40; continue; }
                if (this[k].Position == 38) { this[k].Position = 44; continue; }
                if (this[k].Position == 39) { this[k].Position = 48; continue; }

                if (this[k].Position == 40) { this[k].Position = 37; continue; }
                if (this[k].Position == 41) { continue; }
                if (this[k].Position == 42) { this[k].Position = 45; continue; }
                if (this[k].Position == 43) { this[k].Position = 49; continue; }

                if (this[k].Position == 44) { this[k].Position = 38; continue; }
                if (this[k].Position == 45) { this[k].Position = 42; continue; }
                if (this[k].Position == 46) { continue; }
                if (this[k].Position == 47) { this[k].Position = 50; continue; }

                if (this[k].Position == 48) { this[k].Position = 39; continue; }
                if (this[k].Position == 49) { this[k].Position = 43; continue; }
                if (this[k].Position == 50) { this[k].Position = 47; continue; }
                if (this[k].Position == 51) { continue; }

                //左上
                if (this[k].Position == 68) { this[k].Position = 52; continue; }
                if (this[k].Position == 69) { this[k].Position = 56; continue; }
                if (this[k].Position == 70) { this[k].Position = 60; continue; }
                if (this[k].Position == 71) { this[k].Position = 64; continue; }

                if (this[k].Position == 72) { this[k].Position = 53; continue; }
                if (this[k].Position == 73) { this[k].Position = 57; continue; }
                if (this[k].Position == 74) { this[k].Position = 61; continue; }
                if (this[k].Position == 75) { this[k].Position = 65; continue; }

                if (this[k].Position == 76) { this[k].Position = 54; continue; }
                if (this[k].Position == 77) { this[k].Position = 58; continue; }
                if (this[k].Position == 78) { this[k].Position = 62; continue; }
                if (this[k].Position == 79) { this[k].Position = 66; continue; }

                if (this[k].Position == 80) { this[k].Position = 55; continue; }
                if (this[k].Position == 81) { this[k].Position = 59; continue; }
                if (this[k].Position == 82) { this[k].Position = 63; continue; }
                if (this[k].Position == 83) { this[k].Position = 67; continue; }
                //右下
                if (this[k].Position == 52) { this[k].Position = 68; continue; }
                if (this[k].Position == 53) { this[k].Position = 72; continue; }
                if (this[k].Position == 54) { this[k].Position = 76; continue; }
                if (this[k].Position == 55) { this[k].Position = 80; continue; }

                if (this[k].Position == 56) { this[k].Position = 69; continue; }
                if (this[k].Position == 57) { this[k].Position = 73; continue; }
                if (this[k].Position == 58) { this[k].Position = 77; continue; }
                if (this[k].Position == 59) { this[k].Position = 81; continue; }

                if (this[k].Position == 60) { this[k].Position = 70; continue; }
                if (this[k].Position == 61) { this[k].Position = 74; continue; }
                if (this[k].Position == 62) { this[k].Position = 78; continue; }
                if (this[k].Position == 63) { this[k].Position = 82; continue; }

                if (this[k].Position == 64) { this[k].Position = 71; continue; }
                if (this[k].Position == 65) { this[k].Position = 75; continue; }
                if (this[k].Position == 66) { this[k].Position = 79; continue; }
                if (this[k].Position == 67) { this[k].Position = 83; continue; }
                //右上
                if (this[k].Position == 84) { continue; }
                if (this[k].Position == 85) { this[k].Position = 88; continue; }
                if (this[k].Position == 86) { this[k].Position = 92; continue; }
                if (this[k].Position == 87) { this[k].Position = 96; continue; }

                if (this[k].Position == 88) { this[k].Position = 85; continue; }
                if (this[k].Position == 89) { continue; }
                if (this[k].Position == 90) { this[k].Position = 93; continue; }
                if (this[k].Position == 91) { this[k].Position = 97; continue; }

                if (this[k].Position == 92) { this[k].Position = 86; continue; }
                if (this[k].Position == 93) { this[k].Position = 90; continue; }
                if (this[k].Position == 94) { continue; }
                if (this[k].Position == 95) { this[k].Position = 98; continue; }

                if (this[k].Position == 96) { this[k].Position = 87; continue; }
                if (this[k].Position == 97) { this[k].Position = 91; continue; }
                if (this[k].Position == 98) { this[k].Position = 95; continue; }
                if (this[k].Position == 99) { continue; }

                //右圆钮
                if (this[k].Position == 100) { this[k].Position = 35; continue; }
                if (this[k].Position == 101) { this[k].Position = 34; continue; }
                if (this[k].Position == 102) { this[k].Position = 33; continue; }
                if (this[k].Position == 103) { this[k].Position = 32; continue; }
                if (this[k].Position == 104) { this[k].Position = 31; continue; }
                if (this[k].Position == 105) { this[k].Position = 30; continue; }
                if (this[k].Position == 106) { this[k].Position = 29; continue; }
                if (this[k].Position == 107) { this[k].Position = 28; continue; }

                //左圆钮
                if (this[k].Position == 108) { this[k].Position = 123; continue; }
                if (this[k].Position == 109) { this[k].Position = 122; continue; }
                if (this[k].Position == 110) { this[k].Position = 121; continue; }
                if (this[k].Position == 111) { this[k].Position = 120; continue; }
                if (this[k].Position == 112) { this[k].Position = 119; continue; }
                if (this[k].Position == 113) { this[k].Position = 118; continue; }
                if (this[k].Position == 114) { this[k].Position = 117; continue; }
                if (this[k].Position == 115) { this[k].Position = 116; continue; }

                //下圆钮
                if (this[k].Position == 116) { this[k].Position = 115; continue; }
                if (this[k].Position == 117) { this[k].Position = 114; continue; }
                if (this[k].Position == 118) { this[k].Position = 113; continue; }
                if (this[k].Position == 119) { this[k].Position = 112; continue; }
                if (this[k].Position == 120) { this[k].Position = 111; continue; }
                if (this[k].Position == 121) { this[k].Position = 110; continue; }
                if (this[k].Position == 122) { this[k].Position = 109; continue; }
                if (this[k].Position == 123) { this[k].Position = 108; continue; }

                //上圆钮
                if (this[k].Position == 28) { this[k].Position = 107; continue; }
                if (this[k].Position == 29) { this[k].Position = 106; continue; }
                if (this[k].Position == 30) { this[k].Position = 105; continue; }
                if (this[k].Position == 31) { this[k].Position = 104; continue; }
                if (this[k].Position == 32) { this[k].Position = 103; continue; }
                if (this[k].Position == 33) { this[k].Position = 102; continue; }
                if (this[k].Position == 34) { this[k].Position = 101; continue; }
                if (this[k].Position == 35) { this[k].Position = 100; continue; }
            }
            #endregion
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
        /// 顺时针
        /// </summary>
        public void Clockwise()
        {
            #region
            for (int k = 0; k < Count; k++)
            {
                //左下
                if (this[k].Position == 36) { this[k].Position = 64; continue; }
                if (this[k].Position == 37) { this[k].Position = 60; continue; }
                if (this[k].Position == 38) { this[k].Position = 56; continue; }
                if (this[k].Position == 39) { this[k].Position = 52; continue; }

                if (this[k].Position == 40) { this[k].Position = 65; continue; }
                if (this[k].Position == 41) { this[k].Position = 61; continue; }
                if (this[k].Position == 42) { this[k].Position = 57; continue; }
                if (this[k].Position == 43) { this[k].Position = 53; continue; }

                if (this[k].Position == 44) { this[k].Position = 66; continue; }
                if (this[k].Position == 45) { this[k].Position = 62; continue; }
                if (this[k].Position == 46) { this[k].Position = 58; continue; }
                if (this[k].Position == 47) { this[k].Position = 54; continue; }

                if (this[k].Position == 48) { this[k].Position = 67; continue; }
                if (this[k].Position == 49) { this[k].Position = 63; continue; }
                if (this[k].Position == 50) { this[k].Position = 59; continue; }
                if (this[k].Position == 51) { this[k].Position = 55; continue; }

                //左上
                if (this[k].Position == 52) { this[k].Position = 96; continue; }
                if (this[k].Position == 53) { this[k].Position = 92; continue; }
                if (this[k].Position == 54) { this[k].Position = 88; continue; }
                if (this[k].Position == 55) { this[k].Position = 84; continue; }

                if (this[k].Position == 56) { this[k].Position = 97; continue; }
                if (this[k].Position == 57) { this[k].Position = 93; continue; }
                if (this[k].Position == 58) { this[k].Position = 89; continue; }
                if (this[k].Position == 59) { this[k].Position = 85; continue; }

                if (this[k].Position == 60) { this[k].Position = 98; continue; }
                if (this[k].Position == 61) { this[k].Position = 94; continue; }
                if (this[k].Position == 62) { this[k].Position = 90; continue; }
                if (this[k].Position == 63) { this[k].Position = 86; continue; }

                if (this[k].Position == 64) { this[k].Position = 99; continue; }
                if (this[k].Position == 65) { this[k].Position = 95; continue; }
                if (this[k].Position == 66) { this[k].Position = 91; continue; }
                if (this[k].Position == 67) { this[k].Position = 87; continue; }

                //右下
                if (this[k].Position == 68) { this[k].Position = 48; continue; }
                if (this[k].Position == 69) { this[k].Position = 44; continue; }
                if (this[k].Position == 70) { this[k].Position = 40; continue; }
                if (this[k].Position == 71) { this[k].Position = 36; continue; }

                if (this[k].Position == 72) { this[k].Position = 49; continue; }
                if (this[k].Position == 73) { this[k].Position = 45; continue; }
                if (this[k].Position == 74) { this[k].Position = 41; continue; }
                if (this[k].Position == 75) { this[k].Position = 37; continue; }

                if (this[k].Position == 76) { this[k].Position = 50; continue; }
                if (this[k].Position == 77) { this[k].Position = 46; continue; }
                if (this[k].Position == 78) { this[k].Position = 42; continue; }
                if (this[k].Position == 79) { this[k].Position = 38; continue; }

                if (this[k].Position == 80) { this[k].Position = 51; continue; }
                if (this[k].Position == 81) { this[k].Position = 47; continue; }
                if (this[k].Position == 82) { this[k].Position = 43; continue; }
                if (this[k].Position == 83) { this[k].Position = 39; continue; }

                //右上
                if (this[k].Position == 84) { this[k].Position = 80; continue; }
                if (this[k].Position == 85) { this[k].Position = 76; continue; }
                if (this[k].Position == 86) { this[k].Position = 72; continue; }
                if (this[k].Position == 87) { this[k].Position = 68; continue; }

                if (this[k].Position == 88) { this[k].Position = 81; continue; }
                if (this[k].Position == 89) { this[k].Position = 77; continue; }
                if (this[k].Position == 90) { this[k].Position = 73; continue; }
                if (this[k].Position == 91) { this[k].Position = 69; continue; }

                if (this[k].Position == 92) { this[k].Position = 82; continue; }
                if (this[k].Position == 93) { this[k].Position = 78; continue; }
                if (this[k].Position == 94) { this[k].Position = 74; continue; }
                if (this[k].Position == 95) { this[k].Position = 70; continue; }

                if (this[k].Position == 96) { this[k].Position = 83; continue; }
                if (this[k].Position == 97) { this[k].Position = 79; continue; }
                if (this[k].Position == 98) { this[k].Position = 75; continue; }
                if (this[k].Position == 99) { this[k].Position = 71; continue; }

                //右圆钮
                if (this[k].Position == 100) { this[k].Position = 123; continue; }
                if (this[k].Position == 101) { this[k].Position = 122; continue; }
                if (this[k].Position == 102) { this[k].Position = 121; continue; }
                if (this[k].Position == 103) { this[k].Position = 120; continue; }
                if (this[k].Position == 104) { this[k].Position = 119; continue; }
                if (this[k].Position == 105) { this[k].Position = 118; continue; }
                if (this[k].Position == 106) { this[k].Position = 117; continue; }
                if (this[k].Position == 107) { this[k].Position = 116; continue; }

                //左圆钮
                if (this[k].Position == 108) { this[k].Position = 35; continue; }
                if (this[k].Position == 109) { this[k].Position = 34; continue; }
                if (this[k].Position == 110) { this[k].Position = 33; continue; }
                if (this[k].Position == 111) { this[k].Position = 32; continue; }
                if (this[k].Position == 112) { this[k].Position = 31; continue; }
                if (this[k].Position == 113) { this[k].Position = 30; continue; }
                if (this[k].Position == 114) { this[k].Position = 29; continue; }
                if (this[k].Position == 115) { this[k].Position = 28; continue; }

                //下圆钮
                if (this[k].Position == 116) { this[k].Position = 108; continue; }
                if (this[k].Position == 117) { this[k].Position = 109; continue; }
                if (this[k].Position == 118) { this[k].Position = 110; continue; }
                if (this[k].Position == 119) { this[k].Position = 111; continue; }
                if (this[k].Position == 120) { this[k].Position = 112; continue; }
                if (this[k].Position == 121) { this[k].Position = 113; continue; }
                if (this[k].Position == 122) { this[k].Position = 114; continue; }
                if (this[k].Position == 123) { this[k].Position = 115; continue; }

                //上圆钮
                if (this[k].Position == 28) { this[k].Position = 100; continue; }
                if (this[k].Position == 29) { this[k].Position = 101; continue; }
                if (this[k].Position == 30) { this[k].Position = 102; continue; }
                if (this[k].Position == 31) { this[k].Position = 103; continue; }
                if (this[k].Position == 32) { this[k].Position = 104; continue; }
                if (this[k].Position == 33) { this[k].Position = 105; continue; }
                if (this[k].Position == 34) { this[k].Position = 106; continue; }
                if (this[k].Position == 35) { this[k].Position = 107; continue; }
            }
            #endregion
        }
        /// <summary>
        /// 逆时针
        /// </summary>
        public void AntiClockwise()
        {
            #region
            for (int k = 0; k < Count; k++)
            {
                //左下
                if (this[k].Position == 36) { this[k].Position = 71; continue; }
                if (this[k].Position == 37) { this[k].Position = 75; continue; }
                if (this[k].Position == 38) { this[k].Position = 79; continue; }
                if (this[k].Position == 39) { this[k].Position = 83; continue; }

                if (this[k].Position == 40) { this[k].Position = 70; continue; }
                if (this[k].Position == 41) { this[k].Position = 74; continue; }
                if (this[k].Position == 42) { this[k].Position = 78; continue; }
                if (this[k].Position == 43) { this[k].Position = 82; continue; }

                if (this[k].Position == 44) { this[k].Position = 69; continue; }
                if (this[k].Position == 45) { this[k].Position = 73; continue; }
                if (this[k].Position == 46) { this[k].Position = 77; continue; }
                if (this[k].Position == 47) { this[k].Position = 81; continue; }

                if (this[k].Position == 48) { this[k].Position = 68; continue; }
                if (this[k].Position == 49) { this[k].Position = 72; continue; }
                if (this[k].Position == 50) { this[k].Position = 76; continue; }
                if (this[k].Position == 51) { this[k].Position = 80; continue; }

                //左上
                if (this[k].Position == 52) { this[k].Position = 39; continue; }
                if (this[k].Position == 53) { this[k].Position = 43; continue; }
                if (this[k].Position == 54) { this[k].Position = 47; continue; }
                if (this[k].Position == 55) { this[k].Position = 51; continue; }

                if (this[k].Position == 56) { this[k].Position = 38; continue; }
                if (this[k].Position == 57) { this[k].Position = 42; continue; }
                if (this[k].Position == 58) { this[k].Position = 46; continue; }
                if (this[k].Position == 59) { this[k].Position = 50; continue; }

                if (this[k].Position == 60) { this[k].Position = 37; continue; }
                if (this[k].Position == 61) { this[k].Position = 41; continue; }
                if (this[k].Position == 62) { this[k].Position = 45; continue; }
                if (this[k].Position == 63) { this[k].Position = 49; continue; }

                if (this[k].Position == 64) { this[k].Position = 36; continue; }
                if (this[k].Position == 65) { this[k].Position = 40; continue; }
                if (this[k].Position == 66) { this[k].Position = 44; continue; }
                if (this[k].Position == 67) { this[k].Position = 48; continue; }

                //右下
                if (this[k].Position == 68) { this[k].Position = 87; continue; }
                if (this[k].Position == 69) { this[k].Position = 91; continue; }
                if (this[k].Position == 70) { this[k].Position = 95; continue; }
                if (this[k].Position == 71) { this[k].Position = 99; continue; }

                if (this[k].Position == 72) { this[k].Position = 86; continue; }
                if (this[k].Position == 73) { this[k].Position = 90; continue; }
                if (this[k].Position == 74) { this[k].Position = 94; continue; }
                if (this[k].Position == 75) { this[k].Position = 98; continue; }

                if (this[k].Position == 76) { this[k].Position = 85; continue; }
                if (this[k].Position == 77) { this[k].Position = 89; continue; }
                if (this[k].Position == 78) { this[k].Position = 93; continue; }
                if (this[k].Position == 79) { this[k].Position = 97; continue; }

                if (this[k].Position == 80) { this[k].Position = 84; continue; }
                if (this[k].Position == 81) { this[k].Position = 88; continue; }
                if (this[k].Position == 82) { this[k].Position = 92; continue; }
                if (this[k].Position == 83) { this[k].Position = 96; continue; }

                //右上
                if (this[k].Position == 84) { this[k].Position = 55; continue; }
                if (this[k].Position == 85) { this[k].Position = 59; continue; }
                if (this[k].Position == 86) { this[k].Position = 63; continue; }
                if (this[k].Position == 87) { this[k].Position = 67; continue; }

                if (this[k].Position == 88) { this[k].Position = 54; continue; }
                if (this[k].Position == 89) { this[k].Position = 58; continue; }
                if (this[k].Position == 90) { this[k].Position = 62; continue; }
                if (this[k].Position == 91) { this[k].Position = 66; continue; }

                if (this[k].Position == 92) { this[k].Position = 53; continue; }
                if (this[k].Position == 93) { this[k].Position = 57; continue; }
                if (this[k].Position == 94) { this[k].Position = 61; continue; }
                if (this[k].Position == 95) { this[k].Position = 65; continue; }

                if (this[k].Position == 96) { this[k].Position = 52; continue; }
                if (this[k].Position == 97) { this[k].Position = 56; continue; }
                if (this[k].Position == 98) { this[k].Position = 60; continue; }
                if (this[k].Position == 99) { this[k].Position = 64; continue; }

                //右圆钮
                if (this[k].Position == 100) { this[k].Position = 28; continue; }
                if (this[k].Position == 101) { this[k].Position = 29; continue; }
                if (this[k].Position == 102) { this[k].Position = 30; continue; }
                if (this[k].Position == 103) { this[k].Position = 31; continue; }
                if (this[k].Position == 104) { this[k].Position = 32; continue; }
                if (this[k].Position == 105) { this[k].Position = 33; continue; }
                if (this[k].Position == 106) { this[k].Position = 34; continue; }
                if (this[k].Position == 107) { this[k].Position = 35; continue; }

                //左圆钮
                if (this[k].Position == 108) { this[k].Position = 116; continue; }
                if (this[k].Position == 109) { this[k].Position = 117; continue; }
                if (this[k].Position == 110) { this[k].Position = 118; continue; }
                if (this[k].Position == 111) { this[k].Position = 119; continue; }
                if (this[k].Position == 112) { this[k].Position = 120; continue; }
                if (this[k].Position == 113) { this[k].Position = 121; continue; }
                if (this[k].Position == 114) { this[k].Position = 122; continue; }
                if (this[k].Position == 115) { this[k].Position = 123; continue; }

                //下圆钮
                if (this[k].Position == 116) { this[k].Position = 107; continue; }
                if (this[k].Position == 117) { this[k].Position = 106; continue; }
                if (this[k].Position == 118) { this[k].Position = 105; continue; }
                if (this[k].Position == 119) { this[k].Position = 104; continue; }
                if (this[k].Position == 120) { this[k].Position = 103; continue; }
                if (this[k].Position == 121) { this[k].Position = 102; continue; }
                if (this[k].Position == 122) { this[k].Position = 101; continue; }
                if (this[k].Position == 123) { this[k].Position = 100; continue; }

                //上圆钮
                if (this[k].Position == 28) { this[k].Position = 115; continue; }
                if (this[k].Position == 29) { this[k].Position = 114; continue; }
                if (this[k].Position == 30) { this[k].Position = 113; continue; }
                if (this[k].Position == 31) { this[k].Position = 112; continue; }
                if (this[k].Position == 32) { this[k].Position = 111; continue; }
                if (this[k].Position == 33) { this[k].Position = 110; continue; }
                if (this[k].Position == 34) { this[k].Position = 109; continue; }
                if (this[k].Position == 35) { this[k].Position = 108; continue; }
            }
            #endregion
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
        /// 去除边框灯光
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <returns></returns>
        public void RemoveBorder()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                int position = this[i].Position;
                if (position >= 28 && position <= 35 || position >= 100 && position <= 123)
                    Remove(this[i]);
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
            for (int i = 28; i <= 123; i++)
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
        public void CopyToTheEnd(ColorGroup colorList)
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
                lli.Add(new List<int>() { 51, 55, 80, 84 });
                lli.Add(new List<int>() { 46, 47, 50, 54, 58, 59, 76, 77, 81, 85, 88, 89 });
                lli.Add(new List<int>() { 41, 42, 43, 45, 49, 53, 57, 61, 62, 63, 72, 73, 74, 78, 82, 86, 90, 92, 93, 94 });
                lli.Add(new List<int>() { 36, 37, 38, 39, 40, 44, 48, 52, 56, 60, 64, 65, 66, 67, 68, 69, 70, 71, 75, 79, 83, 87, 91, 95, 96, 97, 98, 99 });
                List<int> _list = new List<int>();
                for (int i = 28; i <= 35; i++)
                {
                    _list.Add(i);
                }
                for (int i = 100; i <= 123; i++)
                {
                    _list.Add(i);
                }
                lli.Add(_list);
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
                lli.Add(new List<int>() { 28, 29, 30, 31, 32, 33, 34, 35 });
                lli.Add(new List<int>() { 108, 64, 65, 66, 67, 96, 97, 98, 99, 100 });
                lli.Add(new List<int>() { 109, 60, 61, 62, 63, 92, 93, 94, 95, 101 });
                lli.Add(new List<int>() { 110, 56, 57, 58, 59, 88, 89, 90, 91, 102 });
                lli.Add(new List<int>() { 111, 52, 53, 54, 55, 84, 85, 86, 87, 103 });
                lli.Add(new List<int>() { 112, 48, 49, 50, 51, 80, 81, 82, 83, 104 });
                lli.Add(new List<int>() { 113, 44, 45, 46, 47, 76, 77, 78, 79, 105 });
                lli.Add(new List<int>() { 114, 40, 41, 42, 43, 72, 73, 74, 75, 106 });
                lli.Add(new List<int>() { 115, 36, 37, 38, 39, 68, 69, 70, 71, 107 });
                lli.Add(new List<int>() { 116, 117, 118, 119, 120, 121, 122, 123 });
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
                lli.Add(new List<int>() { 108, 109, 110, 111, 112, 113, 114, 115 });
                lli.Add(new List<int>() { 28, 64, 60, 56, 52, 48, 44, 40, 36, 116 });
                lli.Add(new List<int>() { 29, 65, 61, 57, 53, 49, 45, 41, 37, 117 });
                lli.Add(new List<int>() { 30, 66, 62, 58, 54, 50, 46, 42, 38, 118 });
                lli.Add(new List<int>() { 31, 67, 63, 59, 55, 51, 47, 43, 39, 119 });
                lli.Add(new List<int>() { 32, 96, 92, 88, 84, 80, 76, 72, 68, 120 });
                lli.Add(new List<int>() { 33, 97, 93, 89, 85, 81, 77, 73, 69, 121 });
                lli.Add(new List<int>() { 34, 98, 94, 90, 86, 82, 78, 74, 70, 122 });
                lli.Add(new List<int>() { 35, 99, 95, 91, 87, 83, 79, 75, 71, 123 });
                lli.Add(new List<int>() { 100, 101, 102, 103, 104, 105, 106, 107 });
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
    }
}
