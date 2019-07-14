using Maker.Business;
using Maker.Business.Model.OperationModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Operation
{
    public class Create
    {
        public static int UP = 0;
        public static int DOWN = 1;
        public static int UPDOWN = 2;
        public static int DOWNUP = 3;
        public static int UPANDDOWN = 4;
        public static int DOWNANDUP = 5;
        public static int FREEZEFRAME = 6;

        public static int ALL = 10;
        public static int OPEN = 11;
        public static int CLOSE = 12;

        
        public static LightGroup CreateLightGroup(CreateFromQuickOperationModel createFromQuickOperationModel)
        {
            LightGroup _lightGroup = new LightGroup();
                //时间
                List<int> _position = new List<int>();
                _position.AddRange(createFromQuickOperationModel.RangeList.ToArray());
            if (createFromQuickOperationModel.Type == UP)
            {
                if (createFromQuickOperationModel.Action == ALL)
                {
                    //开始
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Continued + createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (createFromQuickOperationModel.Action == OPEN)
                {
                    //只开始
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (createFromQuickOperationModel.Action == CLOSE)
                {
                    //只结束
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
            }
            else if (createFromQuickOperationModel.Type == DOWN)
            {
                _position.Reverse();
                if (createFromQuickOperationModel.Action == ALL)
                {
                    //开始
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Continued + createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (createFromQuickOperationModel.Action == OPEN)
                {
                    //只开始
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (createFromQuickOperationModel.Action == CLOSE)
                {
                    //只结束
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
            }
            else if (createFromQuickOperationModel.Type == UPDOWN)
            {
                List<int> _relList = new List<int>();
                _relList.AddRange(_position.ToArray().Reverse());
                _position.AddRange(_relList.ToArray());
                if (createFromQuickOperationModel.Action == ALL)
                {
                    //开始
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Continued + createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (createFromQuickOperationModel.Action == OPEN)
                {
                    //只开始
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (createFromQuickOperationModel.Action == CLOSE)
                {
                    //只结束
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
            }
            else if (createFromQuickOperationModel.Type == DOWNUP)
            {
                _position.Reverse();
                List<int> _relList = new List<int>();
                _relList.AddRange(_position.ToArray().Reverse());
                _position.AddRange(_relList.ToArray());
                if (createFromQuickOperationModel.Action == ALL)
                {
                    //开始
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Continued + createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (createFromQuickOperationModel.Action == OPEN)
                {
                    //只开始
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (createFromQuickOperationModel.Action == CLOSE)
                {
                    //只结束
                    for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = createFromQuickOperationModel.ColorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
            }
            //以下三种只支持Action等于All
            else if (createFromQuickOperationModel.Type == UPANDDOWN || createFromQuickOperationModel.Type == DOWNANDUP)
            {
                //开始
                for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                {
                    for (int i = 0; i < _position.Count; i++)
                    {
                        Light light = new Light
                        {
                            Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                            Action = 144,//动作
                            Position = _position[i],//位置
                            Color = createFromQuickOperationModel.ColorList[j]//位置
                        };
                        _lightGroup.Add(light);
                    }
                }
                //结束
                for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                {
                    for (int i = 0; i < _position.Count; i++)
                    {
                        Light light = new Light
                        {
                            Time = createFromQuickOperationModel.Continued + createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                            Action = 128,//动作
                            Position = _position[i],//位置
                            Color = createFromQuickOperationModel.ColorList[j]//位置
                        };
                        _lightGroup.Add(light);
                    }
                }

                _position.Reverse();

                //开始
                for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                {
                    for (int i = 0; i < _position.Count; i++)
                    {
                        Light light = new Light
                        {
                            Time = createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                            Action = 144,//动作
                            Position = _position[i],//位置
                            Color = createFromQuickOperationModel.ColorList[j]//位置
                        };
                        _lightGroup.Add(light);
                    }
                }
                //结束
                for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                {
                    for (int i = 0; i < _position.Count; i++)
                    {
                        Light light = new Light
                        {
                            Time = createFromQuickOperationModel.Continued + createFromQuickOperationModel.Time + i * createFromQuickOperationModel.Interval + j * createFromQuickOperationModel.Continued,//时间
                            Action = 128,//动作
                            Position = _position[i],//位置
                            Color = createFromQuickOperationModel.ColorList[j]//位置
                        };
                        _lightGroup.Add(light);
                    }
                }
            }
            else if (createFromQuickOperationModel.Type == FREEZEFRAME)
            {
                int mTime = createFromQuickOperationModel.Time;
                for (int j = 0; j < createFromQuickOperationModel.ColorList.Count; j++)
                {
                    for (int i = 0; i < _position.Count; i++)
                    {
                        //开始
                        Light light = new Light
                        {
                            Time = mTime + i * createFromQuickOperationModel.Interval,//时间
                            Action = 144,//动作
                            Position = _position[i],//位置
                            Color = createFromQuickOperationModel.ColorList[j]//位置
                        };
                        _lightGroup.Add(light);
                        //结束
                        Light light2 = new Light
                        {
                            Time = mTime + _position.Count * createFromQuickOperationModel.Interval,//时间
                            Action = 128,//动作
                            Position = _position[i],//位置
                            Color = createFromQuickOperationModel.ColorList[j]//位置
                        };
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

       
        public static LightGroup CreateFromLightScriptFile(string fileName, string stepName)
        {
            ProjectConfigModel projectConfigModel = new ProjectConfigModel();
            XmlSerializerBusiness.Load(ref projectConfigModel, "Config/project.xml");
            return ScriptFileBusiness.FileToLight(AppDomain.CurrentDomain.BaseDirectory + @"Project\"+ projectConfigModel.Path + @"\LightScript\"+fileName,stepName);
        }

        public static LightGroup CreateFromLightFile(string fileName)
        {
            ProjectConfigModel projectConfigModel = new ProjectConfigModel();
            XmlSerializerBusiness.Load(ref projectConfigModel, "Config/project.xml");
            return FileBusiness.CreateInstance().ReadLightFile(AppDomain.CurrentDomain.BaseDirectory + @"Project\" + projectConfigModel.Path + @"\Light\" + fileName);
        }

        public static LightGroup CreateFromMidiFile(string fileName)
        {
            ProjectConfigModel projectConfigModel = new ProjectConfigModel();
            XmlSerializerBusiness.Load(ref projectConfigModel, "Config/project.xml");
            return FileBusiness.CreateInstance().ReadMidiFile(AppDomain.CurrentDomain.BaseDirectory + @"Project\" + projectConfigModel.Path + @"\Light\" + fileName);
        }

        public static LightGroup CreateFromLimitlessLampFile(string fileName)
        {
            ProjectConfigModel projectConfigModel = new ProjectConfigModel();
            XmlSerializerBusiness.Load(ref projectConfigModel, "Config/project.xml");
            return FileBusiness.CreateInstance().ReadLimitlessLampFile(AppDomain.CurrentDomain.BaseDirectory + @"Project\" + projectConfigModel.Path + @"\LimitlessLamp\" + fileName);
        }
    }
}
