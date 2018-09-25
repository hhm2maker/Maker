using Maker.Business;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Windows;
using Maker.Model;
using System.Windows.Controls;
using System.IO;
using Maker.View.LightWindow;

namespace Maker.View.LightUserControl
{
    public class BaseLightUserControl : BaseUserControl, IUserControl
    {
        public BaseLightUserControl()
        {
            _fileExtension = ".light";
            _fileType = "Light";
        }

        private List<Light> lightList;
        protected override void  LoadFileContent()
        {
            lightList = fileBusiness.ReadLightFile(filePath);
        }

        protected override void BaseLightWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            base.BaseLightWindow_Closing(sender,e);
            if (!filePath.Equals(String.Empty))
                SaveFile();
        }

        public override void LoadFile()
        {
            LoadFileContent();
            SetData(lightList);
            if(spHint != null) {
            spHint.Visibility = Visibility.Collapsed;
            mainView.Children[0].Visibility = Visibility.Visible;
            }
        }

        protected override void SaveFile()
        {
            fileBusiness.WriteLightFile(filePath, GetData());
        }

        public virtual void SetData(List<Light> lightList) { }

        public virtual List<Light> GetData() { return null; }


    }
}
