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
        public MainWindow()
        {
            InitializeComponent();
            cmplxNum cislo1 = new cmplxNum("[-12.5i34]");
            cmplxNum cislo2 = new cmplxNum("[12.5<34]");
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
        }
    }
}