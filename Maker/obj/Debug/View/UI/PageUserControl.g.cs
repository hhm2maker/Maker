﻿#pragma checksum "..\..\..\..\View\UI\PageUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54E9A95211A868C883F91A3E4EFDB27A19BB20D2"
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
    /// PageUserControl
    /// </summary>
    public partial class PageUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 307 "..\..\..\..\View\UI\PageUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Maker.View.Device.LaunchpadPro mLaunchpad;
        
        #line default
        #line hidden
        
        
        #line 321 "..\..\..\..\View\UI\PageUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbPosition;
        
        #line default
        #line hidden
        
        
        #line 325 "..\..\..\..\View\UI\PageUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbCount;
        
        #line default
        #line hidden
        
        
        #line 333 "..\..\..\..\View\UI\PageUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDownButton;
        
        #line default
        #line hidden
        
        
        #line 334 "..\..\..\..\View\UI\PageUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLoopButton;
        
        #line default
        #line hidden
        
        
        #line 335 "..\..\..\..\View\UI\PageUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpButton;
        
        #line default
        #line hidden
        
        
        #line 343 "..\..\..\..\View\UI\PageUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbLightName;
        
        #line default
        #line hidden
        
        
        #line 350 "..\..\..\..\View\UI\PageUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbGoto;
        
        #line default
        #line hidden
        
        
        #line 357 "..\..\..\..\View\UI\PageUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbBpm;
        
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
            System.Uri resourceLocater = new System.Uri("/Maker;component/view/ui/pageusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\UI\PageUserControl.xaml"
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
            
            #line 9 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((Maker.View.PageUserControl)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mLaunchpad = ((Maker.View.Device.LaunchpadPro)(target));
            return;
            case 3:
            
            #line 309 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SavePage);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 310 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SetAsTheStartingPage);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbPosition = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.tbCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            
            #line 326 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddCount);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 327 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveCount);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 328 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LastCount);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 329 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NextCount);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnDownButton = ((System.Windows.Controls.Button)(target));
            
            #line 333 "..\..\..\..\View\UI\PageUserControl.xaml"
            this.btnDownButton.Click += new System.Windows.RoutedEventHandler(this.TypeButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnLoopButton = ((System.Windows.Controls.Button)(target));
            
            #line 334 "..\..\..\..\View\UI\PageUserControl.xaml"
            this.btnLoopButton.Click += new System.Windows.RoutedEventHandler(this.TypeButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.btnUpButton = ((System.Windows.Controls.Button)(target));
            
            #line 335 "..\..\..\..\View\UI\PageUserControl.xaml"
            this.btnUpButton.Click += new System.Windows.RoutedEventHandler(this.TypeButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.tbLightName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 15:
            
            #line 344 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ReplaceLight);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 345 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditLight);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 346 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MoveLight);
            
            #line default
            #line hidden
            return;
            case 18:
            this.tbGoto = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 19:
            
            #line 351 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ReplacePage);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 352 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.GotoPage);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 353 "..\..\..\..\View\UI\PageUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MovePage);
            
            #line default
            #line hidden
            return;
            case 22:
            this.tbBpm = ((System.Windows.Controls.TextBox)(target));
            
            #line 357 "..\..\..\..\View\UI\PageUserControl.xaml"
            this.tbBpm.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbBpm_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

