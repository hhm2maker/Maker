using Maker.Business.Model.OperationModel;
using Maker.Model;
using Maker.View.UI.Style.Child;
using Maker.View.UI.Style.Child.Base;
using Maker.View.UI.UserControlDialog;
using Maker.View.UIBusiness;
using Maker.ViewBusiness;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Maker.View.UI.Style.Child.Base.RunPageFileClass;

namespace Maker.View.Style.Child
{
    public class BaseStyle : UserControl
    {
        protected List<Run> runs = new List<Run>();
        protected TextBlock tbMain = null;
        protected TextBlock tbRight = null;
        protected TextBox tbEdit = null;

        protected SolidColorBrush solidNormal = (SolidColorBrush)StaticConstant.mw.Resources["Text_Normal"];
        protected SolidColorBrush solidOrange = (SolidColorBrush)StaticConstant.mw.Resources["Text_Orange"];
        protected SolidColorBrush solidGrey = (SolidColorBrush)StaticConstant.mw.Resources["Text_Grey"];
        protected SolidColorBrush solidGreyBg = (SolidColorBrush)StaticConstant.mw.Resources["Text_Grey_Bg"];
        protected SolidColorBrush solidPurple = (SolidColorBrush)StaticConstant.mw.Resources["Text_Purple"];
        protected SolidColorBrush solidBlue = (SolidColorBrush)StaticConstant.mw.Resources["Text_Blue"];

        public BaseStyleUserControl sw;

        protected List<RunModel> runModels;
        public List<Run> GetRuns(String title, String funType, TextBlock tbMain, List<RunModel> runModels)
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

                if (item.Type == RunModel.RunType.Position)
                {
                    RunPositionClass comboRunClass = new RunPositionClass
                    {
                        Data = (List<String>)item.Data,
                        Os = this as OperationStyle,
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
                        Os = this as OperationStyle,
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
                else if (item.Type == RunModel.RunType.Calc)
                {
                    value.Foreground = solidBlue;
                    value.MouseLeftButtonUp += Value_MouseLeftButtonUp;
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
                else if (item.Type == RunModel.RunType.File)
                {
                    RunFileClass comboRunClass = new RunFileClass
                    {
                        Data = (List<String>)item.Data,
                        Os = this as OperationStyle,
                        Runs = runs,
                        RunCombo = value,
                        TbMain = tbMain,
                    };
                    value.MouseLeftButtonUp += comboRunClass.DrawRange;
                }
                else if (item.Type == RunModel.RunType.PageFile)
                {
                    RunPageFileClass comboRunClass = new RunPageFileClass
                    {
                        IsMultiple = (bool)item.Data,
                        Os = this,
                        Runs = runs,
                        RunCombo = value,
                        TbMain = tbMain,
                    };
                    value.MouseLeftButtonUp += comboRunClass.DrawRange;
                }
                else if (item.Type == RunModel.RunType.Show)
                {
                    value.Foreground = solidGrey;
                }
               
                if (item != runModels[runModels.Count - 1])
                {
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

        public Run GetSplitRun()
        {
            return new Run()
            {
                Foreground = solidOrange,
                Text = ", ",
            };
        }

        protected virtual void RefreshView()
        {

        }

        public virtual void ToRefresh() {
            RefreshView();
        }
        protected virtual List<RunModel> UpdateData()
        {
            return null;
        }

        protected Run selectRun;
        private void Value_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int position = runs.IndexOf((Run)sender);

            if (position == -1)
            {
                return;
            }

            tbMain.Inlines.Clear();
            for (int i = 0; i < position; i++)
            {
                tbMain.Inlines.Add(runs[i]);
            }

            selectRun = runs[position];
            tbEdit.Visibility = Visibility.Visible;
            tbEdit.Text = selectRun.Text;
            tbEdit.Focus();
            for (int i = position + 1; i < runs.Count; i++)
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

        public void ToUpdateData()
        {
            List<RunModel> runModel = UpdateData();
            if (runModel == null)
            {
                return;
            }
            ResetMainText(runModel);
        }

        private void ResetMainText(List<RunModel> runModel)
        {
            runs = GetRuns(Title, FunType.ToString(), tbMain, runModel);

            tbMain.Inlines.Clear();
            foreach (var item in runs)
            {
                tbMain.Inlines.Add(item);
            }
        }

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
                Calc,
                File,
                PageFile,
                Show,
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

            public RunModel(string title, string content, RunType type, Object obj)
            {
                Title = title;
                Content = content;
                Type = type;
                Data = obj;
            }
        }
        public virtual string Title
        {
            get;
            set;
        } = "";

        public virtual StyleType FunType
        {
            get;
            set;
        } = StyleType.Edit;

        public enum StyleType
        {
            Create,
            Edit,
            Animation,
            Generate,
        }

        protected virtual bool OnlyTitle
        {
            get;
            set;
        } = false;

        public void CreateDialog()
        {
            AddParentPanel();
            SetRoutine();
            AddUIToDialog();

            //TODO：移动位置
            //MouseLeftButtonDown += BaseStyle_MouseMove;
        }

        private void BaseStyle_MouseMove(object sender, MouseEventArgs e)
        {
            DataObject dataObject = new DataObject();
            dataObject.SetData("this", this);
            DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy);
        }

        //protected override void OnDragOver(DragEventArgs e)
        //{
        //    base.OnDragOver(e);

        //    //设置Effects 为不接受数据
        //    e.Effects = DragDropEffects.None;

        //    if (e.Data.GetDataPresent(DataFormats.StringFormat))
        //    {
        //        string dataString = (string)e.Data.GetData(DataFormats.StringFormat);
        //        BrushConverter converter = new BrushConverter();

        //        //判断拖动值是否有效
        //        if (converter.IsValid(dataString))
        //        {
        //            //判断Ctrl键是否按下
        //            e.Effects = DragDropEffects.Move;
        //        }
        //    }
        //    e.Handled = true;
        //}


        public void CreateDialogNormal()
        {
            //AddParentPanel();
            //SetRoutine();
            Content = spContacts;
            AddUIToDialog();
        }

        private StackPanel spContacts = new StackPanel();

        //private StackPanel GetTitle()
        //{
        //    return ((Content as StackPanel).Children[0] as Border).Child as StackPanel;
        //    //if (OnlyTitle)
        //    //{
        //    //    return (((Content as StackPanel).Children[0] as Border).Child as DockPanel).Children[1] as StackPanel;
        //    //}
        //    //else {
        //    //    return 
        //    //}
        //}

        private void AddParentPanel()
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Margin = new Thickness(20, 0, 20, 0);

            StackPanel spTopImage = new StackPanel();

            spTopImage.VerticalAlignment = VerticalAlignment.Center;
            spTopImage.Orientation = Orientation.Horizontal;
            Image image = new Image
            {
                Width = 18,
                Margin = new Thickness(0, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/move.png", UriKind.RelativeOrAbsolute))
            };
            image.MouseLeftButtonDown += BaseStyle_MouseMove;
            Image image3 = new Image
            {
                Width = 18,
                Margin = new Thickness(0, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/more_two_white.png", UriKind.RelativeOrAbsolute))
            };
            image3.MouseLeftButtonUp += Image3_MouseLeftButtonDown;

            ContextMenu contextMenu = new ContextMenu();
            image3.ContextMenu = contextMenu;
            MenuItem menuItem = new MenuItem();
            menuItem.SetResourceReference(HeaderedItemsControl.HeaderProperty, "Delete");
            menuItem.Click += MenuItem_Click;
            contextMenu.Items.Add(menuItem);

            spTopImage.Children.Add(image);
            spTopImage.Children.Add(image3);
            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.HighQuality);
            RenderOptions.SetBitmapScalingMode(image3, BitmapScalingMode.HighQuality);
            sp.Children.Add(spTopImage);

            StackPanel spTitle = new StackPanel();
            spTitle.Orientation = Orientation.Vertical;

            sp.Children.Add(spTitle);

            Border borderBottom = new Border();
            borderBottom.HorizontalAlignment = HorizontalAlignment.Stretch;
            borderBottom.VerticalAlignment = VerticalAlignment.Center;
            borderBottom.CornerRadius = new CornerRadius(0, 0, 5, 5);

            spContacts.Orientation = Orientation.Vertical;
            spContacts.Margin = new Thickness(10);
            borderBottom.Child = spContacts;

            sp.Children.Add(borderBottom);
            AddChild(sp);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            int position = (Parent as StackPanel).Children.IndexOf(this);
            sw.operationModels.RemoveAt(position);
            sw.spMain.Children.RemoveAt(position);
            sw.OnRefresh();
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
                    if (strSplit[i].Contains(range+""))
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


        //protected void AddTitleImage(List<String> imageUris, List<MouseButtonEventHandler> es)
        //{
        //    if (imageUris.Count != es.Count)
        //    {
        //        return;
        //    }
        //    StackPanel sp = GetTitle();
        //    StackPanel spBottomImage = new StackPanel();
        //    spBottomImage.Margin = new Thickness(0, 0, 35, 10);
        //    spBottomImage.HorizontalAlignment = HorizontalAlignment.Right;

        //    spBottomImage.Orientation = Orientation.Horizontal;
        //    for (int i = imageUris.Count - 1; i >= 0; i--)
        //    {
        //        Image iv = new Image
        //        {
        //            Width = 20,
        //            Height = 20,
        //            Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/" + imageUris[i], UriKind.RelativeOrAbsolute)),
        //            Stretch = Stretch.Fill
        //        };
        //        if (i == imageUris.Count - 1)
        //        {
        //            iv.Margin = new Thickness(0, 0, 10, 0);
        //        }
        //        else
        //        {
        //            iv.Margin = new Thickness(0, 0, 10, 0);
        //        }
        //        RenderOptions.SetBitmapScalingMode(iv, BitmapScalingMode.Fant);
        //        spBottomImage.Children.Insert(0, iv);
        //        iv.MouseLeftButtonDown += es[i];
        //    }
        //    sp.Children.Add(spBottomImage);
        //}

        //protected Image GetImage(String imageUris,int size, MouseButtonEventHandler e)
        //{
        //    Image image = new Image
        //    {
        //        Width = size,
        //        Height = size,
        //        Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/" + imageUris, UriKind.RelativeOrAbsolute)),
        //        Stretch = Stretch.Fill
        //    };
        //    image.MouseLeftButtonDown += e;
        //    return image;

        //}


        //protected Image GetImage(String imageUris, int size)
        //{
        //    return new Image
        //    {
        //        Width = size,
        //        Height = size,
        //        Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/" + imageUris, UriKind.RelativeOrAbsolute)),
        //        Stretch = Stretch.Fill
        //    };
        //}

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int position = (Parent as StackPanel).Children.IndexOf(this);
            if (position == 0)
            {
                return;
            }


            BaseStyle bd = sw.spMain.Children[position - 1] as BaseStyle;
            BaseStyle bd2 = sw.spMain.Children[position] as BaseStyle;
            sw.spMain.Children.RemoveAt(position - 1);
            sw.spMain.Children.RemoveAt(position - 1);
            sw.spMain.Children.Insert(position - 1, bd2);
            sw.spMain.Children.Insert(position, bd);

            BaseOperationModel bom = sw.operationModels[position - 1] as BaseOperationModel;
            BaseOperationModel bom2 = sw.operationModels[position] as BaseOperationModel;
            sw.operationModels.RemoveAt(position - 1);
            sw.operationModels.RemoveAt(position - 1);
            sw.operationModels.Insert(position - 1, bom2);
            sw.operationModels.Insert(position, bom);

            sw.OnRefresh();
        }

        private void Image2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int position = (Parent as StackPanel).Children.IndexOf(this);

            BaseStyle bd = sw.spMain.Children[position] as BaseStyle;
            BaseStyle bd2 = sw.spMain.Children[position + 1] as BaseStyle;
            sw.spMain.Children.RemoveAt(position);
            sw.spMain.Children.RemoveAt(position);
            sw.spMain.Children.Insert(position, bd2);
            sw.spMain.Children.Insert(position + 1, bd);

            BaseOperationModel bom = sw.operationModels[position] as BaseOperationModel;
            BaseOperationModel bom2 = sw.operationModels[position + 1] as BaseOperationModel;
            sw.operationModels.RemoveAt(position);
            sw.operationModels.RemoveAt(position);
            sw.operationModels.Insert(position, bom2);
            sw.operationModels.Insert(position + 1, bom);

            sw.OnRefresh();
        }

        private void Image3_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (sender as Image).ContextMenu.IsOpen = true; 

        }

        /// <summary>
        /// 常规设置
        /// </summary>
        public void SetRoutine()
        {
            //Background = new SolidColorBrush(Color.FromArgb(255, 38, 39, 41));
        }
        /// <summary>
        /// 设置窗口大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(double width, double height)
        {
            //spContacts.Width = width;
            spContacts.Height = height;
            //Width = width + 140;
            Height = height + 60;
        }


        protected List<UIElement> _UI = new List<UIElement>();
        public int UICount
        {
            get
            {
                return _UI.Count;
            }
        }

        /// <summary>
        /// 添加控件到对话框
        /// </summary>
        private void AddUIToDialog()
        {
            foreach (FrameworkElement ui in _UI)
            {
                if (_UI.IndexOf(ui) != 0)
                {
                    ui.Margin = new Thickness(0, 10, 0, 0);
                }
                spContacts.Children.Add(ui);
            }
        }

        /// <summary>
        /// 删除控件到对话框
        /// </summary>
        protected void RemoveUIToDialog(int position)
        {
            spContacts.Children.RemoveAt(position);
            _UI.RemoveAt(position);
        }

        public void AddUIToDialog(FrameworkElement ui, int position)
        {
            _UI.Insert(position, ui);
            if (_UI.IndexOf(ui) != 0)
            {
                ui.Margin = new Thickness(0, 10, 0, 0);
            }
            spContacts.Children.Insert(position, ui);
        }

        /// <summary>
        /// 添加头部提示文本
        /// </summary>
        public void AddTopHintTextBlock(String textName)
        {
            _UI.Add(GeneralMainViewBusiness.CreateInstance().GetTopHintTextBlock(textName));
        }

        public BaseStyle GetButton(String textName, RoutedEventHandler routedEventHandler, out Button btn)
        {
            btn = ViewBusiness.GetButton(textName, routedEventHandler);
            return this;
        }

        public StackPanel GetVerticalStackPanel(List<FrameworkElement> frameworkElements)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            foreach (var item in frameworkElements)
            {
                sp.Children.Add(item);
            }
            return sp;
        }

        public StackPanel GetHorizontalStackPanel(List<FrameworkElement> frameworkElements)
        {
            StackPanel sp = new StackPanel();
            sp.HorizontalAlignment = HorizontalAlignment.Center;
            sp.Orientation = Orientation.Horizontal;
            foreach (var item in frameworkElements)
            {
                sp.Children.Add(item);
            }
            return sp;
        }

        public BaseStyle GetDockPanel(out DockPanel dp, params FrameworkElement[] frameworkElements)
        {
            dp = GetDockPanel(frameworkElements);
            return this;
        }

        public BaseStyle AddDockPanel(out DockPanel dp, params FrameworkElement[] frameworkElements)
        {
            dp = GetDockPanel(frameworkElements);
            _UI.Add(dp);
            return this;
        }

        public DockPanel GetDockPanel(params FrameworkElement[] frameworkElements)
        {
            DockPanel dp = new DockPanel();
            foreach (var item in frameworkElements)
            {
                if (dp.Children.Count != 0)
                {
                    item.Margin = new Thickness(0, 0, 0, 0);
                    item.VerticalAlignment = VerticalAlignment.Center;
                }
                dp.Children.Add(item);
            }
            return dp;
        }

        public DockPanel GetDockPanel(List<FrameworkElement> frameworkElements)
        {
            DockPanel dp = new DockPanel();
            foreach (var item in frameworkElements)
            {
                dp.Children.Add(item);
            }
            return dp;
        }

        /// <summary>
        /// 添加组合框
        /// </summary>
        /// <param name="textName"></param>
        /// <param name="isIndependent"></param>
        public static ComboBox GetComboBoxStatic(List<String> childTextName, SelectionChangedEventHandler selectionChangedEvent)
        {
            ComboBox cb = new ComboBox();
            cb.SelectedIndex = 0;
            cb.FontSize = 16;
            cb.BorderThickness = new Thickness(2);
            cb.Foreground = new SolidColorBrush(Colors.White);
            cb.Background = new SolidColorBrush(Color.FromRgb(43, 43, 43));
            cb.BorderBrush = new SolidColorBrush(Color.FromRgb(31, 31, 31));
            cb.Padding = new Thickness(10, 5, 10, 5);
            //cb.Margin = new Thickness(16, 0, 0, 0);
            foreach (String child in childTextName)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.SetResourceReference(StyleProperty, "ComboBoxItemStyle1");
                item.Foreground = new SolidColorBrush(Colors.White);
                item.FontSize = 16;
                item.SetResourceReference(ContentProperty, child);
                cb.Items.Add(item);
            }
            if (selectionChangedEvent != null)
            {
                cb.SelectionChanged += selectionChangedEvent;
            }
            return cb;
        }

        /// <summary>
        /// 添加组合框
        /// </summary>
        /// <param name="textName"></param>
        /// <param name="isIndependent"></param>
        public ComboBox GetComboBox(List<String> childTextName, SelectionChangedEventHandler selectionChangedEvent)
        {
            return GetComboBoxStatic(childTextName, selectionChangedEvent);
        }


        /// <summary>
        /// 添加组合框
        /// </summary>
        /// <param name="textName"></param>
        /// <param name="isIndependent"></param>
        public ComboBox GetComboBox(List<String> childTextName, int index, SelectionChangedEventHandler selectionChangedEvent)
        {
            ComboBox cb = new ComboBox();
            cb.SelectedIndex = 0;
            cb.FontSize = 16;
            cb.BorderThickness = new Thickness(2);
            cb.Foreground = new SolidColorBrush(Colors.White);
            cb.Background = new SolidColorBrush(Color.FromRgb(43, 43, 43));
            cb.BorderBrush = new SolidColorBrush(Color.FromRgb(31, 31, 31));
            cb.Padding = new Thickness(10, 5, 10, 5);
            //cb.Margin = new Thickness(16, 0, 0, 0);
            foreach (String child in childTextName)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.SetResourceReference(StyleProperty, "ComboBoxItemStyle1");
                item.Foreground = new SolidColorBrush(Colors.White);
                item.FontSize = 16;
                item.SetResourceReference(ContentProperty, child);
                cb.Items.Add(item);
            }
            cb.SelectedIndex = index;
            if (selectionChangedEvent != null)
            {
                cb.SelectionChanged += selectionChangedEvent;
            }
            return cb;
        }

        /// <summary>
        /// 添加头部提示文本
        /// </summary>
        public void AddTopHintTextBlockForThirdPartyModel(String textName)
        {
            TextBlock tb = new TextBlock();
            tb.FontSize = 16;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));

            tb.Text = textName;
            _UI.Add(tb);
        }

        /// <summary>
        /// 添加标题和值
        /// </summary>
        public Border GetTexeBlock(String textContent)
        {
            return GetTexeBlock(textContent, false);
        }

        /// <summary>
        /// 添加标题和值
        /// </summary>
        public Border GetTexeBlock(String textContent, bool isResourceReference)
        {
            Border border = new Border();
            border.Margin = new Thickness(5, 0, 0, 0);
            border.BorderThickness = new Thickness(2);
            border.HorizontalAlignment = HorizontalAlignment.Stretch;
            border.Background = new SolidColorBrush(Color.FromRgb(43, 43, 43));
            border.BorderBrush = new SolidColorBrush(Color.FromRgb(31, 31, 31));

            TextBlock tbContent = new TextBlock();
            tbContent.Margin = new Thickness(5, 2, 5, 2);
            tbContent.FontSize = 16;
            tbContent.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            if (isResourceReference)
            {
                tbContent.SetResourceReference(TextBlock.TextProperty, textContent);
            }
            else
            {
                tbContent.Text = textContent;
            }

            border.Child = tbContent;

            return border;
        }

        /// <summary>
        /// 添加标题和值
        /// </summary>
        public TextBlock GetTexeBlockNoBorder(String textContent, bool isResourceReference)
        {
            TextBlock tbContent = new TextBlock();
            tbContent.Margin = new Thickness(5, 2, 5, 2);
            tbContent.FontSize = 16;
            tbContent.Foreground = new SolidColorBrush(Color.FromRgb(200, 200, 200));
            if (isResourceReference)
            {
                tbContent.SetResourceReference(TextBlock.TextProperty, textContent);
            }
            else
            {
                tbContent.Text = textContent;
            }
            return tbContent;
        }


        public TextBox GetTexeBox(String textContent)
        {
            TextBox tbContent = new TextBox();
            tbContent.BorderThickness = new Thickness(2);
            tbContent.HorizontalAlignment = HorizontalAlignment.Stretch;
            tbContent.Background = new SolidColorBrush(Color.FromRgb(43, 43, 43));
            tbContent.BorderBrush = new SolidColorBrush(Color.FromRgb(31, 31, 31));
            tbContent.Margin = new Thickness(5, 0, 0, 0);
            tbContent.Padding = new Thickness(5, 2, 5, 2);
            tbContent.FontSize = 16;
            tbContent.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            tbContent.Text = textContent;

            return tbContent;
        }

        public void AddTitleAndControl(String textTitle, FrameworkElement frameworkElement)
        {
            AddTitleAndControl(textTitle, new List<FrameworkElement>() { frameworkElement });
        }

        public BaseStyle AddTitleAndControl(String textTitle, FrameworkElement frameworkElement, Orientation orientation)
        {
            StackPanel dp = new StackPanel();
            dp.Orientation = orientation;
            dp.Children.Add(GetTitle(textTitle));
            frameworkElement.Margin = new Thickness(0, 5, 0, 0);
            dp.Children.Add(frameworkElement);

            _UI.Add(dp);
            return this;
        }

        public void AddTitleAndControl(String textTitle, bool isResourceReference, FrameworkElement frameworkElement)
        {
            AddTitleAndControl(textTitle, isResourceReference, new List<FrameworkElement>() { frameworkElement });
        }

        public void AddTitleAndControl(String textTitle, bool isResourceReference, List<FrameworkElement> frameworkElements)
        {
            DockPanel dp = new DockPanel();

            dp.Children.Add(GetTitle(textTitle, isResourceReference));
            foreach (var item in frameworkElements)
            {
                item.Margin = new Thickness(0, 0, 5, 0);
                dp.Children.Add(item);
            }

            _UI.Add(dp);
        }

        public void AddTitleAndControl(String textTitle, List<FrameworkElement> frameworkElements)
        {
            AddTitleAndControl(textTitle, true, frameworkElements);
        }

        /// <summary>
        /// 添加组合框
        /// </summary>
        /// <param name="textName"></param>
        /// <param name="isIndependent"></param>
        public void AddTitleAndComboBox(String titleName, List<String> childTextName, SelectionChangedEventHandler selectionChangedEvent)
        {
            ComboBox cb = new ComboBox();
            cb.SelectedIndex = 0;
            cb.FontSize = 16;
            cb.Background = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            cb.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            cb.Margin = new Thickness(0, 10, 0, 0);
            if (selectionChangedEvent != null)
            {
                cb.SelectionChanged += selectionChangedEvent;
            }
            foreach (String child in childTextName)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.SetResourceReference(ContentProperty, child);
                cb.Items.Add(item);
            }
            _UI.Add(cb);
        }

        private TextBlock GetTitle(String textTitle)
        {
            return GetTitle(textTitle, true);
        }

        private TextBlock GetTitle(String textTitle, bool isResourceReference)
        {
            TextBlock tbTitle = new TextBlock();
            tbTitle.Margin = new Thickness(0, 0, 5, 0);
            tbTitle.VerticalAlignment = VerticalAlignment.Center;
            tbTitle.FontSize = 16;
            tbTitle.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            if (isResourceReference)
            {
                tbTitle.SetResourceReference(TextBlock.TextProperty, textTitle);
            }
            else
            {
                tbTitle.Text = textTitle;
            }
            return tbTitle;
        }

        /// <summary>
        /// 添加红色提示文本
        /// </summary>
        public void AddRedHintTextBlock(String textName)
        {
            TextBlock tb = new TextBlock();
            tb.FontSize = 14;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 90, 90));

            tb.SetResourceReference(TextBlock.TextProperty, textName);
            tb.TextWrapping = TextWrapping.Wrap;
            _UI.Add(tb);
        }

        /// <summary>
        /// 添加文本框(输入框)
        /// </summary>
        public void AddTextBox()
        {
            _UI.Add(ViewBusiness.GetTextBox());
        }

        /// <summary>
        /// 添加独立/附属复选框
        /// </summary>
        /// <param name="textName"></param>
        /// <param name="isIndependent"></param>
        public void AddCheckBox(String textName, bool isIndependent)
        {
            _UI.Add(UIViewBusiness.GetCheckBox(textName, isIndependent));
        }
        /// <summary>
        /// 添加自定义控件
        /// </summary>
        /// <param name="uie"></param>
        public BaseStyle AddUIElement(UIElement uie)
        {
            _UI.Add(uie);
            return this;
        }

        /// <summary>
        /// 得到指定位置的控件
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public UIElement Get(int position)
        {
            return spContacts.Children[position];
        }


        protected GeneralMainViewBusiness ViewBusiness
        {
            get { return GeneralMainViewBusiness.CreateInstance(); }
        }
    }
}
