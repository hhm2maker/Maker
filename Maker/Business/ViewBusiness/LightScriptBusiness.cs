using Maker.MethodSet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Maker.Model;
using static Maker.Model.EnumCollection;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;

namespace Maker.Business
{
    class LightScriptBusiness
    {
        private ScriptUserControl iuc; 
        private String InCommandLine;
        private String nowControlPath;
        public LightScriptBusiness(ScriptUserControl iuc,String InCommandLine,String nowControlPath)
        {
            this.iuc = iuc;
            lastFilePath = iuc.filePath;

            this.InCommandLine = InCommandLine;
            this.nowControlPath = nowControlPath;
            _globalContent.Add("Light", ContentType.Keyword);
            _globalContent.Add("LightGroup", ContentType.Keyword);
            _globalContent.Add("RangeGroup", ContentType.Keyword);
            _globalContent.Add("ColorGroup", ContentType.Keyword);
            _globalContent.Add("Create.CreateLightGroup", ContentType.Keyword);
            _globalContent.Add("new", ContentType.Keyword);
            lockedDictionary.Clear();
        }
        private Dictionary<String, List<Light>> lockedDictionary = new Dictionary<string, List<Light>>();
        public LightScriptBusiness(ScriptUserControl iuc, String InCommandLine, String nowControlPath, Dictionary<String, List<Light>> lockedDictionary)
        {
            this.iuc = iuc;
            lastFilePath = iuc.mw.LastProjectPath;

            this.InCommandLine = InCommandLine;
            this.nowControlPath = nowControlPath;
            _globalContent.Add("Light", ContentType.Keyword);
            _globalContent.Add("LightGroup", ContentType.Keyword);
            _globalContent.Add("RangeGroup", ContentType.Keyword);
            _globalContent.Add("ColorGroup", ContentType.Keyword);
            _globalContent.Add("Create.CreateLightGroup", ContentType.Keyword);
            _globalContent.Add("new", ContentType.Keyword);
            this.lockedDictionary = lockedDictionary;
        }
        public LightScriptBusiness()
        { }


        public enum ContentType
        {
            Null = -2,
            Keyword = -1,
            Light = 0,
            LightGroup = 1,
            RangeGroup = 2,
            ColorGroup = 3,
        }

        int importPosition = 0;
        Dictionary<String, String> _importContent = new Dictionary<String, String>();//导入内容 名称 - 路径
        Dictionary<String, ContentType> _globalContent = new Dictionary<String, ContentType>();//全局内容

        private List<Light> resultList = new List<Light>();
        public List<Light> GetResult(String partName)
        {
            resultList.Clear();
            _importContent.Clear();//清空导入内容
            nowRelPath = String.Empty;

            string _realName = string.Empty;
            if (partName == null)
            {
                _realName = "Main";
            }
            else {
                _realName = partName;
            }
            resultList = ScriptToLightGroup(InCommandLine, _realName);
            return resultList;
            //List<string> mKeys = new List<string>(_globalContent.Keys);
            //for (int i = _globalContent.Count - 1; i >= 0; i--)
            //{
            //    if (_globalContent[mKeys[i]] != ContentType.Keyword)
            //    {
            //        _globalContent.Remove(mKeys[i]);
            //    }
            //}
        }

      
        public Dictionary<String, String> GetCatalog(ScriptUserControl iuc,String scriptText) {
            iuc.extendsDictionary.Clear();
            iuc.intersectionDictionary.Clear();
            iuc.complementDictionary.Clear();
            //暂时不需要注释
            //StringBuilder builder = new StringBuilder();
            //String[] strsText = scriptText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            //for (int i = 0; i < strsText.Count(); i++)
            //{
            //    if (strsText[i].Contains("//"))
            //    {
            //        int start = strsText[i].IndexOf("//");
            //        if (start != 0)
            //        {
            //            builder.Append(strsText[i].Substring(0, start));
            //        }
            //    }
            //    else
            //    {
            //        builder.Append(strsText[i]);
            //    }
            //}
            //String strAll = builder.ToString();
            String strAll = scriptText;
            //strAll = strAll.Replace(System.Environment.NewLine, "");
            //strAll = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(strAll, " ");
            int leng = strAll.Length;

            bool bStart = false;
            int iStart = 0;

            importPosition = -1;
            for (int i = 0; i < leng; i++)
            {
                if (strAll[i] == '{')
                {
                    importPosition = i;
                    break;
                }
            }
            if (importPosition == -1)
            {
                ShowError("没有给代码块取名字0");
                return null;
            }
            //不处理Import语句
            //String importContainsName = strAll.Substring(0, importPosition);
            //if (importContainsName.Contains("Import"))
            //{
            //    int lastImport = importContainsName.LastIndexOf(";");
            //    if(lastImport == -1)
            //        return null;
            //    importContainsName = importContainsName.Substring(0, lastImport);
            //    String[] impStrs = importContainsName.Split(';');
            //    foreach (string impStr in impStrs)
            //    {
            //        if (!ImportLightScript(impStr))
            //            return null;
            //        else
            //        {
            //            continue;
            //        }
            //    }
            //}
            Dictionary<String, String> _commandBlock = new Dictionary<String, String>();
            for (int i = 0; i < leng; i++)
            {
                if (strAll[i] == '{')
                {
                    bStart = true;
                    iStart = i;
                }
                if (strAll[i] == '}')
                {
                    if (bStart == true)
                    {
                        if (iStart == 0)
                        {
                            ShowError("没有给代码块取名字1");
                            return null;
                        }
                        for (int j = iStart - 1; j >= 0; j--)
                        {
                            if (strAll[j] == '}' || strAll[j] == ';')
                            {
                                bStart = false;
                                string commandBlockName = strAll.Substring(j + 1, iStart - j - 1);
                                commandBlockName = commandBlockName.Replace(System.Environment.NewLine, "");
                                //如果延伸于某个灯光块
                                if (commandBlockName.Contains(" extends "))
                                {
                                    string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" extends "));
                                    _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                    string commandBlockExtendsName = commandBlockName.Substring(commandBlockName.IndexOf(" extends ") + 8);
                                    commandBlockExtendsName = commandBlockExtendsName.Trim();
                                    if (iuc.extendsDictionary.ContainsKey(commandBlockExtendsName))
                                    {
                                        iuc.extendsDictionary[commandBlockExtendsName].Add(commandBlockRealName);
                                    }
                                    else
                                    {
                                        iuc.extendsDictionary.Add(commandBlockExtendsName, new List<string>() { commandBlockRealName });
                                    }
                                }
                                else if (commandBlockName.Contains(" intersection "))
                                {
                                    string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" intersection "));
                                    _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                    string commandBlockIntersectionName = commandBlockName.Substring(commandBlockName.IndexOf(" intersection ") + 13);
                                    commandBlockIntersectionName = commandBlockIntersectionName.Trim();
                                    if (iuc.intersectionDictionary.ContainsKey(commandBlockIntersectionName))
                                    {
                                        iuc.intersectionDictionary[commandBlockIntersectionName].Add(commandBlockRealName);
                                    }
                                    else
                                    {
                                        iuc.intersectionDictionary.Add(commandBlockIntersectionName, new List<string>() { commandBlockRealName });
                                    }
                                }
                                else if (commandBlockName.Contains(" complement "))
                                {
                                    string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" complement "));
                                    _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                    string commandBlockcomplementName = commandBlockName.Substring(commandBlockName.IndexOf(" complement ") + 11);
                                    commandBlockcomplementName = commandBlockcomplementName.Trim();
                                    if (iuc.complementDictionary.ContainsKey(commandBlockcomplementName))
                                    {
                                        iuc.complementDictionary[commandBlockcomplementName].Add(commandBlockRealName);
                                    }
                                    else
                                    {
                                        iuc.complementDictionary.Add(commandBlockcomplementName, new List<string>() { commandBlockRealName });
                                    }
                                }
                                else
                                {
                                    _commandBlock.Add(commandBlockName, strAll.Substring(iStart + 1, i - iStart - 1));
                                }

                                break;
                            }
                            if (j == 0)
                            {
                                if (!(_commandBlock.Count == 0))
                                {
                                    //没有给代码块取名字
                                    ShowError("没有给代码块取名字2");
                                    return null;
                                }
                                else
                                {
                                    bStart = false;
                                    string commandBlockName = strAll.Substring(j, iStart - j);
                                    commandBlockName = commandBlockName.Replace(System.Environment.NewLine, "");

                                    //如果延伸于某个灯光块
                                    if (commandBlockName.Contains(" extends "))
                                    {
                                        string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" extends "));
                                        _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                        string commandBlockExtendsName = commandBlockName.Substring(commandBlockName.IndexOf(" extends ") + 8);
                                        commandBlockExtendsName = commandBlockExtendsName.Trim();
                                        if (iuc.extendsDictionary.ContainsKey(commandBlockExtendsName))
                                        {
                                            iuc.extendsDictionary[commandBlockExtendsName].Add(commandBlockRealName);
                                        }
                                        else
                                        {
                                            iuc.extendsDictionary.Add(commandBlockExtendsName, new List<string>() { commandBlockRealName });
                                        }
                                    }
                                    else if (commandBlockName.Contains(" intersection "))
                                    {
                                        string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" intersection "));
                                        _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                        string commandBlockIntersectionName = commandBlockName.Substring(commandBlockName.IndexOf(" intersection ") + 13);
                                        commandBlockIntersectionName = commandBlockIntersectionName.Trim();
                                        if (iuc.intersectionDictionary.ContainsKey(commandBlockIntersectionName))
                                        {
                                            iuc.intersectionDictionary[commandBlockIntersectionName].Add(commandBlockRealName);
                                        }
                                        else
                                        {
                                            iuc.intersectionDictionary.Add(commandBlockIntersectionName, new List<string>() { commandBlockRealName });
                                        }
                                    }
                                    else if (commandBlockName.Contains(" complement "))
                                    {
                                        string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" complement "));
                                        _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                        string commandBlockcomplementName = commandBlockName.Substring(commandBlockName.IndexOf(" complement ") + 11);
                                        commandBlockcomplementName = commandBlockcomplementName.Trim();
                                        if (iuc.complementDictionary.ContainsKey(commandBlockcomplementName))
                                        {
                                            iuc.complementDictionary[commandBlockcomplementName].Add(commandBlockRealName);
                                        }
                                        else
                                        {
                                            iuc.complementDictionary.Add(commandBlockcomplementName, new List<string>() { commandBlockRealName });
                                        }
                                    }
                                    else
                                    {
                                        _commandBlock.Add(commandBlockName, strAll.Substring(iStart + 1, i - iStart - 1));
                                    }
                                    
                                }
                            }
                        }
                    }
                    else
                    {
                        //没找到左大括号
                        ShowError("没找到左大括号");
                        return null;
                    }
                }
            }
            Dictionary<String, String> newCommandBlock = new Dictionary<String, String>();
            foreach (var item in _commandBlock)
            {
                String mValue = item.Value;
                //去掉首尾空格
                mValue = mValue.Substring(2, mValue.Length - 4);
                newCommandBlock.Add(item.Key.Replace(System.Environment.NewLine, ""), mValue);
            }
            return newCommandBlock;
        }

        public Dictionary<String, String> GetCatalog(String scriptText,out Dictionary<String, List<String>> extendsDictionary, out Dictionary<String, List<String>> intersectionDictionary, out Dictionary<String, List<String>> complementDictionary)
        {
            extendsDictionary = new Dictionary<string, List<string>>();
            intersectionDictionary = new Dictionary<string, List<string>>();
            complementDictionary = new Dictionary<string, List<string>>();
            String strAll = scriptText;
            int leng = strAll.Length;

            bool bStart = false;
            int iStart = 0;

            importPosition = -1;
            for (int i = 0; i < leng; i++)
            {
                if (strAll[i] == '{')
                {
                    importPosition = i;
                    break;
                }
            }
            if (importPosition == -1)
            {
                ShowError("没有给代码块取名字0");
                return null;
            }
            //处理Import语句
            String importContainsName = strAll.Substring(0, importPosition);
            if (importContainsName.Contains("Import"))
            {
                int lastImport = importContainsName.LastIndexOf(";");
                importContainsName = importContainsName.Substring(0, lastImport);
                String[] impStrs = importContainsName.Split(';');
                foreach (string impStr in impStrs)
                {
                    if (!ImportLightScript(impStr)) {
                        return null;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            Dictionary<String, String> _commandBlock = new Dictionary<String, String>();
            for (int i = 0; i < leng; i++)
            {
                if (strAll[i] == '{')
                {
                    bStart = true;
                    iStart = i;
                }
                if (strAll[i] == '}')
                {
                    if (bStart == true)
                    {
                        if (iStart == 0)
                        {
                            ShowError("没有给代码块取名字1");
                            return null;
                        }
                        for (int j = iStart - 1; j >= 0; j--)
                        {
                            if (strAll[j] == '}' || strAll[j] == ';')
                            {
                                bStart = false;
                                string commandBlockName = strAll.Substring(j + 1, iStart - j - 1);
                                commandBlockName = commandBlockName.Replace(System.Environment.NewLine, "");
                                //如果延伸于某个灯光块
                                if (commandBlockName.Contains(" extends "))
                                {
                                    string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" extends "));
                                    _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                    string commandBlockExtendsName = commandBlockName.Substring(commandBlockName.IndexOf(" extends ") + 8);
                                    commandBlockExtendsName = commandBlockExtendsName.Trim();
                                    if (extendsDictionary.ContainsKey(commandBlockExtendsName))
                                    {
                                        extendsDictionary[commandBlockExtendsName].Add(commandBlockRealName);
                                    }
                                    else
                                    {
                                        extendsDictionary.Add(commandBlockExtendsName, new List<string>() { commandBlockRealName });
                                    }
                                }
                                else if (commandBlockName.Contains(" intersection "))
                                {
                                    string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" intersection "));
                                    _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                    string commandBlockIntersectionName = commandBlockName.Substring(commandBlockName.IndexOf(" intersection ") + 13);
                                    commandBlockIntersectionName = commandBlockIntersectionName.Trim();
                                    if (intersectionDictionary.ContainsKey(commandBlockIntersectionName))
                                    {
                                        intersectionDictionary[commandBlockIntersectionName].Add(commandBlockRealName);
                                    }
                                    else
                                    {
                                        intersectionDictionary.Add(commandBlockIntersectionName, new List<string>() { commandBlockRealName });
                                    }
                                }
                                else if (commandBlockName.Contains(" complement "))
                                {
                                    string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" complement "));
                                    _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                    string commandBlockcomplementName = commandBlockName.Substring(commandBlockName.IndexOf(" complement ") + 11);
                                    commandBlockcomplementName = commandBlockcomplementName.Trim();
                                    if (complementDictionary.ContainsKey(commandBlockcomplementName))
                                    {
                                        complementDictionary[commandBlockcomplementName].Add(commandBlockRealName);
                                    }
                                    else
                                    {
                                        complementDictionary.Add(commandBlockcomplementName, new List<string>() { commandBlockRealName });
                                    }
                                }
                                else
                                {
                                    _commandBlock.Add(commandBlockName, strAll.Substring(iStart + 1, i - iStart - 1));
                                }

                                break;
                            }
                            if (j == 0)
                            {
                                if (!(_commandBlock.Count == 0))
                                {
                                    //没有给代码块取名字
                                    ShowError("没有给代码块取名字2");
                                    return null;
                                }
                                else
                                {
                                    bStart = false;
                                    string commandBlockName = strAll.Substring(j, iStart - j);
                                    commandBlockName = commandBlockName.Replace(System.Environment.NewLine, "");

                                    //如果延伸于某个灯光块
                                    if (commandBlockName.Contains(" extends "))
                                    {
                                        string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" extends "));
                                        _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                        string commandBlockExtendsName = commandBlockName.Substring(commandBlockName.IndexOf(" extends ") + 8);
                                        commandBlockExtendsName = commandBlockExtendsName.Trim();
                                        if (extendsDictionary.ContainsKey(commandBlockExtendsName))
                                        {
                                            extendsDictionary[commandBlockExtendsName].Add(commandBlockRealName);
                                        }
                                        else
                                        {
                                            extendsDictionary.Add(commandBlockExtendsName, new List<string>() { commandBlockRealName });
                                        }
                                    }
                                    else if (commandBlockName.Contains(" intersection "))
                                    {
                                        string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" intersection "));
                                        _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                        string commandBlockIntersectionName = commandBlockName.Substring(commandBlockName.IndexOf(" intersection ") + 13);
                                        commandBlockIntersectionName = commandBlockIntersectionName.Trim();
                                        if (intersectionDictionary.ContainsKey(commandBlockIntersectionName))
                                        {
                                            intersectionDictionary[commandBlockIntersectionName].Add(commandBlockRealName);
                                        }
                                        else
                                        {
                                            intersectionDictionary.Add(commandBlockIntersectionName, new List<string>() { commandBlockRealName });
                                        }
                                    }
                                    else if (commandBlockName.Contains(" complement "))
                                    {
                                        string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" complement "));
                                        _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                        string commandBlockcomplementName = commandBlockName.Substring(commandBlockName.IndexOf(" complement ") + 11);
                                        commandBlockcomplementName = commandBlockcomplementName.Trim();
                                        if (complementDictionary.ContainsKey(commandBlockcomplementName))
                                        {
                                            complementDictionary[commandBlockcomplementName].Add(commandBlockRealName);
                                        }
                                        else
                                        {
                                            complementDictionary.Add(commandBlockcomplementName, new List<string>() { commandBlockRealName });
                                        }
                                    }
                                    else
                                    {
                                        _commandBlock.Add(commandBlockName, strAll.Substring(iStart + 1, i - iStart - 1));
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        //没找到左大括号
                        ShowError("没找到左大括号");
                        return null;
                    }
                }
            }
            Dictionary<String, String> newCommandBlock = new Dictionary<String, String>();
            foreach (var item in _commandBlock)
            {
                String mValue = item.Value;
                //去掉首尾空格
                mValue = mValue.Substring(2, mValue.Length - 4);
                newCommandBlock.Add(item.Key.Replace(System.Environment.NewLine, ""), mValue);
            }

            return newCommandBlock;
        }

        public List<Light> ScriptToLightGroup(String scriptText, String lightGroupName)
        {
            try
            {
                //_extendsDictionary.Clear();
                Dictionary<String, List<Light>> _lightGroupDictionaryAll = new Dictionary<String, List<Light>>();

                Dictionary<String, List<int>> _rangeGroupDictionary = new Dictionary<String, List<int>>();
                Dictionary<String, List<int>> _colorGroupDictionary = new Dictionary<String, List<int>>();
                Dictionary<String, List<String>> _extendsDictionary = new Dictionary<String, List<String>>();//延伸关系
                Dictionary<String, List<String>> _intersectionDictionary = new Dictionary<String, List<String>>();//交集关系
                Dictionary<String, List<String>> _complementDictionary = new Dictionary<String, List<String>>();//补集关系


                StringBuilder builder = new StringBuilder();
                String[] strsText = scriptText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                for (int i = 0; i < strsText.Count(); i++)
                {
                    if (strsText[i].Contains("//"))
                    {
                        int start = strsText[i].IndexOf("//");
                        if (start != 0)
                        {
                            builder.Append(strsText[i].Substring(0, start));
                        }
                    }
                    else
                    {
                        builder.Append(strsText[i]);
                    }
                }
                String strAll = builder.ToString();
                strAll = strAll.Replace(System.Environment.NewLine, "");
                strAll = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(strAll, " ");
                int leng = strAll.Length;

                bool bStart = false;
                int iStart = 0;

                importPosition = -1;
                for (int i = 0; i < leng; i++)
                {
                    if (strAll[i] == '{')
                    {
                        importPosition = i;
                        break;
                    }
                }
                if (importPosition == -1)
                {
                    ShowError("没有给代码块取名字0");
                    return null;
                }
                //处理Import语句
                String importContainsName = strAll.Substring(0, importPosition);
                if (importContainsName.Contains("Import"))
                {
                    int lastImport = importContainsName.LastIndexOf(";");
                    importContainsName = importContainsName.Substring(0, lastImport);
                    String[] impStrs = importContainsName.Split(';');
                    foreach (string impStr in impStrs)
                    {
                        if (!ImportLightScript(impStr))
                            return null;
                        else
                        {
                            continue;
                        }
                    }
                }
                Dictionary<String, String> _commandBlock = new Dictionary<String, String>();
                for (int i = 0; i < leng; i++)
                {
                    if (strAll[i] == '{')
                    {
                        bStart = true;
                        iStart = i;
                    }
                    if (strAll[i] == '}')
                    {
                        if (bStart == true)
                        {
                            if (iStart == 0)
                            {
                                ShowError("没有给代码块取名字1");
                                return null;
                            }
                            for (int j = iStart - 1; j >= 0; j--)
                            {
                                if (strAll[j] == '}' || strAll[j] == ';')
                                {
                                    bStart = false;
                                    string commandBlockName = strAll.Substring(j + 1, iStart - j - 1);
                                    //如果延伸于某个灯光块
                                    if (commandBlockName.Contains(" extends "))
                                    {
                                        string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" extends "));
                                        _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                        string commandBlockExtendsName = commandBlockName.Substring(commandBlockName.IndexOf(" extends ") + 8);
                                        commandBlockExtendsName = commandBlockExtendsName.Trim();
                                        if (_extendsDictionary.ContainsKey(commandBlockExtendsName))
                                        {
                                            _extendsDictionary[commandBlockExtendsName].Add(commandBlockRealName);
                                        }
                                        else
                                        {
                                            _extendsDictionary.Add(commandBlockExtendsName, new List<string>() { commandBlockRealName });
                                        }
                                    }
                                    else if (commandBlockName.Contains(" intersection "))
                                    {
                                        string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" intersection "));
                                        _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                        string commandBlockIntersectioName = commandBlockName.Substring(commandBlockName.IndexOf(" intersection ") + 13);
                                        commandBlockIntersectioName = commandBlockIntersectioName.Trim();
                                        if (_intersectionDictionary.ContainsKey(commandBlockIntersectioName))
                                        {
                                            _intersectionDictionary[commandBlockIntersectioName].Add(commandBlockRealName);
                                        }
                                        else
                                        {
                                            _intersectionDictionary.Add(commandBlockIntersectioName, new List<string>() { commandBlockRealName });
                                        }
                                    }
                                    else if (commandBlockName.Contains(" complement "))
                                    {
                                        string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" complement "));
                                        _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                        string commandBlockcomplementame = commandBlockName.Substring(commandBlockName.IndexOf(" complement ") + 11);
                                        commandBlockcomplementame = commandBlockcomplementame.Trim();
                                        if (_complementDictionary.ContainsKey(commandBlockcomplementame))
                                        {
                                            _complementDictionary[commandBlockcomplementame].Add(commandBlockRealName);
                                        }
                                        else
                                        {
                                            _complementDictionary.Add(commandBlockcomplementame, new List<string>() { commandBlockRealName });
                                        }
                                    }
                                    else
                                    {
                                        _commandBlock.Add(commandBlockName, strAll.Substring(iStart + 1, i - iStart - 1));
                                    }

                                    break;
                                }
                                if (j == 0)
                                {
                                    if (!(_commandBlock.Count == 0))
                                    {
                                        //没有给代码块取名字
                                        ShowError("没有给代码块取名字2");
                                        return null;
                                    }
                                    else
                                    {
                                        bStart = false;
                                        string commandBlockName = strAll.Substring(j, iStart - j);
                                        //如果延伸于某个灯光块
                                        if (commandBlockName.Contains(" extends "))
                                        {
                                            string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" extends "));
                                            _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                            string commandBlockExtendsName = commandBlockName.Substring(commandBlockName.IndexOf(" extends ") + 8);
                                            commandBlockExtendsName = commandBlockExtendsName.Trim();
                                            if (_extendsDictionary.ContainsKey(commandBlockExtendsName))
                                            {
                                                _extendsDictionary[commandBlockExtendsName].Add(commandBlockRealName);
                                            }
                                            else
                                            {
                                                _extendsDictionary.Add(commandBlockExtendsName, new List<string>() { commandBlockRealName });
                                            }
                                        }
                                        else if (commandBlockName.Contains(" intersection "))
                                        {
                                            string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" intersection "));
                                            _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                            string commandBlockIntersectioName = commandBlockName.Substring(commandBlockName.IndexOf(" intersection ") + 13);
                                            commandBlockIntersectioName = commandBlockIntersectioName.Trim();
                                            if (_intersectionDictionary.ContainsKey(commandBlockIntersectioName))
                                            {
                                                _intersectionDictionary[commandBlockIntersectioName].Add(commandBlockRealName);
                                            }
                                            else
                                            {
                                                _intersectionDictionary.Add(commandBlockIntersectioName, new List<string>() { commandBlockRealName });
                                            }
                                        }
                                        else if (commandBlockName.Contains(" complement "))
                                        {
                                            string commandBlockRealName = commandBlockName.Substring(0, commandBlockName.IndexOf(" complement "));
                                            _commandBlock.Add(commandBlockRealName.Trim(), strAll.Substring(iStart + 1, i - iStart - 1));
                                            string commandBlockcomplementame = commandBlockName.Substring(commandBlockName.IndexOf(" complement ") + 11);
                                            commandBlockcomplementame = commandBlockcomplementame.Trim();
                                            if (_complementDictionary.ContainsKey(commandBlockcomplementame))
                                            {
                                                _complementDictionary[commandBlockcomplementame].Add(commandBlockRealName);
                                            }
                                            else
                                            {
                                                _complementDictionary.Add(commandBlockcomplementame, new List<string>() { commandBlockRealName });
                                            }
                                        }
                                        else
                                        {
                                            _commandBlock.Add(commandBlockName, strAll.Substring(iStart + 1, i - iStart - 1));
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //没找到左大括号
                            ShowError("没找到左大括号");
                            return null;
                        }
                    }
                }
                foreach (KeyValuePair<String, String> kvp in _commandBlock)
                {
                    if (!lockedDictionary.ContainsKey(kvp.Key))
                    {
                        Dictionary<String, Light> _lightDictionary = new Dictionary<String, Light>();
                        Dictionary<String, List<Light>> _lightGroupDictionary = new Dictionary<String, List<Light>>();
                        Dictionary<String, ContentType> _localContent = new Dictionary<String, ContentType>();//局部内容
                                                                                                              //_globalContent.Add(kvp.Key, ContentType.LightGroup);
                                                                                                              //_localContent.Clear();//本地变量每次循环清空
                        _rangeGroupDictionary.Clear();
                        _colorGroupDictionary.Clear();
                        _lightGroupDictionary.Add(kvp.Key, new List<Light>());
                        //Console.WriteLine("{0}-{1}", kvp.Key, kvp.Value);
                        //try
                        //{
                        String[] strs = kvp.Value.Split(';');
                        for (int i = 0; i < strs.Length - 1; i++)
                        {
                            string commandLine = strs[i].Trim();
                            //根据运算符变为改造语句
                            //思路为.运算先于 = 所以先将.运算的结果计算出算，用GUID存储起来，而点运算的语句则变为GUID 
                            //如：A = B.C(); => A = GUID;  
                            //LightGroup = Edit.HorizontalFlipping().StartTime(); => LightGroup = Guid1.Start(); => LightGroup = Guid2.Start();
                            while (commandLine.Contains("."))
                            {
                                string commandLineLeft = string.Empty;
                                string commandLineRight = string.Empty;
                                string[] commandLineTwo = commandLine.Split('=');
                                if (commandLine.Contains("="))
                                {
                                    if (commandLineTwo.Count() == 2)
                                    {
                                        commandLineLeft = commandLineTwo[0] + "=";
                                        commandLineRight = commandLineTwo[1];
                                    }
                                    else
                                    {
                                        ShowError("格式不正确：" + commandLine);
                                        return null;
                                    }
                                }
                                else
                                {
                                    commandLineRight = commandLine;
                                }

                                //获得点的位置
                                int pointPosition = commandLineRight.IndexOf('.');
                                string startStr = string.Empty;
                                string endStr = string.Empty;
                                //string commandLineRightStart = string.Empty;
                                string commandLineRightEnd = string.Empty;
                                for (int p = pointPosition - 1; p >= 0; p--)
                                {
                                    if (commandLineRight[p] == '.' || commandLineRight[p] == '=')
                                    {
                                        startStr = commandLineRight.Substring(p + 1, pointPosition - p - 1);
                                        //commandLineRightStart = commandLineRight.Substring(0, p+1);
                                        break;
                                    }
                                    if (p == 0)
                                    {
                                        startStr = commandLineRight.Substring(p, pointPosition - p);
                                        //commandLineRightStart = commandLineRight.Substring(0, pointPosition);
                                        break;
                                    }
                                }

                                if (pointPosition + 1 >= commandLineRight.Length)
                                {
                                    ShowError("语句错误：" + commandLineRight);
                                    return null;
                                }
                                for (int p = pointPosition + 1; p < commandLineRight.Length; p++)
                                {
                                    if (commandLineRight[p] == '.')
                                    {
                                        endStr = commandLineRight.Substring(pointPosition + 1, p - pointPosition);
                                        commandLineRightEnd = commandLineRight.Substring(p, commandLineRight.Length - p);
                                        break;
                                    }
                                    else if (p == commandLineRight.Length - 1)
                                    {
                                        endStr = commandLineRight.Substring(pointPosition + 1, p - pointPosition);
                                        commandLineRightEnd = commandLineRight.Substring(p, commandLineRight.Length - p - 1);
                                        break;
                                    }
                                }
                                startStr = startStr.Trim();
                                endStr = endStr.Trim();
                                if (startStr.Equals("Create"))
                                {
                                    if (endStr.StartsWith("CreateLightGroup"))
                                    {
                                        // = Create.CreateLightGroup(时间,范围,间隔,持续时间,颜色);
                                        // = Create.CreateLightGroup(1, 2, 3, 4, 5);
                                        List<Light> _lightGroup = CreateMethod.CreateMain(commandLineRight, _lightGroupDictionary, _rangeGroupDictionary, _colorGroupDictionary, lastFilePath);
                                        if (_lightGroup != null)
                                        {
                                            //改写语句
                                            string uuid = Guid.NewGuid().ToString();
                                            _lightGroupDictionary.Add(uuid, _lightGroup);
                                            _localContent.Add(uuid, ContentType.LightGroup);
                                            commandLine = commandLineLeft + " " + uuid + " " + commandLineRightEnd;
                                            continue;
                                        }
                                        else
                                        {
                                            ShowError("Create_CreateLightGroup创建灯光组失败：输入的参数不对");
                                            return null;
                                        }
                                    }
                                    else if (endStr.StartsWith("FromMidiFile"))
                                    {
                                        // = Create.FromMidiFile("Midi文件路径");
                                        List<Light> _lightGroup = CreateMethod.CreateMain(commandLineRight, _lightGroupDictionary, _rangeGroupDictionary, _colorGroupDictionary, lastFilePath);
                                        if (_lightGroup != null)
                                        {
                                            //改写语句
                                            string uuid = Guid.NewGuid().ToString();
                                            _lightGroupDictionary.Add(uuid, _lightGroup);
                                            _localContent.Add(uuid, ContentType.LightGroup);
                                            commandLine = commandLineLeft + " " + uuid + " " + commandLineRightEnd;

                                            continue;
                                        }
                                        else
                                        {
                                            ShowError("Create_FromMidiFile创建灯光组失败：文件路径格式不对或文件不存在");
                                            return null;
                                        }
                                    }
                                    else if (endStr.StartsWith("FromLightFile"))
                                    {
                                        // = Create.FromLightFile("Light文件路径");
                                        List<Light> _lightGroup = CreateMethod.CreateMain(commandLineRight, _lightGroupDictionary, _rangeGroupDictionary, _colorGroupDictionary, lastFilePath);
                                        if (_lightGroup != null)
                                        {
                                            //改写语句
                                            string uuid = Guid.NewGuid().ToString();
                                            _lightGroupDictionary.Add(uuid, _lightGroup);
                                            _localContent.Add(uuid, ContentType.LightGroup);
                                            commandLine = commandLineLeft + " " + uuid + " " + commandLineRightEnd;

                                            continue;
                                        }
                                        else
                                        {
                                            ShowError("Create_FromLightFile创建灯光组失败：文件路径格式不对或文件不存在");
                                            return null;
                                        }
                                    }
                                    else if (endStr.StartsWith("Automatic"))
                                    {
                                        // = Create.Automatic(方法,参数);
                                        List<Light> _lightGroup = CreateMethod.CreateMain(commandLineRight, _lightGroupDictionary, _rangeGroupDictionary, _colorGroupDictionary, lastFilePath);
                                        if (_lightGroup != null)
                                        {
                                            //改写语句
                                            string uuid = Guid.NewGuid().ToString();
                                            _lightGroupDictionary.Add(uuid, _lightGroup);
                                            _localContent.Add(uuid, ContentType.LightGroup);
                                            commandLine = commandLineLeft + " " + uuid + " " + commandLineRightEnd;
                                            continue;
                                        }
                                        else
                                        {
                                            ShowError("Create_Automatic创建灯光组失败：文件路径格式不对或文件不存在");
                                            return null;
                                        }
                                    }
                                    else if (endStr.StartsWith("Animation"))
                                    {
                                        // = Create.Automatic(方法,参数);
                                        List<Light> _lightGroup = CreateMethod.CreateMain(commandLineRight, _lightGroupDictionary, _rangeGroupDictionary, _colorGroupDictionary, lastFilePath);
                                        if (_lightGroup != null)
                                        {
                                            //改写语句
                                            string uuid = Guid.NewGuid().ToString();
                                            _lightGroupDictionary.Add(uuid, _lightGroup);
                                            _localContent.Add(uuid, ContentType.LightGroup);
                                            commandLine = commandLineLeft + " " + uuid + " " + commandLineRightEnd;

                                            continue;
                                        }
                                        else
                                        {
                                            ShowError("Create_Animation创建灯光组失败");
                                            return null;
                                        }
                                    }
                                    else if (endStr.StartsWith("ChangeIntoMotion"))
                                    {
                                        // = Create.Automatic(方法,参数);
                                        List<Light> _lightGroup = CreateMethod.CreateMain(commandLineRight, _lightGroupDictionary, _rangeGroupDictionary, _colorGroupDictionary, lastFilePath);
                                        if (_lightGroup != null)
                                        {
                                            //改写语句
                                            string uuid = Guid.NewGuid().ToString();
                                            _lightGroupDictionary.Add(uuid, _lightGroup);
                                            _localContent.Add(uuid, ContentType.LightGroup);
                                            commandLine = commandLineLeft + " " + uuid + " " + commandLineRightEnd;

                                            continue;
                                        }
                                        else
                                        {
                                            ShowError("Create_ChangeIntoMotion创建灯光组失败");
                                            return null;
                                        }
                                    }
                                    else if (endStr.StartsWith("Intersection"))
                                    {
                                        // = Create.FromLightFile("Light文件路径");
                                        List<Light> _lightGroup = CreateMethod.CreateMain(commandLineRight, _lightGroupDictionary, _rangeGroupDictionary, _colorGroupDictionary, lastFilePath);
                                        if (_lightGroup != null)
                                        {
                                            //改写语句
                                            string uuid = Guid.NewGuid().ToString();
                                            _lightGroupDictionary.Add(uuid, _lightGroup);
                                            _localContent.Add(uuid, ContentType.LightGroup);
                                            commandLine = commandLineLeft + " " + uuid + " " + commandLineRightEnd;

                                            continue;
                                        }
                                        else
                                        {
                                            ShowError("Create_Intersection创建灯光组失败");
                                            return null;
                                        }
                                    }
                                    else if (endStr.StartsWith("Complement"))
                                    {
                                        // = Create.FromLightFile("Light文件路径");
                                        List<Light> _lightGroup = CreateMethod.CreateMain(commandLineRight, _lightGroupDictionary, _rangeGroupDictionary, _colorGroupDictionary, lastFilePath);
                                        if (_lightGroup != null)
                                        {
                                            //改写语句
                                            string uuid = Guid.NewGuid().ToString();
                                            _lightGroupDictionary.Add(uuid, _lightGroup);
                                            _localContent.Add(uuid, ContentType.LightGroup);
                                            commandLine = commandLineLeft + " " + uuid + " " + commandLineRightEnd;

                                            continue;
                                        }
                                        else
                                        {
                                            ShowError("Create_Complement创建灯光组失败");
                                            return null;
                                        }
                                    }
                                    else
                                    {
                                        return new List<Light>();
                                    }
                                }
                                else if (startStr.Equals("Edit"))
                                {
                                    String editLightGroupName;
                                    if (endStr.Contains(','))
                                    {
                                        editLightGroupName = endStr.Substring(endStr.LastIndexOf('(') + 1, endStr.IndexOf(',') - endStr.LastIndexOf('(') - 1);
                                    }
                                    else
                                    {
                                        editLightGroupName = endStr.Substring(endStr.LastIndexOf('(') + 1, endStr.LastIndexOf(')') - endStr.LastIndexOf('(') - 1);
                                    }
                                    if (_lightGroupDictionary.ContainsKey(editLightGroupName))
                                    {
                                        List<Light> _lightGroup = EditLightGroup(endStr, _lightGroupDictionary[editLightGroupName], _rangeGroupDictionary, _colorGroupDictionary);
                                        if (_lightGroup != null)
                                        {
                                            //改写语句
                                            string uuid = Guid.NewGuid().ToString();
                                            _lightGroupDictionary.Add(uuid, _lightGroup);
                                            _localContent.Add(uuid, ContentType.LightGroup);
                                            commandLine = commandLineLeft + " " + uuid + " " + commandLineRightEnd;
                                            continue;
                                        }
                                        else
                                        {
                                            ShowError("Edit修改灯光组失败：输入的参数不对");
                                            return null;
                                        }
                                    }
                                    else
                                    {
                                        ShowError("需要改变的灯光组" + editLightGroupName + "不存在");
                                        return null;
                                    }
                                }
                                else if (_lightGroupDictionary.ContainsKey(startStr))
                                {
                                    //.前面是灯光组
                                    if (endStr.StartsWith("Add"))
                                    {
                                        //testLightGroup.Add(testLight);
                                        //被插入的对象
                                        String addObjectName = endStr.Substring(endStr.LastIndexOf('(') + 1, endStr.LastIndexOf(')') - endStr.LastIndexOf('(') - 1);
                                        if (_lightGroupDictionary.ContainsKey(addObjectName) || _lightDictionary.ContainsKey(addObjectName))
                                        {
                                            //testLightGroup.Add(testLight);
                                            if (InsertLightOrGroup(commandLineRight, _lightDictionary, _lightGroupDictionary))
                                            {
                                                commandLine = commandLineLeft + " " + startStr + " " + commandLineRightEnd;
                                                continue;
                                            }
                                            else
                                            {
                                                return null;
                                            }
                                        }
                                        else
                                        {
                                            ShowError(addObjectName + "不存在");
                                            return null;
                                        }

                                    }
                                    else if (endStr.StartsWith("SetStartTime("))
                                    {
                                        //testLightGroup.SetStartTime(30);
                                        //开始时间
                                        //String startTime = endStr.Substring(endStr.LastIndexOf('(') + 1, endStr.LastIndexOf(')') - endStr.LastIndexOf('(') - 1);
                                        //_lightGroupDictionary[startStr] = LightGroupMethod.SetStartTime(_lightGroupDictionary[startStr], Convert.ToInt32(startTime));
                                        //commandLine = commandLineLeft + " " + startStr + " " + commandLineRightEnd;
                                        //continue;
                                    }
                                    else if (endStr.StartsWith("SetAllTime("))
                                    {
                                        //testLightGroup.SetAllTime(30);
                                        //全部时间
                                        //String allTime = endStr.Substring(endStr.LastIndexOf('(') + 1, endStr.LastIndexOf(')') - endStr.LastIndexOf('(') - 1);
                                        //_lightGroupDictionary[startStr] = LightGroupMethod.SetAllTime(_lightGroupDictionary[startStr], Convert.ToInt32(allTime));
                                        //commandLine = commandLineLeft + " " + startStr + " " + commandLineRightEnd;
                                        //continue;
                                    }
                                    else if (endStr.StartsWith("GetColor("))
                                    {
                                        //testLightGroup.GetColor();
                                        //改写语句
                                        string uuid = Guid.NewGuid().ToString();
                                        _colorGroupDictionary.Add(uuid, LightGroupMethod.GetColor(_lightGroupDictionary[startStr]));
                                        _localContent.Add(uuid, ContentType.LightGroup);
                                        commandLine = commandLineLeft + " " + uuid + " " + commandLineRightEnd;
                                        continue;
                                    }
                                    else if (endStr.StartsWith("SetAttribute("))
                                    {
                                        //testLightGroup.SetAttribute(Time,+5);
                                        String allContent = endStr.Substring(endStr.LastIndexOf('(') + 1, endStr.LastIndexOf(')') - endStr.LastIndexOf('(') - 1);
                                        String[] strsContent = allContent.Split(',');
                                        if (strsContent.Count() != 2)
                                        {
                                            return new List<Light>();
                                        }
                                        int mOperator = 0;
                                        int value = 0;
                                        if (strsContent[1].StartsWith("+"))
                                        {
                                            mOperator = 1;
                                            value = Convert.ToInt32(strsContent[1].Substring(1));
                                        }
                                        else if (strsContent[1].StartsWith("-"))
                                        {
                                            mOperator = -1;
                                            value = Convert.ToInt32(strsContent[1].Substring(1));
                                        }
                                        else
                                        {
                                            mOperator = 0;
                                            value = Convert.ToInt32(strsContent[1]);
                                        }
                                        _lightGroupDictionary[startStr] = LightGroupMethod.SetAttribute(_lightGroupDictionary[startStr], strsContent[0], mOperator, value);
                                        commandLine = commandLineLeft + " " + startStr + " " + commandLineRightEnd;
                                        continue;
                                    }
                                }
                                else if (_rangeGroupDictionary.ContainsKey(startStr))
                                {
                                    //testLightGroup.Add(testLight);
                                    //.前面是范围组
                                    if (endStr.StartsWith("Add("))
                                    {
                                        //被插入的对象
                                        String addObjectName = endStr.Substring(endStr.LastIndexOf('(') + 1, endStr.LastIndexOf(')') - endStr.LastIndexOf('(') - 1);
                                        if (!InsertRange(commandLineRight, _rangeGroupDictionary))
                                            return null;
                                        else
                                        {
                                            commandLine = commandLineLeft + " " + startStr + " " + commandLineRightEnd;
                                            continue;
                                        }

                                    }
                                }
                                else if (_colorGroupDictionary.ContainsKey(startStr))
                                {
                                    //.前面是颜色组
                                    if (endStr.StartsWith("Add("))
                                    {
                                        //testColorGroup.Add(5);
                                        //被插入的对象
                                        String addObjectName = endStr.Substring(endStr.LastIndexOf('(') + 1, endStr.LastIndexOf(')') - endStr.LastIndexOf('(') - 1);
                                        if (!InsertColor(commandLineRight, _colorGroupDictionary))
                                            return null;
                                        else
                                        {
                                            commandLine = commandLineLeft + " " + startStr + " " + commandLineRightEnd;
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            //如果语句中含有等号
                            if (commandLine.Contains("="))
                            {
                                string[] commandLineEquals = commandLine.Split('=');
                                if (commandLineEquals.Count() != 2)
                                {
                                    ShowError(commandLine + "语句不正确");
                                    return null;
                                }
                                commandLineEquals[1] = commandLineEquals[1].Trim();
                                string _leftCommand = commandLineEquals[0].Trim();
                                ContentType _leftType = ContentType.Null;
                                //Light testLight = new Light();或者Light testLight = new Light(0,o,36,5);
                                Regex P_newLight = new Regex(@"^Light\s+[0-9a-zA-Z_\u4e00-\u9fa5]{1,}\s*");
                                if (P_newLight.IsMatch(_leftCommand))
                                {
                                    _leftType = ContentType.Light;
                                }
                                //LightGroup testLightGroup = new LightGroup();
                                Regex P_newLightGroup = new Regex(@"^LightGroup\s+[0-9a-zA-Z_\u4e00-\u9fa5]{1,}\s*");
                                if (P_newLightGroup.IsMatch(_leftCommand))
                                {
                                    _leftType = ContentType.LightGroup;
                                }

                                //RangeGroup testRangeGroup = new RangeGroup();
                                Regex P_newRangeGroup = new Regex(@"^RangeGroup\s+[0-9a-zA-Z_\u4e00-\u9fa5]{1,}\s*");
                                if (P_newRangeGroup.IsMatch(_leftCommand))
                                {
                                    _leftType = ContentType.RangeGroup;
                                }
                                //ColorGroup testColorGroup = new ColorGroup();
                                Regex P_newcolorGroup = new Regex(@"^ColorGroup\s+[0-9a-zA-Z_\u4e00-\u9fa5]{1,}\s*");
                                if (P_newcolorGroup.IsMatch(_leftCommand))
                                {
                                    _leftType = ContentType.ColorGroup;
                                }
                                //A = B; A可以是Light也可以是LightGroup
                                if (_lightDictionary.ContainsKey(_leftCommand.Trim()))
                                {
                                    _leftType = ContentType.Light;
                                }
                                if (_lightGroupDictionary.ContainsKey(_leftCommand.Trim()))
                                {
                                    _leftType = ContentType.LightGroup;
                                }
                                //创建灯光语句
                                if (_leftType == ContentType.Light)
                                {
                                    MatchCollection matchLightName = Regex.Matches(_leftCommand, @"[0-9a-zA-Z_\u4e00-\u9fa5]{1,}\s*");
                                    //得到灯光名称
                                    String strLightName = matchLightName[matchLightName.Count - 1].Value.Substring(0, matchLightName[matchLightName.Count - 1].Value.Length).Trim();
                                    //如果包含就返回空
                                    //if (ContainLight(strLightName, _lightDictionary, _localContent)) {
                                    //    return new List<Light>();
                                    //}
                                    //根据command右边创建灯光
                                    Light light = _BottomCreateLight(commandLineEquals[1]);
                                    if (light != null)
                                    {
                                        _lightDictionary.Add(strLightName, light);
                                        _localContent.Add(strLightName, ContentType.Light);
                                    }
                                    else
                                    {
                                        return new List<Light>();
                                    }
                                }
                                //创建灯光组语句
                                if (_leftType == ContentType.LightGroup)
                                {
                                    MatchCollection matchLightGroupName = Regex.Matches(_leftCommand, @"[0-9a-zA-Z_\u4e00-\u9fa5\u4e00-\u9fa5]{1,}\s*");
                                    //得到灯光组名称
                                    String strLightGroupName = matchLightGroupName[matchLightGroupName.Count - 1].Value.Substring(0, matchLightGroupName[matchLightGroupName.Count - 1].Value.Length).Trim();
                                    //如果包含就返回空
                                    //if (ContainLightGroup(strLightGroupName, _lightGroupDictionary, _localContent))
                                    //{
                                    //    return new List<Light>();
                                    //}
                                    //根据command右边创建灯光组
                                    List<Light> lightGroup = null;

                                    if (_lightGroupDictionary.ContainsKey(commandLineEquals[1].Trim()))
                                    {
                                        lightGroup = LightBusiness.Copy(_lightGroupDictionary[commandLineEquals[1].Trim()]);
                                    }
                                    else if (commandLineEquals[1].Trim().Equals("Parent"))
                                    {
                                        bool isMatch = false;

                                        foreach (var item in _extendsDictionary)
                                        {
                                            if (item.Value.Contains(kvp.Key) && _lightGroupDictionaryAll.ContainsKey(item.Key))
                                            {
                                                lightGroup = LightBusiness.Copy(_lightGroupDictionaryAll[item.Key]);
                                                isMatch = true;
                                                break;
                                            }
                                        }
                                        if (!isMatch)
                                        {
                                            ShowError("被继承的父类不存在");
                                            return null;
                                        }
                                    }
                                    else
                                    {
                                        lightGroup = _BottomCreateLightGroup(commandLineEquals[1], _lightGroupDictionaryAll);
                                    }
                                    if (lightGroup != null)
                                    {
                                        if (_lightGroupDictionary.ContainsKey(strLightGroupName))
                                        {
                                            _lightGroupDictionary[strLightGroupName] = lightGroup;
                                        }
                                        else
                                        {
                                            _lightGroupDictionary.Add(strLightGroupName, lightGroup);
                                            _localContent.Add(strLightGroupName, ContentType.LightGroup);
                                        }
                                    }
                                    else
                                    {
                                        return new List<Light>();
                                    }
                                }
                                if (_leftType == ContentType.RangeGroup)
                                {
                                    //创建范围组语句
                                    if (P_newRangeGroup.IsMatch(commandLine))
                                    {
                                        if (!CreateRangeGroup(commandLine, _rangeGroupDictionary, _localContent))
                                            return null;
                                    }
                                }
                                if (_leftType == ContentType.ColorGroup)
                                {
                                    //创建颜色组语句
                                    if (!CreateColorGroup(commandLine, _colorGroupDictionary, _localContent))
                                        return null;
                                    else
                                    {
                                        continue;
                                    }
                                }
                                //如果包含点
                                if (_leftCommand.Contains('.'))
                                {
                                    string[] leftCommandTwo = _leftCommand.Split('.');
                                    //是灯光
                                    if (_lightDictionary.ContainsKey(leftCommandTwo[0].Trim()))
                                    {
                                        if (leftCommandTwo[1].Equals("Time"))
                                        {
                                            //testLight.Time = 0;
                                            _lightDictionary[leftCommandTwo[0]].Time = Convert.ToInt32(commandLineEquals[1]);
                                        }
                                        else if (leftCommandTwo[1].Equals("Action"))
                                        {
                                            //testLight.Action = 0;
                                            if (commandLineEquals[1][0] == 'o' && commandLineEquals[1].Length == 1)
                                            {
                                                _lightDictionary[leftCommandTwo[0]].Action = 144;
                                            }
                                            else if (commandLineEquals[1][0] == 'c' && commandLineEquals[1].Length == 1)
                                            {
                                                _lightDictionary[leftCommandTwo[0]].Action = 128;
                                            }
                                            else
                                            {
                                                _lightDictionary[leftCommandTwo[0]].Action = Convert.ToInt32(commandLineEquals[1]);
                                            }
                                        }
                                        else if (leftCommandTwo[1].Equals("Position"))
                                        {
                                            //testLight.Position = 0;
                                            _lightDictionary[leftCommandTwo[0]].Position = Convert.ToInt32(commandLineEquals[1]);
                                        }
                                        else if (leftCommandTwo[1].Equals("Color"))
                                        {
                                            //testLight.Color = 0;
                                            _lightDictionary[leftCommandTwo[0]].Color = Convert.ToInt32(commandLineEquals[1]);
                                        }
                                        else
                                        {
                                            ShowError("Light没有" + leftCommandTwo[1] + "属性");
                                            return null;
                                        }
                                    }
                                    else if (_lightGroupDictionary.ContainsKey(leftCommandTwo[0].Trim()))
                                    {
                                        //是灯光组
                                        if (leftCommandTwo[1].Equals("Color"))
                                        {
                                            //testLight.Color = testColorGroup;
                                            if (_colorGroupDictionary.ContainsKey(commandLineEquals[1]))
                                            {
                                                //_lightGroupDictionary[leftCommandTwo[0]] = LightGroupMethod.SetColor(_lightGroupDictionary[leftCommandTwo[0]], _colorGroupDictionary[commandLineEquals[1]]);
                                            }
                                            else
                                            {
                                                ShowError("没有存在" + leftCommandTwo[1] + "颜色组");
                                                return null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //是否是集合关系
                        bool _isMatch = false;
                        CollectionType type = CollectionType.Intersection;
                        String collectionLightName = String.Empty;
                        foreach (var intersectionList in _intersectionDictionary)
                        {
                            if (intersectionList.Value.Contains(kvp.Key))
                            {
                                collectionLightName = intersectionList.Key;
                                _isMatch = true;
                                type = CollectionType.Intersection;
                                break;
                            }
                        }
                        foreach (var complementList in _complementDictionary)
                        {
                            if (complementList.Value.Contains(kvp.Key))
                            {
                                collectionLightName = complementList.Key;
                                _isMatch = true;
                                type = CollectionType.Complement;
                                break;
                            }
                        }
                        if (_isMatch)
                        {
                            //集合操作
                            List<Light> mainBig = new List<Light>();
                            mainBig.AddRange(_lightGroupDictionaryAll[collectionLightName].ToList());
                            mainBig.AddRange(_lightGroupDictionary[kvp.Key].ToList());
                            mainBig = LightBusiness.Splice(mainBig);

                            List<Light> big = LightBusiness.Split(mainBig, _lightGroupDictionaryAll[collectionLightName]);
                            List<Light> small = LightBusiness.Split(mainBig, _lightGroupDictionary[kvp.Key]);

                            List<Light> result = new List<Light>();
                            if (type == CollectionType.Intersection)
                            {
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
                            }
                            else if (type == CollectionType.Complement)
                            {
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
                            }
                            _lightGroupDictionaryAll[collectionLightName] = result;
                        }
                        else
                        {
                            _lightGroupDictionaryAll.Add(kvp.Key, _lightGroupDictionary[kvp.Key]);
                        }
                        if (kvp.Key.Equals(lightGroupName))
                        {
                            return _lightGroupDictionaryAll[lightGroupName];
                        }
                    }
                    //Locked
                    else {
                        _lightGroupDictionaryAll.Add(kvp.Key, lockedDictionary[kvp.Key]);
                        if (kvp.Key.Equals(lightGroupName))
                        {
                            return _lightGroupDictionaryAll[lightGroupName];
                        }
                    }
                    //所有的语句执行完成
                    //foreach (KeyValuePair<String, List<Light>> kvpLight in _lightGroupDictionary)
                    //    {
                    //        Console.WriteLine(kvpLight.Value.Count);
                    //    }

                    //}
                    //catch(Exception ex)
                    //{
                    //    //分割失败
                    //    Console.WriteLine("读取"+ kvp.Key+"内语句失败");
                    //    Console.WriteLine(ex.Message);
                    //    return;
                    //}
                }
                //if (!_lightGroupDictionaryAll.ContainsKey(lightGroupName)) { }
                ShowError("该文件下没有" + lightGroupName + "灯光组！");
                return null;
        }
            catch
            {
                return null;
            }
        }
        private String lastFilePath = String.Empty; 

        /// <summary>
        /// 保存脚本文件
        /// </summary>
        public void SaveScriptFile(String commandLine)
        {
            if (!nowControlPath.Equals(String.Empty))
            {
                // 文件名
                string fileName = nowControlPath;
                if (fileName.EndsWith(".light")) {
                    fileName += "Script";
                }
                // 创建文件，准备写入
                File.Delete(fileName);
                using (FileStream fs = File.Open(fileName,
                            FileMode.OpenOrCreate,
                            FileAccess.Write))
                {
                    using (StreamWriter wr = new StreamWriter(fs))
                    {
                        wr.Write(commandLine);
                    }
                }
                //if (iuc.mw.bIsEdit)
                //{
                //    iuc.mw.bIsEdit = false;
                //}
                //else {
                //    iuc.mw_.iNowPosition++;
                //    File.Copy(fileName, AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + iuc.mw_.iNowPosition + ".lightScript", true);
                //    if (iuc.mw_.iNowPosition == 999) {
                //        new MessageDialog(iuc.mw_, "Edit999").ShowDialog();
                //        iuc.mw_.ClearCache();
                //        iuc.mw_.iNowPosition = -1;
                //        iuc.mw_._bIsEdit = false;
                //        iuc.mw_.ProjectDocument_SelectionChanged_LightScript();
                //    }
                //}
            }
        }
        /// <summary>
        /// 导入灯光脚本语句
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public String LoadLightScript(String path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                String line;
                StringBuilder builder = new StringBuilder();
                while ((line = sr.ReadLine()) != null)
                {
                    builder.Append(line.ToString() + Environment.NewLine);
                }
                return builder.ToString();
            }
        }
        /// <summary>
        /// 显示错误列表
        /// </summary>
        /// <param name="error">错误信息</param>
        private void ShowError(String error)
        {
            System.Windows.Forms.MessageBox.Show(error);
        }
        
        String nowRelPath = String.Empty;
        /// <summary>
        /// 导入灯光脚本语句
        /// </summary>
        /// <param name="commandLine">语句</param>
        /// <param name="lightGroupDictionary">语句添加进哪个字典</param>
        /// <returns>是否创建成功</returns>
        private bool ImportLightScript(String commandLine)
        {
            if (commandLine.Length < 7)
            {
                ShowError("Import语法有误");
                return false;
            }
            String importCommandLine = commandLine.Substring(7);
            String[] importContent = importCommandLine.Split('^');
            if (importContent.Count() > 2)
            {
                ShowError("Import语法有误");
                return false;
            }
            if (importContent.Count() == 2)
            {
                String importType = importContent[0];//Abspath - 绝对路径  Relpath - 相对路径 Lib - 库路径(默认为库文件，可以不写)

                if (importType.Equals("Abspath"))
                {
                    //Absolutely
                    if (File.Exists(importContent[1]))
                    {
                        _importContent.Add(System.IO.Path.GetFileNameWithoutExtension(importContent[1]), importContent[1]);
                    }
                    else
                    {
                        ShowError("导入灯光脚本文件不存在");
                        return false;
                    }
                }
                else if (importType.Equals("Relpath"))
                {
                    //Relative
                    if (nowRelPath.Equals(String.Empty))
                    {
                        nowRelPath = System.IO.Path.GetDirectoryName(nowControlPath);
                    }
                    var relativePath = importContent[1];
                    Directory.SetCurrentDirectory(nowRelPath);
                    // Relative
                    if (File.Exists(System.IO.Path.GetFullPath(relativePath)))
                    {
                        //Console.WriteLine(System.IO.Path.GetFullPath(relativePath));
                        _importContent.Add(System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFullPath(relativePath)), System.IO.Path.GetFullPath(relativePath));
                        nowRelPath = System.IO.Path.GetFullPath(relativePath);
                    }
                    else
                    {
                        ShowError("导入灯光脚本文件不存在");
                        return false;
                    }
                }
                else if (importType.Equals("Resource"))
                {
                    //Resource
                    if (File.Exists(iuc.mw.LastProjectPath + @"\Resource\" + importContent[1]))
                    {
                        _importContent.Add(importContent[1], iuc.mw.LastProjectPath + @"\Resource\" + importContent[1]);
                    }
                    else
                    {
                        ShowError("导入灯光脚本文件不存在");
                        return false;
                    }
                }
            }
            else
            {
                //Library
                //if (File.Exists(lastFilePath + @"\Library\" + importCommandLine))
                //{
                //    _importContent.Add(importCommandLine.Split('.')[0], lastFilePath + @"\Library\" + importCommandLine);
                //}
                //else
                //{
                //    ShowError("导入灯光脚本文件不存在");
                //    return false;
                //}
                //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Library\" + importCommandLine))
                //{
                //    _importContent.Add(importCommandLine.Split('.')[0], AppDomain.CurrentDomain.BaseDirectory + @"\Library\" + importCommandLine);
                //}
                //else
                //{
                //    ShowError("导入灯光脚本文件不存在");
                //    return false;
                //}
                if (File.Exists(iuc.mw.LastProjectPath + @"\LightScript\" + importCommandLine))
                {
                    _importContent.Add(importCommandLine.Split('.')[0], iuc.mw.LastProjectPath + @"\LightScript\"+ importCommandLine);
                }
                else
                {
                    ShowError("导入灯光脚本文件不存在");
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// 灯光名是否含有其他变量的名称
        /// </summary>
        /// <param name="lightName">灯光名称</param>
        /// <param name="lightDictionary">灯光字典</param>
        /// <param name="_localContent">局部内容</param>
        /// <returns></returns>
        private bool ContainLight(String lightName, Dictionary<String, Light> lightDictionary, Dictionary<String, ContentType> _localContent)
        {
            if (lightDictionary.ContainsKey(lightName))
            {
                ShowError("已经有" + lightName + "灯光");
                return true;
            }
            if (_localContent.ContainsKey(lightName))
            {
                ShowError("已经有" + lightName + "局部变量");
                return true;
            }
            if (_globalContent.ContainsKey(lightName))
            {
                ShowError("已经有" + lightName + "全局变量");
                return true;
            }
            return false;
        }
        /// <summary>
        /// 灯光组名是否含有其他变量的名称
        /// </summary>
        /// <param name="lightName">灯光名称</param>
        /// <param name="lightGroupDictionary">灯光组字典</param>
        /// <param name="_localContent">局部内容</param>
        /// <returns></returns>
        private bool ContainLightGroup(String lightGroupName, Dictionary<String, List<Light>> lightGroupDictionary, Dictionary<String, ContentType> localContent)
        {
            if (lightGroupDictionary.ContainsKey(lightGroupName))
            {
                ShowError("已经有" + lightGroupName + "灯光组");
                return true;
            }
            if (localContent.ContainsKey(lightGroupName))
            {
                ShowError("已经有" + lightGroupName + "局部变量");
                return true;
            }
            if (_globalContent.ContainsKey(lightGroupName))
            {
                ShowError("已经有" + lightGroupName + "全局变量");
                return true;
            }
            return false;
        }

        /// <summary>
        /// 插入灯光[组]
        /// </summary>
        /// <param name="commandLine">语句</param>
        /// <param name="lightDictionary">需要添加的灯光字典</param>
        /// <param name="lightGroupDictionary">需要添加的灯光组字典<</param>
        /// <returns>是否创建成功</returns>
        private bool InsertLightOrGroup(String commandLine, Dictionary<String, Light> lightDictionary, Dictionary<String, List<Light>> lightGroupDictionary)
        {
            //被插入的灯光组
            MatchCollection matchNeedInsertLightOrGroup = Regex.Matches(commandLine, @"^[0-9a-zA-Z_\u4e00-\u9fa5]{1,}\.");
            //插入的灯光[组]
            MatchCollection matchInsertLight = Regex.Matches(commandLine, @"\([0-9a-zA-Z_\u4e00-\u9fa5]{1,}\)$");
            //被插入的灯光组名称字符串
            String strNeedInsertLightOrGroup = matchNeedInsertLightOrGroup[0].Value.Substring(0, matchNeedInsertLightOrGroup[0].Value.Length - 1).Trim();
            if (!lightGroupDictionary.ContainsKey(strNeedInsertLightOrGroup))
            {
                ShowError("没有存在" + strNeedInsertLightOrGroup + "灯光组");
                return false;
            }
            //插入的灯光[组]名称字符串
            String strInsertLightOrGroup = matchInsertLight[0].Value.Substring(1, matchInsertLight[0].Value.Length - 2).Trim();
            int type = -1;//0是灯光，1是灯光组
            if (lightDictionary.ContainsKey(strInsertLightOrGroup))
            {
                type = 0;
            }
            if (lightGroupDictionary.ContainsKey(strInsertLightOrGroup))
            {
                type = 1;
            }
            if (type == -1)
            {
                ShowError("没有存在" + strInsertLightOrGroup + "灯光[组]");
                return false;
            }
            else if (type == 0)
            {
                //添加灯光
                Light light = new Light();
                light.Time = lightDictionary[strInsertLightOrGroup].Time;
                light.Action = lightDictionary[strInsertLightOrGroup].Action;
                light.Position = lightDictionary[strInsertLightOrGroup].Position;
                light.Color = lightDictionary[strInsertLightOrGroup].Color;
                lightGroupDictionary[strNeedInsertLightOrGroup].Add(light);
                return true;
            }
            else
            {
                lightGroupDictionary[strNeedInsertLightOrGroup].AddRange(lightGroupDictionary[strInsertLightOrGroup].ToList());
                //foreach (Light light in lightGroupDictionary[strInsertLightOrGroup])
                //{
                //    lightGroupDictionary[strNeedInsertLightOrGroup].Add(new Light(light.Time, light.Action, light.Position, light.Color));
                //}
                return true;
            }
        }
        /// <summary>
        /// 创建范围组
        /// </summary>
        /// <param name="commandLine">语句</param>
        /// <param name="lightGroupDictionary">语句添加进哪个字典</param>
        /// <returns>是否创建成功</returns>
        private bool CreateRangeGroup(String commandLine, Dictionary<String, List<int>> rangeGroupDictionary, Dictionary<String, ContentType> _localContent)
        {
            MatchCollection matchRangeGroupName = Regex.Matches(commandLine, @"[0-9a-zA-Z_\u4e00-\u9fa5]{1,}\s*=");
            List<int> _rangeGroup = new List<int>();
            String strRangeGroupName = matchRangeGroupName[0].Value.Substring(0, matchRangeGroupName[0].Value.Length - 1).Trim();
            if (commandLine.Contains("\""))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.LastIndexOf(")"));
                String[] parameters = content.Split(',');
                if (parameters.Count() < 3)
                {
                    ShowError("参数个数不匹配");
                    return false;
                }
                //if (parameters[0].Trim().Length < 3)
                //{
                //    ShowError("参数字符不正确");
                //    return false;
                //}
                //if (parameters[0].Trim()[0] != '"' || parameters[0].Trim()[parameters[0].Trim().Length - 1] != '"')
                //{
                //    ShowError("参数字符不正确");
                //    return false;
                //}
                string controlString = content.Substring(1, content.LastIndexOf("\"") - 1);
                char splitNotation;
                if (parameters.Count() == 3)
                {
                    if (parameters[1].Trim().Length != 3)
                    {
                        ShowError("参数分隔符不正确");
                        return false;
                    }
                    if (parameters[1].Trim()[0] != '\'' || parameters[1].Trim()[2] != '\'')
                    {
                        ShowError("参数分隔符不正确");
                        return false;
                    }
                    splitNotation = parameters[1].Trim()[1];
                }
                else {
                    splitNotation = ',';
                }
                char rangeNotation ;
                if (parameters.Count() == 3)
                {
                    if (parameters[2].Trim().Length != 3)
                    {
                        ShowError("参数范围符不正确");
                        return false;
                    }
                    if (parameters[2].Trim()[0] != '\'' || parameters[2].Trim()[2] != '\'')
                    {
                        ShowError("参数范围符不正确");
                        return false;
                    }
                    rangeNotation = parameters[2].Trim()[1];
                }
                else {
                    if (parameters[parameters.Count()-1].Trim().Length != 3)
                    {
                        ShowError("参数范围符不正确");
                        return false;
                    }
                    if (parameters[parameters.Count() - 1].Trim()[0] != '\'' || parameters[parameters.Count() - 1].Trim()[2] != '\'')
                    {
                        ShowError("参数范围符不正确");
                        return false;
                    }
                    rangeNotation = parameters[parameters.Count() - 1].Trim()[1];
                }
               
                string[] strSplit = controlString.Split(splitNotation);
                for (int i = 0; i < strSplit.Length; i++)
                {
                    if (strSplit[i].Contains(rangeNotation))
                    {
                        String[] TwoNumber = null;
                        TwoNumber = strSplit[i].Split(rangeNotation);

                        int One = int.Parse(TwoNumber[0]);
                        int Two = int.Parse(TwoNumber[1]);
                        if (One < Two)
                        {
                            for (int k = One; k <= Two; k++)
                            {
                                _rangeGroup.Add(k);
                            }
                        }
                        else if (One > Two)
                        {
                            for (int k = One; k >= Two; k--)
                            {
                                _rangeGroup.Add(k);
                            }
                        }
                        else
                        {
                            _rangeGroup.Add(One);
                        }
                    }
                    else
                    {
                        _rangeGroup.Add(int.Parse(strSplit[i]));
                    }
                }
            }
            if (rangeGroupDictionary.ContainsKey(strRangeGroupName))
            {
                ShowError("已经有" + strRangeGroupName + "范围组");
                return false;
            }
            if (_localContent.ContainsKey(strRangeGroupName))
            {
                ShowError("已经有" + strRangeGroupName + "局部变量");
                return false;
            }
            if (_globalContent.ContainsKey(strRangeGroupName))
            {
                ShowError("已经有" + strRangeGroupName + "全局变量");
                return false;
            }
            rangeGroupDictionary.Add(strRangeGroupName, _rangeGroup);
            _localContent.Add(strRangeGroupName, ContentType.RangeGroup);
            return true;
        }
        /// <summary>
        /// 插入范围
        /// </summary>
        /// <param name="commandLine">语句</param>
        /// <param name="lightDictionary">需要添加的范围字典</param>
        /// <param name="lightGroupDictionary">需要添加的范围组字典<</param>
        /// <returns>是否创建成功</returns>
        private bool InsertRange(String commandLine, Dictionary<String, List<int>> rangeDictionary)
        {
            //被插入的范围组
            MatchCollection matchNeedInsertRange = Regex.Matches(commandLine, @"^[0-9a-zA-Z_\u4e00-\u9fa5]{1,}\.");
            //插入的数值
            MatchCollection matchInsertRange = Regex.Matches(commandLine, @"\([0-9a-zA-Z_\u4e00-\u9fa5]{1,}\)$");
            //被插入的数值int
            String strNeedInsertRange = matchNeedInsertRange[0].Value.Substring(0, matchNeedInsertRange[0].Value.Length - 1).Trim();
            if (!rangeDictionary.ContainsKey(strNeedInsertRange))
            {
                ShowError("没有存在" + strNeedInsertRange + "范围组");
                return false;
            }
            int intInsertRange = int.Parse(matchInsertRange[0].Value.Substring(1, matchInsertRange[0].Value.Length - 2).Trim());
            rangeDictionary[strNeedInsertRange].Add(intInsertRange);
            return true;
        }
        /// <summary>
        /// 创建颜色组
        /// </summary>
        /// <param name="commandLine">语句</param>
        /// <param name="lightGroupDictionary">语句添加进哪个字典</param>
        /// <returns>是否创建成功</returns>
        private bool CreateColorGroup(String commandLine, Dictionary<String, List<int>> colorGroupDictionary, Dictionary<String, ContentType> _localContent)
        {
            MatchCollection matchColorGroupName = Regex.Matches(commandLine, @"[0-9a-zA-Z_\u4e00-\u9fa5]{1,}\s*=");
            List<int> _ColorGroup = new List<int>();
            String strColorGroupName = matchColorGroupName[0].Value.Substring(0, matchColorGroupName[0].Value.Length - 1).Trim();
            if (commandLine.Contains("\""))
            {
                String content = commandLine.Substring(commandLine.IndexOf('(') + 1);
                content = content.Substring(0, content.LastIndexOf(")"));
                String[] parameters = content.Split(',');

                if (parameters.Count() < 3)
                {
                    ShowError("参数个数不匹配");
                    return false;
                }
                if (content.LastIndexOf("\"") == -1)
                {
                    ShowError("参数字符不正确");
                    return false;
                }
                string controlString = content.Substring(1, content.LastIndexOf("\"")-1);
                char splitNotation;
                if (parameters.Count() == 3)
                {
                    if (parameters[1].Trim().Length != 3)
                    {
                        ShowError("参数分隔符不正确");
                        return false;
                    }
                    if (parameters[1].Trim()[0] != '\'' || parameters[1].Trim()[2] != '\'')
                    {
                        ShowError("参数分隔符不正确");
                        return false;
                    }
                    splitNotation = parameters[1].Trim()[1];
                }
                else {
                    splitNotation = ',';
                }
                char ColorNotation;
                if (parameters.Count() == 3) {
                    if (parameters[2].Trim().Length != 3)
                    {
                        ShowError("参数范围符不正确");
                        return false;
                    }
                    if (parameters[2].Trim()[0] != '\'' || parameters[2].Trim()[2] != '\'')
                    {
                        ShowError("参数范围符不正确");
                        return false;
                    }
                    ColorNotation = parameters[2].Trim()[1];
                }
                else {
                    if (parameters[parameters.Count() - 1].Trim().Length != 3)
                    {
                        ShowError("参数范围符不正确");
                        return false;
                    }
                    if (parameters[parameters.Count() - 1].Trim()[0] != '\'' || parameters[parameters.Count() - 1].Trim()[2] != '\'')
                    {
                        ShowError("参数范围符不正确");
                        return false;
                    }
                    ColorNotation = parameters[parameters.Count() - 1][1];
                }
                string[] strSplit = controlString.Split(splitNotation);
                for (int i = 0; i < strSplit.Length; i++)
                {
                    if (strSplit[i].Contains(ColorNotation))
                    {
                        String[] TwoNumber = null;
                        TwoNumber = strSplit[i].Split(ColorNotation);

                        int One = int.Parse(TwoNumber[0]);
                        int Two = int.Parse(TwoNumber[1]);
                        if (One < Two)
                        {
                            for (int k = One; k <= Two; k++)
                            {
                                _ColorGroup.Add(k);
                            }
                        }
                        else if (One > Two)
                        {
                            for (int k = One; k >= Two; k--)
                            {
                                _ColorGroup.Add(k);
                            }
                        }
                        else
                        {
                            _ColorGroup.Add(One);
                        }
                    }
                    else
                    {
                        _ColorGroup.Add(int.Parse(strSplit[i]));
                    }
                }
            }
            if (colorGroupDictionary.ContainsKey(strColorGroupName))
            {
                ShowError("已经有" + strColorGroupName + "颜色组");
                return false;
            }
            if (_localContent.ContainsKey(strColorGroupName))
            {
                ShowError("已经有" + strColorGroupName + "局部变量");
                return false;
            }
            if (_globalContent.ContainsKey(strColorGroupName))
            {
                ShowError("已经有" + strColorGroupName + "全局变量");
                return false;
            }
            colorGroupDictionary.Add(strColorGroupName, _ColorGroup);
            _localContent.Add(strColorGroupName, ContentType.ColorGroup);
            return true;
        }
        /// <summary>
        /// 插入颜色
        /// </summary>
        /// <param name="commandLine">语句</param>
        /// <param name="lightDictionary">需要添加的颜色字典</param>
        /// <param name="lightGroupDictionary">需要添加的范围组字典<</param>
        /// <returns>是否创建成功</returns>
        private bool InsertColor(String commandLine, Dictionary<String, List<int>> ColorDictionary)
        {
            //被插入的范围组
            MatchCollection matchNeedInsertColor = Regex.Matches(commandLine, @"^[0-9a-zA-Z_\u4e00-\u9fa5]{1,}\.");
            //插入的数值
            MatchCollection matchInsertColor = Regex.Matches(commandLine, @"\([0-9a-zA-Z_\u4e00-\u9fa5]{1,}\)$");
            //被插入的数值int
            String strNeedInsertColor = matchNeedInsertColor[0].Value.Substring(0, matchNeedInsertColor[0].Value.Length - 1).Trim();
            if (!ColorDictionary.ContainsKey(strNeedInsertColor))
            {
                ShowError("没有存在" + strNeedInsertColor + "颜色组");
                return false;
            }
            //插入的灯光[组]名称字符串
            int intInsertColor = int.Parse(matchInsertColor[0].Value.Substring(1, matchInsertColor[0].Value.Length - 2).Trim());
            ColorDictionary[strNeedInsertColor].Add(intInsertColor);
            return true;
        }

        private List<Light> EditLightGroup(String commandContent, List<Light> needEditLightGroup, Dictionary<String, List<int>> rangeGroupDictionary,Dictionary<String, List<int>> colorGroupDictionary)
        {
            List<Light> _lightGroup = null;
            //// = Edit.HorizontalFlipping(testLightGroup);
            //_lightGroup = EditMethod.EditMain(commandContent, needEditLightGroup, rangeGroupDictionary, colorGroupDictionary,iuc.mw.thirdPartys);
            //if (_lightGroup == null)
            //{
            //    //ShowError("Edit修改灯光失败");
            //    return null;
            //}
            return _lightGroup;

        }
        private Light _BottomCreateLight(String commandLine)
        {
            MatchCollection matchLightContent = Regex.Matches(commandLine, @"\([0-9,oc]*\)");
            Light _light = new Light();

            String[] strsContent = matchLightContent[0].Value.Substring(1, matchLightContent[0].Value.Length - 2).Split(',');
            //有四个参数
            if (strsContent.Length == 4)
            {
                _light.Time = int.Parse(strsContent[0]);
                if (strsContent[1].Length == 1 && strsContent[1][0] == 'o')
                {
                    _light.Action = 144;
                }
                else if (strsContent[1].Length == 1 && strsContent[1][0] == 'c')
                {
                    _light.Action = 128;
                }
                else
                {
                    //Console.WriteLine(strsContent[1].Length);
                    _light.Action = int.Parse(strsContent[1]);
                }

                _light.Position = int.Parse(strsContent[2]);
                _light.Color = int.Parse(strsContent[3]);
            }
            return _light;
        }
        private List<Light> _BottomCreateLightGroup(String commandLine,Dictionary<String, List<Light>> lightGroupDictionaryAll)
        {
            List<Light> _lightGroup = new List<Light>();
            //= new LightGroup()
            Regex P_createLightGroup1 = new Regex(@"\s*new\s+LightGroup\(\)");
            //创建灯光语句
            if (P_createLightGroup1.IsMatch(commandLine))
            {
                //不处理
            }
            //= hhmLightScript.A();或者this.A();
            MatchCollection matchCreateLightGroup2 = Regex.Matches(commandLine, @"\s*[0-9a-zA-Z_\u4e00-\u9fa5]{1,}\.[0-9a-zA-Z_\u4e00-\u9fa5]{1,}\(\)");
            if (matchCreateLightGroup2.Count > 0)
            {
                String[] strs = matchCreateLightGroup2[0].Value.Split('.');
                if (strs.Count() != 2)
                {
                    ShowError("创建灯光组格式有误");
                    return null;
                }
                String importFilePathName = strs[0].Trim();
                String importFileChildName = strs[1].Split('(')[0].Trim();

                if (importFilePathName.Equals("this"))
                {
                    if (importFileChildName.Equals("Main"))
                    {
                        ShowError(importFilePathName + "不允许.Main");
                        return null;
                    }
                    if (lightGroupDictionaryAll.ContainsKey(importFileChildName)) {
                        _lightGroup = LightBusiness.Copy(lightGroupDictionaryAll[importFileChildName]);
                    }else {
                        ShowError(importFilePathName + "this.不存在");
                        return null;
                    }
                }
                else
                {
                    if (!_importContent.ContainsKey(importFilePathName))
                    {
                        ShowError(importFilePathName + "不存在");
                        return null;
                    }
                    //先得到完整的语句
                    _lightGroup = ScriptToLightGroup(GetCompleteLightScript(_importContent[importFilePathName]), importFileChildName);
                }
            }
            //// = Create.CreateLightGroup(时间,范围,间隔,持续时间,颜色);
            //// = Create.CreateLightGroup(1, 2, 3, 4, 5)
            //Regex P_Create = new Regex(@"\s*Create\.");
            ////插入灯光[组]语句
            //if (P_Create.IsMatch(commandLine))
            //{
            //    _lightGroup = Create.CreateMain(commandLine, _rangeGroupDictionary, _colorGroupDictionary);
            //    if (_lightGroup == null) {
            //        ShowError("Create_CreateLightGroup创建灯光组：失败，输入的参数不对");
            //        return null;
            //    }
            //}
            return _lightGroup;
        }
       
        string RelativePath(string absolutePath, string relativeTo)
        {
            //from - www.cnphp6.com

            string[] absoluteDirectories = absolutePath.Split('\\');
            string[] relativeDirectories = relativeTo.Split('\\');

            //Get the shortest of the two paths
            int length = absoluteDirectories.Length < relativeDirectories.Length ? absoluteDirectories.Length : relativeDirectories.Length;

            //Use to determine where in the loop we exited
            int lastCommonRoot = -1;
            int index;

            //Find common root
            for (index = 0; index < length; index++)
                if (absoluteDirectories[index] == relativeDirectories[index])
                    lastCommonRoot = index;
                else
                    break;

            //If we didn't find a common prefix then throw
            if (lastCommonRoot == -1)
                throw new ArgumentException("Paths do not have a common base");

            //Build up the relative path
            StringBuilder relativePath = new StringBuilder();

            //Add on the ..
            for (index = lastCommonRoot + 1; index < absoluteDirectories.Length; index++)
                if (absoluteDirectories[index].Length > 0)
                    relativePath.Append("..\\");

            //Add on the folders
            for (index = lastCommonRoot + 1; index < relativeDirectories.Length - 1; index++)
                relativePath.Append(relativeDirectories[index] + "\\");
            relativePath.Append(relativeDirectories[relativeDirectories.Length - 1]);

            return relativePath.ToString();
        }

        public String GetCompleteLightScript(String filePath) {
            //获得数据
            LightScriptBusiness scriptBusiness = new LightScriptBusiness();
            String command = scriptBusiness.LoadLightScript(filePath);
            Dictionary<String, String> dictionary = scriptBusiness.GetCatalog(command,out Dictionary<String, List<String>> extendsDictionary, out Dictionary<String, List<String>> intersectionDictionary, out Dictionary<String, List<String>> complementDictionary);
            String visibleStr = String.Empty;
            String importStr = String.Empty;
            String finalStr = String.Empty;

            Dictionary<String, String> lightScriptDictionary = new Dictionary<string, string>();
            Dictionary<String, bool> visibleDictionary = new Dictionary<String, bool>();
            Dictionary<String, List<String>> containDictionary = new Dictionary<String, List<String>>();
            Dictionary<String, String> finalDictionary = new Dictionary<string, string>();
            List<String> importList = new List<String>();

            foreach (var item in dictionary)
            {
                if (item.Key.Trim().Equals("NoVisible"))
                {
                    visibleStr = item.Value;
                }
                else if (item.Key.Trim().Equals("Contain"))
                {

                }
                else if (item.Key.Trim().Equals("Import"))
                {
                    importStr = item.Value;
                }
                else if (item.Key.Trim().Equals("Final"))
                {
                    finalStr = item.Value;
                }
                else
                {
                    lightScriptDictionary.Add(item.Key, item.Value);
                    visibleDictionary.Add(item.Key, true);
                }
            }
            if (!visibleStr.Equals(String.Empty))
            {
                visibleStr = visibleStr.Replace(System.Environment.NewLine, "");
                visibleStr = visibleStr.Replace("\t", "");

                String[] visibleStrs = visibleStr.Split(';');
                foreach (String str in visibleStrs)
                {
                    if (str.Trim().Equals(String.Empty))
                        continue;
                    if (visibleDictionary.ContainsKey(str))
                    {
                        visibleDictionary[str] = false;
                    }
                }
            }
            if (!importStr.Equals(String.Empty))
            {
                importStr = importStr.Replace(System.Environment.NewLine, "");
                importStr = importStr.Replace("\t", "");

                String[] importStrs = importStr.Split(';');
                foreach (String str in importStrs)
                {
                    if (str.Trim().Equals(String.Empty))
                        continue;

                    if (!importList.Contains(str))
                    {
                        importList.Add(str);
                    }
                }
            }
            if (!finalStr.Equals(String.Empty))
            {
                finalStr = finalStr.Replace(System.Environment.NewLine, "");
                finalStr = finalStr.Replace("\t", "");

                String[] finalStrs = finalStr.Split('.');
                foreach (String str in finalStrs)
                {
                    if (str.Trim().Equals(String.Empty))
                        continue;

                    String[] strs = str.Split(':');
                    finalDictionary.Add(strs[0], strs[1]);
                }
            }

            //拼接数据
            //添加导入库语句
            StringBuilder importbuilder = new StringBuilder();
            foreach (var item in importList)
            {
                importbuilder.Append("Import " + item + ";" + Environment.NewLine);
            }
            //添加主体语句
            StringBuilder builder = new StringBuilder();
            if (!importbuilder.ToString().Equals(String.Empty))
            {
                //去掉最后一个\r\n
                builder.Append(importbuilder.ToString().Substring(importbuilder.ToString().Length - 2));
            }
            foreach (var item in lightScriptDictionary)
            {
                if (!visibleDictionary[item.Key])
                    continue;
                bool isMatch = false;
                foreach (var itemChildren in extendsDictionary)
                {
                    if (itemChildren.Value.Contains(item.Key))
                    {
                        builder.Append(item.Key + " extends " + itemChildren.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine);
                        if (item.Value.Contains(item.Key + "LightGroup"))
                        {
                            builder.Append("\t" + item.Key + ".Add(" + item.Key + "LightGroup);");
                        }
                        builder.Append(Environment.NewLine + "}" + Environment.NewLine);
                        isMatch = true;
                        break;
                    }
                }
                foreach (var itemChildren in intersectionDictionary)
                {
                    if (itemChildren.Value.Contains(item.Key))
                    {
                        builder.Append(item.Key + " intersection " + itemChildren.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine);
                        if (item.Value.Contains(item.Key + "LightGroup"))
                        {
                            builder.Append("\t" + item.Key + ".Add(" + item.Key + "LightGroup);");
                        }
                        builder.Append(Environment.NewLine + "}" + Environment.NewLine);
                        isMatch = true;
                        break;
                    }
                }
                foreach (var itemChildren in complementDictionary)
                {
                    if (itemChildren.Value.Contains(item.Key))
                    {
                        builder.Append(item.Key + " complement " + itemChildren.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine);
                        if (item.Value.Contains(item.Key + "LightGroup"))
                        {
                            builder.Append("\t" + item.Key + ".Add(" + item.Key + "LightGroup);");
                        }
                        builder.Append(Environment.NewLine + "}" + Environment.NewLine);
                        isMatch = true;
                        break;
                    }
                }
                if (!isMatch)
                {
                    builder.Append(item.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine);
                    if (item.Value.Contains(item.Key + "LightGroup"))
                    {
                        builder.Append("\t" + item.Key + ".Add(" + item.Key + "LightGroup);");
                    }
                    builder.Append(Environment.NewLine + "}" + Environment.NewLine);
                }
            }
            builder.Append("Main{" + Environment.NewLine);
            int i = 1;
            foreach (var item in lightScriptDictionary)
            {
                if (builder.ToString().Contains(item.Key + ".Add("))
                {
                    bool isMatch = false;
                    foreach (var itemChildren in intersectionDictionary)
                    {
                        if (itemChildren.Value.Contains(item.Key))
                        {
                            isMatch = true;
                            break;
                        }
                    }
                    foreach (var itemChildren in complementDictionary)
                    {
                        if (itemChildren.Value.Contains(item.Key))
                        {
                            isMatch = true;
                            break;
                        }
                    }
                    if (isMatch)
                    {
                        continue;
                    }
                    builder.Append("\tLightGroup testLightGroup" + i.ToString() + " = this." + item.Key + "();" + Environment.NewLine);
                    if (finalDictionary.ContainsKey(item.Key))
                    {
                        String[] contents = finalDictionary[item.Key].Split(';');
                        StringBuilder commandStringBuilder = new StringBuilder();
                        foreach (String str in contents)
                        {
                            if (str.Equals(String.Empty))
                                continue;
                            String[] strs = str.Split('=');
                            String type = strs[0];
                            String[] _contents = strs[1].Split(',');

                            if (type.Equals("Color"))
                            {
                                foreach (String _str in _contents)
                                {
                                    String[] mContents = _str.Split('-');
                                    if (mContents[0].Equals("Format"))
                                    {
                                        if (mContents[1].Equals("Green"))
                                        {
                                            commandStringBuilder.Append("\tColorGroup testColorGroup" + i.ToString() + " = new ColorGroup(\""
                                                + "73 74 75 76" + "\",' ','-');" + Environment.NewLine
                                              + "\ttestLightGroup" + i.ToString() + ".Color = testColorGroup" + i.ToString() + "; ");
                                        }
                                        if (mContents[1].Equals("Blue"))
                                        {
                                            commandStringBuilder.Append("\tColorGroup testColorGroup" + i.ToString() + " = new ColorGroup(\""
                                            + "33 37 41 45" + "\",' ','-');" + Environment.NewLine
                                          + "\ttestLightGroup" + i.ToString() + ".Color = testColorGroup" + i.ToString() + "; ");
                                        }
                                        if (mContents[1].Equals("Pink"))
                                        {
                                            commandStringBuilder.Append("\tColorGroup testColorGroup" + i.ToString() + " = new ColorGroup(\""
                                           + "4 94 53 57" + "\",' ','-');" + Environment.NewLine
                                         + "\ttestLightGroup" + i.ToString() + ".Color = testColorGroup" + i.ToString() + "; ");
                                        }
                                        if (mContents[1].Equals("Diy"))
                                        {
                                            commandStringBuilder.Append("\tColorGroup testColorGroup" + i.ToString() + " = new ColorGroup(\""
                                       + mContents[2] + "\",' ','-');" + Environment.NewLine
                                     + "\ttestLightGroup" + i.ToString() + ".Color = testColorGroup" + i.ToString() + "; ");
                                        }
                                    }
                                    else if (mContents[0].Equals("Shape"))
                                    {
                                        if (mContents[1].Equals("Square"))
                                        {
                                            commandStringBuilder.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.ShapeColor(testLightGroup" + i.ToString() + ",Square,\"" + mContents[2] + "\");");
                                        }
                                        else if (mContents[1].Equals("RadialVertical"))
                                        {
                                            commandStringBuilder.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.ShapeColor(testLightGroup" + i.ToString() + ",RadialVertical,\"" + mContents[2] + "\");");
                                        }
                                        else if (mContents[1].Equals("RadialHorizontal"))
                                        {
                                            commandStringBuilder.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + "= Edit.ShapeColor(testLightGroup" + i.ToString() + ",RadialHorizontal,\"" + mContents[2] + "\");");
                                        }
                                    }
                                }
                            }
                            if (type.Equals("Shape"))
                            {
                                foreach (String _str in _contents)
                                {
                                    if (_str.Equals("HorizontalFlipping"))
                                    {
                                        commandStringBuilder.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.HorizontalFlipping(testLightGroup" + i.ToString() + ");");
                                    }
                                    if (_str.Equals("VerticalFlipping"))
                                    {
                                        commandStringBuilder.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.VerticalFlipping(testLightGroup" + i.ToString() + ");");
                                    }
                                    if (_str.Equals("Clockwise"))
                                    {
                                        commandStringBuilder.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.Clockwise(testLightGroup" + i.ToString() + ");");
                                    }
                                    if (_str.Equals("AntiClockwise"))
                                    {
                                        commandStringBuilder.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.AntiClockwise(testLightGroup" + i.ToString() + ");");
                                    }
                                }
                            }
                            if (type.Equals("Time"))
                            {
                                foreach (String _str in _contents)
                                {
                                    if (_str.Equals("Reversal"))
                                    {
                                        commandStringBuilder.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.Reversal(testLightGroup" + i.ToString() + ");");
                                    }
                                    String[] mContents = _str.Split('-');
                                    if (mContents[0].Equals("ChangeTime"))
                                    {
                                        commandStringBuilder.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.ChangeTime(testLightGroup" + i.ToString() + "," + mContents[1] + "," + mContents[2] + ");");
                                    }
                                    else if (mContents[0].Equals("StartTime"))
                                    {
                                        commandStringBuilder.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = testLightGroup" + i.ToString() + ".SetStartTime(" + mContents[1] + ");");
                                    }
                                    else if (mContents[0].Equals("AllTime"))
                                    {
                                        commandStringBuilder.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = testLightGroup" + i.ToString() + ".SetAllTime(" + mContents[1] + ");");
                                    }
                                }
                            }
                            if (type.Equals("ColorOverlay"))
                            {
                                foreach (String _str in _contents)
                                {
                                    String[] mContents = _str.Split('-');
                                    if (mContents[0].Equals("true"))
                                    {
                                        commandStringBuilder.Append("\tColorGroup testColorGroup" + i.ToString() + " = new ColorGroup(\""
                                     + mContents[1] + "\",' ','-');" + Environment.NewLine
                                   + "\ttestLightGroup" + i.ToString() + " = Edit.CopyToTheFollow(testLightGroup" + i.ToString() + ",testColorGroup" + i.ToString() + "; ");
                                    }
                                    else
                                    {
                                        commandStringBuilder.Append("\tColorGroup testColorGroup" + i.ToString() + " = new ColorGroup(\""
                                 + mContents[1] + "\",' ','-');" + Environment.NewLine
                               + "\ttestLightGroup" + i.ToString() + " = Edit.CopyToTheEnd(testLightGroup" + i.ToString() + ",testColorGroup" + i.ToString() + "; ");
                                    }
                                }
                            }
                            if (type.Equals("SportOverlay"))
                            {
                                foreach (String _str in _contents)
                                {
                                    String[] mContents = _str.Split('-');
                                    commandStringBuilder.Append("\tRangeGroup testRangeGroup" + i.ToString() + " = new RangeGroup(\""
                             + mContents[0] + "\",' ','-');" + Environment.NewLine
                           + "\ttestLightGroup" + i.ToString() + " = Edit.AccelerationOrDeceleration(testLightGroup" + i.ToString() + ",testRangeGroup" + i.ToString() + "; ");
                                }
                            }
                            if (type.Equals("Other"))
                            {
                                foreach (String _str in _contents)
                                {
                                    if (_str.Equals("RemoveBorder"))
                                    {
                                        commandStringBuilder.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.RemoveBorder(testLightGroup" + i.ToString() + ");");
                                    }
                                }
                            }
                        }
                        builder.Append(commandStringBuilder.ToString());
                    }
                    builder.Append("\tMain.Add(testLightGroup" + i.ToString() + ");" + Environment.NewLine);
                    i++;
                }
            }
            if (finalDictionary.ContainsKey("Main"))
            {
                String[] contents = finalDictionary["Main"].Split(';');
                StringBuilder commandStringBuilder = new StringBuilder();
                foreach (String str in contents)
                {
                    if (str.Equals(String.Empty))
                        continue;
                    String[] strs = str.Split('=');
                    String type = strs[0];
                    String[] _contents = strs[1].Split(',');

                    if (type.Equals("Color"))
                    {
                        foreach (String _str in _contents)
                        {
                            String[] mContents = _str.Split('-');
                            if (mContents[0].Equals("Format"))
                            {
                                if (mContents[1].Equals("Green"))
                                {
                                    commandStringBuilder.Append("\tColorGroup testColorGroup" + i.ToString() + " = new ColorGroup(\""
                                        + "73 74 75 76" + "\",' ','-');" + Environment.NewLine
                                      + "\tMain.Color = testColorGroup" + i.ToString() + "; ");
                                }
                                if (mContents[1].Equals("Blue"))
                                {
                                    commandStringBuilder.Append("\tColorGroup testColorGroup" + i.ToString() + " = new ColorGroup(\""
                                    + "33 37 41 45" + "\",' ','-');" + Environment.NewLine
                                  + "\tMain.Color = testColorGroup" + i.ToString() + "; ");
                                }
                                if (mContents[1].Equals("Pink"))
                                {
                                    commandStringBuilder.Append("\tColorGroup testColorGroup" + i.ToString() + " = new ColorGroup(\""
                                   + "4 94 53 57" + "\",' ','-');" + Environment.NewLine
                                 + "\tMain.Color = testColorGroup" + i.ToString() + "; ");
                                }
                                if (mContents[1].Equals("Diy"))
                                {
                                    commandStringBuilder.Append("\tColorGroup testColorGroup" + i.ToString() + " = new ColorGroup(\""
                               + mContents[2] + "\",' ','-');" + Environment.NewLine
                             + "\tMain.Color = testColorGroup" + i.ToString() + "; ");
                                }
                            }
                            else if (mContents[0].Equals("Shape"))
                            {
                                if (mContents[1].Equals("Square"))
                                {
                                    commandStringBuilder.Append(Environment.NewLine + "\tMain = Edit.ShapeColor(Main,Square,\"" + mContents[2] + "\");");
                                }
                                else if (mContents[1].Equals("RadialVertical"))
                                {
                                    commandStringBuilder.Append(Environment.NewLine + "\tMain = Edit.ShapeColor(Main,RadialVertical,\"" + mContents[2] + "\");");
                                }
                                else if (mContents[1].Equals("RadialHorizontal"))
                                {
                                    commandStringBuilder.Append(Environment.NewLine + "\tMain = Edit.ShapeColor(Main,RadialHorizontal,\"" + mContents[2] + "\");");
                                }
                            }
                        }
                    }
                    if (type.Equals("Shape"))
                    {
                        foreach (String _str in _contents)
                        {
                            if (_str.Equals("HorizontalFlipping"))
                            {
                                commandStringBuilder.Append(Environment.NewLine + "\tMain = Edit.HorizontalFlipping(Main);");
                            }
                            if (_str.Equals("VerticalFlipping"))
                            {
                                commandStringBuilder.Append(Environment.NewLine + "\tMain = Edit.VerticalFlipping(Main);");
                            }
                            if (_str.Equals("Clockwise"))
                            {
                                commandStringBuilder.Append(Environment.NewLine + "\tMain = Edit.Clockwise(Main);");
                            }
                            if (_str.Equals("AntiClockwise"))
                            {
                                commandStringBuilder.Append(Environment.NewLine + "\tMain = Edit.AntiClockwise(Main);");
                            }
                        }
                    }
                    if (type.Equals("Time"))
                    {
                        foreach (String _str in _contents)
                        {
                            if (_str.Equals("Reversal"))
                            {
                                commandStringBuilder.Append(Environment.NewLine + "\tMain = Edit.Reversal(Main);");
                            }
                            String[] mContents = _str.Split('-');
                            if (mContents[0].Equals("ChangeTime"))
                            {
                                commandStringBuilder.Append(Environment.NewLine + "\tMain = Edit.ChangeTime(Main," + mContents[1] + "," + mContents[2] + ");");
                            }
                            else if (mContents[0].Equals("StartTime"))
                            {
                                commandStringBuilder.Append(Environment.NewLine + "\tMain = Main.SetStartTime(" + mContents[1] + ");");
                            }
                            else if (mContents[0].Equals("AllTime"))
                            {
                                commandStringBuilder.Append(Environment.NewLine + "\tMain = Main.SetAllTime(" + mContents[1] + ");");
                            }
                        }
                    }
                    if (type.Equals("ColorOverlay"))
                    {
                        foreach (String _str in _contents)
                        {
                            String[] mContents = _str.Split('-');
                            if (mContents[0].Equals("true"))
                            {
                                commandStringBuilder.Append("\tColorGroup testColorGroup" + i.ToString() + " = new ColorGroup(\""
                             + mContents[1] + "\",' ','-');" + Environment.NewLine
                           + "\tMain = Edit.CopyToTheFollow(Main,testColorGroup" + i.ToString() + ";");
                            }
                            else
                            {
                                commandStringBuilder.Append("\tColorGroup testColorGroup" + i.ToString() + " = new ColorGroup(\""
                         + mContents[1] + "\",' ','-');" + Environment.NewLine
                       + "\tMain = Edit.CopyToTheEnd(Main,testColorGroup" + i.ToString() + ";");
                            }
                        }
                    }
                    if (type.Equals("SportOverlay"))
                    {
                        foreach (String _str in _contents)
                        {
                            String[] mContents = _str.Split('-');
                            commandStringBuilder.Append("\tRangeGroup testRangeGroup" + i.ToString() + " = new RangeGroup(\""
                     + mContents[0] + "\",' ','-');" + Environment.NewLine
                   + "\tMain = Edit.AccelerationOrDeceleration(Main,testRangeGroup" + i.ToString() + ";");
                        }
                    }
                    if (type.Equals("Other"))
                    {
                        foreach (String _str in _contents)
                        {
                            if (_str.Equals("RemoveBorder"))
                            {
                                commandStringBuilder.Append(Environment.NewLine + "\t" + "Main = Edit.RemoveBorder(Main);");
                            }
                        }
                    }
                    builder.Append(commandStringBuilder);
                }
            }
            builder.Append("}");
            return builder.ToString();
        }

    }
}