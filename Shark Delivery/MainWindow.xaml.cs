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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesign;
using System.Data.SqlClient;
using System.IO;

namespace Shark_Delivery
{
    public partial class MainWindow : Window
    {
        List<Item> itemList;
        List<Item> displayList;
        List<Image> images;
        List<StackPanel> panels;
        List<Grid> grids;
        User user;
        int setToLoad;
        public MainWindow(User user)
        {
            InitializeComponent();
            btnCloseMenu.Visibility = Visibility.Collapsed;
            btnOpenMenu.Visibility = Visibility.Visible;
            itemList = new List<Item>();
            displayList = new List<Item>();
            images = new List<Image> { Img1, Img2, Img3, Img4, Img5, Img6, Img7, Img8, Img9 };
            panels = new List<StackPanel> { sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9 };
            grids = new List<Grid> { Grid1, Grid2, Grid3, Grid4, Grid5, Grid6, Grid7, Grid8, Grid9};

            this.user = user;
            LoadMenus();
            LoadUserVariables();
            LoadItems();
            setToLoad = 0;
            DisplayItems(setToLoad);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Collapsed;
            btnCloseMenu.Visibility = Visibility.Visible;
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Visible;
            btnCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            UserProfile profile = new UserProfile(this.user);
            this.Hide();
            profile.Show();
            this.Close();
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            ViewItems itemView = new ViewItems(this.user, DisplayType.Orders);
            this.Hide();
            itemView.Show();
            this.Close();
        }

        private void btnFavorites_Click(object sender, RoutedEventArgs e)
        {
            ViewItems itemView = new ViewItems(this.user, DisplayType.Favorites);
            this.Hide();
            itemView.Show();
            this.Close();
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            ViewItems itemView = new ViewItems(this.user, DisplayType.Cart);
            this.Hide();
            itemView.Show();
            this.Close();
        }

        private void txtSearchKeyDown(object sender, KeyEventArgs e)
        {
           if(e.Key == Key.Enter)
            {
                SearchItem();
            }
        }

        private void search_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchItem();
        }

        private void SearchItem()
        {
            displayList.Clear();
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].GetName().ToUpper().Contains(txtSearch.Text.ToUpper()) || itemList[i].GetGroup().ToUpper().Contains(txtSearch.Text.ToUpper()) || 
                    itemList[i].GetSubgroup().ToUpper().Contains(txtSearch.Text.ToUpper()))
                {
                    displayList.Add(itemList[i]);
                }
            }
            setToLoad = 0;
            DisplayItems(setToLoad);
        }

        private void AddToCart(Item item)
        {
            DbConnection conn = new DbConnection();
            conn.OpenConnection();

            SqlCommand addToCart = new SqlCommand();
            addToCart.Connection = conn.GetConnection();
            addToCart.CommandText = "INSERT INTO CustomerCarts(IdCustomer, IdItem) VALUES(@idC, @idI)";
            addToCart.Parameters.AddWithValue("@idC", user.GetId());
            addToCart.Parameters.AddWithValue("@idI", item.GetId());
            addToCart.ExecuteNonQuery();

            conn.CloseConnection();
            MessageBox.Show("The item " + item.GetName() + " was added to your cart!");
        }

        private void AddToFavorites(Item item)
        {
            DbConnection conn = new DbConnection();
            conn.OpenConnection();

            SqlCommand addToCart = new SqlCommand();
            addToCart.Connection = conn.GetConnection();
            addToCart.CommandText = "INSERT INTO CustomerFavoriteItems(IdCustomer, IdItem) VALUES(@idC, @idI)";
            addToCart.Parameters.AddWithValue("@idC", user.GetId());
            addToCart.Parameters.AddWithValue("@idI", item.GetId());
            addToCart.ExecuteNonQuery();

            conn.CloseConnection();

            MessageBox.Show("The item " + item.GetName() + " was added to your favorite list!");
        }

        private void item1H_Click(object sender, MouseButtonEventArgs e)
        {
            AddToFavorites(itemList[setToLoad * 9 + 0]);
        }

        private void item1C_Click(object sender, MouseButtonEventArgs e)
        {
            AddToCart(itemList[setToLoad*9 + 0]);
        }

        private void item2H_Click(object sender, MouseButtonEventArgs e)
        {
            AddToFavorites(itemList[setToLoad * 9 + 1]);
        }

        private void item2C_Click(object sender, MouseButtonEventArgs e)
        {
            AddToCart(itemList[setToLoad * 9 + 1]);
        }

        private void item3H_Click(object sender, MouseButtonEventArgs e)
        {
            AddToFavorites(itemList[setToLoad * 9 + 2]);
        }

        private void item3C_Click(object sender, MouseButtonEventArgs e)
        {
            AddToCart(itemList[setToLoad * 9 + 2]);
        }

        private void item4H_Click(object sender, MouseButtonEventArgs e)
        {
            AddToFavorites(itemList[setToLoad * 9 + 3]);
        }

        private void item4C_Click(object sender, MouseButtonEventArgs e)
        {
            AddToCart(itemList[setToLoad * 9 + 3]);
        }

        private void item5H_Click(object sender, MouseButtonEventArgs e)
        {
            AddToFavorites(itemList[setToLoad * 9 + 4]);
        }

        private void item5C_Click(object sender, MouseButtonEventArgs e)
        {
            AddToCart(itemList[setToLoad * 9 + 4]);
        }

        private void item6H_Click(object sender, MouseButtonEventArgs e)
        {
            AddToFavorites(itemList[setToLoad * 9 + 5]);
        }

        private void item6C_Click(object sender, MouseButtonEventArgs e)
        {
            AddToCart(itemList[setToLoad * 9 + 5]);
        }

        private void item7H_Click(object sender, MouseButtonEventArgs e)
        {
            AddToFavorites(itemList[setToLoad * 9 + 6]);
        }

        private void item7C_Click(object sender, MouseButtonEventArgs e)
        {
            AddToCart(itemList[setToLoad * 9 + 6]);
        }

        private void item8H_Click(object sender, MouseButtonEventArgs e)
        {
            AddToFavorites(itemList[setToLoad * 9 + 7]);
        }

        private void item8C_Click(object sender, MouseButtonEventArgs e)
        {
            AddToCart(itemList[setToLoad * 9 + 7]);
        }

        private void item9H_Click(object sender, MouseButtonEventArgs e)
        {
            AddToFavorites(itemList[setToLoad * 9 + 8]);
        }

        private void item9C_Click(object sender, MouseButtonEventArgs e)
        {
            AddToCart(itemList[setToLoad * 9 + 8]);
        }

        private void LoadMenus()
        {
            List<ItemMenu> menuList = new List<ItemMenu>();

            DbConnection conn = new DbConnection();
            conn.OpenConnection();

            SqlCommand getGroups = new SqlCommand();
            getGroups.Connection = conn.GetConnection();
            getGroups.CommandText = "SELECT Name FROM Groups";

            SqlDataReader reader = getGroups.ExecuteReader();
            while(reader.Read())
            {
                var list = new List<SubItem>();
                var menu = new ItemMenu(reader[0].ToString(), list);
                menuList.Add(menu);
            }
            reader.Close();

            SqlCommand getSubgroups = new SqlCommand();
            getSubgroups.Connection = conn.GetConnection();
            getSubgroups.CommandText = "SELECT Name, GroupId FROM Subgroups";

            SqlDataReader readerS = getSubgroups.ExecuteReader();
            while(readerS.Read())
            {
                SubItem item = new SubItem(readerS[0].ToString());
                menuList[int.Parse(readerS[1].ToString())-1].SubItems.Add(item);  
            }
            readerS.Close();
            conn.CloseConnection();

            for(int i = 0;  i<menuList.Count; i++)
            {
                StackPanelMenu.Children.Add(new UserControlMenuItem(menuList[i]));
            }
        }

        private void Subgroup_Click(object sender, EventArgs e)
        {
            //HOW
        }

        private void LoadUserVariables()
        {
            txtUserName.Text = this.user.GetFirstName();
            //de setat pb-ul, daca mai apuc
        }

        private void LoadItems()
        {
            DbConnection conn = new DbConnection();
            conn.OpenConnection();

            SqlCommand loadItems = new SqlCommand();
            loadItems.Connection = conn.GetConnection();
            loadItems.CommandText = "SELECT Items.Id, Items.Name, Items.GroupId, Groups.Name, Items.SubgroupId, Subgroups.Name, " +
                                    "Items.Quantity, MeasurementUnits.Name, Items.PriceNoTva, TVAs.TvaValue, Items.PriceWithTva, Providers.Name " +
                                    "FROM Items INNER JOIN Groups ON Items.GroupId = Groups.Id " +
                                    "INNER JOIN Subgroups ON Items.SubgroupId = Subgroups.Id " +
                                    "INNER JOIN Providers ON Items.ProviderId = Providers.Id " +
                                    "INNER JOIN MeasurementUnits ON Items.UnitId = MeasurementUnits.Id " +
                                    "INNER JOIN TVAs ON Items.TvaId = TVAs.Id " +
                                    "ORDER BY Items.GroupId, Items.SubgroupId, Items.Name";

            SqlDataReader reader = loadItems.ExecuteReader();
            while(reader.Read())
            {
                Item item = new Item(int.Parse(reader[0].ToString()), reader[1].ToString(), int.Parse(reader[2].ToString()), reader[3].ToString(),
                                     int.Parse(reader[4].ToString()), reader[5].ToString(), int.Parse(reader[6].ToString()), reader[7].ToString(),
                                     float.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), float.Parse(reader[10].ToString()),
                                     reader[11].ToString());
                itemList.Add(item);
                displayList.Add(item);
            }
            reader.Close();
            conn.CloseConnection();
        }

        private void DisplayItems(int setNr)
        {
            for(int i = setNr*9; i<=setNr*9+8; i++)
            {
                if(i<displayList.Count)
                {
                    SetPositionVisible(i % 9);
                    SetImage(i % 9, displayList[i]);
                    SetToolTip(displayList[i].GetName(), displayList[i].GetPriceWithTva().ToString() + " RON", images[i % 9], grids[i % 9]);
                }
                else
                {
                    SetPositionInvisible(i % 9);
                }
            }
        }

        private void SetImage(int index, Item item)
        {
            BitmapImage bmp = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Inventory\" + item.GetName() +".jpg"));
            images[index].Source = bmp;
        }

        private void SetPositionVisible(int index)
        {
            images[index].Visibility = Visibility.Visible;
            panels[index].Visibility = Visibility.Visible;
            grids[index].Visibility = Visibility.Visible;
        }

        private void SetPositionInvisible(int index)
        {
            images[index].Visibility = Visibility.Hidden;
            panels[index].Visibility = Visibility.Hidden;
            grids[index].Visibility = Visibility.Hidden;
        }

        private void btnLoadPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (this.setToLoad > 0)
                setToLoad--;
            DisplayItems(setToLoad);
        }

        private void btnLoadNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.setToLoad < displayList.Count/9)
                setToLoad++;
            DisplayItems(setToLoad);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboFilter.SelectedItem == cbAlpha)
            {
                displayList.Sort((x, y) => x.GetName().CompareTo(y.GetName()));
            }
            else if (cboFilter.SelectedItem == cbPrA)
            {
                displayList.Sort((x, y) => x.GetPriceWithTva().CompareTo(y.GetPriceWithTva()));
            }
            else if (cboFilter.SelectedItem == cbPrD)
            {
                displayList.Sort((x, y) => y.GetPriceWithTva().CompareTo(x.GetPriceWithTva()));
            }
            setToLoad = 0;
            DisplayItems(setToLoad);
        }

        private void SetToolTip(string textName, string textPrice, Image pb, Grid container)
        {
            SetTxtForToolTip(textName, pb, container, 0);
            SetTxtForToolTip(textPrice, pb, container, 1);            
        }

        private void SetTxtForToolTip(string textName, Image pb, Grid container, int thickNessIndex)
        {
            Border border = new Border();
            border.BorderThickness = new Thickness(2);
            border.BorderBrush = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TextBlock txtNume = new TextBlock();
            txtNume.Text = textName;
            txtNume.Visibility = Visibility.Hidden;
            txtNume.IsEnabled = false;
            txtNume.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            txtNume.FontSize = 20;
            txtNume.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDEDEDE"));
            txtNume.Margin = (thickNessIndex == 0) ? new Thickness(0, 0, 0, 125) : new Thickness(0, 120, 120, 0);
            border.Child = txtNume;

            container.Children.Add(border);

            pb.MouseEnter += new MouseEventHandler((sender, e) => displayNameAvatarHover(sender, e, txtNume));
            pb.MouseLeave += new MouseEventHandler((sender, e) => hideNameAvatarHover(sender, e, txtNume));
        }

        private void displayNameAvatarHover(object sender, EventArgs e, TextBlock txt)
        {
            txt.IsEnabled = true;
            txt.Visibility = Visibility.Visible;
        }

        private void hideNameAvatarHover(object sender, EventArgs e, TextBlock txt)
        {
            txt.IsEnabled = false;
            txt.Visibility = Visibility.Hidden;
        }
    }
}
