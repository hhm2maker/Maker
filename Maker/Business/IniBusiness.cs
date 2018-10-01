using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;

namespace Maker_IDE.Business
{
    class ConfigBusiness
    {
        public Dictionary<string, string> configData;
        string fullFileName;
        public ConfigBusiness(string _fileName)
        {
            configData = new Dictionary<string, string>();
            fullFileName = _fileName;
            bool hasCfgFile = File.Exists(_fileName);
            if (hasCfgFile == false)
            {
                StreamWriter writer = new StreamWriter(File.Create(_fileName), Encoding.Default);
                writer.Close();
            }
            StreamReader reader = new StreamReader(_fileName, Encoding.Default);
            string line;

            int indx = 0;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith(";") || string.IsNullOrEmpty(line))
                    configData.Add(";" + indx++, line);
                else
                {
                    string[] key_value = line.Split('=');
                    if (key_value.Length >= 2)
                        configData.Add(key_value[0], key_value[1]);
                    else
                        configData.Add(";" + indx++, line);
                }
            }
            reader.Close();
        }

        public string Get(string key)
        {
            if (configData.Count <= 0)
                return null;
            else if (configData.ContainsKey(key))
                return configData[key].ToString();
            else
                return null;
        }

        public void Set(string key, string value)
        {
            if (configData.ContainsKey(key))
                configData[key] = value;
            else
                configData.Add(key, value);
        }

        public void Save()
        {
            StreamWriter writer = new StreamWriter(fullFileName, false, Encoding.Default);
            IDictionaryEnumerator enu = configData.GetEnumerator();
            while (enu.MoveNext())
            {
                if (enu.Key.ToString().StartsWith(";"))
                    writer.WriteLine(enu.Value);
                else
                    writer.WriteLine(enu.Key + "=" + enu.Value);
            }
            writer.Close();
        }
    }
}