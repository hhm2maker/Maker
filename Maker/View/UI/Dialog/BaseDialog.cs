using Maker.ViewBusiness;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maker.View.Dialog
{
    public class BaseDialog : Window
    {
        public void CreateDialog(double width, double height ,RoutedEventHandler clickEvent)
        {
            AddParentPanel();
            SetRoutine();
            AddUIToDialog();
            AddOkAndCancelButton(clickEvent);
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
            ShowInTaskbar = false;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ResizeMode = ResizeMode.NoResize;
            Background = new SolidColorBrush(Color.FromArgb(255, 83, 83, 83));
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
        /// <summary>
        /// 添加确认和取消按钮
        /// </summary>
        /// <param name="clickEvent">确认按钮事件</param>
        public void AddOkAndCancelButton(RoutedEventHandler clickEvent) {
            DockPanel dp = (DockPanel)Content;
            StackPanel spResult = new StackPanel();
            spResult.Margin = new Thickness(0, 20, 20, 0);
            spResult.HorizontalAlignment = HorizontalAlignment.Right;
            //确认按钮
            Button btnOk = new Button();
            btnOk.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            btnOk.Foreground = new SolidColorBrush(Color.FromArgb(255,255,255,255));
            btnOk.Background = null;
            btnOk.FontSize = 14;
            btnOk.Width = 60;
            btnOk.SetResourceReference(ContentProperty, "Ok");
            //btnOk.Margin = new Thickness(0, 0, 0, 0);
            if(clickEvent == null)
            {
                btnOk.Click += BtnOk_Click;
            }
            else
            {
                btnOk.Click += clickEvent;
            }
            spResult.Children.Add(btnOk);
            //取消按钮
            Button btnCancel = new Button();
            btnCancel.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            btnCancel.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            btnCancel.Background = null;
            btnCancel.FontSize = 14;
            btnCancel.Width = 60;
            btnCancel.SetResourceReference(ContentProperty, "Cancel");
            btnCancel.Margin = new Thickness(0, 10, 0, 0);
            btnCancel.Click += BtnCancel_Click;
            spResult.Children.Add(btnCancel);
            dp.Children.Add(spResult);
        }
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private List<UIElement> _UI =new List<UIElement>();
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
            tb.FontSize = 14;
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255,240,240,240));
            tb.Margin = new Thickness(0, 20, 0, 0);
            tb.SetResourceReference(TextBlock.TextProperty, textName);
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
            tb.FontSize = 14;
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
            cb.FontSize = 14;
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
