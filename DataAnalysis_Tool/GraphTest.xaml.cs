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

        public void showChart(List<KeyValuePair<string, int>> list)
        {
            List<KeyValuePair<string, int>> MyValue = new List<KeyValuePair<string, int>>();
            foreach (var a in list)
            {
                MyValue.Add(new KeyValuePair<string, int>(a.Key, a.Value));
            }

            foreach (var b in MyValue)
            {
                lbItems.Items.Add(b.Key + " , " + b.Value);
            }
            BarGraph.DataContext = MyValue;  
        }
    }
}
