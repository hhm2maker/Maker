using Maker.Business;
using Maker.Model;
using Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Maker.MethodSet
{
    public static class CreateMethod
    {
        /// <summary>
        /// 主方法，用于派发创建任务
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name="rangeDictionary"></param>
        /// <param name="colorDictionary"></param>
        /// <param name="lastFilePath">当前脚本路径</param>
        /// <returns></returns>
        public static List<Light> CreateMain(String commandLine, Dictionary<String, List<Light>> lightDictionary, Dictionary<String, List<int>> rangeDictionary, Dictionary<String, List<int>> colorDictionary,String lastFilePath) {
          
            Regex P_CreateLightGroup = new Regex(@"\s*CreateLightGroup\(([0-9a-zA-Z_\u4e00-\u9fa5]+,){4}[0-9a-zA-Z_\u4e00-\u9fa5]+\)");
            //插入灯光[组]语句
            if (P_CreateLightGroup.IsMatch(commandLine))
            {
                return CreateLightGroup(commandLine, rangeDictionary, colorDictionary);
            }
            Regex P_CreateLightGroupExtension = new Regex(@"\s*CreateLightGroup\(([0-9a-zA-Z_\u4e00-\u9fa5]+,){5}[0-9a-zA-Z_\u4e00-\u9fa5]+\)");
            if (P_CreateLightGroupExtension.IsMatch(commandLine))
            {
                return CreateLightGroupExtension(commandLine, rangeDictionary, colorDictionary);
            }
            Regex P_CreateLightGroupExtension2 = new Regex(@"\s*CreateLightGroup\(([0-9a-zA-Z_\u4e00-\u9fa5]+,){6}[0-9a-zA-Z_\u4e00-\u9fa5]+\)");
            if (P_CreateLightGroupExtension2.IsMatch(commandLine))
            {
                return CreateLightGroupExtension2(commandLine, rangeDictionary, colorDictionary);
            }
            Regex P_FromMidiFile = new Regex(@"\s*FromMidiFile\([\S\s]*\)");
            if (P_FromMidiFile.IsMatch(commandLine))
            {
                return FromMidiFile(commandLine, lastFilePath);
            }
            Regex P_FromLightFile = new Regex(@"\s*FromLightFile\([\S\s]*\)");
            if (P_FromLightFile.IsMatch(commandLine))
            {
                return FromLightFile(commandLine, lastFilePath);
            }
            Regex P_Automatic = new Regex(@"\s*Automatic\([\S\s]*\)");
            if (P_Automatic.IsMatch(commandLine))
            {
                return Automatic(commandLine);
            }
            Regex P_Animation = new Regex(@"\s*Animation\([\S\s]*\)");
            if (P_Animation.IsMatch(commandLine))
            {
                return Animation(commandLine);
            }
            Regex P_Intersection = new Regex(@"\s*Intersection\([\S\s]*\)");
            if (P_Intersection.IsMatch(commandLine))
            {
                return Intersection(commandLine, lightDictionary);
            }
            Regex P_Complement = new Regex(@"\s*Complement\([\S\s]*\)");
            if (P_Complement.IsMatch(commandLine))
            {
                return Complement(commandLine, lightDictionary);
            }
            //废弃
            Regex P_ChangeIntoMotion = new Regex(@"\s*ChangeIntoMotion\([\S\s]*\)");
            if (P_ChangeIntoMotion.IsMatch(commandLine))
            {
                return ChangeIntoMotion(commandLine);
            }
            return null;
        }

        private static List<Light> Complement(string commandLine, Dictionary<string, List<Light>> lightDictionary)
        {
            MatchCollection matchLightContent = Regex.Matches(commandLine, @"\([\S\s]*\)");
            if (matchLightContent[0].Value.Length <= 2)
                return null;
            String strContent = matchLightContent[0].Value.Substring(1, matchLightContent[0].Value.Length - 2);
            String[] strContents = strContent.Split(',');
            if (strContents.Count() != 2)
                return null;
            //集合操作
            List<Light> mainBig = new List<Light>();
            mainBig.AddRange(lightDictionary[strContents[0]].ToList());
            mainBig.AddRange(lightDictionary[strContents[1]].ToList());
            mainBig = LightBusiness.Splice(mainBig);

            List<Light> big = LightBusiness.Split(mainBig, lightDictionary[strContents[0]]);
            List<Light> small = LightBusiness.Split(mainBig, lightDictionary[strContents[1]]);

            List<Light> result = new List<Light>();
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
            return result;
        }

        private static List<Light> Intersection(string commandLine, Dictionary<string, List<Light>> lightDictionary)
        {
            MatchCollection matchLightContent = Regex.Matches(commandLine, @"\([\S\s]*\)");
            if (matchLightContent[0].Value.Length <= 2)
                return null;
            String strContent = matchLightContent[0].Value.Substring(1, matchLightContent[0].Value.Length - 2);
            String[] strContents = strContent.Split(',');
            if (strContents.Count() != 2)
                return null;
            //集合操作
            List<Light> mainBig = new List<Light>();
            mainBig.AddRange(lightDictionary[strContents[0]].ToList());
            mainBig.AddRange(lightDictionary[strContents[1]].ToList());
            mainBig = LightBusiness.Splice(mainBig);

            List<Light> big = LightBusiness.Split(mainBig, lightDictionary[strContents[0]]);
            List<Light> small = LightBusiness.Split(mainBig, lightDictionary[strContents[1]]);

            List<Light> result = new List<Light>();
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
            return result;
        }

  
        private static List<Light> ChangeIntoMotion(string commandLine)
        {
            List<Light> mLl = new List<Light>();
            MatchCollection matchLightContent = Regex.Matches(commandLine, @"\([\S\s]*\)");
            if (matchLightContent[0].Value.Length <= 2)
                return null;
            String strContent = matchLightContent[0].Value.Substring(1, matchLightContent[0].Value.Length - 2);
            strContent = strContent.Substring(1, strContent.Length - 2);
                String[] strsNumber = strContent.Split(' ');
                List<List<int>> _numberList = new List<List<int>>();
                List<int> list = new List<int>();
                for (int i = 0; i < strsNumber.Count(); i++)
                {
                    if (list.Count != 10)
                    {
                        try
                        {
                            list.Add(Convert.ToInt32(strsNumber[i]));
                        }
                        catch
                        {
                            return null;
                        }
                    }
                    if (list.Count == 10)
                    {
                        _numberList.Add(list.ToList());
                        list.Clear();
                    }
                }
                //使用垂直int集合
                List<List<int>> lli = Model.IntCollection.VerticalIntList.ToList();
                int count = 10 + _numberList.Count;//从0开始算，第0的时候就在最右边出现
                int now = -_numberList.Count + 1;
                int plus = 16;
                while (now < count)
                {
                    for (int i = 0; i < _numberList.Count; i++)
                    {
                        //判断这行能否出现在画板上
                        if (now + i >= 0 && now + i < lli.Count)
                        {
                            for (int j = 0; j < _numberList[i].Count; j++)
                            {
                                if (_numberList[i][j] == 1)
                                {
                                    mLl.Add(new Light((now + _numberList.Count - 1) * plus, 144, lli[now + i][j], 5));
                                    mLl.Add(new Light((now + _numberList.Count) * plus, 128, lli[now + i][j], 64));
                                }
                            }
                        }
                    }
                    now++;
                }
                for (int x = mLl.Count - 1; x >= 0; x--)
                {
                    if (mLl[x].Position == -1)
                    {
                        mLl.RemoveAt(x);
                    }
                }
                return mLl;
        }

        private static List<Light> CreateLightGroupExtension(string commandLine, Dictionary<string, List<int>> rangeDictionary, Dictionary<string, List<int>> colorDictionary)
        {
            MatchCollection matchLightContent = Regex.Matches(commandLine, @"\(([0-9a-zA-Z_\u4e00-\u9fa5]+,){5}[0-9a-zA-Z_\u4e00-\u9fa5]+\)");
            String[] strsContent = matchLightContent[0].Value.Substring(1, matchLightContent[0].Value.Length - 2).Split(',');
            //有六个参数
            if (strsContent.Length == 6)
            {
                List<Light> _lightGroup = new List<Light>();
                //时间
                int _time = int.Parse(strsContent[0]);
                String _range = strsContent[1];
                int _interval = int.Parse(strsContent[2]);
                int _continued = int.Parse(strsContent[3]);
                String _color = strsContent[4];
                String _type = strsContent[5];

                List<int> _position = new List<int>();
                _position.AddRange(rangeDictionary[_range].ToArray());
                if (_type.Equals("Up"))
                {
                    //开始
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _time + i * _interval + j * _continued;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _continued + _time + i * _interval + j * _continued;//时间
                            light.Action = 128;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_type.Equals("Down"))
                {
                    _position.Reverse();
                    //开始
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _time + i * _interval + j * _continued;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _continued + _time + i * _interval + j * _continued;//时间
                            light.Action = 128;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_type.Equals("UpDown"))
                {
                    List<int> _relList = new List<int>();
                    _relList.AddRange(_position.ToArray().Reverse());
                    _position.AddRange(_relList.ToArray());

                    //开始
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _time + i * _interval + j * _continued;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _continued + _time + i * _interval + j * _continued;//时间
                            light.Action = 128;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_type.Equals("DownUp"))
                {
                    _position.Reverse();
                    List<int> _relList = new List<int>();
                    _relList.AddRange(_position.ToArray().Reverse());
                    _position.AddRange(_relList.ToArray());

                    //开始
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _time + i * _interval + j * _continued;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _continued + _time + i * _interval + j * _continued;//时间
                            light.Action = 128;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_type.Equals("UpAndDown") || _type.Equals("DownAndUp"))
                {
                    //开始
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _time + i * _interval + j * _continued;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _continued + _time + i * _interval + j * _continued;//时间
                            light.Action = 128;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                        }
                    }

                    _position.Reverse();

                    //开始
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _time + i * _interval + j * _continued;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _continued + _time + i * _interval + j * _continued;//时间
                            light.Action = 128;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_type.Equals("FreezeFrame"))
                {
                    int mTime = _time;
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            //开始
                            Light light = new Light();
                            light.Time = mTime + i * _interval;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                            //结束
                            Light light2 = new Light();
                            light2.Time = mTime + _position.Count * _interval ;//时间
                            light2.Action = 128;//动作
                            light2.Position = _position[i];//位置
                            light2.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light2);
                        }
                        if (_lightGroup.Count != 0) {
                            mTime += _lightGroup[_lightGroup.Count-1].Time;
                        }
                    }
                }
                return _lightGroup;
            }
            else
            {
                return null;
            }
        }
        private static List<Light> CreateLightGroupExtension2(string commandLine, Dictionary<string, List<int>> rangeDictionary, Dictionary<string, List<int>> colorDictionary)
        {
            MatchCollection matchLightContent = Regex.Matches(commandLine, @"\(([0-9a-zA-Z_\u4e00-\u9fa5]+,){6}[0-9a-zA-Z_\u4e00-\u9fa5]+\)");
            String[] strsContent = matchLightContent[0].Value.Substring(1, matchLightContent[0].Value.Length - 2).Split(',');
            //有七个参数
            if (strsContent.Length == 7)
            {
                List<Light> _lightGroup = new List<Light>();
                //时间
                int _time = int.Parse(strsContent[0]);
                String _range = strsContent[1];
                int _interval = int.Parse(strsContent[2]);
                int _continued = int.Parse(strsContent[3]);
                String _color = strsContent[4];
                String _type = strsContent[5];
                String _action = strsContent[6];

                List<int> _position = new List<int>();
                _position.AddRange(rangeDictionary[_range].ToArray());
                if (_type.Equals("Up"))
                {
                    if (_action.Equals("All"))
                    {
                        //开始
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 144;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                        //结束
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _continued + _time + i * _interval + j * _continued;//时间
                                light.Action = 128;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                    }
                    else if (_action.Equals("Open"))
                    {
                        //只开始
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 144;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                    }
                    else if (_action.Equals("Close"))
                    {
                        //只结束
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 128;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                    }

                }
                else if (_type.Equals("Down"))
                {
                    _position.Reverse();
                    if (_action.Equals("All"))
                    {
                        //开始
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 144;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                        //结束
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _continued + _time + i * _interval + j * _continued;//时间
                                light.Action = 128;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                    }
                    else if (_action.Equals("Open"))
                    {
                        //只开始
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 144;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                    }
                    else if (_action.Equals("Close"))
                    {
                        //只结束
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 128;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                    }
                }
                else if (_type.Equals("UpDown"))
                {
                    List<int> _relList = new List<int>();
                    _relList.AddRange(_position.ToArray().Reverse());
                    _position.AddRange(_relList.ToArray());
                    if (_action.Equals("All"))
                    {
                        //开始
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 144;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                        //结束
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _continued + _time + i * _interval + j * _continued;//时间
                                light.Action = 128;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                    }
                    else if (_action.Equals("Open"))
                    {
                        //只开始
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 144;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                    }
                    else if (_action.Equals("Close"))
                    {
                        //只结束
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 128;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                    }
                }
                else if (_type.Equals("DownUp"))
                {
                    _position.Reverse();
                    List<int> _relList = new List<int>();
                    _relList.AddRange(_position.ToArray().Reverse());
                    _position.AddRange(_relList.ToArray());
                    if (_action.Equals("All"))
                    {
                        //开始
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 144;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                        //结束
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _continued + _time + i * _interval + j * _continued;//时间
                                light.Action = 128;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                    }
                    else if (_action.Equals("Open"))
                    {
                        //只开始
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 144;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                    }
                    else if (_action.Equals("Close"))
                    {
                        //只结束
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 128;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                    }
                }
                //以下三种只支持Action等于All
                else if (_type.Equals("UpAndDown") || _type.Equals("DownAndUp"))
                {
                        //开始
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 144;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                        //结束
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _continued + _time + i * _interval + j * _continued;//时间
                                light.Action = 128;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }

                        _position.Reverse();

                        //开始
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _time + i * _interval + j * _continued;//时间
                                light.Action = 144;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                        //结束
                        for (int j = 0; j < colorDictionary[_color].Count; j++)
                        {
                            for (int i = 0; i < _position.Count; i++)
                            {
                                Light light = new Light();
                                light.Time = _continued + _time + i * _interval + j * _continued;//时间
                                light.Action = 128;//动作
                                light.Position = _position[i];//位置
                                light.Color = colorDictionary[_color][j];//位置
                                _lightGroup.Add(light);
                            }
                        }
                }
                else if (_type.Equals("FreezeFrame"))
                {
                    int mTime = _time;
                    for (int j = 0; j < colorDictionary[_color].Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            //开始
                            Light light = new Light();
                            light.Time = mTime + i * _interval;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light);
                            //结束
                            Light light2 = new Light();
                            light2.Time = mTime + _position.Count * _interval;//时间
                            light2.Action = 128;//动作
                            light2.Position = _position[i];//位置
                            light2.Color = colorDictionary[_color][j];//位置
                            _lightGroup.Add(light2);
                        }
                        if (_lightGroup.Count != 0)
                        {
                            mTime += _lightGroup[_lightGroup.Count - 1].Time;
                        }
                    }
                }
                return _lightGroup;
            }
            else
            {
                return null;
            }
        }
        private static List<Light> Animation(string commandLine)
        {
            List<Light> mLl = new List<Light>();
            MatchCollection matchLightContent = Regex.Matches(commandLine, @"\([\S\s]*\)");
            if (matchLightContent[0].Value.Length <= 2)
                return null;
            String strContent = matchLightContent[0].Value.Substring(1, matchLightContent[0].Value.Length - 2);
            String[] strContents = strContent.Split(',');
            if (strContents[0].Equals("Translation")) {
                String[] strsNumber = strContents[1].Substring(1, strContents[1].Length - 2).Split(' ');
                List<List<int>> _numberList = new List<List<int>>();
                List<int> list = new List<int>();
                for (int i = 0; i < strsNumber.Count(); i++)
                {
                    if (list.Count != 10)
                    {
                        try
                        {
                            list.Add(Convert.ToInt32(strsNumber[i]));
                        }
                        catch
                        {
                            return null;
                        }
                    }
                    if (list.Count == 10)
                    {
                        _numberList.Add(list.ToList());
                        list.Clear();
                    }
                }
                //使用垂直int集合
                List<List<int>> lli = Model.IntCollection.VerticalIntList.ToList();
                int count = 10 + _numberList.Count;//从0开始算，第0的时候就在最右边出现
                int now = -_numberList.Count + 1;
                int plus = 16;
                while (now < count)
                {
                    for (int i = 0; i < _numberList.Count; i++)
                    {
                        //判断这行能否出现在画板上
                        if (now + i >= 0 && now + i < lli.Count)
                        {
                            for (int j = 0; j < _numberList[i].Count; j++)
                            {
                                if (_numberList[i][j] == 1)
                                {
                                    mLl.Add(new Light((now + _numberList.Count - 1) * plus, 144, lli[now + i][j], 5));
                                    mLl.Add(new Light((now + _numberList.Count) * plus, 128, lli[now + i][j], 64));
                                }
                            }
                        }
                    }
                    now++;
                }
                for (int x = mLl.Count - 1; x >= 0; x--)
                {
                    if (mLl[x].Position == -1)
                    {
                        mLl.RemoveAt(x);
                    }
                }
                return mLl;
            }
            return null;
        }

        private static List<Light> Automatic(string commandLine)
        {
            MatchCollection matchLightContent = Regex.Matches(commandLine, @"\([\S\s]*\)");
            if (matchLightContent[0].Value.Length <= 2)
                return null;
            String strContent = matchLightContent[0].Value.Substring(1, matchLightContent[0].Value.Length - 2);

            String[] strsContent = strContent.Split(',');
            if (strsContent.Count() == 2)
            {
                if (strsContent[0].Trim().Equals("RhombusDiffusion"))
                {
                    return RhombusDiffusion(Convert.ToInt32(strsContent[1]));
                }
                else if (strsContent[0].Trim().Equals("Cross"))
                {
                    return Cross(Convert.ToInt32(strsContent[1]));
                }
                else
                {
                    return null;
                }
            }
            else if (strsContent.Count() == 4)
            {
                if (strsContent[0].Trim().Equals("RandomFountain"))
                {
                    if (strsContent[3].Equals("true")) {
                        return RandomFountain(Convert.ToInt32(strsContent[1]), Convert.ToInt32(strsContent[2]), true);
                    }
                }
            }
            return null;
        }

        private static List<Light> RandomFountain(int max, int min, bool containBorder)
        {
            //Random类默认的无参构造函数可以根据当前系统时钟为种子,进行一系列算法得出要求范围内的伪随机数.
            List<Light> mLl = new List<Light>();
            List<List<int>> lli = Model.IntCollection.VerticalIntList.ToList();
            List<int> list = new List<int>();
            Random rd = new Random();
            for (int i = 0; i < 10; i++) {
                list.Add(rd.Next(min, max+1));
            }
            int plus = 16;
            int count = list.Max();
            int now = 0;
            while (now <= count) {
                for (int i = 0; i < list.Count; i++) {
                    if (list[i] >= now && now != 0)
                    {
                        mLl.Add(new Light((now-1) * plus, 144, lli[i][10 - now], 5));
                        mLl.Add(new Light((count * 2 - now ) * plus, 128, lli[i][10 - now], 64));
                    }
                }
                now++;
            }
            RemoveIncorrectlyData(mLl);
            return mLl;
        }

        private static List<Light> RhombusDiffusion(int startPosition)
        {
            if (startPosition < 28 || startPosition > 123)
                return null;
            List<Light> mLl = new List<Light>();
            Dictionary<int, int[]> _dictionary = new Dictionary<int, int[]>();
            List<List<int>> all = new List<List<int>>();
            all.Add(new List<int>() { -1,116, 117, 118, 119, 120, 121, 122, 123, -1 });
            all.Add(new List<int>() { 115, 36, 37, 38, 39, 68, 69, 70, 71, 107 });
            all.Add(new List<int>() { 114, 40, 41, 42, 43, 72, 73, 74, 75, 106 });
            all.Add(new List<int>() { 113, 44, 45, 46, 47, 76, 77, 78, 79, 105 });
            all.Add(new List<int>() { 112, 48, 49, 50, 51, 80, 81, 82, 83, 104 });
            all.Add(new List<int>() { 111, 52, 53, 54, 55, 84, 85, 86, 87, 103 });
            all.Add(new List<int>() { 110, 56, 57, 58, 59, 88, 89, 90, 91, 102 });
            all.Add(new List<int>() { 109, 60, 61, 62, 63, 92, 93, 94, 95, 101 });
            all.Add(new List<int>() { 108, 64, 65, 66, 67, 96, 97, 98, 99, 100 });
            all.Add(new List<int>() { -1, 28, 29, 30, 31, 32, 33, 34, 35, -1 });
            
            int positionX = 0;//一行第几个
            int positionY = 0;//第几列
            for (int i = 0; i < all.Count; i++) {
                if (all[i].Contains(startPosition))
                {
                    for (int j = 0; j < all[i].Count; j++)
                    {
                        if (all[i][j] == startPosition)
                        {
                            _dictionary.Add(i, new int[2] { j, j });
                            positionX = j;
                            positionY = i;
                        }
                    }
                }
                else {
                    _dictionary.Add(i, new int[2] { -1, -1 });
                }
            }
            int count = 0;
            int plus = 16;
            while (true)
            {
                if (count != 0) {
                    if (positionY + count < all.Count) {
                        _dictionary[positionY + count][0] = positionX;
                        _dictionary[positionY + count][1] = positionX;
                    }
                    if (positionY - count >= 0)
                    {
                        _dictionary[positionY - count][0] = positionX;
                        _dictionary[positionY - count][1] = positionX;
                    }
                }
                for (int x = 0;x< _dictionary.Count;x++) {
                    if (_dictionary[x][0] == _dictionary[x][1] && _dictionary[x][0] != -1)
                    {
                        mLl.Add(new Light(count * plus, 144, all[x][_dictionary[x][0]], 5));
                        mLl.Add(new Light((count + 1) * plus, 128, all[x][_dictionary[x][0]], 64));
                        if (_dictionary[x][0] > -1)
                        {
                            _dictionary[x][0]--;
                        }
                        if (_dictionary[x][1] < 10)
                        {
                            _dictionary[x][1]++;
                        }
                        continue;
                    }
                    if (_dictionary[x][0] >= 0)
                    {
                        mLl.Add(new Light(count * plus, 144, all[x][_dictionary[x][0]], 5));
                        mLl.Add(new Light((count + 1) * plus, 128, all[x][_dictionary[x][0]], 64));
                        if (_dictionary[x][0] > -1)
                        {
                            _dictionary[x][0]--;
                        }
                    }
                    if (_dictionary[x][1] <= 9 && _dictionary[x][1]>=0)
                    {
                        mLl.Add(new Light(count * plus, 144, all[x][_dictionary[x][1]], 5));
                        mLl.Add(new Light((count + 1) * plus, 128, all[x][_dictionary[x][1]], 64));
                        if (_dictionary[x][1] < 10 && _dictionary[x][1] >= 0)
                        {
                            _dictionary[x][1]++;
                        }
                    }
                }
                int i = 0;
                foreach (var item in _dictionary) 
                {
                    if (item.Value[0] < 0 && item.Value[1] > 9) {
                        i++;
                    }
                }
                //全员完毕
                if (i == _dictionary.Count) {
                    break;
                }
                count++;
            }
            RemoveIncorrectlyData(mLl);
            return mLl;
        }

        private static void RemoveIncorrectlyData(List<Light> mLl) {
            for (int x = mLl.Count - 1; x >= 0; x--)
            {
                if (mLl[x].Position == -1)
                {
                    mLl.RemoveAt(x);
                }
            }
        }

        private static List<Light> Cross(int startPosition)
        {
            if (startPosition < 28 || startPosition > 123)
                return null;
            List<Light> mLl = new List<Light>();
            bool bTop = true;
            bool bBottom = true;
            bool bLeft = true;
            bool bRight = true;
            int iTop = startPosition;
            int iBottom = startPosition;
            int iLeft = startPosition;
            int iRight = startPosition;
            int count = 0;
            int plus = 16;
            while (bTop || bBottom || bLeft || bRight) {
                if (count == 0) {
                    mLl.Add(new Light(count * plus, 144, startPosition, 5));
                    mLl.Add(new Light((count+1) * plus, 128, startPosition, 64));
                    count++;
                    continue;
                }
                if (bTop) {
                    //最上面
                    if (iTop >= 64 && iTop <= 67)
                    {
                        iTop -= 36;
                    }
                    else if (iTop >= 96 && iTop <= 99)
                    {
                        iTop -= 64;
                    }
                    else if (iTop >= 28 && iTop <= 35)
                    {
                        bTop = false;
                    }
                    else {
                        iTop += 4;
                    }
                    if (bTop) {
                        mLl.Add(new Light(count * plus, 144, iTop, 5));
                        mLl.Add(new Light((count + 1) * plus, 128, iTop, 64));
                    }
                }
                if (bBottom)
                {
                    //最下面
                    if (iBottom >= 36 && iBottom <= 39)
                    {
                        iBottom += 80;
                    }
                    else if (iBottom >= 68 && iBottom <= 71)
                    {
                        iBottom += 52;
                    }
                    else if (iBottom >= 116 && iBottom <= 123)
                    {
                        bBottom = false;
                    }
                    else {
                        iBottom -= 4;
                    }
                    if (bBottom)
                    {
                        mLl.Add(new Light(count * plus, 144, iBottom, 5));
                        mLl.Add(new Light((count + 1) * plus, 128, iBottom, 64));
                    }
                }
                if (bLeft)
                {
                    //最左面
                    if (iLeft >= 36 && iLeft <= 67 && iLeft % 4 == 0)
                    {
                        iLeft = 124 - (iLeft / 4);
                    }
                    else if (iLeft >= 68 && iLeft <= 99 && iLeft % 4 == 0)
                    {
                        iLeft -= 29;
                    }
                    else if (iLeft >= 100 && iLeft <= 107)
                    {
                        iLeft = (iLeft - 100) * 5 +1;
                    }
                    else if (iLeft >= 108 && iLeft <= 115 || iLeft==116 || iLeft==28)
                    {
                        bLeft = false;
                    }
                    else
                    {
                        iLeft -= 1;
                    }
                    if (bLeft)
                    {
                        mLl.Add(new Light(count * plus, 144, iLeft, 5));
                        mLl.Add(new Light((count + 1) * plus, 128, iLeft, 64));
                    }
                }
                if (bRight)
                {
                    //最右面
                    if (iRight >= 68 && iRight <= 99 && (iRight - 3) % 4 == 0)
                    {
                        iRight = 124 - ((iRight - 3) / 4);
                    }
                    else if (iRight >= 36 && iRight <= 67 && (iRight - 3) % 4 == 0)
                    {
                        iRight += 29;
                    }
                    else if (iRight >= 108 && iRight <= 115)
                    {
                        iRight = 64 - (iRight - 108) * 4;
                    }
                    else if (iRight >= 100 && iRight <= 107 || iRight == 123 || iRight == 35)
                    {
                        bRight = false;
                    }
                    else
                    {
                        iRight += 1;
                    }
                    if (bRight)
                    {
                        mLl.Add(new Light(count * plus, 144, iRight, 5));
                        mLl.Add(new Light((count + 1) * plus, 128, iRight, 64));
                    }
                }
                count++;
            }
            return mLl;
        }

        /// <summary>
        /// 通过Midi文件创建灯光组
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name="lastFilePath"></param>
        /// <returns></returns>
        public static List<Light> FromMidiFile(String commandLine,String lastFilePath) {
         
            MatchCollection matchLightContent = Regex.Matches(commandLine, @"\([\S\s]*\)");
            if (matchLightContent[0].Value.Length <= 4)
                return null;
            String strsContent = matchLightContent[0].Value.Substring(2, matchLightContent[0].Value.Length - 4);

            String filePath = String.Empty;
            String nowRelPath = String.Empty;
            String[] importContent = strsContent.Split('^');
            if (importContent.Count() > 2)
            {
                return null;
            }
            if (importContent.Count() == 2)
            {
                String importType = importContent[0];//Abspath - 绝对路径，Relpath - 相对路径，Lib - 库路径(默认为库文件，可以不写)，Resource - 资源文件
                if (importType.Equals("Abspath"))
                {
                    //Absolutely
                    if (File.Exists(importContent[1]))
                    {
                        filePath = importContent[1];
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (importType.Equals("Relpath"))
                {
                    //Relative
                    nowRelPath = Path.GetDirectoryName(lastFilePath);

                    var relativePath = importContent[1];
                    Directory.SetCurrentDirectory(nowRelPath);
                    // Relative
                    if (File.Exists(Path.GetFullPath(relativePath) + ".mid") || File.Exists(System.IO.Path.GetFullPath(relativePath) + ".midi"))
                    {
                        filePath = Path.GetFullPath(relativePath);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (importType.Equals("Resource"))
                {
                    //Resource
                    if (lastFilePath.Equals(""))
                    {
                        filePath = StaticConstant.mw3.lastProjectPath + @"\Resource\" + importContent[1];
                    }
                    else {
                        if (File.Exists(Path.GetDirectoryName(Path.GetDirectoryName(lastFilePath)) + @"\Resource\" + importContent[1] + ".mid")
                            || File.Exists(Path.GetDirectoryName(Path.GetDirectoryName(lastFilePath)) + @"\Resource\" + importContent[1] + ".midi"))
                        {
                            filePath = Path.GetDirectoryName(Path.GetDirectoryName(lastFilePath)) + @"\Resource\" + importContent[1];
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            else
            {
                //Midi
                if (File.Exists(Path.GetDirectoryName(Path.GetDirectoryName(lastFilePath)) + @"\Midi\" + strsContent))
                {
                    filePath = Path.GetDirectoryName(Path.GetDirectoryName(lastFilePath)) + @"\Midi\" + strsContent;
                }
                else
                {
                    return null;
                }
            }
            filePath += ".mid";
            return Business.FileBusiness.CreateInstance().ReadMidiFile(filePath);
        }
        /// <summary>
        /// 通过Light文件创建灯光组
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name="lastFilePath"></param>
        /// <returns></returns>
        public static List<Light> FromLightFile(String commandLine, String lastFilePath)
        {
            MatchCollection matchLightContent = Regex.Matches(commandLine, @"\([\S\s]*\)");
            if (matchLightContent[0].Value.Length <= 4)
                return null;
            String strsContent = matchLightContent[0].Value.Substring(2, matchLightContent[0].Value.Length - 4);
            String filePath = String.Empty;
            String nowRelPath = String.Empty;
            String[] importContent = strsContent.Split('^');
            if (importContent.Count() > 2)
            {
                return null;
            }
            if (importContent.Count() == 2)
            {
                String importType = importContent[0];//Abspath - 绝对路径，Relpath - 相对路径，Lib - 库路径(默认为库文件，可以不写)，Resource - 资源文件
                if (importType.Equals("Abspath"))
                {
                    //Absolutely
                    if (File.Exists(importContent[1]))
                    {
                        filePath = importContent[1];
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (importType.Equals("Relpath"))
                {
                    //Relative
                    nowRelPath = System.IO.Path.GetDirectoryName(lastFilePath);
                    var relativePath = importContent[1];
                    Directory.SetCurrentDirectory(nowRelPath);
                    // Relative
                    if (File.Exists(System.IO.Path.GetFullPath(relativePath+".light")))
                    {
                        filePath = System.IO.Path.GetFullPath(relativePath);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (importType.Equals("Resource"))
                {
                    //Resource
                    if (lastFilePath.Equals(""))
                    {
                        filePath = StaticConstant.mw3.lastProjectPath + @"\Resource\" + importContent[1];
                    }
                    else
                    {
                        if (File.Exists(Path.GetDirectoryName(Path.GetDirectoryName(lastFilePath)) + @"\Resource\" + importContent[1] + ".light"))
                        {
                            filePath = Path.GetDirectoryName(Path.GetDirectoryName(lastFilePath)) + @"\Resource\" + importContent[1];
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            else
            {
                //Midi
                if (File.Exists(Path.GetDirectoryName(Path.GetDirectoryName(lastFilePath)) + @"\Midi\" + strsContent))
                {
                    filePath = Path.GetDirectoryName(Path.GetDirectoryName(lastFilePath)) + @"\Midi\" + strsContent;
                }
                else
                {
                    return null;
                }
            }
            filePath += ".light";
            return Business.FileBusiness.CreateInstance().ReadLightFile(filePath);
        }

        /// <summary>
        /// Create下的原快速生成方式创建灯光组
        /// </summary>
        /// <param name="commandLine">语句</param>
        /// <param name="lightGroupDictionary">语句添加进哪个字典</param>
        /// <returns></returns>
        public static List<Light> CreateLightGroup(String commandLine,
           Dictionary<String, List<int>> rangeDictionary, Dictionary<String, List<int>> colorDictionary)
        {
            MatchCollection matchLightContent = Regex.Matches(commandLine, @"\(([0-9a-zA-Z_\u4e00-\u9fa5]+,){4}[0-9a-zA-Z_\u4e00-\u9fa5]+\)");
            String[] strsContent = matchLightContent[0].Value.Substring(1, matchLightContent[0].Value.Length - 2).Split(',');
            //有五个参数
            if (strsContent.Length == 5)
            {
                List<Light> _lightGroup = new List<Light>();
                //时间
                int _time = int.Parse(strsContent[0]);
                String _range = strsContent[1];
                int _interval = int.Parse(strsContent[2]);
                int _continued = int.Parse(strsContent[3]);
                String _color = strsContent[4];
                //开始
                for (int j = 0; j < colorDictionary[_color].Count; j++)
                {
                    for (int i = 0; i < rangeDictionary[_range].Count; i++)
                    {
                        Light light = new Light();
                        light.Time = _time + i * _interval + j * _continued;//时间
                        light.Action = 144;//动作
                        light.Position = rangeDictionary[_range][i];//位置
                        light.Color = colorDictionary[_color][j];//位置
                        _lightGroup.Add(light);
                    }
                }
                //结束
                for (int j = 0; j < colorDictionary[_color].Count; j++)
                {
                    for (int i = 0; i < rangeDictionary[_range].Count; i++)
                    {
                        Light light = new Light();
                        light.Time = _continued + _time + i * _interval + j * _continued;//时间
                        light.Action = 128;//动作
                        light.Position = rangeDictionary[_range][i];//位置
                        light.Color = colorDictionary[_color][j];//位置
                        _lightGroup.Add(light);
                    }
                }

                return _lightGroup;
            }
            else
            {
                return null;
            }
        }
    }
}
//右上斜线
 //if (iRightTop >= 68 && iRightTop <= 95 && (iRightTop - 3) % 4 == 0)
 //                   {
 //                       iRightTop = 124 - ((iRightTop - 3) / 4) - 1;
 //                   }
 //                   else if (iRightTop >= 36 && iRightTop <= 63 && (iRightTop - 3) % 4 == 0)
 //                   {
 //                       iRightTop += 29 + 4;
 //                   }
 //                   else if (iRightTop >= 108 && iRightTop <= 115)
 //                   {
 //                       iRightTop = 64 - (iRightTop - 108) * 4 + 5;
 //                   }
 //                   else if (iRightTop >= 64 && iRightTop <= 67)
 //                   {
 //                       iRightTop -= 35;
 //                   }
 //                   else if (iRightTop >= 96 && iRightTop <= 98)
 //                   {
 //                       iRightTop -= 63;
 //                   }
 //                   else if (iRightTop >= 28 && iRightTop <= 35 || iRightTop >= 100 && iRightTop <= 107 || iRightTop == 99)
 //                   {
 //                       bRightTop = false;
 //                   }
 //                   else if (iRightTop == 123)
 //                   {
 //                       iRightTop = 107;
 //                   }
 //                   else if (iRightTop == 119)
 //                   {
 //                       iRightTop = 68;
 //                   }
 //                   else if (iRightTop >116 && iRightTop<118)
 //                   {
 //                       iRightTop -= 79;
 //                   }
 //                   else if (iRightTop > 120 && iRightTop< 122)
 //                   {
 //                       iRightTop -= 51;
 //                   }
 //                   else
 //                   {
 //                       iRightTop += 5;
 //                   }
 //                   if (bRightTop)
 //                   {
 //                       mLl.Add(new Light(count* plus, 144, iRightTop, 5));
 //                       mLl.Add(new Light((count + 1) * plus, 128, iRightTop, 64));
 //                   }
