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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;

namespace DataAnalysis_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double[] dataEntries;

        public MainWindow()
        {
            InitializeComponent();
        }

        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

        //Browse for files
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ofd.Filter = "Text|*.txt|JSON|*.json";
            ofd.Multiselect = false;

            ofd.InitialDirectory = "c:\\";

            if ((bool)ofd.ShowDialog())
            {
                try
                {
                    filePath.Text = ofd.FileName.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        //Analyze Data
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            analyzeData();
        }

        //Save graph to png
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void analyzeData()
        {
            int counter = 0;
            string line;
            dataEntries = new double[10];

            System.IO.StreamReader file = new System.IO.StreamReader(filePath.Text);

            while ((line = file.ReadLine()) != null)
            {
                dataEntries[counter] = System.Convert.ToDouble(line);
                counter++;
            }

            file.Close();

            this.Points = new List<DataPoint>
                                {
                                    new DataPoint(0, dataEntries[0]),
                                    new DataPoint(1, dataEntries[1]),
                                    new DataPoint(2, dataEntries[2]),
                                    new DataPoint(3, dataEntries[3]),
                                    new DataPoint(4, dataEntries[4]),
                                    new DataPoint(5, dataEntries[5]),
                                    new DataPoint(6, dataEntries[6])
                                };

            ImportMessage.Text = "Import successful. There were " + counter + " events.";
        }

        public IList<DataPoint> Points { get; private set; }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
