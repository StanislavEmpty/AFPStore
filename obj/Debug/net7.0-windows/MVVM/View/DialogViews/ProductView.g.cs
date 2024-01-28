﻿#pragma checksum "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7466A6AF2188655612C9C6616A2FDF311A0D1ED0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using AFPStore.Core;
using AFPStore.MVVM.Model.Main;
using AFPStore.MVVM.View.DialogViews;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
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


namespace AFPStore.MVVM.View.DialogViews {
    
    
    /// <summary>
    /// ProductView
    /// </summary>
    public partial class ProductView : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 35 "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbName;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbQuantity;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbPrice;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnCreateParam;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ParamsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSave;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnCancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AFPStore;component/mvvm/view/dialogviews/productview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TbName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.TbQuantity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TbPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.BtnCreateParam = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml"
            this.BtnCreateParam.Click += new System.Windows.RoutedEventHandler(this.ButtonAddParams_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ParamsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.BtnSave = ((System.Windows.Controls.Button)(target));
            
            #line 133 "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml"
            this.BtnSave.Click += new System.Windows.RoutedEventHandler(this.ButtonSave_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BtnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 144 "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml"
            this.BtnCancel.Click += new System.Windows.RoutedEventHandler(this.ButtonCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 6:
            
            #line 115 "..\..\..\..\..\..\MVVM\View\DialogViews\ProductView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonDelete_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

