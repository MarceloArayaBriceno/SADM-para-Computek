
using APS.Data;
using APS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
namespace APS.Security;

public interface ISecurityService
{
    Task<bool> AuthUserAsync(User user);
    Task<bool> AuthUserByEmailAsync(User user);
}

public class SecurityService(ISecurityRepository securityRepository) : ISecurityService
{
    private readonly ISecurityRepository _securityRepository = securityRepository;

    public async Task<bool> AuthUserAsync(User user)
    {
        return await _securityRepository.AuthenticateUserAsync(user);
    }
    public async Task<bool> AuthUserByEmailAsync(User user)
    {
        return await _securityRepository.AuthenticateUserAsync(user);
    }
}
*/

using System.Threading.Tasks;

namespace APS.Security
{
    public interface ISecurityService
    {
        Task<bool> AuthUserAsync(User user);
        Task<bool> AuthUserByEmailAsync(User user);
    }

    public class SecurityService : ISecurityService
    {
        private readonly ISecurityRepository _securityRepository;

        public SecurityService(ISecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        public async Task<bool> AuthUserAsync(User user)
        {
            var authenticatedUser = await _securityRepository.AuthenticateUserAsync(user.Email, user.Password);
            return authenticatedUser != null; // Devuelve true si el usuario está autenticado
        }

        public async Task<bool> AuthUserByEmailAsync(User user)
        {
            return await AuthUserAsync(user); // Usa el mismo método de autenticación basado en email y contraseña
        }
    }
}
