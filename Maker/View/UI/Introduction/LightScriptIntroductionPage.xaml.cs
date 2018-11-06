﻿using Maker.View.UI;
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
    /// LightScriptIntroductionPage.xaml 的交互逻辑
    /// </summary>
    public partial class LightScriptIntroductionPage : BaseIntroductionPage
    {
        public LightScriptIntroductionPage(CatalogUserControl cuc, int[] iPosition) : base(cuc, iPosition)
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetButtonList(new List<Button>() { btnLightScript, btnCode });
            SetButtonEvent();
        }
    }
}
