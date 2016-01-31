using System;
using System.IO;
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

namespace DataAnalysis_Tool
{
    /// <summary>
    /// Interaction logic for OverallSessionsWindow.xaml
    /// </summary>
    public partial class OverallSessionsWindow : Window
    {
        List<double>[] values = new List<double>[14];

        public OverallSessionsWindow()
        {
            InitializeComponent();
        }

        private void ButtonAnalyze(object sender, RoutedEventArgs e)
        {
            loadDataFile();
        }

        private void loadDataFile()
        {
            int counter = 0;
            string[] dataEntries = new string[File.ReadAllLines(filePath.Text).Count()];

            System.IO.StreamReader file = new System.IO.StreamReader(filePath.Text);

            string line;

            while ((line = file.ReadLine()) != null)
            {
                dataEntries[counter] = line;
                counter++;
            }

            file.Close();
            parseDataFile(dataEntries);
        }

        private void parseDataFile(string[] dataEntries)
        {
            int size = dataEntries.Length;
            char[] delimiterChars = { ',', ';' };

            double[] totals = new double[14];

            for (int y = 0; y <= 13; y++)
            {
                values[y] = new List<double>();
            }     

            for (int i = 0; i < size; i++)
            {
                string[] dataPoint = dataEntries[i].Split(delimiterChars);

                //0 Sessions ID, 1 Player Name,2 Session Length, 3 #events, 4 #unique biomes, 5 traveled distance, 
                //6 #mining, 7 #building, 8 #harvesting, 9 #crafting, 10 #farming, 11 #exploring, 12 #deaths, 13 #hunt, 14 #fight, 15 #consumables

                for (int a = 0; a <= 13; a++)
                {
                    values[a].Add(System.Convert.ToDouble(dataPoint[a + 2]));
                    totals[a] += System.Convert.ToDouble(dataPoint[a+2]);
                }                   
            }

            tbNumberSessions.Text = size.ToString();

            for (int a = 0; a <= 13; a++)
            {
                //totals
                string t = "tbTotal_" + a;
                TextBlock tb1 = FindName(t) as TextBlock;
                tb1.Text = totals[a].ToString();

                //means
                string m = "tbMean_" + a;
                TextBlock tb2 = FindName(m) as TextBlock;
                tb2.Text = calculateMean(totals[a], size).ToString();

                //median
                string md = "tbMedian_" + a;
                TextBlock tb3 = FindName(md) as TextBlock;
                tb3.Text = calculateMedian(values[a]).ToString();

                //standard deviation
                string s = "tbDeviation_" + a;
                TextBlock tb4 = FindName(s) as TextBlock;
                tb4.Text = calculateStandardDeviation(values[a]).ToString();
            }

        }

        private double calculateMean(double total, int size)
        {
            return total/size;
        }

        private double calculateMedian(List<double> list)
        {
            int size = list.Count();
            int halfIndex = size/2;

            var sortedNumbers = list.OrderBy(n => n);
            double median;

            if((size % 2) == 0){
                median = (sortedNumbers.ElementAt(halfIndex) + sortedNumbers.ElementAt(halfIndex = 1))/2;
            }
            else{
                median = sortedNumbers.ElementAt(halfIndex);
            }

            return median;
        }



        private double calculateStandardDeviation(List<double> list)
        {
            double M = 0.0;
            double S = 0.0;
            int k = 1;
            foreach (double value in list)
            {
                double tmpM = M;
                M += (value - tmpM) / k;
                S += (value - tmpM) * (value - M);
                k++;
            }
            return Math.Sqrt(S / (k - 1));
        }

        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

        private void ButtonBrowse(object sender, RoutedEventArgs e)
        {
            ofd.Filter = "Text|*.txt|JSON|*.json";
            ofd.Multiselect = false;

            ofd.InitialDirectory = "Desktop";

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

        private void ReturnToMainButton(object sender, RoutedEventArgs e)
        {
            ModeSelectWindow win = new ModeSelectWindow();
            win.Show();
            this.Close();
        }
    }
}
