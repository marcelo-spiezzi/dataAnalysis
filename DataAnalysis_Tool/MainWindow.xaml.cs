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
        List<int>       allTraveledBiomes   = new List<int>();
        List<double[]>  eventsPositions     = new List<double[]>();
        List<float>     eventsTimes         = new List<float>();
        List<int>       eventsActionsID     = new List<int>();
        int totalDeaths = 0;
        bool firstEntry = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

        //Browse for files
        private void ButtonBrowse(object sender, RoutedEventArgs e)
        {
            ofd.Filter = "Text|*.txt|JSON|*.json";
            ofd.Multiselect = false;

            ofd.InitialDirectory = "Desktop";

            if ((bool)ofd.ShowDialog())
            {
                try
                {
                    if ((sender as Button).Name == "BrowseInput") filePath.Text = ofd.FileName.ToString();
                    else fileOutputPath.Text = ofd.FileName.ToString();                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        //Analyze Data
        private void ButtonAnalyze(object sender, RoutedEventArgs e)
        {
            if (firstEntry)
            {
                analyzeData();
                firstEntry = false;
            }
            else
            {
                emptyData();
                analyzeData();
            }
        }
    
        private void outputMessageToUser(string msg)
        {
            ImportMessage.Text = msg;
        }

        private void emptyData()
        {
            totalDeaths = 0;
            allTraveledBiomes.Clear();
            eventsPositions.Clear();
            eventsTimes.Clear();
            eventsActionsID.Clear();
            lbMinEquip.Items.Clear();
            lbMinMat.Items.Clear();
            lbBuildMat.Items.Clear();
            lbHarvEquip.Items.Clear();
            lbHarvObj.Items.Clear();
            lbCraftObj.Items.Clear();
            lbPlantedSeed.Items.Clear();
            lbHunted.Items.Clear();
            lbHuntedPlayers.Items.Clear();
            lbConsumCat.Items.Clear();
            lbConsumObj.Items.Clear();
        }

        private void analyzeData()
        {
            int counter = 0;
            string line;
            char[] delimiterChars = { ',', ';' };
        
            System.IO.StreamReader file = new System.IO.StreamReader(filePath.Text);
            var count = File.ReadLines(filePath.Text).Count();

            string[] dataEntries = new string[count];

            file.DiscardBufferedData();
            file.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            file.BaseStream.Position = 0;

            string[] header = file.ReadLine().Split(delimiterChars);

            //save all data entries to the array dataEntires
            while ((line = file.ReadLine()) != null)
            {
                dataEntries[counter] = line;
                counter++;
            }

            file.Close();

            //call functions based on file header
            if (header[0] == "session") { sessionParser(header, dataEntries); };

            outputMessageToUser("Import successful. There were " + counter + " events.");
        }

        private void outputSessionToTextFile()
        {
            string sessionInfo = tbSessionID.Text + "," + tbPlayerName.Text + "," + tbSessionLenght.Text + "," + tbTotalEvents.Text + "," +
                                 tbUniqueBiomes.Text + "," + tbTraveledDist.Text + "," + tbTotalMinedMat.Text + "," + tbTotalBuildMat.Text + "," +
                                 tbTotalHavEquip.Text + "," + tbTotalCrafted.Text + "," + tbTotalPlanted.Text + "," + tbExploringTotal.Text + "," 
                                 + totalDeaths + "," + tbTotalHunted.Text + "," + tbTotalHuntedPlayers.Text + "," + tbTotalConObj.Text;
            File.AppendAllText(fileOutputPath.Text, sessionInfo + Environment.NewLine);

            var count = File.ReadLines(fileOutputPath.Text).Count();

            outputMessageToUser("Outputed successfully. That was the file " + count + " entry.");
        }

        private void sessionParser(string[] header, string[] dataEntries)
        {
            int size = dataEntries.Length - 1;
            char[] delimiterChars = { ',', ';' };

            tbSessionID.Text = header[1];
            tbPlayerName.Text = header[2];
            tbTotalEvents.Text = size.ToString();

            int[] biomesList = new int[size];

            double  distance        = 0;       
            float   sessionLenght   = 0;
            bool    firstEntry      = true;
            double[] oldPosition    = new double[3];
            int     deathsByStarve  = 0;
            int     deathsByHealth  = 0;
            int     exploringEvents = 0;

            List<int> uniqueBiomeValues         = new List<int>();

            List<string> minedMaterials         = new List<string>();
            List<string> miningEquipment        = new List<string>();
            List<string> harvestingEquipment    = new List<string>();
            List<string> harvestedObj           = new List<string>();
            List<string> buildingMaterials      = new List<string>();
            List<string> craftedObjects         = new List<string>();
            List<string> plantedSeeds           = new List<string>();
            List<string> huntedCreatures        = new List<string>();
            List<string> huntedPlayers          = new List<string>();
            List<string> consumedCateg          = new List<string>();
            List<string> consumedObj            = new List<string>();
            List<string> deathEvents            = new List<string>();

            //parse data for each entry
            for (int i = 0; i < size; i++)
            {
                if (dataEntries[i] != null)
                {
                    string[] dataPoint = dataEntries[i].Split(delimiterChars);

                    //generate list of unique biomes
                    if (!uniqueBiomeValues.Contains(System.Convert.ToInt32(dataPoint[4])))
                        uniqueBiomeValues.Add(System.Convert.ToInt32(dataPoint[4]));

                    //info to calculate total time
                    if (i == 0) sessionLenght = System.Convert.ToInt32(dataPoint[1]);
                    if (i == size - 1) sessionLenght = System.Convert.ToInt32(dataPoint[1]) - sessionLenght;

                    //events position
                    double[] position = new double[3] { System.Convert.ToDouble(dataPoint[5]), System.Convert.ToDouble(dataPoint[6]), System.Convert.ToDouble(dataPoint[7])};
                    eventsPositions.Add(position);

                    //calculate distance between positions
                    if (!firstEntry) { distance = distance + Math.Sqrt(Math.Pow((position[0] - oldPosition[0]), 2) + Math.Pow((position[1] - oldPosition[1]), 2) + Math.Pow((position[2] - oldPosition[2]), 2)); }
                    else { firstEntry = false; }

                    oldPosition[0] = position[0];
                    oldPosition[1] = position[1];
                    oldPosition[2] = position[2];

                    allTraveledBiomes.Add(System.Convert.ToInt32(dataPoint[4]));
                    eventsActionsID.Add(System.Convert.ToInt32(dataPoint[0]));
                    eventsTimes.Add(System.Convert.ToSingle(dataPoint[1]));

                    //Populate all the lists according to actions ID
                    //0 Action ID, 1 Date, 2 Sub Action, 3 Unique Value, 4 Biome, 5 X, 6 Y, 7 Z
                    if (System.Convert.ToInt16(dataPoint[0]) == 1)
                    {
                        minedMaterials.Add(dataPoint[3]);
                        miningEquipment.Add(dataPoint[2]);
                    }
                    if (System.Convert.ToInt16(dataPoint[0]) == 2)
                    {
                        buildingMaterials.Add(dataPoint[3]);
                    }
                    if (System.Convert.ToInt16(dataPoint[0]) == 3)
                    {
                        harvestingEquipment.Add(dataPoint[2]);
                        harvestedObj.Add(dataPoint[3]);
                    }
                    if (System.Convert.ToInt16(dataPoint[0]) == 4)
                    {
                        craftedObjects.Add(dataPoint[3]);
                    }
                    if (System.Convert.ToInt16(dataPoint[0]) == 5)
                    {
                        plantedSeeds.Add(dataPoint[3]);
                    }
                    if (System.Convert.ToInt16(dataPoint[0]) == 6)
                    {
                        exploringEvents++;
                    }
                    if (System.Convert.ToInt16(dataPoint[0]) == 7)
                    {
                        deathEvents.Add(dataPoint[2]);
                        if (dataPoint[2] == "Starved") deathsByStarve++;
                        else deathsByHealth++;
                    }
                    if (System.Convert.ToInt16(dataPoint[0]) == 8)
                    {
                        if(dataPoint[2] == "Fight") { huntedPlayers.Add(dataPoint[3]); }
                        else { huntedCreatures.Add(dataPoint[3]); }
                    }
                    if (System.Convert.ToInt16(dataPoint[0]) == 9)
                    {
                        consumedCateg.Add(dataPoint[2]);
                        consumedObj.Add(dataPoint[3]);
                    }
                }
            }

            totalDeaths = deathsByHealth + deathsByStarve;

            //call function to order and print lists
            printListsScreen(minedMaterials, lbMinMat, tbTotalMinedMat);
            printListsScreen(miningEquipment, lbMinEquip, tbTotalMinEquip);
            printListsScreen(harvestedObj, lbHarvObj, tbTotalHavObj);
            printListsScreen(harvestingEquipment, lbHarvEquip, tbTotalHavEquip);
            printListsScreen(buildingMaterials, lbBuildMat, tbTotalBuildMat);
            printListsScreen(craftedObjects, lbCraftObj, tbTotalCrafted);
            printListsScreen(huntedCreatures, lbHunted, tbTotalHunted);
            printListsScreen(plantedSeeds, lbPlantedSeed, tbTotalPlanted);
            printListsScreen(consumedCateg, lbConsumCat, tbTotalConCat);
            printListsScreen(consumedObj, lbConsumObj, tbTotalConObj);
            printListsScreen(huntedPlayers, lbHuntedPlayers, tbTotalHuntedPlayers);

            //fill status values
            int uniqueBiomes = uniqueBiomeValues.Count;
            tbUniqueBiomes.Text = uniqueBiomes.ToString();

            tbSessionLenght.Text = sessionLenght.ToString();
            tbTraveledDist.Text = distance.ToString();
            tbAvgWalkSpeed.Text = (distance/sessionLenght).ToString();

            tbExploringTotal.Text = exploringEvents.ToString();
            tbDeathHealth.Text = deathsByHealth.ToString();
            tbDeathStarved.Text = deathsByStarve.ToString();

            //activate other buttons
            AddToOutputButton.IsEnabled = true;
            ActionsPieChartButton.IsEnabled = true;
            ActionsTimeGraphButton.IsEnabled = true;
            PDFReportButton.IsEnabled = true;

        }

        private void printListsScreen(List<string> list, ListBox listToPrint, TextBlock listTotal)
        {
            var q1 = list.GroupBy(x => x)
                    .Select(g => new { Value = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count);

            foreach (var x in q1) { listToPrint.Items.Add(x.Value + ": " + x.Count); }

            listTotal.Text = list.Count().ToString();
        }

        private void ClickAddFile(object sender, RoutedEventArgs e)
        {
            outputSessionToTextFile();
        }

        private void ClickCreateReport(object sender, RoutedEventArgs e)
        {

        }

        private void ClickActionsOverTime(object sender, RoutedEventArgs e)
        {
            GraphWindow win = new GraphWindow();
            win.Show();
        }

        private void ClickActionsDistribution(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnToMainButton(object sender, RoutedEventArgs e)
        {
            ModeSelectWindow win = new ModeSelectWindow();
            win.Show();
            this.Close();
        }

    }
}
