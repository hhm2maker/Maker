using Maker.Business;
using Maker.Model;
using Maker.View.LightScriptUserControl;
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
using System.Xml.Linq;

namespace Maker.Bridge
{
    public class ScriptUserControlBridge
    {
        private ScriptUserControl view;
        public ScriptUserControlBridge(ScriptUserControl view) {
            this.view = view;
        }
      
        public void GetResult() {
            //view.mLightList = Results.Test(view.scriptModelDictionary);
            view.UpdateData(Business.Currency.OperationUtils.OperationLightDictionaryToMakerLightDictionary(Results.Test(view.scriptModelDictionary)));
            if (!view._bIsEdit) {
                view.SaveFile();
                view.CopyFile();
                view._bIsEdit = false;
            }
        }
        /// <summary>
        /// 根据模型集合返回指定步骤名完整代码
        /// </summary>
        /// <param name="scriptModelDictionary"></param>
        /// <returns></returns>
        public void GetBlockResult(String stepName)
        {
            view.mBlockLightList.Clear();
            view.mBlockLightList = Business.Currency.OperationUtils.OperationLightToMakerLight(Results.Test(view.scriptModelDictionary,stepName)); 
        }

        /// <summary>
        /// 根据模型集合返回指定步骤名完整代码
        /// </summary>
        /// <param name="scriptModelDictionary"></param>
        /// <returns></returns>
        public void GetBlockResult(String stepName, Dictionary<String, ScriptModel> scriptModelDictionary)
        {
            view.mLaunchpadData.Clear();
            view.mLaunchpadData = Business.Currency.OperationUtils.OperationLightToMakerLight(Results.Test(scriptModelDictionary, stepName));
        }

        public Dictionary<String, ScriptModel> GetScriptModelDictionary(String filePath,out String introduce,out String audioResources ) {
            return ScriptFileBusiness.GetScriptModelDictionary(filePath,out introduce,out audioResources);
        }

      
    }
}
