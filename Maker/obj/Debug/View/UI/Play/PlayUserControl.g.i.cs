﻿#pragma checksum "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DBD85B65164C571FD0623E3005D27FCDC27E08AF"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Maker.View;
using Maker.View.Device;
using Maker.View.Play;
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


namespace Maker.View.UI {
    
    
    /// <summary>
    /// PlayUserControl
    /// </summary>
    public partial class PlayUserControl : Maker.View.BaseUserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gMain;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cbIsDD;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbExport;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnImportRealPlayer;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbPosition;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Maker.View.Play.TeachingControl mTeachingControl;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Maker.View.Device.TeachingLaunchpadPro mLaunchpad;
        
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
            System.Uri resourceLocater = new System.Uri("/Maker;component/view/ui/play/playusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.gMain = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 32 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
            ((System.Windows.Controls.ToolBar)(target)).Loaded += new System.Windows.RoutedEventHandler(this.ToolBar_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 33 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LoadKeyboards);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cbIsDD = ((System.Windows.Controls.CheckBox)(target));
            
            #line 35 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
            this.cbIsDD.Checked += new System.Windows.RoutedEventHandler(this.cbIsDD_Checked);
            
            #line default
            #line hidden
            
            #line 35 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
            this.cbIsDD.Unchecked += new System.Windows.RoutedEventHandler(this.cbIsDD_Unchecked);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 39 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
            ((System.Windows.Controls.ToolBar)(target)).Loaded += new System.Windows.RoutedEventHandler(this.ToolBar_Loaded);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 40 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LoadTutorial);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 44 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
            ((System.Windows.Controls.ToolBar)(target)).Loaded += new System.Windows.RoutedEventHandler(this.ToolBar_Loaded);
            
            #line default
            #line hidden
            return;
            case 8:
            this.tbExport = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.btnImportRealPlayer = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
            this.btnImportRealPlayer.Click += new System.Windows.RoutedEventHandler(this.btnImportRealPlayer_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 50 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
            ((System.Windows.Controls.ToolBar)(target)).Loaded += new System.Windows.RoutedEventHandler(this.ToolBar_Loaded);
            
            #line default
            #line hidden
            return;
            case 11:
            this.tbPosition = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.mTeachingControl = ((Maker.View.Play.TeachingControl)(target));
            return;
            case 13:
            this.mLaunchpad = ((Maker.View.Device.TeachingLaunchpadPro)(target));
            return;
            case 14:
            
            #line 57 "..\..\..\..\..\View\UI\Play\PlayUserControl.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

