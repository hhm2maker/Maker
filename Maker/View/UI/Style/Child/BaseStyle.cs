using Maker.Business.Model.OperationModel;
using Maker.ViewBusiness;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Maker.View.Style.Child
{
    
    public class BaseStyle : UserControl
    {
        public StyleWindow sw;
        public virtual string Title
        {
            get;
            set;
        } = "";
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
        }

        public void CreateDialogNormal()
        {
            //AddParentPanel();
            //SetRoutine();
            Content = spContacts;
            AddUIToDialog();
        }

        private StackPanel spContacts = new StackPanel();

        private StackPanel GetTitle()
        {
            return ((Content as StackPanel).Children[0] as Border).Child as StackPanel;
            //if (OnlyTitle)
            //{
            //    return (((Content as StackPanel).Children[0] as Border).Child as DockPanel).Children[1] as StackPanel;
            //}
            //else {
            //    return 
            //}
        }

        private void AddParentPanel()
        {
            DockPanel dp;
            if (OnlyTitle)
            {
                StackPanel sp = new StackPanel();
                sp.Margin = new Thickness(20, 0, 20, 0);
                Border borderTop = new Border();
                borderTop.Background = new SolidColorBrush(Color.FromRgb(74, 74, 74));
                borderTop.HorizontalAlignment = HorizontalAlignment.Stretch;
                borderTop.CornerRadius = new CornerRadius(3);
                borderTop.Margin = new Thickness(0, 15, 0, 0);

                dp = new DockPanel();

                TextBlock tbTitle = new TextBlock();
                tbTitle.Foreground = new SolidColorBrush(Colors.White);
                tbTitle.Margin = new Thickness(10);
                tbTitle.SetResourceReference(TextBlock.TextProperty, Title);
                dp.Children.Add(tbTitle);

                borderTop.Child = dp;

                sp.Children.Add(borderTop);
                AddChild(sp);
            }
            else
            {
                StackPanel sp = new StackPanel();
                sp.Margin = new Thickness(20, 0, 20, 0);
                Border borderTop = new Border();
                borderTop.Background = new SolidColorBrush(Color.FromRgb(74, 74, 74));
                borderTop.HorizontalAlignment = HorizontalAlignment.Stretch;
                borderTop.CornerRadius = new CornerRadius(3, 3, 0, 0);
                borderTop.Margin = new Thickness(0, 15, 0, 0);

                StackPanel spTitle = new StackPanel();
                spTitle.Orientation = Orientation.Vertical;

                dp = new DockPanel();

                spTitle.Children.Add(dp);

                TextBlock tbTitle = new TextBlock();
                tbTitle.Foreground = new SolidColorBrush(Colors.White);
                tbTitle.Margin = new Thickness(10);
                tbTitle.SetResourceReference(TextBlock.TextProperty, Title);
                dp.Children.Add(tbTitle);

                borderTop.Child = spTitle;

                sp.Children.Add(borderTop);

                Border borderBottom = new Border();
                borderBottom.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
                borderBottom.HorizontalAlignment = HorizontalAlignment.Stretch;
                borderBottom.CornerRadius = new CornerRadius(0, 0, 3, 3);

                spContacts.Orientation = Orientation.Vertical;
                spContacts.Margin = new Thickness(10);
                borderBottom.Child = spContacts;

                sp.Children.Add(borderBottom);
                AddChild(sp);
            }
            StackPanel spRight = new StackPanel();

            StackPanel spTopImage = new StackPanel();

            spTopImage.HorizontalAlignment = HorizontalAlignment.Right;
            spTopImage.Orientation = Orientation.Horizontal;
            Image image = new Image
            {
                Width = 20,
                Margin = new Thickness(0, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/arrow_up.png", UriKind.RelativeOrAbsolute))
            };
            image.MouseLeftButtonDown += Image_MouseLeftButtonDown;
            Image image2 = new Image
            {
                Width = 20,
                Margin = new Thickness(0, 0, 15, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/arrow_down.png", UriKind.RelativeOrAbsolute))
            };
            image2.MouseLeftButtonDown += Image2_MouseLeftButtonDown;
            Image image3 = new Image
            {
                Width = 20,
                Margin = new Thickness(0, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/reduce.png", UriKind.RelativeOrAbsolute))
            };
            image3.MouseLeftButtonDown += Image3_MouseLeftButtonDown;
            spTopImage.Children.Add(image);
            spTopImage.Children.Add(image2);
            spTopImage.Children.Add(image3);
            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.HighQuality);
            RenderOptions.SetBitmapScalingMode(image2, BitmapScalingMode.HighQuality);
            RenderOptions.SetBitmapScalingMode(image3, BitmapScalingMode.HighQuality);
            spRight.VerticalAlignment = VerticalAlignment.Center;
            spRight.Children.Add(spTopImage);
            dp.Children.Add(spRight);
        }

        protected void AddTitleImage(List<String> imageUris,List<MouseButtonEventHandler> es)
        {
            if (imageUris.Count != es.Count)
            {
                return;
            }
            StackPanel sp = GetTitle();
            StackPanel spBottomImage = new StackPanel();
            spBottomImage.Margin = new Thickness(0,0,35,10);
            spBottomImage.HorizontalAlignment = HorizontalAlignment.Right;

            spBottomImage.Orientation = Orientation.Horizontal;
            for (int i = imageUris.Count -1 ; i >=0; i--) {
                Image iv = new Image
                {
                    Width = 20,
                    Height = 20,
                    Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/"+ imageUris[i], UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill
                };
                if (i == imageUris.Count - 1)
                {
                    iv.Margin = new Thickness(0, 0, 10, 0);
                }
                else {
                    iv.Margin = new Thickness(0, 0, 10, 0);
                }
                RenderOptions.SetBitmapScalingMode(iv, BitmapScalingMode.Fant);
                spBottomImage.Children.Insert(0, iv);
                iv.MouseLeftButtonDown += es[i];
            }
            sp.Children.Add(spBottomImage);
        }

        protected Image GetImage(String imageUris,int size, MouseButtonEventHandler e)
        {
            Image image = new Image
            {
                Width = size,
                Height = size,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/" + imageUris, UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.Fill
            };
            image.MouseLeftButtonDown += e;
            return image;
             
        }


        protected Image GetImage(String imageUris, int size)
        {
            return new Image
            {
                Width = size,
                Height = size,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/" + imageUris, UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.Fill
            };

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int position = (Parent as StackPanel).Children.IndexOf(this);
            if (position == 0)
            {
                return;
            }
            ListBoxItem box = sw.lbCatalog.Items[position - 1] as ListBoxItem;
            ListBoxItem box2 = sw.lbCatalog.Items[position] as ListBoxItem;
            sw.lbCatalog.Items.RemoveAt(position - 1);
            sw.lbCatalog.Items.RemoveAt(position - 1);
            sw.lbCatalog.Items.Insert(position - 1, box2) ;
            sw.lbCatalog.Items.Insert(position, box);

            BaseStyle bd = sw.svMain.Children[position - 1] as BaseStyle;
            BaseStyle bd2 = sw.svMain.Children[position] as BaseStyle;
            sw.svMain.Children.RemoveAt(position - 1);
            sw.svMain.Children.RemoveAt(position - 1);
            sw.svMain.Children.Insert(position - 1, bd2);
            sw.svMain.Children.Insert(position, bd);

            BaseOperationModel bom = sw.operationModels[position - 1] as BaseOperationModel;
            BaseOperationModel bom2 = sw.operationModels[position ] as BaseOperationModel;
            sw.operationModels.RemoveAt(position - 1);
            sw.operationModels.RemoveAt(position - 1);
            sw.operationModels.Insert(position - 1, bom2);
            sw.operationModels.Insert(position, bom);

            sw.lbCatalog.SelectedIndex = position - 1;
            sw.mw.Test();
        }

        private void Image2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int position = (Parent as StackPanel).Children.IndexOf(this);
            if (position == sw.lbCatalog.Items.Count - 1)
            {
                return;
            }
            ListBoxItem box = sw.lbCatalog.Items[position] as ListBoxItem;
            ListBoxItem box2 = sw.lbCatalog.Items[position+1] as ListBoxItem;
            sw.lbCatalog.Items.RemoveAt(position);
            sw.lbCatalog.Items.RemoveAt(position);
            sw.lbCatalog.Items.Insert(position, box2);
            sw.lbCatalog.Items.Insert(position+1, box);

            BaseStyle bd = sw.svMain.Children[position] as BaseStyle;
            BaseStyle bd2 = sw.svMain.Children[position+1] as BaseStyle;
            sw.svMain.Children.RemoveAt(position);
            sw.svMain.Children.RemoveAt(position);
            sw.svMain.Children.Insert(position, bd2);
            sw.svMain.Children.Insert(position+1, bd);

            BaseOperationModel bom = sw.operationModels[position] as BaseOperationModel;
            BaseOperationModel bom2 = sw.operationModels[position+1] as BaseOperationModel;
            sw.operationModels.RemoveAt(position);
            sw.operationModels.RemoveAt(position);
            sw.operationModels.Insert(position, bom2);
            sw.operationModels.Insert(position+1, bom);

            sw.lbCatalog.SelectedIndex = position + 1;
            sw.mw.Test();
        }

        private void Image3_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int position = (Parent as StackPanel).Children.IndexOf(this);
            sw.lbCatalog.Items.RemoveAt(position);
            sw.operationModels.RemoveAt(position);
            sw.svMain.Children.RemoveAt(position);
            sw.mw.Test();
        }

        /// <summary>
        /// 常规设置
        /// </summary>
        public void SetRoutine() {
            //Background = new SolidColorBrush(Color.FromArgb(255, 38, 39, 41));
        }
        /// <summary>
        /// 设置窗口大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(double width,double height) {
            //spContacts.Width = width;
            spContacts.Height = height;
            //Width = width + 140;
            Height = height + 60;
        }
       
       
        protected List<UIElement> _UI =new List<UIElement>();
        public int UICount {
            get {
                return _UI.Count;
            } 
        }

        /// <summary>
        /// 添加控件到对话框
        /// </summary>
        private void AddUIToDialog() {
            foreach (FrameworkElement ui in _UI) {
                if (_UI.IndexOf(ui) != 0) {
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

        public void AddUIToDialog(FrameworkElement ui,int position)
        {
            _UI.Insert(position,ui);
            if (_UI.IndexOf(ui) != 0)
            {
                ui.Margin = new Thickness(0, 10, 0, 0);
            }
            spContacts.Children.Insert(position,ui);
        }

        /// <summary>
        /// 添加头部提示文本
        /// </summary>
        public void AddTopHintTextBlock(String textName)
        {
            TextBlock tb = new TextBlock();
            tb.FontSize = 16;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255,240,240,240));
         
            tb.SetResourceReference(TextBlock.TextProperty, textName);
            _UI.Add(tb);
        }

        public BaseStyle GetButton(String textName, RoutedEventHandler routedEventHandler,out Button btn)
        {
            btn = GetButton(textName, routedEventHandler);
            return this;
        }

        public Button GetButton(String textName, RoutedEventHandler routedEventHandler)
        {
            Button btn = new Button();
            btn.BorderThickness = new Thickness(2);
            btn.HorizontalAlignment = HorizontalAlignment.Stretch;
            btn.Background = new SolidColorBrush(Color.FromRgb(31, 31, 31));
            btn.BorderBrush = new SolidColorBrush(Color.FromRgb(43, 43, 43)); 
            btn.Margin = new Thickness(5, 0, 0, 0);
            btn.Padding = new Thickness(5, 2, 5, 2);
            btn.FontSize = 16;
            btn.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            btn.SetResourceReference(ContentProperty, textName);
            if (routedEventHandler != null)
            {
                btn.Click += routedEventHandler;
            }
            return btn;
        }

        public StackPanel GetVerticalStackPanel(List<FrameworkElement> frameworkElements)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            foreach (var item in frameworkElements) {
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
                    item.Margin = new Thickness(20,0,0,0);
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
        public ComboBox GetComboBox(List<String> childTextName, SelectionChangedEventHandler selectionChangedEvent)
        {
            ComboBox cb = new ComboBox();
            cb.SelectedIndex = 0;
            cb.FontSize = 16;
            cb.BorderThickness = new Thickness(2);
            cb.Foreground = new SolidColorBrush(Colors.White);
            cb.Background = new SolidColorBrush(Color.FromRgb(43, 43, 43));
            cb.BorderBrush = new SolidColorBrush(Color.FromRgb(31, 31, 31));
            cb.Padding = new Thickness(10,5,10,5);
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
        public ComboBox GetComboBox(List<String> childTextName, int index,SelectionChangedEventHandler selectionChangedEvent)
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
            return GetTexeBlock(textContent,false);
        }

        /// <summary>
        /// 添加标题和值
        /// </summary>
        public Border GetTexeBlock(String textContent,bool isResourceReference)
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
            tbContent.Foreground = new SolidColorBrush(Color.FromRgb(200,200,200));
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

        /// <summary>
        /// 添加标题和值
        /// </summary>
        public TextBox GetTexeBox(String textContent)
        {
            TextBox tbContent = new TextBox();
            tbContent.BorderThickness = new Thickness(2);
            tbContent.HorizontalAlignment = HorizontalAlignment.Stretch;
            tbContent.Background = new SolidColorBrush(Color.FromRgb(43, 43, 43));
            tbContent.BorderBrush = new SolidColorBrush(Color.FromRgb(31, 31, 31));
            tbContent.Margin = new Thickness(5, 0, 0, 0);
            tbContent.Padding = new Thickness(5,2,5,2);
            tbContent.FontSize = 16;
            tbContent.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            tbContent.Text = textContent;

            return tbContent;
        }

        public void AddTitleAndControl(String textTitle, FrameworkElement frameworkElement)
        {
            AddTitleAndControl(textTitle,new List<FrameworkElement>() { frameworkElement });
        }

        public BaseStyle AddTitleAndControl(String textTitle, FrameworkElement frameworkElement,Orientation orientation)
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
            AddTitleAndControl(textTitle, isResourceReference,new List<FrameworkElement>() { frameworkElement });
        }

        public void AddTitleAndControl(String textTitle,bool isResourceReference, List<FrameworkElement> frameworkElements)
        {
            DockPanel dp = new DockPanel();

            dp.Children.Add(GetTitle(textTitle, isResourceReference));
            foreach (var item in frameworkElements)
            {
                item.Margin = new Thickness(0,0,5,0);
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
        public void AddTitleAndComboBox(String titleName,List<String> childTextName, SelectionChangedEventHandler selectionChangedEvent)
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

        private TextBlock GetTitle(String textTitle) {
            return GetTitle(textTitle,true);
        }

        private TextBlock GetTitle(String textTitle, bool isResourceReference)
        {
            TextBlock tbTitle = new TextBlock();
            tbTitle.Margin = new Thickness(0,0,5,0);
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
            TextBox tb = new TextBox();
            tb.FontSize = 16;
            tb.Background = null;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            tb.Margin = new Thickness(0, 10, 0, 0);
            _UI.Add(tb);
        }

        /// <summary>
        /// 添加独立/附属复选框
        /// </summary>
        /// <param name="textName"></param>
        /// <param name="isIndependent"></param>
        public void AddCheckBox(String textName,bool isIndependent)
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
        public UIElement Get(int position) {
            return spContacts.Children[position];
        }

    }
}
