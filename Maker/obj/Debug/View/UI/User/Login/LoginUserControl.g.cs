﻿#pragma checksum "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "87711F12AB80D7925E26396F880230C2D3EBB420"
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


namespace Maker.View.User.Login {
    
    
    /// <summary>
    /// LoginUserControl
    /// </summary>
    public partial class LoginUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox m_tbUserName;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox m_pbPassWord;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox m_tbValidCode;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image m_iValidCode;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button m_btnLogin;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button m_btnRegister;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock m_tbHelp;
        
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
            System.Uri resourceLocater = new System.Uri("/Maker;component/view/ui/user/login/loginusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
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
            
            #line 7 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
            ((Maker.View.User.Login.LoginUserControl)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.m_tbUserName = ((System.Windows.Controls.TextBox)(target));
            
            #line 14 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
            this.m_tbUserName.GotFocus += new System.Windows.RoutedEventHandler(this.Input_GotFocus);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
            this.m_tbUserName.LostFocus += new System.Windows.RoutedEventHandler(this.Input_LostFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.m_pbPassWord = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 18 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
            this.m_pbPassWord.GotFocus += new System.Windows.RoutedEventHandler(this.Input_GotFocus);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
            this.m_pbPassWord.LostFocus += new System.Windows.RoutedEventHandler(this.Input_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.m_tbValidCode = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
            this.m_tbValidCode.GotFocus += new System.Windows.RoutedEventHandler(this.Input_GotFocus);
            
            #line default
            #line hidden
            
            #line 24 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
            this.m_tbValidCode.LostFocus += new System.Windows.RoutedEventHandler(this.Input_LostFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.m_iValidCode = ((System.Windows.Controls.Image)(target));
            
            #line 26 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
            this.m_iValidCode.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.m_iValidCode_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.m_btnLogin = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
            this.m_btnLogin.Click += new System.Windows.RoutedEventHandler(this.m_btnLogin_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.m_btnRegister = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\..\..\View\UI\User\Login\LoginUserControl.xaml"
            this.m_btnRegister.Click += new System.Windows.RoutedEventHandler(this.m_btnRegister_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.m_tbHelp = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

