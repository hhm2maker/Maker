﻿#pragma checksum "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7E3644E9576C630B06EFCBCAF09643F4B13A9D67"
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
using Maker.View.UI.Device.ColorPanel;
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


namespace Maker.View.Dialog {
    
    
    /// <summary>
    /// LimitlessLampUserControl
    /// </summary>
    public partial class LimitlessLampUserControl : Maker.View.BaseUserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gMain;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition cdMain;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Maker.View.UI.Device.ColorPanel.CompleteColorPanel completeColorPanel;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Maker.View.Device.GrowLaunchpad mLaunchpad;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbPoint;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Maker.View.Device.LaunchpadPro previewLaunchpad;
        
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
            System.Uri resourceLocater = new System.Uri("/Maker;component/view/ui/limitless/limitlesslampusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
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
            this.cdMain = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 3:
            this.completeColorPanel = ((Maker.View.UI.Device.ColorPanel.CompleteColorPanel)(target));
            return;
            case 4:
            this.mLaunchpad = ((Maker.View.Device.GrowLaunchpad)(target));
            return;
            case 5:
            
            #line 32 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddColumn);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 33 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveColumn);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 36 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddRow);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 37 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveRow);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 40 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveFile);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lbPoint = ((System.Windows.Controls.ListBox)(target));
            
            #line 44 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
            this.lbPoint.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lbPoint_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 46 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPoint);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 47 "..\..\..\..\..\View\UI\Limitless\LimitlessLampUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemovePoint);
            
            #line default
            #line hidden
            return;
            case 13:
            this.previewLaunchpad = ((Maker.View.Device.LaunchpadPro)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

