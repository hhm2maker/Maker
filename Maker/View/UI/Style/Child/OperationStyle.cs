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

namespace Maker.View.UI.Style.Child
{
    public class OperationStyle : BaseStyle
    {
        public ScriptUserControl suc;


        public OperationStyle(ScriptUserControl suc) {
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

        private List<Light> myData = null;
        protected List<Light> MyData
        {
            get
            {
                if (myData == null)
                {
                    //StaticConstant.mw.projectUserControl.suc.Test(StaticConstant.mw.projectUserControl.suc.GetStepName(), StaticConstant.mw.projectUserControl.suc.sw.lbCatalog.SelectedIndex);
                    StaticConstant.mw.editUserControl.suc.Test(StaticConstant.mw.editUserControl.suc.GetStepName(), (Parent as Panel).Children.IndexOf(this));

                    List<int> times = Business.LightBusiness.GetTimeList(NowData);
                    int position = Convert.ToInt32(StaticConstant.mw.editUserControl.suc.tbTimePointCountLeft.Text) - 1;
                    myData = new List<Light>();
                    for (int i = 0; i < NowData.Count; i++)
                    {
                        if (NowData[i].Time == times[position])
                        {
                            myData.Add(new Light(NowData[i].Time, NowData[i].Action, NowData[i].Position, NowData[i].Color));
                        }
                    }

                    InitData();

                    //清除其他model的缓存数据
                    for (int i = 0; i < (Parent as Panel).Children.Count; i++)
                    {
                        if (i != (Parent as Panel).Children.IndexOf(this) && (Parent as Panel).Children[i] is OperationStyle)
                        {
                            ((Parent as Panel).Children[i] as OperationStyle).myData = null;
                        }
                    }
                }
                return myData;
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

        public override void ToRefresh() {
            RefreshView();
            suc.Test();
        }
    }
}
