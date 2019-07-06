﻿using Maker.View.UI.Base;
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

namespace Maker.View.UI.Edit
{
    /// <summary>
    /// EditUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class EditUserControl : BaseChildUserControl
    {
        public NewMainWindow mw;
        public EditUserControl(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;
            Title = "Edit";
        }
    }
}
