using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Configuration;

using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace WpfApp3
{

    /// <summary>
    /// Interaction logic for ReportLoad.xaml
    /// </summary>
    public partial class ReportLoad : Page
    {
        string ConnectDB = ConfigurationManager.ConnectionStrings["Dbcon"].ConnectionString;
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public ReportLoad()
        {
            InitializeComponent();
            LoadGrid();
        }
        public void LoadGrid()
        {
            SqlConnection con = new SqlConnection(ConnectDB);
            SqlCommand cmd = new SqlCommand("SELECT p.Name, p.Mobile, I.InvoiceID, I.InvDate\r\nFROM PatientInfo p\r\nJOIN Invoice I ON p.PatientID = I.PatientID;", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgReportLoad.ItemsSource = dt.DefaultView;

        }

        private void PrintClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Page2());
        }
    }
}
