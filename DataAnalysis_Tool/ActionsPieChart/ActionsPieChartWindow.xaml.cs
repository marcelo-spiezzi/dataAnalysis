using System.Windows;
using OxyPlot;
using OxyPlot.Series;

namespace ActionsPieChart
{
    /// <summary>
    /// Interaction logic for ActionsPieChartWindow.xaml
    /// </summary>
    public partial class ActionsPieChartWindow : Window
    {
        public ActionsPieChartWindow()
        {
            InitializeComponent();
            DataContext = this;
            var tmp = new PlotModel { Title = "Scatter plot", Subtitle = "Barnsley fern (IFS)" };
            var s1 = new LineSeries
            {
                StrokeThickness = 0,
                MarkerSize = 3,
                MarkerStroke = OxyColors.ForestGreen,
                MarkerType = MarkerType.Plus
            };

            foreach (var pt in ActionsBreakdown.Generate(2000))
            {
                s1.Points.Add(new DataPoint(pt.X, -pt.Y));
            }

            tmp.Series.Add(s1);
            this.ScatterModel = tmp;
        }

        public PlotModel ScatterModel { get; set; }
    }
}
