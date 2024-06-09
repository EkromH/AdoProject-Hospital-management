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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void LoginP(object sender, RoutedEventArgs e)
        {
            Loginpage obj = new Loginpage();
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void PatientE(object sender, RoutedEventArgs e)
        {
            adMain.Content = new Patients();
        }

        private void EmpEntry(object sender, RoutedEventArgs e)
        {
            adMain.Content = new EmaployeeEntry();
        }

        private void Invoice_Click(object sender, RoutedEventArgs e)
        {
            adMain.Content = new Invoice();
        }

        private void EntryView(object sender, RoutedEventArgs e)
        {
            adMain.Content = new ReportLoad();
        }
    }
}
