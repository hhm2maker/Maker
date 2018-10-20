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
          
            //Regex P_Animation = new Regex(@"\s*Animation\([\S\s]*\)");
            //if (P_Animation.IsMatch(commandLine))
            //{
            //    String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
            //    content = content.Substring(0, content.IndexOf(')'));
            //    String[] parameters = content.Split(',');
            //    if (parameters[1].Equals("Serpentine")) {
            //        if (parameters.Count() != 4)
            //        {
            //            return null;
            //        }
            //        return Serpentine(_lightGroup, int.Parse(parameters[2]), int.Parse(parameters[3]));
            //    }
            //    if (parameters[1].Equals("Windmill"))
            //    {
            //        if (parameters.Count() != 3)
            //        {
            //            return null;
            //        }
            //        return Windmill(_lightGroup, int.Parse(parameters[2]));
            //    }
            //}
         
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


   
      
     
        public enum ShapeColorType
        {
            Square,
            RadialVertical,
            RadialHorizontal
        }; 
       
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
