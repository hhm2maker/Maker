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
    public class BaseLightScriptUserControl : BaseUserControl
    {
        public BaseLightScriptUserControl()
        {
            _fileType = ".lightScript";
        }

        protected override void LoadFileContent()
        {
         
        }
    }
}
