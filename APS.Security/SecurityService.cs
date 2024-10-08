
using APS.Data;
using APS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace APS.Security
{
    public interface ISecurityService
    {
        Task<User?> AuthUserAsync(User user);
        Task<User?> AuthUserByEmailAsync(User user);
    }

    public class SecurityService : ISecurityService
    {
        private readonly ISecurityRepository _securityRepository;

        public SecurityService(ISecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        public async Task<User?> AuthUserAsync(User user)
        {
            // Aquí debes devolver el objeto `User`, no un `bool`
            var authenticatedUser = await _securityRepository.AuthenticateUserAsync(user.Email, user.Password);
            return authenticatedUser; // Devuelve el objeto `User` autenticado o `null` si no existe
        }

        public async Task<User?> AuthUserByEmailAsync(User user)
        {
            // Usa el método `AuthUserAsync` y devuelve el usuario autenticado
            return await AuthUserAsync(user);
        }
    }

}
