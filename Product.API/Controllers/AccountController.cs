using Microsoft.AspNetCore.Mvc;
using Product.API.Contracts;
using Product.Application.Contracts.Infrastructure;
using Product.Application.Resources.Auth;

namespace Product.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(ApiRoute.Account.Register)]
        public async Task<IActionResult> RegisterAsync([FromBody] Register model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if ((bool)!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost(ApiRoute.Account.Login)]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Login(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost(ApiRoute.Account.AddRole)]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRole model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
    }
}
