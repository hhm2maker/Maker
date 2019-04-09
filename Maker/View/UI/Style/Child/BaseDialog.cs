using Maker.ViewBusiness;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maker.View.Style.Child
{
    public class BaseSettingUserControl : UserControl
    {
        public void CreateDialog(double width, double height)
        {
            AddParentPanel();
            SetRoutine();
            AddUIToDialog();
            SetSize(width, height);
        }

        private void AddParentPanel()
        {
            AddChild(new DockPanel());
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
            DockPanel dp = (DockPanel)Content;
            StackPanel sp = (StackPanel)dp.Children[0];
            sp.Width = width;
            sp.Height = height;
            Width = width + 140;
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
            DockPanel dp = (DockPanel)Content;
            StackPanel spMain = new StackPanel();
            spMain.Margin = new Thickness(20, 0, 20, 20);
            foreach (FrameworkElement ui in _UI) {
                spMain.Children.Add(ui);
            }
            dp.Children.Add(spMain);
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
            DockPanel dp = (DockPanel)Content;
            StackPanel sp = (StackPanel)dp.Children[0];
            return sp.Children[position];
        }

    }
}
