﻿#pragma checksum "..\..\KmeansWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6C0014F7BC9D8590F5A4FEAA4D0B4D78"
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
    /// KmeansWindow
    /// </summary>
    public partial class KmeansWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbKValue;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BrowseInput;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox filePath;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock UserMessage;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbDimensions;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox valuesList;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox analyzedList;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AnalyzeButton;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RegularGraph;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ClusterGraph;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbRndSeed;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveOutputButton;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BrowseOutput;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OutputFilePath;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox meansList;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox avgDistanceList;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox normalizedValuesList;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox avgDistClusterList;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbAvgDistance;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox numTulpsPerClusterList;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonSaveClusterIDs;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\KmeansWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbClusterToSave;
        
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
            System.Uri resourceLocater = new System.Uri("/DataAnalysis_Tool;component/kmeanswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\KmeansWindow.xaml"
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
            this.tbKValue = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            
            #line 10 "..\..\KmeansWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.importFile);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BrowseInput = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\KmeansWindow.xaml"
            this.BrowseInput.Click += new System.Windows.RoutedEventHandler(this.ButtonBrowse);
            
            #line default
            #line hidden
            return;
            case 4:
            this.filePath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.UserMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.tbDimensions = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.valuesList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.analyzedList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 9:
            this.AnalyzeButton = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\KmeansWindow.xaml"
            this.AnalyzeButton.Click += new System.Windows.RoutedEventHandler(this.ButtonAnalyze);
            
            #line default
            #line hidden
            return;
            case 10:
            this.RegularGraph = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\KmeansWindow.xaml"
            this.RegularGraph.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ClusterGraph = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\KmeansWindow.xaml"
            this.ClusterGraph.Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 12:
            this.tbRndSeed = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.SaveOutputButton = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\KmeansWindow.xaml"
            this.SaveOutputButton.Click += new System.Windows.RoutedEventHandler(this.SaveOutputButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.BrowseOutput = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\KmeansWindow.xaml"
            this.BrowseOutput.Click += new System.Windows.RoutedEventHandler(this.ButtonBrowse);
            
            #line default
            #line hidden
            return;
            case 15:
            this.OutputFilePath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 16:
            this.meansList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 17:
            this.avgDistanceList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 18:
            this.normalizedValuesList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 19:
            this.avgDistClusterList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 20:
            this.tbAvgDistance = ((System.Windows.Controls.TextBox)(target));
            return;
            case 21:
            
            #line 41 "..\..\KmeansWindow.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 41 "..\..\KmeansWindow.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 42 "..\..\KmeansWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 23:
            this.numTulpsPerClusterList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 24:
            this.ButtonSaveClusterIDs = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\KmeansWindow.xaml"
            this.ButtonSaveClusterIDs.Click += new System.Windows.RoutedEventHandler(this.ButtonSaveClusterIDs_Click);
            
            #line default
            #line hidden
            return;
            case 25:
            this.tbClusterToSave = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

