using Messenger.Data;
using Messenger.Data.DAL;
using Messenger.Data.Entity;
using Messenger.Main;
using Messenger.WorkWithData;
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

namespace Messenger.AuthUser
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        

        AuthWindow authWindow;
        public SignIn(AuthWindow authWindow)
        {
            InitializeComponent();
            this.authWindow = authWindow;
            
            
        }

        private void signUpPage_Click(object sender, RoutedEventArgs e)
        {
            authWindow.Next();
        }

        private async void signIn_Click(object sender, RoutedEventArgs e)
        {
            var user = await UserDal.Get(loginTextBox.Text);
            if(user == null)
            {
                MessageBox.Show("Такого логіна немає");
                return;
            }
            if(WorkData.CheckPassword(user, passwordTextBox.Text))
            {
                new MainWindow(user).Show();
                authWindow.Close();
            }
            else
            {
                MessageBox.Show("Невірний пароль");
            }
        }
    }
}
