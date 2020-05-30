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
using static Maker.View.UI.Style.Child.ConditionJudgmentOperationChild;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using Maker.View.LightScriptUserControl;
using Maker.View.UI.Style.Child.Base;

namespace Maker.View.UI.Style.Child
{
    public class OperationStyle : BaseStyle
    {
        public ScriptUserControl suc;
        protected List<Run> runs = new List<Run>();
        protected TextBlock tbMain = null;
        protected TextBlock tbRight = null;
        protected TextBox tbEdit = null;

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

        SolidColorBrush solidNormal = (SolidColorBrush)StaticConstant.mw.Resources["Text_Normal"];
        SolidColorBrush solidOrange = (SolidColorBrush)StaticConstant.mw.Resources["Text_Orange"];
        SolidColorBrush solidGrey = (SolidColorBrush)StaticConstant.mw.Resources["Text_Grey"];
        SolidColorBrush solidGreyBg = (SolidColorBrush)StaticConstant.mw.Resources["Text_Grey_Bg"];
        SolidColorBrush solidPurple = (SolidColorBrush)StaticConstant.mw.Resources["Text_Purple"];


        public class RunModel
        {
            public String Title
            {
                get;
                set;
            }
            public String Content
            {
                get;
                set;
            }

            public RunType Type
            {
                get;
                set;
            }

            public enum RunType
            {
                Normal,
                Position,
                Color,
                Combo,
            }

            public Object Data
            {
                get;
                set;
            }

            public RunModel() { }

            public RunModel(string title, string content)
            {
                Title = title;
                Content = content;
                Type = RunType.Normal;
            }

            public RunModel(string title, string content, RunType type)
            {
                Title = title;
                Content = content;
                Type = type;
            }

            public RunModel(string title, string content, RunType type,Object obj)
            {
                Title = title;
                Content = content;
                Type = type;
                Data = obj;
            }
        }

        List<RunModel> runModels;
        public List<Run> GetRuns(TextBlock tbMain ,List<RunModel> runModels)
        {
            this.runModels = runModels;

            tbEdit.Visibility = Visibility.Collapsed;
            tbEdit.LostFocus += TbEdit_LostFocus;

            List<Run> runs = new List<Run>
            {
                new Run()
                {
                    Foreground = solidNormal,
                    Text = "Create.FromQuick( ",
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

                if (item.Type == RunModel.RunType.Position)
                {
                    RunPositionClass comboRunClass = new RunPositionClass
                    {
                        Data = (List<String>)item.Data,
                        Os = this,
                        RunCombo = value,
                        TbMain = tbMain,
                    };
                    value.MouseLeftButtonUp += comboRunClass.DrawRange;
                }
                else if (item.Type == RunModel.RunType.Color)
                {
                    RunColorClass comboRunClass = new RunColorClass
                    {
                        Data = (List<String>)item.Data,
                        Os = this,
                        RunCombo = value,
                        TbMain = tbMain,
                    };
                    value.MouseLeftButtonUp += comboRunClass.DrawRange;
                }
                else if (item.Type == RunModel.RunType.Combo)
                {
                    RunComboClass comboRunClass = new RunComboClass
                    {
                        Data = (List<String>)item.Data,
                        Os = this,
                        RunCombo = value,
                        TbMain = tbMain,
                    };
                    value.MouseLeftButtonUp += comboRunClass.DrawRange;
                }
                else if(item.Type == RunModel.RunType.Normal)
                {
                    value.Foreground = solidNormal;
                    value.MouseLeftButtonUp += Value_MouseLeftButtonUp;
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

        protected virtual void RefreshView() {
            
        }

        public void ToRefresh() {
            RefreshView();
            suc.Test();
        }
    }
}
