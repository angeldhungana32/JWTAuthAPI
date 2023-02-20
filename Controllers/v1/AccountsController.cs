using JWTAuthAPI.Entities.DTOs.Authentication;
using JWTAuthAPI.Entities.DTOs.UserAccount;
using JWTAuthAPI.Helpers;
using JWTAuthAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthAPI.Controllers.v1
{
    public class AccountsController : V1BaseController
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // POST api/v1/Accounts/Login
        [HttpPost(RoutesConstant.Login)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync([FromBody] AuthenticateRequest authenticateRequest)
        {
            var user = await _accountService.AuthenticateUserAsync(authenticateRequest);
            return user == null ? BadRequest() : Ok(user);
        }

        // POST api/v1/Accounts/Register
        [HttpPost(RoutesConstant.Register)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterAsync([FromBody] UserCreateRequest request)
        {
            var user = await _accountService.AddUserAsync(request.ToEntity(), request.Password);
            if (user == null)
            {
                return BadRequest();
            }

            return Created(string.Format("/User/{0}", user.Id), user.ToResponseDTO());
        }

        // GET api/v1/Accounts/Users/id
        [HttpGet(RoutesConstant.GetUser)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            if (id == null) return BadRequest();

            var user = await _accountService.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        // PUT api/v1/Accounts/Users/id
        [HttpPut(RoutesConstant.UpdateUser)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] UserUpdateRequest request)
        {
            if(string.IsNullOrEmpty(id)) return BadRequest();

            var user = await _accountService.GetUserByIdAsync(id);

            if (user == null) return NotFound();

            var succeeded = await _accountService.UpdateUserAsync(user.UpdateEntity(request));

            if (!succeeded) { throw new Exception($"Failed User Update : {user.Id}"); }

            return NoContent();
        }

        // DELETE api/v1/Accounts/Users/id
        [HttpDelete(RoutesConstant.DeleteUser)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var user = await _accountService.GetUserByIdAsync(id);

            if (user == null) return NotFound();

            var succeeded = await _accountService.DeleteUserAsync(user);

            if (!succeeded) { throw new Exception($"Failed User Delete : {user.Id}"); }

            return NoContent();
        }

    }
}
