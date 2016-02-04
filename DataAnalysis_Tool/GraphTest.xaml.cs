using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace DataAnalysis_Tool
{
    /// <summary>
    /// Interaction logic for GraphTest.xaml
    /// </summary>
    public partial class GraphTest : Window
    {
        public GraphTest()
        {
            InitializeComponent();      
        }

        public void showChart(List<KeyValuePair<double, int>> list, string title)
        {
            GraphObj.Title = title;
            GraphObj.DataContext = list;
        }

    }
}
