﻿#pragma checksum "..\..\ADD_Tax.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "93AC2CB0478A06699085692938F27C37"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace CRM_User_Interface {
    
    
    /// <summary>
    /// ADD_Tax
    /// </summary>
    public partial class ADD_Tax : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 7 "..\..\ADD_Tax.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTaxMain;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\ADD_Tax.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTax_TName;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\ADD_Tax.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTax_TPercent;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\ADD_Tax.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTax_AddTax;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\ADD_Tax.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgrd_Tax;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\ADD_Tax.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn grsrno;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\ADD_Tax.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn grdTaxType;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\ADD_Tax.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn grdPercentage;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\ADD_Tax.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn Action2;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\ADD_Tax.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn Action;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM_User_Interface;component/add_tax.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ADD_Tax.xaml"
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
            this.btnTaxMain = ((System.Windows.Controls.Button)(target));
            
            #line 7 "..\..\ADD_Tax.xaml"
            this.btnTaxMain.Click += new System.Windows.RoutedEventHandler(this.btnTaxMain_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtTax_TName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtTax_TPercent = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.btnTax_AddTax = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\ADD_Tax.xaml"
            this.btnTax_AddTax.Click += new System.Windows.RoutedEventHandler(this.btnTax_AddTax_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dgrd_Tax = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.grsrno = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 7:
            this.grdTaxType = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 8:
            this.grdPercentage = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 9:
            this.Action2 = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 11:
            this.Action = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 10:
            
            #line 31 "..\..\ADD_Tax.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btntaxoptions_Click);
            
            #line default
            #line hidden
            break;
            case 12:
            
            #line 38 "..\..\ADD_Tax.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

