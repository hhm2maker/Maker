using System;
using System.Windows;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class HintDialog : MakerDialog
    {
        public HintDialog(String title,String content)
        {
            InitializeComponent();

            tbTitle.Text = title;
            tbContent.Text = content;

            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;
            btnNotHint.Click += BtnNotHint_Click;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            OnOk();
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            OnCancel();
        }

        private void BtnNotHint_Click(object sender, RoutedEventArgs e)
        {
            OnCancel();
        }

        //1.声明关于事件的委托；
        public delegate void OkEventHandler(object sender, EventArgs e);
        public delegate void CancelEventHandler(object sender, EventArgs e);
        public delegate void NotHintEventHandler(object sender, EventArgs e);

        //2.声明事件；   
        public event OkEventHandler Ok;
        public event CancelEventHandler Cancel;
        public event NotHintEventHandler NotHint;

        //3.编写引发事件的函数；
        public void OnOk()
        {
            Ok?.Invoke(this, new EventArgs());   //发出警报
        }

        public void OnCancel()
        {
            Cancel?.Invoke(this, new EventArgs());  
        }

        public void OnNotHint()
        {
            NotHint?.Invoke(this, new EventArgs());
        }
    }
}
