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
        double[] importedValuesIDs;
        double[][] savedVolumeCoordinates;

        struct calculatedPoint
        {
            public double[] pointValue;
            public int pointID;
        }

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
            ofd.Filter = "Points|*.points|Text|*.txt";
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

        private void clearData()
        {
            valuesList.Items.Clear();
            pointsCoordinatesLB.Items.Clear();
        }

        private double[][] SIVM_algorithm(double[][] points)
        {
            //number of points for the volume is 2^number of dimensions. i.e: 2D = square, 3D = cube, 4D = no idea how its called!
            int dimensions = System.Convert.ToInt16(points[0].Length);
            int numberOfPoints = System.Convert.ToInt16(Math.Pow(2, dimensions));
            double[][] volumePoints = new double[numberOfPoints][];
            double[] volumePointsID = new double[numberOfPoints];

            //normalize data
            double[][] normalizedData = normalizeData(points);

            //find a random P1 point in the sample
            Random rnd = new Random();
            double[] P1 = normalizedData[rnd.Next(0, normalizedData.Length)];          

            //from P1, find the most distant point -> P2
            calculatedPoint P2 = findMostDistantPoint(normalizedData, P1);

            //from P2, find the most distant point -> P3 (first volume coordinate)
            volumePoints[0] = new double[dimensions];
            calculatedPoint P3 = findMostDistantPoint(normalizedData, P2.pointValue);
            volumePoints[0] = P3.pointValue;
            volumePointsID[0] = P3.pointID;

            //from P3, find the most distant point -> P4 (second volume coordinate)
            volumePoints[1] = new double[dimensions];
            calculatedPoint P4 = findMostDistantPoint(normalizedData, P3.pointValue);
            volumePoints[1] = P4.pointValue;
            volumePointsID[1] = P4.pointID;

            //Calculate the most distant point from all points
            for(int i = 2; i < numberOfPoints; i++) //two first points are already allocated
            {
                calculatedPoint PI = new calculatedPoint();
                volumePoints[i] = new double[dimensions]; 
                double[][] currentPoints = new double[i][];
                for(int j = 0; j < i; j++)
                {
                    currentPoints[j] = new double[dimensions];
                    currentPoints[j] = volumePoints[j];
                }
                PI = findMostDistantPointFromMult(normalizedData, currentPoints);
                volumePoints[i] = PI.pointValue;
                volumePointsID[i] = PI.pointID;
            }

            //the calculation was made with normalized data, now we need to get the ids and save the actual points values
            double[][] nonNormalizedVolPoints = new double[numberOfPoints][];

            for (int i = 0; i < points.Length; i++)
            {
                for(int k = 0; k < volumePointsID.Length; k++)
                {
                    if(i == volumePointsID[k])
                       nonNormalizedVolPoints[k] = points[i];
                }               
            }

            return nonNormalizedVolPoints;
        }

        private double[][] normalizeData(double[][] rawData)
        {
            // normalize raw data by computing (x - mean) / stddev
            // primary alternative is min-max:
            // v' = (v - min) / (max - min)

            // make a copy of input data
            double[][] result = new double[rawData.Length][];
            for (int i = 0; i < rawData.Length; ++i)
            {
                result[i] = new double[rawData[i].Length];
                Array.Copy(rawData[i], result[i], rawData[i].Length);
            }

            for (int j = 0; j < result[0].Length; ++j) // each col
            {
                double colSum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    colSum += result[i][j];
                double mean = colSum / result.Length;
                double sum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    sum += (result[i][j] - mean) * (result[i][j] - mean);
                double sd = sum / result.Length;
                for (int i = 0; i < result.Length; ++i)
                    result[i][j] = (result[i][j] - mean) / sd;
            }
            return result;
        }

        private calculatedPoint findMostDistantPoint(double[][] allPoints, double[] refPoint)
        {
            calculatedPoint mostDistantPoint = new calculatedPoint();
            mostDistantPoint.pointValue = new double[refPoint.Length];
            mostDistantPoint.pointID = 0;

            double maxDist = 0;

            //from ref point find the most distant point
            for (int i = 0; i < allPoints.Length; i++)
            {
                double d = 0;
                for (int j = 0; j < allPoints[i].Length; j++)
                    d += (refPoint[j] - allPoints[i][j]) * (refPoint[j] - allPoints[i][j]);
                d = Math.Sqrt(d);
                if (d > maxDist)
                {
                    maxDist = d;
                    mostDistantPoint.pointValue = allPoints[i];
                    mostDistantPoint.pointID = i;
                }
            }
            return mostDistantPoint;
        }

        private calculatedPoint findMostDistantPointFromMult(double[][] allPoints, double[][] refPoints)
        {
            calculatedPoint mostDistantPoint = new calculatedPoint();
            mostDistantPoint.pointValue = new double[refPoints[0].Length];
            mostDistantPoint.pointID = 0;

            double maxDist = 0;
            bool isTheSamePoint = false;

            //from ref point find the most distant point
            for (int i = 0; i < allPoints.Length; i++)
            {
                double sumPointDist = 0;
                // check to not compare with point that has already been used
                isTheSamePoint = false;
                for (int j = 0; j < refPoints.Length; j++)
                    if (allPoints[i] == refPoints[j]) isTheSamePoint = true;
                if (!isTheSamePoint)
                {
                    for (int j = 0; j < refPoints.Length; j++)
                    {
                        if (allPoints[i] != refPoints[j])
                        {
                            double d = 0;
                            for (int k = 0; k < refPoints[j].Length; k++)
                                d += (refPoints[j][k] - allPoints[i][k]) * (refPoints[j][k] - allPoints[i][k]);
                            d = Math.Sqrt(d);
                            sumPointDist = d;
                        }
                    }
                    if (sumPointDist > maxDist)
                    {
                        maxDist = sumPointDist;
                        mostDistantPoint.pointValue = allPoints[i];
                        mostDistantPoint.pointID = i;
                    }
                }   
            }
            return mostDistantPoint;
        }

        private void SaveOutputButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < savedVolumeCoordinates.Length; i++)
            {
                string a = "";
                for (int j = 0; j < savedVolumeCoordinates[i].Length; j++)
                {
                    if (j == 0) a = importedValuesIDs[i] + "," + savedVolumeCoordinates[i][j].ToString();
                    else a += "," + savedVolumeCoordinates[i][j].ToString();                  
                }
                File.AppendAllText(OutputFilePath.Text, a + Environment.NewLine);
            }
                    
        }

        private void ButtonImport(object sender, RoutedEventArgs e)
        {
            clearData();
            string[] fileEntries = analyzeFile();
            importedValues = new double[fileEntries.Length][];
            importedValuesIDs = new double[fileEntries.Length];

            valuesList.Items.Clear();

            for (int i = 0; i < fileEntries.Length; i++)
            {
                double[] a = Array.ConvertAll(fileEntries[i].Split(delimiterChars), Double.Parse);
                //separting IDs from actual data points (first data field is session ID)
                double[] b = new double[a.Length - 1];
                for (int j = 0; j < b.Length; j++)
                    b[j] = a[j + 1];

                importedValuesIDs[i] = a[0];
                importedValues[i] = b;
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
            savedVolumeCoordinates = SIVM_algorithm(importedValues);
            
            for(int i = 0; i < savedVolumeCoordinates.Length; i++){
                string item = "";
                for(int k = 0; k < savedVolumeCoordinates[i].Length; k++)
                    item += savedVolumeCoordinates[i][k] + " ,";
                pointsCoordinatesLB.Items.Add(item);
            }
            SaveOutputButton.IsEnabled = true;
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

            for (int i = 0; i < savedVolumeCoordinates.Length ; i++)
            {
                List<KeyValuePair<double, double>> point = new List<KeyValuePair<double, double>>();
                point.Add(new KeyValuePair<double, double>(savedVolumeCoordinates[i][0], savedVolumeCoordinates[i][1]));
                if (i < savedVolumeCoordinates.Length - 1)
                    point.Add(new KeyValuePair<double, double>(savedVolumeCoordinates[i+1][0], savedVolumeCoordinates[i+1][1]));
                else
                    point.Add(new KeyValuePair<double, double>(savedVolumeCoordinates[0][0], savedVolumeCoordinates[0][1]));
                win.addLines(point);
            }
            

        }
    }
}
