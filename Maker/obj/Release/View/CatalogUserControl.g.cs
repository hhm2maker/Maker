﻿#pragma checksum "..\..\..\View\CatalogUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D1D48A7B38D9E5DFC63D161EF56BB58554D4367A"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Maker.View {
    
    
    /// <summary>
    /// CatalogUserControl
    /// </summary>
    public partial class CatalogUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 87 "..\..\..\View\CatalogUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer svMain;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\View\CatalogUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel spMain;
        
        #line default
        #line hidden
        
        
        #line 264 "..\..\..\View\CatalogUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel spToolTitle;
        
        #line default
        #line hidden
        
        
        #line 305 "..\..\..\View\CatalogUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border bTool;
        
        #line default
        #line hidden
        
        
        #line 306 "..\..\..\View\CatalogUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel spTool;
        
        #line default
        #line hidden
        
        
        #line 313 "..\..\..\View\CatalogUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbProjectPath;
        
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
            System.Uri resourceLocater = new System.Uri("/Maker;component/view/catalogusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\CatalogUserControl.xaml"
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
            this.svMain = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 2:
            this.spMain = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            
            #line 103 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ToFrameWindow);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 121 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ToTextBoxWindow);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 148 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ToPianoRollWindow);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 172 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ToScriptWindow);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 192 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ToFrameWindow);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 217 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ToPageMainWindow);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 232 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ToPlayExportWindow);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 247 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ToFrameWindow);
            
            #line default
            #line hidden
            return;
            case 11:
            this.spToolTitle = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 12:
            
            #line 270 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ToLoadPlayerManagement);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 285 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ToFrameWindow);
            
            #line default
            #line hidden
            return;
            case 14:
            this.bTool = ((System.Windows.Controls.Border)(target));
            return;
            case 15:
            this.spTool = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 16:
            
            #line 310 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ToAboutUserControl);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 311 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ToFeedbackDialog);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 312 "..\..\..\View\CatalogUserControl.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ToHelpOverview);
            
            #line default
            #line hidden
            return;
            case 19:
            this.tbProjectPath = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

