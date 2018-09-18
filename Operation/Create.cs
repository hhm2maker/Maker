using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    public class Create
    {
        public static List<Light> CreateLightGroup( int _time , List<int> rangeList, int _interval, int _continued,  List<int> colorList, String _type)
        {
          
                List<Light> _lightGroup = new List<Light>();
                //时间
                List<int> _position = new List<int>();
                _position.AddRange(rangeList.ToArray());
                if (_type.Equals("Up"))
                {
                    //开始
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _time + i * _interval + j * _continued;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//颜色
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _continued + _time + i * _interval + j * _continued;//时间
                            light.Action = 128;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//颜色
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_type.Equals("Down"))
                {
                    _position.Reverse();
                    //开始
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _time + i * _interval + j * _continued;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _continued + _time + i * _interval + j * _continued;//时间
                            light.Action = 128;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//位置
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
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _time + i * _interval + j * _continued;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _continued + _time + i * _interval + j * _continued;//时间
                            light.Action = 128;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//位置
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
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _time + i * _interval + j * _continued;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _continued + _time + i * _interval + j * _continued;//时间
                            light.Action = 128;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_type.Equals("UpAndDown") || _type.Equals("DownAndUp"))
                {
                    //开始
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _time + i * _interval + j * _continued;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _continued + _time + i * _interval + j * _continued;//时间
                            light.Action = 128;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//位置
                            _lightGroup.Add(light);
                        }
                    }

                    _position.Reverse();

                    //开始
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _time + i * _interval + j * _continued;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                    //结束
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            Light light = new Light();
                            light.Time = _continued + _time + i * _interval + j * _continued;//时间
                            light.Action = 128;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//位置
                            _lightGroup.Add(light);
                        }
                    }
                }
                else if (_type.Equals("FreezeFrame"))
                {
                    int mTime = _time;
                    for (int j = 0; j < colorList.Count; j++)
                    {
                        for (int i = 0; i < _position.Count; i++)
                        {
                            //开始
                            Light light = new Light();
                            light.Time = mTime + i * _interval;//时间
                            light.Action = 144;//动作
                            light.Position = _position[i];//位置
                            light.Color = colorList[j];//位置
                            _lightGroup.Add(light);
                            //结束
                            Light light2 = new Light();
                            light2.Time = mTime + _position.Count * _interval;//时间
                            light2.Action = 128;//动作
                            light2.Position = _position[i];//位置
                            light2.Color = colorList[j];//位置
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
