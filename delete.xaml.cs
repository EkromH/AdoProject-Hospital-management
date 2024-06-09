//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.Configuration;

//namespace WpfApp3
//{
//    / <summary>
//    / Interaction logic for delete.xaml
//    / </summary>
//    public partial class delete : Page
//    {
//        string ConnectDB = ConfigurationManager.ConnectionStrings["Dbcon"].ConnectionString;
//        SqlConnection con = new SqlConnection();
//        SqlCommand cmd = new SqlCommand();
//        SqlDataAdapter adapter = new SqlDataAdapter();
//        DataTable dt = new DataTable();
//        public delete()
//        {
//            InitializeComponent();
//            FillCombo();
//        }

//        public void FillCombo()
//        {
//            con = new SqlConnection(ConnectDB);

//            try
//            {
//                con.Open();
//                string Query = "select * from TestCatagory";
//                SqlCommand sqlCommand = new SqlCommand(Query, con);
//                SqlDataReader reader = sqlCommand.ExecuteReader();
//                while (reader.Read())
//                {
//                    string Name = reader.GetString(1);
//                    combobox.Items.Add(Name);

//                }
//                con.Close();
//            }
//            catch (SqlException ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//        }

//    }
//}

//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

//namespace Inventory.View
//{
//    /// <summary>
//    /// Interaction logic for Order.xaml
//    /// </summary>
//    public partial class Order : Page
//    {
//        string cs = ConfigurationManager.ConnectionStrings["InventoryDBCon"].ConnectionString;
//        string query = "";
//        int productID;
//        SqlCommand cmd = new SqlCommand();
//        SqlConnection con = new SqlConnection();


//        public Order()
//        {
//            InitializeComponent();
//            populateContact();
//            populateProduct();
//            InitializeSelectedProductsTable();
//        }

//        private void populateContact()
//        {
//            con = new SqlConnection(cs);
//            con.Open();
//            query = "SELECT ContactFirstName+' '+ContactLastName Contacts FROM ims.Contacts where ContactTypeID=1";
//            List<string> contacts = new List<string>();
//            cmd = new SqlCommand(query, con);

//            SqlDataReader reader = cmd.ExecuteReader();
//            while (reader.Read())
//            {
//                string contact = reader["Contacts"].ToString();
//                contacts.Add(contact);
//            }
//            cmb_ContactID.ItemsSource = contacts;
//            con.Close();
//        }
//        private void populateProduct()
//        {
//            con = new SqlConnection(cs);
//            con.Open();
//            query = "SELECT PRODUCTNAME FROM ims.PRODUCTS WHERE QuantityAvailable>0";
//            List<string> products = new List<string>();
//            cmd = new SqlCommand(query, con);

//            SqlDataReader reader = cmd.ExecuteReader();
//            while (reader.Read())
//            {
//                string product = reader["PRODUCTNAME"].ToString();
//                products.Add(product);
//            }
//            cmbAvailableProducts.ItemsSource = products;
//            con.Close();
//        }
//        private bool checkQuantity(int productID, int requestedQuantity)
//        {
//            con.Open();
//            string query = "SELECT QuantityAvailable FROM ims.Products WHERE ProductID = @ProductID";
//            SqlCommand command = new SqlCommand(query, con);
//            command.Parameters.AddWithValue("@ProductID", productID);
//            int availableQuantity = (int)command.ExecuteScalar();

//            if (availableQuantity >= requestedQuantity)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//        int GetProductID(string ProductName)
//        {

//            int ProductId = 0;
//            query = "SELECT ProductID FROM ims.Products WHERE ProductName = @ProductName";

//            using (con = new SqlConnection(cs))
//            {
//                using (SqlCommand command = new SqlCommand(query, con))
//                {
//                    command.Parameters.AddWithValue("@ProductName", ProductName);
//                    con.Open();

//                    // Execute the command
//                    object result = command.ExecuteScalar();

//                    // Check if the result is not null
//                    if (result != null && result != DBNull.Value)
//                    {
//                        // Convert the result to integer
//                        ProductId = Convert.ToInt32(result);

//                    }
//                    con.Close();

//                }
//            }
//            return ProductId;


//        }
//        int GetContactID(string ContactName)
//        {
//            int ProductId = 0;
//            query = "SELECT ContactID FROM ims.Contacts WHERE ContactFirstName+' '+ContactLastName = @ContactName";

//            using (con = new SqlConnection(cs))
//            {
//                using (SqlCommand command = new SqlCommand(query, con))
//                {
//                    command.Parameters.AddWithValue("@ContactName", ContactName);
//                    con.Open();

//                    // Execute the command
//                    object result = command.ExecuteScalar();

//                    // Check if the result is not null
//                    if (result != null && result != DBNull.Value)
//                    {
//                        // Convert the result to integer
//                        ProductId = Convert.ToInt32(result);

//                    }
//                    con.Close();

//                }
//            }
//            return ProductId;
//        }
//        private DataTable selectedProductsTable = new DataTable();
//        private void InitializeSelectedProductsTable()
//        {
//            selectedProductsTable.Columns.Add("ProductID", typeof(int));
//            selectedProductsTable.Columns.Add("ProductName", typeof(string));
//            selectedProductsTable.Columns.Add("Quantity", typeof(int));
//            selectedProductsTable.Columns.Add("UnitPrice", typeof(decimal));
//            selectedProductsTable.Columns.Add("TotalAmount", typeof(decimal));

//        }
//        private decimal GetUnitPrice(int productID)
//        {
//            decimal unitPrice = 0;
//            using (con = new SqlConnection(cs))
//            {
//                string query = "SELECT UnitPrice FROM ims.Products WHERE ProductID = @ProductID";
//                using (SqlCommand command = new SqlCommand(query, con))
//                {
//                    command.Parameters.AddWithValue("@ProductID", productID);
//                    con.Open();
//                    object result = command.ExecuteScalar();
//                    if (result != null && result != DBNull.Value)
//                    {
//                        unitPrice = Convert.ToDecimal(result);
//                    }
//                }
//            }
//            return unitPrice;
//        }
//        private void AddProduct_Click(object sender, RoutedEventArgs e)
//        {
//            string selectedProduct;

//            selectedProduct = cmbAvailableProducts.Text;


//            int ProductID = Convert.ToInt32(GetProductID(selectedProduct));
//            decimal unitPrice = GetUnitPrice(ProductID);


//            if (selectedProduct == null)
//            {
//                MessageBox.Show("Please select a product to add.");
//                return;
//            }

//            if (!int.TryParse(txtProductQuantity.Text, out int quantity) || quantity <= 0)
//            {
//                if (checkQuantity(ProductID, quantity) == false)
//                {
//                    MessageBox.Show("Please enter a valid quantity.");
//                    return;
//                }
//            }

//            decimal totalAmount = unitPrice * quantity;



//            selectedProductsTable.Rows.Add(ProductID, selectedProduct, quantity, unitPrice, totalAmount);

//            dgSelectedProducts.ItemsSource = selectedProductsTable.DefaultView;

//            cmbAvailableProducts.SelectedItem = null;
//            txtProductQuantity.Clear();
//        }

//        private int InsertOrder(int contactID, DateTime orderDate)
//        {
//            int orderID = 0;
//            string query = "INSERT INTO ims.Orders (ContactID, OrderDate) VALUES (@ContactID, @OrderDate); SELECT SCOPE_IDENTITY();";

//            SqlConnection con = new SqlConnection(cs);
//            SqlCommand command = new SqlCommand(query, con);
//            command.Parameters.AddWithValue("@ContactID", contactID);
//            command.Parameters.AddWithValue("@OrderDate", orderDate);
//            try
//            {
//                con.Open();
//                orderID = Convert.ToInt32(command.ExecuteScalar());
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error inserting order: " + ex.Message);
//            }


//            return orderID;
//        }

//        private void InsertOrderDetail(int orderID, int productID, int quantity, decimal unitPrice, decimal totalAmount)
//        {
//            string query = "INSERT INTO ims.OrderDetails (OrderID, ProductID, Quantity, UnitPrice, TotalAmount) " +
//                           " VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice, @TotalAmount); " +
//                           " UPDATE ims.Products SET QuantityAvailable = QuantityAvailable - @Quantity " +
//                           " WHERE ProductID = @ProductID ";

//            SqlConnection con = new SqlConnection(cs);
//            SqlCommand command = new SqlCommand(query, con);

//            command.Parameters.AddWithValue("@OrderID", orderID);
//            command.Parameters.AddWithValue("@ProductID", productID);
//            command.Parameters.AddWithValue("@Quantity", quantity);
//            command.Parameters.AddWithValue("@UnitPrice", unitPrice);
//            command.Parameters.AddWithValue("@TotalAmount", totalAmount);

//            try
//            {
//                con.Open();
//                command.ExecuteNonQuery();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error inserting order detail: " + ex.Message);
//            }


//        }

//        private void RemoveButton_Click(object sender, RoutedEventArgs e)
//        {
//            Button button = sender as Button;
//            if (button != null)
//            {
//                // Get the corresponding data item from the DataContext of the button
//                DataRowView dataRowView = button.DataContext as DataRowView;
//                if (dataRowView != null)
//                {
//                    // Remove the data item from the underlying data source
//                    selectedProductsTable.Rows.Remove(dataRowView.Row);
//                }
//            }
//        }

//        private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
//        {
//            int contactID = GetContactID(cmb_ContactID.Text);
//            DateTime orderDate = dpOrderDate.DisplayDate;
//            // Insert a new row into the ims.Orders table
//            int orderID = InsertOrder(contactID, orderDate);

//            // Iterate through the rows in the data grid
//            foreach (DataRowView rowView in dgSelectedProducts.ItemsSource)
//            {
//                DataRow row = rowView.Row;

//                int productID = (int)row["ProductID"];
//                int quantity = (int)row["Quantity"];
//                decimal unitPrice = (decimal)row["UnitPrice"];
//                decimal totalAmount = (decimal)row["TotalAmount"];

//                // Insert a new row into the ims.OrderDetails table
//                InsertOrderDetail(orderID, productID, quantity, unitPrice, totalAmount);
//            }

//            MessageBox.Show("Order confirmed successfully.");
//        }
//    }
//}
