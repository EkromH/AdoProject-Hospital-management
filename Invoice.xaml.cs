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
using System.Configuration;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using WpfApp3;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Page
    {

        string ConnectDB = ConfigurationManager.ConnectionStrings["Dbcon"].ConnectionString;
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public Invoice()
        {
            InitializeComponent();
            dateTimePicker.SelectedDate = DateTime.Today;
            dateTimePicker.IsEnabled = false;
            FillCombo();
            PatientIDCombo();
            LoadGridinv();
            InitializeSelectedProductsTable();
        }
        public void LoadGridinv()
        {
            SqlConnection con = new SqlConnection(ConnectDB);
            SqlCommand cmd = new SqlCommand("Select * from Invoice", con);
            dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            //dgInvoiceDisplay.ItemsSource = dt.DefaultView;

        }
        private void FillCombo()
        {

            con = new SqlConnection(ConnectDB);

            try
            {
                con.Open();
                string Query = "select * from TestList";
                SqlCommand sqlCommand = new SqlCommand(Query, con);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    string Name = reader.GetString(1);
                    comboboxT.Items.Add(Name);

                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PatientIDCombo()
        {
            using (SqlConnection con = new SqlConnection(ConnectDB))
            {
                try
                {
                    con.Open();
                    string Query = "SELECT PatientID FROM PatientInfo";
                    SqlCommand sqlCommand = new SqlCommand(Query, con);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {

                        int CustomerID = reader.GetInt32(0);
                    }

                    reader.Close();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }

        }

        public bool isValid()
        {
            if (txtInvoiceID.Text == String.Empty)
            {
                MessageBox.Show("Invoice is required", "Failed", MessageBoxButton.OK);
                return false;
            }
           
            if (comboboxT.SelectedItem == null)
            {
                MessageBox.Show("Select Test is required", "Failed", MessageBoxButton.OK);
                return false;
            }

            return true;
        }

        public int getTestID(string testName)
        {    int testID = 0;
            using (SqlConnection con = new SqlConnection(ConnectDB))
            {
            

                //testName = comboboxT.SelectedValue.ToString();
                con.Open();
                string query = "Select TestID from TestList where TestName=@TestName";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TestName", testName);

                object result = cmd.ExecuteScalar();

                // Check if the result is not null
                if (result != null && result != DBNull.Value)
                {
                    // Convert the result to integer
                    testID = Convert.ToInt32(result);
                    return testID;
                }
                return testID;

            }

        }


        

      
        private DataTable selectedProductsTable = new DataTable();
        private void InitializeSelectedProductsTable()
        {
            selectedProductsTable.Columns.Add("invoiceID", typeof(string));
            selectedProductsTable.Columns.Add("invDate", typeof(string));
            selectedProductsTable.Columns.Add("Name", typeof(string));
            selectedProductsTable.Columns.Add("TestID", typeof(string));

        }
 

        private void testAdd(object sender, RoutedEventArgs e)
        {
           

            string invoiceID = txtInvoiceID.Text;
            string Name = txtshowName.Text;
            string TestID = comboboxT.Text;
            string invoiceDate = dateTimePicker.SelectedDate.HasValue
                ? dateTimePicker.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss")
                : string.Empty;
          


            selectedProductsTable.Rows.Add(invoiceID, invoiceDate,Name, TestID);
            dgEntryDisplay.ItemsSource = selectedProductsTable.DefaultView;

            
        }
     
        private void InsertOrderDetail(int invoiceID, DateTime invDate, int patientID, int testID)
        {
            string query = "INSERT INTO Invoice (invoiceID,invDate, PatientID, TestID) VALUES (@InvoiceID, @InvoiceDate, @PatientID, @TestID)";

            SqlConnection con = new SqlConnection(ConnectDB);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
            cmd.Parameters.AddWithValue("@PatientID", patientID);
            cmd.Parameters.AddWithValue("@TestID", testID);
            cmd.Parameters.AddWithValue("@InvoiceDate", invDate);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting order detail: " + ex.Message);
            }


        }
        private void ClearInvoice()
        {
            txtInvoiceID.Clear();
            txtshowName.Clear();
            txtshowMobile.Clear();
            txtshowAge.Clear();
            txtshowAddress.Clear();
            comboboxT.SelectedItem=null;
        }

        private void ClickDG(object sender, RoutedEventArgs e)
        {
            int patientID = Insert(txtshowName.Text.ToString(), txtshowMobile.Text.ToString(), Convert.ToInt32(txtshowAge.Text), txtshowAddress.Text.ToString());

            foreach (DataRowView rowView in dgEntryDisplay.ItemsSource)
            {
                int invoiceID = Convert.ToInt32(txtInvoiceID.Text);

                DateTime invoiceDate = dateTimePicker.SelectedDate.Value;
                DataRow row = rowView.Row;
                int TestID = getTestID((string)row["TestID"]);
                InsertOrderDetail(invoiceID, invoiceDate,patientID,TestID);
                patientID++;
            
                
            }

            MessageBox.Show("Order confirmed successfully.");    selectedProductsTable.Clear();
            ClearInvoice();
        }


        //---------------------------------------------------------
        //=============================================================


        private int Insert(string name,string number,int age, string address)
        {
            int patientID = 0;
            try
            {
                
                SqlConnection con = new SqlConnection(ConnectDB);
                if (isValid1())
                {
                    SqlCommand cmd = new SqlCommand("Insert into PatientInfo (Name, Mobile,Age, Address) Values (@Name, @Number, @Age, @Address); SELECT SCOPE_IDENTITY();", con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Number", number);
                    cmd.Parameters.AddWithValue("@Age",age);
                    cmd.Parameters.AddWithValue("@Address", address);
                    con.Open();
                    patientID = cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Insert Successfully", "Saved", MessageBoxButton.OK);

                    return patientID;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return patientID;
        }

        public bool isValid1()
        {
            if (txtshowName.Text == String.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK);
                return false;
            }
            if (txtshowMobile.Text == String.Empty)
            {
                MessageBox.Show("Number name is required", "Failed", MessageBoxButton.OK);
                return false;
            }
            if (txtshowAge.Text == String.Empty)
            {
                MessageBox.Show("Age name is required", "Failed", MessageBoxButton.OK);
                return false;
            }
            if (txtshowAddress.Text == String.Empty)
            {
                MessageBox.Show("Address is required", "Failed", MessageBoxButton.OK);
                return false;
            }
            return true;
        }




    }
}

