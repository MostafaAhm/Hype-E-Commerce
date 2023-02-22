using Product.Application.Resources.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Contracts.Infrastructure
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(Register model);
        Task<AuthResponse> Login(Login model);
        Task<string> AddRoleAsync(AddRole model);
    }
}
