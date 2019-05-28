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
using System.Text.RegularExpressions;

namespace Perceptron_Training_Tool
{
    /// <summary>
    /// Логика взаимодействия для CreateNetworkWindow.xaml
    /// </summary>
    public partial class CreateNetworkWindow : Window
    {
        public int inputsCount;
        public int outputsCount;
        public double saturation;

        public CreateNetworkWindow()
        {
            InitializeComponent();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            try
            {
                inputsCount = Int32.Parse(InputsCountBox.Text);
                outputsCount = Int32.Parse(OutputsCountBox.Text);
                saturation = Double.Parse(SaturationBox.Text);
            }
            catch
            {
                MessageBox.Show("Check the input!", "Error", MessageBoxButton.OK);
            }
        }

        private void InputsCountBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[0-9]");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void OutputsCountBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[0-9]");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void SaturationBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]*(?:\,[0-9]*)?$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
