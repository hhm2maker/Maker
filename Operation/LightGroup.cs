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
                return ;
            }
            int _Min = startPosition - span < 0 ? _Min = startPosition : 100;
            int _Max = startPosition + span > 9 ? 10 - startPosition : 100;
            int mMin = _Min < _Max ? _Min : _Max;
            int min, max;
            if (_Min != 100 && _Max != 100)
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
                    l.Position = after[p];
                    continue;
                }
                int p2 = after.IndexOf(l.Position);
                if (p2 != -1)
                {
                    l.Position = before[p2];
                    //continue;无用
                }
            }
            RemoveIncorrectlyData(this);
            return ;
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
                return ;
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
    }
}
