using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Loginpage.xaml
    /// </summary>
    /// 
    public partial class Loginpage : Window
    {
        string ConnectDB = ConfigurationManager.ConnectionStrings["Dbcon"].ConnectionString;
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public Loginpage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(ConnectDB);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Username=@Username and Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password);

                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    if(txtUsername.Text=="Admin")
                    {
                        MainWindow dashboard = new MainWindow();
                        dashboard.Show();
                        this.Close();
                    }
                    else if(txtUsername.Text == "Reciptionist")
                    {
                        Reciptionist reciptionist = new Reciptionist();
                        reciptionist.Show();
                        this.Close();
                    }
                    else if (txtUsername.Text == "Accounts")
                    {
                        Reciptionist reciptionist = new Reciptionist();
                        reciptionist.Show();
                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Username or password is incorrect");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Loginpage.show();
        }

        private static void show()
        {
            throw new NotImplementedException();
        }
    }
}
