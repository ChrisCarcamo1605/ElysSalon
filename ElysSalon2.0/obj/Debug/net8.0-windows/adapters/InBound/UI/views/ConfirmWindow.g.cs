﻿#pragma checksum "..\..\..\..\..\..\..\adapters\InBound\UI\views\ConfirmWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1D07648F6AB9E9212CD2AEC2535846F034FCEE88"
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


namespace ElysSalon2._0.adapters.InBound.UI.views {
    
    
    /// <summary>
    /// ConfirmWindow
    /// </summary>
    public partial class ConfirmWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 38 "..\..\..\..\..\..\..\adapters\InBound\UI\views\ConfirmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MensajeConfirmacion;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\..\..\adapters\InBound\UI\views\ConfirmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConfirmBtn;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\..\..\..\adapters\InBound\UI\views\ConfirmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NoBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/ElysSalon2.0;component/adapters/inbound/ui/views/confirmwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\..\adapters\InBound\UI\views\ConfirmWindow.xaml"
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
            this.MensajeConfirmacion = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.ConfirmBtn = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\..\..\..\..\adapters\InBound\UI\views\ConfirmWindow.xaml"
            this.ConfirmBtn.Click += new System.Windows.RoutedEventHandler(this.siBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.NoBtn = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\..\..\..\adapters\InBound\UI\views\ConfirmWindow.xaml"
            this.NoBtn.Click += new System.Windows.RoutedEventHandler(this.noBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
