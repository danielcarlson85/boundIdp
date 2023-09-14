using AutoMapper;
using Bound.IDP.Abstractions.Interfaces;
using Bound.IDP.Managers.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Bound.IDP.Runtime.Controllers
{
    /// <summary>
    /// Main controller for the Admin user.
    /// </summary>
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userManager;

        public AdminController(IMapper mapper, IUserService userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        /// <summary>
        /// Returns all users. (Need to be Admin to use)
        /// </summary>
        [Produces(typeof(List<GetUserResponse>))]
        [HttpGet("api/v1/user/admin")]
        [SwaggerResponseExample(200, typeof(List<GetUserResponse>))]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userManager.GetUsersAsync();
            var usersDTO = _mapper.Map<List<User>, List<GetUserResponse>>(users);
            return Ok(usersDTO);
        }


        /// <summary>
        /// Returns a specific user. (Need to be Admin to use)
        /// </summary>
        /// <param name="mail">The mail of the user.</param>
        [Produces(typeof(List<GetUserResponse>))]
        [HttpGet("api/v1/user/admin/{mail}")]
        [SwaggerResponseExample(200, typeof(List<GetUserResponse>))]
        public async Task<IActionResult> GetUserAsync(string mail)
        {
            var user = await _userManager.GetUserAsync(mail);
            var userDTO = _mapper.Map<User, GetUserResponse>(user);
            return Ok(userDTO);
        }

        /// <summary>
        /// Creates a new Admin user. (Need to be Admin to use)
        /// </summary>
        /// <param name="user">The user request model to create</param>
        [Produces(typeof(List<CreateUserResponse>))]
        [HttpPost("api/v1/user/admin/")]
        [SwaggerRequestExample(typeof(GetUserResponse), typeof(GetUserResponse))]
        [SwaggerResponseExample(200, typeof(List<CreateUserResponse>))]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest user)
        {
            var userToCreate = _mapper.Map<CreateUserRequest, Microsoft.Graph.User>(user);
            await _userManager.CreateUserAsync(userToCreate, user.Password, "Admin");

            return Ok();
        }

        /// <summary>
        /// Updates the specified user. (Need to be Admin to use)
        /// </summary>
        /// <param name="objectId">The mail of the user to update.</param>
        /// <param name="user">The user request model to update</param>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPut("api/v1/user/admin/{objectId}")]
        [SwaggerRequestExample(typeof(UpdateUserRequest), typeof(UpdateUserRequest))]
        [SwaggerResponseExample(200, typeof(List<UpdateUserResponse>))]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest user, string objectId)
        {
            //var userToUpdated = _mapper.Map<UpdateUserRequest, User>(user);
            //await _userManager.UpdateUserAsync(userToUpdated, objectId, user.Password, "User");
            var updatedUser = await _userManager.GetUserAsync(objectId);
            var updatedUserDTO = _mapper.Map<User, UpdateUserResponse>(updatedUser);

            return Ok(updatedUserDTO);
        }

        /// <summary>
        /// Deletes the specified user. (Need to be Admin to use)
        /// </summary>
        /// <param name="objectId">The mail of the user to delete.</param>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete("api/v1/user/admin/{objectId}")]
        public async Task<IActionResult> DeleteUserAsync(string objectId)
        {
            await _userManager.DeleteUserAsync(objectId);
            return NoContent();
        }
    }
}
