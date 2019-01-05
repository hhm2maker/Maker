using Maker.Business.ScriptUserControlBusiness;
using Maker.Business.ViewBusiness.Currency;
using Maker.Model;
using Maker.View.LightScriptUserControl;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maker.Business.ViewBusiness.ScriptUserControl
{
    public static class Results
    {
        public static Dictionary<string, List<Light>> Test(Dictionary<String, ScriptModel> scriptModelDictionary)
        {
            return ScriptFileBusiness.Test(scriptModelDictionary) ;
        }
        public static List<Light> Test(Dictionary<String, ScriptModel> scriptModelDictionary,String stepName)
        {
            return ScriptFileBusiness.Test(scriptModelDictionary, stepName);
        }
    }
}
