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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using PerceptronLibrary;
using OxyPlot;
using OxyPlot.Wpf;

namespace Perceptron_Training_Tool
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Perceptron network;
        int inputsCount;
        int outputsCount;
        double saturation;

        double[,] inSet = { { 1, 0 }, { 0, 1 }, { 1, 1 }, { 0, 0 } };
        double[,] outSet = { { 0, 1 }, { 0, 1 }, { 1, 1 }, { 0, 0 } };

        public IList<DataPoint> Points { get; private set; }
        public MainWindow()
        {
            //double[] input = { 0, 0.7 };
            //double[,] inSet = { { 1, 0 }, { 0, 1 }, { 1, 1 }, { 0, 0 } };
            //double[,] outSet = { { 0, 1 }, { 0, 1 }, { 1, 1 }, { 0, 0 } };

            //network.Train(inSet, outSet, 2000);
        }
        
        protected override void OnClosed(EventArgs e)
        {
            Console.Beep();
            base.OnClosed(e);
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
            System.Windows.Application.Current.Shutdown();
        }

        void ExportToPoints()
        {
            Points = new List<DataPoint>(network.ErrorPlot.Capacity);
            foreach (DataPlot dp in network.ErrorPlot)
            {
                Points.Add(new DataPoint(dp.Epoch, dp.Error));
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Text = ""; 
            try
            {
                double[] Input = Array.ConvertAll(InputBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), Double.Parse);
                foreach (double i in Input)
                {
                    if(i < 0 || i > 1)
                    {
                        MessageBox.Show("Use only float numbers between 0 and 1", "Invalid number");
                        return;
                    }
                    if(Input.Length != network.Size)
                    {
                        MessageBox.Show($"Your network uses only {network.Size} float numbers as input", "Invalid count of numbers");
                        return;
                    }
                }
                double[] output = network.calculateOutput(Input);
                for(int i = 0; i < output.Length; i++)
                {
                    output[i] = Math.Round(output[i], 3);
                    OutputBox.Text += output[i].ToString();
                    OutputBox.Text += " ";
                }
            }
            catch
            {
                MessageBox.Show($"Check the input", "Error");
            }
        }

        private void InputBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]*(?:\,[0-9]*)?$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void OpenChart_Click(object sender, RoutedEventArgs e)
        {
            ExportToPoints();
            GraphWindow graphWindow = new GraphWindow();
            LineSeries lineserie = new LineSeries
            {
                ItemsSource = Points,
                StrokeThickness = 2,
                Color = System.Windows.Media.Colors.Blue,
                LineStyle = LineStyle.Solid,
                MarkerType = MarkerType.None,
            };
            graphWindow.Plot.Series.Add(lineserie);
            graphWindow.Owner = this;
            graphWindow.Show();
        }

        private void CreateNeuralNetwork_Click(object sender, RoutedEventArgs e)
        {
            CreateNetworkWindow createNetworkWindow = new CreateNetworkWindow();
            createNetworkWindow.Owner = this;
            if (createNetworkWindow.ShowDialog() == true)
            {
                inputsCount = createNetworkWindow.inputsCount;
                outputsCount = createNetworkWindow.outputsCount;
                saturation = createNetworkWindow.saturation;
                network = new Perceptron(inputsCount, outputsCount);
            }
        }

        private void TrainingSetup_Click(object sender, RoutedEventArgs e)
        {
            TrainWindow trainWindow = new TrainWindow();
            if(trainWindow.ShowDialog() == true)
            {
                network.Train(inSet, outSet, 0.005);
            }
        }

        private void Train_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
