using System.Buffers;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        private const string defaultInputText = "";
        private bool isDarkTheme = true;

        // Timer for validating input after user stops typing
        static int timerInterval = 2000;
        System.Threading.Timer timer = null;

        // FrontBackConnector for connecting the GUI with the backend
        private FrontBackConnector frontBackConnector;
        private memoryManager memoryManager = new memoryManager();
        private FileManager fileManager = new FileManager();

        private string[] loadedMemory = new string[3];
        private string[] currentMemory = new string[3];


        public MainWindow()
        {
            InitializeComponent();
            tbInputBox.Text = defaultInputText;
            frontBackConnector = new FrontBackConnector(this);

            loadMemoryFromFile();
        }

        // Theme Toggle
        private void BtnThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current;
            app.Resources.MergedDictionaries.Clear();

            if (isDarkTheme)
            {
                app.Resources.MergedDictionaries.Add(
                    new ResourceDictionary { Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative) });
            }
            else
            {
                app.Resources.MergedDictionaries.Add(
                    new ResourceDictionary { Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative) });
            }

            isDarkTheme = !isDarkTheme;
            UpdateLayout();
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
                {
                    SetErrorMessage("");
                    return;
                }
                    
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


        //--------------------------------------------------------------------------------
        bool firstEntry = true;
        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            lbResultOutput.Content = "";
            try
            {
                lbResultOutput.Content = frontBackConnector.evaluateExpression(tbInputBox.Text);
                if (firstEntry == true) { 
                tbLog.Text = tbLog.Text + tbInputBox.Text + " = " + frontBackConnector.evaluateExpression(tbInputBox.Text);
                firstEntry = false;
                } else
                {
                    tbLog.Text = tbLog.Text + "\n" + tbInputBox.Text + " = " + frontBackConnector.evaluateExpression(tbInputBox.Text);
                }
            }
            catch (Exception f)
            {

                lbErrorOutput.Content = f.Message;
            }

        }
        //--------------------------------------------------------------------------------
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tbInputBox.Text = tbInputBox.Text + "+";
            }
            catch (Exception f)
            {

                lbErrorOutput.Content = f.Message;
            }

        }
        //--------------------------------------------------------------------------------
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tbInputBox.Text = tbInputBox.Text + "-";
            }
            catch (Exception f)
            {

                lbErrorOutput.Content = f.Message;
            }

        }
        //--------------------------------------------------------------------------------
        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tbInputBox.Text = tbInputBox.Text + "*";
            }
            catch (Exception f)
            {

                lbErrorOutput.Content = f.Message;
            }

        }
        //--------------------------------------------------------------------------------
        private void btnDiv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tbInputBox.Text = tbInputBox.Text + "/";
            }
            catch (Exception f)
            {

                lbErrorOutput.Content = f.Message;
            }

        }
        //--------------------------------------------------------------------------------
        private void btnInsertRiI_Click(object sender, RoutedEventArgs e)
        {
            tbInputBox.Text += ComplexNumberHelper.FormatRiI(tbA.Text, tbB.Text);
        }

        private void btnInsertRjI_Click(object sender, RoutedEventArgs e)
        {
            tbInputBox.Text += ComplexNumberHelper.FormatRjI(tbA.Text, tbB.Text);
        }

        private void btnInsertPolar_Click(object sender, RoutedEventArgs e)
        {
            tbInputBox.Text += ComplexNumberHelper.FormatPolar(tbA.Text, tbB.Text);
        }
        //--------------------------------------------------------------------------------
        //Memory
        private void btnSaveM1_Click(object sender, RoutedEventArgs e)
        {
            if (lbResultOutput.Content == "")
            {
                lbErrorOutput.Content = "There is nothing to save in memory.";
            }
            else
            {
                memoryManager.Save(1, lbResultOutput.Content.ToString());
                lbActiveM1.Content = lbResultOutput.Content.ToString();
            }
        }

        private void btnLoadM1_Click(object sender, RoutedEventArgs e)
        {
            tbInputBox.Text += memoryManager.Load(1);
        }

        private void btnClearM1_Click(object sender, RoutedEventArgs e)
        {
            memoryManager.Clear(1);
            lbActiveM1.Content = "";
        }

        private void btnSaveM2_Click(object sender, RoutedEventArgs e)
        {
            if (lbResultOutput.Content == "")
            {
                lbErrorOutput.Content = "There is nothing to save in memory.";
            }
            else
            {
                memoryManager.Save(2, lbResultOutput.Content.ToString());
                lbActiveM2.Content = lbResultOutput.Content.ToString();
            }
        }

        private void btnLoadM2_Click(object sender, RoutedEventArgs e)
        {
            tbInputBox.Text += memoryManager.Load(2);
        }

        private void btnClearM2_Click(object sender, RoutedEventArgs e)
        {
            memoryManager.Clear(2);
            lbActiveM2.Content = "";
        }

        private void btnSaveM3_Click(object sender, RoutedEventArgs e)
        {
            if (lbResultOutput.Content == "")
            {
                lbErrorOutput.Content = "There is nothing to save in memory.";
            }
            else
            {
                memoryManager.Save(3, lbResultOutput.Content.ToString());
                lbActiveM3.Content = lbResultOutput.Content.ToString();
            }
        }

        private void btnLoadM3_Click(object sender, RoutedEventArgs e)
        {
            tbInputBox.Text += memoryManager.Load(3);
        }

        private void btnClearM3_Click(object sender, RoutedEventArgs e)
        {
            memoryManager.Clear(3);
            lbActiveM3.Content = "";
        }

        //--------------------------------------------------------------------------------
        //Load memory from file on start
        private void loadMemoryFromFile() {
            try 
            {
                loadedMemory = fileManager.loadLastMemory();

                for (int i = 0; i < 3; i++) {
                    loadedMemory[i] = loadedMemory[i].Where(c => !Char.IsWhiteSpace(c)).Aggregate("", (current, c) => current + c); //Remove whitespace
                    memoryManager.Save(i + 1, loadedMemory[i]);
                }

                lbActiveM1.Content = loadedMemory[0];
                lbActiveM2.Content = loadedMemory[1];
                lbActiveM3.Content = loadedMemory[2];

            }
            catch (Exception e)
            {
                lbErrorOutput.Content = e.Message;
            } 
        }

        private void saveMemoryToFile()
        {
            try
            {
                fileManager.saveToMemory(currentMemory);
            }
            catch (Exception e)
            {
                lbErrorOutput.Content = e.Message;
            }
        }


        //TODO: Implement that current result is saved to selected memory a,b,c



        // Window closing event for possible data loss prevention
        public bool compareMemory(string[] mem1, string[] mem2)
        {
            for (int i = 0; i < mem1.Length; i++)
            {
                if (mem1[i] != mem2[i])
                {
                    return false;
                }
            }
            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                currentMemory[i] = memoryManager.Load(i + 1);
            }

            if(compareMemory(currentMemory, loadedMemory))
            {
                saveMemoryToFile();
            }
            else
            {
                string msg = "Memory has changed. Do you want to save changes?";
                MessageBoxResult result = MessageBox.Show(msg, "Data App", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    saveMemoryToFile();
                }
            }
        }

        private void cbShowLog_Checked(object sender, RoutedEventArgs e)
        {
           LogBorder.Height = 150; // Bez animací
        }

        private void cbShowLog_Unchecked(object sender, RoutedEventArgs e)
        {
            LogBorder.Height = 0;
        }

    }
}