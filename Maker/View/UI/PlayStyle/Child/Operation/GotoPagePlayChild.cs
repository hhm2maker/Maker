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

namespace Maker.View.UI.Style.Child
{
    public partial class GotoPagePlayChild : OperationStyle
    {
        public override string Title { get; set; } = "Play lights";
        private GotoPagePlayModel gotoPagePlayModel;
        public GotoPagePlayChild(GotoPagePlayModel gotoPagePlayModel)
        {
            this.gotoPagePlayModel = gotoPagePlayModel;
            //构建对话框
            tbPageName = GetTexeBox(gotoPagePlayModel.PageName);
            tbPageName.IsEnabled = false;
            AddTitleAndControl("GotoColon", tbPageName);

            GetButton("Replace", ReplaceLight, out Button btnReplace);
            GetButton("Remove", RemoveLight, out Button btnRemove);

            AddDockPanel(out DockPanel dp,btnReplace, btnRemove);
            dp.HorizontalAlignment = HorizontalAlignment.Center;

            CreateDialog();

            tbPageName.LostFocus += TbPolyploidy_LostFocus;
        }

        private void TbPolyploidy_LostFocus(object sender, RoutedEventArgs e)
        {
            gotoPagePlayModel.PageName = tbPageName.Text;
            NeedRefresh();
        }

        private void ReplaceLight(object sender, RoutedEventArgs e)
        {
            List<String> fileNames = new List<string>();
            fileNames.AddRange(FileBusiness.CreateInstance().GetFilesName(StaticConstant.mw.LastProjectPath + @"\Play", new List<string>() { ".lightPage" }));
            ShowLightListDialog dialog = new ShowLightListDialog(StaticConstant.mw, tbPageName.Text, fileNames);
            if (dialog.ShowDialog() == true)
            {
                if (tbPageName.Text.Equals(dialog.selectItem)) {
                    return;
                }
                tbPageName.Text = dialog.selectItem;
                gotoPagePlayModel.PageName = tbPageName.Text;
                NeedRefresh();
            }
        }

        private void RemoveLight(object sender, RoutedEventArgs e)
        {
            if (gotoPagePlayModel.PageName.Equals(String.Empty)) {
                return;
            }
            tbPageName.Text = "";
            gotoPagePlayModel.PageName = tbPageName.Text;
            NeedRefresh();
        }

        public TextBox tbPageName;
        public ComboBox cbOperation;

        public override bool ToSave() {
            if (tbPageName.Text.Equals(String.Empty))
            {
                tbPageName.Focus();
                return false;
            }
            else {
                return true;
            }
          
        }
    }
}
