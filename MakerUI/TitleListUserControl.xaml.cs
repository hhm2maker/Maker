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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MakerUI.ListUserControl;

namespace MakerUI
{
    /// <summary>
    /// TitleListUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class TitleListUserControl : UserControl
    {
        public TitleListUserControl()
        {
            InitializeComponent();
            bRight = bNew;
        }

        public Border bRight;
        public void InitData(List<String> strs, PositionChange onPositionChange, MouseButtonEventHandler e)
        {
            luc.InitData(strs, onPositionChange);

            bNew.MouseLeftButtonUp += e;
        }

        public void InitData(List<String> strs, PositionChange onPositionChange,int firstPosition, MouseButtonEventHandler e)
        {
            luc.InitData(strs, onPositionChange, firstPosition);

            bNew.MouseLeftButtonUp += e;
        }
    }
}
