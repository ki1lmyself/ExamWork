using Abp.Authorization.Users;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ExamWork.DataAccessLayer;

namespace ExamWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {

            string userLogin = loginTextBox.Text;
            string userPassword = passwordTextBox.Text;

            if (CheckUser(userLogin, userPassword))
            {
                try
                {
                    ClientProduct products = new ClientProduct();
                    products.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private bool CheckUserCredentials(string login, string password)
        {
            // Здесь производится запрос к базе данных для проверки логина и пароля
            // Возвращаем true, если пользователь найден, иначе - false
            return false;
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            ClientProduct product = new ClientProduct();
            product.Show();
        }
    }
}