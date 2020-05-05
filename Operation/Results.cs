using Maker.Model;
using Microsoft.CSharp;
using Operation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Operation
{
    public static class Results
    {
        public static Dictionary<string, List<Light>> Test(Dictionary<String, ScriptModel> scriptModelDictionary)
        {
            return ScriptFileBusiness.Test(scriptModelDictionary);
        }
        public static List<Light> Test(Dictionary<String, ScriptModel> scriptModelDictionary, String stepName)
        {
            if (ScriptFileBusiness.Test(scriptModelDictionary, stepName).ContainsKey(stepName))
                return ScriptFileBusiness.Test(scriptModelDictionary, stepName)[stepName];
            else
                return new List<Light>();
        }
    }
}
