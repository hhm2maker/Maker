﻿using Maker.Business;
using Maker.Business.ScriptUserControlBusiness;
using Maker.Business.ViewBusiness.ScriptUserControl;
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
            view.mLightList.Clear();
            view.mLightList = Results.Test(view.scriptModelDictionary);
            view.UpdateData(view.mLightList);
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
            view.mBlockLightList = Results.Test(view.scriptModelDictionary,stepName); 
        }

        public Dictionary<String, ScriptModel> GetScriptModelDictionary(String filePath) {
            return Business.ViewBusiness.Currency.ScriptFileBusiness.GetScriptModelDictionary(filePath);
        }
    }
}
