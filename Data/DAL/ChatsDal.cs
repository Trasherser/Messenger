using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Messenger;
using Messenger.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Data.DAL
{
    public class ChatsDal
    {
        private static DataContext dataContext = ((App)Application.Current).dataContext;

        public static void GetMyChatsLoad(User user)
        {
            dataContext.Entry(user).Collection("Chats").Load();  // использование навигационого свойства
            
        }

        public static async Task Add(Chats chats)
        {
            await dataContext.chats.AddAsync(chats);
            await dataContext.SaveChangesAsync();
        }

        public static async Task<bool> IsUniqueChatId(int id)
        {
            return await dataContext.chats.AnyAsync(c => c.Id == id);
        }
    }

   
}
