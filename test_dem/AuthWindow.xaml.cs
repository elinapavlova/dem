using Service;
using System.Windows;

namespace test_dem
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Authorize_Click(object sender, RoutedEventArgs e)
        {
            var login = Login.Text.Trim();
            var password = Password.Password.Trim();

            var isLoginValid = UserService.IsLoginValid(login);
            var isPasswordValid = UserService.IsPasswordValid(password);
            var isLoginExist = UserService.IsLoginExist(login);

            if (isLoginValid != true || isPasswordValid != true || isLoginExist != true)
            {
               // return;
            }

            var result = UserService.Authorize(login, password);
            if (result == null)
            {
                MessageBox.Show("Неверный пароль");
                return;
            }

            GlobalContainer.Role = result.Role;

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

    }
}
