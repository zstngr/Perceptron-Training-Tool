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

namespace Perceptron_Training_Tool
{
    /// <summary>
    /// Логика взаимодействия для TrainWindow.xaml
    /// </summary>
    public partial class TrainWindow : Window
    {
        public List<string> TrainTypes;

        public TrainWindow()
        {
            InitializeComponent();
            TrainTypes = new List<string> { "Epoch Train", "Error Train"};
            SelectTypeBox.ItemsSource = TrainTypes;
        }

        private void EpochBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void ErrorBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Train_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SelectTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox selectTypeBox = (ComboBox)sender;
            if (selectTypeBox.SelectedItem.ToString() == "Epoch Train")
            {
                ErrorBox.IsEnabled = false;
                ErrorBox.Text = "0";
                EpochBox.IsEnabled = true;
            }
            else if (selectTypeBox.SelectedItem.ToString() == "Error Train")
            {
                EpochBox.IsEnabled = false;
                EpochBox.Text = "0";
                ErrorBox.IsEnabled = true;
            }
        }

        private void SelectActivationTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
