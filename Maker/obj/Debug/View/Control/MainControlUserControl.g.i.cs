﻿#pragma checksum "..\..\..\..\View\Control\MainControlUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A7F788B5D328CFAFE1942F1E780C7FBB1690E75A"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Maker.View.Control;
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


namespace Maker.View.Control {
    
    
    /// <summary>
    /// MainControlUserControl
    /// </summary>
    public partial class MainControlUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\View\Control\MainControlUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button tbNumberUserControl;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\View\Control\MainControlUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button tbFrameUserControl;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\View\Control\MainControlUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button tbLiveUserControl;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\View\Control\MainControlUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button tbOpen;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\View\Control\MainControlUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button tbSave;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\View\Control\MainControlUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button tbSaveAs;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\View\Control\MainControlUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel mainDockPanel;
        
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
            System.Uri resourceLocater = new System.Uri("/Maker;component/view/control/maincontrolusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Control\MainControlUserControl.xaml"
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
            
            #line 9 "..\..\..\..\View\Control\MainControlUserControl.xaml"
            ((Maker.View.Control.MainControlUserControl)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbNumberUserControl = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\..\View\Control\MainControlUserControl.xaml"
            this.tbNumberUserControl.Click += new System.Windows.RoutedEventHandler(this.ToUserControl);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbFrameUserControl = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\..\View\Control\MainControlUserControl.xaml"
            this.tbFrameUserControl.Click += new System.Windows.RoutedEventHandler(this.ToUserControl);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tbLiveUserControl = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\..\View\Control\MainControlUserControl.xaml"
            this.tbLiveUserControl.Click += new System.Windows.RoutedEventHandler(this.ToUserControl);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbOpen = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\View\Control\MainControlUserControl.xaml"
            this.tbOpen.Click += new System.Windows.RoutedEventHandler(this.Open);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tbSave = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\..\View\Control\MainControlUserControl.xaml"
            this.tbSave.Click += new System.Windows.RoutedEventHandler(this.tbSave_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tbSaveAs = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\View\Control\MainControlUserControl.xaml"
            this.tbSaveAs.Click += new System.Windows.RoutedEventHandler(this.tbSaveAs_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.mainDockPanel = ((System.Windows.Controls.DockPanel)(target));
            
            #line 28 "..\..\..\..\View\Control\MainControlUserControl.xaml"
            this.mainDockPanel.SizeChanged += new System.Windows.SizeChangedEventHandler(this.mainDockPanel_SizeChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

