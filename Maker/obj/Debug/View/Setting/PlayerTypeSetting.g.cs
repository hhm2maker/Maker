﻿#pragma checksum "..\..\..\..\View\Setting\PlayerTypeSetting.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "93D420D77C252BE7A9B6963AE372BEBEDE976F81"
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
    /// PlayerTypeSetting
    /// </summary>
    public partial class PlayerTypeSetting : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\View\Setting\PlayerTypeSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Maker.View.Setting.PlayerTypeSetting ucMain;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\View\Setting\PlayerTypeSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbParagraphLightList;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\View\Setting\PlayerTypeSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbAccurate;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\View\Setting\PlayerTypeSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbParagraphIntList;
        
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
            System.Uri resourceLocater = new System.Uri("/Maker;component/view/setting/playertypesetting.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Setting\PlayerTypeSetting.xaml"
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
            this.ucMain = ((Maker.View.Setting.PlayerTypeSetting)(target));
            return;
            case 2:
            this.rbParagraphLightList = ((System.Windows.Controls.RadioButton)(target));
            
            #line 41 "..\..\..\..\View\Setting\PlayerTypeSetting.xaml"
            this.rbParagraphLightList.Checked += new System.Windows.RoutedEventHandler(this.RadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.rbAccurate = ((System.Windows.Controls.RadioButton)(target));
            
            #line 42 "..\..\..\..\View\Setting\PlayerTypeSetting.xaml"
            this.rbAccurate.Checked += new System.Windows.RoutedEventHandler(this.RadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.rbParagraphIntList = ((System.Windows.Controls.RadioButton)(target));
            
            #line 43 "..\..\..\..\View\Setting\PlayerTypeSetting.xaml"
            this.rbParagraphIntList.Checked += new System.Windows.RoutedEventHandler(this.RadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 46 "..\..\..\..\View\Setting\PlayerTypeSetting.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

