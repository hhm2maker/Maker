using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Maker.View.Style.Child;
using Maker.Model;
using Maker.Business;
using Operation;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using Maker.View.LightScriptUserControl;
using Maker.View.UI.Style.Child.Base;
using Maker.View.Play;

namespace Maker.View.UI.Style.Child
{
    public class ExportStyle : BaseStyle
    {
        public PlayExportUserControl suc;
   

        public ExportStyle(PlayExportUserControl suc) {
            this.suc = suc;
        }

        public void ToCreate() {
            //构建基础控件
            tbMain = GetTexeBlockNoBorder("", false);
            tbMain.FontSize = 18;
            tbMain.TextWrapping = TextWrapping.Wrap;

            tbRight = GetTexeBlockNoBorder("", false);
            tbRight.FontSize = 18;
            tbRight.TextWrapping = TextWrapping.Wrap;

            tbEdit = GetTexeBox("");
            tbEdit.FontSize = 18;
            tbEdit.TextWrapping = TextWrapping.Wrap;

            ToUpdateData();

            AddDockPanel(out DockPanel dp, tbMain, tbEdit, tbRight);

            CreateDialog();
        }


        protected List<Light> NowData
        {
            get
            {
                return StaticConstant.mw.editUserControl.suc.mLaunchpadData;
            }
        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        protected virtual void InitData() { }

        public virtual void Refresh()
        {
            StaticConstant.mw.editUserControl.suc.spMyHint.Visibility = Visibility.Visible;
            StaticConstant.mw.editUserControl.suc.spRefresh.Visibility = Visibility.Visible;
        }

        public void NeedRefresh()
        {
            StaticConstant.mw.editUserControl.suc.spRefresh.Visibility = Visibility.Visible;
        }

        public virtual bool ToSave()
        {
            return true;
        }

        public override void ToRefresh()
        {
            RefreshView();
            suc.Test();
        }
    }
}
