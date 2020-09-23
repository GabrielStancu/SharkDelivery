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
using System.Data.SqlClient;
using System.Data;

namespace Shark_Delivery
{
    /// <summary>
    /// Interaction logic for ViewItems.xaml
    /// </summary>
    public partial class ViewItems : Window
    {
        User user;
        DisplayType type;
        public ViewItems(User user, DisplayType type)
        {
            InitializeComponent();
            this.user = user;
            this.type = type;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(user);
            this.Hide();
            main.Show();
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void LoadFavorites()
        {
            DbConnection conn = new DbConnection();
            conn.OpenConnection();

            SqlCommand getFavorites = new SqlCommand();
            getFavorites.Connection = conn.GetConnection();
            getFavorites.CommandText = "SELECT CustomerFavoriteItems.Id, Items.Name, Groups.Name, Subgroups.Name, FORMAT(Items.PriceWithTva, 'N2'), Providers.Name " +
                                       "FROM Items " +
                                       "INNER JOIN CustomerFavoriteItems ON CustomerFavoriteItems.IdItem = Items.Id " +
                                       "INNER JOIN Groups ON Items.GroupId = Groups.Id " +
                                       "INNER JOIN Subgroups ON Items.SubgroupId = Subgroups.Id " +
                                       "INNER JOIN Providers ON Items.ProviderId = Providers.Id " +
                                       "WHERE CustomerFavoriteItems.IdCustomer = @id";
            getFavorites.Parameters.AddWithValue("@id", this.user.GetId());

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = getFavorites;

            System.Data.DataTable dt = new System.Data.DataTable();
            adapter.Fill(dt);
            dgView.ItemsSource = dt.DefaultView;

            conn.CloseConnection();
        }

        private void LoadCart()
        {
            DbConnection conn = new DbConnection();
            conn.OpenConnection();

            SqlCommand getCart = new SqlCommand();
            getCart.Connection = conn.GetConnection();
            getCart.CommandText = "SELECT CustomerCarts.Id, Items.Name, Groups.Name, Subgroups.Name, FORMAT(Items.PriceWithTva, 'N2'), Providers.Name " +
                                       "FROM Items " +
                                       "INNER JOIN CustomerCarts ON CustomerCarts.IdItem = Items.Id " +
                                       "INNER JOIN Groups ON Items.GroupId = Groups.Id " +
                                       "INNER JOIN Subgroups ON Items.SubgroupId = Subgroups.Id " +
                                       "INNER JOIN Providers ON Items.ProviderId = Providers.Id " +
                                       "WHERE CustomerCarts.IdCustomer = @id";
            getCart.Parameters.AddWithValue("@id", this.user.GetId());

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = getCart;

            System.Data.DataTable dt = new System.Data.DataTable();
            adapter.Fill(dt);
            dgView.ItemsSource = dt.DefaultView;

            conn.CloseConnection();
        }

        private void LoadOrders()
        {
            DbConnection conn = new DbConnection();
            conn.OpenConnection();

            SqlCommand getOrders = new SqlCommand();
            getOrders.Connection = conn.GetConnection();
            getOrders.CommandText = "SELECT OrderedItems.Id, Orders.HeaderText, Items.Name, Groups.Name, Subgroups.Name, " +
                                    "States.Name, OrderedItems.Quantity, Items.PriceWithTva, Users.FirstName + ' ' + Users.LastName " +
                                    "FROM OrderedItems " +
                                    "INNER JOIN Orders ON OrderedItems.OrderId = Orders.Id " +
                                    "INNER JOIN Items ON OrderedItems.ItemId = Items.Id " +
                                    "INNER JOIN Groups ON Items.GroupId = Groups.Id " +
                                    "INNER JOIN Subgroups ON Items.SubgroupId = Subgroups.Id " +
                                    "INNER JOIN States ON Orders.StateId = States.Id " +
                                    "INNER JOIN Users ON Orders.DelivererId = Users.Id " + 
                                    "INNER JOIN Customers ON Orders.CustomerId = Customers.Id " +
                                    "WHERE Orders.CustomerId = @id";
            getOrders.Parameters.AddWithValue("@id", this.user.GetId());

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = getOrders;

            System.Data.DataTable dt = new System.Data.DataTable();
            adapter.Fill(dt);
            dgView.ItemsSource = dt.DefaultView;

            conn.CloseConnection();
        }

        private void PaintFavorites()
        {
            dgView.Columns[0].Header = "Remove Item";
            dgView.Columns[1].Visibility = Visibility.Hidden; //id
            dgView.Columns[2].Header = "Item name";
            dgView.Columns[3].Header = "Group";
            dgView.Columns[4].Header = "Subgroup";
            dgView.Columns[5].Header = "Price (with TVA)";
            dgView.Columns[6].Header = "Provider";
            dgView.Columns[0].DisplayIndex = 6;
        }

        private void PaintCart()
        {
            dgView.Columns[0].Header = "Remove Item";
            dgView.Columns[1].Visibility = Visibility.Hidden; //id
            dgView.Columns[2].Header = "Item name";
            dgView.Columns[3].Header = "Group";
            dgView.Columns[4].Header = "Subgroup";
            dgView.Columns[5].Header = "Price (with TVA)";
            dgView.Columns[6].Header = "Provider";
            dgView.Columns[0].DisplayIndex = 6;
        }

        private void PaintOrders()
        {     
            dgView.Columns[0].Visibility = Visibility.Hidden;
            dgView.Columns[1].Visibility = Visibility.Hidden; //id
            dgView.Columns[2].Header = "Order Name";
            dgView.Columns[3].Header = "Item Name";
            dgView.Columns[4].Header = "Group";
            dgView.Columns[5].Header = "Subgroup";
            dgView.Columns[6].Header = "Command status";
            dgView.Columns[7].Header = "Quantity";
            dgView.Columns[8].Header = "Price (with TVA)";
            dgView.Columns[9].Header = "Deliverer";
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.type==DisplayType.Favorites)
            {
                LoadFavorites();
                PaintFavorites();
                txtHeader.Text = "Favorite Items";
                btnPlaceCommand.Visibility = Visibility.Hidden;
            }    
            else if(this.type == DisplayType.Cart)
            {
                LoadCart();
                PaintCart();
                txtHeader.Text = "Your Shopping Cart";
                btnPlaceCommand.Visibility = Visibility.Visible;
            }
            else if (this.type == DisplayType.Orders)
            {
                LoadOrders();
                PaintOrders();
                txtHeader.Text = "Your Orders";
                btnPlaceCommand.Visibility = Visibility.Hidden;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow;
            int id;
            try
            {
                dataRow = (DataRowView)dgView.SelectedItem;
                id = int.Parse(dataRow.Row.ItemArray[0].ToString());
            }
            catch(Exception)
            {
                return;
            }       

            SqlCommand removeItem = new SqlCommand();
            string tableName = string.Empty;
            if (this.type == DisplayType.Favorites)
                tableName = "CustomerFavoriteItems";
            else if (this.type == DisplayType.Cart)
                tableName = "CustomerCarts";

            DbConnection conn = new DbConnection();
            conn.OpenConnection();

            removeItem.Connection = conn.GetConnection();
            removeItem.CommandText = "DELETE FROM " + tableName + " WHERE Id = @id";
            removeItem.Parameters.AddWithValue("@id", id);
            removeItem.ExecuteNonQuery();

            conn.CloseConnection();

            if (this.type == DisplayType.Favorites)
            {
                LoadFavorites();
                PaintFavorites();
            }
            else if (this.type == DisplayType.Cart)
            {
                LoadCart();
                PaintCart();
            }          
        }

        private void btnPlaceCommand_Click(object sender, RoutedEventArgs e)
        {
            if (this.user.GetTown() != string.Empty && this.user.GetStreet() != string.Empty && this.user.GetFlatHouseNr() != string.Empty &&
                this.user.GetFirstName() != string.Empty && this.user.GetLastName() != string.Empty && this.user.GetPhoneNr() != string.Empty &&
                this.user.GetMailAddress() != string.Empty)
                MessageBox.Show("Your command was successfully placed!"); //to do-> fereastra cu verificare detalii
            else
                MessageBox.Show("You haven't completed your profile details, the command could not be placed!");
        }
    }
}
