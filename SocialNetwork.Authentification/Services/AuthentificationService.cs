using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.Authentification.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthentificationService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> LogIn(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return result.Succeeded;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IEnumerable<IdentityError>> Register(string email, string password, string confirmPassword, params string[] roles)
        {
            if (roles.Length == 0)
            {
                return new List<IdentityError> { new IdentityError { Description = "Amount of roles couldn't be 0." } };
            }

            var allRoles = _roleManager.Roles.ToList();
            var incorrectRoles = roles.Except(allRoles.Select(r => r.Name)).ToList();

            if (incorrectRoles.Any())
            {
                return new List<IdentityError>
                {
                    new IdentityError {Description = $"Incorrect roles names: {string.Join(", ", incorrectRoles)}."}
                };
            }

            var user = new User { Email = email, UserName = email };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return result.Errors;
            }

            await _userManager.AddToRolesAsync(user, roles);
            await _signInManager.SignInAsync(user, false);
            return new List<IdentityError>();
        }
    }
}
