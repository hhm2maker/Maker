using Maker.Business;
using Maker.Model;
using Operation;
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
          
            //Regex P_ShapeColor = new Regex(@"\s*ShapeColor\([\S\s]*\)");
            //if (P_ShapeColor.IsMatch(commandLine))
            //{
            //    String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
            //    content = content.Substring(0, content.IndexOf(')'));
            //    String[] parameters = content.Split(',');
            //    if (parameters.Count() != 3)
            //    {
            //        return null;
            //    }
            //    if (parameters[1].Equals("Square"))
            //    {
            //        return ShapeColor(_lightGroup, ShapeColorType.Square, parameters[2].Substring(1, parameters[2].Length-2));
            //    }
            //    else if (parameters[1].Equals("RadialVertical"))
            //    {
            //        return ShapeColor(_lightGroup, ShapeColorType.RadialVertical, parameters[2].Substring(1, parameters[2].Length - 2));
            //    }
            //    else if (parameters[1].Equals("RadialHorizontal"))
            //    {
            //        return ShapeColor(_lightGroup, ShapeColorType.RadialHorizontal, parameters[2].Substring(1, parameters[2].Length - 2));
            //    }
            //}
          
            Regex P_IfThen = new Regex(@"\s*IfThen\([\S\s]*\)");
            if (P_IfThen.IsMatch(commandLine))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.IndexOf(')'));
                return IfThen(_lightGroup, content,rangeGroupDictionary);
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
