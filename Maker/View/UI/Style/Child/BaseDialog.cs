using Maker.ViewBusiness;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Maker.View.Style.Child
{
    
    public class BaseDialog : UserControl
    {
        public StyleWindow sw;
        protected virtual string Title
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
        private StackPanel spContacts;
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

                dp = new DockPanel();

                TextBlock tbTitle = new TextBlock();
                tbTitle.Foreground = new SolidColorBrush(Colors.White);
                tbTitle.Margin = new Thickness(10);
                tbTitle.SetResourceReference(TextBlock.TextProperty, Title);
                dp.Children.Add(tbTitle);

                borderTop.Child = dp;

                sp.Children.Add(borderTop);

                Border borderBottom = new Border();
                borderBottom.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
                borderBottom.HorizontalAlignment = HorizontalAlignment.Stretch;
                borderBottom.CornerRadius = new CornerRadius(0, 0, 3, 3);

                spContacts = new StackPanel();
                spContacts.Orientation = Orientation.Vertical;
                spContacts.Margin = new Thickness(10);
                borderBottom.Child = spContacts;

                sp.Children.Add(borderBottom);
                AddChild(sp);
            }
            StackPanel spRight = new StackPanel();
            spRight.HorizontalAlignment = HorizontalAlignment.Right;
            spRight.Orientation = Orientation.Horizontal;
            Image image = new Image
            {
                Width = 20,
                Margin = new Thickness(0, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/arrow_up.png", UriKind.RelativeOrAbsolute))
            };
            Image image2 = new Image
            {
                Width = 20,
                Margin = new Thickness(0, 0, 15, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/arrow_down.png", UriKind.RelativeOrAbsolute))
            };
            Image image3 = new Image
            {
                Width = 20,
                Margin = new Thickness(0, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/reduce.png", UriKind.RelativeOrAbsolute))
            };
            image3.MouseLeftButtonDown += Image3_MouseLeftButtonDown;
            spRight.Children.Add(image);
            spRight.Children.Add(image2);
            spRight.Children.Add(image3);
            dp.Children.Add(spRight);
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
                spContacts.Children.Add(ui);
            }
        }
        /// <summary>
        /// 添加头部提示文本
        /// </summary>
        public void AddTopHintTextBlock(String textName)
        {
            TextBlock tb = new TextBlock();
            tb.FontSize = 16;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255,240,240,240));
            tb.Margin = new Thickness(0, 20, 0, 0);
            tb.SetResourceReference(TextBlock.TextProperty, textName);
            _UI.Add(tb);
        }
        /// <summary>
        /// 添加头部提示文本
        /// </summary>
        public void AddTopHintTextBlockForThirdPartyModel(String textName)
        {
            TextBlock tb = new TextBlock();
            tb.FontSize = 16;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            tb.Margin = new Thickness(0, 20, 0, 0);
            tb.Text = textName;
            _UI.Add(tb);
        }
        /// <summary>
        /// 添加红色提示文本
        /// </summary>
        public void AddRedHintTextBlock(String textName)
        {
            TextBlock tb = new TextBlock();
            tb.FontSize = 14;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 90, 90));
            tb.Margin = new Thickness(0, 20, 0, 0);
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
        public void AddUIElement(UIElement uie)
        {
            _UI.Add(uie);
        }
        /// <summary>
        /// 添加组合框
        /// </summary>
        /// <param name="textName"></param>
        /// <param name="isIndependent"></param>
        public void AddComboBox(List<String> childTextName, SelectionChangedEventHandler selectionChangedEvent)
        {
            ComboBox cb = new ComboBox();
            cb.SelectedIndex = 0;
            cb.FontSize = 16;
            cb.Background = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            cb.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            cb.Margin = new Thickness(0, 10, 0, 0);
            if (selectionChangedEvent != null) {
                cb.SelectionChanged += selectionChangedEvent;
            }
            foreach (String child in childTextName) {
                ComboBoxItem item = new ComboBoxItem();
                item.SetResourceReference(ContentProperty, child);
                cb.Items.Add(item);
            }
            _UI.Add(cb);
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
