using Maker.Bridge;
using Maker.Business;
using Maker.Commands;
using Maker.Model;
using Maker.View;
using Maker.View.Control;
using Maker.View.Dialog;
using Maker.View.Dialog.Automatic;
using Maker.View.Dialog.Script;
using Maker.View.Style;
using Maker.ViewBusiness;
using Operation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Maker.Model.EnumCollection;

namespace Maker
{
    /// <summary>
    /// InputUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class InputUserControl : UserControl
    {
       
        public MainWindow mw;
        public InputUserControlBridge bridge;
        public InputUserControl(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            //绑定命令
            CommandBinding binding = new CommandBinding(DataCommands.Editcommand);
            binding.Executed += Binding_Executed;
            binding.CanExecute += Binding_CanExecute;
            //左侧图片
            miColor.CommandBindings.Add(binding);
            miShape.CommandBindings.Add(binding);
            miTime.CommandBindings.Add(binding);
            miSuperposition.CommandBindings.Add(binding);
            miOther.CommandBindings.Add(binding);
            miAnimation.CommandBindings.Add(binding);
            miThirdParty.CommandBindings.Add(binding);
            //按钮
            btnSelectEditorReplace.CommandBindings.Add(binding);
            btnSelectEditorAdd.CommandBindings.Add(binding);
            tbIfThenReplace.CommandBindings.Add(binding);
            tbIfThenRemove.CommandBindings.Add(binding);

            //CommandManager.InvalidateRequerySuggested();

            //bridge = new InputUserControlBridge(this);
            //viewBusiness = new InputUserControlViewBusiness(this);
            //加载库文件
            bridge.InitLibrary(bridge.GetLibrary(), LibraryMenuItem_Click);

            //初始化控件
            InitView();

            //放置在底部
            //for (int i = spMainLeft.Children.Count - 1; i >= 0; i--)
            //{
            //    UIElement u = spMainLeft.Children[i] as UIElement;
            //    spMainLeft.Children.RemoveAt(i);
            //    spMainBottom.Children.Insert(0, u);
            //}
            //StackPanel sp = spMainBottom.Children[1] as StackPanel;
            //sp.Orientation = Orientation.Horizontal;

            //for (int i = 0; i < sp.Children.Count; i++)
            //{
            //    StackPanel u = sp.Children[i] as StackPanel;
            //    u.Margin = new Thickness(10, 10, 20, u.Margin.Bottom);
            //}
            //svMainLeft.Width = 0;
            //svMainBottom.Visibility = Visibility.Visible;
        }

        private void InitView()
        {
            lbColor.HideText();
            lbColor.ToSmall();

            AddStepControlToolTip();
        }


        private void Binding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (lbStep.SelectedIndex != -1);
        }

        private void Binding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            HideAllPopup();
            if (sender == miColor)
            {
                popColor.IsOpen = false;
                popColor.IsOpen = true;
            }
            if (sender == miShape)
            {
                popShape.IsOpen = false;
                popShape.IsOpen = true;
            }
            if (sender == miTime)
            {
                popTime.IsOpen = false;
                popTime.IsOpen = true;
            }
            if (sender == miSuperposition)
            {
                popSuperposition.IsOpen = false;
                popSuperposition.IsOpen = true;
            }
            if (sender == miAnimation)
            {
                popAnimation.IsOpen = false;
                popAnimation.IsOpen = true;
            }
            if (sender == miOther)
            {
                popOther.IsOpen = false;
                popOther.IsOpen = true;
            }
            if (sender == miThirdParty)
            {
                popThirdParty.IsOpen = false;
                popThirdParty.IsOpen = true;
            }
            if (sender == btnSelectEditorReplace || sender == btnSelectEditorAdd || sender == tbIfThenReplace || sender == tbIfThenRemove)
            {
                ToLightScript(sender);
            }
            e.Handled = true;
        }

        public Dictionary<string, List<int>> rangeDictionary = new Dictionary<string, List<int>>();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromArgb(255, 40, 40, 40)));
            LoadRangeFile();
            Width = ((DockPanel)Parent).ActualWidth;

            foreach (var item in lbColor.Items)
            {
                (item as ListBoxItem).MouseLeftButtonUp += InputUserControl_MouseLeftButtonUp;
            }
        }
        private void LoadRangeFile()
        {
            bridge.LoadRangeFile();
        }
        public Dictionary<String, String> lightScriptDictionary = new Dictionary<string, string>();
        public Dictionary<String, bool> visibleDictionary = new Dictionary<String, bool>();
        public Dictionary<String, List<String>> containDictionary = new Dictionary<String, List<String>>();
        public Dictionary<String, List<String>> extendsDictionary = new Dictionary<string, List<String>>();
        public List<String> importList = new List<String>();
        public Dictionary<String, String> finalDictionary = new Dictionary<string, string>();
        public Dictionary<String, List<String>> intersectionDictionary = new Dictionary<string, List<String>>();
        public Dictionary<String, List<String>> complementDictionary = new Dictionary<string, List<String>>();
        public Dictionary<String, List<Light>> lockedDictionary = new Dictionary<string, List<Light>>();
        public String introduceText;
        /// <summary>
        /// 更新步骤列表的父类
        /// </summary>
        public void UpdateExtends()
        {
            for (int i = 0; i < lbStep.Items.Count; i++)
            {
                bool isHaveFather = false;
                StackPanel sp = (StackPanel)lbStep.Items[i];
                TextBlock blockStepName = (TextBlock)sp.Children[1];
                foreach (var item in extendsDictionary)
                {
                    if (item.Value.Contains(blockStepName.Text))
                    {
                        TextBlock blockParentName = (TextBlock)sp.Children[3];
                        blockParentName.Text = item.Key;
                        isHaveFather = true;
                        break;
                    }
                }
                if (!isHaveFather)
                {
                    TextBlock blockParentName = (TextBlock)sp.Children[3];
                    blockParentName.Text = "";
                }
            }
        }
        /// <summary>
        /// 更新交集集合关系
        /// </summary>
        public void UpdateIntersection()
        {
            List<String> ls = GetStepNameCollection();
            for (int i = 0; i < ls.Count; i++)
            {
                StackPanel sp = (StackPanel)lbStep.Items[i];
                DockPanel panel = (DockPanel)sp.Children[0];
                foreach (var id in intersectionDictionary)
                {
                    //如果是子集
                    if (id.Value.Contains(ls[i]))
                    {
                        //panel.Margin = new Thickness(30,0,0,0);
                        Image visibleImage = (Image)panel.Children[0];
                        visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/branch.png", UriKind.RelativeOrAbsolute));
                        break;
                    }
                }
                //if (!isCollection)
                //{
                //    TextBlock blockStepName = (TextBlock)sp.Children[1];
                //    if (visibleDictionary[blockStepName.Text])
                //    {
                //        Image visibleImage = (Image)panel.Children[0];
                //        visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/visible.png", UriKind.RelativeOrAbsolute));
                //    }
                //    else
                //    {
                //        Image visibleImage = (Image)panel.Children[0];
                //        visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/novisible.png", UriKind.RelativeOrAbsolute));
                //    }
                //}
            }
        } 
        /// <summary>
           /// 更新补集集合关系
           /// </summary>
        public void UpdateComplement()
        {
            List<String> ls = GetStepNameCollection();
            for (int i = 0; i < ls.Count; i++)
            {
                StackPanel sp = (StackPanel)lbStep.Items[i];
                DockPanel panel = (DockPanel)sp.Children[0];
                foreach (var id in complementDictionary)
                {
                    //如果是子集
                    if (id.Value.Contains(ls[i]))
                    {
                        //panel.Margin = new Thickness(30,0,0,0);
                        Image visibleImage = (Image)panel.Children[0];
                        visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/branch.png", UriKind.RelativeOrAbsolute));
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 更新步骤可见或不可见
        /// </summary>
        public void UpdateVisible()
        {
            List<String> ls = GetStepNameCollection();
            for (int i = 0; i < lbStep.Items.Count; i++)
            {
                bool isMatch = false;
                foreach (var id in intersectionDictionary)
                {
                    if (id.Value.Contains(ls[i]))
                    {
                        isMatch = true;
                        break;
                    }
                }
                foreach (var id in complementDictionary)
                {
                    if (id.Value.Contains(ls[i]))
                    {
                        isMatch = true;
                        break;
                    }
                }
                //如果是子集
                if (isMatch)
                    continue;

                StackPanel sp = (StackPanel)lbStep.Items[i];
                TextBlock blockStepName = (TextBlock)sp.Children[1];
                if (visibleDictionary[blockStepName.Text])
                {
                    DockPanel panel = (DockPanel)sp.Children[0];
                    Image visibleImage = (Image)panel.Children[0];
                    visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/visible.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    DockPanel panel = (DockPanel)sp.Children[0];
                    Image visibleImage = (Image)panel.Children[0];
                    visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/novisible.png", UriKind.RelativeOrAbsolute));
                }
            }
            //根据父类可见不可见，来改变子类可见不可见
            for (int i = 0; i < lbStep.Items.Count; i++)
            {
                StackPanel sp = (StackPanel)lbStep.Items[i];
                TextBlock blockStepName = (TextBlock)sp.Children[1];
                TextBlock blockParentName = (TextBlock)sp.Children[3];

                if (!blockParentName.Text.Equals(String.Empty))
                {
                    //如果父类不可见
                    if (!visibleDictionary[blockParentName.Text])
                    {
                        DockPanel panel = (DockPanel)sp.Children[0];
                        Image visibleImage = (Image)panel.Children[0];
                        visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/novisible.png", UriKind.RelativeOrAbsolute));
                        visibleDictionary[blockStepName.Text] = false;
                    }
                }
            }
        }
        /// <summary>
        /// 更新锁定
        /// </summary>
        public void UpdateLocked()
        {
            for (int i = 0; i < lbStep.Items.Count; i++)
            {
                StackPanel sp = (StackPanel)lbStep.Items[i];
                DockPanel panel = (DockPanel)sp.Children[4];
                Image lockedImage = (Image)panel.Children[0];
                //如果列表包含该步骤名
                if (lockedDictionary.ContainsKey(GetStepName(i)))
                {
                    //加上锁的图标
                    lockedImage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/locked.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    lockedImage.Source = null;
                }
            }
        }
        private void ToLightScript(object sender, RoutedEventArgs e)
        {
            ToLightScript(sender);
        }

        private void ToLightScript(object sender)
        {
            String stepName = GetUsableStepName();
            if (stepName == null)
            {
                new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                return;
            }
            String commandLine = String.Empty;
            //如果是快速生成 - 添加
            if (sender == btnFastGenerationrAdd || sender == btnFastGenerationrSelect)
            {
                char splitNotation = ',';
                if (mw.strInputFormatDelimiter.Equals("Comma"))
                {
                    splitNotation = ',';
                }
                else if (mw.strInputFormatDelimiter.Equals("Space"))
                {
                    splitNotation = ' ';
                }
                char rangeNotation = '-';
                if (mw.strInputFormatRange.Equals("Shortbar"))
                {
                    rangeNotation = '-';
                }
                else if (mw.strInputFormatRange.Equals("R"))
                {
                    rangeNotation = 'r';
                }
                StringBuilder fastGenerationrRangeBuilder = new StringBuilder();
                if (rangeDictionary.ContainsKey(tbFastGenerationrRange.Text))
                {
                    for (int i = 0; i < rangeDictionary[tbFastGenerationrRange.Text].Count; i++)
                    {
                        if (i != rangeDictionary[tbFastGenerationrRange.Text].Count - 1)
                        {
                            fastGenerationrRangeBuilder.Append(rangeDictionary[tbFastGenerationrRange.Text][i] + splitNotation.ToString());
                        }
                        else
                        {
                            fastGenerationrRangeBuilder.Append(rangeDictionary[tbFastGenerationrRange.Text][i]);
                        }
                    }
                }
                else
                {
                    if (IsTrueContent(tbFastGenerationrRange.Text, splitNotation, rangeNotation))
                    {
                        fastGenerationrRangeBuilder.Append(tbFastGenerationrRange.Text);
                    }
                    else
                    {
                        tbFastGenerationrRange.Select(0, tbFastGenerationrRange.Text.Length);
                        tbFastGenerationrRange.Focus();
                        return;
                    }
                }
                StringBuilder fastGenerationrColorBuilder = new StringBuilder();
                if (rangeDictionary.ContainsKey(tbFastGenerationrColor.Text))
                {
                    for (int i = 0; i < rangeDictionary[tbFastGenerationrColor.Text].Count; i++)
                    {
                        if (i != rangeDictionary[tbFastGenerationrColor.Text].Count - 1)
                        {
                            fastGenerationrColorBuilder.Append(rangeDictionary[tbFastGenerationrColor.Text][i] + splitNotation.ToString());
                        }
                        else
                        {
                            fastGenerationrColorBuilder.Append(rangeDictionary[tbFastGenerationrColor.Text][i]);
                        }
                    }
                }
                else
                {
                    if (IsTrueContent(tbFastGenerationrColor.Text, splitNotation, rangeNotation))
                    {
                        fastGenerationrColorBuilder.Append(tbFastGenerationrColor.Text);
                    }
                    else
                    {
                        tbFastGenerationrColor.Select(0, tbFastGenerationrColor.Text.Length);
                        tbFastGenerationrColor.Focus();
                        return;
                    }
                }
                object result = null;
                try
                {
                    string expression = tbFastGenerationrTime.Text;
                    System.Data.DataTable eval = new System.Data.DataTable();
                    result = eval.Compute(expression, "");
                }
                catch
                {
                    tbFastGenerationrTime.Select(0, tbFastGenerationrTime.Text.Length);
                    tbFastGenerationrTime.Focus();
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(tbFastGenerationrInterval.Text, "^\\d+$"))
                {
                    tbFastGenerationrInterval.Select(0, tbFastGenerationrInterval.Text.Length);
                    tbFastGenerationrInterval.Focus();
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(tbFastGenerationrContinued.Text, "^\\d+$"))
                {
                    tbFastGenerationrContinued.Select(0, tbFastGenerationrContinued.Text.Length);
                    tbFastGenerationrContinued.Focus();
                    return;
                }
                commandLine =
                    "\tRangeGroup " + stepName + "Range = new RangeGroup(\""
                    + fastGenerationrRangeBuilder.ToString() + "\",'" + splitNotation + "','" + rangeNotation + "');" + Environment.NewLine
                    + "\tColorGroup " + stepName + "Color = new ColorGroup(\""
                    + fastGenerationrColorBuilder.ToString() + "\",'" + splitNotation + "','" + rangeNotation + "');" + Environment.NewLine
                    + "\tLightGroup " + stepName + "LightGroup = Create.CreateLightGroup("
                    + result + ","
                      + stepName + "Range,"
                        + tbFastGenerationrInterval.Text + ","
                          + tbFastGenerationrContinued.Text + ","
                               + stepName + "Color";
                //Type
                if (cbFastGenerationrType.SelectedIndex == -1)
                    return;
                if (cbFastGenerationrType.SelectedIndex == 0)
                {
                    commandLine += ",Up";
                }
                else if (cbFastGenerationrType.SelectedIndex == 1)
                {
                    commandLine += ",Down";
                }
                else if (cbFastGenerationrType.SelectedIndex == 2)
                {
                    commandLine += ",UpDown";
                }
                else if (cbFastGenerationrType.SelectedIndex == 3)
                {
                    commandLine += ",DownUp";
                }
                else if (cbFastGenerationrType.SelectedIndex == 4)
                {
                    commandLine += ",UpAndDown";
                }
                else if (cbFastGenerationrType.SelectedIndex == 5)
                {
                    commandLine += ",DownAndUp";
                }
                else if (cbFastGenerationrType.SelectedIndex == 6)
                {
                    commandLine += ",FreezeFrame";
                }
                //Action
                if (cbFastGenerationrAction.SelectedIndex == -1)
                    return;
                if (cbFastGenerationrAction.SelectedIndex == 0)
                {
                    commandLine += ",All);";
                }
                else if (cbFastGenerationrAction.SelectedIndex == 1)
                {
                    commandLine += ",Open);";
                }
                else if (cbFastGenerationrAction.SelectedIndex == 2)
                {
                    commandLine += ",Close);";
                }
                //有BUG，已添加了具有相同键的项。 错误处理有问题
                lightScriptDictionary.Add(stepName, commandLine);
                visibleDictionary.Add(stepName, true);
                containDictionary.Add(stepName, new List<string>() { stepName });
            }
            if (sender == btnSelectEditorReplace)
            {
                //修改属性
                if (lbStep.SelectedIndex == -1)
                {
                    return;
                }
                if (lockedDictionary.ContainsKey(GetStepName()))
                {
                    new MessageDialog(mw, "TheStepIsLocked").ShowDialog();
                    return;
                }
                if (!tbSelectEditorTime.Text.Trim().Equals(String.Empty))
                {
                    try
                    {
                        String result;
                        if (tbSelectEditorTime.Text.Trim()[0] == '+' || tbSelectEditorTime.Text.Trim()[0] == '-')
                        {
                            //计算数学表达式
                            string expression = tbSelectEditorTime.Text.Substring(1);
                            System.Data.DataTable eval = new System.Data.DataTable();
                            result = eval.Compute(expression, "").ToString();
                            result = tbSelectEditorTime.Text.Trim()[0] + result;
                        }
                        else
                        {
                            //计算数学表达式
                            string expression = tbSelectEditorTime.Text;
                            System.Data.DataTable eval = new System.Data.DataTable();
                            result = eval.Compute(expression, "").ToString();
                        }
                        lightScriptDictionary[GetStepName()] += Environment.NewLine + "\t" + GetStepName() + "LightGroup.SetAttribute(Time," + result + ");";
                    }
                    catch
                    {
                        tbSelectEditorTime.Select(0, tbSelectEditorTime.Text.Length);
                        tbSelectEditorTime.Focus();
                        return;
                    }
                }
                if (!tbSelectEditorPosition.Text.Trim().Equals(String.Empty))
                {
                    String strNumber = tbSelectEditorPosition.Text.Trim();
                    if (strNumber[0] == '+' || strNumber[0] == '-')
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber.Substring(1), "^\\d+$"))
                        {
                            tbSelectEditorPosition.Select(0, tbSelectEditorPosition.Text.Length);
                            tbSelectEditorPosition.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber, "^\\d+$"))
                        {
                            tbSelectEditorPosition.Select(0, tbSelectEditorPosition.Text.Length);
                            tbSelectEditorPosition.Focus();
                            return;
                        }
                    }
                    lightScriptDictionary[GetStepName()] += Environment.NewLine + "\t" + GetStepName() + "LightGroup.SetAttribute(Position," + tbSelectEditorPosition.Text.Trim() + ");";
                }
                if (!tbSelectEditorColor.Text.Trim().Equals(String.Empty))
                {
                    String strNumber = tbSelectEditorColor.Text.Trim();
                    if (strNumber[0] == '+' || strNumber[0] == '-')
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber.Substring(1), "^\\d+$"))
                        {
                            tbSelectEditorColor.Select(0, tbSelectEditorColor.Text.Length);
                            tbSelectEditorColor.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber, "^\\d+$"))
                        {
                            tbSelectEditorColor.Select(0, tbSelectEditorColor.Text.Length);
                            tbSelectEditorColor.Focus();
                            return;
                        }
                    }
                    lightScriptDictionary[GetStepName()] += Environment.NewLine + "\t" + GetStepName() + "LightGroup.SetAttribute(Color," + tbSelectEditorColor.Text.Trim() + ");";
                }
            }
            if (sender == btnSelectEditorAdd)
            {
                //复制并修改
                if (lbStep.SelectedIndex == -1)
                {
                    return;
                }
                commandLine = "\tLightGroup " + stepName + "LightGroup = Parent;" + Environment.NewLine;
                if (!tbSelectEditorTime.Text.Trim().Equals(String.Empty))
                {
                    try
                    {
                        String result;
                        if (tbSelectEditorTime.Text.Trim()[0] == '+' || tbSelectEditorTime.Text.Trim()[0] == '-')
                        {
                            //计算数学表达式
                            string expression = tbSelectEditorTime.Text.Substring(1);
                            System.Data.DataTable eval = new System.Data.DataTable();
                            result = eval.Compute(expression, "").ToString();
                            result = tbSelectEditorTime.Text.Trim()[0] + result;
                        }
                        else
                        {
                            //计算数学表达式
                            string expression = tbSelectEditorTime.Text;
                            System.Data.DataTable eval = new System.Data.DataTable();
                            result = eval.Compute(expression, "").ToString();
                        }
                        commandLine += "\t" + stepName + "LightGroup.SetAttribute(Time," + result + ");";
                    }
                    catch
                    {
                        tbSelectEditorTime.Select(0, tbSelectEditorTime.Text.Length);
                        tbSelectEditorTime.Focus();
                        return;
                    }
                }
                if (!tbSelectEditorPosition.Text.Trim().Equals(String.Empty))
                {
                    String strNumber = tbSelectEditorPosition.Text.Trim();
                    if (strNumber[0] == '+' || strNumber[0] == '-')
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber.Substring(1), "^\\d+$"))
                        {
                            tbSelectEditorPosition.Select(0, tbSelectEditorPosition.Text.Length);
                            tbSelectEditorPosition.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber, "^\\d+$"))
                        {
                            tbSelectEditorPosition.Select(0, tbSelectEditorPosition.Text.Length);
                            tbSelectEditorPosition.Focus();
                            return;
                        }
                    }
                    commandLine += "\t" + stepName + "LightGroup.SetAttribute(Position," + tbSelectEditorPosition.Text.Trim() + ");";
                }
                if (!tbSelectEditorColor.Text.Trim().Equals(String.Empty))
                {
                    String strNumber = tbSelectEditorColor.Text.Trim();
                    if (strNumber[0] == '+' || strNumber[0] == '-')
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber.Substring(1), "^\\d+$"))
                        {
                            tbSelectEditorColor.Select(0, tbSelectEditorColor.Text.Length);
                            tbSelectEditorColor.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(strNumber, "^\\d+$"))
                        {
                            tbSelectEditorColor.Select(0, tbSelectEditorColor.Text.Length);
                            tbSelectEditorColor.Focus();
                            return;
                        }
                    }
                    commandLine += "\t" + stepName + "LightGroup.SetAttribute(Color," + tbSelectEditorColor.Text.Trim() + ");";
                }
                lightScriptDictionary.Add(stepName, commandLine);
                visibleDictionary.Add(stepName, true);
                containDictionary.Add(stepName, new List<string>() { stepName });
                if (extendsDictionary.ContainsKey(GetStepName()))
                {
                    extendsDictionary[GetStepName()].Add(stepName);
                }
                else
                {
                    extendsDictionary.Add(GetStepName(), new List<string>() { stepName });
                }
            }
            if (sender == tbIfThenReplace || sender == tbIfThenRemove)
            {
                if (lbStep.SelectedIndex == -1)
                {
                    return;
                }
                if (lockedDictionary.ContainsKey(GetStepName())) {
                    new MessageDialog(mw, "TheStepIsLocked").ShowDialog();
                    return;
                }
                char splitNotation = ',';
                if (mw.strInputFormatDelimiter.Equals("Comma"))
                {
                    splitNotation = ',';
                }
                else if (mw.strInputFormatDelimiter.Equals("Space"))
                {
                    splitNotation = ' ';
                }

                char rangeNotation = '-';
                if (mw.strInputFormatRange.Equals("Shortbar"))
                {
                    rangeNotation = '-';
                }
                else if (mw.strInputFormatRange.Equals("R"))
                {
                    rangeNotation = 'r';
                }
                if (!lightScriptDictionary[GetStepName()].Contains(GetStepName() + "LightGroup"))
                {
                    return;
                }
                String control = String.Empty;
                String ifPrerequisite = String.Empty;
                String ifPrerequisiteBottom = String.Empty;
                if (sender == tbIfThenReplace)
                    control = "Edit";
                else
                    control = "Remove";

                for (int j = 0; j < 4; j++)
                {
                    int x = 1;
                    while (x <= 100000)
                    {
                        if (!containDictionary[GetStepName()].Contains("Step" + x))
                        {
                            //不存在重复
                            break;
                        }
                        x++;
                    }
                    if (x > 100000)
                    {
                        new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                        return;
                    }
                    if (j == 0 && !tbIfTime.Text.Equals(String.Empty))
                    {
                        try
                        {
                            string expression = tbIfTime.Text;
                            System.Data.DataTable eval = new System.Data.DataTable();
                            object result = eval.Compute(expression, "");

                            ifPrerequisite = Environment.NewLine + "\tRangeGroup " + "step" + x.ToString() + "Range = new RangeGroup(\""
                             + result + "\",'" + splitNotation + "','" + rangeNotation + "');";
                            containDictionary[GetStepName()].Add("Step" + x);
                            ifPrerequisiteBottom = "Time:step" + x.ToString() + "Range";
                        }
                        catch
                        {
                            tbIfTime.Select(0, tbIfTime.Text.Length);
                            tbIfTime.Focus();
                            return;
                        }
                    }
                    if (j == 1 && cbIfAction.SelectedIndex != 0)
                    {
                        StringBuilder ifActionBuilder = new StringBuilder();
                        if (ifPrerequisiteBottom.Equals(String.Empty))
                        {
                            if (cbIfAction.SelectedIndex == 1)
                            {
                                ifPrerequisiteBottom = "Action:Open";
                            }
                            else if (cbIfAction.SelectedIndex == 2)
                            {
                                ifPrerequisiteBottom = "Action:Close";
                            }
                        }
                        else
                        {
                            if (cbIfAction.SelectedIndex == 1)
                            {
                                ifPrerequisiteBottom += "&&Action:Open";
                            }
                            else if (cbIfAction.SelectedIndex == 2)
                            {
                                ifPrerequisiteBottom += "&&Action:Close";
                            }
                        }
                    }
                    if (j == 2 && !tbIfPosition.Text.Equals(String.Empty))
                    {
                        StringBuilder ifPositionBuilder = new StringBuilder();
                        if (rangeDictionary.ContainsKey(tbIfPosition.Text))
                        {
                            for (int i = 0; i < rangeDictionary[tbIfPosition.Text].Count; i++)
                            {
                                if (i != rangeDictionary[tbIfPosition.Text].Count - 1)
                                {
                                    ifPositionBuilder.Append(rangeDictionary[tbIfPosition.Text][i] + splitNotation.ToString());
                                }
                                else
                                {
                                    ifPositionBuilder.Append(rangeDictionary[tbIfPosition.Text][i]);
                                }
                            }
                        }
                        else
                        {
                            if (IsTrueContent(tbIfPosition.Text, splitNotation, rangeNotation))
                            {
                                ifPositionBuilder.Append(tbIfPosition.Text);
                            }
                            else
                            {
                                tbIfPosition.Select(0, tbIfPosition.Text.Length);
                                tbIfPosition.Focus();
                                return;
                            }
                        }
                        ifPrerequisite += Environment.NewLine + "\tRangeGroup " + "step" + x.ToString() + "Range = new RangeGroup(\""
                         + ifPositionBuilder.ToString() + "\",'" + splitNotation + "','" + rangeNotation + "');";
                        containDictionary[GetStepName()].Add("Step" + x);
                        if (ifPrerequisiteBottom.Equals(String.Empty))
                        {
                            ifPrerequisiteBottom = "Position:step" + x.ToString() + "Range";
                        }
                        else
                        {
                            ifPrerequisiteBottom += "&&Position:step" + x.ToString() + "Range";
                        }
                    }
                    if (j == 3 && !tbIfColor.Text.Equals(String.Empty))
                    {
                        StringBuilder ifColorBuilder = new StringBuilder();
                        if (rangeDictionary.ContainsKey(tbIfColor.Text))
                        {
                            for (int i = 0; i < rangeDictionary[tbIfColor.Text].Count; i++)
                            {
                                if (i != rangeDictionary[tbIfColor.Text].Count - 1)
                                {
                                    ifColorBuilder.Append(rangeDictionary[tbIfColor.Text][i] + splitNotation.ToString());
                                }
                                else
                                {
                                    ifColorBuilder.Append(rangeDictionary[tbIfColor.Text][i]);
                                }
                            }
                        }
                        else
                        {
                            if (IsTrueContent(tbIfColor.Text, splitNotation, rangeNotation))
                            {
                                ifColorBuilder.Append(tbIfColor.Text);
                            }
                            else
                            {
                                tbIfColor.Select(0, tbIfColor.Text.Length);
                                tbIfColor.Focus();
                                return;
                            }
                        }
                        ifPrerequisite += Environment.NewLine + "\tRangeGroup " + "step" + x.ToString() + "Range = new RangeGroup(\""
                         + ifColorBuilder.ToString() + "\",'" + splitNotation + "','" + rangeNotation + "');";
                        containDictionary[GetStepName()].Add("Step" + x);
                        if (ifPrerequisiteBottom.Equals(String.Empty))
                        {
                            ifPrerequisiteBottom = "Color:step" + x.ToString() + "Range";
                        }
                        else
                        {
                            ifPrerequisiteBottom += "&&Color:step" + x.ToString() + "Range";
                        }
                    }
                }
                String thenPrerequisite = String.Empty;
                if (!tbThenTime.Text.Equals(String.Empty))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(tbThenTime.Text, "^\\d+$"))
                    {
                        tbThenTime.Select(0, tbThenTime.Text.Length);
                        tbThenTime.Focus();
                        return;
                    }
                    thenPrerequisite = "Time:" + tbThenTime.Text;
                }
                if (!tbThenPosition.Text.Equals(String.Empty))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(tbThenPosition.Text, "^\\d+$"))
                    {
                        tbThenPosition.Select(0, tbThenPosition.Text.Length);
                        tbThenPosition.Focus();
                        return;
                    }
                    if (thenPrerequisite.Equals(String.Empty))
                    {
                        thenPrerequisite = "Position:" + tbThenPosition.Text;
                    }
                    else
                    {
                        thenPrerequisite = "&&Position:" + tbThenPosition.Text;
                    }
                }
                if (!tbThenColor.Text.Equals(String.Empty))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(tbThenColor.Text, "^\\d+$"))
                    {
                        tbThenColor.Select(0, tbThenColor.Text.Length);
                        tbThenColor.Focus();
                        return;
                    }
                    if (thenPrerequisite.Equals(String.Empty))
                    {
                        thenPrerequisite = "Color:" + tbThenColor.Text;
                    }
                    else
                    {
                        thenPrerequisite = "&&Color:" + tbThenColor.Text;
                    }
                }
                lightScriptDictionary[GetStepName()] += ifPrerequisite + Environment.NewLine
                + "\t" + GetStepName() + "LightGroup = Edit.IfThen(" + GetStepName() + "LightGroup," + ifPrerequisiteBottom
                 + "," + thenPrerequisite + "," + control + ");";
                //RangeGroup Step2Range = new RangeGroup("36", ' ', '-');
                //Step2LightGroup = Edit.IfThen(Step2LightGroup, Position:Step2Range, Time:+1, Remove);
            }
            if (RefreshData())
            {
                if (sender == btnFastGenerationrAdd || sender == btnFastGenerationrSelect)
                {
                    AddStep(stepName, "");
                    //如果选中，就在列表选中最后一个
                    if (sender == btnFastGenerationrSelect)
                    {
                        lbStep.SelectedIndex = lbStep.Items.Count - 1;
                    }
                }
                if (sender == btnSelectEditorAdd)
                {
                    AddStep(stepName, GetStepName());
                    //如果选中，就在列表选中最后一个
                    if (sender == btnSelectEditorAdd)
                    {
                        lbStep.SelectedIndex = lbStep.Items.Count - 1;
                    }
                }
            }
        }
        /// <summary>
        /// 内容是否正确
        /// </summary>
        /// <param name="content"></param>
        /// <param name="split"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool IsTrueContent(String content, char split, char range)
        {
            try
            {
                List<int> mList = new List<int>();
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
                                mList.Add(k);
                            }
                        }
                        else if (One > Two)
                        {
                            for (int k = One; k >= Two; k--)
                            {
                                mList.Add(k);
                            }
                        }
                        else
                        {
                            mList.Add(One);
                        }
                    }
                    else
                    {
                        mList.Add(int.Parse(strSplit[i]));
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SaveLightScriptFile(String fileName,bool isSaveLocked)
        {
            StringBuilder mBuilder = new StringBuilder();
            foreach (var item in lightScriptDictionary)
            {
                bool isMatch = false;
                foreach (var itemChildren in extendsDictionary)
                {
                    if (itemChildren.Value.Contains(item.Key))
                    {
                        mBuilder.Append(item.Key + " extends " + itemChildren.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine + "}" + Environment.NewLine);
                        isMatch = true;
                        break;
                    }
                }
                foreach (var itemChildren in intersectionDictionary)
                {
                    if (itemChildren.Value.Contains(item.Key))
                    {
                        mBuilder.Append(item.Key + " intersection " + itemChildren.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine + "}" + Environment.NewLine);
                        isMatch = true;
                        break;
                    }
                }
                foreach (var itemChildren in complementDictionary)
                {
                    if (itemChildren.Value.Contains(item.Key))
                    {
                        mBuilder.Append(item.Key + " complement " + itemChildren.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine + "}" + Environment.NewLine);
                        isMatch = true;
                        break;
                    }
                }
                if (!isMatch)
                {
                    mBuilder.Append(item.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine + "}" + Environment.NewLine);
                }
            }
            StringBuilder mVisibleBuilder = new StringBuilder();
            foreach (var item in visibleDictionary)
            {
                if (!item.Value)
                {
                    if (!mVisibleBuilder.ToString().Equals(String.Empty))
                    {
                        mVisibleBuilder.Append("\t" + item.Key + Environment.NewLine);
                    }
                    else
                    {
                        mVisibleBuilder.Append("NoVisible{" + Environment.NewLine);
                        mVisibleBuilder.Append("\t" + item.Key + ";" + Environment.NewLine);
                    }
                }
            }
            if (!mVisibleBuilder.ToString().Equals(String.Empty))
            {
                mVisibleBuilder.Append("}" + Environment.NewLine);
                mBuilder.Append(mVisibleBuilder.ToString());
            }
            if (containDictionary.Count != 0)
            {
                StringBuilder mContainBuilder = new StringBuilder();
                mContainBuilder.Append("Contain{" + Environment.NewLine);
                foreach (var item in containDictionary)
                {
                    mContainBuilder.Append("\t" + item.Key + ":");
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        if (i != item.Value.Count - 1)
                        {
                            mContainBuilder.Append(item.Value[i] + ",");
                        }
                        else
                        {
                            mContainBuilder.Append(item.Value[i] + ";" + Environment.NewLine);
                        }
                    }
                }
                mContainBuilder.Append("}" + Environment.NewLine);
                mBuilder.Append(mContainBuilder.ToString());
            }
            StringBuilder mIntroduceBuilder = new StringBuilder();
            mIntroduceBuilder.Append("Introduce{" + Environment.NewLine);
            mIntroduceBuilder.Append(introduceText + Environment.NewLine);
            mIntroduceBuilder.Append("}" + Environment.NewLine);
            mBuilder.Append(mIntroduceBuilder.ToString());
            if (importList.Count != 0)
            {
                StringBuilder mImportBuilder = new StringBuilder();
                mImportBuilder.Append("Import{" + Environment.NewLine);
                foreach (var item in importList)
                {
                    mImportBuilder.Append("\t" + item + ";" + Environment.NewLine);
                }
                mImportBuilder.Append("}" + Environment.NewLine);
                mBuilder.Append(mImportBuilder.ToString());
            }
            if (finalDictionary.Count != 0)
            {
                StringBuilder mFinalBuilder = new StringBuilder();
                mFinalBuilder.Append("Final{" + Environment.NewLine);
                foreach (var item in finalDictionary)
                {
                    if (!item.Value.Equals(String.Empty))
                        mFinalBuilder.Append("\t" + item.Key + ":" + item.Value + "." + Environment.NewLine);
                }
                mFinalBuilder.Append("}" + Environment.NewLine);
                mBuilder.Append(mFinalBuilder.ToString());
            }
            if (lockedDictionary.Count != 0)
            {
                StringBuilder mLockedBuilder = new StringBuilder();
                mLockedBuilder.Append("Locked{" + Environment.NewLine);
                foreach (var item in lockedDictionary)
                {
                    if (!item.Value.Equals(String.Empty))
                        mLockedBuilder.Append("\t" + item.Key + ":" + Business.FileBusiness.CreateInstance().String2Base(Business.FileBusiness.CreateInstance().WriteMidiContent(item.Value)) + "." + Environment.NewLine);
                }
                mLockedBuilder.Append("}" + Environment.NewLine);
                mBuilder.Append(mLockedBuilder.ToString());
            }
            if (fileName.Equals(String.Empty))
            {
                LightScriptBusiness _scriptBusiness;

                //LightScriptBusiness _scriptBusiness = new LightScriptBusiness(this, mBuilder.ToString(), mw.lightScriptFilePath);
                //_scriptBusiness.SaveScriptFile(mBuilder.ToString());
            }
            else
            {
                // 创建文件，准备写入
                File.Delete(fileName);
                using (FileStream fs = File.Open(fileName,
                            FileMode.OpenOrCreate,
                            FileAccess.Write))
                {
                    using (StreamWriter wr = new StreamWriter(fs))
                    {
                        wr.Write(mBuilder.ToString());
                    }
                }
            }
        }
        public String GetCompleteScript()
        {
            //添加导入库语句
            StringBuilder importbuilder = new StringBuilder();
            foreach (var item in importList)
            {
                importbuilder.Append("Import " + item + ";" + Environment.NewLine);
            }
            //添加主体语句
            StringBuilder builder = new StringBuilder();
            if (!importbuilder.ToString().Equals(String.Empty))
            {
                //去掉最后一个\r\n
                //builder.Append(importbuilder.ToString().Substring(0,importbuilder.ToString().Length-2));
                builder.Append(importbuilder.ToString());
            }
            foreach (var item in lightScriptDictionary)
            {
                if (!visibleDictionary[item.Key])
                    continue;
                bool isMatch = false;
                foreach (var itemChildren in extendsDictionary)
                {
                    if (itemChildren.Value.Contains(item.Key))
                    {
                        builder.Append(item.Key + " extends " + itemChildren.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine);
                        if (item.Value.Contains(item.Key + "LightGroup"))
                        {
                            builder.Append("\t" + item.Key + ".Add(" + item.Key + "LightGroup);");
                        }
                        builder.Append(Environment.NewLine + "}" + Environment.NewLine);
                        isMatch = true;
                        break;
                    }
                }
                foreach (var itemChildren in intersectionDictionary)
                {
                    if (itemChildren.Value.Contains(item.Key))
                    {
                        builder.Append(item.Key + " intersection " + itemChildren.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine);
                        if (item.Value.Contains(item.Key + "LightGroup"))
                        {
                            builder.Append("\t" + item.Key + ".Add(" + item.Key + "LightGroup);");
                        }
                        builder.Append(Environment.NewLine + "}" + Environment.NewLine);
                        isMatch = true;
                        break;
                    }
                }
                foreach (var itemChildren in complementDictionary)
                {
                    if (itemChildren.Value.Contains(item.Key))
                    {
                        builder.Append(item.Key + " complement " + itemChildren.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine);
                        if (item.Value.Contains(item.Key + "LightGroup"))
                        {
                            builder.Append("\t" + item.Key + ".Add(" + item.Key + "LightGroup);");
                        }
                        builder.Append(Environment.NewLine + "}" + Environment.NewLine);
                        isMatch = true;
                        break;
                    }
                }
                if (!isMatch)
                {
                    builder.Append(item.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine);
                    if (item.Value.Contains(item.Key + "LightGroup"))
                    {
                        builder.Append("\t" + item.Key + ".Add(" + item.Key + "LightGroup);");
                    }
                    builder.Append(Environment.NewLine + "}" + Environment.NewLine);
                }
            }
            builder.Append("Main{" + Environment.NewLine);
            int i = 1;
            foreach (var item in lightScriptDictionary)
            {
                if (builder.ToString().Contains(item.Key + ".Add("))
                {

                    bool isMatch = false;
                    foreach (var itemChildren in intersectionDictionary)
                    {
                        if (itemChildren.Value.Contains(item.Key))
                        {
                            isMatch = true;
                            break;
                        }
                    }
                    foreach (var itemChildren in complementDictionary)
                    {
                        if (itemChildren.Value.Contains(item.Key))
                        {
                            isMatch = true;
                            break;
                        }
                    }
                    if (isMatch)
                    {
                        continue;
                    }
                    builder.Append("\tLightGroup testLightGroup" + i.ToString() + " = this." + item.Key + "();" + Environment.NewLine);
                    int finalName = 0;
                    if (finalDictionary.ContainsKey(item.Key))
                    {
                        String[] contents = finalDictionary[item.Key].Split(';');
                        StringBuilder command = new StringBuilder();
                        foreach (String str in contents)
                        {
                            if (str.Equals(String.Empty))
                                continue;
                            String[] strs = str.Split('=');
                            String type = strs[0];
                            String[] _contents = strs[1].Split(',');

                            if (type.Equals("Color"))
                            {
                                foreach (String _str in _contents)
                                {
                                    String[] mContents = _str.Split('-');
                                    if (mContents[0].Equals("Format"))
                                    {
                                        if (mContents[1].Equals("Green"))
                                        {
                                            command.Append("\tColorGroup testColorGroup" + i.ToString() +finalName + " = new ColorGroup(\""
                                                + "73 74 75 76" + "\",' ','-');" + Environment.NewLine
                                              + "\ttestLightGroup" + i.ToString() + ".Color = testColorGroup" + i.ToString() + finalName + "; ");
                                        }
                                        if (mContents[1].Equals("Blue"))
                                        {
                                            command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName + " = new ColorGroup(\""
                                            + "33 37 41 45" + "\",' ','-');" + Environment.NewLine
                                          + "\ttestLightGroup" + i.ToString() + ".Color = testColorGroup" + i.ToString() + finalName + "; ");
                                        }
                                        if (mContents[1].Equals("Pink"))
                                        {
                                            command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName + " = new ColorGroup(\""
                                           + "4 94 53 57" + "\",' ','-');" + Environment.NewLine
                                         + "\ttestLightGroup" + i.ToString() + ".Color = testColorGroup" + i.ToString() + finalName + "; ");
                                        }
                                        if (mContents[1].Equals("Diy"))
                                        {
                                            command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName + " = new ColorGroup(\""
                                       + mContents[2] + "\",' ','-');" + Environment.NewLine
                                     + "\ttestLightGroup" + i.ToString() + ".Color = testColorGroup" + i.ToString() + finalName + "; ");
                                        }
                                    }
                                    else if (mContents[0].Equals("Shape"))
                                    {
                                        if (mContents[1].Equals("Square"))
                                        {
                                            command.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.ShapeColor(testLightGroup" + i.ToString() + ",Square,\"" + mContents[2] + "\");");
                                        }
                                        else if (mContents[1].Equals("RadialVertical"))
                                        {
                                            command.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.ShapeColor(testLightGroup" + i.ToString() + ",RadialVertical,\"" + mContents[2] + "\");");
                                        }
                                        else if (mContents[1].Equals("RadialHorizontal"))
                                        {
                                            command.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + "= Edit.ShapeColor(testLightGroup" + i.ToString() + ",RadialHorizontal,\"" + mContents[2] + "\");");
                                        }
                                    }
                                }
                            }
                            if (type.Equals("Shape"))
                            {
                                foreach (String _str in _contents)
                                {
                                    if (_str.Equals("HorizontalFlipping"))
                                    {
                                        command.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.HorizontalFlipping(testLightGroup" + i.ToString() + ");");
                                    }
                                    if (_str.Equals("VerticalFlipping"))
                                    {
                                        command.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.VerticalFlipping(testLightGroup" + i.ToString() + ");");
                                    }
                                    if (_str.Equals("Clockwise"))
                                    {
                                        command.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.Clockwise(testLightGroup" + i.ToString() + ");");
                                    }
                                    if (_str.Equals("AntiClockwise"))
                                    {
                                        command.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.AntiClockwise(testLightGroup" + i.ToString() + ");");
                                    }
                                }
                            }
                            if (type.Equals("Time"))
                            {
                                foreach (String _str in _contents)
                                {
                                    if (_str.Equals("Reversal"))
                                    {
                                        command.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.Reversal(testLightGroup" + i.ToString() + ");");
                                    }
                                    String[] mContents = _str.Split('-');
                                    if (mContents[0].Equals("ChangeTime"))
                                    {
                                        command.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.ChangeTime(testLightGroup" + i.ToString() + "," + mContents[1] + "," + mContents[2] + ");");
                                    }
                                    else if (mContents[0].Equals("StartTime"))
                                    {
                                        command.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = testLightGroup" + i.ToString() + ".SetStartTime(" + mContents[1] + ");");
                                    }
                                    else if (mContents[0].Equals("AllTime"))
                                    {
                                        command.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = testLightGroup" + i.ToString() + ".SetAllTime(" + mContents[1] + ");");
                                    }
                                }
                            }
                            if (type.Equals("ColorOverlay"))
                            {
                                foreach (String _str in _contents)
                                {
                                    String[] mContents = _str.Split('-');
                                    if (mContents[0].Equals("true"))
                                    {
                                        command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName + " = new ColorGroup(\""
                                     + mContents[1] + "\",' ','-');" + Environment.NewLine
                                   + "\ttestLightGroup" + i.ToString() + " = Edit.CopyToTheFollow(testLightGroup" + i.ToString() + ",testColorGroup" + finalName + i.ToString() + "; ");
                                    }
                                    else
                                    {
                                        command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName + " = new ColorGroup(\""
                                 + mContents[1] + "\",' ','-');" + Environment.NewLine
                               + "\ttestLightGroup" + i.ToString() + " = Edit.CopyToTheEnd(testLightGroup" + i.ToString() + ",testColorGroup" + i.ToString() + finalName + "; ");
                                    }
                                }
                            }
                            if (type.Equals("SportOverlay"))
                            {
                                foreach (String _str in _contents)
                                {
                                    String[] mContents = _str.Split('-');
                                    command.Append("\tRangeGroup testRangeGroup" + i.ToString() + finalName + " = new RangeGroup(\""
                             + mContents[0] + "\",' ','-');" + Environment.NewLine
                           + "\ttestLightGroup" + i.ToString() + " = Edit.AccelerationOrDeceleration(testLightGroup" + i.ToString() + ",testRangeGroup" + i.ToString() + finalName + "; ");
                                }
                            }
                            if (type.Equals("Other"))
                            {
                                foreach (String _str in _contents)
                                {
                                    if (_str.Equals("RemoveBorder"))
                                    {
                                        command.Append(Environment.NewLine + "\t" + "testLightGroup" + i.ToString() + " = Edit.RemoveBorder(testLightGroup" + i.ToString() + ");");
                                    }
                                }
                            }
                            finalName++;
                        }
                        builder.Append(command.ToString());
                    }
                    builder.Append("\tMain.Add(testLightGroup" + i.ToString() + ");");
                    i++;
                }
            }
            if (finalDictionary.ContainsKey("Main"))
            {
                String[] contents = finalDictionary["Main"].Split(';');
                StringBuilder command = new StringBuilder();
                int finalName = 0;
                foreach (String str in contents)
                {
                    if (str.Equals(String.Empty))
                        continue;
                    String[] strs = str.Split('=');
                    String type = strs[0];
                    String[] _contents = strs[1].Split(',');

                    if (type.Equals("Color"))
                    {
                        foreach (String _str in _contents)
                        {
                            String[] mContents = _str.Split('-');
                            if (mContents[0].Equals("Format"))
                            {
                                if (mContents[1].Equals("Green"))
                                {
                                    command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName+" = new ColorGroup(\""
                                        + "73 74 75 76" + "\",' ','-');" + Environment.NewLine
                                      + "\tMain.Color = testColorGroup" + i.ToString() + finalName + "; ");
                                }
                                if (mContents[1].Equals("Blue"))
                                {
                                    command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName + " = new ColorGroup(\""
                                    + "33 37 41 45" + "\",' ','-');" + Environment.NewLine
                                  + "\tMain.Color = testColorGroup" + i.ToString() + finalName + "; ");
                                }
                                if (mContents[1].Equals("Pink"))
                                {
                                    command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName + " = new ColorGroup(\""
                                   + "4 94 53 57" + "\",' ','-');" + Environment.NewLine
                                 + "\tMain.Color = testColorGroup" + finalName + i.ToString() + "; ");
                                }
                                if (mContents[1].Equals("Diy"))
                                {
                                    command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName + " = new ColorGroup(\""
                               + mContents[2] + "\",' ','-');" + Environment.NewLine
                             + "\tMain.Color = testColorGroup" + i.ToString() + finalName + "; ");
                                }
                            }
                            else if (mContents[0].Equals("Shape"))
                            {
                                if (mContents[1].Equals("Square"))
                                {
                                    command.Append(Environment.NewLine + "\tMain = Edit.ShapeColor(Main,Square,\"" + mContents[2] + "\");");
                                }
                                else if (mContents[1].Equals("RadialVertical"))
                                {
                                    command.Append(Environment.NewLine + "\tMain = Edit.ShapeColor(Main,RadialVertical,\"" + mContents[2] + "\");");
                                }
                                else if (mContents[1].Equals("RadialHorizontal"))
                                {
                                    command.Append(Environment.NewLine + "\tMain = Edit.ShapeColor(Main,RadialHorizontal,\"" + mContents[2] + "\");");
                                }
                            }
                        }
                    }
                    if (type.Equals("Shape"))
                    {
                        foreach (String _str in _contents)
                        {
                            if (_str.Equals("HorizontalFlipping"))
                            {
                                command.Append(Environment.NewLine + "\tMain = Edit.HorizontalFlipping(Main);");
                            }
                            if (_str.Equals("VerticalFlipping"))
                            {
                                command.Append(Environment.NewLine + "\tMain = Edit.VerticalFlipping(Main);");
                            }
                            if (_str.Equals("Clockwise"))
                            {
                                command.Append(Environment.NewLine + "\tMain = Edit.Clockwise(Main);");
                            }
                            if (_str.Equals("AntiClockwise"))
                            {
                                command.Append(Environment.NewLine + "\tMain = Edit.AntiClockwise(Main);");
                            }
                        }
                    }
                    if (type.Equals("Time"))
                    {
                        foreach (String _str in _contents)
                        {
                            if (_str.Equals("Reversal"))
                            {
                                command.Append(Environment.NewLine + "\tMain = Edit.Reversal(Main);");
                            }
                            String[] mContents = _str.Split('-');
                            if (mContents[0].Equals("ChangeTime"))
                            {
                                command.Append(Environment.NewLine + "\tMain = Edit.ChangeTime(Main," + mContents[1] + "," + mContents[2] + ");");
                            }
                            else if (mContents[0].Equals("StartTime"))
                            {
                                command.Append(Environment.NewLine + "\tMain = Main.SetStartTime(" + mContents[1] + ");");
                            }
                            else if (mContents[0].Equals("AllTime"))
                            {
                                command.Append(Environment.NewLine + "\tMain = Main.SetAllTime(" + mContents[1] + ");");
                            }
                        }
                    }
                    if (type.Equals("ColorOverlay"))
                    {
                        foreach (String _str in _contents)
                        {
                            String[] mContents = _str.Split('-');
                            if (mContents[0].Equals("true"))
                            {
                                command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName + " = new ColorGroup(\""
                             + mContents[1] + "\",' ','-');" + Environment.NewLine
                           + "\tMain = Edit.CopyToTheFollow(Main,testColorGroup" + i.ToString() + finalName + ";");
                            }
                            else
                            {
                                command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName + " = new ColorGroup(\""
                         + mContents[1] + "\",' ','-');" + Environment.NewLine
                       + "\tMain = Edit.CopyToTheEnd(Main,testColorGroup" + i.ToString() + finalName + ";");
                            }
                        }
                    }
                    if (type.Equals("SportOverlay"))
                    {
                        foreach (String _str in _contents)
                        {
                            String[] mContents = _str.Split('-');
                            command.Append("\tRangeGroup testRangeGroup" + i.ToString() + finalName + " = new RangeGroup(\""
                     + mContents[0] + "\",' ','-');" + Environment.NewLine
                   + "\tMain = Edit.AccelerationOrDeceleration(Main,testRangeGroup" + i.ToString() + finalName + ";");
                        }
                    }
                    if (type.Equals("Other"))
                    {
                        foreach (String _str in _contents)
                        {
                            if (_str.Equals("RemoveBorder"))
                            {
                                command.Append(Environment.NewLine + "\t" + "Main = Edit.RemoveBorder(Main);");
                            }
                        }
                    }
                    finalName++;
                }
                builder.Append(command);
            }
            builder.Append(Environment.NewLine+"}");
            return builder.ToString();
            //Console.WriteLine(Environment.NewLine +builder.ToString()+Environment.NewLine); 
        }
        public bool RefreshData()
        {
            SaveLightScriptFile("",false);

            //LightScriptBusiness scriptBusiness = new LightScriptBusiness(this, GetCompleteScript(), mw.lightScriptFilePath,lockedDictionary);
            //mLightList = scriptBusiness.GetResult(null);

            //if (mLightList == null)
            //{
            //    bridge.UpdateData(new List<Light>());
            //    //出错了回退
            //    Unmake();
            //    return false;
            //}
            //bridge.UpdateData(mLightList);
            return true;
        }
        public bool RefreshData(bool isSaveLocked)
        {
            SaveLightScriptFile("",isSaveLocked);

            //LightScriptBusiness scriptBusiness = new LightScriptBusiness(this, GetCompleteScript(), mw.lightScriptFilePath);
            //mLightList = scriptBusiness.GetResult(null);

            //if (mLightList == null)
            //{
            //    bridge.UpdateData(new List<Light>());
            //    //出错了回退
            //    Unmake();
            //    return false;
            //}
            //bridge.UpdateData(mLightList);
            return true;
        }

        public List<Light> mLightList = new List<Light>();

        public List<Light> RefreshData(String partName)
        {
            //添加导入库语句
            StringBuilder importbuilder = new StringBuilder();
            foreach (var item in importList)
            {
                importbuilder.Append("Import " + item + ";" + Environment.NewLine);
            }
            //添加主体语句
            StringBuilder builder = new StringBuilder();
            if (!importbuilder.ToString().Equals(String.Empty))
            {
                //去掉最后一个\r\n
                //builder.Append(importbuilder.ToString().Substring(0,importbuilder.ToString().Length-2));
                builder.Append(importbuilder.ToString());
            }
            foreach (var item in lightScriptDictionary)
            {
                bool isMatch = false;
                foreach (var itemChildren in extendsDictionary)
                {
                    if (itemChildren.Value.Contains(item.Key))
                    {
                        builder.Append(item.Key + " extends " + itemChildren.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine);
                        if (item.Value.Contains(item.Key + "LightGroup"))
                        {
                            builder.Append("\t" + item.Key + ".Add(" + item.Key + "LightGroup);");
                        }
                        builder.Append(Environment.NewLine + "}" + Environment.NewLine);
                        isMatch = true;
                        break;
                    }
                }
                if (!isMatch)
                {
                    builder.Append(item.Key + "{" + Environment.NewLine + item.Value + Environment.NewLine);
                    if (item.Value.Contains(item.Key + "LightGroup"))
                    {
                        builder.Append("\t" + item.Key + ".Add(" + item.Key + "LightGroup);");
                    }
                    builder.Append(Environment.NewLine + "}" + Environment.NewLine);
                }
            }
            builder.Append("Main{" + Environment.NewLine);
            int i = 1;
            foreach (var item in lightScriptDictionary)
            {
                if (builder.ToString().Contains(item.Key + ".Add("))
                {
                    builder.Append("\tLightGroup testLightGroup" + i.ToString() + " = this." + item.Key + "();" + Environment.NewLine);
                    builder.Append("\tMain.Add(testLightGroup" + i.ToString() + ");" + Environment.NewLine);
                    i++;
                }
            }
            builder.Append("}");

            //LightScriptBusiness scriptBusiness = new LightScriptBusiness(this, builder.ToString(), mw.lightScriptFilePath);
            //return scriptBusiness.GetResult(partName);
            return null;
        }
        public String GetUsableStepName()
        {
            //从1开始计算
            int i = 1;
            while (i <= 100000)//最多100000步
            {
                if (!ContainsStepName("Step" + i))
                {
                    return "Step" + i;
                }
                i++;
            }
            return null;
        }

        public enum ShowMode
        { Launchpad, DataGrid };
        public ShowMode mShow = ShowMode.Launchpad;

        /// <summary>
        /// 添加步骤
        /// </summary>
        /// <param name="stepName">步骤名</param>
        /// <param name="parentName">父类名</param>
        public void AddStep(String stepName, String parentName)
        {
            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            DockPanel visiblePanel = new DockPanel();
            visiblePanel.MouseLeftButtonDown += VisiblePanel_MouseLeftButtonDown;
            visiblePanel.Width = 30;
            visiblePanel.Height = 30;
            Image visibleImage = new Image
            {
                Width = 20,
                Height = 20,
                Source = new BitmapImage(new Uri("pack://application:,,,/Image/visible.png", UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.Fill
            };
            RenderOptions.SetBitmapScalingMode(visibleImage, BitmapScalingMode.Fant);
            visibleImage.VerticalAlignment = VerticalAlignment.Center;
            visibleImage.HorizontalAlignment = HorizontalAlignment.Center;
            visiblePanel.Children.Add(visibleImage);
            visiblePanel.VerticalAlignment = VerticalAlignment.Center;
            sp.Children.Add(visiblePanel);
            TextBlock blockStepName = new TextBlock
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)),
                FontSize = 14,
                Margin = new Thickness(10, 0, 0, 0),
                Width = 80,
                Text = stepName
            };
            blockStepName.PreviewMouseLeftButtonDown += RenameStepName;
            blockStepName.VerticalAlignment = VerticalAlignment.Center;
            sp.Children.Add(blockStepName);
            TextBlock blockHintParent = new TextBlock();
            blockHintParent.SetResourceReference(TextBlock.TextProperty, "ParentColon");
            blockHintParent.FontSize = 14;
            blockHintParent.VerticalAlignment = VerticalAlignment.Center;
            blockHintParent.Margin = new Thickness(10, 0, 0, 0);
            sp.Children.Add(blockHintParent);
            TextBlock blockParentName = new TextBlock
            {
                FontSize = 14,
                Text = parentName,
                Width = 80,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)),
                VerticalAlignment = VerticalAlignment.Center
            };
            sp.Children.Add(blockParentName);
            DockPanel lockedPanel = new DockPanel
            {
                Width = 30,
                Height = 30
            };
            Image lockedImage = new Image
            {
                Width = 20,
                Height = 20,
                Stretch = Stretch.Fill
            };
            RenderOptions.SetBitmapScalingMode(lockedImage, BitmapScalingMode.Fant);
            lockedImage.VerticalAlignment = VerticalAlignment.Center;
            lockedImage.HorizontalAlignment = HorizontalAlignment.Center;
            lockedPanel.Children.Add(lockedImage);
            lockedPanel.VerticalAlignment = VerticalAlignment.Center;
            sp.Children.Add(lockedPanel);
            lbStep.Items.Add(sp);
        }

        private void StyleIntegration(object sender, RoutedEventArgs e)
        {
            if (lbStep.SelectedIndex == -1)
            {
                return;
            }
            if (!finalDictionary.ContainsKey(GetStepName()))
                return;
            //没有可操作的灯光组
            if (!lightScriptDictionary[GetStepName()].Contains(GetStepName() + "LightGroup"))
            {
                return;
            }
            String[] contents = finalDictionary[GetStepName()].Split(';');
            StringBuilder command = new StringBuilder();
            foreach (String str in contents)
            {
                if (str.Equals(String.Empty))
                    continue;
                String[] strs = str.Split('=');
                String type = strs[0];
                String[] _contents = strs[1].Split(',');

                if (type.Equals("Color"))
                {
                    foreach (String _str in _contents)
                    {
                        String[] mContents = _str.Split('-');
                        if (mContents[0].Equals("Format"))
                        {
                            String colorGroupName = String.Empty;
                            int i = 1;
                            while (i <= 100000)
                            {
                                if (!containDictionary[GetStepName()].Contains("Step" + i))
                                {
                                    containDictionary[GetStepName()].Add("Step" + i);
                                    colorGroupName = "Step" + i + "Color";
                                    break;
                                }
                                i++;
                            }
                            if (i > 100000)
                            {
                                new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                                return;
                            }

                            if (mContents[1].Equals("Green"))
                            {
                                command.Append("\tColorGroup " + colorGroupName + " = new ColorGroup(\""
                                    + "73 74 75 76" + "\",' ','-');" + Environment.NewLine
                                  + "\t" + GetStepName() + "LightGroup.Color = " + colorGroupName + ";" + Environment.NewLine
                                   );
                            }
                            if (mContents[1].Equals("Blue"))
                            {
                                command.Append("\tColorGroup " + colorGroupName + " = new ColorGroup(\""
                                  + "33 37 41 45" + "\",' ','-');" + Environment.NewLine
                                + "\t" + GetStepName() + "LightGroup.Color = " + colorGroupName + ";" + Environment.NewLine);
                            }
                            if (mContents[1].Equals("Pink"))
                            {
                                command.Append("\tColorGroup " + colorGroupName + " = new ColorGroup(\""
                                + "4 94 53 57" + "\",' ','-');" + Environment.NewLine
                              + "\t" + GetStepName() + "LightGroup.Color = " + colorGroupName + ";" + Environment.NewLine);
                            }
                            if (mContents[1].Equals("Diy"))
                            {
                                command.Append("\tColorGroup " + colorGroupName + " = new ColorGroup(\""
                              + mContents[2] + "\",' ','-');" + Environment.NewLine
                            + "\t" + GetStepName() + "LightGroup.Color = " + colorGroupName + ";" + Environment.NewLine);
                            }
                        }
                        else if (mContents[0].Equals("Shape"))
                        {
                            if (mContents[1].Equals("Square"))
                            {
                                command.Append("\t" + GetStepName() + "LightGroup = Edit.ShapeColor(" + GetStepName() + "LightGroup,Square,\"" + mContents[2] + "\");" + Environment.NewLine);
                            }
                            else if (mContents[1].Equals("RadialVertical"))
                            {
                                command.Append("\t" + GetStepName() + "LightGroup = Edit.ShapeColor(" + GetStepName() + "LightGroup,RadialVertical,\"" + mContents[2] + "\");" + Environment.NewLine);
                            }
                            else if (mContents[1].Equals("RadialHorizontal"))
                            {
                                command.Append("\t" + GetStepName() + "LightGroup = Edit.ShapeColor(" + GetStepName() + "LightGroup,RadialHorizontal,\"" + mContents[2] + "\");" + Environment.NewLine);
                            }
                        }
                    }
                }
                if (type.Equals("Shape"))
                {
                    foreach (String _str in _contents)
                    {
                        if (_str.Equals("HorizontalFlipping"))
                        {
                            command.Append("\t" + GetStepName() + "LightGroup = Edit.HorizontalFlipping(" + GetStepName() + "LightGroup);" + Environment.NewLine);
                        }
                        if (_str.Equals("VerticalFlipping"))
                        {
                            command.Append("\t" + GetStepName() + "LightGroup = Edit.VerticalFlipping(" + GetStepName() + "LightGroup);" + Environment.NewLine);
                        }
                        if (_str.Equals("Clockwise"))
                        {
                            command.Append("\t" + GetStepName() + "LightGroup = Edit.Clockwise(" + GetStepName() + "LightGroup);" + Environment.NewLine);

                        }
                        if (_str.Equals("AntiClockwise"))
                        {
                            command.Append("\t" + GetStepName() + "LightGroup = Edit.AntiClockwise(" + GetStepName() + "LightGroup);" + Environment.NewLine);
                        }
                    }
                }
                if (type.Equals("Time"))
                {
                    foreach (String _str in _contents)
                    {
                        if (_str.Equals("Reversal"))
                        {
                            command.Append("\t" + GetStepName() + "LightGroup = Edit.Reversal(" + GetStepName() + "LightGroup);" + Environment.NewLine);
                        }
                        String[] mContents = _str.Split('-');
                        if (mContents[0].Equals("ChangeTime"))
                        {
                            command.Append("\t" + GetStepName() + "LightGroup = Edit.ChangeTime(" + GetStepName() + "LightGroup," + mContents[1] + "," + mContents[2] + ");" + Environment.NewLine);

                        }
                        else if (mContents[0].Equals("StartTime"))
                        {
                            command.Append("\t" + GetStepName() + "LightGroup = " + GetStepName() + "LightGroup.SetStartTime(" + mContents[1] + ");" + Environment.NewLine);
                        }
                        else if (mContents[0].Equals("AllTime"))
                        {
                            command.Append("\t" + GetStepName() + "LightGroup = " + GetStepName() + "LightGroup.SetAllTime(" + mContents[1] + ");" + Environment.NewLine);
                        }
                    }
                }
                if (type.Equals("ColorOverlay"))
                {
                    foreach (String _str in _contents)
                    {
                        String[] mContents = _str.Split('-');
                        if (mContents[0].Equals("true"))
                        {
                            String colorGroupName = String.Empty;
                            int i = 1;
                            while (i <= 100000)
                            {
                                if (!containDictionary[GetStepName()].Contains("Step" + i))
                                {
                                    containDictionary[GetStepName()].Add("Step" + i);
                                    colorGroupName = "Step" + i + "Color";
                                    break;
                                }
                                i++;
                            }
                            if (i > 100000)
                            {
                                new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                                return;
                            }
                            command.Append("\tColorGroup " + colorGroupName + " = new ColorGroup(\""
                              + mContents[1] + "\",' ','-');" + Environment.NewLine
                            + "\t" + GetStepName() + "LightGroup = Edit.CopyToTheFollow(" + GetStepName() + "LightGroup," + colorGroupName + "); " + Environment.NewLine);
                        }
                        else
                        {
                            String colorGroupName = String.Empty;
                            int i = 1;
                            while (i <= 100000)
                            {
                                if (!containDictionary[GetStepName()].Contains("Step" + i))
                                {
                                    containDictionary[GetStepName()].Add("Step" + i);
                                    colorGroupName = "Step" + i + "Color";
                                    break;
                                }
                                i++;
                            }
                            if (i > 100000)
                            {
                                new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                                return;
                            }
                            command.Append("\tColorGroup " + colorGroupName + " = new ColorGroup(\""
                              + mContents[1] + "\",' ','-');" + Environment.NewLine
                            + "\t" + GetStepName() + "LightGroup = Edit.CopyToTheEnd(" + GetStepName() + "LightGroup," + colorGroupName + "); " + Environment.NewLine);
                        }
                    }
                }
                if (type.Equals("SportOverlay"))
                {
                    foreach (String _str in _contents)
                    {
                        String[] mContents = _str.Split('-');

                        String rangeGroupName = String.Empty;
                        int i = 1;
                        while (i <= 100000)
                        {
                            if (!containDictionary[GetStepName()].Contains("Step" + i))
                            {
                                containDictionary[GetStepName()].Add("Step" + i);
                                rangeGroupName = "Step" + i + "Range";
                                break;
                            }
                            i++;
                        }
                        if (i > 100000)
                        {
                            new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                            return;
                        }
                        command.Append("\tRangeGroup " + rangeGroupName + " = new RangeGroup(\""
                              + mContents[0] + "\",' ','-');" + Environment.NewLine
                            + "\t" + GetStepName() + "LightGroup = Edit.AccelerationOrDeceleration(" + GetStepName() + "LightGroup," + rangeGroupName + "); " + Environment.NewLine);
                    }
                }
                if (type.Equals("Other"))
                {
                    foreach (String _str in _contents)
                    {
                        if (_str.Equals("RemoveBorder"))
                        {
                            command.Append("\t" + GetStepName() + "LightGroup = Edit.RemoveBorder(" + GetStepName() + "LightGroup);" + Environment.NewLine);
                        }
                    }
                }
            }
            if (!command.ToString().Equals(String.Empty))
            {
                lightScriptDictionary[GetStepName()] += command.ToString();
                finalDictionary.Remove(GetStepName());
                RefreshData();
            }

        }

        private void RenameStepName(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                //双击时执行
                if (lbStep.SelectedIndex == -1)
                    return;
                GetStringDialog stringD = null;
                if (stringD.ShowDialog() == true)
                {
                    String oldName = GetStepName();
                    String newName = stringD.mString;
                    if (oldName.Equals(newName))
                    {
                        //和原名字相同，不改变
                        return;
                    }
                    //改变继承
                    Dictionary<String, List<String>> mExtendsDictionary = new Dictionary<String, List<String>>();
                    foreach (var item in extendsDictionary)
                    {
                        if (item.Key.Equals(oldName))
                        {
                            mExtendsDictionary.Add(newName, item.Value);
                        }
                        else
                        {
                            mExtendsDictionary.Add(item.Key, item.Value);
                        }
                    }
                    foreach (var item in mExtendsDictionary)
                    {
                        if (item.Value.Contains(oldName))
                        {
                            item.Value.Remove(oldName);
                            item.Value.Add(newName);
                        }
                    }
                    extendsDictionary = mExtendsDictionary;
                    //改变可见不可见
                    bool b = visibleDictionary[oldName];
                    visibleDictionary.Remove(oldName);
                    visibleDictionary.Add(newName, b);

                    String newScript = lightScriptDictionary[oldName];
                    //新的包含
                    List<String> ls = new List<string>();
                    //是否包含
                    if (containDictionary[oldName].Contains(newName))
                    {
                        //如果包含
                        //将原包含字段的字段替换
                        int x = 1;
                        while (x <= 100000)
                        {
                            if (!containDictionary[oldName].Contains("Step" + x))
                            {
                                //不存在重复
                                break;
                            }
                            x++;
                        }
                        if (x > 100000)
                        {
                            new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                            return;
                        }
                        newScript = newScript.Replace(newName + "Light", "Step" + x + "Light");
                        newScript = newScript.Replace(newName + "LightGroup", "Step" + x + "LightGroup");
                        newScript = newScript.Replace(newName + "Range", "Step" + x + "Range");
                        newScript = newScript.Replace(newName + "Color", "Step" + x + "Color");
                    }
                    //新名字和其他字段不冲突
                    newScript = newScript.Replace(oldName + "Light", newName + "Light");
                    newScript = newScript.Replace(oldName + "LightGroup", newName + "LightGroup");
                    newScript = newScript.Replace(oldName + "Range", newName + "Range");
                    newScript = newScript.Replace(oldName + "Color", newName + "Color");
                    ls.Add(newName);
                    foreach (String s in containDictionary[oldName])
                    {
                        ls.Add(s);
                    }
                    if (ls.Contains(oldName))
                    {
                        ls.Remove(oldName);
                    }
                    Dictionary<String, List<String>> mContainDictionary = new Dictionary<String, List<String>>();
                    List<string> mContainKey = new List<string>(containDictionary.Keys);
                    for (int i = 0; i < mContainKey.Count; i++)
                    {
                        if (i == lbStep.SelectedIndex)
                        {
                            mContainDictionary.Add(newName, ls);
                        }
                        mContainDictionary.Add(mContainKey[i], containDictionary[mContainKey[i]]);
                    }
                    containDictionary = mContainDictionary;
                    containDictionary.Remove(oldName);

                    Dictionary<String, String> mLightScriptDictionary = new Dictionary<String, String>();
                    List<string> mKey = new List<string>(lightScriptDictionary.Keys);
                    for (int i = 0; i < mKey.Count; i++)
                    {
                        if (i == lbStep.SelectedIndex)
                        {
                            mLightScriptDictionary.Add(newName, newScript);
                        }
                        mLightScriptDictionary.Add(mKey[i], lightScriptDictionary[mKey[i]]);
                    }
                    lightScriptDictionary = mLightScriptDictionary;
                    lightScriptDictionary.Remove(oldName);

                    Dictionary<string, string> _lightScriptDictionary = new Dictionary<string, string>();
                    foreach (var item in lightScriptDictionary)
                    {
                        _lightScriptDictionary.Add(item.Key, item.Value);
                    }
                    lightScriptDictionary = _lightScriptDictionary;

                    //最后界面里更改以及刷新继承
                    StackPanel panel = (StackPanel)lbStep.Items[lbStep.SelectedIndex];
                    TextBlock block = (TextBlock)panel.Children[1];
                    block.Text = newName;
                    UpdateExtends();
                    RefreshData();
                }
                e.Handled = true;
            }
        }

        private void CheckProperties(object sender, RoutedEventArgs e)
        {
            //if (lbStep.SelectedIndex == -1)
            //{
            //    return;
            //}
            //CheckPropertiesDialog propertiesDialog = new CheckPropertiesDialog(mw, RefreshData(GetStepName()));
            //propertiesDialog.ShowDialog();
        }

        private void VisiblePanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            List<String> ls = GetStepNameCollection();
            bool isMatch = false;
            foreach (var id in intersectionDictionary)
            {
                //如果是子集
                if (id.Value.Contains(GetVisibleImageStepName(sender)))
                {
                    isMatch = true;
                    break;
                }
            }
            foreach (var id in complementDictionary)
            {
                //如果是子集
                if (id.Value.Contains(GetVisibleImageStepName(sender)))
                {
                    isMatch = true;
                    break;
                }
            }
            if (isMatch)
                return;

            if (visibleDictionary[GetVisibleImageStepName(sender)])
            {
                DockPanel panel = (DockPanel)sender;
                Image visibleImage = (Image)panel.Children[0];
                visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/novisible.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                DockPanel panel = (DockPanel)sender;
                Image visibleImage = (Image)panel.Children[0];
                visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/visible.png", UriKind.RelativeOrAbsolute));
            }
            visibleDictionary[GetVisibleImageStepName(sender)] = !visibleDictionary[GetVisibleImageStepName(sender)];
            UpdateVisible();

            RefreshData();
        }

        private void CancelParent(object sender, RoutedEventArgs e)
        {
            //没有父类不需要解除
            if (GetParentName().Trim().Equals(String.Empty))
                return;
            //不能隔代解除
            foreach (var mItem in extendsDictionary)
            {
                if (mItem.Value.Contains(GetParentName()))
                {
                    System.Windows.Forms.MessageBox.Show("选中项的父亲有自己的父亲,不能隔代解除，请先解除父亲的父亲!");
                    return;
                }
            }
            //对比包含字段
            //父类字段不能与子类字段重复，否则替换
            Dictionary<String, String> _dictionary = new Dictionary<String, String>();//老字段，新字段
            List<String> newKey = new List<string>();
            foreach (String str in containDictionary[GetParentName()])
            {
                //!newKey.Contains(str) 如果没有这个条件，就会 如果同有Step3这个元素，Step3-Step4,Step4-Step4 
                if (!containDictionary[GetStepName()].Contains(str) && !newKey.Contains(str))
                {
                    //该字段不存在重复
                    _dictionary.Add(str, str);
                    newKey.Add(str);
                }
                else
                {
                    int x = 1;
                    while (x <= 100000)
                    {
                        if (!containDictionary[GetStepName()].Contains("Step" + x) && !newKey.Contains("Step" + x))
                        {
                            //不存在重复
                            _dictionary.Add(str, "Step" + x);
                            newKey.Add("Step" + x);
                            break;
                        }
                        x++;
                    }
                    if (x > 100000)
                    {
                        new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                        return;
                    }
                }
            }
            String oldName = _dictionary.Keys.First();

            //给_dictionary 根据Step排序
            ArrayList list = new ArrayList(_dictionary.Keys.ToArray());
            IComparer fileNameComparer = new FilesNameComparerClass();
            list.Sort(fileNameComparer);
            Dictionary<String, String> mDictionary = new Dictionary<String, String>();
            //一定要在这里添加，如果在下面 遍历_dictionary时添加，会导致顺序颠倒，缺少行数
            for (int i = 0; i < list.Count; i++)
            {
                if (!containDictionary[GetStepName()].Contains(list[i].ToString()))
                    containDictionary[GetStepName()].Add(list[i].ToString());
            }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                mDictionary.Add(list[i].ToString(), _dictionary[list[i].ToString()]);
            }
            _dictionary = mDictionary;

            //foreach (var item222 in _dictionary)
            //{
            //    Console.WriteLine(item222.Key + "---" + item222.Value);
            //}
            String parentCommand = lightScriptDictionary[GetParentName()];
            foreach (var item in _dictionary)
            {
                parentCommand = parentCommand.Replace(item.Key + "Light", item.Value + "Light");
                parentCommand = parentCommand.Replace(item.Key + "LightGroup", item.Value + "LightGroup");
                parentCommand = parentCommand.Replace(item.Key + "Range", item.Value + "Range");
                parentCommand = parentCommand.Replace(item.Key + "Color", item.Value + "Color");
            }
            String oldChildrenCommand = lightScriptDictionary[GetStepName()];
            String newChildrenCommand = parentCommand + Environment.NewLine + oldChildrenCommand;
            newChildrenCommand = newChildrenCommand.Replace("Parent", oldName + "LightGroup");
            //newChildrenCommand = newChildrenCommand.Replace("Parent", _dictionary.Keys.First() + "LightGroup");

            extendsDictionary[GetParentName()].Remove(GetStepName());
            lightScriptDictionary[GetStepName()] = newChildrenCommand;
            StackPanel sp = (StackPanel)lbStep.SelectedItem;
            TextBlock block = (TextBlock)sp.Children[3];
            block.Text = String.Empty;

            RefreshData();
        }


        private void EditPart(object sender, RoutedEventArgs e)
        {
            MainControlWindow control = new MainControlWindow(mw, RefreshData(GetStepName()), true);
            if (control.ShowDialog() == true)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("\tLightGroup " + GetStepName() + "LightGroup = new LightGroup();" + Environment.NewLine);
                int i = 1;
                foreach (Light l in control.mLightList)
                {
                    builder.Append("\tLight light" + i + " = new LightGroup(" + l.Time + "," + l.Action + "," + l.Position + "," + l.Color + ");" + Environment.NewLine);
                    builder.Append("\t" + GetStepName() + "LightGroup.Add(" + "light" + i + ");" + Environment.NewLine);
                    i++;
                }
                lightScriptDictionary[GetStepName()] = builder.ToString();
                RefreshData();
            }
        }
        /// <summary>
        /// 获取步骤集合
        /// </summary>
        /// <returns></returns>
        public List<String> GetStepNameCollection()
        {
            List<String> lsGetName = new List<string>();
            for (int i = 0; i < lbStep.Items.Count; i++)
            {
                StackPanel sp = (StackPanel)lbStep.Items[i];
                TextBlock blockStepName = (TextBlock)sp.Children[1];
                lsGetName.Add(blockStepName.Text);
            }
            return lsGetName;
        }
        /// <summary>
        /// 获取步骤名
        /// </summary>
        /// <returns></returns>
        public String GetStepName()
        {
            StackPanel sp = (StackPanel)lbStep.SelectedItem;
            TextBlock blockStepName = (TextBlock)sp.Children[1];
            return blockStepName.Text;
        }
        /// <summary>
        /// 获取步骤名
        /// </summary>
        /// <returns></returns>
        public String GetStepName(int position)
        {
            StackPanel sp = (StackPanel)lbStep.Items[position];
            TextBlock blockStepName = (TextBlock)sp.Children[1];
            return blockStepName.Text;
        }
        /// <summary>
        /// 获取步骤名
        /// </summary>
        /// <returns></returns>
        public String GetStepName(StackPanel sp)
        {
            TextBlock blockStepName = (TextBlock)sp.Children[1];
            return blockStepName.Text;
        }
        /// <summary>
        /// 获取步骤父类名
        /// </summary>
        /// <returns></returns>
        private String GetParentName()
        {
            StackPanel sp = (StackPanel)lbStep.SelectedItem;
            TextBlock blockParentName = (TextBlock)sp.Children[3];
            return blockParentName.Text;
        }
        /// <summary>
        /// 获取步骤父类名
        /// </summary>
        /// <returns></returns>
        private String GetParentName(StackPanel sp)
        {
            TextBlock blockParentName = (TextBlock)sp.Children[3];
            return blockParentName.Text;
        }
        /// <summary>
        /// 是否包含该步骤名
        /// </summary>
        /// <param name="StepName"></param>
        /// <returns></returns>
        private bool ContainsStepName(String StepName)
        {
            for (int i = 0; i < lbStep.Items.Count; i++)
            {
                StackPanel sp = (StackPanel)lbStep.Items[i];
                if (sp.Children[1] is TextBlock blockStepName)
                {
                    if (blockStepName.Text.Equals(StepName))
                        return true;
                }
            }
            return false;
        }

        private void ShowPart(object sender, RoutedEventArgs e)
        {
            if (lbStep.SelectedIndex == -1)
                return;

            //ControlScriptDialog dialog = new ControlScriptDialog(this);
            //if (dialog.ShowDialog() == true)
            //{
            //    StringBuilder builder = new StringBuilder();
            //    for (int i = 0; i < dialog.lbMain.Items.Count; i++)
            //    {
            //        if (i != dialog.lbMain.Items.Count - 1)
            //        {
            //            builder.Append("\t" + dialog.lbMain.Items[i].ToString() + Environment.NewLine);
            //        }
            //        else
            //        {
            //            builder.Append("\t" + dialog.lbMain.Items[i].ToString());
            //        }
            //    }
            //    lightScriptDictionary[GetStepName()] = builder.ToString();
            //    RefreshData();
            //}

        }

        private void NewStep(object sender, RoutedEventArgs e)
        {
            String stepName = String.Empty;
            //从1开始计算
            int i = 1;
            while (i <= 100000)//最多100000步
            {
                if (!ContainsStepName("Step" + i))
                {
                    stepName = "Step" + i;
                    break;
                }
                i++;
            }
            if (stepName.Equals(String.Empty))
            {
                new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                return;
            }
            lightScriptDictionary.Add(stepName, "");
            visibleDictionary.Add(stepName, true);
            containDictionary.Add(stepName, new List<string>() { stepName });
            AddStep(stepName, "");
            //如果选中，就在列表选中最后一个
            lbStep.SelectedIndex = lbStep.Items.Count - 1;
            RefreshData();
        }

        private void CopyStep(object sender, RoutedEventArgs e)
        {
            while (lbStep.SelectedItems.Count > 0)
            {
                String stepName = String.Empty;
                //从1开始计算
                int i = 1;
                while (i <= 100000)//最多100000步
                {
                    if (!ContainsStepName("Step" + i))
                    {
                        stepName = "Step" + i;
                        break;
                    }
                    i++;
                }
                if (stepName.Equals(String.Empty))
                {
                    new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                    return;
                }

                StackPanel sp = (StackPanel)lbStep.SelectedItems[0];
                String oldName = GetStepName(sp);
                String oldParent = GetParentName(sp);
                //改变继承
                foreach (var item in extendsDictionary)
                {
                    if (item.Value.Contains(oldName))
                    {
                        item.Value.Add(stepName);
                    }
                }
                //改变可见不可见
                bool b = visibleDictionary[oldName];
                visibleDictionary.Add(stepName, b);

                String newScript = lightScriptDictionary[oldName];
                //新的包含
                List<String> ls = new List<string>();
                //是否包含
                if (containDictionary[oldName].Contains(stepName))
                {
                    //如果包含
                    //将原包含字段的字段替换
                    int x = 1;
                    while (x <= 100000)
                    {
                        if (!containDictionary[oldName].Contains("Step" + x))
                        {
                            //不存在重复
                            break;
                        }
                        x++;
                    }
                    if (x > 100000)
                    {
                        new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                        return;
                    }
                    newScript = newScript.Replace(stepName + "Light", "Step" + x + "Light");
                    newScript = newScript.Replace(stepName + "LightGroup", "Step" + x + "LightGroup");
                    newScript = newScript.Replace(stepName + "Range", "Step" + x + "Range");
                    newScript = newScript.Replace(stepName + "Color", "Step" + x + "Color");
                }
                //新名字和其他字段不冲突
                newScript = newScript.Replace(oldName + "Light", stepName + "Light");
                newScript = newScript.Replace(oldName + "LightGroup", stepName + "LightGroup");
                newScript = newScript.Replace(oldName + "Range", stepName + "Range");
                newScript = newScript.Replace(oldName + "Color", stepName + "Color");
                ls.Add(stepName);
                foreach (String s in containDictionary[oldName])
                {
                    ls.Add(s);
                }
                if (ls.Contains(oldName))
                {
                    ls.Remove(oldName);
                }
                containDictionary.Add(stepName, ls);
                lightScriptDictionary.Add(stepName, newScript);
                //决定样式还是复制吧。
                if (finalDictionary.ContainsKey(oldName))
                {
                    finalDictionary.Add(stepName, finalDictionary[oldName]);
                }
                //最后界面里更改以及刷新继承
                AddStep(stepName, oldParent);
                lbStep.SelectedItems.Remove(lbStep.SelectedItems[0]);
                //UpdateExtends();
            }
            UpdateVisible();
            RefreshData();
        }

        private void DelStep(object sender, RoutedEventArgs e)
        {
            DelStep();
        }
        private void DelStep()
        {
            if (lbStep.SelectedIndex == -1)
                return;
           
            System.Windows.Forms.MessageBoxButtons mssBoxBt = System.Windows.Forms.MessageBoxButtons.OKCancel;
            System.Windows.Forms.MessageBoxIcon mssIcon = System.Windows.Forms.MessageBoxIcon.Warning;
            System.Windows.Forms.MessageBoxDefaultButton mssDefbt = System.Windows.Forms.MessageBoxDefaultButton.Button1;
            System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("是否删除\"" + GetStepName() + "\"", "提示", mssBoxBt, mssIcon, mssDefbt);
            if (dr == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            List<int> selectIndexList = new List<int>();
            for (int i = 0; i < lbStep.SelectedItems.Count; i++)
            {
                if (lockedDictionary.ContainsKey(GetStepName(lbStep.Items.IndexOf(lbStep.SelectedItems[i]))))
                {
                    new MessageDialog(mw, "TheStepIsLocked").ShowDialog();
                    continue;
                }
                selectIndexList.Add(lbStep.Items.IndexOf(lbStep.SelectedItems[i]));
            }
            selectIndexList.Sort();
            lbStep.SelectedItems.Clear();
            foreach (int i in selectIndexList)
            {
                lbStep.SelectedItems.Add(lbStep.Items[i]);
            }
            while (lbStep.SelectedItems.Count > 0)
            {
                StackPanel panel = (StackPanel)lbStep.SelectedItems[0];
                String stepName = GetStepName(panel);

                String parentName = GetParentName(panel);
                if (extendsDictionary.ContainsKey(stepName))
                {
                    if (extendsDictionary[stepName].Count != 0)
                    {
                        System.Windows.Forms.MessageBox.Show("选中项为其他项的父类，请先解除父子关系之后再删除!");
                        return;
                    }
                }
                if (!parentName.Equals(String.Empty))
                    extendsDictionary[parentName].Remove(stepName);
                lightScriptDictionary.Remove(stepName);

                Dictionary<string, string> _lightScriptDictionary = new Dictionary<string, string>();
                foreach (var item in lightScriptDictionary)
                {
                    _lightScriptDictionary.Add(item.Key, item.Value);
                }
                lightScriptDictionary = _lightScriptDictionary;

                visibleDictionary.Remove(stepName);
                containDictionary.Remove(stepName);
                finalDictionary.Remove(stepName);

                String _parentName = String.Empty;
                //交集
                foreach (var item in intersectionDictionary)
                {
                    if (item.Value.Contains(stepName))
                    {
                        item.Value.Remove(stepName);
                        _parentName = item.Key;
                        break;
                    }
                }
                if (!_parentName.Equals(String.Empty))
                {
                    if (intersectionDictionary[_parentName].Count == 0)
                    {
                        intersectionDictionary.Remove(_parentName);
                    }
                }
                if (intersectionDictionary.ContainsKey(stepName))
                {
                    intersectionDictionary.Remove(stepName);
                }
                //补集
                _parentName = String.Empty;
                foreach (var item in complementDictionary)
                {
                    if (item.Value.Contains(stepName))
                    {
                        item.Value.Remove(stepName);
                        _parentName = item.Key;
                        break;
                    }
                }
                if (!_parentName.Equals(String.Empty))
                {
                    if (complementDictionary[_parentName].Count == 0)
                    {
                        complementDictionary.Remove(_parentName);
                    }
                }
                if (complementDictionary.ContainsKey(stepName))
                {
                    complementDictionary.Remove(stepName);
                }
                //lbStep.Items.RemoveAt(lbStep.SelectedIndex);
                //lbStep.SelectedIndex = -1;
                lbStep.Items.Remove(lbStep.SelectedItems[0]);
                //lbStep.SelectedItems.Remove(lbStep.SelectedItems[0]);
            }
            RefreshData();
        }
        private void MergeStep(object sender, RoutedEventArgs e)
        {
            if (lbStep.SelectedIndex == -1)
                return;
            if (lbStep.SelectedItems.Count <= 1)
                return;

            System.Windows.Forms.MessageBoxButtons mssBoxBt = System.Windows.Forms.MessageBoxButtons.OKCancel;
            System.Windows.Forms.MessageBoxIcon mssIcon = System.Windows.Forms.MessageBoxIcon.Warning;
            System.Windows.Forms.MessageBoxDefaultButton mssDefbt = System.Windows.Forms.MessageBoxDefaultButton.Button1;
            System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("是否合并", "提示", mssBoxBt, mssIcon, mssDefbt);
            if (dr == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            List<int> selectIndexList = new List<int>();
            for (int i = 0; i < lbStep.SelectedItems.Count; i++)
            {
                selectIndexList.Add(lbStep.Items.IndexOf(lbStep.SelectedItems[i]));
            }
            selectIndexList.Sort();
            lbStep.SelectedItems.Clear();
            foreach (int i in selectIndexList)
            {
                StackPanel sp = (StackPanel)lbStep.Items[i];
                if (!GetParentName(sp).Equals(String.Empty))
                {
                    lbStep.SelectedItem = lbStep.Items[i];
                    System.Windows.Forms.MessageBox.Show("请先取消与父类的关系");
                    return;
                }
                if (extendsDictionary.ContainsKey(GetStepName(sp)))
                {
                    if (extendsDictionary[GetStepName(sp)].Count > 0)
                    {
                        lbStep.SelectedItem = lbStep.Items[i];
                        System.Windows.Forms.MessageBox.Show("该步骤有子类，不允许合并!");
                        return;
                    }
                }
                if (finalDictionary.ContainsKey(GetStepName(sp)))
                {
                    lbStep.SelectedItem = lbStep.Items[i];
                    System.Windows.Forms.MessageBox.Show("该步骤有样式，请先将样式融入步骤!");
                    return;
                }
                if (intersectionDictionary.ContainsKey(GetStepName(sp)))
                {
                    lbStep.SelectedItem = lbStep.Items[i];
                    System.Windows.Forms.MessageBox.Show("该步骤有(交)子集，请先将集合融入步骤!");
                    return;
                }
                foreach (var item in intersectionDictionary)
                {
                    if (item.Value.Contains(GetStepName(sp)))
                    {
                        lbStep.SelectedItem = lbStep.Items[i];
                        System.Windows.Forms.MessageBox.Show("该步骤是(交)子集，请先将集合融入步骤!");
                        return;
                    }
                }
                if (complementDictionary.ContainsKey(GetStepName(sp)))
                {
                    lbStep.SelectedItem = lbStep.Items[i];
                    System.Windows.Forms.MessageBox.Show("该步骤有(补)子集，请先将集合融入步骤!");
                    return;
                }
                foreach (var item in complementDictionary)
                {
                    if (item.Value.Contains(GetStepName(sp)))
                    {
                        lbStep.SelectedItem = lbStep.Items[i];
                        System.Windows.Forms.MessageBox.Show("该步骤是(补)子集，请先将集合融入步骤!");
                        return;
                    }
                }
                if (lockedDictionary.ContainsKey(GetStepName(sp)))
                {
                    lbStep.SelectedItem = lbStep.Items[i];
                    System.Windows.Forms.MessageBox.Show("该步骤已被锁定，请先解锁!");
                    return;
                }
                lbStep.SelectedItems.Add(lbStep.Items[i]);
            }
            while (lbStep.SelectedItems.Count > 1)
            {
                //Console.WriteLine("--------");
                //第1个被选中的融入第0个被选中的 后面融入前面
                //对比包含字段
                //被包含类字段不能与包含类字段重复，否则替换 
                List<String> newKey = new List<string>();
                Dictionary<String, String> _dictionary = new Dictionary<String, String>();//老字段，新字段
                StackPanel oldPanel = (StackPanel)lbStep.SelectedItems[1];
                StackPanel newPanel = (StackPanel)lbStep.SelectedItems[0];

                foreach (String str in containDictionary[GetStepName(oldPanel)])
                {
                    if (!containDictionary[GetStepName(newPanel)].Contains(str))
                    {
                        //该字段不存在重复
                        _dictionary.Add(str, str);
                        newKey.Add(str);
                    }
                    else
                    {
                        int x = 1;
                        while (x <= 100000)
                        {
                            if (!containDictionary[GetStepName(newPanel)].Contains("Step" + x) && !newKey.Contains("Step" + x))
                            {
                                //不存在重复
                                _dictionary.Add(str, "Step" + x);
                                newKey.Add("Step" + x);
                                break;
                            }
                            x++;
                        }
                        if (x > 100000)
                        {
                            new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                            return;
                        }
                    }
                }
                String parentCommand = lightScriptDictionary[GetStepName(oldPanel)];
                foreach (var item in _dictionary)
                {
                    parentCommand = parentCommand.Replace(item.Key + "Light", item.Value + "Light");
                    parentCommand = parentCommand.Replace(item.Key + "LightGroup", item.Value + "LightGroup");
                    parentCommand = parentCommand.Replace(item.Key + "Range", item.Value + "Range");
                    parentCommand = parentCommand.Replace(item.Key + "Color", item.Value + "Color");
                    containDictionary[GetStepName(newPanel)].Add(item.Value);
                }
                String oldChildrenCommand = lightScriptDictionary[GetStepName(newPanel)];
                String newChildrenCommand = parentCommand + Environment.NewLine + oldChildrenCommand;
                newChildrenCommand += Environment.NewLine + "\t" + GetStepName(newPanel) + "LightGroup.Add(" + _dictionary.Values.First() + "LightGroup);";

                lightScriptDictionary[GetStepName(newPanel)] = newChildrenCommand;
                lightScriptDictionary.Remove(GetStepName(oldPanel));

                Dictionary<string, string> _lightScriptDictionary = new Dictionary<string, string>();
                foreach (var item in lightScriptDictionary)
                {
                    _lightScriptDictionary.Add(item.Key, item.Value);
                }
                lightScriptDictionary = _lightScriptDictionary;

                visibleDictionary.Remove(GetStepName(oldPanel));
                containDictionary.Remove(GetStepName(oldPanel));
                lbStep.Items.Remove(lbStep.SelectedItems[1]);
            }
            RefreshData();
        }
        private String GetVisibleImageStepName(Object sender)
        {
            DockPanel dock = (DockPanel)sender;
            StackPanel panel = (StackPanel)dock.Parent;
            TextBlock blockStepName = (TextBlock)panel.Children[1];
            return blockStepName.Text;
        }
        public void SetSize(double width, double height)
        {
            Width = width;
            Height = height;
        }
        public void UpdateData(List<Light> mLightList)
        {
            //bridge.UpdateData(mLightList);
        }
        private void BtnLastTimePoint_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            bridge.ToLastTime();
        }
        private void BtnNextTimePoint_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            bridge.ToNextTime();
        }
        private void TimePointCountLeft_TextChanged(object sender, TextChangedEventArgs e)
        {
            bridge.tbTimePointCountLeft_TextChanged();
        }

        public void SetLaunchpadSize() {
            double minSize = dpShow.ActualWidth < dpShow.ActualHeight - 70 - 52 ? dpShow.ActualWidth : dpShow.ActualHeight - 70 - 52;
            mLaunchpad.Size = minSize;
        }
        private void DockPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (mShow == ShowMode.Launchpad)
            {
                SetLaunchpadSize();
            }
            else if (mShow == ShowMode.DataGrid)
            {
                dgMain.Width = dpShow.ActualWidth;
                dgMain.Height = dpShow.ActualHeight;

                //viewBusiness.SetBackGroundFromWidth(dgMain.Width);
            }
        }

        private void ClearInput(object sender, RoutedEventArgs e)
        {
        }

        private void ShowRangeList(object sender, RoutedEventArgs e)
        {
            ShowRangeListDialog dialog = null;
            if (dialog.ShowDialog() == true)
            {
                if (sender == btnFastGenerationrRange)
                {
                    String[] str = rangeDictionary.Keys.ToArray();
                    tbFastGenerationrRange.Text = str[dialog.lbMain.SelectedIndex];
                }
                if (sender == btnFastGenerationrColor)
                {
                    String[] str = rangeDictionary.Keys.ToArray();
                    tbFastGenerationrColor.Text = str[dialog.lbMain.SelectedIndex];
                }
                if (sender == btnIfPositionRange)
                {
                    String[] str = rangeDictionary.Keys.ToArray();
                    tbIfPosition.Text = str[dialog.lbMain.SelectedIndex];
                }
                if (sender == btnIfColor)
                {
                    String[] str = rangeDictionary.Keys.ToArray();
                    tbIfColor.Text = str[dialog.lbMain.SelectedIndex];
                }
            }
            SaveRangeFile();
        }

        private void SaveRangeFile()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in rangeDictionary)
            {
                builder.Append(item.Key + ":");
                for (int i = 0; i < item.Value.Count; i++)
                {
                    if (i == item.Value.Count - 1)
                    {
                        builder.Append(item.Value[i] + ";" + Environment.NewLine);
                    }
                    else
                    {
                        builder.Append(item.Value[i] + ",");
                    }
                }
            }
            String filePath = AppDomain.CurrentDomain.BaseDirectory + @"RangeList\test.Range";
            //获得文件路径
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            FileStream f = new FileStream(filePath, FileMode.OpenOrCreate);
            for (int j = 0; j < builder.Length; j++)
            {
                //Console.WriteLine((int)line[j]);
                f.WriteByte((byte)builder[j]);
            }
            f.Close();
        }

      
        private void LbStep_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (lbStep.SelectedIndex == -1)
            //    return;
            //StyleWindow style = new StyleWindow(mw);
            //if (finalDictionary.ContainsKey(GetStepName()))
            //{
            //    style.SetData(finalDictionary[GetStepName()]);
            //}
            //else
            //{
            //    style.SetData("");
            //}
            //if (style.ShowDialog() == true)
            //{
            //    if (finalDictionary.ContainsKey(GetStepName()))
            //    {
            //        if (!style._Content.Equals(String.Empty))
            //        {
            //            finalDictionary[GetStepName()] = style._Content;
            //        }
            //        else
            //        {
            //            finalDictionary.Remove(GetStepName());
            //        }
            //    }
            //    else
            //    {
            //        if (!style._Content.Equals(String.Empty))
            //            finalDictionary.Add(GetStepName(), style._Content);
            //    }
            //    RefreshData();
            //}
        }

        private void DrawRange(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DrawRangeDialog dialog = new DrawRangeDialog(mw);
            StringBuilder builder = new StringBuilder();
            if (dialog.ShowDialog() == true)
            {
                foreach (int i in dialog.Content)
                {
                    builder.Append(i + " ");
                }
                if (sender == btnFastGenerationrDraw)
                {
                    tbFastGenerationrRange.Text = builder.ToString().Trim();
                }
                if (sender == btnIfPositionDraw)
                {
                    tbIfPosition.Text = builder.ToString().Trim();
                }
            }
        }

        private void LbStep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbStep.SelectedIndex == -1)
            {
                bridge.UpdateForColor(mLightList,false);
                AddStepControlToolTip();
            }
            else
            {
                bridge.UpdateForColor(RefreshData(GetStepName()),true);
                spStepControl.ToolTip = null;
            }
        }
        private void AddStepControlToolTip() {
            if (mw.strMyLanguage.Equals("zh-CN"))
                spStepControl.ToolTip = "你需要选中步骤";
            else
                spStepControl.ToolTip = "You need to select the step";
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            if (toolBar.Template.FindName("OverflowGrid", toolBar) is FrameworkElement overflowGrid)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
            if (toolBar.Template.FindName("MainPanelBorder", toolBar) is FrameworkElement mainPanelBorder)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }

        private void UserControl_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                DelStep();
            }
            if (e.KeyboardDevice.Modifiers == System.Windows.Input.ModifierKeys.Control && e.Key == System.Windows.Input.Key.Z)
            {
                Unmake();
            }
            if (e.KeyboardDevice.Modifiers == System.Windows.Input.ModifierKeys.Control && e.Key == System.Windows.Input.Key.Y)
            {
                Redo();
            }
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (lbStep.Items.Count == 0 || lbStep.SelectedIndex == -1)
            {
                lbStepMenu.IsOpen = false;
            }
        }

       
        private void SetCollection(object sender, RoutedEventArgs e)
        {
            if (lbStep.SelectedIndex == -1 || lbStep.Items.Count < 2)
                return;
            if (!GetParentName().Equals(String.Empty))
            {
                System.Windows.Forms.MessageBox.Show("请先取消与父类的关系");
                return;
            }
            if (extendsDictionary.ContainsKey(GetStepName()))
            {
                if (extendsDictionary[GetStepName()].Count > 0)
                {
                    System.Windows.Forms.MessageBox.Show("该步骤有子类，不允许集合操作!");
                    return;
                }
            }
            String _name = String.Empty;
            CollectionType type = CollectionType.Intersection;
            foreach (var item in intersectionDictionary)
            {
                if (item.Value.Contains(GetStepName()))
                {
                    _name = item.Key;
                    type = CollectionType.Intersection;
                    break;
                }
            }
            foreach (var item in complementDictionary)
            {
                if (item.Value.Contains(GetStepName()))
                {
                    _name = item.Key;
                    type = CollectionType.Complement;
                    break;
                }
            }
            CollectionDialog dialog = null;
            if (_name.Equals(String.Empty))
            {
                //dialog = new CollectionDialog(this);
            }
            else
            {
                //dialog = new CollectionDialog(this, _name, type);
            }
            if (dialog.ShowDialog() == true)
            {
                if (!_name.Equals(String.Empty))
                {
                    //去除集合关系
                    if (type == CollectionType.Intersection)
                    {
                        intersectionDictionary[_name].Remove(GetStepName());
                        if (intersectionDictionary[_name].Count == 0)
                        {
                            intersectionDictionary.Remove(_name);
                        }
                    }
                    else if (type == CollectionType.Complement)
                    {
                        complementDictionary[_name].Remove(GetStepName());
                        if (complementDictionary[_name].Count == 0)
                        {
                            complementDictionary.Remove(_name);
                        }
                    }
                }
                String parentName = dialog.cbLightName.SelectedItem.ToString();
                if (dialog.cbType.SelectedIndex == 1)
                {
                    if (intersectionDictionary.ContainsKey(parentName))
                    {
                        intersectionDictionary[parentName].Add(GetStepName());
                    }
                    else
                    {
                        intersectionDictionary.Add(parentName, new List<string>() { GetStepName() });
                    }
                }
                else if (dialog.cbType.SelectedIndex == 2)
                {
                    if (complementDictionary.ContainsKey(parentName))
                    {
                        complementDictionary[parentName].Add(GetStepName());
                    }
                    else
                    {
                        complementDictionary.Add(parentName, new List<string>() { GetStepName() });
                    }
                }
                if (dialog.cbType.SelectedIndex != 0)
                {
                    //交换位置
                    List<String> ls = GetStepNameCollection();
                    int pPosition = ls.IndexOf(parentName);
                    int cPosition = ls.IndexOf(GetStepName());
                    if (cPosition > pPosition + 1)
                    {
                        //界面交换
                        List<StackPanel> stackPanels = new List<StackPanel>();
                        foreach (Object o in lbStep.Items)
                        {
                            stackPanels.Add((StackPanel)o);
                        }
                        lbStep.Items.Clear();
                        StackPanel sp = stackPanels[pPosition + 1];
                        stackPanels[pPosition + 1] = stackPanels[cPosition];
                        stackPanels[cPosition] = sp;

                        foreach (StackPanel s in stackPanels)
                        {
                            lbStep.Items.Add(s);
                        }
                        lbStep.SelectedIndex = pPosition + 1;
                        //数据交换
                        Dictionary<String, String> mLightScriptDictionary = new Dictionary<String, String>();
                        List<string> mKey = new List<string>(lightScriptDictionary.Keys);
                        String strUp = lightScriptDictionary[mKey[pPosition + 1]];
                        String strDown = lightScriptDictionary[mKey[cPosition]];
                        for (int i = 0; i < mKey.Count; i++)
                        {
                            if (i == pPosition + 1)
                            {
                                mLightScriptDictionary.Add(mKey[cPosition], strDown);
                            }
                            else if (i == cPosition)
                            {
                                mLightScriptDictionary.Add(mKey[pPosition + 1], strUp);
                            }
                            else
                            {
                                mLightScriptDictionary.Add(mKey[i], lightScriptDictionary[mKey[i]]);
                            }
                        }
                        lightScriptDictionary = mLightScriptDictionary;
                    }
                    else if (cPosition == pPosition - 1)
                    {
                        //界面交换
                        List<StackPanel> stackPanels = new List<StackPanel>();
                        foreach (Object o in lbStep.Items)
                        {
                            stackPanels.Add((StackPanel)o);
                        }
                        lbStep.Items.Clear();
                        StackPanel sp = stackPanels[pPosition];
                        stackPanels[pPosition] = stackPanels[cPosition];
                        stackPanels[cPosition] = sp;

                        foreach (StackPanel s in stackPanels)
                        {
                            lbStep.Items.Add(s);
                        }
                        lbStep.SelectedIndex = pPosition + 1;
                        //数据交换
                        Dictionary<String, String> mLightScriptDictionary = new Dictionary<String, String>();
                        List<string> mKey = new List<string>(lightScriptDictionary.Keys);
                        String strUp = lightScriptDictionary[mKey[pPosition]];
                        String strDown = lightScriptDictionary[mKey[cPosition]];
                        for (int i = 0; i < mKey.Count; i++)
                        {
                            if (i == pPosition)
                            {
                                mLightScriptDictionary.Add(mKey[cPosition], strDown);
                            }
                            else if (i == cPosition)
                            {
                                mLightScriptDictionary.Add(mKey[pPosition], strUp);
                            }
                            else
                            {
                                mLightScriptDictionary.Add(mKey[i], lightScriptDictionary[mKey[i]]);
                            }
                        }
                        lightScriptDictionary = mLightScriptDictionary;
                    }
                    else if (cPosition < pPosition - 1)
                    {
                        //界面交换
                        List<StackPanel> stackPanels = new List<StackPanel>();
                        foreach (Object o in lbStep.Items)
                        {
                            stackPanels.Add((StackPanel)o);
                        }
                        lbStep.Items.Clear();
                        //抽取子集
                        StackPanel sp = stackPanels[cPosition];
                        stackPanels.RemoveAt(cPosition);
                        foreach (StackPanel s in stackPanels)
                        {
                            lbStep.Items.Add(s);
                        }
                        lbStep.Items.Insert(pPosition, sp);
                        //数据交换
                        Dictionary<String, String> mLightScriptDictionary = new Dictionary<String, String>();
                        List<string> mKey = new List<string>(lightScriptDictionary.Keys);
                        //抽取子集
                        String nowKey = mKey[cPosition];
                        String strDown = lightScriptDictionary[mKey[cPosition]];
                        mKey.RemoveAt(cPosition);
                        for (int i = 0; i < mKey.Count; i++)
                        {
                            if (i == pPosition)
                            {
                                mLightScriptDictionary.Add(nowKey, strDown);
                            }
                            mLightScriptDictionary.Add(mKey[i], lightScriptDictionary[mKey[i]]);
                        }
                        lightScriptDictionary = mLightScriptDictionary;
                    }
                }

                UpdateIntersection();
                RefreshData();
            }
        }

        private void CollectionIntegration(object sender, RoutedEventArgs e)
        {
            String parentName = String.Empty;
            String childName = String.Empty;
            CollectionType type = CollectionType.Intersection;
            foreach (var item in intersectionDictionary)
            {
                if (item.Value.Contains(GetStepName()))
                {
                    parentName = item.Key;
                    childName = GetStepName();
                    type = CollectionType.Intersection;
                }
            }
            if (intersectionDictionary.ContainsKey(GetStepName()))
            {
                parentName = GetStepName();
                childName = intersectionDictionary[GetStepName()][0];
                type = CollectionType.Intersection;
            }
            foreach (var item in complementDictionary)
            {
                if (item.Value.Contains(GetStepName()))
                {
                    parentName = item.Key;
                    childName = GetStepName();
                    type = CollectionType.Complement;
                }
            }
            if (complementDictionary.ContainsKey(GetStepName()))
            {
                parentName = GetStepName();
                childName = complementDictionary[GetStepName()][0];
                type = CollectionType.Complement;
            }
            if (parentName.Equals(String.Empty))
            {
                return;
            }
            List<String> ls = GetStepNameCollection();
            int pPosition = ls.IndexOf(parentName);
            int cPosition = ls.IndexOf(childName);
            List<String> newKey = new List<string>();
            Dictionary<String, String> _dictionary = new Dictionary<String, String>();//老字段，新字段
            StackPanel oldPanel = (StackPanel)lbStep.Items[pPosition];
            StackPanel newPanel = (StackPanel)lbStep.Items[cPosition];

            foreach (String str in containDictionary[GetStepName(oldPanel)])
            {
                if (!containDictionary[GetStepName(newPanel)].Contains(str))
                {
                    //该字段不存在重复
                    _dictionary.Add(str, str);
                    newKey.Add(str);
                }
                else
                {
                    int x = 1;
                    while (x <= 100000)
                    {
                        if (!containDictionary[GetStepName(newPanel)].Contains("Step" + x) && !newKey.Contains("Step" + x))
                        {
                            //不存在重复
                            _dictionary.Add(str, "Step" + x);
                            newKey.Add("Step" + x);
                            break;
                        }
                        x++;
                    }
                    if (x > 100000)
                    {
                        new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                        return;
                    }
                }
            }
            String parentCommand = lightScriptDictionary[GetStepName(oldPanel)];
            foreach (var item in _dictionary)
            {
                parentCommand = parentCommand.Replace(item.Key + "Light", item.Value + "Light");
                parentCommand = parentCommand.Replace(item.Key + "LightGroup", item.Value + "LightGroup");
                parentCommand = parentCommand.Replace(item.Key + "Range", item.Value + "Range");
                parentCommand = parentCommand.Replace(item.Key + "Color", item.Value + "Color");
                containDictionary[GetStepName(newPanel)].Add(item.Value);
            }
            String oldChildrenCommand = lightScriptDictionary[GetStepName(newPanel)];
            String newChildrenCommand = parentCommand + Environment.NewLine + oldChildrenCommand;
            newChildrenCommand += Environment.NewLine + "\t" + GetStepName(newPanel) + "LightGroup.Add(" + _dictionary.Values.First() + "LightGroup);";
            if (type == CollectionType.Intersection)
            {
                newChildrenCommand += Environment.NewLine + "\t" + GetStepName(newPanel) + "LightGroup = Create.Intersection(" + GetStepName(newPanel) + "LightGroup," + _dictionary.Values.First() + "LightGroup);";
            }
            else if (type == CollectionType.Complement)
            {
                newChildrenCommand += Environment.NewLine + "\t" + GetStepName(newPanel) + "LightGroup = Create.Complement(" + GetStepName(newPanel) + "LightGroup," + _dictionary.Values.First() + "LightGroup);";
            }

            lightScriptDictionary[GetStepName(newPanel)] = newChildrenCommand;
            lightScriptDictionary.Remove(GetStepName(oldPanel));

            Dictionary<string, string> _lightScriptDictionary = new Dictionary<string, string>();
            foreach (var item in lightScriptDictionary)
            {
                _lightScriptDictionary.Add(item.Key, item.Value);
            }
            lightScriptDictionary = _lightScriptDictionary;

            visibleDictionary.Remove(GetStepName(oldPanel));
            containDictionary.Remove(GetStepName(oldPanel));
            lbStep.Items.Remove(lbStep.Items[cPosition]);

            String _parentName = String.Empty;
            foreach (var item in intersectionDictionary)
            {
                if (item.Value.Contains(childName))
                {
                    item.Value.Remove(childName);
                    _parentName = item.Key;
                    break;
                }
            }
            if (!_parentName.Equals(String.Empty))
            {
                if (intersectionDictionary[_parentName].Count == 0)
                {
                    intersectionDictionary.Remove(_parentName);
                }
            }
            if (intersectionDictionary.ContainsKey(childName))
            {
                intersectionDictionary.Remove(childName);
            }
            foreach (var item in complementDictionary)
            {
                if (item.Value.Contains(childName))
                {
                    item.Value.Remove(childName);
                    _parentName = item.Key;
                    break;
                }
            }
            if (!_parentName.Equals(String.Empty))
            {
                if (complementDictionary[_parentName].Count == 0)
                {
                    complementDictionary.Remove(_parentName);
                }
            }
            if (complementDictionary.ContainsKey(childName))
            {
                complementDictionary.Remove(childName);
            }
            RefreshData();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (rangeDictionary.ContainsKey(tb.Text))
            {
                StringBuilder builder = new StringBuilder();
                foreach (int i in rangeDictionary[tb.Text])
                {
                    builder.Append(i + " ");
                }
                tb.ToolTip = builder.ToString().Trim();
            }
            else
            {
                tb.ToolTip = tb.Text;
            }
        }

        private void EditToScript(object sender, RoutedEventArgs e)
        {
            HideAllPopup();

            //没有可操作的步骤
            if (lbStep.SelectedIndex == -1)
                return;

            for (int k = 0; k < lbStep.SelectedItems.Count; k++)
            {
                StackPanel sp = (StackPanel)lbStep.SelectedItems[k];
                if (lockedDictionary.ContainsKey(GetStepName(sp)))
                {
                    new MessageDialog(mw, "TheStepIsLocked").ShowDialog();
                    continue;
                }
                //没有可操作的灯光组
                if (!lightScriptDictionary[GetStepName(sp)].Contains(GetStepName(sp) + "LightGroup"))
                {
                    continue;
                }
                String command = String.Empty;
                if (sender == btnHorizontalFlipping)
                {
                    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.HorizontalFlipping(" + GetStepName(sp) + "LightGroup);";
                }
                if (sender == btnVerticalFlipping)
                {
                    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.VerticalFlipping(" + GetStepName(sp) + "LightGroup);";
                }
                if (sender == btnLowerLeftSlashFlipping)
                {
                    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.LowerLeftSlashFlipping(" + GetStepName(sp) + "LightGroup);";
                }
                if (sender == btnLowerRightSlashFlipping)
                {
                    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.LowerRightSlashFlipping(" + GetStepName(sp) + "LightGroup);";
                }
                if (sender == btnFold)
                {
                    //Edit_FoldDialog dialog = new Edit_FoldDialog(mw, GetStepName(sp));
                    //if (dialog.ShowDialog() == true)
                    //{
                    //    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.Fold(" + GetStepName(sp) + "LightGroup,";
                    //    if (dialog.cbOrientation.SelectedIndex == 0)
                    //    {
                    //        command += "Horizontal,";
                    //    }
                    //    else if (dialog.cbOrientation.SelectedIndex == 1)
                    //    {
                    //        command += "Vertical,";
                    //    }
                    //    command += dialog.tbStartPosition.Text + "," + dialog.tbSpan.Text + "); ";
                    //}
                    //else
                    //{
                    //    return;
                    //}
                }
                if (sender == btnClockwise)
                {
                    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.Clockwise(" + GetStepName(sp) + "LightGroup);";
                }
                if (sender == btnAntiClockwise)
                {
                    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.AntiClockwise(" + GetStepName(sp) + "LightGroup);";
                }
                if (sender == btnReversal)
                {
                    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.Reversal(" + GetStepName(sp) + "LightGroup);";
                }
                if (sender == btnExtendTime)
                {
                    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.ChangeTime(" + GetStepName(sp) + "LightGroup,0,2);";
                }
                if (sender == btnShortenTime)
                {
                    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.ChangeTime(" + GetStepName(sp) + "LightGroup,1,2);";
                }
                if (sender == btnMatchTime)
                {
                    GetNumberDialog dialog = new GetNumberDialog(mw, "TotalTimeLatticeColon", false);
                    if (dialog.ShowDialog() == true)
                    {
                        command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.MatchTotalTimeLattice(" + GetStepName(sp) + "LightGroup," + dialog.OneNumber + ");";
                    }
                }
                if (sender == btnInterceptTime)
                {
                    //InterceptTimeDialog dialog = new InterceptTimeDialog(mw);
                    //if (dialog.ShowDialog() == true)
                    //{
                    //    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.InterceptTime(" + GetStepName(sp) + "LightGroup," + dialog.Min+","+dialog.Max + ");";
                    //}
                }
                if (sender == btnRemoveBorder)
                {
                    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.RemoveBorder(" + GetStepName(sp) + "LightGroup);";
                }
                if (sender == btnDiyTime)
                {
                    ChangeTimeDialog ct = new ChangeTimeDialog(mw);
                    if (ct.ShowDialog() == true)
                    {
                        command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.ChangeTime(" + GetStepName(sp) + "LightGroup," + ct.cbOperation.SelectedIndex + "," + ct.tbPolyploidy.Text + ");";
                    }
                }
                if (sender == btnFillColor)
                {
                    GetNumberDialog dialog = new GetNumberDialog(mw, "FillColorColon", false);
                    if (dialog.ShowDialog() == true)
                    {
                        command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.FillColor(" + GetStepName(sp) + "LightGroup," + dialog.OneNumber + ");";
                    }
                }
                if (sender == btnChangeColorYellow || sender == btnChangeColorBlue || sender == btnChangeColorPink || sender == btnChangeColorDiy || sender == btnColorChange)
                {
                    String colorGroupName = String.Empty;
                    int i = 1;
                    while (i <= 100000)
                    {
                        if (!containDictionary[GetStepName(sp)].Contains("Step" + i))
                        {
                            containDictionary[GetStepName(sp)].Add("Step" + i);
                            colorGroupName = "Step" + i + "Color";
                            break;
                        }
                        i++;
                    }
                    if (i > 100000)
                    {
                        new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
                        return;
                    }
                    command = Environment.NewLine + "\tColorGroup " + colorGroupName + " = new ColorGroup(\"";
                    if (sender == btnChangeColorYellow)
                        command += "73 74 75 76";
                    if (sender == btnChangeColorBlue)
                        command += "33 37 41 45";
                    if (sender == btnChangeColorPink)
                        command += "4 94 53 57";
                    if (sender == btnChangeColorDiy)
                    {
                        mw.mActionBeanList = RefreshData(GetStepName());
                        List<int> mColor = new List<int>();
                        for (int j = 0; j < mw.mActionBeanList.Count; j++)
                        {
                            if (mw.mActionBeanList[j].Action == 144)
                            {
                                if (!mColor.Contains(mw.mActionBeanList[j].Color))
                                {
                                    mColor.Add(mw.mActionBeanList[j].Color);
                                }
                            }
                        }
                        mColor.Sort();
                        GetNumberDialog dialog = new GetNumberDialog(mw, "CustomFormattedColorColon", true, mColor);
                        if (dialog.ShowDialog() == true)
                        {
                            StringBuilder mBuilder = new StringBuilder();
                            for (int x = 0; x < dialog.MultipleNumber.Count; x++)
                            {
                                if (x != dialog.MultipleNumber.Count - 1)
                                {
                                    mBuilder.Append(dialog.MultipleNumber[x] + " ");
                                }
                                else
                                {
                                    mBuilder.Append(dialog.MultipleNumber[x]);
                                }
                            }
                            command += mBuilder.ToString();
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (sender == btnColorChange)
                    {
                        ListChangeColorDialog colorDialog = new ListChangeColorDialog(mw, RefreshData(GetStepName(sp)));
                        if (colorDialog.ShowDialog() == true)
                        {
                            StringBuilder mBuilder = new StringBuilder();
                            for (int x = 0; x < colorDialog.lbColor.Items.Count; x++)
                            {
                                if (x != colorDialog.lbColor.Items.Count - 1)
                                {
                                    mBuilder.Append(colorDialog.lbColor.Items[x].ToString() + " ");
                                }
                                else
                                {
                                    mBuilder.Append(colorDialog.lbColor.Items[x].ToString());
                                }
                            }
                            command += mBuilder.ToString();
                        }
                        else
                        {
                            return;
                        }
                    }
                    command += "\",' ','-');" + Environment.NewLine
                  + "\t" + GetStepName(sp) + "LightGroup.Color = " + colorGroupName + ";";
                }
                if (sender == btnColorWithCount)
                {
                    GetNumberDialog dialog = new GetNumberDialog(mw, "WithCountColon", true);
                    if (dialog.ShowDialog() == true)
                    {
                        StringBuilder builder = new StringBuilder();
                        foreach (int i in dialog.MultipleNumber) {
                            builder.Append(i + " ");
                        }
                        command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.ColorWithCount(" + GetStepName(sp) + "LightGroup,\"" + builder.ToString().Trim() + "\");";
                    }
                }
                if (sender == btnSetStartTime)
                {
                    GetNumberDialog dialog = new GetNumberDialog(mw, "PleaseEnterTheStartTimeColon", false);
                    if (dialog.ShowDialog() == true)
                    {
                        command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = " + GetStepName(sp) + "LightGroup.SetStartTime(" + dialog.OneNumber + ");";
                    }
                    else
                    {
                        return;
                    }
                }
                if (sender == btnSetEndTime)
                {
                    Edit_SetEndTimeDialog dialog = new Edit_SetEndTimeDialog(mw, GetStepName(sp) + "LightGroup");
                    if (dialog.ShowDialog() == true)
                    {
                        command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.SetEndTime(" + GetStepName(sp) + "LightGroup,";
                        if (dialog.cbType.SelectedIndex == 0)
                        {
                            command += "All,";
                        }
                        else if (dialog.cbType.SelectedIndex == 1)
                        {
                            command += "End,";
                        }
                        else if (dialog.cbType.SelectedIndex == 2)
                        {
                            command += "AllAndEnd,";
                        }
                        command += dialog.tbValue.Text + ");";
                    }
                    else
                    {
                        return;
                    }
                }
                if (sender == btnSetAllTime)
                {
                    GetNumberDialog dialog = new GetNumberDialog(mw, "PleaseEnterTheConstantTimeColon", false);
                    if (dialog.ShowDialog() == true)
                    {
                        command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = " + GetStepName(sp) + "LightGroup.SetAllTime(" + dialog.OneNumber + ");";
                    }
                    else
                    {
                        return;
                    }
                }
                if (sender == btnDisappear)
                {
                    //Edit_AnimationDisappearDialog dialog = new Edit_AnimationDisappearDialog(mw, GetStepName(sp));
                    //if (dialog.ShowDialog() == true)
                    //{
                    //    command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.Animation(" + GetStepName(sp) + "LightGroup,Serpentine," + dialog.tbStartTime.Text + "," + dialog.tbInterval.Text + ");";
                    //}
                    //else
                    //{
                    //    return;
                    //}
                }
                if (sender == btnWindmill)
                {
                    GetNumberDialog dialog = new GetNumberDialog(mw, "IntervalColon", false);
                    if (dialog.ShowDialog() == true)
                    {
                        command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.Animation(" + GetStepName(sp) + "LightGroup,Windmill," + dialog.OneNumber + ");";
                    }
                }
                if (sender == btnCopyToTheEnd)
                {
                    String colorGroupName = String.Empty;
                    int i = 1;
                    while (i <= 100000)
                    {
                        if (!containDictionary[GetStepName(sp)].Contains("Step" + i))
                        {
                            containDictionary[GetStepName(sp)].Add("Step" + i);
                            colorGroupName = "Step" + i + "Color";
                            break;
                        }
                        i++;
                    }
                    if (i > 100000)
                    {
                        new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
                        return;
                    }
                    GetNumberDialog dialog = new GetNumberDialog(mw, "PleaseEnterANewColorGroupColon", true);
                    if (dialog.ShowDialog() == true)
                    {
                        StringBuilder mBuilder = new StringBuilder();
                        for (int x = 0; x < dialog.MultipleNumber.Count; x++)
                        {
                            if (x != dialog.MultipleNumber.Count - 1)
                            {
                                mBuilder.Append(dialog.MultipleNumber[x].ToString() + " ");
                            }
                            else
                            {
                                mBuilder.Append(dialog.MultipleNumber[x].ToString());
                            }
                        }
                        command = Environment.NewLine + "\tColorGroup " + colorGroupName + " = new ColorGroup(\""
                          + mBuilder.ToString() + "\",' ','-');" + Environment.NewLine
                        + "\t" + GetStepName(sp) + "LightGroup = Edit.CopyToTheEnd(" + GetStepName(sp) + "LightGroup," + colorGroupName + "); ";
                    }
                    else
                    {
                        return;
                    }
                }
                if (sender == btnCopyToTheFollow)
                {
                    String colorGroupName = String.Empty;
                    int i = 1;
                    while (i <= 100000)
                    {
                        if (!containDictionary[GetStepName(sp)].Contains("Step" + i))
                        {
                            containDictionary[GetStepName(sp)].Add("Step" + i);
                            colorGroupName = "Step" + i + "Color";
                            break;
                        }
                        i++;
                    }
                    if (i > 100000)
                    {
                        new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
                        return;
                    }
                    GetNumberDialog dialog = new GetNumberDialog(mw, "PleaseEnterANewColorGroupColon", true);
                    if (dialog.ShowDialog() == true)
                    {
                        StringBuilder mBuilder = new StringBuilder();
                        for (int x = 0; x < dialog.MultipleNumber.Count; x++)
                        {
                            if (x != dialog.MultipleNumber.Count - 1)
                            {
                                mBuilder.Append(dialog.MultipleNumber[x].ToString() + " ");
                            }
                            else
                            {
                                mBuilder.Append(dialog.MultipleNumber[x].ToString());
                            }
                        }
                        command = Environment.NewLine + "\tColorGroup " + colorGroupName + " = new ColorGroup(\""
                          + mBuilder.ToString() + "\",' ','-');" + Environment.NewLine
                        + "\t" + GetStepName(sp) + "LightGroup = Edit.CopyToTheFollow(" + GetStepName(sp) + "LightGroup," + colorGroupName + "); ";
                    }
                    else
                    {
                        return;
                    }
                }
                if (sender == btnAccelerationOrDeceleration)
                {
                    String rangeGroupName = String.Empty;
                    int i = 1;
                    while (i <= 100000)
                    {
                        if (!containDictionary[GetStepName(sp)].Contains("Step" + i))
                        {
                            containDictionary[GetStepName(sp)].Add("Step" + i);
                            rangeGroupName = "Step" + i + "Range";
                            break;
                        }
                        i++;
                    }
                    if (i > 100000)
                    {
                        new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
                        return;
                    }
                    GetNumberDialog dialog = new GetNumberDialog(mw, "PleaseEnterTheDurationColon", true);
                    if (dialog.ShowDialog() == true)
                    {
                        StringBuilder mBuilder = new StringBuilder();
                        for (int x = 0; x < dialog.MultipleNumber.Count; x++)
                        {
                            if (x != dialog.MultipleNumber.Count - 1)
                            {
                                mBuilder.Append(dialog.MultipleNumber[x].ToString() + " ");
                            }
                            else
                            {
                                mBuilder.Append(dialog.MultipleNumber[x].ToString());
                            }
                        }
                        command = Environment.NewLine + "\tRangeGroup " + rangeGroupName + " = new RangeGroup(\""
                          + mBuilder.ToString() + "\",' ','-');" + Environment.NewLine
                        + "\t" + GetStepName(sp) + "LightGroup = Edit.AccelerationOrDeceleration(" + GetStepName(sp) + "LightGroup," + rangeGroupName + "); ";
                    }
                    else
                    {
                        return;
                    }

                }
                if (sender == miSquare || sender == miRadialVertical || sender == miRadialHorizontal)
                {
                //    ShapeColorDialog dialog;
                //    if (sender == miSquare)
                //    {
                //        dialog = new ShapeColorDialog(mw, 0);
                //    }
                //    else if (sender == miRadialVertical)
                //    {
                //        dialog = new ShapeColorDialog(mw, 1);
                //    }
                //    else
                //    {
                //        dialog = new ShapeColorDialog(mw, 2);
                //    }
                //    if (dialog.ShowDialog() == true)
                //    {
                //        if (sender == miSquare)
                //        {
                //            command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.ShapeColor(" + GetStepName(sp) + "LightGroup,Square,\"" + dialog.content + "\");";
                //        }
                //        else if (sender == miRadialVertical)
                //        {
                //            command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.ShapeColor(" + GetStepName(sp) + "LightGroup,RadialVertical,\"" + dialog.content + "\");";
                //        }
                //        else if (sender == miRadialHorizontal)
                //        {
                //            command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Edit.ShapeColor(" + GetStepName(sp) + "LightGroup,RadialHorizontal,\"" + dialog.content + "\");";
                //        }
                //    }
                }
                lightScriptDictionary[GetStepName(sp)] += command;
            }
            RefreshData();
        }
        private void Automatic(object sender, RoutedEventArgs e)
        {
            HideAllPopup();

            String stepName = GetUsableStepName();
            if (stepName == null)
            {
                new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
                return;
            }
            String commandLine = String.Empty;
            if (sender == miRhombusDiffusion || sender == miCross)
            {
                GetNumberDialog dialog = new GetNumberDialog(mw, "StartTimeColon", false);
                if (dialog.ShowDialog() == true)
                {
                    if (sender == miRhombusDiffusion)
                    {
                        commandLine = "\tLightGroup " + stepName + "LightGroup = Create.Automatic(RhombusDiffusion,"
                        + dialog.OneNumber + ");";
                    }
                    if (sender == miCross)
                    {
                        commandLine = "\tLightGroup " + stepName + "LightGroup = Create.Automatic(Cross,"
                        + dialog.OneNumber + ");";
                    }
                }
                else
                {
                    return;
                }
            }
            if (sender == miRandomFountain)
            {
                RandomFountainDialog dialog = new RandomFountainDialog(mw);
                if (dialog.ShowDialog() == true)
                {
                    commandLine = "\tLightGroup " + stepName + "LightGroup = Create.Automatic(RandomFountain,"
                    + dialog.Max + "," + dialog.Min + ",true);";
                }
                else
                {
                    return;
                }
            }
            lightScriptDictionary.Add(stepName, commandLine);
            visibleDictionary.Add(stepName, true);
            containDictionary.Add(stepName, new List<string>() { stepName });
            if (RefreshData())
            {
                AddStep(stepName, "");
                lbStep.SelectedIndex = lbStep.Items.Count - 1;
            }
        }

        private void Translation(object sender, RoutedEventArgs e)
        {
            //ChangeIntoMotionDialog dialog = new ChangeIntoMotionDialog(mw);
            //if (dialog.ShowDialog() == true)
            //{
            //    StringBuilder builder = new StringBuilder();
            //    foreach (int number in dialog.NumberList)
            //    {
            //        builder.Append(number.ToString() + " ");
            //    }
            //    String stepName = GetUsableStepName();
            //    if (stepName == null)
            //    {
            //        new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
            //        return;
            //    }
            //    String commandLine = "\tLightGroup " + stepName + "LightGroup = Create.Animation(Translation,\""
            //        + builder.ToString().Trim() + "\");";

            //    lightScriptDictionary.Add(stepName, commandLine);
            //    visibleDictionary.Add(stepName, true);
            //    containDictionary.Add(stepName, new List<string>() { stepName });
            //    if (RefreshData())
            //    {
            //        AddStep(stepName, "");
            //        lbStep.SelectedIndex = lbStep.Items.Count - 1;
            //    }
            //}
        }

        private void Menu_MouseEnter(object sender, RoutedEventArgs e)
        {
            HideAllPopup();

            if (sender == miThirdParty)
            {
                popThirdParty.IsOpen = false;
                popThirdParty.IsOpen = true;
            }
            if (sender == miAutomatic)
            {
                popAutomatic.IsOpen = false;
                popAutomatic.IsOpen = true;
            }
            if (sender == miLibrary)
            {
                popLibrary.IsOpen = false;
                popLibrary.IsOpen = true;
            }
            if (sender == miMycontent)
            {
                popMycontent.IsOpen = false;
                popMycontent.IsOpen = true;
            }
            if (sender == miDebug)
            {
                popDebug.IsOpen = false;
                popDebug.IsOpen = true;
            }
            e.Handled = true;
        }

        private void UserControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HideAllPopup();
        }

        private void HideAllPopup()
        {
            popColor.IsOpen = false;
            popShape.IsOpen = false;
            popTime.IsOpen = false;
            popSuperposition.IsOpen = false;
            popOther.IsOpen = false;
            popThirdParty.IsOpen = false;
            popAnimation.IsOpen = false;
            popAutomatic.IsOpen = false;
            popDebug.IsOpen = false;
            popLibrary.IsOpen = false;
            popMycontent.IsOpen = false;
        }

        private void Menu_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender == miColor)
            {
                miChildColor.IsSubmenuOpen = true;
            }
            if (sender == miShape)
            {
                miChildShape.IsSubmenuOpen = true;
            }
            if (sender == miTime)
            {
                miChildTime.IsSubmenuOpen = true;
            }
            if (sender == miSuperposition)
            {
                miChildSuperposition.IsSubmenuOpen = true;
            }
            if (sender == miThirdParty)
            {
                miChildThirdParty.IsSubmenuOpen = true;
            }
            if (sender == miOther)
            {
                miChildOther.IsSubmenuOpen = true;
            }
            if (sender == miAnimation)
            {
                miChildAnimation.IsSubmenuOpen = true;
            }
            if (sender == miAutomatic)
            {
                miChildAutomatic.IsSubmenuOpen = true;
            }
            if (sender == miLibrary)
            {
                miChildLibrary.IsSubmenuOpen = true;
            }
            if (sender == miMycontent)
            {
                //获取最新的我的内容
                //bridge.InitMyContent(bridge.GetMyContent(""), MyContentMenuItem_Click);
                miChildMycontent.IsSubmenuOpen = true;
            }
            if (sender == miDebug)
            {
                miChildDebug.IsSubmenuOpen = true;
            }
        }

        private void ImageUnmake_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Unmake();
        }

        private void ImageRedo_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Redo();
        }

        private void Unmake()
        {
            //if (mw.iNowPosition == -1)
            //    return;
            //int mINowPosition = mw.iNowPosition - 1;
            //int selectedIndex = lbStep.SelectedIndex;
            ////if (mINowPosition == -1)
            ////    mINowPosition = 99;
            //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + mINowPosition + ".lightScript"))
            //{
            //    File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + mINowPosition + ".lightScript", mw.lightScriptFilePath, true);
            //    mw._bIsEdit = true;
            //    mw.bIsEdit = true;
            //    mw.iNowPosition -= 1;
            //    mw.ProjectDocument_SelectionChanged_LightScript();
            //    if (selectedIndex < lbStep.Items.Count) {
            //        lbStep.SelectedIndex = selectedIndex;
            //    }
            //}
        }
        private void Redo()
        {
            //if (mw.iNowPosition == -1)
            //    return;
            //int mINowPosition = mw.iNowPosition + 1;
            //int selectedIndex = lbStep.SelectedIndex;
            //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + mINowPosition + ".lightScript"))
            //{
            //    File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + mINowPosition + ".lightScript", mw.lightScriptFilePath, true);
            //    mw._bIsEdit = true;
            //    mw.bIsEdit = true;
            //    mw.iNowPosition += 1;
            //    mw.ProjectDocument_SelectionChanged_LightScript();
            //    if (selectedIndex < lbStep.Items.Count)
            //    {
            //        lbStep.SelectedIndex = selectedIndex;
            //    }
            //}
        }

        private void GetExecutionTime(object sender, RoutedEventArgs e)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            ////耗时巨大的代码  
            //RefreshData();
            //sw.Stop();
            //TimeSpan ts2 = sw.Elapsed;
            //new MessageDialog(mw, String.Format("总共花费{0}ms.", ts2.TotalMilliseconds), 0).ShowDialog();
            //File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Cache\" + mw.iNowPosition + ".lightScript");
            //mw.iNowPosition--;
        }

        private void GetCompleteScript(object sender, RoutedEventArgs e)
        {
            new MessageDialog(mw, GetCompleteScript(), 0).ShowDialog();
        }

        private void CbFastGenerationrAction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFastGenerationrType.SelectedIndex > 3)
            {
                cbFastGenerationrAction.SelectedIndex = 0;
            }

        }

        private void CbMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (spSelectEditor == null)
                return;
            switch (cbMode.SelectedIndex)
            {
                case 0:
                    spSelectEditor.Visibility = Visibility.Visible;
                    spFastGenerationr.Visibility = Visibility.Visible;
                    spConditionJudgment.Visibility = Visibility.Visible;
                    break;
                case 1:
                    spSelectEditor.Visibility = Visibility.Collapsed;
                    spFastGenerationr.Visibility = Visibility.Visible;
                    spConditionJudgment.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    spSelectEditor.Visibility = Visibility.Visible;
                    spFastGenerationr.Visibility = Visibility.Collapsed;
                    spConditionJudgment.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void MiIntroduce_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EidtTextDialog edit = new EidtTextDialog();
            edit.SetData(introduceText);
            if (edit.ShowDialog() == true)
            {
                introduceText = edit.GetData();
                SaveLightScriptFile("",false);
            }
        }

        private void LibraryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //MenuItem item = (MenuItem)sender;
            //ImportLibraryDialog dialog = new ImportLibraryDialog(mw, AppDomain.CurrentDomain.BaseDirectory + @"\Library\" + item.Header.ToString() + ".lightScript");
            //if (dialog.ShowDialog() == true)
            //{
            //    if (!importList.Contains(item.Header.ToString() + ".lightScript"))
            //    {
            //        importList.Add(item.Header.ToString() + ".lightScript");
            //    }
            //    String UsableStepName = GetUsableStepName();
            //    AddStep(UsableStepName, "");
            //    String command = "\tLightGroup " + UsableStepName + "LightGroup = " + item.Header.ToString() + "." + dialog.lbMain.SelectedItem.ToString() + "();";
            //    lightScriptDictionary.Add(UsableStepName, command);
            //    visibleDictionary.Add(UsableStepName, true);
            //    containDictionary.Add(UsableStepName, new List<String>() { UsableStepName });
            //    RefreshData();
            //}
        }
        private void MyContentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            if ((mw.lastProjectPath + @"\LightScript\" + item.Header.ToString() + ".lightScript").Equals(mw.lightScriptFilePath))
            {
                return;
            }
            //ImportLibraryDialog dialog = new ImportLibraryDialog(mw, mw.lastProjectPath + @"\LightScript\" + item.Header.ToString() + ".lightScript");
            //if (dialog.ShowDialog() == true)
            //{
            //    if (!importList.Contains(item.Header.ToString() + ".lightScript"))
            //    {
            //        importList.Add(item.Header.ToString() + ".lightScript");
            //    }
            //    String UsableStepName = GetUsableStepName();
            //    AddStep(UsableStepName, "");
            //    String command = "\tLightGroup " + UsableStepName + "LightGroup = " + item.Header.ToString() + "." + dialog.lbMain.SelectedItem.ToString() + "();";
            //    lightScriptDictionary.Add(UsableStepName, command);
            //    visibleDictionary.Add(UsableStepName, true);
            //    containDictionary.Add(UsableStepName, new List<String>() { UsableStepName });
            //    RefreshData();
            //}
        }

        private void Child_SubmenuClosed(object sender, RoutedEventArgs e)
        {
            MenuItem item = e.OriginalSource as MenuItem;
            if (item.Parent is Menu)
            {
                HideAllPopup();
            }
        }

        private void LbStep_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lbStep.SelectedIndex = -1;
        }

        private void SpMainStep_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //StyleWindow style = new StyleWindow(mw);
            //if (finalDictionary.ContainsKey("Main"))
            //{
            //    style.SetData(finalDictionary["Main"]);
            //}
            //else
            //{
            //    style.SetData("");
            //}
            //if (style.ShowDialog() == true)
            //{
            //    if (finalDictionary.ContainsKey("Main"))
            //    {
            //        if (!style._Content.Equals(String.Empty))
            //        {
            //            finalDictionary["Main"] = style._Content;
            //        }
            //        else
            //        {
            //            finalDictionary.Remove("Main");
            //        }
            //    }
            //    else
            //    {
            //        if (!style._Content.Equals(String.Empty))
            //            finalDictionary.Add("Main", style._Content);
            //    }
            //    RefreshData();
            //}
        }

        public int needChangeColor;
        private void InputUserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
         {
            pColor.IsOpen = false;
          
            int newColor = lbColor.Items.IndexOf(sender);
            if (newColor == 0 || newColor == needChangeColor)
                return;

            StringBuilder mBuilder = new StringBuilder();
            List<int> mColor = bridge.mColor.ToList();
            mColor.Sort();
            for (int x = 0; x < mColor.Count; x++)
            {
                if (x != mColor.Count - 1)
                {
                    if (mColor[x] == needChangeColor)
                    {
                        mBuilder.Append(newColor + " ");
                    }
                    else
                    {
                        mBuilder.Append(mColor[x].ToString() + " ");
                    }
                }
                else
                {
                    if (mColor[x] == needChangeColor)
                    {
                        mBuilder.Append(newColor);
                    }
                    else
                    {
                        mBuilder.Append(mColor[x].ToString());
                    }
                }
            }

            if (lbStep.SelectedIndex != -1)
            {
                //没有可操作的步骤
                if (lbStep.SelectedIndex == -1)
                    return;

                //for (int k = 0; k < lbStep.SelectedItems.Count; k++)
                //{
                //    StackPanel sp = (StackPanel)lbStep.SelectedItems[k];
                //    //没有可操作的灯光组
                //    if (!lightScriptDictionary[GetStepName(sp)].Contains(GetStepName(sp) + "LightGroup"))
                //    {
                //        continue;
                //    }
                //    String command = String.Empty;
                //    String colorGroupName = String.Empty;
                //    int i = 1;
                //    while (i <= 100000)
                //    {
                //        if (!containDictionary[GetStepName(sp)].Contains("Step" + i))
                //        {
                //            containDictionary[GetStepName(sp)].Add("Step" + i);
                //            colorGroupName = "Step" + i + "Color";
                //            break;
                //        }
                //        i++;
                //    }
                //    if (i > 100000)
                //    {
                //        new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
                //        return;
                //    }
                //    command = Environment.NewLine + "\tColorGroup " + colorGroupName + " = new ColorGroup(\"";
                //    command += mBuilder.ToString();

                //    command += "\",' ','-');" + Environment.NewLine
                //  + "\t" + GetStepName(sp) + "LightGroup.Color = " + colorGroupName + ";";

                //    lightScriptDictionary[GetStepName(sp)] += command;
                //}
                if (finalDictionary.ContainsKey(GetStepName()))
                {
                    String str = finalDictionary[GetStepName()];
                    str = str.Substring(0, str.Length - 1);
                    finalDictionary[GetStepName()] = str + ";Color=Format-Diy-" + mBuilder.ToString() + ";";
                }
                else
                {
                    finalDictionary.Add(GetStepName(), "Color=Format-Diy-" + mBuilder.ToString() + ";");
                }
            }
            else
            {
                //总体操作 =》Main操作
                if (finalDictionary.ContainsKey("Main"))
                {
                    String str = finalDictionary["Main"];
                    str = str.Substring(0, str.Length - 1);
                    finalDictionary["Main"] = str + ";Color=Format-Diy-" + mBuilder.ToString() + ";";
                }
                else
                {
                    finalDictionary.Add("Main", "Color=Format-Diy-" + mBuilder.ToString() + ";");
                }
            }
            RefreshData();
        }

        private void LockingStep(object sender, RoutedEventArgs e)
        {
            if (lbStep.SelectedIndex == -1)
                return;

            String stepName = GetStepName();
            if (lockedDictionary.ContainsKey(stepName))
            {
                //解锁
                lockedDictionary.Remove(stepName);
            }
            else {
                lockedDictionary.Add(stepName, RefreshData(stepName));
            }
            UpdateLocked();
            RefreshData();
            //多步骤锁定/解锁
            //for (int i = 0;i<lbStep.SelectedItems.Count;i++)
            //{
            //    StackPanel sp = (StackPanel)lbStep.SelectedItems[i];
            //    if (!GetParentName(sp).Equals(String.Empty))
            //    {
            //        lbStep.SelectedItem = lbStep.Items[i];
            //        System.Windows.Forms.MessageBox.Show("请先取消与父类的关系");
            //        return;
            //    }
            //    if (finalDictionary.ContainsKey(GetStepName(sp)))
            //    {
            //        lbStep.SelectedItem = lbStep.Items[i];
            //        System.Windows.Forms.MessageBox.Show("该步骤有样式，请先将样式融入步骤!");
            //        return;
            //    }
            //    if (intersectionDictionary.ContainsKey(GetStepName(sp)))
            //    {
            //        lbStep.SelectedItem = lbStep.Items[i];
            //        System.Windows.Forms.MessageBox.Show("该步骤有(交)子集，请先将集合融入步骤!");
            //        return;
            //    }
            //    foreach (var item in intersectionDictionary)
            //    {
            //        if (item.Value.Contains(GetStepName(sp)))
            //        {
            //            lbStep.SelectedItem = lbStep.Items[i];
            //            System.Windows.Forms.MessageBox.Show("该步骤是(交)子集，请先将集合融入步骤!");
            //            return;
            //        }
            //    }
            //    if (complementDictionary.ContainsKey(GetStepName(sp)))
            //    {
            //        lbStep.SelectedItem = lbStep.Items[i];
            //        System.Windows.Forms.MessageBox.Show("该步骤有(补)子集，请先将集合融入步骤!");
            //        return;
            //    }
            //    foreach (var item in complementDictionary)
            //    {
            //        if (item.Value.Contains(GetStepName(sp)))
            //        {
            //            lbStep.SelectedItem = lbStep.Items[i];
            //            System.Windows.Forms.MessageBox.Show("该步骤是(补)子集，请先将集合融入步骤!");
            //            return;
            //        }
            //    }
            //}

        }


        //private void MiEdit_SubmenuOpened(object sender, RoutedEventArgs e)
        //{
        //    int _iNowPosition = mw.iNowPosition - 1;
        //    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + _iNowPosition + ".lightScript"))
        //    {
        //        miUnmake.IsEnabled = true;
        //    }
        //    else
        //    {
        //        miUnmake.IsEnabled = false;
        //    }
        //    _iNowPosition = mw.iNowPosition + 1;
        //    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + _iNowPosition + ".lightScript"))
        //    {
        //        miRedo.IsEnabled = true;
        //    }
        //    else
        //    {
        //        miRedo.IsEnabled = false;
        //    }
        //}

    }
    
}
