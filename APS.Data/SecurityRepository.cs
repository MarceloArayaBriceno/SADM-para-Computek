
using APS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
namespace APS.Data;

public interface ISecurityRepository
{
    Task<bool> AuthenticateUserAsync(User user);
    Task<bool> AuthorizeUserAsync(User user);
}

public class SecurityRepository(ApdatadbContext context) : Repository(context), ISecurityRepository
{
    public async Task<bool> AuthorizeUserAsync(User user)
    {
        return true;
    }

    public async Task<bool> AuthenticateUserAsync(User user)
    {
        static bool validate(User user, User found)
        {
            var validations = new List<bool>
            {
                (user != null),
                (found != null),
                //(user.Username == found.Username),
                (user.Email == found.Email),
                (user.Password == found.Password),
                (!string.IsNullOrEmpty(user.Password))
            };

            return validations.All(x => x);
        }

        var foundUser = await Context.Users.SingleOrDefaultAsync(u => u.Email == user.Email);
        return validate(user, foundUser);
    }
}

*/

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

