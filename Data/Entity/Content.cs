using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Data.Entity
{
    public class Content
    {
        
        public int Id { get; set; }
        public int Id_chats { get; set; }
        public Guid Id_user { get; set; }  //delete
        public String TypeContent { get; set; }
        public String Contents { get; set; }
        public DateTime CreateDt { get; set; }

        public User User { get; set; } = null!;
    }
}
