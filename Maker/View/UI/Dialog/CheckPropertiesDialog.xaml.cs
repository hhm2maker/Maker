using Maker.Model;
using Maker.View.Control;
using Maker.View.UI.UserControlDialog;
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

namespace Maker.View.Dialog
{
    /// <summary>
    /// CheckPropertiesDialog.xaml 的交互逻辑
    /// </summary>
    public partial class CheckPropertiesDialog : MakerDialog
    {
        private NewMainWindow mw;
        private List<Light> mLightList;
        public CheckPropertiesDialog(NewMainWindow mw, List<Light> mLightList)
        {
            InitializeComponent();
            this.mw = mw;
            this.mLightList = mLightList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReadOtherAttribute();
        }
        List<int> mColor = new List<int>();
        private void ReadOtherAttribute()
        {
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
            Boolean b_Top = false;
            Boolean b_Right = false;
            Boolean b_Left = false;
            Boolean b_Bottom = false;
            for (int j = 0; j < mLightList.Count; j++)
            {
                int position = mLightList[j].Position;
                if (position >= 28 && position <= 35)
                {
                    b_Top = true;
                }
                if (position >= 100 && position <= 107)
                {
                    b_Right = true;
                }
                if (position >= 108 && position <= 115)
                {
                    b_Left = true;
                }
                if (position >= 116 && position <= 123)
                {
                    b_Bottom = true;
                }
            }
            tbColorCount.Text = mColor.Count.ToString();
            tbLightCount.Text = mLightList.Count.ToString();
            if (b_Top)
            {
                tbTopLight.Text = "是";
            }
            else
            {
                tbTopLight.Text = "否";
            }
            StringBuilder sb = new StringBuilder();
            if (b_Right)
            {
                if (sb.ToString().Equals(""))
                {
                    sb.Append("右");
                }
                else
                {
                    sb.Append(" 右");
                }
            }
            if (b_Left)
            {
                if (sb.ToString().Equals(""))
                {
                    sb.Append("左");
                }
                else
                {
                    sb.Append(" 左");
                }
            }
            if (b_Bottom)
            {
                if (sb.ToString().Equals(""))
                {
                    sb.Append("下");
                }
                else
                {
                    sb.Append(" 下");
                }
            }
            if (sb.ToString().Equals(""))
            {
                sb.Append("否");
            }
            tbOtherLight.Text = sb.ToString();
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mw.RemoveDialog();
        }
    }
}
