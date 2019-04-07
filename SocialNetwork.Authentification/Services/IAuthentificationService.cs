using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.Authentification.Services
{
    public interface IAuthentificationService
    {
        Task<bool> LogIn(string email, string password);
        Task LogOut();
        Task<IEnumerable<IdentityError>> Register(string email, string password, string confirmPassword,
            params string[] roles);
    }
}