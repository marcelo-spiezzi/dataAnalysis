﻿#pragma checksum "..\..\FilterDataWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "82DF4897B581DB3482EA7288922BEA1B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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


namespace DataAnalysis_Tool {
    
    
    /// <summary>
    /// FilterDataWindow
    /// </summary>
    public partial class FilterDataWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\FilterDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BrowseInput;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\FilterDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox filePath;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\FilterDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock UserMessage;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\FilterDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Return;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\FilterDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveOutputButton;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\FilterDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BrowseOutput;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\FilterDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OutputFilePath;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\FilterDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox importedLB;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\FilterDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AnalyzeButton;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\FilterDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox filteredLB;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\FilterDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BrowseInputFilter;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\FilterDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox filePathFilter;
        
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
            System.Uri resourceLocater = new System.Uri("/DataAnalysis_Tool;component/filterdatawindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FilterDataWindow.xaml"
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
            
            #line 9 "..\..\FilterDataWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonImport);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BrowseInput = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\FilterDataWindow.xaml"
            this.BrowseInput.Click += new System.Windows.RoutedEventHandler(this.ButtonBrowse);
            
            #line default
            #line hidden
            return;
            case 3:
            this.filePath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.UserMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.Return = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\FilterDataWindow.xaml"
            this.Return.Click += new System.Windows.RoutedEventHandler(this.ReturnToMainButton);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SaveOutputButton = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\FilterDataWindow.xaml"
            this.SaveOutputButton.Click += new System.Windows.RoutedEventHandler(this.SaveOutputButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BrowseOutput = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\FilterDataWindow.xaml"
            this.BrowseOutput.Click += new System.Windows.RoutedEventHandler(this.ButtonBrowse);
            
            #line default
            #line hidden
            return;
            case 8:
            this.OutputFilePath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.importedLB = ((System.Windows.Controls.ListBox)(target));
            return;
            case 10:
            this.AnalyzeButton = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\FilterDataWindow.xaml"
            this.AnalyzeButton.Click += new System.Windows.RoutedEventHandler(this.ButtonAnalyze);
            
            #line default
            #line hidden
            return;
            case 11:
            this.filteredLB = ((System.Windows.Controls.ListBox)(target));
            return;
            case 12:
            this.BrowseInputFilter = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\FilterDataWindow.xaml"
            this.BrowseInputFilter.Click += new System.Windows.RoutedEventHandler(this.ButtonBrowse);
            
            #line default
            #line hidden
            return;
            case 13:
            this.filePathFilter = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

