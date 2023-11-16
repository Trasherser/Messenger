using Messenger.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Messenger.Data.DAL
{
    public class UserDal
    {
        private static DataContext dataContext = ((App)Application.Current).dataContext;

        public static async Task Add(User user)
        {
            await dataContext.AddAsync(user);
            await dataContext.SaveChangesAsync();
        }

        public static async Task<User?> Get(String login)
        {
            return await dataContext.users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public static async Task<User?> Get(Guid id)
        {
            return await dataContext.users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public static async Task<List<User>> GetListUser()
        {
            return await dataContext.users.ToListAsync();
        }

        public static async void Update(User user)
        {
            var temp = dataContext.users.FirstOrDefault(u => u.Id == user.Id);
            if(temp == null) { return; }
            if(temp.Login != user.Login) { temp.Login = user.Login; }
            if(temp.CreateDt == user.CreateDt) { temp.CreateDt = user.CreateDt; }
            if(temp.Password == user.Password) { temp.Password = user.Password; }
            if(temp.Email == user.Email) { temp.Email = user.Email; }

            MessageBox.Show("Updating user");
            await dataContext.SaveChangesAsync();
        }

        public static async Task<bool> GetStatusConfirmCode()
        {
            return await dataContext.users.AnyAsync(u => u.ConfirmCode == null);
        }

        public static async void SetNullConfirmCode(Guid id)
        {
            var temp = await dataContext.users.FirstOrDefaultAsync(u => u.Id == id);
            if(temp != null)
            {
                temp.ConfirmCode = null;
                await dataContext.SaveChangesAsync();
            }
             
        }



    }
}
