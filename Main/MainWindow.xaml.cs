using Messenger.Data.DAL;
using Messenger.Data.Entity;
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
using System.Windows.Shapes;

namespace Messenger.Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User myAccount;
        ChatPage PageChat;
        

        public MainWindow(User myAccount)
        {
            InitializeComponent();
            this.myAccount = myAccount;
            PageChat = new ChatPage(myAccount);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetChatsToList();
            mainFrame.Content = PageChat;
        }

        private void SetChatsToList()
        {
            ChatsDal.GetMyChatsLoad(myAccount);
            chatsList.ItemsSource = myAccount.Chats;
            chatsList.DisplayMemberPath = "Id";
        }

       
        private void chatsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender is ListBox lb)
            {
                PageChat.ReSelectChat(myAccount.Chats[lb.SelectedIndex]);
                //MessageBox.Show(lb.SelectedIndex.ToString());
                //mainFrame.Content = new ChatPage(myAccount.Chats[lb.SelectedIndex]);
            }
            
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new SettingsUserWindow(myAccount).ShowDialog();
        }

        private async void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(inputIndex.Text);
                var temp1 = await ChatsDal.IsUniqueChatId(index);
                if (temp1)
                {
                    MessageBox.Show("Error index no unique");
                    return;
                }
                Chats chat = new Chats()
                {
                    Id = index,
                    Id_user = myAccount.Id,
                    CreateDt = DateTime.Now,
                };

                await ChatsDal.Add(chat);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            

            
        }

        private async void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32(inputIndex.Text);
            var temp = await ChatsDal.IsUniqueChatId(index);
            if (!temp)
            {
                MessageBox.Show("Error index no unique");
                return;
            }
            Chats chat = new Chats()
            {
                Id = index,
                Id_user = myAccount.Id,
                CreateDt = DateTime.Now,
            };

            await ChatsDal.Add(chat);
            SetChatsToList();
            PageChat.ReSelectChat(chat);
        }
    }
}
