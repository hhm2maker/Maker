using Maker.Business;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Windows;
using Maker.Model;
using System.Windows.Controls;
using System.IO;

namespace Maker.View.LightScriptUserControl
{
    public class BaseLightScriptUserControl : BaseMakerLightUserControl
    {
        public BaseLightScriptUserControl()
        {
            _fileExtension = ".lightScript";
            _fileType = "LightScript";
        }

        protected override void LoadFileContent()
        {
         
        }
    }
}
