using Maker.Business;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Windows;
using Maker.Model;
using System.Windows.Controls;
using System.IO;

namespace Maker.View.LightScriptWindow
{
    public class BaseLightScriptWindow : BaseWindow
    {
        public BaseLightScriptWindow()
        {
            _fileType = ".lightScript";
        }

        protected override List<Light> LoadFileContent()
        {
            return fileBusiness.ReadLightFile(filePath);
        }

    }
}
