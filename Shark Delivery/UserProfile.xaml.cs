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

namespace Shark_Delivery
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        User user;
        public UserProfile(User user)
        {
            InitializeComponent();
            this.user = user;
            LoadUserDetails();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if(!UpdateLocalUser()) return;
            DbConnection conn = new DbConnection();
            conn.OpenConnection();

            SqlCommand updateUser = new SqlCommand();
            updateUser.Connection = conn.GetConnection();
            updateUser.CommandText = "UPDATE Customers SET FirstName = @first, LastName = @last, Town = @town, Street = @street, " +
                                     "[Flat/House Nr.] = @flat, PhoneNr = @phone, MailAddress = @mail WHERE Id = @id";
            updateUser.Parameters.AddWithValue("@first", this.user.GetFirstName());
            updateUser.Parameters.AddWithValue("@last", this.user.GetLastName());
            updateUser.Parameters.AddWithValue("@town", this.user.GetTown());
            updateUser.Parameters.AddWithValue("@street", this.user.GetStreet());
            updateUser.Parameters.AddWithValue("@flat", this.user.GetFlatHouseNr());
            updateUser.Parameters.AddWithValue("@phone", this.user.GetPhoneNr());
            updateUser.Parameters.AddWithValue("@mail", this.user.GetMailAddress());
            updateUser.Parameters.AddWithValue("@id", this.user.GetId());

            updateUser.ExecuteNonQuery();
            conn.CloseConnection();
            MessageBox.Show("Changes saved successfully!");
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(user);
            this.Hide();
            main.Show();
            this.Close();
        }

        private void LoadUserDetails()
        {
            txtFirstName.Text = this.user.GetFirstName();
            txtLastName.Text = this.user.GetLastName();
            txtTown.Text = this.user.GetTown();
            txtStreet.Text = this.user.GetStreet();
            txtFlatHouse.Text = this.user.GetFlatHouseNr();
            txtPhoneNr.Text = this.user.GetPhoneNr();
            txtEmail.Text = this.user.GetMailAddress();
        }

        private bool UpdateLocalUser()
        {
            if (txtFirstName.Text.Length > 20)
            {
                MessageBox.Show("First name too long, max 20 chars...");
                return false;
            }
                

            if (txtLastName.Text.Length > 20)
            {
                MessageBox.Show("Last name too long, max 20 chars...");
                return false;
            }
                
            if(txtTown.Text.Length > 20)
            {
                MessageBox.Show("Town name too long, max 20 chars...");
                return false;
            }

            if(txtStreet.Text.Length > 20)
            {
                MessageBox.Show("Street name too long, max 20 chars...");
                return false;
            }

            if (txtFlatHouse.Text.Length > 20)
            {
                MessageBox.Show("Flat/House number too long, max 20 chars...");
                return false;
            }

            if (txtPhoneNr.Text.Length != 10)
            {
                MessageBox.Show("Phone number length incorrect, should be 10 chars...");
                return false;
            }
            else if(txtPhoneNr.Text[0] != '0')
            {
                MessageBox.Show("Phone number should start with 0...");
                return false;
            }

            if (txtEmail.Text.Length > 40)
            {
                MessageBox.Show("Mail address too long, max 40 chars...");
                return false;
            }
            else if (!txtPhoneNr.Text.Contains('@') && !txtPhoneNr.Text.Contains('.'))
            {
                MessageBox.Show("Mail address incorrect, should contain '@' and '.'...");
                return false;
            }

            this.user.SetFirstName(txtFirstName.Text);
            this.user.SetLastName(txtLastName.Text);
            this.user.SetTown(txtTown.Text);
            this.user.SetStreet(txtStreet.Text);
            this.user.SetFlatHouseNr(txtFlatHouse.Text);
            this.user.SetPhoneNr(txtPhoneNr.Text);
            this.user.SetMailAddress(txtEmail.Text);
            return true;
        }
    }
}
