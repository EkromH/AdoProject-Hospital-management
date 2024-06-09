using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Xml.Linq;
using System.Configuration;

namespace WpfApp3
{
    
    public partial class Patients : Page
    {
    string ConnectDB = ConfigurationManager.ConnectionStrings["Dbcon"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter adapter = new SqlDataAdapter();
    DataTable dt = new DataTable();
        public Patients()
        {
            InitializeComponent();
            LoadGrid();
        }
        //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=HMS_ADO;Integrated Security=True");

        public void LoadGrid()
        {
            SqlConnection con = new SqlConnection(ConnectDB);
            SqlCommand cmd = new SqlCommand("Select * from PatientInfo",con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgDisplay.ItemsSource = dt.DefaultView;

        }
       

        public bool isValid()
        {
            if (txtName.Text == String.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK);
                return false;
            }
            if (txtNumber.Text == String.Empty)
            {
                MessageBox.Show("Number name is required", "Failed", MessageBoxButton.OK);
                return false;
            }
            if (txtAge.Text == String.Empty)
            {
                MessageBox.Show("Age name is required", "Failed", MessageBoxButton.OK);
                return false;
            }
            if (txtAddress.Text == String.Empty)
            {
                MessageBox.Show("Address is required", "Failed", MessageBoxButton.OK);
                return false;
            }
            return true;
        }


        private void Insert(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectDB);
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("Insert into PatientInfo (Name, Mobile,Age, Address) Values (@Name, @Number, @Age, @Address)", con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Number", txtNumber.Text);
                    cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Insert Successfully", "Saved", MessageBoxButton.OK);

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectDB);
            try
            {
                con.Open();
                string Query = "delete from PatientInfo where PatientID='"+this.txtID.Text+"'";
                SqlCommand cmd =new SqlCommand(Query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted");
                con.Close();
                LoadGrid();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectDB);
            try
            {
                con.Open();
                string Query = "Update PatientInfo set  Name='"+this.txtName.Text+"', Mobile='"+this.txtNumber.Text+"', Age='"+this.txtAge.Text +"',Address='"+this.txtAddress.Text+ "' where PatientID='"+this.txtID.Text+"' ";
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update");
                con.Close();
                LoadGrid();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}

