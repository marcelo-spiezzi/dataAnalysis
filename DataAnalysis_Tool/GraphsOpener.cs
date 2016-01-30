using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace DataAnalysis_Tool
{
    class GraphsOpener
    {
        public GraphsOpener(Type mainWindowType, string title = null)
        {
            this.MainWindowType = mainWindowType;
            this.Title = title ?? mainWindowType.Namespace;
        }

        public string Title { get; private set; }
        public Type MainWindowType { get; set; }

        public override string ToString()
        {
            return this.Title;
        }

        public Window Create()
        {
            return Activator.CreateInstance(this.MainWindowType) as Window;
        }
    }
}
