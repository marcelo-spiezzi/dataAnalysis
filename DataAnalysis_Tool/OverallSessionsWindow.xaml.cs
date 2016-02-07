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
using System.ComponentModel;

namespace DataAnalysis_Tool
{
    /// <summary>
    /// Interaction logic for OverallSessionsWindow.xaml
    /// </summary>
    public partial class OverallSessionsWindow : Window
    {
        //list of all entries by type. Used to display graphs
        List<double>[] savedValues = new List<double>[14];
        //array of bool to know which paramater to print in output file
        bool[] outputFilters = new bool[14];
        //table with total, mean, median and std deviation for all 14 parameters
        double[][] calculatedValues;
        //convinience to print to file. I store the imported data as it came so I dont need to transpose the savedValues matrix
        string[] savedDataEntries;

        char[] delimiterChars = { ',', ';' };

        public OverallSessionsWindow()
        {
            InitializeComponent();
            for (int i = 0; i < outputFilters.Length; i++) outputFilters[i] = false;
        }

        private void displayUserMessage(string msg)
        {
            UserMessage.Text = msg;
        }

        private void ButtonAnalyze(object sender, RoutedEventArgs e)
        {
            loadDataFile();
        }

        //load input file and create an array dataEntries to call parseDataFile()
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
            calculatedValues = parseDataFile(dataEntries);
            printInViewer(calculatedValues);
            savedDataEntries = dataEntries;
        }

        //parse each entry by the 14 different values and populate the list<double>[] values
        //call the functions calculate mean, median and standard deviation
        private double[][] parseDataFile(string[] dataEntries)
        {
            int size = dataEntries.Length;
            double[] totals = new double[14];
            double[][] finalValues = new double[14][];

            for (int y = 0; y <= 13; y++)
            {
                savedValues[y] = new List<double>();
            }     

            for (int i = 0; i < size; i++)
            {
                string[] dataPoint = dataEntries[i].Split(delimiterChars);

                for (int a = 0; a <= 13; a++)
                {
                    savedValues[a].Add(System.Convert.ToDouble(dataPoint[a + 2]));
                    totals[a] += System.Convert.ToDouble(dataPoint[a+2]);
                }                   
            }

            tbNumberSessions.Text = size.ToString();
           
            for (int a = 0; a <= 13; a++)
            {
                finalValues[a] = new double[4];
                //totals
                finalValues[a][0] = totals[a];               
                //means
                finalValues[a][1] = calculateMean(totals[a], size);
                //median
                finalValues[a][2] = calculateMedian(savedValues[a]);
                //standard deviation
                finalValues[a][3] = calculateStandardDeviation(savedValues[a]);
                //enable graphs buttons
                string b = "Button_" + a;
                Button bt = FindName(b) as Button;
                bt.IsEnabled = true;
            }

            return finalValues;
        }

        private void printInViewer(double[][] values)
        {
            for (int a = 0; a <= 13; a++)
            {
                //totals
                string t = "tbTotal_" + a;
                TextBlock tb1 = FindName(t) as TextBlock;
                tb1.Text = values[a][0].ToString();

                //means
                string m = "tbMean_" + a;
                TextBlock tb2 = FindName(m) as TextBlock;
                tb2.Text = values[a][1].ToString("F4");

                //median
                string md = "tbMedian_" + a;
                TextBlock tb3 = FindName(md) as TextBlock;
                tb3.Text = values[a][2].ToString("F4");

                //standard deviation
                string s = "tbDeviation_" + a;
                TextBlock tb4 = FindName(s) as TextBlock;
                tb4.Text = values[a][3].ToString("F4");
            }
        }

        private int printToFile(string[] fileEntries, bool[] filters)
        {
            int numberOfFilters = 0;
          
            for (int i = 0; i < filters.Length; i++)
            {
                if (filters[i]) { numberOfFilters++; }
            }

            if (numberOfFilters == 0) return 0;

            double[][] points = allocateList(fileEntries.Length, numberOfFilters);

            for(int i = 0; i < fileEntries.Length ; i++)
            {
                string[] entries = fileEntries[i].Split(delimiterChars);
                int pos = 0;

                for(int j = 0; j < numberOfFilters; j++)
                {                   
                    if (filters[j])
                    {
                        points[i][pos] = System.Convert.ToDouble(entries[j+2]);
                        pos++;
                    }                 
                }
            }

            for (int i = 0; i < points.Length; i++)
            {
                string item = "";
                for (int j = 0; j < points[i].Length; j++)
                {
                    if (j == 0) item = points[i][j].ToString("F6");
                    else item += "," + points[i][j].ToString("F6");                   
                }
                File.AppendAllText(filePathOutput.Text, item + Environment.NewLine);
            }                 

            return numberOfFilters;
        }

        //allocate space to store the data for the output based on the number of active filters
        private double[][] allocateList(int numberOfEntries, int numberOfDimensions)
        {
            double[][] points = new double[numberOfEntries][];

            for (int i = 0; i < numberOfEntries; i++)
            {
                for (int j = 0; j < numberOfDimensions; j++)
                {
                    points[i] = new double[numberOfDimensions];
                    points[i][j] = 0.0;
                }
            }              

            return points;
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
                    if((sender as Button).Name == "BrowseInput") filePath.Text = ofd.FileName.ToString();
                    else filePathOutput.Text = ofd.FileName.ToString();
                   
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

        //Generic button click function to display graphs based on button ID
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] button = (sender as Button).Name.Split('_');
            int index = System.Convert.ToInt16(button[1]);

            List<KeyValuePair<double, int>> valueList = new List<KeyValuePair<double, int>>();

            var q1 = savedValues[index].GroupBy(x => x)
                    .Select(g => new { Value = g.Key, Count = g.Count() });

            foreach (var x in q1) { valueList.Add(new KeyValuePair<double, int>(x.Value, x.Count)); }

            GraphTest win = new GraphTest();          
            win.Show();
            win.showChart(valueList, (sender as Button).Content.ToString());
        }

        private void ButtonSave(object sender, RoutedEventArgs e)
        {
            int a = printToFile(savedDataEntries, outputFilters);
            if (a == 0) { displayUserMessage("No filters selected. File not saved"); }
            else displayUserMessage("Saved Successfully to file with " + a + " dimensions");
        }

        //Generic checkbox events to toogle values in the outputFilters array
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string[] cb = (sender as CheckBox).Name.Split('_');
            int index = System.Convert.ToInt16(cb[1]);
            outputFilters[index] = true;
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            string[] cb = (sender as CheckBox).Name.Split('_');
            int index = System.Convert.ToInt16(cb[1]);
            outputFilters[index] = false;
        }
    }
}
