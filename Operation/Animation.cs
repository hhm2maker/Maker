using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    public class Animation
    {
        public static LightGroup Serpentine(LightGroup lightGroup, int startTime, int interval)
        {
            List<Light> ll = LightBusiness.LightGroupToListLight(lightGroup);
            ll = LightBusiness.Sort(lightGroup);
            List<int> list = new List<int>(){ 81,82,83,84,85,86,87,88,
                                  78,77,76,75,74,73,72,71,
                                  61,62,63,64,65,66,67,68,
                                  58,57,56,55,54,53,52,51,
                                  41,42,43,44,45,46,47,48,
                                  38,37,36,35,34,33,32,31,
                                  21,22,23,24,25,26,27,28,
                                  18,17,16,15,14,13,12,11};

            for (int i = 0; i < list.Count(); i++)
            {
                int time = startTime + interval * i;
                for (int j = 0; j < ll.Count; j++)
                {
                    if (ll[j].Action == 128 && ll[j].Time > time && ll[j].Position == list[i])
                    {
                        //需要消除的
                        ll[j].Time = time;
                    }
                }
            }
            ll = LightBusiness.SortCouple(ll);
            bool b = false;
            for (int i = ll.Count - 1; i >= 0; i--)
            {
                if (b)
                {
                    ll.RemoveAt(i);
                    b = false;
                }
                if (i % 2 == 1)
                {
                    if (ll[i].Time == ll[i - 1].Time && ll[i].Time == startTime + interval * list.IndexOf(ll[i].Position))
                    {
                        ll.RemoveAt(i);
                        b = true;
                    }
                }
            }
            ll = LightBusiness.Sort(ll);
            lightGroup = LightBusiness.LightGroupToListLight(ll);
            return lightGroup;
        }

        public static LightGroup Windmill(LightGroup lightGroup, int interval)
        {
            List<Light> ll = LightBusiness.LightGroupToListLight(lightGroup);
            ll = LightBusiness.Copy(ll);
            List<Light> _lightGroup = LightBusiness.Copy(ll);
            for (int i = 0; i < 7; i++)
            {
                _lightGroup = ToWindmill(_lightGroup, interval, i);
                ll.AddRange(_lightGroup);
            }
            lightGroup = LightBusiness.LightGroupToListLight(ll);
            return lightGroup;
        }

        private static List<Light> ToWindmill(List<Light> lightGroup, int interval, int count)
        {
            lightGroup = LightBusiness.Copy(lightGroup);
            for (int i = 0; i < lightGroup.Count; i++)
            {
                lightGroup[i].Time += interval;
                if (count == 0)
                {
                    //左下
                    if (lightGroup[i].Position == 47) { lightGroup[i].Position = 46; continue; }
                    else if (lightGroup[i].Position == 43) { lightGroup[i].Position = 42; continue; }
                    else if (lightGroup[i].Position == 39) { lightGroup[i].Position = 38; continue; }
                    else if (lightGroup[i].Position == 51) { lightGroup[i].Position = 51; continue; }

                    else if (lightGroup[i].Position == 46) { lightGroup[i].Position = 50; continue; }
                    else if (lightGroup[i].Position == 42) { lightGroup[i].Position = 41; continue; }
                    else if (lightGroup[i].Position == 38) { lightGroup[i].Position = 37; continue; }
                    else if (lightGroup[i].Position == 41) { lightGroup[i].Position = 45; continue; }
                    else if (lightGroup[i].Position == 36) { lightGroup[i].Position = 40; continue; }
                    else if (lightGroup[i].Position == 37) { lightGroup[i].Position = 36; continue; }
                    else if (lightGroup[i].Position == 45) { lightGroup[i].Position = 49; continue; }
                    else if (lightGroup[i].Position == 44) { lightGroup[i].Position = 48; continue; }
                    else if (lightGroup[i].Position == 40) { lightGroup[i].Position = 44; continue; }
                    else if (lightGroup[i].Position == 50) { lightGroup[i].Position = 54; continue; }
                    else if (lightGroup[i].Position == 49) { lightGroup[i].Position = 53; continue; }
                    else if (lightGroup[i].Position == 48) { lightGroup[i].Position = 52; continue; }
                    //左上
                    else if (lightGroup[i].Position == 54) { lightGroup[i].Position = 58; continue; }
                    else if (lightGroup[i].Position == 53) { lightGroup[i].Position = 57; continue; }
                    else if (lightGroup[i].Position == 52) { lightGroup[i].Position = 56; continue; }
                    else if (lightGroup[i].Position == 55) { lightGroup[i].Position = 55; continue; }

                    else if (lightGroup[i].Position == 58) { lightGroup[i].Position = 59; continue; }
                    else if (lightGroup[i].Position == 57) { lightGroup[i].Position = 61; continue; }
                    else if (lightGroup[i].Position == 56) { lightGroup[i].Position = 60; continue; }
                    else if (lightGroup[i].Position == 61) { lightGroup[i].Position = 62; continue; }
                    else if (lightGroup[i].Position == 64) { lightGroup[i].Position = 65; continue; }
                    else if (lightGroup[i].Position == 60) { lightGroup[i].Position = 64; continue; }
                    else if (lightGroup[i].Position == 62) { lightGroup[i].Position = 63; continue; }
                    else if (lightGroup[i].Position == 66) { lightGroup[i].Position = 67; continue; }
                    else if (lightGroup[i].Position == 65) { lightGroup[i].Position = 66; continue; }
                    else if (lightGroup[i].Position == 59) { lightGroup[i].Position = 88; continue; }
                    else if (lightGroup[i].Position == 63) { lightGroup[i].Position = 92; continue; }
                    else if (lightGroup[i].Position == 67) { lightGroup[i].Position = 96; continue; }
                    //右下
                    else if (lightGroup[i].Position == 83) { lightGroup[i].Position = 79; continue; }
                    else if (lightGroup[i].Position == 82) { lightGroup[i].Position = 78; continue; }
                    else if (lightGroup[i].Position == 81) { lightGroup[i].Position = 77; continue; }
                    else if (lightGroup[i].Position == 80) { lightGroup[i].Position = 80; continue; }

                    else if (lightGroup[i].Position == 77) { lightGroup[i].Position = 76; continue; }
                    else if (lightGroup[i].Position == 78) { lightGroup[i].Position = 74; continue; }
                    else if (lightGroup[i].Position == 79) { lightGroup[i].Position = 75; continue; }
                    else if (lightGroup[i].Position == 74) { lightGroup[i].Position = 73; continue; }
                    else if (lightGroup[i].Position == 71) { lightGroup[i].Position = 70; continue; }
                    else if (lightGroup[i].Position == 75) { lightGroup[i].Position = 71; continue; }
                    else if (lightGroup[i].Position == 73) { lightGroup[i].Position = 72; continue; }
                    else if (lightGroup[i].Position == 69) { lightGroup[i].Position = 68; continue; }
                    else if (lightGroup[i].Position == 70) { lightGroup[i].Position = 69; continue; }
                    else if (lightGroup[i].Position == 76) { lightGroup[i].Position = 47; continue; }
                    else if (lightGroup[i].Position == 72) { lightGroup[i].Position = 43; continue; }
                    else if (lightGroup[i].Position == 68) { lightGroup[i].Position = 39; continue; }
                    ////右上
                    else if (lightGroup[i].Position == 96) { lightGroup[i].Position = 97; continue; }
                    else if (lightGroup[i].Position == 92) { lightGroup[i].Position = 93; continue; }
                    else if (lightGroup[i].Position == 88) { lightGroup[i].Position = 89; continue; }
                    else if (lightGroup[i].Position == 84) { lightGroup[i].Position = 84; continue; }


                    else if (lightGroup[i].Position == 89) { lightGroup[i].Position = 85; continue; }
                    else if (lightGroup[i].Position == 93) { lightGroup[i].Position = 94; continue; }
                    else if (lightGroup[i].Position == 97) { lightGroup[i].Position = 98; continue; }
                    else if (lightGroup[i].Position == 94) { lightGroup[i].Position = 90; continue; }
                    else if (lightGroup[i].Position == 99) { lightGroup[i].Position = 95; continue; }
                    else if (lightGroup[i].Position == 98) { lightGroup[i].Position = 99; continue; }
                    else if (lightGroup[i].Position == 90) { lightGroup[i].Position = 86; continue; }
                    else if (lightGroup[i].Position == 91) { lightGroup[i].Position = 87; continue; }
                    else if (lightGroup[i].Position == 95) { lightGroup[i].Position = 91; continue; }
                    else if (lightGroup[i].Position == 85) { lightGroup[i].Position = 81; continue; }
                    else if (lightGroup[i].Position == 86) { lightGroup[i].Position = 82; continue; }
                    else if (lightGroup[i].Position == 87) { lightGroup[i].Position = 83; continue; }
                }
                if (count == 1)
                {
                    //左下
                    if (lightGroup[i].Position == 42) { lightGroup[i].Position = 41; continue; }
                    else if (lightGroup[i].Position == 38) { lightGroup[i].Position = 37; continue; }
                    else if (lightGroup[i].Position == 46) { lightGroup[i].Position = 46; continue; }
                    else if (lightGroup[i].Position == 51) { lightGroup[i].Position = 51; continue; }

                    else if (lightGroup[i].Position == 37) { lightGroup[i].Position = 36; continue; }
                    else if (lightGroup[i].Position == 36) { lightGroup[i].Position = 40; continue; }
                    else if (lightGroup[i].Position == 41) { lightGroup[i].Position = 45; continue; }
                    else if (lightGroup[i].Position == 40) { lightGroup[i].Position = 44; continue; }
                    else if (lightGroup[i].Position == 44) { lightGroup[i].Position = 48; continue; }
                    else if (lightGroup[i].Position == 45) { lightGroup[i].Position = 49; continue; }
                    else if (lightGroup[i].Position == 49) { lightGroup[i].Position = 53; continue; }
                    else if (lightGroup[i].Position == 48) { lightGroup[i].Position = 52; continue; }
                    else if (lightGroup[i].Position == 52) { lightGroup[i].Position = 56; continue; }
                    else if (lightGroup[i].Position == 53) { lightGroup[i].Position = 57; continue; }
                    //左上
                    else if (lightGroup[i].Position == 57) { lightGroup[i].Position = 61; continue; }
                    else if (lightGroup[i].Position == 56) { lightGroup[i].Position = 60; continue; }
                    else if (lightGroup[i].Position == 55) { lightGroup[i].Position = 55; continue; }
                    else if (lightGroup[i].Position == 58) { lightGroup[i].Position = 58; continue; }

                    else if (lightGroup[i].Position == 60) { lightGroup[i].Position = 64; continue; }
                    else if (lightGroup[i].Position == 64) { lightGroup[i].Position = 65; continue; }
                    else if (lightGroup[i].Position == 61) { lightGroup[i].Position = 62; continue; }
                    else if (lightGroup[i].Position == 65) { lightGroup[i].Position = 66; continue; }
                    else if (lightGroup[i].Position == 66) { lightGroup[i].Position = 67; continue; }
                    else if (lightGroup[i].Position == 62) { lightGroup[i].Position = 63; continue; }
                    else if (lightGroup[i].Position == 63) { lightGroup[i].Position = 92; continue; }
                    else if (lightGroup[i].Position == 67) { lightGroup[i].Position = 96; continue; }
                    else if (lightGroup[i].Position == 96) { lightGroup[i].Position = 97; continue; }
                    else if (lightGroup[i].Position == 92) { lightGroup[i].Position = 93; continue; }
                    //右下
                    else if (lightGroup[i].Position == 79) { lightGroup[i].Position = 75; continue; }
                    else if (lightGroup[i].Position == 78) { lightGroup[i].Position = 74; continue; }
                    else if (lightGroup[i].Position == 77) { lightGroup[i].Position = 77; continue; }
                    else if (lightGroup[i].Position == 80) { lightGroup[i].Position = 80; continue; }

                    else if (lightGroup[i].Position == 75) { lightGroup[i].Position = 71; continue; }
                    else if (lightGroup[i].Position == 71) { lightGroup[i].Position = 70; continue; }
                    else if (lightGroup[i].Position == 74) { lightGroup[i].Position = 73; continue; }
                    else if (lightGroup[i].Position == 70) { lightGroup[i].Position = 69; continue; }
                    else if (lightGroup[i].Position == 69) { lightGroup[i].Position = 68; continue; }
                    else if (lightGroup[i].Position == 73) { lightGroup[i].Position = 72; continue; }
                    else if (lightGroup[i].Position == 72) { lightGroup[i].Position = 43; continue; }
                    else if (lightGroup[i].Position == 68) { lightGroup[i].Position = 39; continue; }
                    else if (lightGroup[i].Position == 39) { lightGroup[i].Position = 38; continue; }
                    else if (lightGroup[i].Position == 43) { lightGroup[i].Position = 42; continue; }
                    //右上
                    else if (lightGroup[i].Position == 97) { lightGroup[i].Position = 98; continue; }
                    else if (lightGroup[i].Position == 93) { lightGroup[i].Position = 94; continue; }
                    else if (lightGroup[i].Position == 89) { lightGroup[i].Position = 89; continue; }
                    else if (lightGroup[i].Position == 84) { lightGroup[i].Position = 84; continue; }

                    else if (lightGroup[i].Position == 98) { lightGroup[i].Position = 99; continue; }
                    else if (lightGroup[i].Position == 99) { lightGroup[i].Position = 95; continue; }
                    else if (lightGroup[i].Position == 94) { lightGroup[i].Position = 90; continue; }
                    else if (lightGroup[i].Position == 95) { lightGroup[i].Position = 91; continue; }
                    else if (lightGroup[i].Position == 91) { lightGroup[i].Position = 87; continue; }
                    else if (lightGroup[i].Position == 90) { lightGroup[i].Position = 86; continue; }
                    else if (lightGroup[i].Position == 86) { lightGroup[i].Position = 82; continue; }
                    else if (lightGroup[i].Position == 87) { lightGroup[i].Position = 83; continue; }
                    else if (lightGroup[i].Position == 83) { lightGroup[i].Position = 79; continue; }
                    else if (lightGroup[i].Position == 82) { lightGroup[i].Position = 78; continue; }
                }
                if (count == 2)
                {
                    //左下
                    if (lightGroup[i].Position == 37) { lightGroup[i].Position = 36; continue; }
                    else if (lightGroup[i].Position == 41) { lightGroup[i].Position = 41; continue; }
                    else if (lightGroup[i].Position == 46) { lightGroup[i].Position = 46; continue; }
                    else if (lightGroup[i].Position == 51) { lightGroup[i].Position = 51; continue; }

                    else if (lightGroup[i].Position == 36) { lightGroup[i].Position = 40; continue; }
                    else if (lightGroup[i].Position == 40) { lightGroup[i].Position = 44; continue; }
                    else if (lightGroup[i].Position == 44) { lightGroup[i].Position = 48; continue; }
                    else if (lightGroup[i].Position == 48) { lightGroup[i].Position = 52; continue; }
                    else if (lightGroup[i].Position == 52) { lightGroup[i].Position = 56; continue; }
                    else if (lightGroup[i].Position == 56) { lightGroup[i].Position = 60; continue; }
                    //左上
                    else if (lightGroup[i].Position == 60) { lightGroup[i].Position = 64; continue; }
                    else if (lightGroup[i].Position == 55) { lightGroup[i].Position = 55; continue; }
                    else if (lightGroup[i].Position == 55) { lightGroup[i].Position = 55; continue; }
                    else if (lightGroup[i].Position == 58) { lightGroup[i].Position = 58; continue; }

                    else if (lightGroup[i].Position == 64) { lightGroup[i].Position = 65; continue; }
                    else if (lightGroup[i].Position == 65) { lightGroup[i].Position = 66; continue; }
                    else if (lightGroup[i].Position == 66) { lightGroup[i].Position = 67; continue; }
                    else if (lightGroup[i].Position == 67) { lightGroup[i].Position = 96; continue; }
                    else if (lightGroup[i].Position == 96) { lightGroup[i].Position = 97; continue; }
                    else if (lightGroup[i].Position == 97) { lightGroup[i].Position = 98; continue; }
                    //右下
                    else if (lightGroup[i].Position == 75) { lightGroup[i].Position = 71; continue; }
                    else if (lightGroup[i].Position == 74) { lightGroup[i].Position = 74; continue; }
                    else if (lightGroup[i].Position == 77) { lightGroup[i].Position = 77; continue; }
                    else if (lightGroup[i].Position == 80) { lightGroup[i].Position = 80; continue; }

                    else if (lightGroup[i].Position == 71) { lightGroup[i].Position = 70; continue; }
                    else if (lightGroup[i].Position == 70) { lightGroup[i].Position = 69; continue; }
                    else if (lightGroup[i].Position == 69) { lightGroup[i].Position = 68; continue; }
                    else if (lightGroup[i].Position == 68) { lightGroup[i].Position = 39; continue; }
                    else if (lightGroup[i].Position == 39) { lightGroup[i].Position = 38; continue; }
                    else if (lightGroup[i].Position == 38) { lightGroup[i].Position = 37; continue; }
                    //右上
                    else if (lightGroup[i].Position == 98) { lightGroup[i].Position = 99; continue; }
                    else if (lightGroup[i].Position == 94) { lightGroup[i].Position = 94; continue; }
                    else if (lightGroup[i].Position == 89) { lightGroup[i].Position = 89; continue; }
                    else if (lightGroup[i].Position == 84) { lightGroup[i].Position = 84; continue; }

                    else if (lightGroup[i].Position == 99) { lightGroup[i].Position = 95; continue; }
                    else if (lightGroup[i].Position == 95) { lightGroup[i].Position = 91; continue; }
                    else if (lightGroup[i].Position == 91) { lightGroup[i].Position = 87; continue; }
                    else if (lightGroup[i].Position == 87) { lightGroup[i].Position = 83; continue; }
                    else if (lightGroup[i].Position == 83) { lightGroup[i].Position = 79; continue; }
                    else if (lightGroup[i].Position == 79) { lightGroup[i].Position = 75; continue; }
                }
                if (count == 3)
                {
                    //左下
                    if (lightGroup[i].Position == 36) { lightGroup[i].Position = 40; continue; }
                    else if (lightGroup[i].Position == 41) { lightGroup[i].Position = 41; continue; }
                    else if (lightGroup[i].Position == 46) { lightGroup[i].Position = 46; continue; }
                    else if (lightGroup[i].Position == 51) { lightGroup[i].Position = 51; continue; }

                    else if (lightGroup[i].Position == 40) { lightGroup[i].Position = 44; continue; }
                    else if (lightGroup[i].Position == 44) { lightGroup[i].Position = 48; continue; }
                    else if (lightGroup[i].Position == 48) { lightGroup[i].Position = 52; continue; }
                    else if (lightGroup[i].Position == 52) { lightGroup[i].Position = 56; continue; }
                    else if (lightGroup[i].Position == 56) { lightGroup[i].Position = 60; continue; }
                    else if (lightGroup[i].Position == 60) { lightGroup[i].Position = 64; continue; }

                    //左上
                    else if (lightGroup[i].Position == 64) { lightGroup[i].Position = 65; continue; }
                    else if (lightGroup[i].Position == 55) { lightGroup[i].Position = 55; continue; }
                    else if (lightGroup[i].Position == 55) { lightGroup[i].Position = 55; continue; }
                    else if (lightGroup[i].Position == 58) { lightGroup[i].Position = 58; continue; }

                    else if (lightGroup[i].Position == 65) { lightGroup[i].Position = 66; continue; }
                    else if (lightGroup[i].Position == 66) { lightGroup[i].Position = 67; continue; }
                    else if (lightGroup[i].Position == 67) { lightGroup[i].Position = 96; continue; }
                    else if (lightGroup[i].Position == 96) { lightGroup[i].Position = 97; continue; }
                    else if (lightGroup[i].Position == 97) { lightGroup[i].Position = 98; continue; }
                    else if (lightGroup[i].Position == 98) { lightGroup[i].Position = 99; continue; }
                    //右下
                    else if (lightGroup[i].Position == 71) { lightGroup[i].Position = 70; continue; }
                    else if (lightGroup[i].Position == 74) { lightGroup[i].Position = 74; continue; }
                    else if (lightGroup[i].Position == 77) { lightGroup[i].Position = 77; continue; }
                    else if (lightGroup[i].Position == 80) { lightGroup[i].Position = 80; continue; }

                    else if (lightGroup[i].Position == 70) { lightGroup[i].Position = 69; continue; }
                    else if (lightGroup[i].Position == 69) { lightGroup[i].Position = 68; continue; }
                    else if (lightGroup[i].Position == 68) { lightGroup[i].Position = 39; continue; }
                    else if (lightGroup[i].Position == 39) { lightGroup[i].Position = 38; continue; }
                    else if (lightGroup[i].Position == 38) { lightGroup[i].Position = 37; continue; }
                    else if (lightGroup[i].Position == 37) { lightGroup[i].Position = 36; continue; }
                    //右上
                    else if (lightGroup[i].Position == 99) { lightGroup[i].Position = 95; continue; }
                    else if (lightGroup[i].Position == 94) { lightGroup[i].Position = 94; continue; }
                    else if (lightGroup[i].Position == 89) { lightGroup[i].Position = 89; continue; }
                    else if (lightGroup[i].Position == 84) { lightGroup[i].Position = 84; continue; }

                    else if (lightGroup[i].Position == 95) { lightGroup[i].Position = 91; continue; }
                    else if (lightGroup[i].Position == 91) { lightGroup[i].Position = 87; continue; }
                    else if (lightGroup[i].Position == 87) { lightGroup[i].Position = 83; continue; }
                    else if (lightGroup[i].Position == 83) { lightGroup[i].Position = 79; continue; }
                    else if (lightGroup[i].Position == 79) { lightGroup[i].Position = 75; continue; }
                    else if (lightGroup[i].Position == 75) { lightGroup[i].Position = 71; continue; }
                }
                if (count == 4)
                {
                    //左下
                    if (lightGroup[i].Position == 40) { lightGroup[i].Position = 44; continue; }
                    else if (lightGroup[i].Position == 41) { lightGroup[i].Position = 45; continue; }
                    else if (lightGroup[i].Position == 46) { lightGroup[i].Position = 46; continue; }
                    else if (lightGroup[i].Position == 51) { lightGroup[i].Position = 51; continue; }

                    else if (lightGroup[i].Position == 44) { lightGroup[i].Position = 48; continue; }
                    else if (lightGroup[i].Position == 45) { lightGroup[i].Position = 49; continue; }
                    else if (lightGroup[i].Position == 48) { lightGroup[i].Position = 52; continue; }
                    else if (lightGroup[i].Position == 49) { lightGroup[i].Position = 53; continue; }
                    else if (lightGroup[i].Position == 52) { lightGroup[i].Position = 56; continue; }
                    else if (lightGroup[i].Position == 53) { lightGroup[i].Position = 57; continue; }
                    else if (lightGroup[i].Position == 56) { lightGroup[i].Position = 60; continue; }
                    else if (lightGroup[i].Position == 57) { lightGroup[i].Position = 61; continue; }
                    else if (lightGroup[i].Position == 60) { lightGroup[i].Position = 64; continue; }
                    else if (lightGroup[i].Position == 64) { lightGroup[i].Position = 65; continue; }
                    //左上
                    else if (lightGroup[i].Position == 65) { lightGroup[i].Position = 66; continue; }
                    else if (lightGroup[i].Position == 61) { lightGroup[i].Position = 62; continue; }
                    else if (lightGroup[i].Position == 55) { lightGroup[i].Position = 55; continue; }
                    else if (lightGroup[i].Position == 58) { lightGroup[i].Position = 58; continue; }

                    else if (lightGroup[i].Position == 66) { lightGroup[i].Position = 67; continue; }
                    else if (lightGroup[i].Position == 62) { lightGroup[i].Position = 63; continue; }
                    else if (lightGroup[i].Position == 67) { lightGroup[i].Position = 96; continue; }
                    else if (lightGroup[i].Position == 63) { lightGroup[i].Position = 92; continue; }
                    else if (lightGroup[i].Position == 96) { lightGroup[i].Position = 97; continue; }
                    else if (lightGroup[i].Position == 92) { lightGroup[i].Position = 93; continue; }
                    else if (lightGroup[i].Position == 97) { lightGroup[i].Position = 98; continue; }
                    else if (lightGroup[i].Position == 93) { lightGroup[i].Position = 94; continue; }
                    else if (lightGroup[i].Position == 98) { lightGroup[i].Position = 99; continue; }
                    else if (lightGroup[i].Position == 99) { lightGroup[i].Position = 95; continue; }
                    //右下
                    else if (lightGroup[i].Position == 70) { lightGroup[i].Position = 69; continue; }
                    else if (lightGroup[i].Position == 74) { lightGroup[i].Position = 73; continue; }
                    else if (lightGroup[i].Position == 77) { lightGroup[i].Position = 77; continue; }
                    else if (lightGroup[i].Position == 80) { lightGroup[i].Position = 80; continue; }

                    else if (lightGroup[i].Position == 69) { lightGroup[i].Position = 68; continue; }
                    else if (lightGroup[i].Position == 73) { lightGroup[i].Position = 72; continue; }
                    else if (lightGroup[i].Position == 68) { lightGroup[i].Position = 39; continue; }
                    else if (lightGroup[i].Position == 72) { lightGroup[i].Position = 43; continue; }
                    else if (lightGroup[i].Position == 39) { lightGroup[i].Position = 38; continue; }
                    else if (lightGroup[i].Position == 43) { lightGroup[i].Position = 42; continue; }
                    else if (lightGroup[i].Position == 38) { lightGroup[i].Position = 37; continue; }
                    else if (lightGroup[i].Position == 42) { lightGroup[i].Position = 41; continue; }
                    else if (lightGroup[i].Position == 37) { lightGroup[i].Position = 36; continue; }
                    else if (lightGroup[i].Position == 36) { lightGroup[i].Position = 40; continue; }
                    //右上
                    else if (lightGroup[i].Position == 95) { lightGroup[i].Position = 91; continue; }
                    else if (lightGroup[i].Position == 94) { lightGroup[i].Position = 90; continue; }
                    else if (lightGroup[i].Position == 89) { lightGroup[i].Position = 89; continue; }
                    else if (lightGroup[i].Position == 84) { lightGroup[i].Position = 84; continue; }

                    else if (lightGroup[i].Position == 91) { lightGroup[i].Position = 87; continue; }
                    else if (lightGroup[i].Position == 90) { lightGroup[i].Position = 86; continue; }
                    else if (lightGroup[i].Position == 87) { lightGroup[i].Position = 83; continue; }
                    else if (lightGroup[i].Position == 86) { lightGroup[i].Position = 82; continue; }
                    else if (lightGroup[i].Position == 83) { lightGroup[i].Position = 79; continue; }
                    else if (lightGroup[i].Position == 82) { lightGroup[i].Position = 78; continue; }
                    else if (lightGroup[i].Position == 79) { lightGroup[i].Position = 75; continue; }
                    else if (lightGroup[i].Position == 78) { lightGroup[i].Position = 74; continue; }
                    else if (lightGroup[i].Position == 75) { lightGroup[i].Position = 71; continue; }
                    else if (lightGroup[i].Position == 71) { lightGroup[i].Position = 70; continue; }
                }
                if (count == 5)
                {
                    //左下
                    if (lightGroup[i].Position == 44) { lightGroup[i].Position = 48; continue; }
                    else if (lightGroup[i].Position == 45) { lightGroup[i].Position = 49; continue; }
                    else if (lightGroup[i].Position == 46) { lightGroup[i].Position = 50; continue; }
                    else if (lightGroup[i].Position == 51) { lightGroup[i].Position = 51; continue; }

                    else if (lightGroup[i].Position == 50) { lightGroup[i].Position = 54; continue; }
                    else if (lightGroup[i].Position == 48) { lightGroup[i].Position = 52; continue; }
                    else if (lightGroup[i].Position == 49) { lightGroup[i].Position = 53; continue; }
                    else if (lightGroup[i].Position == 52) { lightGroup[i].Position = 56; continue; }
                    else if (lightGroup[i].Position == 53) { lightGroup[i].Position = 57; continue; }
                    else if (lightGroup[i].Position == 56) { lightGroup[i].Position = 60; continue; }
                    else if (lightGroup[i].Position == 57) { lightGroup[i].Position = 61; continue; }
                    else if (lightGroup[i].Position == 60) { lightGroup[i].Position = 64; continue; }
                    else if (lightGroup[i].Position == 64) { lightGroup[i].Position = 65; continue; }
                    else if (lightGroup[i].Position == 65) { lightGroup[i].Position = 66; continue; }
                    else if (lightGroup[i].Position == 61) { lightGroup[i].Position = 62; continue; }
                    else if (lightGroup[i].Position == 54) { lightGroup[i].Position = 58; continue; }

                    //左上
                    else if (lightGroup[i].Position == 66) { lightGroup[i].Position = 67; continue; }
                    else if (lightGroup[i].Position == 62) { lightGroup[i].Position = 63; continue; }
                    else if (lightGroup[i].Position == 58) { lightGroup[i].Position = 59; continue; }
                    else if (lightGroup[i].Position == 55) { lightGroup[i].Position = 55; continue; }

                    else if (lightGroup[i].Position == 59) { lightGroup[i].Position = 88; continue; }
                    else if (lightGroup[i].Position == 67) { lightGroup[i].Position = 96; continue; }
                    else if (lightGroup[i].Position == 63) { lightGroup[i].Position = 92; continue; }
                    else if (lightGroup[i].Position == 96) { lightGroup[i].Position = 97; continue; }
                    else if (lightGroup[i].Position == 92) { lightGroup[i].Position = 93; continue; }
                    else if (lightGroup[i].Position == 97) { lightGroup[i].Position = 98; continue; }
                    else if (lightGroup[i].Position == 93) { lightGroup[i].Position = 94; continue; }
                    else if (lightGroup[i].Position == 98) { lightGroup[i].Position = 99; continue; }
                    else if (lightGroup[i].Position == 99) { lightGroup[i].Position = 95; continue; }
                    else if (lightGroup[i].Position == 95) { lightGroup[i].Position = 91; continue; }
                    else if (lightGroup[i].Position == 94) { lightGroup[i].Position = 90; continue; }
                    else if (lightGroup[i].Position == 88) { lightGroup[i].Position = 89; continue; }
                    //右下
                    else if (lightGroup[i].Position == 69) { lightGroup[i].Position = 68; continue; }
                    else if (lightGroup[i].Position == 73) { lightGroup[i].Position = 72; continue; }
                    else if (lightGroup[i].Position == 77) { lightGroup[i].Position = 76; continue; }
                    else if (lightGroup[i].Position == 80) { lightGroup[i].Position = 80; continue; }

                    else if (lightGroup[i].Position == 76) { lightGroup[i].Position = 47; continue; }
                    else if (lightGroup[i].Position == 68) { lightGroup[i].Position = 39; continue; }
                    else if (lightGroup[i].Position == 72) { lightGroup[i].Position = 43; continue; }
                    else if (lightGroup[i].Position == 39) { lightGroup[i].Position = 38; continue; }
                    else if (lightGroup[i].Position == 43) { lightGroup[i].Position = 42; continue; }
                    else if (lightGroup[i].Position == 38) { lightGroup[i].Position = 37; continue; }
                    else if (lightGroup[i].Position == 42) { lightGroup[i].Position = 41; continue; }
                    else if (lightGroup[i].Position == 37) { lightGroup[i].Position = 36; continue; }
                    else if (lightGroup[i].Position == 36) { lightGroup[i].Position = 40; continue; }
                    else if (lightGroup[i].Position == 40) { lightGroup[i].Position = 44; continue; }
                    else if (lightGroup[i].Position == 41) { lightGroup[i].Position = 45; continue; }
                    else if (lightGroup[i].Position == 47) { lightGroup[i].Position = 46; continue; }
                    //右上
                    else if (lightGroup[i].Position == 91) { lightGroup[i].Position = 87; continue; }
                    else if (lightGroup[i].Position == 90) { lightGroup[i].Position = 86; continue; }
                    else if (lightGroup[i].Position == 89) { lightGroup[i].Position = 85; continue; }
                    else if (lightGroup[i].Position == 84) { lightGroup[i].Position = 84; continue; }

                    else if (lightGroup[i].Position == 85) { lightGroup[i].Position = 81; continue; }
                    else if (lightGroup[i].Position == 87) { lightGroup[i].Position = 83; continue; }
                    else if (lightGroup[i].Position == 86) { lightGroup[i].Position = 82; continue; }
                    else if (lightGroup[i].Position == 83) { lightGroup[i].Position = 79; continue; }
                    else if (lightGroup[i].Position == 82) { lightGroup[i].Position = 78; continue; }
                    else if (lightGroup[i].Position == 79) { lightGroup[i].Position = 75; continue; }
                    else if (lightGroup[i].Position == 78) { lightGroup[i].Position = 74; continue; }
                    else if (lightGroup[i].Position == 75) { lightGroup[i].Position = 71; continue; }
                    else if (lightGroup[i].Position == 71) { lightGroup[i].Position = 70; continue; }
                    else if (lightGroup[i].Position == 70) { lightGroup[i].Position = 69; continue; }
                    else if (lightGroup[i].Position == 74) { lightGroup[i].Position = 73; continue; }
                    else if (lightGroup[i].Position == 81) { lightGroup[i].Position = 77; continue; }
                }
                if (count == 6)
                {
                    //左下
                    if (lightGroup[i].Position == 48) { lightGroup[i].Position = 52; continue; }
                    else if (lightGroup[i].Position == 49) { lightGroup[i].Position = 53; continue; }
                    else if (lightGroup[i].Position == 50) { lightGroup[i].Position = 54; continue; }
                    else if (lightGroup[i].Position == 51) { lightGroup[i].Position = 55; continue; }

                    else if (lightGroup[i].Position == 58) { lightGroup[i].Position = 59; continue; }
                    else if (lightGroup[i].Position == 54) { lightGroup[i].Position = 58; continue; }
                    else if (lightGroup[i].Position == 53) { lightGroup[i].Position = 57; continue; }
                    else if (lightGroup[i].Position == 52) { lightGroup[i].Position = 56; continue; }
                    else if (lightGroup[i].Position == 62) { lightGroup[i].Position = 63; continue; }
                    else if (lightGroup[i].Position == 61) { lightGroup[i].Position = 62; continue; }
                    else if (lightGroup[i].Position == 57) { lightGroup[i].Position = 61; continue; }
                    else if (lightGroup[i].Position == 56) { lightGroup[i].Position = 60; continue; }
                    else if (lightGroup[i].Position == 66) { lightGroup[i].Position = 67; continue; }
                    else if (lightGroup[i].Position == 65) { lightGroup[i].Position = 66; continue; }
                    else if (lightGroup[i].Position == 64) { lightGroup[i].Position = 65; continue; }
                    else if (lightGroup[i].Position == 60) { lightGroup[i].Position = 64; continue; }
                    //左上
                    else if (lightGroup[i].Position == 67) { lightGroup[i].Position = 96; continue; }
                    else if (lightGroup[i].Position == 63) { lightGroup[i].Position = 92; continue; }
                    else if (lightGroup[i].Position == 59) { lightGroup[i].Position = 88; continue; }
                    else if (lightGroup[i].Position == 55) { lightGroup[i].Position = 84; continue; }

                    else if (lightGroup[i].Position == 89) { lightGroup[i].Position = 85; continue; }
                    else if (lightGroup[i].Position == 88) { lightGroup[i].Position = 89; continue; }
                    else if (lightGroup[i].Position == 92) { lightGroup[i].Position = 93; continue; }
                    else if (lightGroup[i].Position == 96) { lightGroup[i].Position = 97; continue; }
                    else if (lightGroup[i].Position == 97) { lightGroup[i].Position = 98; continue; }
                    else if (lightGroup[i].Position == 93) { lightGroup[i].Position = 94; continue; }
                    else if (lightGroup[i].Position == 94) { lightGroup[i].Position = 90; continue; }
                    else if (lightGroup[i].Position == 90) { lightGroup[i].Position = 86; continue; }
                    else if (lightGroup[i].Position == 98) { lightGroup[i].Position = 99; continue; }
                    else if (lightGroup[i].Position == 99) { lightGroup[i].Position = 95; continue; }
                    else if (lightGroup[i].Position == 95) { lightGroup[i].Position = 91; continue; }
                    else if (lightGroup[i].Position == 91) { lightGroup[i].Position = 87; continue; }
                    //右下
                    else if (lightGroup[i].Position == 80) { lightGroup[i].Position = 51; continue; }
                    else if (lightGroup[i].Position == 76) { lightGroup[i].Position = 47; continue; }
                    else if (lightGroup[i].Position == 72) { lightGroup[i].Position = 43; continue; }
                    else if (lightGroup[i].Position == 68) { lightGroup[i].Position = 39; continue; }

                    else if (lightGroup[i].Position == 46) { lightGroup[i].Position = 50; continue; }
                    else if (lightGroup[i].Position == 47) { lightGroup[i].Position = 46; continue; }
                    else if (lightGroup[i].Position == 43) { lightGroup[i].Position = 42; continue; }
                    else if (lightGroup[i].Position == 39) { lightGroup[i].Position = 38; continue; }
                    else if (lightGroup[i].Position == 45) { lightGroup[i].Position = 49; continue; }
                    else if (lightGroup[i].Position == 41) { lightGroup[i].Position = 45; continue; }
                    else if (lightGroup[i].Position == 42) { lightGroup[i].Position = 41; continue; }
                    else if (lightGroup[i].Position == 38) { lightGroup[i].Position = 37; continue; }
                    else if (lightGroup[i].Position == 44) { lightGroup[i].Position = 48; continue; }
                    else if (lightGroup[i].Position == 40) { lightGroup[i].Position = 44; continue; }
                    else if (lightGroup[i].Position == 36) { lightGroup[i].Position = 40; continue; }
                    else if (lightGroup[i].Position == 37) { lightGroup[i].Position = 36; continue; }
                    //右上
                    else if (lightGroup[i].Position == 84) { lightGroup[i].Position = 80; continue; }
                    else if (lightGroup[i].Position == 85) { lightGroup[i].Position = 81; continue; }
                    else if (lightGroup[i].Position == 86) { lightGroup[i].Position = 82; continue; }
                    else if (lightGroup[i].Position == 87) { lightGroup[i].Position = 83; continue; }

                    else if (lightGroup[i].Position == 77) { lightGroup[i].Position = 76; continue; }
                    else if (lightGroup[i].Position == 81) { lightGroup[i].Position = 77; continue; }
                    else if (lightGroup[i].Position == 82) { lightGroup[i].Position = 78; continue; }
                    else if (lightGroup[i].Position == 83) { lightGroup[i].Position = 79; continue; }
                    else if (lightGroup[i].Position == 73) { lightGroup[i].Position = 72; continue; }
                    else if (lightGroup[i].Position == 74) { lightGroup[i].Position = 73; continue; }
                    else if (lightGroup[i].Position == 78) { lightGroup[i].Position = 74; continue; }
                    else if (lightGroup[i].Position == 79) { lightGroup[i].Position = 75; continue; }
                    else if (lightGroup[i].Position == 69) { lightGroup[i].Position = 68; continue; }
                    else if (lightGroup[i].Position == 70) { lightGroup[i].Position = 69; continue; }
                    else if (lightGroup[i].Position == 71) { lightGroup[i].Position = 70; continue; }
                    else if (lightGroup[i].Position == 75) { lightGroup[i].Position = 71; continue; }
                }
            }
            return lightGroup;
        }
    }
}
