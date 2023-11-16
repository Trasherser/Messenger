using Messenger.Data.DAL;
using Messenger.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Messenger.WorkWithData
{
    public static class WorkData
    {
        //SOLID -> O

       
        public static User? CheckDataUser(this User user)
        {
            int[] rangeLogin = { 3, 15 };
            if (user.Login.Length > rangeLogin.Last() || user.Login.Length < rangeLogin.First())
            {
                MessageBox.Show($"Error Login Length Range [{rangeLogin.First()} - {rangeLogin.Last()}]");
                return null;
            }

            int[] rangePassword = { 8, 30 };
            if (user.Password.Length > rangePassword.Last() || user.Password.Length < rangePassword.First())
            {
                MessageBox.Show($"Error Password Length Range [{rangePassword.First()} - {rangePassword.Last()}]");
                return null;
            }

            var temp = user.Email.Split("@");
            MessageBox.Show(temp[0].Length + " - " + temp.Last().Length);
            if (temp.Length != 2) 
            {
                MessageBox.Show("Error email not symbols @");
                return null;
                
            }
            int[] rangeEmail = { 5, 15 };
            if (temp.First().Length > rangeEmail.Last() || temp.First().Length < rangeEmail.First())
            { 
                return null;
            }
            if (temp.Last().Length > rangeEmail.Last() || temp.Last().Length < rangeEmail.First())
            {
                return null;
            }
            return user;

        }
        //Рандом - один на проект
        public static String GetConfirmCode()
        {
            Random rand = new Random();
            String str = "";
            for (int i = 0; i < 6; i++) 
            {
                str += rand.Next(9);
            }
            return str;
        }

        public static bool CheckPassword(User? user, String pass) 
        {
            return user?.Password == pass;
        }

        public static SmtpClient? GetSmtpClient()
        {
            #region get and check config
            String? host = App.GetConfiguration("smtp:host");
            if (host == null)
            {
                MessageBox.Show("Error getting host");
                return null;
            }
            String? portString = App.GetConfiguration("smtp:port");
            if (portString == null)
            {
                MessageBox.Show("Error getting host");
                return null;
            }
            int port;
            try
            {
                port = int.Parse(portString);
            }
            catch (Exception)
            {
                MessageBox.Show("Error parsing port");
                return null;
            }
            String? email = App.GetConfiguration("smtp:email");
            if (email == null)
            {
                MessageBox.Show("Error getting email");
                return null;
            }
            String? password = App.GetConfiguration("smtp:password");
            if (password == null)
            {
                MessageBox.Show("Error getting password");
                return null;
            }
            String? sslString = App.GetConfiguration("smtp:ssl");
            if (sslString == null)
            {
                MessageBox.Show("Error getting ssl");
                return null;
            }
            bool ssl;
            try
            {
                ssl = bool.Parse(sslString);
            }
            catch (Exception)
            {
                MessageBox.Show("Error parsing ssl");
                return null;
            }
            #endregion

            

            return new SmtpClient(host, port)
            {
                EnableSsl = ssl,
                Credentials = new NetworkCredential(email, password)
            };

        }

    }
}
