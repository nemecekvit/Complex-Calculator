using System.Reflection.Emit;
using System.Runtime.CompilerServices;
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

        // Timer for validating input after user stops typing
        static int timerInterval = 2000;
        System.Threading.Timer timer = null;

        // FrontBackConnector for connecting the GUI with the backend
        private FrontBackConnector frontBackConnector;
        public void Log(string s)
        {
            log += s + "\n";
            tbLog.Text = log;
        }
       
        public MainWindow()
        {
            InitializeComponent();
            tbInputBox.Text = defaultInputText;
            frontBackConnector = new FrontBackConnector(this);
        }

        //--------------------------------------------------------------------------------
        //Setters for error and result messages
        public void SetErrorMessage(string message)
        {
            lbErrorOutput.Content = message;
        }
        public void SetResultMessage(string message)
        {
            lbResultOutput.Content = message;
        }

        //--------------------------------------------------------------------------------
        //Input box text change events
        private void tbInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Color change
            if (tbInputBox.Text != defaultInputText)
            {
                tbInputBox.Foreground = new SolidColorBrush(Colors.Black);
            }

            //Timer for validating input after user stops typing
            DisposeTimer();
            timer = new System.Threading.Timer(TimerElapsed, null, timerInterval, timerInterval);
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

        //--------------------------------------------------------------------------------
        //Valide input string after user stoppes typing
        //Must execute in the main thread
        public void ValidateInput()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (tbInputBox.Text == defaultInputText)
                    SetErrorMessage("");

                try
                {
                    frontBackConnector.validateExpression(tbInputBox.Text);
                    SetErrorMessage("Syntax is in order");
                }
                catch (System.Exception e)
                {
                    SetErrorMessage(e.Message);
                }
            }
            ));
        }
        private void TimerElapsed(Object obj)
        {
            ValidateInput();
            DisposeTimer();
        }

        private void DisposeTimer()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        //--------------------------------------------------------------------------------
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

        //--------------------------------------------------------------------------------
        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            lbResultOutput.Content = "";
            try
            {
                lbResultOutput.Content = frontBackConnector.evaluateExpression(tbInputBox.Text);
            }
            catch (Exception f)
            {

                lbErrorOutput.Content = f.Message;
            }

        }
        //--------------------------------------------------------------------------------
        //Memory
        public void showMemory(string[] memory)
        {
            //TODO: Memory is array of strings with lenght 3
            //      Implement this method to show memory in the GUI
        }

        //TODO: Implement that current result is saved to selected memory a,b,c



        // Window closing event for possible data loss prevention
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*
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
            */
        }

    }
}