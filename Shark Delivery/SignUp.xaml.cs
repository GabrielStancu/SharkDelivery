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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            LogIn LogIn = new LogIn();
            this.Hide();
            LogIn.Show();
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            DbConnection conn = new DbConnection();
            conn.OpenConnection();

            if(!UserAlreadyRegistered())
            {
                if(txtLogPassword.Password == txtConfirmLogPassword.Password)
                {
                    SqlCommand createUser = new SqlCommand();
                    createUser.Connection = conn.GetConnection();
                    createUser.CommandText = "INSERT INTO Customers(UserName, Password) VALUES(@user, @pass)";
                    createUser.Parameters.AddWithValue("@user", txtLogUserName.Text);
                    createUser.Parameters.AddWithValue("@pass", txtLogPassword.Password);
                    createUser.ExecuteNonQuery();
                    MessageBox.Show("New user created succesfully!");
                }
                else
                {
                    MessageBox.Show("The passwords do not match...");
                }
            }
            else
            {
                MessageBox.Show("The user already exists...");
            }
            conn.CloseConnection();
        }

        private bool UserAlreadyRegistered()
        {
            bool exists = false;
            DbConnection conn = new DbConnection();
            conn.OpenConnection();

            SqlCommand getUser = new SqlCommand();
            getUser.Connection = conn.GetConnection();
            getUser.CommandText = "SELECT * FROM Customers WHERE UserName = @user";
            getUser.Parameters.AddWithValue("@user", txtLogUserName.Text);

            SqlDataReader reader = getUser.ExecuteReader();
            if(reader.Read())
            {
                exists = true;
            }
            reader.Close();
            conn.CloseConnection();
            return exists;
        }
    }
}
