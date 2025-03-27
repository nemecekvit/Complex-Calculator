using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Complex_Number_Calculator_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string log = "";
        private const string defaultInputText = "Please input you expression";
        public void Log(string s)
        {
            log += s + "\n";
            tbLog.Text = log;
        }

        public MainWindow()
        {
            InitializeComponent();
            tbInputBox.Text = defaultInputText;
            /*
            FileManager fileManager = new FileManager();
            

            string ip = "([1i45] + [0i0])*2";

            
            cmplxNum result = Evaluator.Evaluate(ip);
            ResultTextBlock.Text = result.ToString();
            
            string[] cache= new string[] {result.ToString(),result.ToString(), result.ToString() };
            fileManager.saveExpressionToCVS(cache);

            string[] cache2 = fileManager.loadSavedExpressionsFromCVS();
            ResultTextBlock2.Text = cache2[0];
            ResultTextBlock3.Text = cache2[1];
            ResultTextBlock4.Text = cache2[2];
            */


            /*
            cmplxNum vysledek = cislo1 + cislo2;
            cmplxNum vysledek1 = cislo1 - cislo2;
            cmplxNum vysledek2 = cislo1 * cislo2;
            cmplxNum vysledek3 = cislo1 / cislo2;
            cmplxNum vysledek4 = cislo1 * cislo2;
            cmplxNum vysledek5 = -cislo1;
            double vysledek6 = cislo1.Abs();
            ResultTextBlock.Text = vysledek.ToString();
            ResultTextBlock2.Text = vysledek1.ToString();
            ResultTextBlock3.Text = vysledek2.ToString();
            ResultTextBlock4.Text = vysledek3.ToString();
            ResultTextBlock5.Text = vysledek4.ToString();
            ResultTextBlock6.Text = vysledek5.ToString();
            ResultTextBlock7.Text = vysledek6.ToString();
            */
        }



        //Input box text change events

        private void tbInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbInputBox.Text != defaultInputText)
            {
                tbInputBox.Foreground = new SolidColorBrush(Colors.Black);
            }

        }
        private void tbInputBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbInputBox.Text == defaultInputText)
            {
                tbInputBox.Text = "";
                tbInputBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
        private void tbInputBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbInputBox.Text))
            {
                tbInputBox.Text = defaultInputText;
                tbInputBox.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }
        //Show/Hide log button
        private void cbShowLog_Click(object sender, RoutedEventArgs e)
        {
            if (tbLog.Visibility == Visibility.Visible)
            {
                tbLog.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbLog.Visibility = Visibility.Visible;
            }
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            lbResultOutput.Content = "";
            try
            {
                lbResultOutput.Content = Evaluator.Evaluate(tbInputBox.Text).ToString();
            }
            catch (Exception f)
            {

                lbErrorOutput.Content = f.Message;
            }

        }
        /* Window closing event for possible data loss prevention
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Closing called");

            // If data is dirty, notify user and ask for a response
            if (true)
            {
                string msg = "Data is dirty. Close without saving?";
                MessageBoxResult result =MessageBox.Show(msg,"Data App",MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                }
            }
        }
        */
    }
}