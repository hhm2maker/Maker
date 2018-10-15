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
        /// <summary>
        /// 水平翻转
        /// </summary>
        public void HorizontalFlipping()
        {
            #region
            for (int k = 0; k < Count; k++)
            {
                //左下
                if (this[k].Position == 36) { this[k].Position = 64; continue; }
                if (this[k].Position == 37) { this[k].Position = 65; continue; }
                if (this[k].Position == 38) { this[k].Position = 66; continue; }
                if (this[k].Position == 39) { this[k].Position = 67; continue; }

                if (this[k].Position == 40) { this[k].Position = 60; continue; }
                if (this[k].Position == 41) { this[k].Position = 61; continue; }
                if (this[k].Position == 42) { this[k].Position = 62; continue; }
                if (this[k].Position == 43) { this[k].Position = 63; continue; }

                if (this[k].Position == 44) { this[k].Position = 56; continue; }
                if (this[k].Position == 45) { this[k].Position = 57; continue; }
                if (this[k].Position == 46) { this[k].Position = 58; continue; }
                if (this[k].Position == 47) { this[k].Position = 59; continue; }

                if (this[k].Position == 48) { this[k].Position = 52; continue; }
                if (this[k].Position == 49) { this[k].Position = 53; continue; }
                if (this[k].Position == 50) { this[k].Position = 54; continue; }
                if (this[k].Position == 51) { this[k].Position = 55; continue; }

                //左上
                if (this[k].Position == 52) { this[k].Position = 48; continue; }
                if (this[k].Position == 53) { this[k].Position = 49; continue; }
                if (this[k].Position == 54) { this[k].Position = 50; continue; }
                if (this[k].Position == 55) { this[k].Position = 51; continue; }

                if (this[k].Position == 56) { this[k].Position = 44; continue; }
                if (this[k].Position == 57) { this[k].Position = 45; continue; }
                if (this[k].Position == 58) { this[k].Position = 46; continue; }
                if (this[k].Position == 59) { this[k].Position = 47; continue; }

                if (this[k].Position == 60) { this[k].Position = 40; continue; }
                if (this[k].Position == 61) { this[k].Position = 41; continue; }
                if (this[k].Position == 62) { this[k].Position = 42; continue; }
                if (this[k].Position == 63) { this[k].Position = 43; continue; }

                if (this[k].Position == 64) { this[k].Position = 36; continue; }
                if (this[k].Position == 65) { this[k].Position = 37; continue; }
                if (this[k].Position == 66) { this[k].Position = 38; continue; }
                if (this[k].Position == 67) { this[k].Position = 39; continue; }

                //右下
                if (this[k].Position == 68) { this[k].Position = 96; continue; }
                if (this[k].Position == 69) { this[k].Position = 97; continue; }
                if (this[k].Position == 70) { this[k].Position = 98; continue; }
                if (this[k].Position == 71) { this[k].Position = 99; continue; }

                if (this[k].Position == 72) { this[k].Position = 92; continue; }
                if (this[k].Position == 73) { this[k].Position = 93; continue; }
                if (this[k].Position == 74) { this[k].Position = 94; continue; }
                if (this[k].Position == 75) { this[k].Position = 95; continue; }

                if (this[k].Position == 76) { this[k].Position = 88; continue; }
                if (this[k].Position == 77) { this[k].Position = 89; continue; }
                if (this[k].Position == 78) { this[k].Position = 90; continue; }
                if (this[k].Position == 79) { this[k].Position = 91; continue; }

                if (this[k].Position == 80) { this[k].Position = 84; continue; }
                if (this[k].Position == 81) { this[k].Position = 85; continue; }
                if (this[k].Position == 82) { this[k].Position = 86; continue; }
                if (this[k].Position == 83) { this[k].Position = 87; continue; }

                //右上
                if (this[k].Position == 84) { this[k].Position = 80; continue; }
                if (this[k].Position == 85) { this[k].Position = 81; continue; }
                if (this[k].Position == 86) { this[k].Position = 82; continue; }
                if (this[k].Position == 87) { this[k].Position = 83; continue; }

                if (this[k].Position == 88) { this[k].Position = 76; continue; }
                if (this[k].Position == 89) { this[k].Position = 77; continue; }
                if (this[k].Position == 90) { this[k].Position = 78; continue; }
                if (this[k].Position == 91) { this[k].Position = 79; continue; }

                if (this[k].Position == 92) { this[k].Position = 72; continue; }
                if (this[k].Position == 93) { this[k].Position = 73; continue; }
                if (this[k].Position == 94) { this[k].Position = 74; continue; }
                if (this[k].Position == 95) { this[k].Position = 75; continue; }

                if (this[k].Position == 96) { this[k].Position = 68; continue; }
                if (this[k].Position == 97) { this[k].Position = 69; continue; }
                if (this[k].Position == 98) { this[k].Position = 70; continue; }
                if (this[k].Position == 99) { this[k].Position = 71; continue; }

                //右圆钮
                if (this[k].Position == 100) { this[k].Position = 107; continue; }
                if (this[k].Position == 101) { this[k].Position = 106; continue; }
                if (this[k].Position == 102) { this[k].Position = 105; continue; }
                if (this[k].Position == 103) { this[k].Position = 104; continue; }
                if (this[k].Position == 104) { this[k].Position = 103; continue; }
                if (this[k].Position == 105) { this[k].Position = 102; continue; }
                if (this[k].Position == 106) { this[k].Position = 101; continue; }
                if (this[k].Position == 107) { this[k].Position = 100; continue; }

                //左圆钮
                if (this[k].Position == 108) { this[k].Position = 115; continue; }
                if (this[k].Position == 109) { this[k].Position = 114; continue; }
                if (this[k].Position == 110) { this[k].Position = 113; continue; }
                if (this[k].Position == 111) { this[k].Position = 112; continue; }
                if (this[k].Position == 112) { this[k].Position = 111; continue; }
                if (this[k].Position == 113) { this[k].Position = 110; continue; }
                if (this[k].Position == 114) { this[k].Position = 109; continue; }
                if (this[k].Position == 115) { this[k].Position = 108; continue; }

                //下圆钮
                if (this[k].Position == 116) { this[k].Position = 28; continue; }
                if (this[k].Position == 117) { this[k].Position = 29; continue; }
                if (this[k].Position == 118) { this[k].Position = 30; continue; }
                if (this[k].Position == 119) { this[k].Position = 31; continue; }
                if (this[k].Position == 120) { this[k].Position = 32; continue; }
                if (this[k].Position == 121) { this[k].Position = 33; continue; }
                if (this[k].Position == 122) { this[k].Position = 34; continue; }
                if (this[k].Position == 123) { this[k].Position = 35; continue; }

                //上圆钮
                if (this[k].Position == 28) { this[k].Position = 116; continue; }
                if (this[k].Position == 29) { this[k].Position = 117; continue; }
                if (this[k].Position == 30) { this[k].Position = 118; continue; }
                if (this[k].Position == 31) { this[k].Position = 119; continue; }
                if (this[k].Position == 32) { this[k].Position = 120; continue; }
                if (this[k].Position == 33) { this[k].Position = 121; continue; }
                if (this[k].Position == 34) { this[k].Position = 122; continue; }
                if (this[k].Position == 35) { this[k].Position = 123; continue; }
            }
            #endregion
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
    }
}
