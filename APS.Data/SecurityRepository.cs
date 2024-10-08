
using APS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.Data
{
    public interface ISecurityRepository
    {
        Task<User?> AuthenticateUserAsync(string email, string password);
        Task<bool> AuthorizeUserAsync(User user);
    }

    public class SecurityRepository : Repository, ISecurityRepository
    {
        public SecurityRepository(ApdatadbContext context) : base(context)
        {
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var foundUser = await Context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (foundUser != null && !string.IsNullOrEmpty(password))
            {
                Console.WriteLine($"Usuario encontrado: {foundUser.Email}");
                return foundUser;
            }

            Console.WriteLine("Usuario no encontrado o credenciales inválidas.");
            return null;
        }


        public async Task<bool> AuthorizeUserAsync(User user)
        {
          
            return true; 
        }
    }
}

