using System;
using System.Collections.Generic;
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

namespace Messenger.AuthUser
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        bool switchPage = true;
        Page[] pages = new Page[2];

        public AuthWindow()
        {
            InitializeComponent();
            pages[0] = new SignIn(this);
            pages[1] = new SignUp(this);
            AuthFrame.Content = pages.First();

        }
        public void Next()
        {
            if (switchPage == true)
            {
                AuthFrame.Content = pages.Last();
                switchPage = false;
            }
            else
            {
                AuthFrame.Content = pages.First();
                switchPage = true;
            }

        }
    }
}
