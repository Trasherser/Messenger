using Messenger.Data.DAL;
using Messenger.Data.Entity;
using Messenger.WorkWithData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Messenger.Main
{
    /// <summary>
    /// Логика взаимодействия для SettingsUserWindow.xaml
    /// </summary>
    public partial class SettingsUserWindow : Window
    {
        public User myAccount;
        public SettingsUserWindow(User myAccount)
        {
            InitializeComponent();
            this.myAccount = myAccount;
            stackPanel.DataContext = myAccount;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = new User
            {
                Id = myAccount.Id,
                Login = login.Text,
                Password = password.Text,
                Email = email.Text,
                ConfirmCode = myAccount.ConfirmCode,
                CreateDt = myAccount.CreateDt,
            }.CheckDataUser();
            if (user != null)
            {
                UserDal.Update(user);
            }

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            User user = await UserDal.Get(myAccount.Id);
            if (user.ConfirmCode != null)
            {
                statusConfirmCode.Text = "Треба підтвердити почту";
                sendButton.Visibility = Visibility.Visible;
            }
            else
            {
                statusConfirmCode.Text = "Почта підтверджена";
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SmtpClient? smtpClient = WorkData.GetSmtpClient();
            if (smtpClient == null) { return; }
            smtpClient?.Send(
                App.GetConfiguration("smtp:email"),
                myAccount.Email,
                "Підтвердження пошти",
                myAccount.ConfirmCode.ToString());

            MessageBox.Show("Надіслано");
            inputCode.Visibility = Visibility.Visible;
        }

        private void inputCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is TextBox text)
            {
                if(text.Text.Length == 6)
                {
                    if(text.Text == myAccount.ConfirmCode)
                    {
                        UserDal.SetNullConfirmCode(myAccount.Id);
                        sendButton.Visibility = Visibility.Hidden;
                        inputCode.Visibility = Visibility.Hidden;
                    }
                }
            }
        }
    }
}





























//Regex reg = new Regex(@"^[^\.]([\w\d_]\.?){1,18}[^\.]@[\w\d_]{1,20}\.\w{2,20}$");
//result = reg.Match(user.Email);
//return result.Success;