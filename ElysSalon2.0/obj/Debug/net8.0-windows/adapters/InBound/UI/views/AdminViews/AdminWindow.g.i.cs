﻿#pragma checksum "..\..\..\..\..\..\..\..\adapters\InBound\UI\views\AdminViews\AdminWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "078D6933D80F32D362E6A9B6CABB8A52C04E01DD"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews {
    
    
    /// <summary>
    /// AdminWindow
    /// </summary>
    public partial class AdminWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\..\..\..\..\adapters\InBound\UI\views\AdminViews\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button itemsBtn;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\..\..\..\..\adapters\InBound\UI\views\AdminViews\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button generalBtn;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\..\..\..\..\adapters\InBound\UI\views\AdminViews\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button salesBtn;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\..\..\..\..\adapters\InBound\UI\views\AdminViews\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button mailBtn;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\..\..\..\..\adapters\InBound\UI\views\AdminViews\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exitBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ElysSalon2.0;component/adapters/inbound/ui/views/adminviews/adminwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\..\..\adapters\InBound\UI\views\AdminViews\AdminWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.itemsBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.generalBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.salesBtn = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\..\..\..\..\..\adapters\InBound\UI\views\AdminViews\AdminWindow.xaml"
            this.salesBtn.Click += new System.Windows.RoutedEventHandler(this.salesBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.mailBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.exitBtn = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\..\..\..\..\..\adapters\InBound\UI\views\AdminViews\AdminWindow.xaml"
            this.exitBtn.Click += new System.Windows.RoutedEventHandler(this.exitBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
