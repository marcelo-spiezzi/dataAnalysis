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
        List<List<uniqueEventValues>> allEventsBySessions = new List<List<uniqueEventValues>>();

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

        public struct uniqueEventValues
        {
            public int sessionID;
            public string name;
            public int type;
            public string date;
            public double deltaTime;
            public string subAction;
            public string uniqueValue;
            public int biome;
            public double x;
            public double y;
            public double z;
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
            allEventsBySessions.Clear();

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

            displayAllSessionsOnViewer(-1);

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

            return dataEntries;
        }

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

            for (int y = 0; y <= numberOfSessions - 1; y++)
            {
                List<uniqueEventValues> thisSessionEvents = new List<uniqueEventValues>();

                for (int z = 0; z < dataEntries.Length; z++)
                {
                    string[] dataPoint = dataEntries[z].Split(delimiterChars);

                    uniqueEventValues a = new uniqueEventValues();
                    a.sessionID = System.Convert.ToInt16(dataPoint[0]);
                    a.name = dataPoint[1];
                    a.type = System.Convert.ToInt16(dataPoint[2]);
                    a.date = dataPoint[3];
                    a.deltaTime = System.Convert.ToDouble(dataPoint[4]);
                    a.subAction = dataPoint[5];
                    a.uniqueValue = dataPoint[6];
                    a.biome = System.Convert.ToInt16(dataPoint[7]);
                    a.x = System.Convert.ToDouble(dataPoint[8]);
                    a.y = System.Convert.ToDouble(dataPoint[9]);
                    a.z = System.Convert.ToDouble(dataPoint[10]);

                    if (a.sessionID == y)
                    {
                        thisSessionEvents.Add(a);
                    }
                }
                allEventsBySessions.Add(thisSessionEvents);
            }

            outputMessageToUser("Import successful. There were " + dataEntries.Length + " events in " + numberOfSessions + " unique sessions");
        }

        private void outputMessageToUser(string msg)
        {
            ImportMessage.Text = msg;
        }       

        //export all sessions statistcs to output file. Session analyzes is made by function sessionParser()
        private void outputSessionToTextFile()
        {
            //time to analyze all sessions and do the calculations for all of them 
            foreach (var subList in allEventsBySessions)
            {
                eventOverallValues sessionValues = new eventOverallValues();
                sessionValues = sessionSummary(subList);

                string sessionInfo = sessionValues.sessionID + "," + sessionValues.playerName + "," + sessionValues.sessionLenght + "," + sessionValues.numberEvents + "," +
                     sessionValues.uniqueBiomes + "," + sessionValues.traveledDistance + "," + sessionValues.miningEvents + "," + sessionValues.buildingEvents + "," +
                     sessionValues.harvestingEvents + "," + sessionValues.craftingEvents + "," + sessionValues.farmingEvents + "," + sessionValues.exploringEvents + ","
                     + sessionValues.deathsEvents + "," + sessionValues.huntEvents + "," + sessionValues.fightEvents + "," + sessionValues.consumableEvents;
                File.AppendAllText(fileOutputPath.Text, sessionInfo + Environment.NewLine);
            }

            var count = File.ReadLines(fileOutputPath.Text).Count();
            outputMessageToUser("Outputed successfully. Added " + allEventsBySessions.Count() + " sessions. File has now " + count + " sessions.");
        }

        private eventOverallValues sessionSummary(List<uniqueEventValues> sessionList)
        {
            eventOverallValues sessionValues = new eventOverallValues();

            bool    firstEntry      = true;
            double[] oldPosition    = new double[3];
            int     counter         = 0;

            List<int> uniqueBiomeValues         = new List<int>();

            //parse data for each entry
            foreach (var value in sessionList)
            {
                uniqueEventValues dataPoint = new uniqueEventValues();
                dataPoint = value;

                sessionValues.numberEvents++;

                //generate list of unique biomes
                if (!uniqueBiomeValues.Contains(dataPoint.biome))
                    uniqueBiomeValues.Add(dataPoint.biome);

                //info to calculate total time
                if (counter == 0) sessionValues.sessionLenght = dataPoint.deltaTime;
                if (counter == sessionList.Count() - 1) sessionValues.sessionLenght += dataPoint.deltaTime;
                counter++;

                //events position
                double[] position = new double[3] { dataPoint.x, dataPoint.y, dataPoint.z };

                //calculate distance between positions
                if (!firstEntry) 
                {
                    sessionValues.traveledDistance += Math.Sqrt((position[0] - oldPosition[0])*(position[0] - oldPosition[0]) + 
                                                                (position[1] - oldPosition[1])*(position[1] - oldPosition[1]) + 
                                                                (position[2] - oldPosition[2])*(position[2]- oldPosition[2])); 
                }
                else 
                {
                    sessionValues.playerName = dataPoint.name;
                    sessionValues.sessionID = dataPoint.sessionID;
                    firstEntry = false; 
                }

                oldPosition[0] = position[0];
                oldPosition[1] = position[1];
                oldPosition[2] = position[2];                

                //Populate all the lists according to actions ID
                //0 session id, 1 player name, 2 action ID, 3 date, 4 deltaTime, 5 sub action, 6 unique value, 7 biome, 8 x, 9 y, 10 z
                if (dataPoint.type == 1) { sessionValues.miningEvents++; }
                if (dataPoint.type == 2) { sessionValues.buildingEvents++; }
                if (dataPoint.type == 3) { sessionValues.harvestingEvents++; }
                if (dataPoint.type == 4) { sessionValues.craftingEvents++; }
                if (dataPoint.type == 5) { sessionValues.farmingEvents++; }
                if (dataPoint.type == 6) { sessionValues.exploringEvents++; }
                if (dataPoint.type == 7) { sessionValues.deathsEvents++; }
                if (dataPoint.type == 8)
                {
                    if(dataPoint.subAction == "Fight") { sessionValues.fightEvents++; }
                    else { sessionValues.huntEvents++; }
                }
                if (dataPoint.type == 9) { sessionValues.consumableEvents++; }
            }

            sessionValues.uniqueBiomes = uniqueBiomeValues.Count();

            return sessionValues;
        }
       
        //Display session(s) statistcs on viewer according to session ID. -1 all sessions.
        private void displayAllSessionsOnViewer(int sessionID)
        {
            List<string> miningEquipment = new List<string>();
            List<string> minedMaterials = new List<string>();
            List<string> buildingMaterials = new List<string>();
            List<string> harvestingEquipment = new List<string>();
            List<string> harvestedObj = new List<string>();
            List<string> craftedObjects = new List<string>();
            List<string> plantedSeeds = new List<string>();
            List<string> deathEvents = new List<string>();
            List<string> huntedPlayers = new List<string>();
            List<string> huntedCreatures = new List<string>();
            List<string> consumedCateg = new List<string>();
            List<string> consumedObj = new List<string>();

            int exploringEvents = 0;
            int deathsByStarve = 0;
            int deathsByHealth = 0;
            string playerName = "";
            int sID = 0;

            emptyData();

            foreach (var subList in allEventsBySessions)
            {
                foreach(var a in subList)
                {
                    uniqueEventValues dataPoint = new uniqueEventValues();
                    dataPoint = a;

                    //add to list only the selected sessionID or all if session ID == -1
                    if (dataPoint.sessionID == sessionID || sessionID == -1)
                    {
                        playerName = dataPoint.name;
                        sID = dataPoint.sessionID;

                        //Populate all the lists according to actions ID
                        if (dataPoint.type == 1)
                        {
                            miningEquipment.Add(dataPoint.subAction);
                            minedMaterials.Add(dataPoint.uniqueValue);
                        }
                        if (dataPoint.type == 2)
                        {
                            buildingMaterials.Add(dataPoint.uniqueValue);
                        }
                        if (dataPoint.type == 3)
                        {
                            harvestingEquipment.Add(dataPoint.subAction);
                            harvestedObj.Add(dataPoint.uniqueValue);
                        }
                        if (dataPoint.type == 4)
                        {
                            craftedObjects.Add(dataPoint.subAction);
                        }
                        if (dataPoint.type == 5)
                        {
                            plantedSeeds.Add(dataPoint.uniqueValue);
                        }
                        if (dataPoint.type == 6)
                        {
                            exploringEvents++;
                        }
                        if (dataPoint.type == 7)
                        {
                            deathEvents.Add(dataPoint.uniqueValue);
                            if (dataPoint.subAction == "Starved") deathsByStarve++;
                            else deathsByHealth++;
                        }
                        if (dataPoint.type == 8)
                        {
                            if (dataPoint.subAction == "Fight") { huntedPlayers.Add(dataPoint.uniqueValue); }
                            else { huntedCreatures.Add(dataPoint.uniqueValue); }
                        }
                        if (dataPoint.type == 9)
                        {
                            consumedCateg.Add(dataPoint.subAction);
                            consumedObj.Add(dataPoint.uniqueValue);
                        }
                    }
                }
            }

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
            //int uniqueBiomes = uniqueBiomeValues.Count;
            //tbUniqueBiomes.Text = uniqueBiomes.ToString();

            //tbSessionLenght.Text = sessionLenght.ToString();
            //tbTraveledDist.Text = distance.ToString();
            //tbAvgWalkSpeed.Text = (distance / sessionLenght).ToString();

            if (sessionID == -1)
            {
                tbPlayerName.Text = "all";
                tbSessionID.Text = "all";
            }
            else
            {
                tbPlayerName.Text = playerName;
                tbSessionID.Text = sID.ToString();
            }

            tbExploringTotal.Text = exploringEvents.ToString();
            tbDeathHealth.Text = deathsByHealth.ToString();
            tbDeathStarved.Text = deathsByStarve.ToString();
        }

        //not in use now, should be used with displayAllSessionsOnViewer()
        private void printListsScreen(List<string> list, ListBox listToPrint, TextBox listTotal)
        {
            var q1 = list.GroupBy(x => x)
                    .Select(g => new { Value = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count);

            foreach (var x in q1) { listToPrint.Items.Add(x.Value + ": " + x.Count); }

            listTotal.Text = list.Count().ToString();
        }


        // -------- ENABLE AND DISABLE ALL FILTERS ----------- //

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

        private void tbSessionID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text == "all")
            {
                displayAllSessionsOnViewer(-1);
                return;
            }
            if (IsAllDigits((sender as TextBox).Text) && tbSessionID.IsEnabled) { displayAllSessionsOnViewer(System.Convert.ToInt16((sender as TextBox).Text)); }
        }


    }
}
