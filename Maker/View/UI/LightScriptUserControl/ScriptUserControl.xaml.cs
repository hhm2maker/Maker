using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Maker.Bridge;
using Maker.Business;
using Maker.Commands;
using Maker.Model;
using Maker.View.Control;
using Maker.View.Dialog;
using Maker.View.Dialog.Automatic;
using Maker.View.Style;
using Maker.ViewBusiness;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Maker.Model.EnumCollection;
using System.Xml;
using System.Xml.Linq;
using Maker.Business.Model.OperationModel;
using Operation;
using Maker.View.UI.UserControlDialog;
using Maker.Business.Model;
using static Maker.Business.Model.ThirdPartySetupsModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Maker.View.UI.Dialog.WindowDialog;
using System.Windows.Shapes;

namespace Maker.View.LightScriptUserControl
{
    /// <summary>
    /// ScriptWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScriptUserControl : BaseLightScriptUserControl
    {
        //public MainWindow mw_;
        public InputUserControlBridge _bridge;

        private ScriptUserControlBridge bridge;

        public ScriptUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            //Width = mw.ActualWidth;
            //Height = mw.ActualHeight;

            mainView = gMain_;
            HideControl();

            bridge = new ScriptUserControlBridge(this);

            LoadConfig();

            //绑定命令
            CommandBinding binding = new CommandBinding(DataCommands.Editcommand);
            binding.Executed += Binding_Executed;
            binding.CanExecute += Binding_CanExecute;

            //CommandManager.InvalidateRequerySuggested();

            _bridge = new InputUserControlBridge(this);
            //加载库文件
            _bridge.InitLibrary(_bridge.GetLibrary(), LibraryMenuItem_Click);

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
            thirdPartys = GetThirdParty();
            InitThirdParty(thirdPartys, ThirdPartysMenuItem_Click);

            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Colors.Transparent));
            mLaunchpad.SetButtonBackground(new SolidColorBrush(Colors.Transparent));
            mLaunchpad.AddMembrane();
            mLaunchpad.IsMembrane = true;

            sw = new StyleWindow(this);
            Grid.SetRow(sw, 0);
            gRight.Children.Add(sw);
        }
        public StyleWindow sw;

        /// <summary>
        /// 第三方插件列表
        /// </summary>
        public List<ThirdPartyModelsModel.ThirdPartyModel> thirdPartys = new List<ThirdPartyModelsModel.ThirdPartyModel>();

        public String strInputFormatDelimiter;
        public String strInputFormatRange;

        public char StrInputFormatDelimiter
        {
            get
            {
                if (strInputFormatDelimiter.Equals("Comma"))
                {
                    return ',';
                }
                else if (strInputFormatDelimiter.Equals("Space"))
                {
                    return ' ';
                }
                else
                {
                    return ' ';
                }
            }

            set
            {
                if (value == ',')
                {
                    strInputFormatDelimiter = "Comma";
                }
                else if (value == ' ')
                {
                    strInputFormatDelimiter = "Space";
                }
                else
                {
                    strInputFormatDelimiter = "Comma";
                }
            }
        }

        public char StrInputFormatRange
        {
            get
            {
                if (strInputFormatRange.Equals("Shortbar"))
                {
                    return '-';
                }
                else if (strInputFormatRange.Equals("R"))
                {
                    return 'r';
                }
                else
                {
                    return '-';
                }
            }

            set
            {
                if (value == '-')
                {
                    strInputFormatDelimiter = "Shortbar";
                }
                else if (value == 'r')
                {
                    strInputFormatDelimiter = "R";
                }
                else
                {
                    strInputFormatDelimiter = "Shortbar";
                }
            }
        }

        private void LoadConfig()
        {
            //输入
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/input.xml");
            XmlNode inputRoot = doc.DocumentElement;
            //格式
            XmlNode inputFormat = inputRoot.SelectSingleNode("Format");
            XmlNode Delimiter = inputFormat.SelectSingleNode("Delimiter");
            strInputFormatDelimiter = Delimiter.InnerText;
            XmlNode Range = inputFormat.SelectSingleNode("Range");
            strInputFormatRange = Range.InnerText;
        }

        private void InitView()
        {
            //lbColor.HideText();
            //lbColor.ToSmall();

            AddStepControlToolTip();
        }


        private void Binding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (lbStep.SelectedIndex != -1);
        }

        private void Binding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            HideAllPopup();
          
            e.Handled = true;
        }

        public Dictionary<string, List<int>> rangeDictionary = new Dictionary<string, List<int>>();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Width = mw.ActualWidth;
            //Height = mw.gMost.ActualHeight;

            //mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromArgb(255, 40, 40, 40)));
            LoadRangeFile();

            foreach (var item in lbColor.Items)
            {
                (item as ListBoxItem).MouseLeftButtonUp += InputUserControl_MouseLeftButtonUp;
            }

            //ToolTip toolTip = new System.Windows.Controls.ToolTip();
            //toolTip.Content = Application.Current.Resources["IDoNotThinkItWorks"];
            //toolTip.SetValue(StyleProperty, null);
            //iNewStep.ToolTip = toolTip;

            InitFormat();
        }

        private void InitFormat()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/input.xml");
            XmlNode inputRoot = doc.DocumentElement;
            //格式
            XmlNode inputFormat = inputRoot.SelectSingleNode("Format");
            XmlNode Delimiter = inputFormat.SelectSingleNode("Delimiter");
            strInputFormatDelimiter = Delimiter.InnerText;
            XmlNode Range = inputFormat.SelectSingleNode("Range");
            strInputFormatRange = Range.InnerText;
        }

        private void LoadRangeFile()
        {
            _bridge.LoadRangeFile();
        }
        public Dictionary<String, String> lightScriptDictionary = new Dictionary<string, string>();
        public Dictionary<String, ScriptModel> scriptModelDictionary = new Dictionary<string, ScriptModel>();

        //public Dictionary<String, bool> visibleDictionary = new Dictionary<String, bool>();
        public Dictionary<String, List<String>> containDictionary = new Dictionary<String, List<String>>();
        public Dictionary<String, List<String>> extendsDictionary = new Dictionary<string, List<String>>();
        public List<String> importList = new List<String>();
        public Dictionary<String, String> finalDictionary = new Dictionary<string, string>();
        public Dictionary<String, List<String>> intersectionDictionary = new Dictionary<string, List<String>>();
        public Dictionary<String, List<String>> complementDictionary = new Dictionary<string, List<String>>();
        public Dictionary<String, List<Model.Light>> lockedDictionary = new Dictionary<string, List<Model.Light>>();

        private String introduce = "";
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
        /// 更新所有集合关系
        /// </summary>
        public void UpdateCollection()
        {
            UpdateIntersection();
            UpdateComplement();
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
                foreach (var scriptModel in scriptModelDictionary)
                {
                    //如果是子集
                    if (scriptModel.Value.Intersection.Contains(ls[i]))
                    {
                        //panel.Margin = new Thickness(30,0,0,0);
                        Image visibleImage = (Image)panel.Children[0];
                        visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/branch.png", UriKind.RelativeOrAbsolute));
                        break;
                    }
                }
            }

            //if (!isCollection)
            //{
            //    TextBlock blockStepName = (TextBlock)sp.Children[1];
            //    if (visibleDictionary[blockStepName.Text])
            //    {
            //        Image visibleImage = (Image)panel.Children[0];
            //        visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/visible.png", UriKind.RelativeOrAbsolute));
            //    }
            //    else
            //    {
            //        Image visibleImage = (Image)panel.Children[0];
            //        visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/novisible.png", UriKind.RelativeOrAbsolute));
            //    }
            //}
        }
        /// <summary>
        /// 更新补集集合关系
        /// </summary>
        public void UpdateComplement()
        {
            List<String> ls = GetStepNameCollection();
            for (int i = 0; i < ls.Count; i++) { 
                StackPanel sp = (StackPanel)lbStep.Items[i];
                DockPanel panel = (DockPanel)sp.Children[0];

                if (scriptModelDictionary[ls[i]].OperationModels.Find(model => model is CreateFromStepOperationModel) != null)
                {
                    //如果是子集
                    Image visibleImage = (Image)panel.Children[0];
                    visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/branch.png", UriKind.RelativeOrAbsolute));
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
                if (scriptModelDictionary[blockStepName.Text].Visible)
                {
                    DockPanel panel = (DockPanel)sp.Children[0];
                    Image visibleImage = (Image)panel.Children[0];
                    visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/visible.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    DockPanel panel = (DockPanel)sp.Children[0];
                    Image visibleImage = (Image)panel.Children[0];
                    visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/novisible.png", UriKind.RelativeOrAbsolute));
                }

                CreateFromStepOperationModel createFromStepOperationModel = (CreateFromStepOperationModel)scriptModelDictionary[ls[i]].OperationModels.Find(model => model is CreateFromStepOperationModel);
                    if (createFromStepOperationModel != null)
                    {
                    //如果是子集
                        scriptModelDictionary[ls[i]].Visible = scriptModelDictionary[createFromStepOperationModel.StepName].Visible;
                        DockPanel panel = (DockPanel)sp.Children[0];
                        Image visibleImage = (Image)panel.Children[0];
                        visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/branch.png", UriKind.RelativeOrAbsolute));
                    }

                    //foreach (var scriptModel in scriptModelDictionary)
                    //{
                    //    //如果是子集
                    //    if (scriptModel.Value.Intersection.Contains(ls[i]))
                    //    {
                    //        //panel.Margin = new Thickness(30,0,0,0);
                    //        Image visibleImage = (Image)panel.Children[0];
                    //        visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/branch.png", UriKind.RelativeOrAbsolute));
                    //        break;
                    //    }
                    //}
                }
            //根据父类可见不可见，来改变子类可见不可见
            //for (int i = 0; i < lbStep.Items.Count; i++)
            //{
            //    StackPanel sp = (StackPanel)lbStep.Items[i];
            //    TextBlock blockStepName = (TextBlock)sp.Children[1];
            //    TextBlock blockParentName = (TextBlock)sp.Children[3];

            //    if (!blockParentName.Text.Equals(String.Empty))
            //    {
            //        //如果父类不可见
            //        if (!scriptModelDictionary[blockParentName.Text].Visible)
            //        {
            //            DockPanel panel = (DockPanel)sp.Children[0];
            //            Image visibleImage = (Image)panel.Children[0];
            //            visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/novisible.png", UriKind.RelativeOrAbsolute));
            //            scriptModelDictionary[blockStepName.Text].Visible = false;
            //        }
            //    }
            //}
         
        
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
                    lockedImage.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/locked.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    lockedImage.Source = null;
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
                //if (!visibleDictionary[item.Key])
                //    continue;
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
                                            command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName + " = new ColorGroup(\""
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
                                    command.Append("\tColorGroup testColorGroup" + i.ToString() + finalName + " = new ColorGroup(\""
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
            builder.Append(Environment.NewLine + "}");

            //Console.WriteLine(builder.ToString());
            return builder.ToString();
            //Console.WriteLine(Environment.NewLine +builder.ToString()+Environment.NewLine); 
        }

        public void Test()
        {
            spHint.Visibility = Visibility.Collapsed;

            bridge.GetResult();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + (iNowPosition - 1) + ".lightScript"))
            {
                iUnmake.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/revoke_blue.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                iUnmake.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/revoke_gray.png", UriKind.RelativeOrAbsolute));
            }
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + (iNowPosition + 1) + ".lightScript"))
            {
                iRedo.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/redo_blue.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                iRedo.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/redo_gray.png", UriKind.RelativeOrAbsolute));
            }
        }

        public void Test(String stepName,int position)
        {
            Dictionary<String, ScriptModel> _scriptModelDictionary = new Dictionary<string, ScriptModel>();
            foreach (var item in scriptModelDictionary)
            {
                ScriptModel _scriptModel = ObjectCopier.Clone(item.Value);
                _scriptModelDictionary.Add(item.Key, _scriptModel);
                //ScriptModel ss = TransExpV2<ScriptModel, ScriptModel>.Trans(item.Value);
                //_scriptModelDictionary.Add(item.Key, ss);
            }
            _scriptModelDictionary[stepName].OperationModels.RemoveAt(position);
            bridge.GetBlockResult(stepName, _scriptModelDictionary);
        }
      

        public static class ObjectCopier
        {
            /// <summary>
            /// Perform a deep Copy of the object.
            /// </summary>
            /// <typeparam name="T">The type of object being copied.</typeparam>
            /// <param name="source">The object instance to copy.</param>
            /// <returns>The copied object.</returns>
            public static T Clone<T>(T source)
            {
                if (!typeof(T).IsSerializable)
                {
                    throw new ArgumentException("The type must be serializable.", "source");
                }

                // Don't serialize a null object, simply return the default for that object
                if (Object.ReferenceEquals(source, null))
                {
                    return default(T);
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream();
                using (stream)
                {
                    formatter.Serialize(stream, source);
                    stream.Seek(0, SeekOrigin.Begin);
                    return (T)formatter.Deserialize(stream);
                }
            }
        }

        public static class TransExpV2<TIn, TOut>
        {

            private static readonly Func<TIn, TOut> cache = GetFunc();
            private static Func<TIn, TOut> GetFunc()
            {
                ParameterExpression parameterExpression = System.Linq.Expressions.Expression.Parameter(typeof(TIn), "p");
                List<MemberBinding> memberBindingList = new List<MemberBinding>();

                foreach (var item in typeof(TOut).GetProperties())
                {
                    if (!item.CanWrite)
                        continue;

                    MemberExpression property = System.Linq.Expressions.Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    MemberBinding memberBinding = System.Linq.Expressions.Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }

                MemberInitExpression memberInitExpression = System.Linq.Expressions.Expression.MemberInit(System.Linq.Expressions.Expression.New(typeof(TOut)), memberBindingList.ToArray());
                Expression<Func<TIn, TOut>> lambda = System.Linq.Expressions.Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

                return lambda.Compile();
            }

            public static TOut Trans(TIn tIn)
            {
                return cache(tIn);
            }

        }

        public void Test(String stepName)
        {
            bridge.GetBlockResult(stepName);
        }
        public override List<Model.Light> GetData()
        {
            List<Model.Light> ll = new List<Model.Light>();
            foreach (var item in mLightDictionary)
            {
                ll.AddRange(item.Value);
            }
            return ll;
        }

        public Dictionary<string, List<Model.Light>> mLightDictionary;
        public List<Model.Light> mBlockLightList = new List<Model.Light>();

        public List<Model.Light> RefreshData(String partName)
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

            LightScriptBusiness scriptBusiness = new LightScriptBusiness(this, builder.ToString(), filePath);
            return scriptBusiness.GetResult(partName);
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
                Orientation = Orientation.Horizontal,
                Background = new SolidColorBrush(Colors.Transparent),
                AllowDrop = true,
            };
            sp.PreviewDrop += lbStep_Drop;
            DockPanel visiblePanel = new DockPanel();
            visiblePanel.MouseLeftButtonDown += VisiblePanel_MouseLeftButtonDown;
            visiblePanel.Width = 30;
            visiblePanel.Height = 30;
            Image visibleImage = new Image
            {
                Width = 20,
                Height = 20,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/visible.png", UriKind.RelativeOrAbsolute)),
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
            //没有可操作的灯光组
            if (!scriptModelDictionary[GetStepName()].Value.Contains(GetStepName() + "LightGroup"))
            {
                return;
            }
            //List<String> contain = scriptModelDictionary[GetStepName()].Contain;
            //scriptModelDictionary[GetStepName()].Value += Code.OperationModelsToCode(scriptModelDictionary[GetStepName()], ref contain);
            //scriptModelDictionary[GetStepName()].OperationModels.Clear();
            //TODO: 测试时需要测试，生成时可以去掉
            //Test();
        }

        private void RenameStepName(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                //双击时执行
                if (lbStep.SelectedIndex == -1)
                    return;
                GetStringDialog stringD = new GetStringDialog(mw, "StepName", "NewStepNameColon", "");
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
                    //bool b = visibleDictionary[oldName];
                    //visibleDictionary.Remove(oldName);
                    //visibleDictionary.Add(newName, b);

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
                    //RefreshData();
                }
                e.Handled = true;
            }
        }

        private void CheckProperties(object sender, RoutedEventArgs e)
        {
            if (lbStep.SelectedIndex == -1)
            {
                return;
            }
            mw.ShowMakerDialog(new CheckPropertiesDialog(mw, mBlockLightList));
        }

        private void VisiblePanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //List<String> ls = GetStepNameCollection();
            String stepName = GetVisibleImageStepName(sender);
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

            if (scriptModelDictionary[stepName].Visible)
            {
                DockPanel panel = (DockPanel)sender;
                Image visibleImage = (Image)panel.Children[0];
                visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/novisible.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                DockPanel panel = (DockPanel)sender;
                Image visibleImage = (Image)panel.Children[0];
                visibleImage.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/visible.png", UriKind.RelativeOrAbsolute));
            }
            scriptModelDictionary[stepName].Visible = !scriptModelDictionary[stepName].Visible;
            UpdateVisible();
            Test();
        }

        private void CancelParent(object sender, RoutedEventArgs e)
        {
            ////没有父类不需要解除
            //String parentName = GetParentName().Trim();
            //String childName = GetStepName();
            //if (parentName.Equals(String.Empty))
            //    return;
            ////不能隔代解除
            //if (!scriptModelDictionary[parentName].OperationModels[0].Equals(String.Empty))
            //{
            //    System.Windows.Forms.MessageBox.Show("选中项的父类有自己的父类,不能隔代解除，请先解除父类的父类!");
            //    return;
            //}
            ////对比包含字段
            ////父类字段不能与子类字段重复，否则替换
            //Dictionary<String, String> _dictionary = new Dictionary<String, String>();//老字段，新字段
            //List<String> newKey = new List<string>();
            //foreach (String str in scriptModelDictionary[parentName].Contain)
            //{
            //    //!newKey.Contains(str) 如果没有这个条件，就会 如果同有Step3这个元素，Step3-Step4,Step4-Step4 
            //    if (!scriptModelDictionary[childName].Contain.Contains(str) && !newKey.Contains(str))
            //    {
            //        //该字段不存在重复
            //        _dictionary.Add(str, str);
            //        newKey.Add(str);
            //    }
            //    else
            //    {
            //        int x = 1;
            //        while (x <= 100000)
            //        {
            //            if (!scriptModelDictionary[childName].Contain.Contains("Step" + x) && !newKey.Contains("Step" + x))
            //            {
            //                //不存在重复
            //                _dictionary.Add(str, "Step" + x);
            //                newKey.Add("Step" + x);
            //                break;
            //            }
            //            x++;
            //        }
            //        if (x > 100000)
            //        {
            //            new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
            //            return;
            //        }
            //    }
            //}
            //String oldName = _dictionary.Keys.First();

            ////给_dictionary 根据Step排序
            //ArrayList list = new ArrayList(_dictionary.Keys.ToArray());
            //IComparer fileNameComparer = new FilesNameComparerClass();
            //list.Sort(fileNameComparer);
            //Dictionary<String, String> mDictionary = new Dictionary<String, String>();
            ////一定要在这里添加，如果在下面 遍历_dictionary时添加，会导致顺序颠倒，缺少行数
            //for (int i = 0; i < list.Count; i++)
            //{
            //    if (!scriptModelDictionary[childName].Contain.Contains(list[i].ToString()))
            //        scriptModelDictionary[childName].Contain.Add(list[i].ToString());
            //}
            //for (int i = list.Count - 1; i >= 0; i--)
            //{
            //    mDictionary.Add(list[i].ToString(), _dictionary[list[i].ToString()]);
            //}
            //_dictionary = mDictionary;

            //String parentCommand = scriptModelDictionary[parentName].Value;
            //foreach (var item in _dictionary)
            //{
            //    parentCommand = parentCommand.Replace(item.Key + "Light", item.Value + "Light");
            //    parentCommand = parentCommand.Replace(item.Key + "LightGroup", item.Value + "LightGroup");
            //    parentCommand = parentCommand.Replace(item.Key + "RangeGroup", item.Value + "RangeGroup");
            //    parentCommand = parentCommand.Replace(item.Key + "ColorGroup", item.Value + "ColorGroup");
            //    parentCommand = parentCommand.Replace(item.Key + "PositionGroup", item.Value + "PositionGroup");
            //}
            //String oldChildrenCommand = scriptModelDictionary[childName].Value;
            //String myCommand = Environment.NewLine + "\tLightGroup " + childName + "LightGroup = " + parentName + "LightGroup;" + Environment.NewLine;
            //String newChildrenCommand = parentCommand + myCommand + oldChildrenCommand;
          
            //scriptModelDictionary[childName].Value = newChildrenCommand;
            //StackPanel sp = (StackPanel)lbStep.SelectedItem;
            //TextBlock block = (TextBlock)sp.Children[3];
            //block.Text = String.Empty;

            //Test();
        }


        private void EditPart(object sender, RoutedEventArgs e)
        {
            MainControlWindow control = new MainControlWindow(mw, RefreshData(GetStepName()), true);
            if (control.ShowDialog() == true)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("\tLightGroup " + GetStepName() + "LightGroup = new LightGroup();" + Environment.NewLine);
                int i = 1;
                foreach (Model.Light l in control.mLightList)
                {
                    builder.Append("\tLight light" + i + " = new LightGroup(" + l.Time + "," + l.Action + "," + l.Position + "," + l.Color + ");" + Environment.NewLine);
                    builder.Append("\t" + GetStepName() + "LightGroup.Add(" + "light" + i + ");" + Environment.NewLine);
                    i++;
                }
                lightScriptDictionary[GetStepName()] = builder.ToString();
                // RefreshData();
            }
        }
        /// <summary>
        /// 获取步骤集合
        /// </summary>
        /// <returns></returns>
        public List<String> GetStepNameCollection()
        {
            return scriptModelDictionary.Keys.ToList();
            //List<String> lsGetName = new List<string>();
            //for (int i = 0; i < lbStep.Items.Count; i++)
            //{
            //    StackPanel sp = (StackPanel)lbStep.Items[i];
            //    TextBlock blockStepName = (TextBlock)sp.Children[1];
            //    lsGetName.Add(blockStepName.Text);
            //}
            //return lsGetName;
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
            String stepName = GetStepName();
            ControlScriptDialog dialog = new ControlScriptDialog(this, scriptModelDictionary[stepName].Value);
            if (dialog.ShowDialog() == true)
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < dialog.lbMain.Items.Count; i++)
                {
                    if (i != dialog.lbMain.Items.Count - 1)
                    {
                        builder.Append("\t" + dialog.lbMain.Items[i].ToString() + Environment.NewLine);
                    }
                    else
                    {
                        builder.Append("\t" + dialog.lbMain.Items[i].ToString());
                    }
                }
                scriptModelDictionary[GetStepName()].Value = builder.ToString();
                Test();
            }
        }

        private void NewStep(object sender, RoutedEventArgs e)
        {
            ScriptModel scriptModel = new ScriptModel();
            String strName = GetUsableStepName();
            scriptModel.Name = strName;
            //scriptModel.Value = "LightGroup " + stepName + "LightGroup = new LightGroup();";
            scriptModel.Visible = true;
            scriptModelDictionary.Add(strName, scriptModel);
            UpdateStep();
            Test();
            //如果选中，就在列表选中最后一个
            lbStep.SelectedIndex = lbStep.Items.Count - 1;
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

                ScriptModel scriptModel = new ScriptModel();
                scriptModel.Name = stepName;
                scriptModel.Visible = scriptModelDictionary[oldName].Visible;
                foreach (var item in scriptModelDictionary[oldName].OperationModels)
                {
                    scriptModel.OperationModels.Add((BaseOperationModel)item.Clone());
                }

                //String newScript = scriptModelDictionary[oldName].Value;
                ////新的包含
                //List<String> ls = new List<string>();
                ////是否包含
                //if (scriptModelDictionary[oldName].Contain.Contains(stepName))
                //{
                //    //如果包含
                //    //将原包含字段的字段替换
                //    int x = 1;
                //    while (x <= 100000)
                //    {
                //        if (!scriptModelDictionary[oldName].Contain.Contains("Step" + x))
                //        {
                //            //不存在重复
                //            break;
                //        }
                //        x++;
                //    }
                //    if (x > 100000)
                //    {
                //        new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
                //        return;
                //    }
                //    newScript = newScript.Replace(stepName + "Light", "Step" + x + "Light");
                //    newScript = newScript.Replace(stepName + "LightGroup", "Step" + x + "LightGroup");
                //    newScript = newScript.Replace(stepName + "RangeGroup", "Step" + x + "RangeGroup");
                //    newScript = newScript.Replace(stepName + "ColorGroup", "Step" + x + "ColorGroup");
                //    newScript = newScript.Replace(stepName + "PositionGroup", "Step" + x + "PositionGroup");
                //}
                ////新名字和其他字段不冲突
                //newScript = newScript.Replace(oldName + "Light", stepName + "Light");
                //newScript = newScript.Replace(oldName + "LightGroup", stepName + "LightGroup");
                //newScript = newScript.Replace(oldName + "RangeGroup", stepName + "RangeGroup");
                //newScript = newScript.Replace(oldName + "ColorGroup", stepName + "ColorGroup");
                //newScript = newScript.Replace(oldName + "PositionGroup", stepName + "PositionGroup");

                //ls.Add(stepName);
                //foreach (String s in scriptModelDictionary[oldName].Contain)
                //{
                //    ls.Add(s);
                //}
                //if (ls.Contains(oldName))
                //{
                //    ls.Remove(oldName);
                //}
                //scriptModel.Contain = ls;
                //scriptModel.Value = newScript;
                //决定样式还是复制吧。
                if (finalDictionary.ContainsKey(oldName))
                {
                    finalDictionary.Add(stepName, finalDictionary[oldName]);
                }
                //最后界面里更改以及刷新继承
                scriptModelDictionary.Add(stepName, scriptModel);
                lbStep.SelectedItems.Remove(lbStep.SelectedItems[0]);
            }
            UpdateStep();
            UpdateVisible();
            Test();
            lbStep.SelectedIndex = lbStep.Items.Count - 1;
            lbStep.ScrollIntoView(lbStep.Items[lbStep.Items.Count-1]);
        }

        private void DelStep(object sender, RoutedEventArgs e)
        {
            DelStep();
        }

        private void DelStep()
        {
            if (lbStep.SelectedIndex == -1)
                return;

            if (mw.hintModelDictionary.ContainsKey(3))
            {
                if (mw.hintModelDictionary[0].IsHint == false)
                {
                    ToDelStep();
                    return;
                }
            }
            HintDialog hintDialog = new HintDialog("删除步骤", "您是否要删除步骤？",
              delegate (System.Object _o, RoutedEventArgs _e)
              {
                  ToDelStep();
                  mw.RemoveDialog();
              },
              delegate (System.Object _o, RoutedEventArgs _e)
              {
                  mw.RemoveDialog();
              },
              delegate (System.Object _o, RoutedEventArgs _e)
              {
                  mw.NotHint(3);
              }
             );
            mw.ShowMakerDialog(hintDialog);
        }

        private void ToDelStep()
        {
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
                foreach (var item in scriptModelDictionary)
                {
                    if (item.Value.OperationModels[0] is CreateFromStepOperationModel)
                    {
                        if ((item.Value.OperationModels[0] as CreateFromStepOperationModel).StepName.Equals(stepName)) {
                            System.Windows.Forms.MessageBox.Show("选中项为其他项的父类，请先解除父子关系之后再删除!");
                            return;
                        }
                    }
                }
                if (scriptModelDictionary[stepName].Intersection != null && scriptModelDictionary[stepName].Intersection.Count > 0)
                {
                    System.Windows.Forms.MessageBox.Show("它是其他交类的父集合");
                    return;
                }
                if (scriptModelDictionary[stepName].Complement != null && scriptModelDictionary[stepName].Complement.Count > 0)
                {
                    System.Windows.Forms.MessageBox.Show("它是其他补类的父集合");
                    return;
                }

                scriptModelDictionary.Remove(stepName);

                Dictionary<String, ScriptModel> _lightScriptDictionary = new Dictionary<String, ScriptModel>();
                foreach (var item in scriptModelDictionary)
                {
                    _lightScriptDictionary.Add(item.Key, item.Value);
                }
                scriptModelDictionary = _lightScriptDictionary;

                String _parentName = String.Empty;
                ////交集
                //foreach (var item in intersectionDictionary)
                //{
                //    if (item.Value.Contains(stepName))
                //    {
                //        item.Value.Remove(stepName);
                //        _parentName = item.Key;
                //        break;
                //    }
                //}
                //if (!_parentName.Equals(String.Empty))
                //{
                //    if (intersectionDictionary[_parentName].Count == 0)
                //    {
                //        intersectionDictionary.Remove(_parentName);
                //    }
                //}
                //if (intersectionDictionary.ContainsKey(stepName))
                //{
                //    intersectionDictionary.Remove(stepName);
                //}
                ////补集
                //_parentName = String.Empty;
                //foreach (var item in complementDictionary)
                //{
                //    if (item.Value.Contains(stepName))
                //    {
                //        item.Value.Remove(stepName);
                //        _parentName = item.Key;
                //        break;
                //    }
                //}
                //if (!_parentName.Equals(String.Empty))
                //{
                //    if (complementDictionary[_parentName].Count == 0)
                //    {
                //        complementDictionary.Remove(_parentName);
                //    }
                //}
                //if (complementDictionary.ContainsKey(stepName))
                //{
                //    complementDictionary.Remove(stepName);
                //}
                //lbStep.Items.RemoveAt(lbStep.SelectedIndex);
                //lbStep.SelectedIndex = -1;
                lbStep.Items.Remove(lbStep.SelectedItems[0]);
                //lbStep.SelectedItems.Remove(lbStep.SelectedItems[0]);
            }
            Test();
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

                ScriptModel scriptModel = scriptModelDictionary.Values.ToList().Find(delegate (ScriptModel model)
                {
                    return (model.OperationModels.ToList().FindAll(delegate (BaseOperationModel item)
                      {
                          if (item is CreateFromStepOperationModel)
                          {
                              return (item as CreateFromStepOperationModel).StepName.Equals(GetStepName(sp));
                          }
                          else {
                              return false;
                          }
                      }).Count) != 0;
                });

                if (scriptModel != null) {
                    lbStep.SelectedItem = lbStep.Items[i];
                    System.Windows.Forms.MessageBox.Show("该步骤有子类，不允许合并!");
                    return;
                }

                //foreach (var model in scriptModelDictionary)
                //{
                //    if (model.Value.Parent.Equals(GetStepName(sp)))
                //    {
                //        lbStep.SelectedItem = lbStep.Items[i];
                //        System.Windows.Forms.MessageBox.Show("该步骤有子类，不允许合并!");
                //        return;
                //    }
                //}

                if (finalDictionary.ContainsKey(GetStepName(sp)))
                {
                    lbStep.SelectedItem = lbStep.Items[i];
                    System.Windows.Forms.MessageBox.Show("该步骤有样式，请先将样式融入步骤!");
                    return;
                }
                if (scriptModelDictionary[GetStepName(sp)].Intersection.Count > 0)
                {
                    lbStep.SelectedItem = lbStep.Items[i];
                    System.Windows.Forms.MessageBox.Show("该步骤有(交)子集，请先将集合融入步骤!");
                    return;
                }
                if (scriptModelDictionary[GetStepName(sp)].Complement.Count > 0)
                {
                    lbStep.SelectedItem = lbStep.Items[i];
                    System.Windows.Forms.MessageBox.Show("该步骤有(补)子集，请先将集合融入步骤!");
                    return;
                }

                if (lockedDictionary.ContainsKey(GetStepName(sp)))
                {
                    lbStep.SelectedItem = lbStep.Items[i];
                    System.Windows.Forms.MessageBox.Show("该步骤已被锁定，请先解锁!");
                    return;
                }
                lbStep.SelectedItems.Add(lbStep.Items[i]);
            }
            //while (lbStep.SelectedItems.Count > 1)
            //{
            //    //Console.WriteLine("--------");
            //    //第1个被选中的融入第0个被选中的 后面融入前面
            //    //对比包含字段
            //    //被包含类字段不能与包含类字段重复，否则替换 
            //    List<String> newKey = new List<string>();
            //    Dictionary<String, String> _dictionary = new Dictionary<String, String>();//老字段，新字段
            //    StackPanel oldPanel = (StackPanel)lbStep.SelectedItems[1];
            //    StackPanel newPanel = (StackPanel)lbStep.SelectedItems[0];

            //    foreach (String str in scriptModelDictionary[GetStepName(oldPanel)].Contain)
            //    {
            //        if (!scriptModelDictionary[GetStepName(newPanel)].Contain.Contains(str))
            //        {
            //            //该字段不存在重复
            //            _dictionary.Add(str, str);
            //            newKey.Add(str);
            //        }
            //        else
            //        {
            //            int x = 1;
            //            while (x <= 100000)
            //            {
            //                if (!scriptModelDictionary[GetStepName(newPanel)].Contain.Contains("Step" + x) && !newKey.Contains("Step" + x))
            //                {
            //                    //不存在重复
            //                    _dictionary.Add(str, "Step" + x);
            //                    newKey.Add("Step" + x);
            //                    break;
            //                }
            //                x++;
            //            }
            //            if (x > 100000)
            //            {
            //                new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
            //                return;
            //            }
            //        }
            //    }
            //    String parentCommand = scriptModelDictionary[GetStepName(oldPanel)].Value;
            //    foreach (var item in _dictionary)
            //    {
            //        parentCommand = parentCommand.Replace(item.Key + "Light", item.Value + "Light");
            //        parentCommand = parentCommand.Replace(item.Key + "LightGroup", item.Value + "LightGroup");
            //        parentCommand = parentCommand.Replace(item.Key + "RangeGroup", item.Value + "RangeGroup");
            //        parentCommand = parentCommand.Replace(item.Key + "ColorGroup", item.Value + "ColorGroup");
            //        parentCommand = parentCommand.Replace(item.Key + "PositionGroup", item.Value + "PositionGroup");
            //        scriptModelDictionary[GetStepName(newPanel)].Contain.Add(item.Value);
            //    }
            //    String oldChildrenCommand = scriptModelDictionary[GetStepName(newPanel)].Value;
            //    String newChildrenCommand = parentCommand + Environment.NewLine + oldChildrenCommand;
            //    newChildrenCommand += Environment.NewLine + "\t" + GetStepName(newPanel) + "LightGroup.AddRange(" + _dictionary.Values.First() + "LightGroup);";

            //    scriptModelDictionary[GetStepName(newPanel)].Value = newChildrenCommand;
            //    scriptModelDictionary.Remove(GetStepName(oldPanel));

            //    Dictionary<String, ScriptModel> _lightScriptDictionary = new Dictionary<String, ScriptModel>();
            //    foreach (var item in scriptModelDictionary)
            //    {
            //        _lightScriptDictionary.Add(item.Key, item.Value);
            //    }
            //    scriptModelDictionary = _lightScriptDictionary;

            //    lbStep.Items.Remove(lbStep.SelectedItems[1]);
            //}
            Test();
        }
        private String GetVisibleImageStepName(Object sender)
        {
            DockPanel dock = (DockPanel)sender;
            StackPanel panel = (StackPanel)dock.Parent;
            TextBlock blockStepName = (TextBlock)panel.Children[1];
            return blockStepName.Text;
        }

        public List<Model.Light> mLaunchpadData = new List<Model.Light>();

        public void SetSize(double width, double height)
        {
            Width = width;
            Height = height;
        }
        public void UpdateData(Dictionary<string, List<Model.Light>> mLightList)
        {
            _bridge.UpdateData(mLightList);
        }
        private void BtnLastTimePoint_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _bridge.ToLastTime();
        }
        private void BtnNextTimePoint_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _bridge.ToNextTime();
        }
        private void TimePointCountLeft_TextChanged(object sender, TextChangedEventArgs e)
        {
            _bridge.tbTimePointCountLeft_TextChanged();
        }

        public void SetLaunchpadSize()
        {
            double minSize = dpShow.ActualWidth < dpShow.ActualHeight - 70 - 40 ? dpShow.ActualWidth : dpShow.ActualHeight - 70 - 40;
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
                //dgMain.Width = dpShow.ActualWidth;
                //dgMain.Height = dpShow.ActualHeight;
            }
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
            if (lbStep.SelectedIndex == -1 || scriptModelDictionary[GetStepName()].OperationModels.Count == 0)
                return;
            StyleWindow style = new StyleWindow(this);
            style.SetData(scriptModelDictionary[GetStepName()].OperationModels);
            mw.AddSetting(style);
        }

        int lastStepSelect = -1;
        private void LbStep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lastStepSelect != -1 && lbStep.Items.Count >= lastStepSelect + 1)
            {
                StackPanel sp = lbStep.Items[lastStepSelect] as StackPanel;
                sp.Background = new SolidColorBrush(Colors.Transparent);
            }

            lastStepSelect = lbStep.SelectedIndex;
            if (lbStep.SelectedIndex == -1)
            {
                //_bridge.UpdateForColor(mLightList, false);
                AddStepControlToolTip();
                sw.lbCatalog.Items.Clear();
                sw.svMain.Children.Clear();
            }
            else
            {
                //Test(GetStepName());
                //_bridge.UpdateForColor(mBlockLightList, true);
                //TODO:
                //spStepControl.ToolTip = null;

                //更新右侧Style
                StackPanel sp = lbStep.SelectedItem as StackPanel;
                sp.Background = new SolidColorBrush(Colors.Black);
                sw.SetData(scriptModelDictionary[GetStepName()].OperationModels);
            }
        }
        private void AddStepControlToolTip()
        {
            //if (mw.strMyLanguage.Equals("zh-CN"))
            //    spStepControl.ToolTip = "你需要选中步骤";
            //else
            //    spStepControl.ToolTip = "You need to select the step";
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
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.Z)
            {
                Unmake();
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.Y)
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
                dialog = new CollectionDialog(this);
            }
            else
            {
                dialog = new CollectionDialog(this, _name, type);
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
                    scriptModelDictionary[parentName].Intersection.Add(GetStepName());
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
                        Dictionary<String, ScriptModel> mLightScriptDictionary = new Dictionary<String, ScriptModel>();
                        List<string> mKey = new List<string>(scriptModelDictionary.Keys);
                        ScriptModel strUp = scriptModelDictionary[mKey[pPosition + 1]];
                        ScriptModel strDown = scriptModelDictionary[mKey[cPosition]];
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
                                mLightScriptDictionary.Add(mKey[i], scriptModelDictionary[mKey[i]]);
                            }
                        }
                        scriptModelDictionary = mLightScriptDictionary;
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
                        Dictionary<String, ScriptModel> mLightScriptDictionary = new Dictionary<String, ScriptModel>();
                        List<string> mKey = new List<string>(scriptModelDictionary.Keys);
                        ScriptModel strUp = scriptModelDictionary[mKey[pPosition]];
                        ScriptModel strDown = scriptModelDictionary[mKey[cPosition]];
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
                                mLightScriptDictionary.Add(mKey[i], scriptModelDictionary[mKey[i]]);
                            }
                        }
                        scriptModelDictionary = mLightScriptDictionary;
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
                        Dictionary<String, ScriptModel> mLightScriptDictionary = new Dictionary<String, ScriptModel>();
                        List<string> mKey = new List<string>(scriptModelDictionary.Keys);
                        //抽取子集
                        String nowKey = mKey[cPosition];
                        ScriptModel strDown = scriptModelDictionary[mKey[cPosition]];
                        mKey.RemoveAt(cPosition);
                        for (int i = 0; i < mKey.Count; i++)
                        {
                            if (i == pPosition)
                            {
                                mLightScriptDictionary.Add(nowKey, strDown);
                            }
                            mLightScriptDictionary.Add(mKey[i], scriptModelDictionary[mKey[i]]);
                        }
                        scriptModelDictionary = mLightScriptDictionary;
                    }
                }
                UpdateCollection();
                Test();
            }
        }

        private void CollectionIntegration(object sender, RoutedEventArgs e)
        {

            String parentName = String.Empty;
            String childName = GetStepName();
            CollectionType type = CollectionType.Intersection;
            foreach (var scriptModel in scriptModelDictionary)
            {
                if (scriptModel.Value.Intersection.Contains(childName))
                {
                    parentName = scriptModel.Key;
                    childName = GetStepName();
                    type = CollectionType.Intersection;
                    break;
                }
            }
            if (scriptModelDictionary[childName].Intersection.Count > 0)
            {
                parentName = GetStepName();
                childName = scriptModelDictionary[childName].Intersection[0];
                type = CollectionType.Intersection;
            }

            foreach (var scriptModel in scriptModelDictionary)
            {
                if (scriptModel.Value.Complement.Contains(childName))
                {
                    parentName = scriptModel.Key;
                    childName = GetStepName();
                    type = CollectionType.Complement;
                    break;
                }
            }
            if (scriptModelDictionary[childName].Complement.Count > 0)
            {
                parentName = GetStepName();
                childName = scriptModelDictionary[childName].Complement[0];
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

            //foreach (String str in scriptModelDictionary[GetStepName(newPanel)].Contain)
            //{
            //    if (!scriptModelDictionary[GetStepName(oldPanel)].Contain.Contains(str))
            //    {
            //        //该字段不存在重复
            //        _dictionary.Add(str, str);
            //        newKey.Add(str);
            //    }
            //    else
            //    {
            //        int x = 1;
            //        while (x <= 100000)
            //        {
            //            if (!scriptModelDictionary[GetStepName(oldPanel)].Contain.Contains("Step" + x) && !newKey.Contains("Step" + x))
            //            {
            //                //不存在重复
            //                _dictionary.Add(str, "Step" + x);
            //                newKey.Add("Step" + x);
            //                break;
            //            }
            //            x++;
            //        }
            //        if (x > 100000)
            //        {
            //            new MessageDialog(mw, "NoNameIsAvailable").ShowDialog();
            //            return;
            //        }
            //    }
            //}
            //String parentCommand = scriptModelDictionary[GetStepName(newPanel)].Value;
            //foreach (var item in _dictionary)
            //{
            //    parentCommand = parentCommand.Replace(item.Key + "Light", item.Value + "Light");
            //    parentCommand = parentCommand.Replace(item.Key + "LightGroup", item.Value + "LightGroup");
            //    parentCommand = parentCommand.Replace(item.Key + "RangeGroup", item.Value + "RangeGroup");
            //    parentCommand = parentCommand.Replace(item.Key + "ColorGroup", item.Value + "ColorGroup");
            //    parentCommand = parentCommand.Replace(item.Key + "PositionGroup", item.Value + "PositionGroup");
            //    scriptModelDictionary[GetStepName(newPanel)].Contain.Add(item.Value);
            //}
            //String oldChildrenCommand = scriptModelDictionary[GetStepName(oldPanel)].Value;
            //String newChildrenCommand = oldChildrenCommand + Environment.NewLine + parentCommand;
            ////newChildrenCommand += Environment.NewLine + "\t" + GetStepName(newPanel) + "LightGroup.Add(" + _dictionary.Values.First() + "LightGroup);";
            //if (type == CollectionType.Intersection)
            //{
            //    newChildrenCommand += Environment.NewLine + "\t" + GetStepName(oldPanel) + "LightGroup.CollectionOperation(LightGroup.INTERSECTION," + _dictionary.Values.First() + "LightGroup);";
            //}
            //else if (type == CollectionType.Complement)
            //{
            //    newChildrenCommand += Environment.NewLine + "\t" + GetStepName(oldPanel) + "LightGroup.CollectionOperation(LightGroup.COMPLEMENT ," + _dictionary.Values.First() + "LightGroup);";
            //}
            //scriptModelDictionary[GetStepName(oldPanel)].Value = newChildrenCommand;
            //scriptModelDictionary.Remove(GetStepName(newPanel));

            //Dictionary<string, ScriptModel> _lightScriptDictionary = new Dictionary<string, ScriptModel>();
            //foreach (var item in scriptModelDictionary)
            //{
            //    _lightScriptDictionary.Add(item.Key, item.Value);
            //}
            //scriptModelDictionary = _lightScriptDictionary;

            ////visibleDictionary.Remove(GetStepName(oldPanel));
            ////containDictionary.Remove(GetStepName(oldPanel));
            //lbStep.Items.Remove(lbStep.Items[cPosition]);

            //String _parentName = String.Empty;
            //foreach (var item in scriptModelDictionary)
            //{
            //    if (item.Value.Intersection.Contains(childName))
            //    {
            //        item.Value.Intersection.Remove(childName);
            //        _parentName = item.Key;
            //        break;
            //    }
            //}
            ////if (!_parentName.Equals(String.Empty))
            ////{
            ////    if (intersectionDictionary[_parentName].Count == 0)
            ////    {
            ////        intersectionDictionary.Remove(_parentName);
            ////    }
            ////}
            ////if (intersectionDictionary.ContainsKey(childName))
            ////{
            ////    intersectionDictionary.Remove(childName);
            ////}
            //foreach (var item in scriptModelDictionary)
            //{
            //    if (item.Value.Complement.Contains(childName))
            //    {
            //        item.Value.Complement.Remove(childName);
            //        _parentName = item.Key;
            //        break;
            //    }
            //}
            ////if (!_parentName.Equals(String.Empty))
            ////{
            ////    if (complementDictionary[_parentName].Count == 0)
            ////    {
            ////        complementDictionary.Remove(_parentName);
            ////    }
            ////}
            ////if (complementDictionary.ContainsKey(childName))
            ////{
            ////    complementDictionary.Remove(childName);
            ////}
            //Test();
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

        private void EditToScript2(object sender, RoutedEventArgs e)
        {
            DragDrop.DoDragDrop((TreeViewItem)sender, (TreeViewItem)sender, DragDropEffects.Copy);
            //DragDrop.DoDragDrop((TreeViewItem)sender, ((UIElement)sender).GetValue(NameProperty).ToString(), DragDropEffects.Copy);
        }

        private void EditToScript(object sender, RoutedEventArgs e)
        {
            if (lbStep.SelectedItems.Count == 0)
                return;
            for (int i = 0; i < lbStep.SelectedItems.Count; i++)
            {
                String name = GetStepName(lbStep.SelectedItems[i] as StackPanel);
                if (lockedDictionary.ContainsKey(name))
                {
                    new MessageDialog(mw, "TheStepIsLocked").ShowDialog();
                    return;
                }
                ScriptModel scriptModel = scriptModelDictionary[name];
                //没有可操作的灯光组
                //if (!scriptModel.Value.Contains(GetStepName(sp) + "LightGroup"))
                //{
                //    return;
                //}
                String command = String.Empty;
                if (sender == btnHorizontalFlipping)
                {
                    scriptModel.OperationModels.Add(new HorizontalFlippingOperationModel());
                }
                if (sender == btnVerticalFlipping)
                {
                    scriptModel.OperationModels.Add(new VerticalFlippingOperationModel());
                }
                if (sender == btnLowerLeftSlashFlipping)
                {
                    scriptModel.OperationModels.Add(new LowerLeftSlashFlippingOperationModel());
                }
                if (sender == btnLowerRightSlashFlipping)
                {
                    scriptModel.OperationModels.Add(new LowerRightSlashFlippingOperationModel());
                }
                if (sender == btnFold)
                {
                    scriptModel.OperationModels.Add(new FoldOperationModel(FoldOperationModel.Orientation.VERTICAL, 0, 0));
                }
                if (sender == btnClockwise)
                {
                    scriptModel.OperationModels.Add(new ClockwiseOperationModel());
                }
                if (sender == btnAntiClockwise)
                {
                    scriptModel.OperationModels.Add(new AntiClockwiseOperationModel());
                }
                if (sender == btnReversal)
                {
                    scriptModel.OperationModels.Add(new ReversalOperationModel());
                }
                if (sender == btnExtendTime)
                {
                    scriptModel.OperationModels.Add(new ChangeTimeOperationModel(ChangeTimeOperationModel.Operation.MULTIPLICATION, 2));
                }
                if (sender == btnShortenTime)
                {
                    scriptModel.OperationModels.Add(new ChangeTimeOperationModel(ChangeTimeOperationModel.Operation.DIVISION, 2));
                }
                if (sender == btnDiyTime)
                {
                    scriptModel.OperationModels.Add(new ChangeTimeOperationModel(ChangeTimeOperationModel.Operation.MULTIPLICATION, 1));
                }
                if (sender == btnMatchTime)
                {
                    scriptModel.OperationModels.Add(new OneNumberOperationModel("MatchTotalTimeLattice", 12, "TotalTimeLatticeColon", OneNumberOperationModel.NumberType.OTHER));
                }
                if (sender == btnInterceptTime)
                {
                    scriptModel.OperationModels.Add(new InterceptTimeOperationModel(0, 12));
                }
                if (sender == btnRemoveBorder)
                {
                    scriptModel.OperationModels.Add(new RemoveBorderOperationModel());
                }
                if (sender == btnFillColor)
                {
                    scriptModel.OperationModels.Add(new OneNumberOperationModel("FillColor", 5, "FillColorColon", OneNumberOperationModel.NumberType.COLOR));
                }
                if (sender == btnChangeColorYellow)
                {
                    scriptModel.OperationModels.Add(new ChangeColorOperationModel(new List<int>() { 73, 74, 75, 76 }));
                }
                if (sender == btnChangeColorBlue)
                {
                    scriptModel.OperationModels.Add(new ChangeColorOperationModel(new List<int>() { 33, 37, 41, 45 }));
                }
                if (sender == btnChangeColorPink)
                {
                    scriptModel.OperationModels.Add(new ChangeColorOperationModel(new List<int>() { 4, 94, 53, 57 }));
                }
                if (sender == btnChangeColorDiy)
                {
                    List<Model.Light> mLightList = mLightDictionary[name];
                    List<int> mColor = new List<int>();
                    for (int j = 0; j < mLightList.Count; j++)
                    {
                        if (mLightList[j].Action == 144)
                        {
                            if (!mColor.Contains(mLightList[j].Color))
                            {
                                mColor.Add(mLightList[j].Color);
                            }
                        }
                    }
                    mColor.Sort();
                    scriptModel.OperationModels.Add(new ChangeColorOperationModel(mColor));
                }
                if (sender == btnColorChange)
                {
                    ListChangeColorDialog colorDialog = new ListChangeColorDialog(mw, mLightDictionary[name]);
                    if (colorDialog.ShowDialog() == true)
                    {
                        List<int> mColor = new List<int>();
                        for (int x = 0; x < colorDialog.lbColor.Items.Count; x++)
                        {
                            if (int.TryParse(colorDialog.lbColor.Items[x].ToString(), out int color))
                            {
                                mColor.Add(color);
                            }
                            else
                            {
                                return;
                            }
                        }
                        scriptModel.OperationModels.Add(new ChangeColorOperationModel(mColor));
                    }
                    else
                    {
                        return;
                    }
                }
                if (sender == btnColorWithCount)
                {
                    scriptModel.OperationModels.Add(new ColorWithCountOperationModel(new List<int>() { 5 }));
                }
                if (sender == btnSetStartTime)
                {
                    scriptModel.OperationModels.Add(new OneNumberOperationModel("SetStartTime", 0, "PleaseEnterTheStartTimeColon", OneNumberOperationModel.NumberType.OTHER));
                }
                if (sender == btnSetEndTime)
                {
                    scriptModel.OperationModels.Add(new SetEndTimeOperationModel(SetEndTimeOperationModel.Type.ALL, "12"));
                }
                if (sender == btnSetAllTime)
                {
                    scriptModel.OperationModels.Add(new OneNumberOperationModel("SetAllTime", 12, "PleaseEnterTheConstantTimeColon", OneNumberOperationModel.NumberType.OTHER));
                }
                if (sender == btnDisappear)
                {
                    scriptModel.OperationModels.Add(new AnimationDisappearOperationModel(0, 12));
                }
                if (sender == btnWindmill)
                {
                    scriptModel.OperationModels.Add(new OneNumberOperationModel("Animation.Windmill", 12, "IntervalColon", OneNumberOperationModel.NumberType.OTHER));
                }
                if (sender == btnCopyToTheEnd)
                {
                    scriptModel.OperationModels.Add(new CopyToTheEndOperationModel(new List<int>() { 5 }));
                }
                if (sender == btnCopyToTheFollow)
                {
                    scriptModel.OperationModels.Add(new CopyToTheFollowOperationModel(new List<int>() { 5 }));
                }
                if (sender == btnAccelerationOrDeceleration)
                {
                    scriptModel.OperationModels.Add(new AccelerationOrDecelerationOperationModel(new List<int>() { 100 }));
                }
                if (sender == miSquare
                    || sender == miRadialVertical
                    || sender == miRadialHorizontal)
                {
                    if (sender == miSquare)
                    {
                        scriptModel.OperationModels.Add(new ShapeColorOperationModel(ShapeColorOperationModel.ShapeType.SQUARE, new List<int>() { 5, 5, 5, 5, 5, 5 }));
                    }
                    if (sender == miRadialVertical)
                    {
                        scriptModel.OperationModels.Add(new ShapeColorOperationModel(ShapeColorOperationModel.ShapeType.RADIALVERTICAL, new List<int>() { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }));
                    }
                    if (sender == miRadialHorizontal)
                    {
                        scriptModel.OperationModels.Add(new ShapeColorOperationModel(ShapeColorOperationModel.ShapeType.RADIALHORIZONTAL, new List<int>() { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }));
                    }
                }
                //if (sender.Parent == miChildThirdParty)
                //{
                //    ThirdPartyOperationModel thirdPartyOperationModel = new ThirdPartyOperationModel(thirdPartys[miChildThirdParty.Items.IndexOf(sender)].name, thirdPartys[miChildThirdParty.Items.IndexOf(sender)].dll);

                //    XDocument doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"Operation\View\" + thirdPartys[miChildThirdParty.Items.IndexOf(sender)].view);
                //    foreach (XElement element in doc.Element("Views").Elements())
                //    {
                //        thirdPartyOperationModel.Parameters.Add(element.Attribute("default").Value);
                //    }
                //    scriptModel.OperationModels.Add(thirdPartyOperationModel);
                //}

                //StyleWindow style = new StyleWindow(mw);
                sw.SetData(scriptModelDictionary[name].OperationModels, true);
                //mw.AddSetting(style);
            }
            Test();
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
            //visibleDictionary.Add(stepName, true);
            containDictionary.Add(stepName, new List<string>() { stepName });
            //if (RefreshData())
            //{
            //    AddStep(stepName, "");
            //    lbStep.SelectedIndex = lbStep.Items.Count - 1;
            //}
        }

        private void Menu_MouseEnter(object sender, RoutedEventArgs e)
        {
            HideAllPopup();

            e.Handled = true;
        }

        private void UserControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HideAllPopup();
        }

        private void HideAllPopup()
        {

        }


        private void ImageUnmake_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Unmake();
        }

        private void ImageRedo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Redo();
        }

        private void Unmake()
        {
            if (iNowPosition == -1)
                return;
            int mINowPosition = iNowPosition - 1;
            int selectedIndex = lbStep.SelectedIndex;
            //if (mINowPosition == -1)
            //    mINowPosition = 99;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + mINowPosition + ".lightScript"))
            {
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + mINowPosition + ".lightScript", filePath, true);
                _bIsEdit = true;
                isRedo = false;

                iNowPosition -= 1;
                //重新加载
                LoadFile(System.IO.Path.GetFileName(filePath));
                if (selectedIndex < lbStep.Items.Count)
                {
                    lbStep.SelectedIndex = selectedIndex;
                }
            }
        }

        public bool isRedo = false;

        private void Redo()
        {
            if (iNowPosition == -1)
                return;
            int mINowPosition = iNowPosition + 1;
            int selectedIndex = lbStep.SelectedIndex;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + mINowPosition + ".lightScript"))
            {
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + mINowPosition + ".lightScript", filePath, true);
                _bIsEdit = true;
                isRedo = true;

                iNowPosition += 1;
                //重新加载
                LoadFile(System.IO.Path.GetFileName(filePath));
                if (selectedIndex < lbStep.Items.Count)
                {
                    lbStep.SelectedIndex = selectedIndex;
                }
            }
        }

        private void GetExecutionTime(object sender, MouseButtonEventArgs e)
        {
            _bIsEdit = true;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //耗时巨大的代码  
            Test();
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            new MessageDialog(mw, String.Format("总共花费{0}ms.", ts2.TotalMilliseconds), 0).ShowDialog();
            //File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Cache\" + iNowPosition + ".lightScript");
            //iNowPosition--;
        }

        private void GetCompleteScript(object sender, MouseButtonEventArgs e)
        {
            new MessageDialog(mw, GetCompleteScript(), 0).ShowDialog();
        }


        private void MiIntroduce_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EidtTextDialog edit = new EidtTextDialog();
            edit.SetData(introduce);
            if (edit.ShowDialog() == true)
            {
                introduce = edit.GetData();
                Test();
                //SaveLightScriptFile("", false);
            }
        }

        private void LibraryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //MenuItem item = (MenuItem)sender;
            // ImportLibraryDialog dialog = new ImportLibraryDialog(mw, AppDomain.CurrentDomain.BaseDirectory + @"\Library\" + item.Header.ToString() + ".lightScript");
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
            //    //visibleDictionary.Add(UsableStepName, true);
            //    containDictionary.Add(UsableStepName, new List<String>() { UsableStepName });
            //    //RefreshData();
            //}
        }



        private void MyContentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DragDrop.DoDragDrop((ListBoxItem)sender, (ListBoxItem)sender, DragDropEffects.Copy);
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
            return;
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
            //    //RefreshData();
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
            List<int> mColor = _bridge.mColor.ToList();
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
            //RefreshData();
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
            else
            {
                lockedDictionary.Add(stepName, RefreshData(stepName));
            }
            UpdateLocked();
            //RefreshData();
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
        public int iNowPosition = -1;
        public void ClearCache()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"Cache");
                if (!dir.Exists)
                    return;
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public bool _bIsEdit = false;

        protected override void LoadFileContent()
        {
            if (!_bIsEdit)
            {
                ClearCache();
                iNowPosition = -1;
            }
            //Import Introduce Final Locked
            scriptModelDictionary.Clear();
            scriptModelDictionary = bridge.GetScriptModelDictionary(filePath, out String introduce,out String audioResources);
            this.introduce = introduce;
            AudioResources = audioResources;
            tbMusic.Text = AudioResources;

            UpdateStep();
            UpdateCollection();
            //command =
            //    "PositionGroup  Step1Position = new PositionGroup(\"36 37 38 39\",' ','-');" +
            //    "ColorGroup Step1Color = new ColorGroup(\"5\", ' ', '-');" +
            //    "LightGroup Step1LightGroup = Create.CreateLightGroup(0, Step1Position, 12, 12, Step1Color, Create.UP,Create.ALL);";
            //SaveFile();

            Test();
            //mw.SetSpFilePosition(mw.filePosition);
        }

        public void UpdateStep()
        {
            lbStep.Items.Clear();
            foreach (var item in scriptModelDictionary)
            {
                CreateFromStepOperationModel createFromStepOperationModel = (CreateFromStepOperationModel)item.Value.OperationModels.Find(model => model is CreateFromStepOperationModel);

                if (createFromStepOperationModel != null)
                {
                    AddStep(item.Value.Name, createFromStepOperationModel.StepName);
                }
                else {
                    AddStep(item.Value.Name, "");
                }
         
            }
            UpdateVisible();
        }
        protected override void CreateFile(String filePath)
        {
            //获取对象
            XDocument xDoc = new XDocument();
            XElement xRoot = new XElement("Root");
            XElement xScripts = new XElement("Scripts");
            xDoc.Add(xRoot);
            xRoot.Add(xScripts);
            xDoc.Save(filePath);
        }

        public override void SaveFile()
        {
            if (filePath.Equals(String.Empty))
                return;
            //获取对象
            XDocument xDoc = new XDocument();
            XElement xRoot = new XElement("Root");
            XElement xScripts = new XElement("Scripts");
            xDoc.Add(xRoot);
            xRoot.Add(xScripts);
            XElement xIntroduce = new XElement("Introduce")
            {
                Value = introduce
            };
            xRoot.Add(xIntroduce);
            XElement xAudioResources = new XElement("AudioResources")
            {
                Value = AudioResources
            };
            xRoot.Add(xAudioResources);
            foreach (var item in scriptModelDictionary)
            {
                XElement xScript = new XElement("Script");
                xScript.SetAttributeValue("name", item.Key);
                xScript.SetAttributeValue("value", Business.FileBusiness.CreateInstance().String2Base(item.Value.Value));
                xScript.SetAttributeValue("visible", item.Value.Visible);
                StringBuilder builder = new StringBuilder();
                //for (int i = 0; i < item.Value.Contain.Count; i++)
                //{
                //    builder.Append(item.Value.Contain[i] + " ");
                //}
                xScript.SetAttributeValue("contain", builder.ToString().Trim());

                StringBuilder builder2 = new StringBuilder();
                if (item.Value.Intersection != null)
                {
                    for (int i = 0; i < item.Value.Intersection.Count; i++)
                    {
                        builder2.Append(item.Value.Intersection[i] + " ");

                    }
                    xScript.SetAttributeValue("intersection", builder2.ToString().Trim());
                }
                else
                {
                    xScript.SetAttributeValue("intersection", "");
                }

                StringBuilder builder3 = new StringBuilder();
                if (item.Value.Complement != null)
                {
                    for (int i = 0; i < item.Value.Complement.Count; i++)
                    {
                        builder3.Append(item.Value.Complement[i] + " ");
                    }
                    xScript.SetAttributeValue("complement", builder3.ToString().Trim());
                }
                else
                {
                    xScript.SetAttributeValue("complement", "");
                }
         
                foreach (var mItem in item.Value.OperationModels)
                {
                    xScript.Add(mItem.GetXElement());
                }
                xScripts.Add(xScript);
            }
            xDoc.Save(filePath);
        }

        public void CopyFile()
        {
            if (isRedo)
                iNowPosition++;

            if (_bIsEdit)
                return;

            iNowPosition++;
            File.Copy(filePath, AppDomain.CurrentDomain.BaseDirectory + @"Cache\" + iNowPosition + ".lightScript", true);
            if (iNowPosition == 999)
            {
                new MessageDialog(mw, "Edit999").ShowDialog();
                ClearCache();
                iNowPosition = -1;
                _bIsEdit = false;
                Test();
            }
        }
        /// <summary>
        /// 清除输入控件里的数据
        /// </summary>
        public void ClearInputUserControl()
        {
            //UpdateData(new List<Light>());
            lbStep.Items.Clear();
            lightScriptDictionary.Clear();
            //visibleDictionary.Clear();
            containDictionary.Clear();
            extendsDictionary.Clear();
            intersectionDictionary.Clear();
            complementDictionary.Clear();
            importList.Clear();
            finalDictionary.Clear();
            introduce = String.Empty;
            lockedDictionary.Clear();
        }


        private void lbStep_Drop(object sender2, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(TreeViewItem)) is TreeViewItem)
            {
                TreeViewItem sender = e.Data.GetData(typeof(TreeViewItem)) as TreeViewItem;
                StackPanel sp;
                if (e.OriginalSource is StackPanel)
                {
                    sp = (StackPanel)e.OriginalSource;
                }
                else
                {
                    FrameworkElement element = (FrameworkElement)e.OriginalSource;
                    while (!(element is StackPanel))
                    {
                        element = (FrameworkElement)element.Parent;
                    }
                    sp = (StackPanel)element;
                }
             
            }
            //Test();
        }
        //不写入XML,而是直接写入代码
        //private void lbStep_Drop(object sender2, DragEventArgs e)
        //{
        //    TreeViewItem sender = e.Data.GetData(typeof(TreeViewItem)) as TreeViewItem;
        //    StackPanel sp;
        //    if (e.OriginalSource is StackPanel)
        //    {
        //        sp = (StackPanel)e.OriginalSource;
        //    }
        //    else
        //    {
        //        FrameworkElement element = (FrameworkElement)e.OriginalSource;
        //        int i = 0;
        //        while (!(element is StackPanel))
        //        {
        //            element = (FrameworkElement)element.Parent;
        //            if (i == 10)
        //                break;
        //            i++;
        //        }
        //        sp = (StackPanel)element;
        //    }

        //    if (lockedDictionary.ContainsKey(GetStepName(sp)))
        //    {
        //        new MessageDialog(mw, "TheStepIsLocked").ShowDialog();
        //        return;
        //    }
        //    ScriptModel scriptModel = scriptModelDictionary[GetStepName(sp)];
        //    //没有可操作的灯光组
        //    if (!scriptModel.Value.Contains(GetStepName(sp) + "LightGroup"))
        //    {
        //        return;
        //    }
        //    String command = String.Empty;
        //    if (sender == btnHorizontalFlipping)
        //    {
        //        scriptModel.OperationModels.Add(new HorizontalFlippingOperationModel());
        //        //scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.HorizontalFlipping();";
        //    }
        //    if (sender == btnVerticalFlipping)
        //    {
        //        scriptModel.OperationModels.Add(new VerticalFlippingOperationModel());
        //        //scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.VerticalFlipping();";
        //    }
        //    if (sender == btnLowerLeftSlashFlipping)
        //    {
        //        scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.LowerLeftSlashFlipping();";
        //    }
        //    if (sender == btnLowerRightSlashFlipping)
        //    {
        //        scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.LowerRightSlashFlipping();";
        //    }
        //    if (sender == btnFold)
        //    {
        //        Edit_FoldDialog dialog = new Edit_FoldDialog(mw, GetStepName(sp));
        //        if (dialog.ShowDialog() == true)
        //        {
        //            scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.Fold(";
        //            if (dialog.cbOrientation.SelectedIndex == 0)
        //            {
        //                scriptModel.Value += "LightGroup.HORIZONTAL,";
        //            }
        //            else if (dialog.cbOrientation.SelectedIndex == 1)
        //            {
        //                scriptModel.Value += "LightGroup.VERTICAL,";
        //            }
        //            scriptModel.Value += dialog.tbStartPosition.Text + "," + dialog.tbSpan.Text + "); ";
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    if (sender == btnClockwise)
        //    {
        //        scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.Clockwise();";
        //    }
        //    if (sender == btnAntiClockwise)
        //    {
        //        scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.AntiClockwise();";
        //    }
        //    if (sender == btnReversal)
        //    {
        //        scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.Reversal();";
        //    }
        //    if (sender == btnExtendTime)
        //    {
        //        scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.ChangeTime(LightGroup.MULTIPLICATION,2);";
        //    }
        //    if (sender == btnShortenTime)
        //    {
        //        scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.ChangeTime(LightGroup.DIVISION,2);";
        //    }
        //    if (sender == btnDiyTime)
        //    {
        //        ChangeTimeDialog ct = new ChangeTimeDialog(mw);
        //        if (ct.ShowDialog() == true)
        //        {
        //            if (ct.cbOperation.SelectedIndex == 0)
        //            {
        //                scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.ChangeTime(LightGroup.MULTIPLICATION," + ct.tbPolyploidy.Text + ");";
        //            }
        //            else
        //            {
        //                scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.ChangeTime(LightGroup.DIVISION," + ct.tbPolyploidy.Text + ");";
        //            }
        //        }
        //    }
        //    if (sender == btnMatchTime)
        //    {
        //        GetNumberDialog dialog = new GetNumberDialog(mw, "TotalTimeLatticeColon", false);
        //        if (dialog.ShowDialog() == true)
        //        {
        //            scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.MatchTotalTimeLattice(" + dialog.OneNumber + ");";
        //        }
        //    }
        //    if (sender == btnInterceptTime)
        //    {
        //        InterceptTimeDialog dialog = new InterceptTimeDialog(mw);
        //        if (dialog.ShowDialog() == true)
        //        {
        //            scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.InterceptTime(" + dialog.Min + "," + dialog.Max + ");";
        //        }
        //    }
        //    if (sender == btnRemoveBorder)
        //    {
        //        scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.RemoveBorder();";
        //    }

        //    if (sender == btnFillColor)
        //    {
        //        GetNumberDialog dialog = new GetNumberDialog(mw, "FillColorColon", false);
        //        if (dialog.ShowDialog() == true)
        //        {
        //            scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.FillColor(" + dialog.OneNumber + ");";
        //        }
        //    }

        //    if (sender == btnChangeColorYellow || sender == btnChangeColorBlue || sender == btnChangeColorPink || sender == btnChangeColorDiy || sender == btnColorChange)
        //    {
        //        String colorGroupName = String.Empty;
        //        int i = 1;
        //        while (i <= 100000)
        //        {
        //            if (!scriptModel.Contain.Contains("Step" + i))
        //            {
        //                scriptModel.Contain.Add("Step" + i);
        //                colorGroupName = "Step" + i + "ColorGroup";
        //                break;
        //            }
        //            i++;
        //        }
        //        if (i > 100000)
        //        {
        //            new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
        //            return;
        //        }
        //        command = Environment.NewLine + "\tColorGroup " + colorGroupName + " = new ColorGroup(\"";
        //        if (sender == btnChangeColorYellow)
        //            command += "73 74 75 76";
        //        if (sender == btnChangeColorBlue)
        //            command += "33 37 41 45";
        //        if (sender == btnChangeColorPink)
        //            command += "4 94 53 57";
        //        if (sender == btnChangeColorDiy)
        //        {
        //            mLightList = RefreshData(GetStepName());
        //            List<int> mColor = new List<int>();
        //            for (int j = 0; j < mLightList.Count; j++)
        //            {
        //                if (mLightList[j].Action == 144)
        //                {
        //                    if (!mColor.Contains(mLightList[j].Color))
        //                    {
        //                        mColor.Add(mLightList[j].Color);
        //                    }
        //                }
        //            }
        //            mColor.Sort();
        //            GetNumberDialog dialog = new GetNumberDialog(mw, "CustomFormattedColorColon", true, mColor);
        //            if (dialog.ShowDialog() == true)
        //            {
        //                StringBuilder mBuilder = new StringBuilder();
        //                for (int x = 0; x < dialog.MultipleNumber.Count; x++)
        //                {
        //                    if (x != dialog.MultipleNumber.Count - 1)
        //                    {
        //                        mBuilder.Append(dialog.MultipleNumber[x] + " ");
        //                    }
        //                    else
        //                    {
        //                        mBuilder.Append(dialog.MultipleNumber[x]);
        //                    }
        //                }
        //                command += mBuilder.ToString();
        //            }
        //            else
        //            {
        //                return;
        //            }
        //        }
        //        if (sender == btnColorChange)
        //        {
        //            ListChangeColorDialog colorDialog = new ListChangeColorDialog(mw, RefreshData(GetStepName(sp)));
        //            if (colorDialog.ShowDialog() == true)
        //            {
        //                StringBuilder mBuilder = new StringBuilder();
        //                for (int x = 0; x < colorDialog.lbColor.Items.Count; x++)
        //                {
        //                    if (x != colorDialog.lbColor.Items.Count - 1)
        //                    {
        //                        mBuilder.Append(colorDialog.lbColor.Items[x].ToString() + " ");
        //                    }
        //                    else
        //                    {
        //                        mBuilder.Append(colorDialog.lbColor.Items[x].ToString());
        //                    }
        //                }
        //                command += mBuilder.ToString();
        //            }
        //            else
        //            {
        //                return;
        //            }
        //        }
        //        command += "\",' ','-');" + Environment.NewLine
        //      + "\t" + GetStepName(sp) + "LightGroup.SetColor(" + colorGroupName + ");";
        //        scriptModel.Value += command;
        //    }
        //    if (sender == btnColorWithCount)
        //    {

        //        GetNumberDialog dialog = new GetNumberDialog(mw, "WithCountColon", true);
        //        if (dialog.ShowDialog() == true)
        //        {
        //            String colorGroupName = String.Empty;
        //            int i = 1;
        //            while (i <= 100000)
        //            {
        //                if (!scriptModel.Contain.Contains("Step" + i))
        //                {
        //                    scriptModel.Contain.Add("Step" + i);
        //                    colorGroupName = "Step" + i + "ColorGroup";
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (i > 100000)
        //            {
        //                new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
        //                return;
        //            }
        //            StringBuilder builder = new StringBuilder();
        //            foreach (int n in dialog.MultipleNumber)
        //            {
        //                builder.Append(n + " ");
        //            }
        //            command = Environment.NewLine + "\tColorGroup " + colorGroupName + " = new ColorGroup(\"" + builder.ToString().Trim();
        //            command += "\",' ','-');";
        //            command += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.ColorWithCount(" + colorGroupName + ");";
        //            scriptModel.Value += command;
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    if (sender == btnSetStartTime)
        //    {
        //        GetNumberDialog dialog = new GetNumberDialog(mw, "PleaseEnterTheStartTimeColon", false);
        //        if (dialog.ShowDialog() == true)
        //        {
        //            scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.SetStartTime(" + dialog.OneNumber + ");";
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    if (sender == btnSetEndTime)
        //    {
        //        Edit_SetEndTimeDialog dialog = new Edit_SetEndTimeDialog(mw, GetStepName(sp) + "LightGroup");
        //        if (dialog.ShowDialog() == true)
        //        {
        //            command = Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.SetEndTime(";
        //            if (dialog.cbType.SelectedIndex == 0)
        //            {
        //                command += "LightGroup.ALL,";
        //            }
        //            else if (dialog.cbType.SelectedIndex == 1)
        //            {
        //                command += "LightGroup.END,";
        //            }
        //            else if (dialog.cbType.SelectedIndex == 2)
        //            {
        //                command += "LightGroup.ALLANDEND,";
        //            }
        //            command += "\"" + dialog.tbValue.Text + "\");";
        //            scriptModel.Value += command;
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    if (sender == btnSetAllTime)
        //    {
        //        GetNumberDialog dialog = new GetNumberDialog(mw, "PleaseEnterTheConstantTimeColon", false);
        //        if (dialog.ShowDialog() == true)
        //        {
        //            scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.SetAllTime(" + dialog.OneNumber + ");";
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    if (sender == btnDisappear)
        //    {
        //        Edit_AnimationDisappearDialog dialog = new Edit_AnimationDisappearDialog(mw, GetStepName(sp));
        //        if (dialog.ShowDialog() == true)
        //        {
        //            scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Animation.Serpentine(" + GetStepName(sp) + "LightGroup," + dialog.tbStartTime.Text + "," + dialog.tbInterval.Text + ");";
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    if (sender == btnWindmill)
        //    {
        //        GetNumberDialog dialog = new GetNumberDialog(mw, "IntervalColon", false);
        //        if (dialog.ShowDialog() == true)
        //        {
        //            scriptModel.Value += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup = Animation.Windmill(" + GetStepName(sp) + "LightGroup," + dialog.OneNumber + ");";
        //        }
        //    }
        //    if (sender == btnCopyToTheEnd)
        //    {
        //        String colorGroupName = String.Empty;
        //        int i = 1;
        //        while (i <= 100000)
        //        {
        //            if (!scriptModel.Contain.Contains("Step" + i))
        //            {
        //                scriptModel.Contain.Add("Step" + i);
        //                colorGroupName = "Step" + i + "ColorGroup";
        //                break;
        //            }
        //            i++;
        //        }
        //        if (i > 100000)
        //        {
        //            new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
        //            return;
        //        }
        //        GetNumberDialog dialog = new GetNumberDialog(mw, "PleaseEnterANewColorGroupColon", true);
        //        if (dialog.ShowDialog() == true)
        //        {
        //            StringBuilder mBuilder = new StringBuilder();
        //            for (int x = 0; x < dialog.MultipleNumber.Count; x++)
        //            {
        //                if (x != dialog.MultipleNumber.Count - 1)
        //                {
        //                    mBuilder.Append(dialog.MultipleNumber[x].ToString() + " ");
        //                }
        //                else
        //                {
        //                    mBuilder.Append(dialog.MultipleNumber[x].ToString());
        //                }
        //            }
        //            command = Environment.NewLine + "\tColorGroup " + colorGroupName + " = new ColorGroup(\""
        //              + mBuilder.ToString() + "\",' ','-');" + Environment.NewLine
        //            + "\t" + GetStepName(sp) + "LightGroup.CopyToTheEnd(" + colorGroupName + "); ";
        //            scriptModel.Value += command;
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    if (sender == btnCopyToTheFollow)
        //    {
        //        String colorGroupName = String.Empty;
        //        int i = 1;
        //        while (i <= 100000)
        //        {
        //            if (!scriptModel.Contain.Contains("Step" + i))
        //            {
        //                scriptModel.Contain.Add("Step" + i);
        //                colorGroupName = "Step" + i + "ColorGroup";
        //                break;
        //            }
        //            i++;
        //        }
        //        if (i > 100000)
        //        {
        //            new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
        //            return;
        //        }
        //        GetNumberDialog dialog = new GetNumberDialog(mw, "PleaseEnterANewColorGroupColon", true);
        //        if (dialog.ShowDialog() == true)
        //        {
        //            StringBuilder mBuilder = new StringBuilder();
        //            for (int x = 0; x < dialog.MultipleNumber.Count; x++)
        //            {
        //                if (x != dialog.MultipleNumber.Count - 1)
        //                {
        //                    mBuilder.Append(dialog.MultipleNumber[x].ToString() + " ");
        //                }
        //                else
        //                {
        //                    mBuilder.Append(dialog.MultipleNumber[x].ToString());
        //                }
        //            }
        //            command = Environment.NewLine + "\tColorGroup " + colorGroupName + " = new ColorGroup(\""
        //              + mBuilder.ToString() + "\",' ','-');" + Environment.NewLine
        //            + "\t" + GetStepName(sp) + "LightGroup.CopyToTheFollow(" + colorGroupName + "); ";
        //            scriptModel.Value += command;
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }

        //    if (sender == btnAccelerationOrDeceleration)
        //    {
        //        String rangeGroupName = String.Empty;
        //        int i = 1;
        //        while (i <= 100000)
        //        {
        //            if (!scriptModel.Contain.Contains("Step" + i))
        //            {
        //                scriptModel.Contain.Add("Step" + i);
        //                rangeGroupName = "Step" + i + "RangeGroup";
        //                break;
        //            }
        //            i++;
        //        }
        //        if (i > 100000)
        //        {
        //            new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
        //            return;
        //        }
        //        GetNumberDialog dialog = new GetNumberDialog(mw, "PleaseEnterTheDurationColon", true);
        //        if (dialog.ShowDialog() == true)
        //        {
        //            StringBuilder mBuilder = new StringBuilder();
        //            for (int x = 0; x < dialog.MultipleNumber.Count; x++)
        //            {
        //                if (x != dialog.MultipleNumber.Count - 1)
        //                {
        //                    mBuilder.Append(dialog.MultipleNumber[x].ToString() + " ");
        //                }
        //                else
        //                {
        //                    mBuilder.Append(dialog.MultipleNumber[x].ToString());
        //                }
        //            }
        //            command = Environment.NewLine + "\tRangeGroup " + rangeGroupName + " = new RangeGroup(\""
        //              + mBuilder.ToString() + "\",' ','-');" + Environment.NewLine
        //            + "\t" + GetStepName(sp) + "LightGroup.AccelerationOrDeceleration(" + rangeGroupName + "); ";
        //            scriptModel.Value += command;
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }

        //    if (sender == miSquare || sender == miRadialVertical || sender == miRadialHorizontal)
        //    {
        //        String colorGroupName = String.Empty;
        //        int i = 1;
        //        while (i <= 100000)
        //        {
        //            if (!scriptModel.Contain.Contains("Step" + i))
        //            {
        //                scriptModel.Contain.Add("Step" + i);
        //                colorGroupName = "Step" + i + "ColorGroup";
        //                break;
        //            }
        //            i++;
        //        }
        //        if (i > 100000)
        //        {
        //            new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
        //            return;
        //        }

        //        ShapeColorDialog dialog;
        //        if (sender == miSquare)
        //        {
        //            dialog = new ShapeColorDialog(mw, 0);
        //        }
        //        else if (sender == miRadialVertical)
        //        {
        //            dialog = new ShapeColorDialog(mw, 1);
        //        }
        //        else
        //        {
        //            dialog = new ShapeColorDialog(mw, 2);
        //        }
        //        if (dialog.ShowDialog() == true)
        //        {
        //            command = Environment.NewLine + "\tColorGroup " + colorGroupName + " = new ColorGroup(\""
        //              + dialog.content + "\",' ','-');";

        //            if (sender == miSquare)
        //            {
        //                command += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.ShapeColor(LightGroup.SQUARE," + colorGroupName + ");";
        //            }
        //            else if (sender == miRadialVertical)
        //            {
        //                command += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.ShapeColor(LightGroup.RADIALVERTICAL," + colorGroupName + ");";
        //            }
        //            else if (sender == miRadialHorizontal)
        //            {
        //                command += Environment.NewLine + "\t" + GetStepName(sp) + "LightGroup.ShapeColor(LightGroup.RADIALHORIZONTAL," + colorGroupName + ");";
        //            }
        //            scriptModel.Value += command;
        //        }
        //    }
        //    Test();
        //}
        private void cTime_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _bridge.UpdateData();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender == iExecutionTime)
                iExecutionTime.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/executiontime_blue.png", UriKind.RelativeOrAbsolute));
            if (sender == iCompleteScript)
                iCompleteScript.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/completescript_blue.png", UriKind.RelativeOrAbsolute));
            if (sender == iIntroduce)
                iIntroduce.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/introduce_blue.png", UriKind.RelativeOrAbsolute));
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender == iExecutionTime)
                iExecutionTime.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/executiontime_gray.png", UriKind.RelativeOrAbsolute));
            if (sender == iCompleteScript)
                iCompleteScript.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/completescript_gray.png", UriKind.RelativeOrAbsolute));
            if (sender == iIntroduce)
                iIntroduce.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/introduce_gray.png", UriKind.RelativeOrAbsolute));

        }


        public void NewFromImport(String fileName, String _stepName)
        {
            String stepName = GetUsableStepName();
            //String commandLine = "\tLightGroup Step1LightGroup = Create.CreateFromLightScriptFile(\"" + fileName + "\",\"" + _stepName + "\");";
            ScriptModel scriptModel = new ScriptModel();
            scriptModel.Name = stepName;
            scriptModel.Value = "";
            scriptModel.Visible = true;
            scriptModel.OperationModels = new List<BaseOperationModel>() { new CreateFromFileOperationModel(fileName, _stepName) };
            //scriptModel.Contain = new List<string>() { stepName };
            scriptModel.Intersection = new List<string>();
            scriptModel.Complement = new List<string>();
            scriptModelDictionary.Add(stepName, scriptModel);
            UpdateStep();

            lbStep.SelectedIndex = lbStep.Items.Count - 1;

            Test();
        }

        /// <summary>
        /// 获取第三方插件列表
        /// </summary>
        /// <returns></returns>
        public List<ThirdPartyModelsModel.ThirdPartyModel> GetThirdParty()
        {
            ThirdPartyModelsModel thirdPartyModelsModel = new ThirdPartyModelsModel();
            XmlSerializerBusiness.Load(ref thirdPartyModelsModel, AppDomain.CurrentDomain.BaseDirectory + @"\Operation\DetailedList.xml");
            return thirdPartyModelsModel.ThirdPartyModels;
        }
        /// <summary>
        /// 将第三方扩展加载到界面
        /// </summary>
        /// <param name="clickEvent"></param>
        /// <returns></returns>
        public void InitThirdParty(List<ThirdPartyModelsModel.ThirdPartyModel> thirdPartys, RoutedEventHandler clickEvent)
        {
            if (thirdPartys.Count != 0)
            {
                List<String> strs = new List<string>();
                foreach (var item in thirdPartys)
                {
                    strs.Add(item.text);
                }
                GeneralViewBusiness.SetStringsAndClickEventToMenuItemView(miThirdParty, strs, clickEvent, true, 16);
            }
            miThirdParty.Items.Add(new Separator());
            MenuItem menuItem = new MenuItem();
            menuItem.SetResourceReference(MenuItem.HeaderProperty, "Setup");
            menuItem.Click += AddThirdParty;
            miThirdParty.Items.Add(menuItem);
        }

        public void ThirdPartysMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DragDrop.DoDragDrop((TreeViewItem)sender, (TreeViewItem)sender, DragDropEffects.Copy);
           
        }

     
        private void lbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lbMain = sender as ListBox;
            if (lbMain.SelectedIndex == -1)
                return;
            for (int i = 0; i < mw.projectUserControl.suc.thirdPartys.Count; i++)
            {
                if (mw.projectUserControl.suc.thirdPartys[i].name.Equals(thirdPartySetupsModel.ThirdPartySetupModels[lbMain.SelectedIndex].Name))
                {
                    mw.ShowMakerDialog(new ErrorDialog(mw, "SuspectedInstalled"));
                    lbMain.SelectedIndex = -1;
                    return;
                }
            }

            HintDialog hintDialog = new HintDialog("安装编辑插件", thirdPartySetupsModel.ThirdPartySetupModels[lbMain.SelectedIndex].Text,
            delegate (System.Object _o, RoutedEventArgs _e)
            {
                //安装编辑插件
                SetupEditPlugIn(thirdPartySetupsModel, lbMain);
                lbMain.SelectedIndex = -1;
            },
            delegate (System.Object _o, RoutedEventArgs _e)
            {
                mw.RemoveDialog();
                lbMain.SelectedIndex = -1;
            }
           );
            mw.ShowMakerDialog(hintDialog);
        }

        private void SetupEditPlugIn(ThirdPartySetupsModel thirdPartySetupModel,ListBox lbMain)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Operation\View\" + thirdPartySetupsModel.ThirdPartySetupModels[lbMain.SelectedIndex].View + ".xml")
                         || File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Operation\Dll\" + thirdPartySetupsModel.ThirdPartySetupModels[lbMain.SelectedIndex].Dll + ".dll"))
            {
                //弹出提示框 - 是否覆盖
                mw.ShowMakerDialog(new SetupEditPlugInDialog(mw, thirdPartySetupFilePath, thirdPartySetupsModel.ThirdPartySetupModels[lbMain.SelectedIndex]));
                lbMain.SelectedIndex = -1;
            }
            else
            {
                //直接安装
                mw.projectUserControl.suc.SetupEditPlugIn(thirdPartySetupsModel.ThirdPartySetupModels[lbMain.SelectedIndex]);
                lbMain.SelectedIndex = -1;
            }
        }

        public void SetupEditPlugIn(ThirdPartySetupModel thirdPartySetupModel)
        {
            thirdPartys.Add(new ThirdPartyModelsModel.ThirdPartyModel(thirdPartySetupModel.Name, thirdPartySetupModel.Text, thirdPartySetupModel.View, thirdPartySetupModel.Dll));
            InitThirdParty(thirdPartys, ThirdPartysMenuItem_Click);

            ThirdPartyModelsModel thirdPartyModelsModel = new ThirdPartyModelsModel();
            thirdPartyModelsModel.ThirdPartyModels = thirdPartys;
            XmlSerializerBusiness.Save(thirdPartyModelsModel, AppDomain.CurrentDomain.BaseDirectory + @"\Operation\DetailedList.xml");
        }

     
        private void iRefresh_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Test();
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
                String stepName = GetUsableStepName();
                ScriptModel scriptModel = new ScriptModel();
                scriptModel.Name = stepName;
                scriptModel.Value = "";
                scriptModel.Visible = true;
                scriptModel.Intersection = new List<string>();
                scriptModel.Complement = new List<string>();

                if (sender == miRhombusDiffusion)
                {
                    scriptModel.OperationModels.Add(new CreateFromAutomaticOperationModel(new CreateFromAutomaticOperationModel.RhombusDiffusionAutomaticOperationModel(11,1)));
                }
                else if (sender == miCross)
                {
                    scriptModel.OperationModels.Add(new CreateFromAutomaticOperationModel(new CreateFromAutomaticOperationModel.CrossAutomaticOperationModel(11,1)));
                }
                else if (sender == miRandomFountain)
                {
                    scriptModel.OperationModels.Add(new CreateFromAutomaticOperationModel(new CreateFromAutomaticOperationModel.RandomFountainAutomaticOperationModel(0, 7)));
                }
                scriptModelDictionary.Add(stepName, scriptModel);
                UpdateStep();

                lbStep.SelectedIndex = lbStep.Items.Count - 1;

                Test();

        }

        private ThirdPartySetupsModel thirdPartySetupsModel;
        private String thirdPartySetupFilePath;
        private void AddThirdParty(object sender, RoutedEventArgs e)
        {
            String SetupFilePath = AppDomain.CurrentDomain.BaseDirectory;
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (File.Exists(SetupFilePath))
                openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(SetupFilePath);  //注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "XML文件|*.xml";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                thirdPartySetupsModel = new ThirdPartySetupsModel();
                XmlSerializerBusiness.Load(ref thirdPartySetupsModel, openFileDialog.FileName);

                thirdPartySetupFilePath = openFileDialog.FileName;

                List<String> strs = new List<string>();
                for (int i = 0; i < thirdPartySetupsModel.ThirdPartySetupModels.Count; i++)
                {
                    strs.Add(thirdPartySetupsModel.ThirdPartySetupModels[i].Name);
                }
                mw.ShowMakerDialog(new ListDialog(mw, strs, lbMain_SelectionChanged, "点击想要安装的插件完成安装"));
            }
        }


        private void FastGeneration_Click(object sender, RoutedEventArgs e)
        {
            ScriptModel scriptModel = new ScriptModel();
            scriptModel.Name = GetUsableStepName();
            scriptModel.Value = "";
            scriptModel.Visible = true;
            scriptModel.Intersection = new List<string>();
            scriptModel.Complement = new List<string>();

            scriptModel.OperationModels.Add(
                new CreateFromQuickOperationModel(0,
                new List<int> { 11},
                12,
                12,
               new List<int> { 5 },
                 Create.UP,
                  Create.ALL));

            scriptModelDictionary.Add(scriptModel.Name, scriptModel);
            UpdateStep();
          
            //如果选中，就在列表选中最后一个
            if (sender == btnFastGenerationAndSelection)
            {
                lbStep.SelectedIndex = lbStep.Items.Count - 1;
            }

            Test();
        }

        private void SelectEditor_Click(object sender, RoutedEventArgs e)
        {
            if (lbStep.SelectedIndex == -1)
            {
                return;
            }
            String stepName = GetUsableStepName();
            if (sender == btnSelectEditorReplace)
            {
                //修改属性
                if (lockedDictionary.ContainsKey(GetStepName()))
                {
                    new MessageDialog(mw, "TheStepIsLocked").ShowDialog();
                    return;
                }

                ScriptModel scriptModel = scriptModelDictionary[GetStepName()];
                SetAttributeOperationModel setAttributeOperationModel = new SetAttributeOperationModel();
                setAttributeOperationModel.AttributeOperationModels = new List<SetAttributeOperationModel.AttributeOperationModel>() { new SetAttributeOperationModel.AttributeOperationModel(SetAttributeOperationModel.AttributeOperationModel.AttributeType.POSITION, "+0") };
                scriptModel.OperationModels.Add(setAttributeOperationModel);
                //UpdateStep();
                sw.SetData(scriptModelDictionary[scriptModel.Name].OperationModels, true);
                Test();
            }
            else if (sender == btnSelectEditorAdd)
            {
                //复制并修改
                ScriptModel scriptModel = new ScriptModel();
                scriptModel.OperationModels = new List<BaseOperationModel>() { new CreateFromStepOperationModel(GetStepName()) };
                scriptModel.Name = stepName;
                scriptModel.Value = "";
                scriptModel.Visible = true;
                //scriptModel.Contain = new List<string>() { stepName };
                scriptModelDictionary.Add(stepName, scriptModel);
                SetAttributeOperationModel setAttributeOperationModel = new SetAttributeOperationModel();
                setAttributeOperationModel.AttributeOperationModels = new List<SetAttributeOperationModel.AttributeOperationModel>() { new SetAttributeOperationModel.AttributeOperationModel(SetAttributeOperationModel.AttributeOperationModel.AttributeType.POSITION, "+0") };
                scriptModel.OperationModels.Add(setAttributeOperationModel);
              
                UpdateStep();
                lbStep.SelectedIndex = lbStep.Items.Count - 1;
                Test();
            }
        }

        private void ConditionJudgment_Click(object sender, RoutedEventArgs e)
        {
            if (lbStep.SelectedIndex == -1)
            {
                return;
            }
            ScriptModel scriptModel = scriptModelDictionary[GetStepName()];
            if (lockedDictionary.ContainsKey(GetStepName()))
            {
                new MessageDialog(mw, "TheStepIsLocked").ShowDialog();
                return;
            }
            if (sender == btnConditionJudgmentReplace || sender == btnConditionJudgmentReplace)
            {
                scriptModel.OperationModels.Add(new ConditionJudgmentOperationModel(ConditionJudgmentOperationModel.Operation.REPLACE, -1, 0, new List<int>(), new List<int>(), "", "", ""));
            }
            else
            {
                scriptModel.OperationModels.Add(new ConditionJudgmentOperationModel(ConditionJudgmentOperationModel.Operation.REMOVE, -1, 0, new List<int>(), new List<int>(), "", "", ""));
            }
            sw.SetData(scriptModelDictionary[scriptModel.Name].OperationModels, true);
        }

        private void MiMycontent_Click(object sender, RoutedEventArgs e)
        {
            ShowMyContentWindowDialog dialog = new ShowMyContentWindowDialog(this);
            if (dialog.ShowDialog() == true) {
                if (dialog.resultFileName.EndsWith(".lightScript")) {
                    if ((mw.LastProjectPath + @"\LightScript\" + dialog.resultFileName).Equals(filePath))
                    {
                        return;
                    }
                    ImportLibraryDialog _dialog = new ImportLibraryDialog(mw, mw.LastProjectPath + @"\LightScript\" + dialog.resultFileName);
                    mw.ShowMakerDialog(_dialog);
                }
                else 
                {
                    String stepName = GetUsableStepName();
                    ScriptModel scriptModel = new ScriptModel();
                    scriptModel.OperationModels = new List<BaseOperationModel>() { new CreateFromFileOperationModel(dialog.resultFileName,"") };
                    scriptModel.Name = stepName;
                    scriptModel.Value = "";
                    scriptModel.Visible = true;
                    //scriptModel.Contain = new List<string>() { stepName };
                    scriptModel.Intersection = new List<string>();
                    scriptModel.Complement = new List<string>();
                    scriptModelDictionary.Add(stepName, scriptModel);
                    UpdateStep();

                    lbStep.SelectedIndex = lbStep.Items.Count - 1;

                    Test();
                }
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Test();
            spRefresh.Visibility = Visibility.Collapsed;
            spHint.Visibility = Visibility.Collapsed;
        }

        public String AudioResources = String.Empty;
        private void ChooseAudio(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            if (mw.strMyLanguage.Equals("en-US"))
            {
                openFileDialog1.Filter = "Wav file(*.wav)|*.wav|Mp3 file(*.mp3)|*.mp3|All files(*.*)|*.*";
            }
            else
            {
                openFileDialog1.Filter = "Wav文件(*.wav)|*.wav|Mp3文件(*.mp3)|*.mp3|所有文件(*.*)|*.*";
            }
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AudioResources = openFileDialog1.FileName;
                tbMusic.Text = AudioResources;
            }
        }

        private void lbStep_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            svTime.ScrollToVerticalOffset(e.VerticalOffset * 32);
        }

        bool bTimeLine = false;
        private void cTime_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bTimeLine = true;

            TimeLineChange(e.GetPosition(cTimeLine as Canvas));
        }

        public double nowTimeP = 0;
        public int nowTimeI = 0;

        private void cTimeLine_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bTimeLine = false;
        }

        private void cTimeLine_MouseMove(object sender, MouseEventArgs e)
        {
            if (bTimeLine)
            {
                TimeLineChange(e.GetPosition(cTimeLine as Canvas));
            }
        }

        private void TimeLineChange(Point p)
        {
            nowTimeP = p.X / cTimeLine.ActualWidth;

            int now = (int)(LightBusiness.GetMax(GetData()) * nowTimeP);

            for (int i = 0; i < _bridge.liTime.Count; i++)
            {
                if (now == _bridge.liTime[i])
                {
                    nowTimeI = _bridge.liTime[i];
                    break;
                }

                if (now < _bridge.liTime[i])
                {
                    if (i == 0)
                    {
                        nowTimeI = _bridge.liTime[i];
                        break;
                    }
                    else
                    {
                        nowTimeI = now - _bridge.liTime[i - 1] < _bridge.liTime[i] - now ? _bridge.liTime[i - 1] : _bridge.liTime[i];
                        break;
                    }
                }
            }

            _bridge.nowTimePoint = _bridge.liTime.IndexOf(nowTimeI) + 1;
            _bridge.LoadFrame();
            //tbTimePointCountLeft.Text = (_bridge.liTime.IndexOf(nowTimeI) + 1).ToString();

            //cTimeLine.Children.Clear();
            //Line line = new Line()
            //{
            //    X1 = d * cTimeLine.ActualWidth,
            //    X2 = d * cTimeLine.ActualWidth + 5,
            //    Y1 = 0,
            //    Y2 = cTimeLine.ActualHeight,
            //    Stroke = new SolidColorBrush(Colors.White),
            //    //StrokeThickness = 10
            //};
            //cTimeLine.Children.Add(line);
        
        }

        private void cTimeLine_MouseLeave(object sender, MouseEventArgs e)
        {
            bTimeLine = false;
        }

        public void OnDrawTimeLine() {
            nowTimeI = _bridge.liTime[_bridge.nowTimePoint - 1];
            nowTimeP = _bridge.liTime[_bridge.nowTimePoint - 1] * 1.0 / LightBusiness.GetMax(GetData());

            cTimeLine.Children.Clear();
            Line line = new Line()
            {
                X1 = _bridge.liTime[_bridge.nowTimePoint - 1] * 1.0 / LightBusiness.GetMax(GetData()) * cTimeLine.ActualWidth,
                X2 = _bridge.liTime[_bridge.nowTimePoint - 1] * 1.0 / LightBusiness.GetMax(GetData()) * cTimeLine.ActualWidth + 5,
                Y1 = 0,
                Y2 = cTimeLine.ActualHeight,
                Stroke = new SolidColorBrush(Colors.White),
                //StrokeThickness = 10
            };
            cTimeLine.Children.Add(line);
        }
    }
}