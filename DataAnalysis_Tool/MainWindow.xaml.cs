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

namespace DataAnalysis_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<List<string>> allEventsBySession = new List<List<string>>();

        char[] delimiterChars = { ',', ';' };

        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

        bool firstEntry = true;

        public struct eventOverallValues
        {
            public int sessionID;
            public string playerName;
            public double sessionLenght;
            public int numberEvents;
            public int uniqueBiomes;
            public double traveledDistance;
            public int miningEvents;
            public int buildingEvents;
            public int harvestingEvents;
            public int craftingEvents;
            public int farmingEvents;
            public int exploringEvents;
            public int deathsEvents;
            public int huntEvents;
            public int fightEvents;
            public int consumableEvents;
        } 

        public MainWindow()
        {
            InitializeComponent();
        }
        

        // ----------------- BUTTONS CALLS ------------------ //

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

        private void ButtonAnalyze(object sender, RoutedEventArgs e)
        {
            if (firstEntry)
            {
                parseInputBySession();
                firstEntry = false;
            }
            else
            {
                emptyData();
                parseInputBySession();
            }

            //activate other buttons
            AddToOutputButton.IsEnabled = true;
            ActionsPieChartButton.IsEnabled = true;
            ActionsTimeGraphButton.IsEnabled = true;
            PDFReportButton.IsEnabled = true;
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

        // ----------------- OTHER FUNCTIONS ------------------ //

        //analyze all events in a file loaded at analyzeFile() and add them to lists based on their unique IDs
        private void parseInputBySession()
        {        
            //get the file, create a list AllEntriesList with all entries by line 
            string[] dataEntries = analyzeFile();

            //calculate the number of unique sessions
            int oldSessionID = -1;
            int numberOfSessions = 0;
           
            for(int i = 0 ; i < dataEntries.Length - 1 ; i++)
            {
                string[] dataPoint = dataEntries[i].Split(delimiterChars);
                if (System.Convert.ToInt16(dataPoint[0]) != oldSessionID)
                {
                    numberOfSessions++;
                    oldSessionID = System.Convert.ToInt16(dataPoint[0]);
                }
            }

            //populate a list of lists with the the data points according to the session ID
            //parent list = sessions ; child list = session events
            for (int y = 0; y <= numberOfSessions - 1; y++)
            {
                List<string> thisSessionEvents = new List<string>();

                for(int z = 0; z < dataEntries.Length ; z++)
                {                   
                    string[] dataPoint = dataEntries[z].Split(delimiterChars);
                    if (System.Convert.ToInt16(dataPoint[0]) == y)
                    {
                        thisSessionEvents.Add(dataEntries[z]);
                    }              
                }
                allEventsBySession.Add(thisSessionEvents);
            }          
        }

        private void outputMessageToUser(string msg)
        {
            ImportMessage.Text = msg;
        }

        private void emptyData()
        {
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

            outputMessageToUser("Import successful. There were " + counter + " events.");

            return dataEntries;
        }

        //export all sessions statistcs to output file. Session analyzes is made by function sessionParser()
        private void outputSessionToTextFile()
        {
            //time to analyze all sessions and do the calculations for all of them 
            foreach (var subList in allEventsBySession)
            {
                eventOverallValues sessionValues = new eventOverallValues();
                sessionValues = sessionsParser(subList);

                string sessionInfo = sessionValues.sessionID + "," + sessionValues.playerName + "," + sessionValues.sessionLenght + "," + sessionValues.numberEvents + "," +
                     sessionValues.uniqueBiomes + "," + sessionValues.traveledDistance + "," + sessionValues.miningEvents + "," + sessionValues.buildingEvents + "," +
                     sessionValues.harvestingEvents + "," + sessionValues.craftingEvents + "," + sessionValues.farmingEvents + "," + sessionValues.exploringEvents + ","
                     + sessionValues.deathsEvents + "," + sessionValues.huntEvents + "," + sessionValues.fightEvents + "," + sessionValues.consumableEvents;
                File.AppendAllText(fileOutputPath.Text, sessionInfo + Environment.NewLine);
            }

            var count = File.ReadLines(fileOutputPath.Text).Count();
            outputMessageToUser("Outputed successfully. Added" + allEventsBySession.Count() + " events. File has now " + count + " events.");
        }

        private eventOverallValues sessionsParser(List<string> sessionList)
        {
            eventOverallValues sessionValues = new eventOverallValues();

            bool    firstEntry      = true;
            double[] oldPosition    = new double[3];
            int     counter         = 0;

            List<int> uniqueBiomeValues         = new List<int>();

            //parse data for each entry
            foreach (var value in sessionList)
            {
                string[] dataPoint = value.Split(delimiterChars);

                sessionValues.numberEvents++;

                //generate list of unique biomes
                if (!uniqueBiomeValues.Contains(System.Convert.ToInt32(dataPoint[7])))
                    uniqueBiomeValues.Add(System.Convert.ToInt32(dataPoint[7]));

                //info to calculate total time
                if (counter == 0) sessionValues.sessionLenght = System.Convert.ToInt32(dataPoint[4]);
                if (counter == sessionList.Count() - 1) sessionValues.sessionLenght = System.Convert.ToInt32(dataPoint[4]) - sessionValues.sessionLenght;
                counter++;

                //events position
                double[] position = new double[3] { System.Convert.ToDouble(dataPoint[8]), System.Convert.ToDouble(dataPoint[9]), System.Convert.ToDouble(dataPoint[10])};

                //calculate distance between positions
                if (!firstEntry) 
                {
                    sessionValues.traveledDistance += Math.Sqrt((position[0] - oldPosition[0])*(position[0] - oldPosition[0]) + 
                                                                (position[1] - oldPosition[1])*(position[1] - oldPosition[1]) + 
                                                                (position[2] - oldPosition[2])*(position[2]- oldPosition[2])); 
                }
                else 
                {
                    sessionValues.playerName = dataPoint[1];
                    sessionValues.sessionID = System.Convert.ToInt16(dataPoint[0]);
                    firstEntry = false; 
                }

                oldPosition[0] = position[0];
                oldPosition[1] = position[1];
                oldPosition[2] = position[2];                

                //Populate all the lists according to actions ID
                //0 session id, 1 player name, 2 action ID, 3 date, 4 deltaTime, 5 sub action, 6 unique value, 7 biome, 8 x, 9 y, 10 z
                if (System.Convert.ToInt16(dataPoint[2]) == 1) { sessionValues.miningEvents++; }
                if (System.Convert.ToInt16(dataPoint[2]) == 2) { sessionValues.buildingEvents++; }
                if (System.Convert.ToInt16(dataPoint[2]) == 3) { sessionValues.harvestingEvents++; }
                if (System.Convert.ToInt16(dataPoint[2]) == 4) { sessionValues.craftingEvents++; }
                if (System.Convert.ToInt16(dataPoint[2]) == 5) { sessionValues.farmingEvents++; }
                if (System.Convert.ToInt16(dataPoint[2]) == 6) { sessionValues.exploringEvents++; }
                if (System.Convert.ToInt16(dataPoint[2]) == 7) { sessionValues.deathsEvents++; }
                if (System.Convert.ToInt16(dataPoint[2]) == 8)
                {
                    if(dataPoint[2] == "Fight") { sessionValues.fightEvents++; }
                    else { sessionValues.huntEvents++; }
                }
                if (System.Convert.ToInt16(dataPoint[2]) == 9) { sessionValues.consumableEvents++; }
            }

            sessionValues.uniqueBiomes = uniqueBiomeValues.Count();

            return sessionValues;
        }

        //not in use now, will be used to display a specific session in the viewer
        private void displaySessionOnViewer()
        {
            /*
            allTraveledBiomes.Add(System.Convert.ToInt32(dataPoint[7]));
            eventsActionsID.Add(System.Convert.ToInt32(dataPoint[2]));
            eventsTimes.Add(System.Convert.ToSingle(dataPoint[3]));


            //Populate all the lists according to actions ID
            //0 session id, 1 player name, 2 action ID, 3 date, 4 deltaTime, 5 sub action, 6 unique value, 7 biome, 8 x, 9 y, 10 z
            if (System.Convert.ToInt16(dataPoint[2]) == 1)
            {
                miningEquipment.Add(dataPoint[5]);
                minedMaterials.Add(dataPoint[6]);
            }
            if (System.Convert.ToInt16(dataPoint[2]) == 2)
            {
                buildingMaterials.Add(dataPoint[6]);
            }
            if (System.Convert.ToInt16(dataPoint[2]) == 3)
            {
                harvestingEquipment.Add(dataPoint[5]);
                harvestedObj.Add(dataPoint[6]);
            }
            if (System.Convert.ToInt16(dataPoint[2]) == 4)
            {
                craftedObjects.Add(dataPoint[6]);
            }
            if (System.Convert.ToInt16(dataPoint[2]) == 5)
            {
                plantedSeeds.Add(dataPoint[6]);
            }
            if (System.Convert.ToInt16(dataPoint[2]) == 6)
            {
                exploringEvents++;
            }
            if (System.Convert.ToInt16(dataPoint[2]) == 7)
            {
                deathEvents.Add(dataPoint[6]);
                if (dataPoint[5] == "Starved") deathsByStarve++;
                else deathsByHealth++;
            }
            if (System.Convert.ToInt16(dataPoint[2]) == 8)
            {
                if (dataPoint[2] == "Fight") { huntedPlayers.Add(dataPoint[3]); }
                else { huntedCreatures.Add(dataPoint[6]); }
            }
            if (System.Convert.ToInt16(dataPoint[2]) == 9)
            {
                consumedCateg.Add(dataPoint[5]);
                consumedObj.Add(dataPoint[6]);
            }

            //call function to order and print lists
            /*printListsScreen(minedMaterials, lbMinMat, tbTotalMinedMat);
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
            tbAvgWalkSpeed.Text = (distance / sessionLenght).ToString();

            tbExploringTotal.Text = exploringEvents.ToString();
            tbDeathHealth.Text = deathsByHealth.ToString();
            tbDeathStarved.Text = deathsByStarve.ToString();*/
        }

        //not in use now, should be used with displaySessionOnViewer()
        private void printListsScreen(List<string> list, ListBox listToPrint, TextBlock listTotal)
        {
            var q1 = list.GroupBy(x => x)
                    .Select(g => new { Value = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count);

            foreach (var x in q1) { listToPrint.Items.Add(x.Value + ": " + x.Count); }

            listTotal.Text = list.Count().ToString();
        }

    }
}
