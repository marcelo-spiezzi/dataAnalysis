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
    /// Interaction logic for FilterDataWindow.xaml
    /// </summary>
    public partial class FilterDataWindow : Window
    {
        char[] delimiterChars = { ',', ';' };
        string[] savedDataEntries;
        string[] filteredValues;

        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

        public FilterDataWindow()
        {
            InitializeComponent();
        }

        private void analyzeFile()
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

            savedDataEntries = dataEntries;
        }
        
        //load filter file that has specific sessions IDs to be outputed and save to array filterSessionIDs
        private int[] analyzeFilterFile()
        {
            int counter = 0;
            int numEntries = File.ReadAllLines(filePathFilter.Text).Count();
            string[] dataEntries = new string[numEntries];
            int[] filterSessionIDs = new int[numEntries];

            System.IO.StreamReader file = new System.IO.StreamReader(filePathFilter.Text);

            string line;

            while ((line = file.ReadLine()) != null)
            {
                dataEntries[counter] = line;
                filterSessionIDs[counter] = System.Convert.ToInt16(dataEntries[counter]);
                counter++;
            }

            file.Close();

            return filterSessionIDs;
        }

        private void clearData()
        {
            filteredLB.Items.Clear();
            importedLB.Items.Clear();
        }

        private void ButtonBrowse(object sender, RoutedEventArgs e)
        {
            ofd.Filter = "Main|*.main|Filter|*.filter|Text|*.txt";
            ofd.Multiselect = false;

            ofd.InitialDirectory = "Desktop";

            if ((bool)ofd.ShowDialog())
            {
                try
                {
                    if ((sender as Button) == BrowseOutput) OutputFilePath.Text = ofd.FileName.ToString();
                    else if ((sender as Button) == BrowseInputFilter) filePathFilter.Text = ofd.FileName.ToString();
                    else filePath.Text = ofd.FileName.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void ButtonImport(object sender, RoutedEventArgs e)
        {
            clearData();
            analyzeFile();

            for (int i = 0; i < savedDataEntries.Length; i++)
            {
                importedLB.Items.Add(savedDataEntries[i]);                
            }

            AnalyzeButton.IsEnabled = true;
        }

        private void ButtonAnalyze(object sender, RoutedEventArgs e)
        {
            int[] filters = analyzeFilterFile();
            filteredValues = new string[filters.Length];

            for (int i = 0; i < savedDataEntries.Length; i++)
            {
                string[] entry = savedDataEntries[i].Split(delimiterChars);
                for(int j = 0; j < filters.Length ; j++)
                {
                    if (System.Convert.ToInt16(entry[0]) == filters[j])
                    {
                        filteredValues[j] = savedDataEntries[i];
                        filteredLB.Items.Add(filteredValues[j].ToString());
                    }
                }                
            }

            SaveOutputButton.IsEnabled = true;
        }

        private void SaveOutputButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < filteredValues.Length; i++ )
                File.AppendAllText(OutputFilePath.Text, filteredValues[i] + Environment.NewLine);
        }

        private void ReturnToMainButton(object sender, RoutedEventArgs e)
        {
            ModeSelectWindow win = new ModeSelectWindow();
            win.Show();
            this.Close();
        }


    }
}
