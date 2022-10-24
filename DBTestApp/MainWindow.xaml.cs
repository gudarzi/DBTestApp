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

namespace DBTestApp
{
    public partial class MainWindow : Window
    {
        public List<User> DatabaseUsers { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Create()
        {
            using (DataContext context = new DataContext())
            {
                string name = NameTextBox.Text;
                string address = AddressTextBox.Text;

                if (name != null && address != null)
                {
                    context.Users.Add(new User() { Name = name, Address = address});
                    context.SaveChanges();
                }
            }
        }

        public void Read()
        {
            using (DataContext context = new DataContext())
            {
                DatabaseUsers = context.Users.ToList();
                ItemList.ItemsSource = DatabaseUsers;
            }
        }

        public void Update()
        {
            using (DataContext context = new DataContext())
            {
                User? selectedUser = ItemList.SelectedItem as User;

                if (selectedUser is null) return;

                string Name = NameTextBox.Text;
                string Address = AddressTextBox.Text;

                if (Name != null && Address != null)
                {
                    User user = context.Users.Single(x => x.Id == selectedUser.Id);

                    user.Name = Name;
                    user.Address = Address;
                
                    context.SaveChanges();
                }
            }
        }

        public void Delete()
        {
            using (DataContext context = new DataContext())
            {
                User? selectedUser = ItemList.SelectedItem as User;

                if (selectedUser is null) return;

                if (selectedUser != null)
                {
                    User user = context.Users.Single(x => x.Id == selectedUser.Id);

                    context.Users.Remove(user);

                    context.SaveChanges();
                }
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Create();
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            Read();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ItemList.ItemsSource = null;
            ItemList.Items.Clear();
        }
    }
}
