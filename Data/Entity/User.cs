using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Messenger.Data.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? ConfirmCode { get; set; }
        public DateTime CreateDt { get; set; }

        public List<Chats>? Chats { get; set; }
    }
}
