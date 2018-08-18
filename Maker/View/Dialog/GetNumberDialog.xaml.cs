using Maker.Business;
using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Maker.View.Dialog
{
    /// <summary>
    /// GetOneNumberDialog.xaml 的交互逻辑
    /// </summary>
    public partial class GetNumberDialog : Window
    {
        private Window window;
        private String hint;
        private bool isMultiple;
      
        public int OneNumber {
            get;
            set;
        }
        public List<int> MultipleNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="hint"></param>
        /// <param name="isMultiple">是否是多个数值</param>
        public GetNumberDialog(Window window, String hint,bool isMultiple)
        {
            InitializeComponent();
            this.window = window;
            this.hint = hint;
            this.isMultiple = isMultiple;
            Owner = window;
        }
        private int defaultNumber = -1;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="hint"></param>
        /// <param name="isMultiple">是否是多个数值</param>
        public GetNumberDialog(Window window, String hint, bool isMultiple,int defaultNumber)
        {
            InitializeComponent();
            this.window = window;
            this.hint = hint;
            this.isMultiple = isMultiple;
            this.defaultNumber = defaultNumber;
            Owner = window;
        }
        private List<int> mColor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="hint"></param>
        /// <param name="isMultiple"></param>
        /// <param name="mColor">复位颜色</param>
        public GetNumberDialog(Window window, string hint, bool isMultiple, List<int> mColor) : this(window, hint, isMultiple)
        {
            this.mColor = mColor;
        }
        private List<int> cannotContain;
        bool isOverlaysAllowed;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="hint"></param>
        /// <param name="isMultiple"></param>
        /// <param name="cannotContain">不能被包含</param>
        /// <param name="isOverlaysAllowed">是否允许覆盖</param>
        public GetNumberDialog(Window window, string hint, bool isMultiple, List<int> cannotContain, bool isOverlaysAllowed) {
            InitializeComponent();
            Owner = window;
            this.window = window;
            this.hint = hint;
            this.isMultiple = isMultiple;
            this.cannotContain = cannotContain;
            this.isOverlaysAllowed = isOverlaysAllowed;
        }
      
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (!isMultiple)
                {
                    OneNumber = Convert.ToInt32(tbNumber.Text);
                    if (cannotContain != null && cannotContain.Count > 0)
                    {
                        if (cannotContain.Contains(OneNumber)) {
                            if(isOverlaysAllowed == false)
                            {
                                new MessageDialog(window, "TheFrameHasATimeNode").ShowDialog();
                                tbNumber.Select(0, tbNumber.ToString().Length);
                                tbNumber.Focus();
                                return;
                            }
                            else
                            {
                                OkOrCancelDialog dialog = new OkOrCancelDialog(window, "WhetherOrNotToCoverTheExistingTimeNodesInTheFrame");
                                if (dialog.ShowDialog() == false)
                                {
                                    tbNumber.Select(0, tbNumber.ToString().Length);
                                    tbNumber.Focus();
                                    return;
                                }
                            }
                          
                        }
                    }
                }
                else {
                    MultipleNumber = new List<int>();
                    String[] MultipleStrs = tbNumber.Text.Split(' ');
                    foreach (String number in MultipleStrs) {
                        if (!number.Trim().Equals(String.Empty)) {
                            MultipleNumber.Add(Convert.ToInt32(number));
                        }
                    }
                }
            }
            catch {
                tbNumber.Select(0, tbNumber.ToString().Length);
                tbNumber.Focus();
                return;
            }
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isMultiple)
            {
                SetResourceReference(Window.TitleProperty, "GetNumbers");
            }
            else {
                tbHelp.Visibility = Visibility.Collapsed;
            }
            tbHint.SetResourceReference(TextBlock.TextProperty, hint);
            if (mColor != null && mColor.Count>0) {
                btnReset.Visibility = Visibility.Visible;
                btnReverse.Visibility = Visibility.Visible;
                StringBuilder builder = new StringBuilder();
                foreach (int i in mColor)
                {
                    builder.Append(i + " ");
                }
                tbNumber.Text = builder.ToString().Trim();
            }
            if (defaultNumber !=  -1) {
                tbNumber.Text = defaultNumber.ToString();
            }
            tbNumber.Focus();
        }
        private void Reset(object sender, RoutedEventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            foreach (int i in mColor) {
                builder.Append(i + " ");
            }
            tbNumber.Text = builder.ToString().Trim();
        }
        private void Reverse(object sender, RoutedEventArgs e)
        {
            //反选非mColor反选，而是内容反选
            try
            {
                MultipleNumber = new List<int>();
                String[] MultipleStrs = tbNumber.Text.Split(' ');
                foreach (String number in MultipleStrs)
                {
                    if (!number.Trim().Equals(String.Empty))
                    {
                        MultipleNumber.Add(Convert.ToInt32(number));
                    }
                }
            }
            catch
            {
                tbNumber.Select(0, tbNumber.ToString().Length);
                tbNumber.Focus();
                return;
            }
            List<int> li = new List<int>();
            li.AddRange(MultipleNumber.ToArray());
            li.Reverse();
            StringBuilder builder = new StringBuilder();
            foreach (int i in li)
            {
                builder.Append(i + " ");
            }
            tbNumber.Text = builder.ToString().Trim();
        }

    //     try
    //        {
    //            String[] colors = tbNewColor.Text.Trim().Split(' ');
    //            foreach (String str in colors)
    //            {
    //                if (!Regex.IsMatch(str.ToString(), @"^[1-9]\d*$|^0$"))
    //                {
    //                    tbTextOne.Foreground = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#FF0000"));
    //                    return;
    //                }
    //int colorInt = int.Parse(str);
    //                if (colorInt > 0 && colorInt< 128)
    //                {
    //                    lNewColor.Add(colorInt);
    //                }
    //                else
    //                {
    //                    tbTextOne.Foreground = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#FF0000"));
    //                    return;
    //                }

    //            }
    //            tbTextOne.Foreground = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#263047"));
    //        }
    //        catch
    //        {
    //            tbTextOne.Foreground = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#FF0000"));
    //            return;
    //        }
    }
}
