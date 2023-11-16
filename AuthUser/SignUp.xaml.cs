using Messenger.WorkWithData;
using Messenger.Data.DAL;
using Messenger.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Messenger.Main;

namespace Messenger.AuthUser
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        User user;
        AuthWindow authWindow;
        public SignUp(AuthWindow authWindow)
        {
            InitializeComponent();
            this.authWindow = authWindow;
            user = new User
            {
                Id = Guid.NewGuid(),
                Login = "",
                Password = "",
                Email = "",
                ConfirmCode = WorkData.GetConfirmCode(),
                CreateDt = DateTime.Now,
            };
            this.DataContext = user;
        }

        private void signInPage_Click(object sender, RoutedEventArgs e)
        {
            authWindow.Next();

        }

        private async void signUp_Click(object sender, RoutedEventArgs e)
        {
            if(user.CheckDataUser() != null)
            {
                await UserDal.Add(user);
                MessageBox.Show("Успішно зареєструвалися");
                new MainWindow(user).Show();
                authWindow.Close();
            }
            

        }


    }
}
