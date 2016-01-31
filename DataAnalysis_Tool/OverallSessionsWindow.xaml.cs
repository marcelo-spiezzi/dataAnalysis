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
        List<int> eventsIDList              = new List<int>();
        List<int> uniqueBiomesList          = new List<int>();
        List<double> gameplayLenghtList     = new List<double>();
        List<double> traveledDistanceList   = new List<double>();
        List<int> miningEventsList          = new List<int>();
        List<int> buildingEventsList        = new List<int>();
        List<int> harvestingEventsList      = new List<int>();
        List<int> craftingEventsList        = new List<int>();
        List<int> farmingEventsList         = new List<int>();
        List<int> exploringEventsList       = new List<int>();
        List<int> deathsEventsList          = new List<int>();
        List<int> huntEventsList            = new List<int>();
        List<int> fightEventsList           = new List<int>();
        List<int> consumableEventsList      = new List<int>();

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

            for (int i = 0; i < size; i++)
            {
                string[] dataPoint = dataEntries[i].Split(delimiterChars);

                //0 Sessions ID, 1 Player Name,2 Session Length, 3 #events, 4 #unique biomes, 5 traveled distance, 
                //6 #mining, 7 #building, 8 #harvesting, 9 #crafting, 10 #farming, 11 #exploring, 12 #deaths, 13 #hunt, 14 #fight, 15 #consumables

                gameplayLenghtList.Add(System.Convert.ToDouble(dataPoint[2]));
                eventsIDList.Add(System.Convert.ToInt16(dataPoint[3]));
                uniqueBiomesList.Add(System.Convert.ToInt16(dataPoint[4]));
                traveledDistanceList.Add(System.Convert.ToDouble(dataPoint[5]));
                miningEventsList.Add(System.Convert.ToInt16(dataPoint[6]));
                buildingEventsList.Add(System.Convert.ToInt16(dataPoint[7]));
                harvestingEventsList.Add(System.Convert.ToInt16(dataPoint[8]));
                craftingEventsList.Add(System.Convert.ToInt16(dataPoint[9]));
                farmingEventsList.Add(System.Convert.ToInt16(dataPoint[10]));
                exploringEventsList.Add(System.Convert.ToInt16(dataPoint[11]));
                deathsEventsList.Add(System.Convert.ToInt16(dataPoint[12]));
                huntEventsList.Add(System.Convert.ToInt16(dataPoint[13]));
                fightEventsList.Add(System.Convert.ToInt16(dataPoint[14]));
                consumableEventsList.Add(System.Convert.ToInt16(dataPoint[15]));

                for (int a = 0; a <= 13; a++)
                {
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
            }

        }

        private double calculateMean(double total, int size)
        {
            return total/size;
        }

        private float calculateMedian(List<float> list)
        {
            float median = 0;

            return median;
        }

        private float calculateStandardDeviation(List<float> list)
        {
            float standardDeviation = 0;

            return standardDeviation;
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
