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
    /// Interaction logic for KmeansWindow.xaml
    /// </summary>
    public partial class KmeansWindow : Window
    {
        double[][] importedValues;
        double[] importedValuesIDs;
        double[] avgDistances;
        double avgMeansDistance;
        double[][] finalMeans;
        int usedRandom;
        int usedK;
        bool usedNormalized;

        int[] clusteredData;
        int numberOfClusters = 0;

        char[] delimiterChars = { ',', ';' };

        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

        public bool NormalizeData = true;

        public KmeansWindow()
        {
            InitializeComponent();
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

        bool IsAllDigits(string s)
        {
            if (s == "") return false;
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        private void ButtonAnalyze(object sender, RoutedEventArgs e)
        {
            if (IsAllDigits(tbKValue.Text)){
                messageToUser("Starting the process...");                
                analyzeList();
            }
                
            else
                messageToUser("Please enter a numeric value for K");
        }

        private void messageToUser(string msg)
        {
            UserMessage.Text = msg;
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

        private void importFile(object sender, RoutedEventArgs e)
        {            
            string[] fileEntries = analyzeFile();
            importedValues = new double[fileEntries.Length][];
            importedValuesIDs = new double[fileEntries.Length];

            valuesList.Items.Clear();

            for (int i = 0; i < fileEntries.Length; i++)
            {
                double[] a = Array.ConvertAll(fileEntries[i].Split(delimiterChars), Double.Parse);
                if (i == 0)
                {
                    tbDimensions.Text = a.Length.ToString();
                }
                //separting IDs from actual data points (first data field is session ID)
                double[] b = new double[a.Length - 1];
                for (int j = 0; j < b.Length; j++)
                    b[j] = a[j + 1];

                importedValuesIDs[i] = a[0];
                importedValues[i] = b;
            }

            //Im adding the values to the ListBox here and not on the later for just to double check the created list is correct
            for (int i = 0; i < importedValues.Length; i++){

                string position = "";

                for (int j = 0; j < importedValues[i].Length; j++)
                {
                    if (j == 0) position = i + ">" + importedValues[i][j];
                    else { position = position + " | " + importedValues[i][j]; }
                }
                valuesList.Items.Add(position);
            }

            AnalyzeButton.IsEnabled = true;
            RegularGraph.IsEnabled = true;
            messageToUser("File imported successfully. There were " + importedValues.Length + " entries");
        }

        private void clearResults()
        {
            analyzedList.Items.Clear();
            avgDistanceList.Items.Clear();
            avgDistClusterList.Items.Clear();
            normalizedValuesList.Items.Clear();
            meansList.Items.Clear();
            numTulpsPerClusterList.Items.Clear();
        }

        private void analyzeList()
        {
            clearResults();
            clusteredData = new int[importedValues.Length];
            numberOfClusters = System.Convert.ToInt16(tbKValue.Text);
            clusteredData = Cluster(importedValues, numberOfClusters, System.Convert.ToInt16(tbRndSeed.Text));
            ClusterGraph.IsEnabled = true;
            SaveOutputButton.IsEnabled = true;
            messageToUser("Cluster analysis completed");
        }

        private void printClustersToList(int[] clusteredData, double[][] means, double[][] data)
        {
            int[] tulpsPerCluster = new int[numberOfClusters];

            for (int i = 0; i < clusteredData.Length; i++)
            {
                for (int k = 0; k < numberOfClusters; k++)
                {
                   if (clusteredData[i] == k)  tulpsPerCluster[k]++;
                }
            }

            for (int k = 0; k < numberOfClusters; k++)
            {
                analyzedList.Items.Add("== Cluster " + k +  " ==");
                normalizedValuesList.Items.Add("== Cluster " + k + " ==");
                double a = (System.Convert.ToDouble(tulpsPerCluster[k]) / System.Convert.ToDouble(clusteredData.Length))*100;
                numTulpsPerClusterList.Items.Add(tulpsPerCluster[k] + " | " + a.ToString("F2") + "%");

                for (int i = 0; i < importedValues.Length; i++)
                {
                    int clusterID = clusteredData[i];
                    if (clusterID != k) continue;
                    string item = "";
                    string normalizedItem = "";
                    for (int j = 0; j < importedValues[i].Length; j++)
                    {                        
                        if (j == 0)
                        {
                            normalizedItem = data[i][j].ToString("F6");
                            item = importedValues[i][j].ToString();
                        }
                        else
                        {
                            normalizedItem = normalizedItem + " | " + data[i][j].ToString("F6");
                            item = item + " | " + importedValues[i][j].ToString();
                        }
                    }
                    normalizedValuesList.Items.Add(normalizedItem);
                    analyzedList.Items.Add(item);
                }
            }

            for (int i = 0; i < means.Length; i++)
            {
                string mean = "";
                for (int j = 0; j < means[i].Length; j++)
                {
                    if (j == 0) mean = means[i][j].ToString("F6");
                    else mean = mean + " | " + means[i][j].ToString("F6");
                }
                meansList.Items.Add(mean);
            }
        }

        private void printClustersToFile()
        {
            int[] tulpsPerCluster = new int[numberOfClusters];

            File.AppendAllText(OutputFilePath.Text, "K= " + usedK + ", Random Seed= " + usedRandom + ", Normalized Data= " + usedNormalized + Environment.NewLine + Environment.NewLine);
            for (int k = 0; k < numberOfClusters; k++)
            {
                File.AppendAllText(OutputFilePath.Text, "== Cluster " + k + " ==" + Environment.NewLine);
                for (int i = 0; i < importedValues.Length; i++)
                {
                    int clusterID = clusteredData[i];
                    if (clusterID != k) continue;
                    string item = "";
                    for (int j = 0; j < importedValues[i].Length; j++)
                    {
                        if (j == 0) item = importedValues[i][j].ToString("F4");
                        else item = item + " | " + importedValues[i][j].ToString("F4");
                    }
                    File.AppendAllText(OutputFilePath.Text, item + Environment.NewLine);
                }
            }
            File.AppendAllText(OutputFilePath.Text, Environment.NewLine + "============ Distribution ============" + Environment.NewLine);
            for (int i = 0; i < clusteredData.Length; i++)
            {
                for (int k = 0; k < numberOfClusters; k++)
                {
                    if (clusteredData[i] == k) tulpsPerCluster[k]++;
                }
            }
            for (int k = 0; k < numberOfClusters; k++)
            {
                double a = (System.Convert.ToDouble(tulpsPerCluster[k]) / System.Convert.ToDouble(clusteredData.Length)) * 100;
                File.AppendAllText(OutputFilePath.Text, "Cluster " + k + ": " + tulpsPerCluster[k] + " | " + a.ToString("F2") + "%" + Environment.NewLine);
            }
            File.AppendAllText(OutputFilePath.Text, Environment.NewLine +  "============ Centroids ============" + Environment.NewLine);
            for (int i = 0; i < finalMeans.Length; i++)
            {
                string mean = "";
                for (int j = 0; j < finalMeans[i].Length; j++)
                {
                    if (j == 0) mean = finalMeans[i][j].ToString("F6");
                    else mean = mean + " | " + finalMeans[i][j].ToString("F6");
                }
                File.AppendAllText(OutputFilePath.Text, "Cluster " + i + ": " + mean + Environment.NewLine);
            }
            File.AppendAllText(OutputFilePath.Text, Environment.NewLine + "== Tulps avg distance to centroids ==" + Environment.NewLine);
            for (int i = 0; i < avgDistances.Length; i++)
            {            
                File.AppendAllText(OutputFilePath.Text, "Cluster " + i + ": " + avgDistances[i].ToString("F4") + Environment.NewLine);
            }
        }

        private bool printSelectedClusterToFile(int id)
        {
            if (id > numberOfClusters || id < 0) return false;

            for (int i = 0; i < importedValues.Length; i++)
            {
                int clusterID = clusteredData[i];
                if (clusterID != id) continue;
                File.AppendAllText(OutputFilePath.Text, importedValuesIDs[i] + Environment.NewLine);
            }

            return true;
        }

        public int[] Cluster(double[][] rawData, int numClusters, int randomSeed)
        {
            // k-means clustering
            // index of return is tuple ID, cell is cluster ID
            // ex: [2 1 0 0 2 2] means tuple 0 is cluster 2, tuple 1 is cluster 1, tuple 2 is cluster 0, tuple 3 is cluster 0, etc.
            // an alternative clustering DS to save space is to use the .NET BitArray class
            double[][] data;
            if (NormalizeData) data = Normalized(rawData); 
            else data = rawData;

            bool changed = true; // was there a change in at least one cluster assignment?
            bool success = true; // were all means able to be computed? (no zero-count clusters)

            // init clustering[] to get things started
            // an alternative is to initialize means to randomly selected tuples
            // then the processing loop is
            // loop
            //    update clustering
            //    update means
            // end loop
            int[] clustering = InitClustering(data.Length, numClusters, randomSeed); // semi-random initialization
            double[][] means = Allocate(numClusters, data[0].Length); // small convenience

            int maxCount = data.Length * 10; // sanity check
            int ct = 0;
            while (changed == true && success == true && ct < maxCount)
            {
                ++ct; // k-means typically converges very quickly
                success = UpdateMeans(data, clustering, means); // compute new cluster means if possible. no effect if fail
                changed = UpdateClustering(data, clustering, means); // (re)assign tuples to clusters. no effect if fail
            }
            // consider adding means[][] as an out parameter - the final means could be computed
            // the final means are useful in some scenarios (e.g., discretization and RBF centroids)
            // and even though you can compute final means from final clustering, in some cases it
            // makes sense to return the means (at the expense of some method signature uglinesss)
            //
            // another alternative is to return, as an out parameter, some measure of cluster goodness
            // such as the average distance between cluster means, or the average distance between tuples in 
            // a cluster, or a weighted combination of both3

            usedK = numClusters;
            usedRandom = randomSeed;
            usedNormalized = NormalizeData;

            finalMeans = CalculateMeans(rawData, clustering, means);
            printClustersToList(clustering, finalMeans, data);
            avgDistances = CalcAvgDistancesTulpsInCluster(clustering, rawData, finalMeans);
            avgMeansDistance = CalculateAvgDistanceBetweenMeans(finalMeans);
            tbMeansAvgDist.Text = avgMeansDistance.ToString("F4");

            return clustering;
        }

        private double[] CalcAvgDistancesTulpsInCluster(int[] clusteredData, double[][] data, double[][] means)
        {
            double[] avgs = new double[numberOfClusters];

            for (int k = 0; k < numberOfClusters; k++)
            {
                double avg = 0;
                int count = 0;
                avgDistanceList.Items.Add("== Cluster " + k + " ==");
                for (int i = 0; i < data.Length; i++)
                {
                    int clusterID = clusteredData[i];
                    if (clusterID != k) continue;
                    avg += Distance(data[i], means[k]);
                    count++;
                    avgDistanceList.Items.Add(Distance(data[i], means[k]).ToString("F4"));
                }
                avgs[k] = avg/count;
                avgDistClusterList.Items.Add((avg/count).ToString("F4"));
            }

            double total = 0;
            foreach(var a in avgs)
            {
                total += a;
            }
            tbAvgDistance.Text = (total / numberOfClusters).ToString("F4");

            return avgs;
        }

        private double[][] CalculateMeans(double[][] data, int[] clustering, double[][] means)
        {
            int numClusters = means.Length;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            // update, zero-out means so it can be used as scratch matrix 
            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] = 0.0;

            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                for (int j = 0; j < data[i].Length; ++j)
                    means[cluster][j] += data[i][j]; // accumulate sum
            }

            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] /= clusterCounts[k];

            return means;
        }

        private double CalculateAvgDistanceBetweenMeans(double[][] means)
        {
            double totalDistance = 0;

            for (int i = 0; i < means.Length; i++)
            {
                double pointDist = 0;
                for (int j = 0; j < means.Length; j++)
                {
                    double dist = 0;
                    for (int k = 0; k < means[i].Length; k++)
                        dist += (means[i][k] - means[j][k]) * (means[i][k] - means[j][k]);
                    pointDist += Math.Sqrt(dist);  
                }
                totalDistance += pointDist;
            }

            return totalDistance/means.Length;
        }

        private double[][] Normalized(double[][] rawData)
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

        private int[] InitClustering(int numTuples, int numClusters, int randomSeed)
        {
            // init clustering semi-randomly (at least one tuple in each cluster)
            // consider alternatives, especially k-means++ initialization,
            // or instead of randomly assigning each tuple to a cluster, pick
            // numClusters of the tuples as initial centroids/means then use
            // those means to assign each tuple to an initial cluster.
            Random random = new Random(randomSeed);
            int[] clustering = new int[numTuples];
            for (int i = 0; i < numClusters; ++i) // make sure each cluster has at least one tuple
                clustering[i] = i;
            for (int i = numClusters; i < clustering.Length; ++i)
                clustering[i] = random.Next(0, numClusters); // other assignments random
            return clustering;
        }

        private double[][] Allocate(int numClusters, int numColumns)
        {
            // convenience matrix allocator for Cluster()
            double[][] result = new double[numClusters][];
            for (int k = 0; k < numClusters; ++k)
                result[k] = new double[numColumns];
            return result;
        }

        private bool UpdateMeans(double[][] data, int[] clustering, double[][] means)
        {
            // returns false if there is a cluster that has no tuples assigned to it
            // parameter means[][] is really a ref parameter

            // check existing cluster counts
            // can omit this check if InitClustering and UpdateClustering
            // both guarantee at least one tuple in each cluster (usually true)
            int numClusters = means.Length;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false; // bad clustering. no change to means[][]

            // update, zero-out means so it can be used as scratch matrix 
            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] = 0.0;

            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                for (int j = 0; j < data[i].Length; ++j)
                    means[cluster][j] += data[i][j]; // accumulate sum
            }

            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] /= clusterCounts[k]; // danger of div by 0
            return true;
        }

        private bool UpdateClustering(double[][] data, int[] clustering, double[][] means)
        {
            // (re)assign each tuple to a cluster (closest mean)
            // returns false if no tuple assignments change OR
            // if the reassignment would result in a clustering where
            // one or more clusters have no tuples.

            int numClusters = means.Length;
            bool changed = false;

            int[] newClustering = new int[clustering.Length]; // proposed result
            Array.Copy(clustering, newClustering, clustering.Length);

            double[] distances = new double[numClusters]; // distances from curr tuple to each mean

            for (int i = 0; i < data.Length; ++i) // walk thru each tuple
            {
                for (int k = 0; k < numClusters; ++k)
                    distances[k] = Distance(data[i], means[k]); // compute distances from curr tuple to all k means

                int newClusterID = MinIndex(distances); // find closest mean ID
                if (newClusterID != newClustering[i])
                {
                    changed = true;
                    newClustering[i] = newClusterID; // update
                }
            }

            if (changed == false)
                return false; // no change so bail and don't update clustering[][]

            // check proposed clustering[] cluster counts
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = newClustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false; // bad clustering. no change to clustering[][]

            Array.Copy(newClustering, clustering, newClustering.Length); // update
            return true; // good clustering and at least one change
        }

        private double Distance(double[] tuple, double[] mean)
        {
            // Euclidean distance between two vectors for UpdateClustering()
            // consider alternatives such as Manhattan distance
            double sumSquaredDiffs = 0.0;
            for (int j = 0; j < tuple.Length; ++j)
                sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
            return Math.Sqrt(sumSquaredDiffs);
        }

        private int MinIndex(double[] distances)
        {
            // index of smallest value in array
            // helper for UpdateClustering()
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexOfMin = k;
                }
            }
            return indexOfMin;
        }

        //display windown with scattered values
        private void Button_Click(object sender, RoutedEventArgs e)
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
        
        //display window with scattered values by cluster
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ScatterSeriesWindow win = new ScatterSeriesWindow();
            List<KeyValuePair<double, double>>[] list = new List<KeyValuePair<double,double>>[numberOfClusters];

            for (int i = 0; i < numberOfClusters ; i++)
            {
                list[i] = new List<KeyValuePair<double, double>>();
                for(int j = 0; j < importedValues.Length; j++)
                {
                    if (clusteredData[j] == i)
                    {
                        list[i].Add(new KeyValuePair<double, double>(importedValues[j][0], importedValues[j][1]));
                    }                   
                }              
            }

            win.Show();
            win.showMultipleSeries(list , "Clusters View");
        }

        private void SaveOutputButton_Click(object sender, RoutedEventArgs e)
        {
            printClustersToFile();
            messageToUser("File saved!");
        }

        //Binding was not working properly =/
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            NormalizeData = true;
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            NormalizeData = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ModeSelectWindow win = new ModeSelectWindow();
            win.Show();
            this.Close();
        }

        private void ButtonSaveClusterIDs_Click(object sender, RoutedEventArgs e)
        {
            if (IsAllDigits(tbClusterToSave.Text))
                if (!printSelectedClusterToFile(System.Convert.ToInt16(tbClusterToSave.Text)))
                    messageToUser("please enter a valid number for the cluster");
        }


    }
}
