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
            var isLoginValid = UserService.IsLoginValid(Login.Text);
            var isPasswordValid = UserService.IsPasswordValid(Password.Password);
            var isLoginExist = UserService.IsLoginExist(Login.Text);

            //if (isLoginValid != true || isPasswordValid != true || isLoginExist != true)
            //{
            //    return;
            //}

            if (isLoginExist != true)
            {
                return;
            }

            var result = UserService.Authorize(Login.Text, Password.Password);
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
