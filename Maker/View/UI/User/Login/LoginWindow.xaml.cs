using Maker.View.Control;
using Maker.View.Online;
using Maker.View.Online.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maker.View.User.Login
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public MainWindow mw;
        public LoginWindow(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            luc = new LoginUserControl(this);
            ruc = new RegisterUserControl(this);
            mainp.Content = luc;
        }

        public LoginUserControl luc;
        public RegisterUserControl ruc;

        public void selTrans()
        {
            if (mainp.Content == luc)
            {
                Transitionals.Transitions.RotateTransition t = new Transitionals.Transitions.RotateTransition();
                mainp.Transition = t;
                mainp.Content = ruc;
            }
            else
            {
                Transitionals.Transitions.RotateTransition t = new Transitionals.Transitions.RotateTransition();
                t.Direction = Transitionals.Transitions.RotateDirection.Right;
                mainp.Transition = t;
                mainp.Content = luc;
            }
        }

        public UserInfo _loginUser;
    }
}
