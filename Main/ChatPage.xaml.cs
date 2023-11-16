using Messenger.Data.DAL;
using Messenger.Data.Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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

namespace Messenger.Main
{
    /// <summary>
    /// Логика взаимодействия для ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page, INotifyPropertyChanged
    {
        User myAccount;
        Chats CurrentChat;
        public List<Content> contentsList;
        public List<Content> ContentsList
        { get => contentsList;
            set
            {
                contentsList = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ContentsList)));
            }
        }
        

        public ChatPage(User user)
        {
            InitializeComponent();
            myAccount = user;
            
        }
        private CancellationTokenSource cancellationToken = new();
        public void ReSelectChat(Chats chat)
        {
            ContentsList?.Clear();   
            ContentsList = ContentDal.GetContentList(chat.Id).Result;
            CurrentChat = chat;
            UpdateMessage();
            lvDataBinding.ItemsSource = ContentsList;
            cancellationToken.Cancel();
            cancellationToken = new CancellationTokenSource();
            Sync(cancellationToken.Token);

            
        }

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {   
            if (messageTextBox.Text.ToString().IsNullOrEmpty())
            {
                return;
            }
            if(CurrentChat == null)
            {
                return;
            }

            Content content = new Content()
            {
                Id = 0,
                Id_chats = CurrentChat.Id,
                Contents = messageTextBox.Text.ToString(),
                TypeContent = "Message",
                CreateDt = DateTime.Now,
                User = myAccount
            };
            await ContentDal.Add(content);
            ReSelectChat(CurrentChat);
            messageTextBox.Text = "";

        }

        

        private async void Sync(CancellationToken cancellation)
        {
            
            if (cancellation.IsCancellationRequested)
            {
                return;
            }
            else
            {
                await Task.Delay(4000);
                lvDataBinding.ItemsSource = ContentDal.GetContentList(CurrentChat.Id).Result;
                Sync(cancellation);
            }

            
            
        }

        public void UpdateMessage()
        {
            foreach (Content content in ContentsList)
            {
                ContentDal.LoadUserInfo(content);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, e);
        }
    }

    
}
