using Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace test_dem.Pages
{
    /// <summary>
    /// Interaction logic for ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            if (GlobalContainer.Role.Name == "Admin")
            {
                AddManager.IsEnabled = true;
            }
            Clients.ItemsSource = ClientService.GetAll();
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var surname = Surname.Text.Trim();
            var name = Name.Text.Trim();
            var lastname = Lastname.Text.Trim();
            var phone = ClientService.MapPhone(Phone.Text.Trim());

            var isDataValid = ClientService.IsDataValid(surname, name, lastname, phone);
            if (isDataValid == false)
            {
                MessageBox.Show("Данные невалидны");
                return;
            }
            var result = ClientService.Add(surname, name, lastname, phone);
            if (result == null)
            {
                MessageBox.Show("Не удалось создать клиента");
                return;
            }
            Clients.ItemsSource = ClientService.GetAll();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            var client = (Client)Clients.SelectedItem;
            if (client == null)
            {
                return;
            }
            var result = ClientService.Remove(client.Id);
            if (result == null)
            {
                MessageBox.Show($"Клиент {client.Surname} {client.Name} не может быть удален");
                return;
            }
            Clients.ItemsSource = ClientService.GetAll();
        }

        private void Clients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var client = (Client)Clients.CurrentItem;
            if (client == null)
            {
                return;
            }
            GlobalContainer.ClientId = client.Id;
            GlobalContainer.ClientName = client.Name;
            GlobalContainer.ClientSurname = client.Surname;

            MainWindow window = new MainWindow
            {
                Title = "Карточка клиента"
            };
            window.Frame.Navigate(new ClientCardPage());
        }

        private void Clients_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var client = (Client)Clients.CurrentItem;
            if (client == null)
            {
                return;
            }
            GlobalContainer.ClientId = client.Id;
            GlobalContainer.ClientName = client.Name;
            GlobalContainer.ClientSurname = client.Surname;

            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.Title = "Карточка клиента";
            window.Frame.Navigate(new ClientCardPage());
        }
    }
}
