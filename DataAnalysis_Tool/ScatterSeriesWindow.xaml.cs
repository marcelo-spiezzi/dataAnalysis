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
using System.Windows.Controls.DataVisualization.Charting;

namespace DataAnalysis_Tool
{
    /// <summary>
    /// Interaction logic for ScatterSeriesWindow.xaml
    /// </summary>
    public partial class ScatterSeriesWindow : Window
    {
        public ScatterSeriesWindow()
        {
            InitializeComponent();
        }

        public void showChart(List<KeyValuePair<double, double>> list, string title)
        {
            GraphObj.Title = title;
            addSeries(list, 1);
        }

        public void addSeries(List<KeyValuePair<double, double>> list, int index)
        {
            ScatterSeries scatter = new ScatterSeries();
            scatter.Title = "Cluster " + index;
            scatter.DependentValuePath = "Value";
            scatter.IndependentValuePath = "Key";
            scatter.ItemsSource = list;
            GraphObj.Series.Add(scatter);
        }

        public void addLines(List<KeyValuePair<double, double>> list)
        {
            var style = new Style();
            style.TargetType = typeof(LineDataPoint);
            style.Setters.Add(new Setter(BackgroundProperty, Brushes.Red));

            LineSeries line = new LineSeries();
            line.DependentValuePath = "Value";
            line.IndependentValuePath = "Key";
            line.DataPointStyle = style;
            line.ItemsSource = list;
            GraphObj.Series.Add(line);
        }

        public void showMultipleSeries(List<KeyValuePair<double, double>>[] list, string title)
        {
            GraphObj.Title = title;
            for (int i = 0; i < list.Length; i++ ) { addSeries(list[i], i); }
        }

    }
}
