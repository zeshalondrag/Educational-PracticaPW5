﻿#pragma checksum "..\..\OrdersСhecksPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6D998C32C859AC0DE6924F5ED939DB926CD3FC4A035082F85F63073010AF0E9A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Dealership;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace Dealership {
    
    
    /// <summary>
    /// OrdersСhecksPage
    /// </summary>
    public partial class OrdersСhecksPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\OrdersСhecksPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ordersChecksDgr;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\OrdersСhecksPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame TablesPage;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\OrdersСhecksPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox clientCbx;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\OrdersСhecksPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox employeeCbx;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\OrdersСhecksPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox autosalonCbx;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\OrdersСhecksPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox brandCbx;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\OrdersСhecksPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button add;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\OrdersСhecksPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button edit;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\OrdersСhecksPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button delete;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\OrdersСhecksPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button check;
        
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
            System.Uri resourceLocater = new System.Uri("/Dealership;component/orders%d0%a1heckspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\OrdersСhecksPage.xaml"
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
            
            #line 11 "..\..\OrdersСhecksPage.xaml"
            ((Dealership.OrdersСhecksPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ordersChecksDgr = ((System.Windows.Controls.DataGrid)(target));
            
            #line 27 "..\..\OrdersСhecksPage.xaml"
            this.ordersChecksDgr.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ordersChecksDgr_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TablesPage = ((System.Windows.Controls.Frame)(target));
            return;
            case 4:
            this.clientCbx = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.employeeCbx = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.autosalonCbx = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.brandCbx = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.add = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\OrdersСhecksPage.xaml"
            this.add.Click += new System.Windows.RoutedEventHandler(this.add_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.edit = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\OrdersСhecksPage.xaml"
            this.edit.Click += new System.Windows.RoutedEventHandler(this.edit_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.delete = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\OrdersСhecksPage.xaml"
            this.delete.Click += new System.Windows.RoutedEventHandler(this.delete_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.check = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\OrdersСhecksPage.xaml"
            this.check.Click += new System.Windows.RoutedEventHandler(this.check_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

