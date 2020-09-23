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
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void lblSignUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SignUp SignUp = new SignUp();
            this.Hide();
            SignUp.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //does nothing, will remove later 
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            User user = GetUser();
            if (user != null)
            {
                MainWindow main = new MainWindow(GetUser());
                this.Hide();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("The data you inserted is not valid...");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private User GetUser()
        {
            User user;
            DbConnection conn = new DbConnection();
            conn.OpenConnection();

            SqlCommand getUser = new SqlCommand();
            getUser.Connection = conn.GetConnection();
            getUser.CommandText = "SELECT * FROM Customers WHERE UserName = @user AND Password = @pass";
            getUser.Parameters.AddWithValue("@user", txtLogUserName.Text);
            getUser.Parameters.AddWithValue("@pass", txtLogPassword.Password);

            SqlDataReader reader = getUser.ExecuteReader();
            if (reader.Read())
            {
                user = new User(int.Parse(reader[0].ToString()), reader[3].ToString(), reader[4].ToString(), 
                    reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString());
            }
            else
            {
                user = null;
            }
            reader.Close();
            conn.CloseConnection();
            return user;
        }
    }
}
