﻿#pragma checksum "..\..\..\EmployeeHomePage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "93EC5301085B262E11A140532918A4CA41D9421C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Presentation;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Presentation {
    
    
    /// <summary>
    /// EmployeeHomePage
    /// </summary>
    public partial class EmployeeHomePage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\EmployeeHomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Avatar;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\EmployeeHomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtFullName;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\EmployeeHomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtPosition;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\EmployeeHomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtDepartment;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\EmployeeHomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstActions;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\EmployeeHomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox NotificationList;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\EmployeeHomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border DetailPanel;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\EmployeeHomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DetailTitle;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\EmployeeHomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DetailContent;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Presentation;component/employeehomepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\EmployeeHomePage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Avatar = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.txtFullName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.txtPosition = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txtDepartment = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.lstActions = ((System.Windows.Controls.ListBox)(target));
            
            #line 29 "..\..\..\EmployeeHomePage.xaml"
            this.lstActions.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ActionList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 67 "..\..\..\EmployeeHomePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DailyAttendance);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 70 "..\..\..\EmployeeHomePage.xaml"
            ((System.Windows.Controls.Grid)(target)).PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.NotificationList = ((System.Windows.Controls.ListBox)(target));
            
            #line 80 "..\..\..\EmployeeHomePage.xaml"
            this.NotificationList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.NotificationList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.DetailPanel = ((System.Windows.Controls.Border)(target));
            return;
            case 10:
            this.DetailTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.DetailContent = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            
            #line 97 "..\..\..\EmployeeHomePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DailyAttendance);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

