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
        protected List<Run> runs = new List<Run>();
        protected TextBlock tbMain = null;
        protected TextBlock tbRight = null;
        protected TextBox tbEdit = null;

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

        SolidColorBrush solidNormal = (SolidColorBrush)StaticConstant.mw.Resources["Text_Normal"];
        SolidColorBrush solidOrange = (SolidColorBrush)StaticConstant.mw.Resources["Text_Orange"];
        SolidColorBrush solidGrey = (SolidColorBrush)StaticConstant.mw.Resources["Text_Grey"];
        SolidColorBrush solidGreyBg = (SolidColorBrush)StaticConstant.mw.Resources["Text_Grey_Bg"];
        SolidColorBrush solidPurple = (SolidColorBrush)StaticConstant.mw.Resources["Text_Purple"];
        SolidColorBrush solidBlue = (SolidColorBrush)StaticConstant.mw.Resources["Text_Blue"];


        List<RunModel> runModels;
        public List<Run> GetRuns(String title,String funType,TextBlock tbMain ,List<RunModel> runModels)
        {
            this.runModels = runModels;

            tbEdit.Visibility = Visibility.Collapsed;
            tbEdit.LostFocus += TbEdit_LostFocus;

            List<Run> runs = new List<Run>
            {
                new Run()
                {
                    Foreground = solidNormal,
                    Text = (string)Application.Current.FindResource(funType)+"."+(string)Application.Current.FindResource(title)+"( ",
                }
            };
            foreach (RunModel item in runModels)
            {
                runs.Add(new Run()
                {
                    Foreground = solidGrey,
                    Background = solidGreyBg,
                    Text = (string)Application.Current.FindResource(item.Title),
                });

                Run value = new Run()
                {
                    Foreground = solidPurple,
                    Text = item.Content,
                };
                runs.Add(value);

                if(item.Type == RunModel.RunType.Normal)
                {
                    value.Foreground = solidNormal;
                    value.MouseLeftButtonUp += Value_MouseLeftButtonUp;
                }
                else if (item.Type == RunModel.RunType.Calc)
                {
                    value.Foreground = solidBlue;
                    value.MouseLeftButtonUp += Value_MouseLeftButtonUp;
                }
                else if (item.Type == RunModel.RunType.File)
                {
                    //RunFileClass comboRunClass = new RunFileClass
                    //{
                    //    Data = (List<String>)item.Data,
                    //    Os = this,
                    //    Runs = runs,
                    //    RunCombo = value,
                    //    TbMain = tbMain,
                    //};
                    //value.MouseLeftButtonUp += comboRunClass.DrawRange;
                }
                else if (item.Type == RunModel.RunType.Show)
                {
                    value.Foreground = solidGrey;
                }

                if (item != runModels[runModels.Count - 1]) {
                    runs.Add(GetSplitRun());
                }
            }
            runs.Add(new Run()
            {
                Foreground = solidNormal,
                Text = ");",
            });
            return runs;
        }

     

        protected Run selectRun;
        private void Value_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int position = runs.IndexOf((Run)sender);

            if (position == -1) {
                return;
            }

            tbMain.Inlines.Clear();
            for (int i = 0; i < position; i++) {
                tbMain.Inlines.Add(runs[i]);
            }

            selectRun = runs[position];
            tbEdit.Visibility = Visibility.Visible;
            tbEdit.Text = selectRun.Text;
            tbEdit.Focus();
            for (int i = position+1; i < runs.Count; i++)
            {
                tbRight.Inlines.Add(runs[i]);
            }
        }

        private void TbEdit_LostFocus(object sender, RoutedEventArgs e)
        {
            selectRun.Text = (sender as TextBox).Text;
            tbEdit.Text = "";
            tbRight.Inlines.Clear();
            tbEdit.Visibility = Visibility.Collapsed;

            tbMain.Inlines.Clear();
            for (int i = 0; i < runs.Count; i++)
            {
                tbMain.Inlines.Add(runs[i]);
            }
            ToRefresh();
        }

        public Run GetSplitRun()
        {
            return new Run()
            {
                Foreground = solidOrange,
                Text = ", ",
            };
        }

        protected virtual List<RunModel> UpdateData()
        {
            return null;
        }

        protected virtual void RefreshView() {
            
        }

        public void ToUpdateData() {
            List<RunModel> runModel = UpdateData();
            if (runModel == null) {
                return;
            }
            ResetMainText(runModel);
        }

        private void ResetMainText(List<RunModel> runModel) {
            runs = GetRuns(Title, FunType.ToString(), tbMain, runModel);

            tbMain.Inlines.Clear();
            foreach (var item in runs)
            {
                tbMain.Inlines.Add(item);
            }
        }

        public void ToRefresh() {
            RefreshView();
        }

        /// <summary>
        /// 内容是否正确
        /// </summary>
        /// <param name="content"></param>
        /// <param name="split"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public List<int> GetTrueContent(String content, char split, char range)
        {
            try
            {
                List<int> nums = new List<int>();
                string[] strSplit = content.Split(split);
                for (int i = 0; i < strSplit.Length; i++)
                {
                    if (strSplit[i].Contains(range))
                    {
                        String[] TwoNumber = null;
                        TwoNumber = strSplit[i].Split(range);

                        int One = int.Parse(TwoNumber[0]);
                        int Two = int.Parse(TwoNumber[1]);
                        if (One < Two)
                        {
                            for (int k = One; k <= Two; k++)
                            {
                                nums.Add(k);
                            }
                        }
                        else if (One > Two)
                        {
                            for (int k = One; k >= Two; k--)
                            {
                                nums.Add(k);
                            }
                        }
                        else
                        {
                            nums.Add(One);
                        }
                    }
                    else
                    {
                        nums.Add(int.Parse(strSplit[i]));
                    }
                }
                return nums;
            }
            catch
            {
                return null;
            }
        }
    }
}
