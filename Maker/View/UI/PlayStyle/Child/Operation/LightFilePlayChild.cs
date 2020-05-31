using Maker.Business;
using Maker.Business.Model.OperationModel;
using Maker.Model;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Maker.View.LightScriptUserControl;

namespace Maker.View.UI.Style.Child
{
    public partial class LightFilePlayChild : OperationStyle
    {
        public override string Title { get; set; } = "Play lights";
        private LightFilePlayModel lightFilePlayModel;
        public LightFilePlayChild(LightFilePlayModel lightFilePlayModel, ScriptUserControl suc):base(suc)
        {
            this.lightFilePlayModel = lightFilePlayModel;
            //构建对话框
            tbFileName = GetTexeBox(lightFilePlayModel.FileName);
            tbFileName.IsEnabled = false;
            AddTitleAndControl("FileNameColon", tbFileName);

            GetButton("Replace", ReplaceLight, out Button btnReplace);
            GetButton("Remove", RemoveLight, out Button btnRemove);

            AddDockPanel(out DockPanel dp,btnReplace, btnRemove);
            dp.HorizontalAlignment = HorizontalAlignment.Center;

            tbBpm = GetTexeBox(lightFilePlayModel.Bpm.ToString());
            AddTitleAndControl("BPMColon", tbBpm);

            CreateDialog();

            tbFileName.LostFocus += TbPolyploidy_LostFocus;
            tbBpm.LostFocus += TbPolyploidy_LostFocus2;
        }

        private void TbPolyploidy_LostFocus(object sender, RoutedEventArgs e)
        {
            lightFilePlayModel.FileName = tbFileName.Text;
            NeedRefresh();
        }

        private void TbPolyploidy_LostFocus2(object sender, RoutedEventArgs e)
        {
            if (Double.TryParse(tbBpm.Text, out double multiple))
            {
                lightFilePlayModel.Bpm = multiple;
            }
            NeedRefresh();
        }

        private void ReplaceLight(object sender, RoutedEventArgs e)
        {
            List<String> fileNames = new List<string>();
            fileNames.AddRange(FileBusiness.CreateInstance().GetFilesName(StaticConstant.mw.LastProjectPath + @"\Light", new List<string>() { ".light" }));
            fileNames.AddRange(FileBusiness.CreateInstance().GetFilesName(StaticConstant.mw.LastProjectPath + @"\LightScript", new List<string>() { ".lightScript" }));
            fileNames.AddRange(FileBusiness.CreateInstance().GetFilesName(StaticConstant.mw.LastProjectPath + @"\Midi", new List<string>() { ".mid" }));
            ShowLightListDialog dialog = new ShowLightListDialog(StaticConstant.mw, tbFileName.Text, fileNames);
            if (dialog.ShowDialog() == true)
            {
                if (tbFileName.Text.Equals(dialog.selectItem)) {
                    return;
                }
                tbFileName.Text = dialog.selectItem;
                lightFilePlayModel.FileName = tbFileName.Text;
                NeedRefresh();
            }
        }

        private void RemoveLight(object sender, RoutedEventArgs e)
        {
            if (lightFilePlayModel.FileName.Equals(String.Empty)) {
                return;
            }
            tbFileName.Text = "";
            lightFilePlayModel.FileName = tbFileName.Text;
            NeedRefresh();
        }

        public TextBox tbFileName;
        public TextBox tbBpm;
        public ComboBox cbOperation;

        public override bool ToSave() {
            if (tbFileName.Text.Equals(String.Empty))
            {
                tbFileName.Focus();
                return false;
            }
            if (tbBpm.Text.Equals(String.Empty))
            {
                tbBpm.Focus();
                return false;
            }
            if (Double.TryParse(tbBpm.Text, out double bpm))
            {
                lightFilePlayModel.Bpm = bpm;
                return true;
            }
            else
            {
                tbBpm.Focus();
                return false;
            }
        }
    }
}
