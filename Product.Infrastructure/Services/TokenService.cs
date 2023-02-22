using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Product.Application.Contracts.Infrastructure;
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
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateToken(ApplicationUser user);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SymmetricSecurityKey key;
        public TokenService(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            this.config = config;
            _userManager = userManager;
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:key"]));
        }
        public async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = key;
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(double.Parse(config["JWT:DurationInDays"])),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
