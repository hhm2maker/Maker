﻿#pragma checksum "..\..\..\..\View\Setting\DeviceManagementWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BF0AACE7ECA7CD4CC89D4AEB5D3B67C7273EA932"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Maker.View.Setting;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Maker.View.Setting {
    
    
    /// <summary>
    /// DeviceManagementWindow
    /// </summary>
    public partial class DeviceManagementWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\View\Setting\DeviceManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbMain;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\View\Setting\DeviceManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNewDevice;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\View\Setting\DeviceManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdateDevice;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\View\Setting\DeviceManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOk;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Maker;component/view/setting/devicemanagementwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Setting\DeviceManagementWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 14 "..\..\..\..\View\Setting\DeviceManagementWindow.xaml"
            ((Maker.View.Setting.DeviceManagementWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lbMain = ((System.Windows.Controls.ListBox)(target));
            return;
            case 3:
            this.btnNewDevice = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\..\View\Setting\DeviceManagementWindow.xaml"
            this.btnNewDevice.Click += new System.Windows.RoutedEventHandler(this.NewOrUpdateDevice);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnUpdateDevice = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\View\Setting\DeviceManagementWindow.xaml"
            this.btnUpdateDevice.Click += new System.Windows.RoutedEventHandler(this.NewOrUpdateDevice);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 23 "..\..\..\..\View\Setting\DeviceManagementWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteDevice);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 24 "..\..\..\..\View\Setting\DeviceManagementWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RunDevice);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnOk = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\View\Setting\DeviceManagementWindow.xaml"
            this.btnOk.Click += new System.Windows.RoutedEventHandler(this.btnOk_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

