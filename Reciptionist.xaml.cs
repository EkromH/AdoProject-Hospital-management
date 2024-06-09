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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for Reciptionist.xaml
    /// </summary>
    public partial class Reciptionist : Window
    {
        public Reciptionist()
        {
            InitializeComponent();
        }

        private void LoginRP(object sender, RoutedEventArgs e)
        {
            Loginpage obj = new Loginpage();
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void PatientEntry(object sender, RoutedEventArgs e)
        {
            Main.Content = new Patients();
        }

        private void Invoice_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Invoice();
        }
    }
}
