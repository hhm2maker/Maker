using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static LightGroup CreateLightGroup( int _time , List<int> rangeList, int _interval, int _continued,  List<int> colorList, int _type, int _action)
        {
            LightGroup _lightGroup = new LightGroup();
                //时间
                List<int> _position = new List<int>();
                _position.AddRange(rangeList.ToArray());
            if (_type == UP)
            {
                if (_action == ALL)
                {
                    //开始
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _time + i * _interval + j * _continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _continued + _time + i * _interval + j * _continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_action == OPEN)
                {
                    //只开始
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _time + i * _interval + j * _continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_action == CLOSE)
                {
                    //只结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _time + i * _interval + j * _continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
            }
            else if (_type == DOWN)
            {
                _position.Reverse();
                if (_action == ALL)
                {
                    //开始
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _time + i * _interval + j * _continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _continued + _time + i * _interval + j * _continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_action == OPEN)
                {
                    //只开始
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _time + i * _interval + j * _continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_action == CLOSE)
                {
                    //只结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _time + i * _interval + j * _continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
            }
            else if (_type == UPDOWN)
            {
                List<int> _relList = new List<int>();
                _relList.AddRange(_position.ToArray().Reverse());
                _position.AddRange(_relList.ToArray());
                if (_action == ALL)
                {
                    //开始
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _time + i * _interval + j * _continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _continued + _time + i * _interval + j * _continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_action==OPEN)
                {
                    //只开始
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _time + i * _interval + j * _continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_action == CLOSE)
                {
                    //只结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _time + i * _interval + j * _continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
            }
            else if (_type == DOWNUP)
            {
                _position.Reverse();
                List<int> _relList = new List<int>();
                _relList.AddRange(_position.ToArray().Reverse());
                _position.AddRange(_relList.ToArray());
                if (_action == ALL)
                {
                    //开始
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _time + i * _interval + j * _continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _continued + _time + i * _interval + j * _continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_action ==OPEN)
                {
                    //只开始
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _time + i * _interval + j * _continued,//时间
                                Action = 144,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_action==CLOSE)
                {
                    //只结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light
                            {
                                Time = _time + i * _interval + j * _continued,//时间
                                Action = 128,//动作
                                Position = _position[i],//位置
                                Color = colorList[j]//位置
                            };
                            _lightGroup.Add(light);
                        }
                    }
                }
            }
            //以下三种只支持Action等于All
            else if (_type ==UPANDDOWN || _type == DOWNANDUP)
            {
                //开始
                for (int j = 0; j < colorList.Count; j++)
                {
                    for (int i = 0; i < _position.Count; i++)
                    {
                        Light light = new Light
                        {
                            Time = _time + i * _interval + j * _continued,//时间
                            Action = 144,//动作
                            Position = _position[i],//位置
                            Color = colorList[j]//位置
                        };
                        _lightGroup.Add(light);
                    }
                }
                //结束
                for (int j = 0; j < colorList.Count; j++)
                {
                    for (int i = 0; i < _position.Count; i++)
                    {
                        Light light = new Light
                        {
                            Time = _continued + _time + i * _interval + j * _continued,//时间
                            Action = 128,//动作
                            Position = _position[i],//位置
                            Color = colorList[j]//位置
                        };
                        _lightGroup.Add(light);
                    }
                }

                _position.Reverse();

                //开始
                for (int j = 0; j < colorList.Count; j++)
                {
                    for (int i = 0; i < _position.Count; i++)
                    {
                        Light light = new Light
                        {
                            Time = _time + i * _interval + j * _continued,//时间
                            Action = 144,//动作
                            Position = _position[i],//位置
                            Color = colorList[j]//位置
                        };
                        _lightGroup.Add(light);
                    }
                }
                //结束
                for (int j = 0; j < colorList.Count; j++)
                {
                    for (int i = 0; i < _position.Count; i++)
                    {
                        Light light = new Light
                        {
                            Time = _continued + _time + i * _interval + j * _continued,//时间
                            Action = 128,//动作
                            Position = _position[i],//位置
                            Color = colorList[j]//位置
                        };
                        _lightGroup.Add(light);
                    }
                }
            }
            else if (_type == FREEZEFRAME)
            {
                int mTime = _time;
                for (int j = 0; j < colorList.Count; j++)
                {
                    for (int i = 0; i < _position.Count; i++)
                    {
                        //开始
                        Light light = new Light
                        {
                            Time = mTime + i * _interval,//时间
                            Action = 144,//动作
                            Position = _position[i],//位置
                            Color = colorList[j]//位置
                        };
                        _lightGroup.Add(light);
                        //结束
                        Light light2 = new Light
                        {
                            Time = mTime + _position.Count * _interval,//时间
                            Action = 128,//动作
                            Position = _position[i],//位置
                            Color = colorList[j]//位置
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
    }
}
