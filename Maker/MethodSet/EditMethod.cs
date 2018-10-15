using Maker.Business;
using Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Maker.MethodSet
{
    public static class EditMethod
    {
        /// <summary>
        /// 主方法，用于派发创建任务
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name="lightGroup"></param>
        /// <returns></returns>
        public static List<Light> EditMain(String commandLine, List<Light> lightGroup, Dictionary<String, List<int>> rangeGroupDictionary, Dictionary<String, List<int>> colorGroupDictionary,List<ThirdPartyModel> thirdPartyModelList)
        {
            List<Light> _lightGroup = LightBusiness.Copy(lightGroup);
          
            Regex P_AntiClockwise = new Regex(@"\s*AntiClockwise\([0-9a-zA-Z_\u4e00-\u9fa5]{1,}\)");
            if (P_AntiClockwise.IsMatch(commandLine))
            {
                return AntiClockwise(_lightGroup);
            }
            Regex P_Clockwise = new Regex(@"\s*Clockwise\([0-9a-zA-Z_\u4e00-\u9fa5]{1,}\)");
            if (P_Clockwise.IsMatch(commandLine))
            {
                return Clockwise(_lightGroup);
            }
            Regex P_Reversal = new Regex(@"\s*Reversal\([0-9a-zA-Z_\u4e00-\u9fa5]{1,}\)");
            if (P_Reversal.IsMatch(commandLine))
            {
                return Reversal(_lightGroup);
            }
            Regex P_RemoveBorder = new Regex(@"\s*RemoveBorder\([0-9a-zA-Z_\u4e00-\u9fa5]{1,}\)");
            if (P_RemoveBorder.IsMatch(commandLine))
            {
                return RemoveBorder(_lightGroup);
            }
            Regex P_ChangeTime = new Regex(@"\s*ChangeTime\([\S\s]*\)");
            if (P_ChangeTime.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(')+1);
                content = content.Substring(0, content.IndexOf(')'));
                String[] parameters = content.Split(',');
                if (parameters.Count() != 3) {
                    return null;
                }
                if (Convert.ToInt32(parameters[1]) == (int)Operator.Multiplication)
                {
                    return ChangeTime(_lightGroup, Operator.Multiplication, Convert.ToDouble(parameters[2]));
                }
                else if (Convert.ToInt32(parameters[1]) == (int)Operator.Division) {
                    return ChangeTime(_lightGroup, Operator.Division, Convert.ToDouble(parameters[2]));
                }
            }
            Regex P_ShapeColor = new Regex(@"\s*ShapeColor\([\S\s]*\)");
            if (P_ShapeColor.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                String[] parameters = content.Split(',');
                if (parameters.Count() != 3)
                {
                    return null;
                }
                if (parameters[1].Equals("Square"))
                {
                    return ShapeColor(_lightGroup, ShapeColorType.Square, parameters[2].Substring(1, parameters[2].Length-2));
                }
                else if (parameters[1].Equals("RadialVertical"))
                {
                    return ShapeColor(_lightGroup, ShapeColorType.RadialVertical, parameters[2].Substring(1, parameters[2].Length - 2));
                }
                else if (parameters[1].Equals("RadialHorizontal"))
                {
                    return ShapeColor(_lightGroup, ShapeColorType.RadialHorizontal, parameters[2].Substring(1, parameters[2].Length - 2));
                }
            }
            Regex P_CopyToTheEnd = new Regex(@"\s*CopyToTheEnd\([\S\s]*\)");
            if (P_CopyToTheEnd.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                //如果包含逗号
                if (content.Contains(','))
                {
                    String[] parameters = content.Split(',');
                    if (parameters.Count() != 2 )
                    {
                        return null;
                    }

                    if (colorGroupDictionary.ContainsKey(parameters[1].Trim()))
                    {
                       return CopyToTheEnd(_lightGroup, colorGroupDictionary[parameters[1].Trim()]);
                    }
                    else
                    {
                        return null;
                    }
                }
                else {
                    return CopyToTheEnd(_lightGroup, new List<int>());
                }
            }
            Regex P_CopyToTheFollow = new Regex(@"\s*CopyToTheFollow\([\S\s]*\)");
            if (P_CopyToTheFollow.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                //如果包含逗号
                if (content.Contains(','))
                {
                    String[] parameters = content.Split(',');
                    if (parameters.Count() != 2)
                    {
                        return null;
                    }
                    if (colorGroupDictionary.ContainsKey(parameters[1].Trim()))
                    {
                        return CopyToTheFollow(_lightGroup, colorGroupDictionary[parameters[1].Trim()]);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return CopyToTheFollow(_lightGroup, new List<int>());
                }
            }
            Regex P_AccelerationOrDeceleration = new Regex(@"\s*AccelerationOrDeceleration\([\S\s]*\)");
            if (P_AccelerationOrDeceleration.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                //如果包含逗号
                if (content.Contains(','))
                {
                    String[] parameters = content.Split(',');
                    if (parameters.Count() != 2)
                    {
                        return null;
                    }
                    if (rangeGroupDictionary.ContainsKey(parameters[1].Trim()))
                    {
                        return AccelerationOrDeceleration(_lightGroup, rangeGroupDictionary[parameters[1].Trim()]);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            Regex P_IfThen = new Regex(@"\s*IfThen\([\S\s]*\)");
            if (P_IfThen.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                return IfThen(_lightGroup, content,rangeGroupDictionary);
            }
            Regex P_SetEndTime = new Regex(@"\s*SetEndTime\([\S\s]*\)");
            if (P_SetEndTime.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                String[] parameters = content.Split(',');
                if (parameters.Count() != 3)
                {
                    return null;
                }
                return SetEndTime(_lightGroup, parameters[1],parameters[2]);
            }
            Regex P_FillColor = new Regex(@"\s*FillColor\([\S\s]*\)");
            if (P_FillColor.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                String[] parameters = content.Split(',');
                if (parameters.Count() != 2)
                {
                    return null;
                }
                return FillColor(_lightGroup, int.Parse(parameters[1]));
            }
            Regex P_Animation = new Regex(@"\s*Animation\([\S\s]*\)");
            if (P_Animation.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                String[] parameters = content.Split(',');
                if (parameters[1].Equals("Serpentine")) {
                    if (parameters.Count() != 4)
                    {
                        return null;
                    }
                    return Serpentine(_lightGroup, int.Parse(parameters[2]), int.Parse(parameters[3]));
                }
                if (parameters[1].Equals("Windmill"))
                {
                    if (parameters.Count() != 3)
                    {
                        return null;
                    }
                    return Windmill(_lightGroup, int.Parse(parameters[2]));
                }
            }
            Regex P_Fold = new Regex(@"\s*Fold\([\S\s]*\)");
            if (P_Fold.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                String[] parameters = content.Split(',');
                if (parameters.Count() != 4)
                {
                    return null;
                }
                return Fold(_lightGroup, parameters[1], int.Parse(parameters[2]), int.Parse(parameters[3]));
            }
            Regex P_MatchTotalTimeLattice = new Regex(@"\s*MatchTotalTimeLattice\([\S\s]*\)");
            if (P_MatchTotalTimeLattice.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                String[] parameters = content.Split(',');
                if (parameters.Count() != 2)
                {
                    return null;
                }
                return MatchTotalTimeLattice(_lightGroup, int.Parse(parameters[1]));
            }
            Regex P_ColorWithCount = new Regex(@"\s*ColorWithCount\([\S\s]*\)");
            if (P_ColorWithCount.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                String[] parameters = content.Split(',');
                if (parameters.Count() != 2)
                {
                    return null;
                }
                String[] strs = parameters[1].Substring(1, parameters[1].Length - 2).Trim().Split(' ');
                List<int> mIntList = new List<int>();
                foreach (String str in strs) {
                    mIntList.Add(int.Parse(str));
                }
                return ColorWithCount(_lightGroup, mIntList);
            }
            Regex P_InterceptTime = new Regex(@"\s*InterceptTime\([\S\s]*\)");
            if (P_InterceptTime.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                String[] parameters = content.Split(',');
                if (parameters.Count() != 3)
                {
                    return null;
                }
                return InterceptTime(_lightGroup, int.Parse(parameters[1]),int.Parse(parameters[2]));
            }

            //第三方
            String thirdPartyName = commandLine.Substring(0,commandLine.IndexOf('(')).Trim();
            String dllFilePath = String.Empty;
            foreach (var item in thirdPartyModelList) {
                if (item.name.Equals(thirdPartyName))
                {
                    dllFilePath = item.dll;
                    break;
                }
            }
            if (!dllFilePath.Equals(String.Empty)) {
                //Type type = ass.GetType("HorizontalFlipping.HorizontalFlipping");//程序集下所有的类
                //Executable废弃该文件夹
                Assembly ass = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + @"Operation\Dll\"+ dllFilePath + ".dll");
                Type[] types = ass.GetTypes();
                Type type = null;
                foreach (Type t in types) {
                    if(t.ToString().Contains("."+ thirdPartyName)) {
                        type = t;
                        break;
                    }
                }
                if (type == null)
                    return null;
                
                //判断是否继承于IGetOperationResult类
                Type _type = type.GetInterface("Operation.IGetOperationResult");
                if (_type == null)
                    return null;
                Object o = Activator.CreateInstance(type);
                MethodInfo mi = o.GetType().GetMethod("GetOperationResult");
                List<Operation.Light> lol = new List<Operation.Light>();
                foreach (Light l in _lightGroup)
                {
                    lol.Add(new Operation.Light(l.Time, l.Action, l.Position, l.Color));
                }

                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));

                if (content.Contains(','))
                {
                    lol = (List<Operation.Light>)mi.Invoke(o, new Object[] { lol, content.Substring(content.IndexOf(',')+1) });
                }
                else {
                    lol = (List<Operation.Light>)mi.Invoke(o, new Object[] { lol,""});
                }

                _lightGroup = new List<Light>();
                foreach (Operation.Light l in lol)
                {
                    _lightGroup.Add(new Light(l.Time, l.Action, l.Position, l.Color));
                }
                return _lightGroup;
            }
            return null;
        }
        /// <summary>
        /// 截取时间内的灯光
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static List<Light> InterceptTime(List<Light> lightGroup, int min, int max)
        {
            lightGroup = LightBusiness.SortCouple(lightGroup);
            int _max;
            if (max == -1)
                _max = LightBusiness.GetMax(lightGroup);
              else
                _max = max;
            int _min;
            if (min == -1)
                _min = LightBusiness.GetMin(lightGroup);
            else
                _min = min;
            List<Light> listLight = new List<Light>();
            for (int i = 0; i<lightGroup.Count;i++)
            {
                if (lightGroup[i].Time >= min && lightGroup[i].Time <= max) {
                    listLight.Add(new Light(lightGroup[i].Time, lightGroup[i].Action, lightGroup[i].Position, lightGroup[i].Color));
                }
                else if (lightGroup[i].Time < min && lightGroup[i].Action == 144)
                {
                    if (lightGroup[i + 1].Time >= min && lightGroup[i + 1].Action == 128) {
                        listLight.Add(new Light(min, lightGroup[i].Action, lightGroup[i].Position, lightGroup[i].Color));
                    }
                }
                if (lightGroup[i].Time > max && lightGroup[i].Action == 128)
                {
                    if (i == 0)
                        continue;
                    if (lightGroup[i - 1].Time <= max && lightGroup[i - 1].Action == 144)
                    {
                        listLight.Add(new Light(max, lightGroup[i].Action, lightGroup[i].Position, lightGroup[i].Color));
                    }
                }
            }
            return listLight;
        }

        /// <summary>
        /// 根据次数变换颜色
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <param name="colorList"></param>
        /// <returns></returns>
        private static List<Light> ColorWithCount(List<Light> lightGroup, List<int> colorList)
        {
            lightGroup = LightBusiness.SortCouple(lightGroup);
            int i = 0;
            int nowPosition = -1;
            int colorCount = colorList.Count;
            foreach (Light l in lightGroup) {
                if (nowPosition == -1)
                    nowPosition = l.Position;
                if (l.Position == nowPosition ) {
                    if (l.Action == 144) {
                        l.Color = colorList[i];
                        i = (i + 1) % colorCount;
                    }
                }
                else {
                    i = 0;
                    nowPosition = l.Position;
                    if (l.Action == 144)
                    {
                        l.Color = colorList[i];
                        i = (i + 1) % colorCount;
                    }
                }
            }
            return lightGroup;
        }

      

        private static List<Light> Fold(List<Light> lightGroup, string orientation, int startPosition, int span)
        {
            if (startPosition <= 0 || startPosition >= 10)
                return null;
            List<List<int>> mList = new List<List<int>>();
            if (orientation.Equals("Horizontal"))
            {
                mList.AddRange(IntCollection.HorizontalIntList.ToList());
            }
            else if (orientation.Equals("Vertical"))
            {
                mList.AddRange(IntCollection.VerticalIntList.ToList());
            }
            else {
                return null;
            }
            int _Min = startPosition - span < 0 ? _Min = startPosition :100;
            int _Max = startPosition + span > 9? 10 - startPosition : 100;
            int mMin = _Min < _Max ? _Min : _Max;
            int min,max;
            if (_Min != 100 && _Max != 100)
            {
                min = startPosition - span < 0 ? 0 : startPosition - span;
                max = startPosition + span > 9 ? 9 : startPosition + span;
            }
            else {
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
            foreach (Light l in lightGroup)
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
            RemoveIncorrectlyData(lightGroup);
            return lightGroup;

        }

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
        
       private static List<Light> Windmill(List<Light> lightGroup, int interval)
        {
            lightGroup = LightBusiness.Copy(lightGroup);
            List<Light> _lightGroup = LightBusiness.Copy(lightGroup);
            for (int i = 0; i < 7; i++) {
                _lightGroup = ToWindmill(_lightGroup, interval,i);
                lightGroup.AddRange(_lightGroup);
            }
            return lightGroup;
        }
        private static List<Light> ToWindmill(List<Light> lightGroup, int interval,int count) {
            lightGroup = LightBusiness.Copy(lightGroup);
            for (int i = 0; i < lightGroup.Count; i++) {
                lightGroup[i].Time += interval;
                if (count == 0) {
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

        private static List<Light> Serpentine(List<Light> lightGroup, int startTime, int interval)
        {
            lightGroup = LightBusiness.Sort(lightGroup);
            List<int> list = new List<int>(){ 64,65,66,67,96,97,98,99,
                                  95,94,93,92,63,62,61,60,
                                  56,57,58,59,88,89,90,91,
                                  87,86,85,84,55,54,53,52,
                                  48,49,50,51,80,81,82,83,
                                  79,78,77,76,47,46,45,44,
                                  40,41,42,43,72,73,74,75,
                                  71,70,69,68,39,38,37,36};

            for (int i = 0; i < list.Count(); i++) {
                int time = startTime + interval * i;
                for(int j = 0; j < lightGroup.Count; j++) {
                    if (lightGroup[j].Action == 128 && lightGroup[j].Time>time && lightGroup[j].Position == list[i]) {
                        //需要消除的
                        lightGroup[j].Time = time;
                    }    
                }
            }
            lightGroup = LightBusiness.SortCouple(lightGroup);
            bool b = false;
            for(int i = lightGroup.Count - 1; i >= 0; i--) {
                if (b) {
                    lightGroup.RemoveAt(i);
                    b = false;
                }
                if (i % 2 == 1) {
                    if (lightGroup[i].Time == lightGroup[i - 1].Time && lightGroup[i].Time == startTime + interval * list.IndexOf(lightGroup[i].Position))
                    {
                        lightGroup.RemoveAt(i);
                        b = true;
                    }
                }
            }
            lightGroup = LightBusiness.Sort(lightGroup);
            return lightGroup;
        }

        private static List<Light> FillColor(List<Light> lightGroup, int v)
        {
            lightGroup = LightBusiness.Sort(lightGroup);
            int max = LightBusiness.GetMax(lightGroup);
            List<Light> mLl = new List<Light>();
            for(int i = 28; i <= 123; i++) {
                int nowTime = 0;
                for (int j = 0; j < lightGroup.Count; j++)
                {
                    if (lightGroup[j].Position == i) {
                        //如果是开始
                        if (lightGroup[j].Action == 144) {
                            //时间大于nowTime
                            if (lightGroup[j].Time > nowTime) {
                                //填充一组
                                mLl.Add(new Light(nowTime, 144, i, v));
                                mLl.Add(new Light(lightGroup[j].Time, 128, i, 64));
                            }
                        }
                        if (lightGroup[j].Action == 128) {
                            nowTime = lightGroup[j].Time;
                        }
                    }
                }
                if (nowTime < max) {
                    mLl.Add(new Light(nowTime, 144, i, v));
                    mLl.Add(new Light(max, 128, i, 64));
                }
            }
           lightGroup.AddRange(mLl.ToList());
            return lightGroup;
        }

        private static List<Light> MatchTotalTimeLattice(List<Light> lightGroup, int v)
        {
            lightGroup = LightBusiness.Sort(lightGroup);
            int max = LightBusiness.GetMax(lightGroup);
            double d = (double)v / max;
            for (int i = 0; i < lightGroup.Count; i++) {
                int result = (int)Math.Round(lightGroup[i].Time * d, MidpointRounding.AwayFromZero);
                if (result > v)
                    result--;
                lightGroup[i].Time = result; 
            }
            return lightGroup;
        }

        private static List<Light> SetEndTime(List<Light> lightGroup, string v1, string v2)
        {
            if (v1.Equals("All")) {
                if (v2.Contains("+")) {
                    if (!int.TryParse(v2.Substring(1),out int number)) {
                        return null;
                    }
                    foreach (Light l in lightGroup) {
                        if (l.Action == 128)
                            l.Time += number;
                    }
                    return lightGroup;
                }
                else if (v2.Contains("-"))
                {
                    if (!int.TryParse(v2.Substring(1), out int number))
                    {
                        return null;
                    }
                    foreach (Light l in lightGroup)
                    {
                        if (l.Action == 128)
                            l.Time -= number;
                    }
                    return lightGroup;
                }
                else {
                    if (!int.TryParse(v2, out int number))
                    {
                        return null;
                    }
                    foreach (Light l in lightGroup)
                    {
                        if (l.Action == 128)
                            l.Time = number;
                    }
                    return lightGroup;
                }
            }
            else if (v1.Equals("End"))
            {
                int time = -1;
                foreach (Light l in lightGroup) {
                    if (l.Time > time && l.Action == 128) {
                        time = l.Time;
                    }
                }
                if (v2.Contains("+"))
                {
                    if (!int.TryParse(v2.Substring(1), out int number))
                    {
                        return null;
                    }
                    foreach (Light l in lightGroup)
                    {
                        if(l.Time == time && l.Action == 128)
                            l.Time += number;
                    }
                    return lightGroup;
                }
                else if (v2.Contains("-"))
                {
                    if (!int.TryParse(v2.Substring(1), out int number))
                    {
                        return null;
                    }
                    foreach (Light l in lightGroup)
                    {
                        if (l.Time == time && l.Action == 128)
                            l.Time -= number;
                    }
                    return lightGroup;
                }
                else
                {
                    if (!int.TryParse(v2, out int number))
                    {
                        return null;
                    }
                    foreach (Light l in lightGroup)
                    {
                        if (l.Time == time && l.Action == 128)
                            l.Time = number;
                    }
                    return lightGroup;
                }
            }
            else if (v1.Equals("AllAndEnd"))
            {
                List<Light> mLl = new List<Light>();
                int position = -1;
                int time = -1;
                for (int i = 28; i <= 123; i++)
                {
                    position = -1;
                    time = -1;
                    for (int j = 0; j < lightGroup.Count; j++)
                    {
                        if (lightGroup[j].Position == i && lightGroup[j].Action == 128)
                        {
                            if (lightGroup[j].Time > time)
                            {
                                position = j;
                                time = lightGroup[j].Time;
                            }
                        }
                    }
                    if (position > -1)
                        mLl.Add(lightGroup[position]);
                }
                if (v2.Contains("+"))
                {
                    if (!int.TryParse(v2.Substring(1), out int number))
                    {
                        return null;
                    }
                    foreach (Light l in mLl)
                    {
                        l.Time += number;
                    }
                    return lightGroup;
                }
                else if (v2.Contains("-"))
                {
                    if (!int.TryParse(v2.Substring(1), out int number))
                    {
                        return null;
                    }
                    foreach (Light l in mLl)
                    {
                        l.Time -= number;
                    }
                    return lightGroup;
                }
                else
                {
                    if (!int.TryParse(v2, out int number))
                    {
                        return null;
                    }
                    foreach (Light l in mLl)
                    {
                        l.Time = number;
                    }
                    return lightGroup;
                }
            }
            return null;
        }

        public static List<Light> ShapeColor(List<Light> lightGroup, ShapeColorType _type, string v)
        {
         
            String[] strs = v.Split(' ');
            List<int> _numbers = new List<int>();
          
            foreach (String str in strs)
            {
                try
                {
                    if (!str.Equals(String.Empty))
                    {
                        _numbers.Add(Convert.ToInt32(str));
                    }
                }
                catch
                {
                    return null;
                }
            }
           
            //方形
            if (_type == ShapeColorType.Square) {
                if (_numbers.Count != 5)
                {
                    return null;
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
                if (_numbers[0] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[0].Contains(l.Position))
                        {
                            l.Color = _numbers[0];
                        }
                    }
                }
                if (_numbers[1] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[1].Contains(l.Position))
                        {
                            l.Color = _numbers[1];
                        }
                    }
                }
                if (_numbers[2] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[2].Contains(l.Position))
                        {
                            l.Color = _numbers[2];
                        }
                    }
                }
                if (_numbers[3] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[3].Contains(l.Position))
                        {
                            l.Color = _numbers[3];
                        }
                    }
                }
                if (_numbers[4] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[4].Contains(l.Position))
                        {
                            l.Color = _numbers[4];
                        }
                    }
                }
            }
            //垂直径向
            if (_type == ShapeColorType.RadialVertical)
            {
                if (_numbers.Count != 10)
                {
                    return null;
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
                if (_numbers[0] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[0].Contains(l.Position))
                        {
                            l.Color = _numbers[0];
                        }
                    }
                }
                if (_numbers[1] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[1].Contains(l.Position))
                        {
                            l.Color = _numbers[1];
                        }
                    }
                }
                if (_numbers[2] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[2].Contains(l.Position))
                        {
                            l.Color = _numbers[2];
                        }
                    }
                }
                if (_numbers[3] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[3].Contains(l.Position))
                        {
                            l.Color = _numbers[3];
                        }
                    }
                }
                if (_numbers[4] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[4].Contains(l.Position))
                        {
                            l.Color = _numbers[4];
                        }
                    }
                }
                if (_numbers[5] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[5].Contains(l.Position))
                        {
                            l.Color = _numbers[5];
                        }
                    }
                }
                if (_numbers[6] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[6].Contains(l.Position))
                        {
                            l.Color = _numbers[6];
                        }
                    }
                }
                if (_numbers[7] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[7].Contains(l.Position))
                        {
                            l.Color = _numbers[7];
                        }
                    }
                }
                if (_numbers[8] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[8].Contains(l.Position))
                        {
                            l.Color = _numbers[8];
                        }
                    }
                }
                if (_numbers[9] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[9].Contains(l.Position))
                        {
                            l.Color = _numbers[9];
                        }
                    }
                }
            }    
            //水平径向
            if (_type == ShapeColorType.RadialHorizontal)
            {
                if (_numbers.Count != 10)
                {
                    return null;
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
                if (_numbers[0] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[0].Contains(l.Position))
                        {
                            l.Color = _numbers[0];
                        }
                    }
                }
                if (_numbers[1] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[1].Contains(l.Position))
                        {
                            l.Color = _numbers[1];
                        }
                    }
                }
                if (_numbers[2] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[2].Contains(l.Position))
                        {
                            l.Color = _numbers[2];
                        }
                    }
                }
                if (_numbers[3] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[3].Contains(l.Position))
                        {
                            l.Color = _numbers[3];
                        }
                    }
                }
                if (_numbers[4] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[4].Contains(l.Position))
                        {
                            l.Color = _numbers[4];
                        }
                    }
                }
                if (_numbers[5] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[5].Contains(l.Position))
                        {
                            l.Color = _numbers[5];
                        }
                    }
                }
                if (_numbers[6] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[6].Contains(l.Position))
                        {
                            l.Color = _numbers[6];
                        }
                    }
                }
                if (_numbers[7] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[7].Contains(l.Position))
                        {
                            l.Color = _numbers[7];
                        }
                    }
                }
                if (_numbers[8] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[8].Contains(l.Position))
                        {
                            l.Color = _numbers[8];
                        }
                    }
                }
                if (_numbers[9] != 0)
                {
                    foreach (Light l in lightGroup)
                    {
                        if (lli[9].Contains(l.Position))
                        {
                            l.Color = _numbers[9];
                        }
                    }
                }
            }
            return lightGroup;
        }


   
      
   
        /// <summary>
        /// 顺时针
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <returns></returns>
        public static List<Light> Clockwise(List<Light> lightGroup)
        {
            #region
            for (int k = 0; k < lightGroup.Count; k++)
            {
                //左下
                if (lightGroup[k].Position == 36) { lightGroup[k].Position = 64; continue; }
                if (lightGroup[k].Position == 37) { lightGroup[k].Position = 60; continue; }
                if (lightGroup[k].Position == 38) { lightGroup[k].Position = 56; continue; }
                if (lightGroup[k].Position == 39) { lightGroup[k].Position = 52; continue; }

                if (lightGroup[k].Position == 40) { lightGroup[k].Position = 65; continue; }
                if (lightGroup[k].Position == 41) { lightGroup[k].Position = 61; continue; }
                if (lightGroup[k].Position == 42) { lightGroup[k].Position = 57; continue; }
                if (lightGroup[k].Position == 43) { lightGroup[k].Position = 53; continue; }

                if (lightGroup[k].Position == 44) { lightGroup[k].Position = 66; continue; }
                if (lightGroup[k].Position == 45) { lightGroup[k].Position = 62; continue; }
                if (lightGroup[k].Position == 46) { lightGroup[k].Position = 58; continue; }
                if (lightGroup[k].Position == 47) { lightGroup[k].Position = 54; continue; }

                if (lightGroup[k].Position == 48) { lightGroup[k].Position = 67; continue; }
                if (lightGroup[k].Position == 49) { lightGroup[k].Position = 63; continue; }
                if (lightGroup[k].Position == 50) { lightGroup[k].Position = 59; continue; }
                if (lightGroup[k].Position == 51) { lightGroup[k].Position = 55; continue; }

                //左上
                if (lightGroup[k].Position == 52) { lightGroup[k].Position = 96; continue; }
                if (lightGroup[k].Position == 53) { lightGroup[k].Position = 92; continue; }
                if (lightGroup[k].Position == 54) { lightGroup[k].Position = 88; continue; }
                if (lightGroup[k].Position == 55) { lightGroup[k].Position = 84; continue; }

                if (lightGroup[k].Position == 56) { lightGroup[k].Position = 97; continue; }
                if (lightGroup[k].Position == 57) { lightGroup[k].Position = 93; continue; }
                if (lightGroup[k].Position == 58) { lightGroup[k].Position = 89; continue; }
                if (lightGroup[k].Position == 59) { lightGroup[k].Position = 85; continue; }

                if (lightGroup[k].Position == 60) { lightGroup[k].Position = 98; continue; }
                if (lightGroup[k].Position == 61) { lightGroup[k].Position = 94; continue; }
                if (lightGroup[k].Position == 62) { lightGroup[k].Position = 90; continue; }
                if (lightGroup[k].Position == 63) { lightGroup[k].Position = 86; continue; }

                if (lightGroup[k].Position == 64) { lightGroup[k].Position = 99; continue; }
                if (lightGroup[k].Position == 65) { lightGroup[k].Position = 95; continue; }
                if (lightGroup[k].Position == 66) { lightGroup[k].Position = 91; continue; }
                if (lightGroup[k].Position == 67) { lightGroup[k].Position = 87; continue; }

                //右下
                if (lightGroup[k].Position == 68) { lightGroup[k].Position = 48; continue; }
                if (lightGroup[k].Position == 69) { lightGroup[k].Position = 44; continue; }
                if (lightGroup[k].Position == 70) { lightGroup[k].Position = 40; continue; }
                if (lightGroup[k].Position == 71) { lightGroup[k].Position = 36; continue; }

                if (lightGroup[k].Position == 72) { lightGroup[k].Position = 49; continue; }
                if (lightGroup[k].Position == 73) { lightGroup[k].Position = 45; continue; }
                if (lightGroup[k].Position == 74) { lightGroup[k].Position = 41; continue; }
                if (lightGroup[k].Position == 75) { lightGroup[k].Position = 37; continue; }

                if (lightGroup[k].Position == 76) { lightGroup[k].Position = 50; continue; }
                if (lightGroup[k].Position == 77) { lightGroup[k].Position = 46; continue; }
                if (lightGroup[k].Position == 78) { lightGroup[k].Position = 42; continue; }
                if (lightGroup[k].Position == 79) { lightGroup[k].Position = 38; continue; }

                if (lightGroup[k].Position == 80) { lightGroup[k].Position = 51; continue; }
                if (lightGroup[k].Position == 81) { lightGroup[k].Position = 47; continue; }
                if (lightGroup[k].Position == 82) { lightGroup[k].Position = 43; continue; }
                if (lightGroup[k].Position == 83) { lightGroup[k].Position = 39; continue; }

                //右上
                if (lightGroup[k].Position == 84) { lightGroup[k].Position = 80; continue; }
                if (lightGroup[k].Position == 85) { lightGroup[k].Position = 76; continue; }
                if (lightGroup[k].Position == 86) { lightGroup[k].Position = 72; continue; }
                if (lightGroup[k].Position == 87) { lightGroup[k].Position = 68; continue; }

                if (lightGroup[k].Position == 88) { lightGroup[k].Position = 81; continue; }
                if (lightGroup[k].Position == 89) { lightGroup[k].Position = 77; continue; }
                if (lightGroup[k].Position == 90) { lightGroup[k].Position = 73; continue; }
                if (lightGroup[k].Position == 91) { lightGroup[k].Position = 69; continue; }

                if (lightGroup[k].Position == 92) { lightGroup[k].Position = 82; continue; }
                if (lightGroup[k].Position == 93) { lightGroup[k].Position = 78; continue; }
                if (lightGroup[k].Position == 94) { lightGroup[k].Position = 74; continue; }
                if (lightGroup[k].Position == 95) { lightGroup[k].Position = 70; continue; }

                if (lightGroup[k].Position == 96) { lightGroup[k].Position = 83; continue; }
                if (lightGroup[k].Position == 97) { lightGroup[k].Position = 79; continue; }
                if (lightGroup[k].Position == 98) { lightGroup[k].Position = 75; continue; }
                if (lightGroup[k].Position == 99) { lightGroup[k].Position = 71; continue; }

                //右圆钮
                if (lightGroup[k].Position == 100) { lightGroup[k].Position = 123; continue; }
                if (lightGroup[k].Position == 101) { lightGroup[k].Position = 122; continue; }
                if (lightGroup[k].Position == 102) { lightGroup[k].Position = 121; continue; }
                if (lightGroup[k].Position == 103) { lightGroup[k].Position = 120; continue; }
                if (lightGroup[k].Position == 104) { lightGroup[k].Position = 119; continue; }
                if (lightGroup[k].Position == 105) { lightGroup[k].Position = 118; continue; }
                if (lightGroup[k].Position == 106) { lightGroup[k].Position = 117; continue; }
                if (lightGroup[k].Position == 107) { lightGroup[k].Position = 116; continue; }

                //左圆钮
                if (lightGroup[k].Position == 108) { lightGroup[k].Position = 35; continue; }
                if (lightGroup[k].Position == 109) { lightGroup[k].Position = 34; continue; }
                if (lightGroup[k].Position == 110) { lightGroup[k].Position = 33; continue; }
                if (lightGroup[k].Position == 111) { lightGroup[k].Position = 32; continue; }
                if (lightGroup[k].Position == 112) { lightGroup[k].Position = 31; continue; }
                if (lightGroup[k].Position == 113) { lightGroup[k].Position = 30; continue; }
                if (lightGroup[k].Position == 114) { lightGroup[k].Position = 29; continue; }
                if (lightGroup[k].Position == 115) { lightGroup[k].Position = 28; continue; }

                //下圆钮
                if (lightGroup[k].Position == 116) { lightGroup[k].Position = 108; continue; }
                if (lightGroup[k].Position == 117) { lightGroup[k].Position = 109; continue; }
                if (lightGroup[k].Position == 118) { lightGroup[k].Position = 110; continue; }
                if (lightGroup[k].Position == 119) { lightGroup[k].Position = 111; continue; }
                if (lightGroup[k].Position == 120) { lightGroup[k].Position = 112; continue; }
                if (lightGroup[k].Position == 121) { lightGroup[k].Position = 113; continue; }
                if (lightGroup[k].Position == 122) { lightGroup[k].Position = 114; continue; }
                if (lightGroup[k].Position == 123) { lightGroup[k].Position = 115; continue; }

                //上圆钮
                if (lightGroup[k].Position == 28) { lightGroup[k].Position = 100; continue; }
                if (lightGroup[k].Position == 29) { lightGroup[k].Position = 101; continue; }
                if (lightGroup[k].Position == 30) { lightGroup[k].Position = 102; continue; }
                if (lightGroup[k].Position == 31) { lightGroup[k].Position = 103; continue; }
                if (lightGroup[k].Position == 32) { lightGroup[k].Position = 104; continue; }
                if (lightGroup[k].Position == 33) { lightGroup[k].Position = 105; continue; }
                if (lightGroup[k].Position == 34) { lightGroup[k].Position = 106; continue; }
                if (lightGroup[k].Position == 35) { lightGroup[k].Position = 107; continue; }
            }
            #endregion
            return lightGroup;
        }
        /// <summary>
        /// 逆时针
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <returns></returns>
        public static List<Light> AntiClockwise(List<Light> lightGroup)
        {
            #region
            for (int k = 0; k < lightGroup.Count; k++)
            {
                //左下
                if (lightGroup[k].Position == 36) { lightGroup[k].Position = 71; continue; }
                if (lightGroup[k].Position == 37) { lightGroup[k].Position = 75; continue; }
                if (lightGroup[k].Position == 38) { lightGroup[k].Position = 79; continue; }
                if (lightGroup[k].Position == 39) { lightGroup[k].Position = 83; continue; }

                if (lightGroup[k].Position == 40) { lightGroup[k].Position = 70; continue; }
                if (lightGroup[k].Position == 41) { lightGroup[k].Position = 74; continue; }
                if (lightGroup[k].Position == 42) { lightGroup[k].Position = 78; continue; }
                if (lightGroup[k].Position == 43) { lightGroup[k].Position = 82; continue; }

                if (lightGroup[k].Position == 44) { lightGroup[k].Position = 69; continue; }
                if (lightGroup[k].Position == 45) { lightGroup[k].Position = 73; continue; }
                if (lightGroup[k].Position == 46) { lightGroup[k].Position = 77; continue; }
                if (lightGroup[k].Position == 47) { lightGroup[k].Position = 81; continue; }

                if (lightGroup[k].Position == 48) { lightGroup[k].Position = 68; continue; }
                if (lightGroup[k].Position == 49) { lightGroup[k].Position = 72; continue; }
                if (lightGroup[k].Position == 50) { lightGroup[k].Position = 76; continue; }
                if (lightGroup[k].Position == 51) { lightGroup[k].Position = 80; continue; }

                //左上
                if (lightGroup[k].Position == 52) { lightGroup[k].Position = 39; continue; }
                if (lightGroup[k].Position == 53) { lightGroup[k].Position = 43; continue; }
                if (lightGroup[k].Position == 54) { lightGroup[k].Position = 47; continue; }
                if (lightGroup[k].Position == 55) { lightGroup[k].Position = 51; continue; }

                if (lightGroup[k].Position == 56) { lightGroup[k].Position = 38; continue; }
                if (lightGroup[k].Position == 57) { lightGroup[k].Position = 42; continue; }
                if (lightGroup[k].Position == 58) { lightGroup[k].Position = 46; continue; }
                if (lightGroup[k].Position == 59) { lightGroup[k].Position = 50; continue; }

                if (lightGroup[k].Position == 60) { lightGroup[k].Position = 37; continue; }
                if (lightGroup[k].Position == 61) { lightGroup[k].Position = 41; continue; }
                if (lightGroup[k].Position == 62) { lightGroup[k].Position = 45; continue; }
                if (lightGroup[k].Position == 63) { lightGroup[k].Position = 49; continue; }

                if (lightGroup[k].Position == 64) { lightGroup[k].Position = 36; continue; }
                if (lightGroup[k].Position == 65) { lightGroup[k].Position = 40; continue; }
                if (lightGroup[k].Position == 66) { lightGroup[k].Position = 44; continue; }
                if (lightGroup[k].Position == 67) { lightGroup[k].Position = 48; continue; }

                //右下
                if (lightGroup[k].Position == 68) { lightGroup[k].Position = 87; continue; }
                if (lightGroup[k].Position == 69) { lightGroup[k].Position = 91; continue; }
                if (lightGroup[k].Position == 70) { lightGroup[k].Position = 95; continue; }
                if (lightGroup[k].Position == 71) { lightGroup[k].Position = 99; continue; }

                if (lightGroup[k].Position == 72) { lightGroup[k].Position = 86; continue; }
                if (lightGroup[k].Position == 73) { lightGroup[k].Position = 90; continue; }
                if (lightGroup[k].Position == 74) { lightGroup[k].Position = 94; continue; }
                if (lightGroup[k].Position == 75) { lightGroup[k].Position = 98; continue; }

                if (lightGroup[k].Position == 76) { lightGroup[k].Position = 85; continue; }
                if (lightGroup[k].Position == 77) { lightGroup[k].Position = 89; continue; }
                if (lightGroup[k].Position == 78) { lightGroup[k].Position = 93; continue; }
                if (lightGroup[k].Position == 79) { lightGroup[k].Position = 97; continue; }

                if (lightGroup[k].Position == 80) { lightGroup[k].Position = 84; continue; }
                if (lightGroup[k].Position == 81) { lightGroup[k].Position = 88; continue; }
                if (lightGroup[k].Position == 82) { lightGroup[k].Position = 92; continue; }
                if (lightGroup[k].Position == 83) { lightGroup[k].Position = 96; continue; }

                //右上
                if (lightGroup[k].Position == 84) { lightGroup[k].Position = 55; continue; }
                if (lightGroup[k].Position == 85) { lightGroup[k].Position = 59; continue; }
                if (lightGroup[k].Position == 86) { lightGroup[k].Position = 63; continue; }
                if (lightGroup[k].Position == 87) { lightGroup[k].Position = 67; continue; }

                if (lightGroup[k].Position == 88) { lightGroup[k].Position = 54; continue; }
                if (lightGroup[k].Position == 89) { lightGroup[k].Position = 58; continue; }
                if (lightGroup[k].Position == 90) { lightGroup[k].Position = 62; continue; }
                if (lightGroup[k].Position == 91) { lightGroup[k].Position = 66; continue; }

                if (lightGroup[k].Position == 92) { lightGroup[k].Position = 53; continue; }
                if (lightGroup[k].Position == 93) { lightGroup[k].Position = 57; continue; }
                if (lightGroup[k].Position == 94) { lightGroup[k].Position = 61; continue; }
                if (lightGroup[k].Position == 95) { lightGroup[k].Position = 65; continue; }

                if (lightGroup[k].Position == 96) { lightGroup[k].Position = 52; continue; }
                if (lightGroup[k].Position == 97) { lightGroup[k].Position = 56; continue; }
                if (lightGroup[k].Position == 98) { lightGroup[k].Position = 60; continue; }
                if (lightGroup[k].Position == 99) { lightGroup[k].Position = 64; continue; }

                //右圆钮
                if (lightGroup[k].Position == 100) { lightGroup[k].Position = 28; continue; }
                if (lightGroup[k].Position == 101) { lightGroup[k].Position = 29; continue; }
                if (lightGroup[k].Position == 102) { lightGroup[k].Position = 30; continue; }
                if (lightGroup[k].Position == 103) { lightGroup[k].Position = 31; continue; }
                if (lightGroup[k].Position == 104) { lightGroup[k].Position = 32; continue; }
                if (lightGroup[k].Position == 105) { lightGroup[k].Position = 33; continue; }
                if (lightGroup[k].Position == 106) { lightGroup[k].Position = 34; continue; }
                if (lightGroup[k].Position == 107) { lightGroup[k].Position = 35; continue; }

                //左圆钮
                if (lightGroup[k].Position == 108) { lightGroup[k].Position = 116; continue; }
                if (lightGroup[k].Position == 109) { lightGroup[k].Position = 117; continue; }
                if (lightGroup[k].Position == 110) { lightGroup[k].Position = 118; continue; }
                if (lightGroup[k].Position == 111) { lightGroup[k].Position = 119; continue; }
                if (lightGroup[k].Position == 112) { lightGroup[k].Position = 120; continue; }
                if (lightGroup[k].Position == 113) { lightGroup[k].Position = 121; continue; }
                if (lightGroup[k].Position == 114) { lightGroup[k].Position = 122; continue; }
                if (lightGroup[k].Position == 115) { lightGroup[k].Position = 123; continue; }

                //下圆钮
                if (lightGroup[k].Position == 116) { lightGroup[k].Position = 107; continue; }
                if (lightGroup[k].Position == 117) { lightGroup[k].Position = 106; continue; }
                if (lightGroup[k].Position == 118) { lightGroup[k].Position = 105; continue; }
                if (lightGroup[k].Position == 119) { lightGroup[k].Position = 104; continue; }
                if (lightGroup[k].Position == 120) { lightGroup[k].Position = 103; continue; }
                if (lightGroup[k].Position == 121) { lightGroup[k].Position = 102; continue; }
                if (lightGroup[k].Position == 122) { lightGroup[k].Position = 101; continue; }
                if (lightGroup[k].Position == 123) { lightGroup[k].Position = 100; continue; }

                //上圆钮
                if (lightGroup[k].Position == 28) { lightGroup[k].Position = 115; continue; }
                if (lightGroup[k].Position == 29) { lightGroup[k].Position = 114; continue; }
                if (lightGroup[k].Position == 30) { lightGroup[k].Position = 113; continue; }
                if (lightGroup[k].Position == 31) { lightGroup[k].Position = 112; continue; }
                if (lightGroup[k].Position == 32) { lightGroup[k].Position = 111; continue; }
                if (lightGroup[k].Position == 33) { lightGroup[k].Position = 110; continue; }
                if (lightGroup[k].Position == 34) { lightGroup[k].Position = 109; continue; }
                if (lightGroup[k].Position == 35) { lightGroup[k].Position = 108; continue; }
            }
            #endregion
            return lightGroup;
        }
        /// <summary>
        /// 反转
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <returns></returns>
        public static List<Light> Reversal(List<Light> lightGroup) {
            int max = LightBusiness.GetMax(lightGroup);
            int min = LightBusiness.GetMin(lightGroup);
            if (max == -1 && min == -1 && lightGroup.Count/2!=0)
                return lightGroup;
            //两两组合
            lightGroup = LightBusiness.SortCouple(lightGroup);
            for (int i = 0; i < lightGroup.Count; i++)
            {
                //调整时间
                lightGroup[i].Time = max - lightGroup[i].Time + min;
            }
            for (int i = 0; i < lightGroup.Count; i++) {
                if (i / 2 == 1) {
                    Light l = lightGroup[i];
                    lightGroup[i] = lightGroup[i - 1];
                    lightGroup[i - 1] = l;
                }
            }
            for (int i = 0; i < lightGroup.Count; i++)
            {
                if (lightGroup[i].Action == 144) {
                    lightGroup[i].Action = 128;
                }
                else if (lightGroup[i].Action == 128)
                {
                    lightGroup[i].Action = 144;
                }
            }
            return lightGroup;
        }
        /// <summary>
        /// 去除边框灯光
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <returns></returns>
        public static List<Light> RemoveBorder(List<Light> lightGroup)
        {
            for (int i = lightGroup.Count - 1; i >= 0; i--) {
                int position = lightGroup[i].Position;
                if (position >= 28 && position <= 35 || position >= 100 && position <= 123)
                    lightGroup.Remove(lightGroup[i]);
            }
            return lightGroup;
        }
        public enum Operator
        {
            Multiplication = 0,
            Division = 1
        };
        public enum ShapeColorType
        {
            Square,
            RadialVertical,
            RadialHorizontal
        };
        public static List<Light> ChangeTime(List<Light> lightGroup, Operator mOperator,Double multiple) {
            if (mOperator == Operator.Multiplication) {
                for (int i = 0; i < lightGroup.Count; i++)
                {
                    lightGroup[i].Time = Convert.ToInt32(lightGroup[i].Time * multiple);
                }
            }
            else {
                for (int i = 0; i < lightGroup.Count; i++)
                {
                    lightGroup[i].Time = Convert.ToInt32(lightGroup[i].Time / multiple);
                }
            }
            return lightGroup;
        }
        public static List<Light> CopyToTheEnd(List<Light> lightGroup,List<int> colorList) {
            //就是复制自己
            if (colorList.Count == 0)
            {
                //获取最后的时间
                lightGroup = LightBusiness.Sort(lightGroup);
                int time = lightGroup[lightGroup.Count - 1].Time;
                List<Light> ll = LightGroupMethod.SetStartTime(lightGroup, time);
                for (int i = 0; i < ll.Count; i++)
                {
                    lightGroup.Add(ll[i]);
                }
            }
            else {
                //得到原灯光
                List<Light> ll = LightBusiness.Copy(lightGroup);
                for (int j = 0; j < colorList.Count; j++) {
                    //获取最后的时间
                    lightGroup = LightBusiness.Sort(lightGroup);
                    int time = lightGroup[lightGroup.Count - 1].Time;
                    List<Light> mLl =LightGroupMethod.SetStartTime(ll, time);
                    for (int i = 0; i < mLl.Count; i++)
                    {
                        //if (mLl[i].Action == 144)
                            mLl[i].Color = colorList[j];
                        lightGroup.Add(mLl[i]);
                    }
                }
            }
            return lightGroup;
        }
        public static List<Light> CopyToTheFollow(List<Light> lightGroup, List<int> colorList)
        {
            lightGroup = LightBusiness.SortCouple(lightGroup);
            List<Light> _lightGroup = LightBusiness.Copy(lightGroup);

            if (colorList.Count == 0) {
                for (int i = 0; i < lightGroup.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        int width = lightGroup[i + 1].Time - lightGroup[i].Time;
                        _lightGroup.Add(new Light(lightGroup[i].Time + width, lightGroup[i].Action, lightGroup[i].Position, lightGroup[i].Color));
                        _lightGroup.Add(new Light(lightGroup[i + 1].Time + width, lightGroup[i + 1].Action, lightGroup[i + 1].Position, lightGroup[i + 1].Color));
                    }
                }
            }
            else
            {
                for (int i = 0; i < lightGroup.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        int width = lightGroup[i + 1].Time - lightGroup[i].Time;
                        for (int j = 0; j < colorList.Count; j++) {
                            _lightGroup.Add(new Light(lightGroup[i].Time + (j + 1) * width, lightGroup[i].Action, lightGroup[i].Position, colorList[j]));
                            _lightGroup.Add(new Light(lightGroup[i+1].Time + (j + 1) * width, lightGroup[i+1].Action, lightGroup[i+1].Position, colorList[j]));
                        }
                    }   
                }
            }
            return _lightGroup;
        }
        public static List<Light> AccelerationOrDeceleration(List<Light> lightGroup, List<int> rangeList)
        {
            List<Light> _lightGroup = LightBusiness.Sort(lightGroup);
            for (int i = 0; i < rangeList.Count; i++) {
                lightGroup = LightBusiness.Sort(lightGroup);
                int time = lightGroup[lightGroup.Count - 1].Time;
                List<Light> mLightGroup = new List<Light>();
                for (int j = 0; j < _lightGroup.Count; j++)
                {
                    mLightGroup.Add(new Light(time + (int)(_lightGroup[j].Time * (rangeList[i]/100.0)), _lightGroup[j].Action, _lightGroup[j].Position, _lightGroup[j].Color));
                }
                for (int k = 0; k < mLightGroup.Count; k++) {
                    lightGroup.Add(new Light(mLightGroup[k].Time, mLightGroup[k].Action, mLightGroup[k].Position, mLightGroup[k].Color));
                }
            }
            return lightGroup;
        }
        private static List<Light> IfThen(List<Light> lightGroup, string commandLine, Dictionary<String, List<int>> rangeGroupDictionary)
        {
            //IfThen(灯光组名称,"Time=范围&&Color=范围","Time=+5&&Position=-5",Edit);
            //IfThen(灯光组名称,"Time=范围&&Color=范围","",Remove);
            List<Light> oldLightGroup = LightBusiness.Copy(lightGroup);
            List<Light> _lightGroup = LightBusiness.Copy(lightGroup) ;

            String[] strs = commandLine.Split(',');
            if (strs.Count() != 4) { 
                return null;
            }
            String[] strsIf = System.Text.RegularExpressions.Regex.Split(strs[1], "&&");
            for(int i =0;i< strsIf.Count(); i++) {
                String[] strsIfPrerequisite = strsIf[i].Split(':');
                if (strsIfPrerequisite.Count() != 2) {
                    return null;
                }
                if (!strsIfPrerequisite[0].Equals("Action") && !rangeGroupDictionary.ContainsKey(strsIfPrerequisite[1]))
                {
                    return null;
                }
                if (strsIfPrerequisite[0].Equals("Time"))
                {
                    for (int x = _lightGroup.Count-1; x >= 0; x--)
                    {
                        bool isMatch = false;
                        foreach (int number in rangeGroupDictionary[strsIfPrerequisite[1]]) {
                            if (_lightGroup[x].Time == number)
                            {
                                isMatch = true;
                                break;
                            }
                        }
                        if (!isMatch) {
                            //如果不匹配，删除
                            _lightGroup.RemoveAt(x);
                        }
                    }
                }
                else if (strsIfPrerequisite[0].Equals("Action"))
                {
                    if (strsIfPrerequisite[1].Equals("Open"))
                    {
                        for (int x = _lightGroup.Count - 1; x >= 0; x--)
                        {
                            if (_lightGroup[x].Action != 144)
                            {
                                //如果不匹配，删除
                                _lightGroup.RemoveAt(x);
                            }
                        }
                    }
                    else if (strsIfPrerequisite[1].Equals("Close"))
                    {
                        for (int x = _lightGroup.Count - 1; x >= 0; x--)
                        {
                            if (_lightGroup[x].Action != 128)
                            {
                                //如果不匹配，删除
                                _lightGroup.RemoveAt(x);
                            }
                        }
                    }
                }
                else if (strsIfPrerequisite[0].Equals("Position"))
                {
                    for (int x = _lightGroup.Count-1; x >= 0; x--)
                    {
                        bool isMatch = false;
                        foreach (int number in rangeGroupDictionary[strsIfPrerequisite[1]]) {
                            if (_lightGroup[x].Position == number)
                            {
                                isMatch = true;
                                break;
                            }
                        }
                        if (!isMatch)
                        {
                            //如果不匹配，删除
                            _lightGroup.RemoveAt(x);
                        }
                    }
                }
                else if (strsIfPrerequisite[0].Equals("Color"))
                {
                    for (int x = _lightGroup.Count-1; x >= 0; x--)
                    {
                        bool isMatch = false;
                        foreach (int number in rangeGroupDictionary[strsIfPrerequisite[1]]) {
                            if (_lightGroup[x].Color == number)
                            {
                                isMatch = true;
                                break;
                            }
                        }
                        if (!isMatch)
                        {
                            //如果不匹配，删除
                            _lightGroup.RemoveAt(x);
                        }
                    }
                }
            }
            //到这一步，剩下的都是需要修改的内容
            //把原内容删除
            for (int i = oldLightGroup.Count - 1; i >= 0; i--)
            {
                for (int x = 0; x < _lightGroup.Count; x++) {
                    if (oldLightGroup[i].Time == _lightGroup[x].Time
                       && oldLightGroup[i].Action == _lightGroup[x].Action
                       && oldLightGroup[i].Position == _lightGroup[x].Position
                       && oldLightGroup[i].Color == _lightGroup[x].Color) {
                        oldLightGroup.Remove(oldLightGroup[i]);
                        break;
                    }
                }
            }
            if (strs[3].Equals("Remove"))
            {
                return oldLightGroup;
            }
            else if (strs[3].Equals("Edit"))
            {
                String[] strsThen = System.Text.RegularExpressions.Regex.Split(strs[2], "&&");
                for (int i = 0; i < strsThen.Count(); i++)
                {
                    String[] strsIfPrerequisite = strsThen[i].Split(':');
                    if (strsIfPrerequisite.Count() != 2)
                    {
                        return null;
                    }
                    if (strsIfPrerequisite[0].Equals("Time"))
                    {
                        if (strsIfPrerequisite[1].Contains("+"))
                        {
                            for (int x = 0; x < _lightGroup.Count; x++)
                            {
                                _lightGroup[x].Time += int.Parse(strsIfPrerequisite[1].Substring(1));
                            }
                        }
                        else if (strsIfPrerequisite[1].Contains("-"))
                        {
                            for (int x = 0; x < _lightGroup.Count; x++)
                            {
                                _lightGroup[x].Time -= int.Parse(strsIfPrerequisite[1].Substring(1));
                            }
                        }
                        else
                        {
                            for (int x = 0; x < _lightGroup.Count; x++)
                            {
                                _lightGroup[x].Time = int.Parse(strsIfPrerequisite[1]);
                            }
                        }
                    }
                    if (strsIfPrerequisite[0].Equals("Position"))
                    {
                        if (strsIfPrerequisite[1].Contains("+"))
                        {
                            for (int x = 0; x < _lightGroup.Count; x++)
                            {
                                _lightGroup[x].Position += int.Parse(strsIfPrerequisite[1].Substring(1));
                            }
                        }
                        else if (strsIfPrerequisite[1].Contains("-"))
                        {
                            for (int x = 0; x < _lightGroup.Count; x++)
                            {
                                _lightGroup[x].Position -= int.Parse(strsIfPrerequisite[1].Substring(1));
                            }
                        }
                        else
                        {
                            for (int x = 0; x < _lightGroup.Count; x++)
                            {
                                _lightGroup[x].Position = int.Parse(strsIfPrerequisite[1]);
                            }
                        }
                    }
                    if (strsIfPrerequisite[0].Equals("Color"))
                    {
                        if (strsIfPrerequisite[1].Contains("+"))
                        {
                            for (int x = 0; x < _lightGroup.Count; x++)
                            {
                                _lightGroup[x].Color += int.Parse(strsIfPrerequisite[1].Substring(1));
                            }
                        }
                        else if (strsIfPrerequisite[1].Contains("-"))
                        {
                            for (int x = 0; x < _lightGroup.Count; x++)
                            {
                                _lightGroup[x].Color -= int.Parse(strsIfPrerequisite[1].Substring(1));
                            }
                        }
                        else
                        {
                            for (int x = 0; x < _lightGroup.Count; x++)
                            {
                                _lightGroup[x].Color = int.Parse(strsIfPrerequisite[1]);
                            }
                        }
                    }
                }
                for (int i = 0; i < _lightGroup.Count; i++) {
                    oldLightGroup.Add(_lightGroup[i]);
                }
                return oldLightGroup;
            }
            else {
                return null;

            }
        }


    }
}
