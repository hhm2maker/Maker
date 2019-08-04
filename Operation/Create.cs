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
      
        public static int RHOMBUSDIFFUSION = 20;
        public static int CROSS = 21;
        public static int RANDOMFOUNTAIN = 22;
        public static LightGroup Automatic(int automaticType,params int[] nums)
        {
            if (automaticType == RHOMBUSDIFFUSION)
            {
                return RhombusDiffusion(nums[0]);
            }
            else if (automaticType == CROSS)
            {
                return Cross(nums[0]);
            }
            else if (automaticType == RANDOMFOUNTAIN)
            {
                return RandomFountain(nums[0],nums[1],true);
            }
        
            return null;
        }

        private static LightGroup RandomFountain(int max, int min, bool containBorder)
        {
            //Random类默认的无参构造函数可以根据当前系统时钟为种子,进行一系列算法得出要求范围内的伪随机数.
            LightGroup mLl = new LightGroup();
            List<List<int>> lli = IntCollection.VerticalIntList.ToList();
            List<int> list = new List<int>();
            Random rd = new Random();
            for (int i = 0; i < 10; i++)
            {
                list.Add(rd.Next(min, max + 1));
            }
            int plus = 16;
            int count = list.Max();
            int now = 0;
            while (now <= count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] >= now && now != 0)
                    {
                        mLl.Add(new Light((now - 1) * plus, 144, lli[i][10 - now], 5));
                        mLl.Add(new Light((count * 2 - now) * plus, 128, lli[i][10 - now], 64));
                    }
                }
                now++;
            }
            RemoveIncorrectlyData(mLl);
            return mLl;
        }

        private static LightGroup RhombusDiffusion(int startPosition)
        {
            if (startPosition >= 100)
                return null;
            LightGroup mLl = new LightGroup();
            Dictionary<int, int[]> _dictionary = new Dictionary<int, int[]>();
            List<List<int>> all = new List<List<int>>();
            all.Add(new List<int>() { 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 });
            all.Add(new List<int>() { 80, 81, 82, 83, 84, 85, 86, 87, 88, 89 });
            all.Add(new List<int>() { 70, 71, 72, 73, 74, 75, 76, 77, 78, 79 });
            all.Add(new List<int>() { 60, 61, 62, 63, 64, 65, 66, 67, 68, 69 });
            all.Add(new List<int>() { 50, 51, 52, 53, 54, 55, 56, 57, 58, 59 });
            all.Add(new List<int>() { 40, 41, 42, 43, 44, 45, 46, 47, 48, 49 });
            all.Add(new List<int>() { 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 });
            all.Add(new List<int>() { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 });
            all.Add(new List<int>() { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 });
            all.Add(new List<int>() {0,1,2,3,4,5,6,7,8,9 });

            int positionX = 0;//一行第几个
            int positionY = 0;//第几列
            for (int i = 0; i < all.Count; i++)
            {
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
                else
                {
                    _dictionary.Add(i, new int[2] { -1, -1 });
                }
            }
            int count = 0;
            int plus = 16;
            while (true)
            {
                if (count != 0)
                {
                    if (positionY + count < all.Count)
                    {
                        _dictionary[positionY + count][0] = positionX;
                        _dictionary[positionY + count][1] = positionX;
                    }
                    if (positionY - count >= 0)
                    {
                        _dictionary[positionY - count][0] = positionX;
                        _dictionary[positionY - count][1] = positionX;
                    }
                }
                for (int x = 0; x < _dictionary.Count; x++)
                {
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
                    if (_dictionary[x][1] <= 9 && _dictionary[x][1] >= 0)
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
                    if (item.Value[0] < 0 && item.Value[1] > 9)
                    {
                        i++;
                    }
                }
                //全员完毕
                if (i == _dictionary.Count)
                {
                    break;
                }
                count++;
            }
            RemoveIncorrectlyData(mLl);
            return mLl;
        }

        private static void RemoveIncorrectlyData(List<Light> mLl)
        {
            for (int x = mLl.Count - 1; x >= 0; x--)
            {
                if (mLl[x].Position == 0 || mLl[x].Position == 99)
                {
                    mLl.RemoveAt(x);
                }
            }
        }

        private static LightGroup Cross(int startPosition)
        {
            if (startPosition < 28 || startPosition > 123)
                return null;
            LightGroup mLl = new LightGroup();
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
            while (bTop || bBottom || bLeft || bRight)
            {
                if (count == 0)
                {
                    mLl.Add(new Light(count * plus, 144, startPosition, 5));
                    mLl.Add(new Light((count + 1) * plus, 128, startPosition, 64));
                    count++;
                    continue;
                }
                if (bTop)
                {
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
                    else
                    {
                        iTop += 4;
                    }
                    if (bTop)
                    {
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
                    else
                    {
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
                        iLeft = (iLeft - 100) * 5 + 1;
                    }
                    else if (iLeft >= 108 && iLeft <= 115 || iLeft == 116 || iLeft == 28)
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
    }
}
