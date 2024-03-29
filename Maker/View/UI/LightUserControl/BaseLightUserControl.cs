﻿using Maker.Business;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Windows;
using Maker.Model;
using System.Windows.Controls;
using System.IO;
using Operation;

namespace Maker.View.LightUserControl
{
    public class BaseLightUserControl : BaseMakerLightUserControl
    {
        public BaseLightUserControl()
        {
            _fileExtension = ".light";
            _fileType = "Light";
        }

        private List<Light> lightList;
        protected override void  LoadFileContent()
        {
            if (filePath.EndsWith(".light")) {
                lightList = Operation.FileBusiness.CreateInstance().ReadLightFile(filePath);
            }
            else
            {
                lightList = Operation.FileBusiness.CreateInstance().ReadMidiFile(filePath);
            }

            SetData(lightList);
            if (spHint != null)
            {
                spHint.Visibility = Visibility.Collapsed;
                mainView.Children[0].Visibility = Visibility.Visible;
            }
        }

        protected override void BaseLightWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            base.BaseLightWindow_Closing(sender,e);
            if (!filePath.Equals(String.Empty))
                SaveFile();
        }

        public override void SaveFile()
        {
            if (filePath.EndsWith(".light"))
            {
                Operation.FileBusiness.CreateInstance().WriteLightFile(filePath, GetData());
            }
            else
            {
                Operation.FileBusiness.CreateInstance().WriteMidiFile(filePath, GetData());
            }
        }

   
    }
}
