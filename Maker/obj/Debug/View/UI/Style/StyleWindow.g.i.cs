﻿#pragma checksum "..\..\..\..\..\View\UI\Style\StyleWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "80051CA8AB18078D75F47DB83DFC220AC595FB0F"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Maker.View.UI.UserControlDialog;
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


namespace Maker.View.Style {
    
    
    /// <summary>
    /// StyleWindow
    /// </summary>
    public partial class StyleWindow : Maker.View.UI.UserControlDialog.MakerDialog, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\..\View\UI\Style\StyleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbCatalog;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\View\UI\Style\StyleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel svMain;
        
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
            System.Uri resourceLocater = new System.Uri("/Maker;component/view/ui/style/stylewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UI\Style\StyleWindow.xaml"
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
            this.lbCatalog = ((System.Windows.Controls.ListBox)(target));
            
            #line 12 "..\..\..\..\..\View\UI\Style\StyleWindow.xaml"
            this.lbCatalog.MouseEnter += new System.Windows.Input.MouseEventHandler(this.lbCatalog_MouseEnter);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\..\..\View\UI\Style\StyleWindow.xaml"
            this.lbCatalog.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lbCatalog_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.svMain = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

