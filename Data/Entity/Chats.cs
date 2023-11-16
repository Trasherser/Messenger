using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Data.Entity
{
    public class Chats
    {
        
        public int Id { get; set; }
        public Guid Id_user { get; set; }
        public DateTime CreateDt { get; set; }
    }
    

}
