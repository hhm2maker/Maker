using Maker.View.Setting;
using Maker.ViewBusiness;
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

namespace Maker.View.Introduction
{
    /// <summary>
    /// LightIntroductionPage.xaml 的交互逻辑
    /// </summary>
    public partial class ToolIntroductionPage : BaseIntroductionPage
    {
        public ToolIntroductionPage(CatalogUserControl cuc,int[] iPosition) :base(cuc, iPosition)
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetButtonList(new List<Button>() { btnPlayer });
            SetButtonEvent();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(sender == tbPlayerTypeSetting)
            {
                 cuc.AddSetting(new PlayerTypeSetting(cuc.mw));
            }
            else if (sender == tbPlayerDefaultSetting)
            {
                cuc.AddSetting(new PlayerDefaultSetting(cuc.mw));
            }
        }
    }
}
