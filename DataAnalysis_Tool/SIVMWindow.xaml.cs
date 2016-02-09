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
using System.IO;

namespace DataAnalysis_Tool
{
    /// <summary>
    /// Interaction logic for SIVMWindow.xaml
    /// </summary>
    public partial class SIVMWindow : Window
    {
        char[] delimiterChars = { ',', ';' };
        double[][] importedValues;

        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

        public SIVMWindow()
        {
            InitializeComponent();
            /*Line myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            myLine.X1 = 1;
            myLine.X2 = 50;
            myLine.Y1 = 1;
            myLine.Y2 = 50;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            Grid.Children.Add(myLine);*/
        }

        //analyze file and returns an array with all lines
        private string[] analyzeFile()
        {
            int counter = 0;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(filePath.Text);
            var count = File.ReadLines(filePath.Text).Count();

            string[] dataEntries = new string[count];

            file.DiscardBufferedData();
            file.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            file.BaseStream.Position = 0;

            //save all data entries to the array dataEntires
            while ((line = file.ReadLine()) != null)
            {
                dataEntries[counter] = line;
                counter++;
            }

            file.Close();

            return dataEntries;
        }

        private void ButtonBrowse(object sender, RoutedEventArgs e)
        {
            ofd.Filter = "Text|*.txt|JSON|*.json";
            ofd.Multiselect = false;

            ofd.InitialDirectory = "Desktop";

            if ((bool)ofd.ShowDialog())
            {
                try
                {
                    if ((sender as Button) == BrowseOutput) OutputFilePath.Text = ofd.FileName.ToString();
                    else filePath.Text = ofd.FileName.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void ReturnToMainButton(object sender, RoutedEventArgs e)
        {
            ModeSelectWindow win = new ModeSelectWindow();
            win.Show();
            this.Close();
        }

        private void SaveOutputButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonImport(object sender, RoutedEventArgs e)
        {
            string[] fileEntries = analyzeFile();
            importedValues = new double[fileEntries.Length][];

            valuesList.Items.Clear();

            for (int i = 0; i < fileEntries.Length; i++)
            {
                double[] a = Array.ConvertAll(fileEntries[i].Split(delimiterChars), Double.Parse);
                if (i == 0)
                {
                    
                }
                importedValues[i] = a;
            }

            //Im adding the values to the ListBox here and not on the later for just to double check the created list is correct
            for (int i = 0; i < importedValues.Length; i++)
            {
                string position = "";

                for (int j = 0; j < importedValues[i].Length; j++)
                {
                    if (j == 0) position = i + ">" + importedValues[i][j];
                    else { position = position + " | " + importedValues[i][j]; }
                }
                valuesList.Items.Add(position);
            }

            AnalyzeButton.IsEnabled = true;
            GraphButton.IsEnabled = true;
        }

        private void ButtonGraph(object sender, RoutedEventArgs e)
        {
            ScatterSeriesWindow win = new ScatterSeriesWindow();
            List<KeyValuePair<double, double>> list = new List<KeyValuePair<double, double>>();

            for (int i = 0; i < importedValues.Length; i++)
            {
                list.Add(new KeyValuePair<double, double>(importedValues[i][0], importedValues[i][1]));
            }

            win.Show();
            win.showChart(list, "Data distribution");
        }

        private void ButtonAnalyze(object sender, RoutedEventArgs e)
        {

            GraphButton2.IsEnabled = true;
        }

        private void ButtonGraph2(object sender, RoutedEventArgs e)
        {
            ScatterSeriesWindow win = new ScatterSeriesWindow();
            List<KeyValuePair<double, double>> list = new List<KeyValuePair<double, double>>();

            for (int i = 0; i < importedValues.Length; i++)
            {
                list.Add(new KeyValuePair<double, double>(importedValues[i][0], importedValues[i][1]));
            }

            win.Show();
            win.showChart(list, "Data distribution");
        }
    }
}
