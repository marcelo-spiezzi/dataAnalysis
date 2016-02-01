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

namespace DataAnalysis_Tool
{
    /// <summary>
    /// Interaction logic for ModeSelectWindow.xaml
    /// </summary>
    public partial class ModeSelectWindow : Window
    {
        public ModeSelectWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OverallSessionsWindow win = new OverallSessionsWindow();
            win.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GraphWindow win = new GraphWindow();
            win.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GraphWindow win = new GraphWindow();
            win.Show();
            this.Close();
        }
    }
}
