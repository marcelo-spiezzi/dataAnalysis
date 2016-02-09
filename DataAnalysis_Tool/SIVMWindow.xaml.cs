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
        double[][] savedVolumeCoordinates;

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

            //find a random P1 point in the sample
            Random rnd = new Random();         
            double[] P1 = points[rnd.Next(0, points.Length)];          

            //from P1, find the most distant point -> P2
            double[] P2 = findMostDistantPoint(points, P1);

            //from P2, find the most distant point -> P3 (first volume coordinate)
            volumePoints[0] = new double[dimensions];
            volumePoints[0]  = findMostDistantPoint(points, P2);

            //from P3, find the most distant point -> P4 (second volume coordinate)
            volumePoints[1] = new double[dimensions];
            volumePoints[1] = findMostDistantPoint(points, volumePoints[0]);

            //Calculate the most distant point from P3 and P4 -> P5
            for(int i = 2; i < numberOfPoints; i++) //two first points are already allocated
            {
                volumePoints[i] = new double[dimensions]; 
                double[][] currentPoints = new double[i][];
                for(int j = 0; j < i; j++)
                {
                    currentPoints[j] = new double[dimensions];
                    currentPoints[j] = volumePoints[j];
                }
                volumePoints[i] = findMostDistantPointFromMult(points, currentPoints);
            }

            return volumePoints;
        }

        private double[] findMostDistantPoint(double[][] allPoints, double[] refPoint)
        {
            double[] mostDistantPoint = new double[refPoint.Length];
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
                    mostDistantPoint = allPoints[i];
                }
            }
            return mostDistantPoint;
        }

        private double[] findMostDistantPointFromMult(double[][] allPoints, double[][] refPoints)
        {
            double[] mostDistantPoint = new double[refPoints[0].Length];

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
                        mostDistantPoint = allPoints[i];
                    }
                }   
            }
            return mostDistantPoint;
        }

        private void SaveOutputButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonImport(object sender, RoutedEventArgs e)
        {
            clearData();
            string[] fileEntries = analyzeFile();
            importedValues = new double[fileEntries.Length][];

            valuesList.Items.Clear();

            for (int i = 0; i < fileEntries.Length; i++)
            {
                double[] a = Array.ConvertAll(fileEntries[i].Split(delimiterChars), Double.Parse);
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
            savedVolumeCoordinates = SIVM_algorithm(importedValues);
            
            for(int i = 0; i < savedVolumeCoordinates.Length; i++){
                string item = "";
                for(int k = 0; k < savedVolumeCoordinates[i].Length; k++)
                    item += savedVolumeCoordinates[i][k] + " ,";
                pointsCoordinatesLB.Items.Add(item);
            }
            
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
