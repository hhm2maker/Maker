﻿#pragma checksum "..\..\..\..\..\View\Dialog\Script\Create_CreateLightGroupDialog.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F31B273A521AC820327D1D4A7560A630028A2083"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Maker.View.Dialog.Script;
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


namespace Maker.View.Dialog.Script {
    
    
    /// <summary>
    /// Create_CreateLightGroupDialog
    /// </summary>
    public partial class Create_CreateLightGroupDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\..\View\Dialog\Script\Create_CreateLightGroupDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbTime;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\View\Dialog\Script\Create_CreateLightGroupDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbRangeName;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\View\Dialog\Script\Create_CreateLightGroupDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbInterval;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\..\View\Dialog\Script\Create_CreateLightGroupDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbDuration;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\View\Dialog\Script\Create_CreateLightGroupDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbColorName;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\View\Dialog\Script\Create_CreateLightGroupDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbType;
        
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
            System.Uri resourceLocater = new System.Uri("/Maker;component/view/dialog/script/create_createlightgroupdialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\Dialog\Script\Create_CreateLightGroupDialog.xaml"
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
            
            #line 8 "..\..\..\..\..\View\Dialog\Script\Create_CreateLightGroupDialog.xaml"
            ((Maker.View.Dialog.Script.Create_CreateLightGroupDialog)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbTime = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.tbRangeName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbInterval = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tbDuration = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tbColorName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.cbType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            
            #line 41 "..\..\..\..\..\View\Dialog\Script\Create_CreateLightGroupDialog.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnOk_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 42 "..\..\..\..\..\View\Dialog\Script\Create_CreateLightGroupDialog.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
