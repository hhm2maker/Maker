﻿#pragma checksum "..\..\..\..\View\Help\HelpOverviewWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7E921745D8EF272EF2516E6934B34434CE5263B6"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Maker.View.Help;
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


namespace Maker.View.Help {
    
    
    /// <summary>
    /// HelpOverviewWindow
    /// </summary>
    public partial class HelpOverviewWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbLeft;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBoxItem lbiNone;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBoxItem lbiInstanceDocument;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBoxItem lbiOldHelpDocument;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBoxItem lbiFlowChart;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBoxItem lbiDeveloperDocumentation;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WebBrowser wbMain;
        
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
            System.Uri resourceLocater = new System.Uri("/Maker;component/view/help/helpoverviewwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
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
            this.lbLeft = ((System.Windows.Controls.ListBox)(target));
            
            #line 18 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
            this.lbLeft.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 21 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
            ((System.Windows.Controls.ListBoxItem)(target)).PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ListBoxItem_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lbiNone = ((System.Windows.Controls.ListBoxItem)(target));
            return;
            case 4:
            this.lbiInstanceDocument = ((System.Windows.Controls.ListBoxItem)(target));
            
            #line 25 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
            this.lbiInstanceDocument.Selected += new System.Windows.RoutedEventHandler(this.ToHelpWindow);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lbiOldHelpDocument = ((System.Windows.Controls.ListBoxItem)(target));
            
            #line 26 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
            this.lbiOldHelpDocument.Selected += new System.Windows.RoutedEventHandler(this.ToHelpWindow);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lbiFlowChart = ((System.Windows.Controls.ListBoxItem)(target));
            
            #line 27 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
            this.lbiFlowChart.Selected += new System.Windows.RoutedEventHandler(this.DefaultOpenFlowChart);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lbiDeveloperDocumentation = ((System.Windows.Controls.ListBoxItem)(target));
            
            #line 28 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
            this.lbiDeveloperDocumentation.Selected += new System.Windows.RoutedEventHandler(this.ToHelpWindow);
            
            #line default
            #line hidden
            return;
            case 8:
            this.wbMain = ((System.Windows.Controls.WebBrowser)(target));
            
            #line 30 "..\..\..\..\View\Help\HelpOverviewWindow.xaml"
            this.wbMain.LoadCompleted += new System.Windows.Navigation.LoadCompletedEventHandler(this.wbMain_LoadCompleted);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
