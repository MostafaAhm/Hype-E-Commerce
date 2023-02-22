using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Product.Application.Contracts.Infrastructure;
using Product.Application.Resources.Auth;
using Product.Domain.Entities;
using Product.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }
        public async Task<string> AddRoleAsync(AddRole model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Sonething went wrong";
        }

        public async Task<AuthResponse> Login(Login model)
        {
            var authReponse = new AuthResponse();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authReponse.Message = "Email or Password is incorrect!";
                return authReponse;
            }

            var jwtSecurityToken = await _tokenService.CreateToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authReponse.IsAuthenticated = true;
            authReponse.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authReponse.Email = user.Email;
            authReponse.Username = user.UserName;
            authReponse.ExpiresOn = jwtSecurityToken.ValidTo;
            authReponse.Roles = rolesList.ToList();

            return authReponse;
        }

        public async Task<AuthResponse> RegisterAsync(Register model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthResponse { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthResponse { Message = "Username is already registered!" };

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthResponse { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, model.Role);

            var jwtSecurityToken = await _tokenService.CreateToken(user);

            return new AuthResponse
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { model.Role },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }


        
    }
}
