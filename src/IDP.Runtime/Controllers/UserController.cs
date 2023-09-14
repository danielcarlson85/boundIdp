using AutoMapper;
using Bound.EventBus;
using Bound.IDP.Abstractions.Constants;
using Bound.IDP.Abstractions.Interfaces;
using Bound.IDP.Managers.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bound.IDP.Runtime.Controllers
{
    /// <summary>
    /// Main controller for the User.
    /// </summary>
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userManager;
       // private readonly IUserEventBusHandler _userEventBusHandler;

        public UserController(IMapper mapper, IUserService userManager, IUserEventBusHandler userEventBusHandler)
        {
            _mapper = mapper;
            _userManager = userManager;
          //  _userEventBusHandler = userEventBusHandler;
        }

        // To call through API Gateway use [GET] https://localhost:44375/user

        /// <summary>
        /// Returns the logged in user.
        /// </summary>
        [Produces(typeof(GetUserResponse))]
        [HttpGet("api/v1/user/")]
        [SwaggerResponseExample(200, typeof(GetUserResponse))]
        public async Task<IActionResult> GetUserAsync()
        {
            try
            {
                var userEmail = GetUserEmail();
                var user = await _userManager.GetUserAsync(userEmail);
                var userDTO = _mapper.Map<User, GetUserResponse>(user);

                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return Ok($"Error" + ex.Message);
            }
        }

        // To call through API Gateway use [POST] https://localhost:44375/user
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user request model to create</param>
        [Produces(typeof(List<CreateUserResponse>))]
        [HttpPost("api/v1/user")]
        [SwaggerRequestExample(typeof(GetUserResponse), typeof(GetUserResponse))]
        [SwaggerResponseExample(200, typeof(List<CreateUserResponse>))]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest user)
        {
            var userToCreate = _mapper.Map<CreateUserRequest, User>(user);
            await _userManager.CreateUserAsync(userToCreate, user.Password, "User");

            //Puts a message with the created user on queue: user
           // await _userEventBusHandler.SendMessageAsync($"{JsonSerializer.Serialize(userToCreate)}");

            return Ok("Check you mail");
        }

        // To call through API Gateway use [PUT] https://localhost:44375/user
        /// <summary>
        /// Updates the logged in user.
        /// </summary>
        /// <param name="user">The user request model to update</param>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPut("api/v1/user")]
        [SwaggerRequestExample(typeof(UpdateUserRequest), typeof(UpdateUserRequest))]
        [SwaggerResponseExample(200, typeof(List<UpdateUserResponse>))]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest user)
        {
            //TODO: Will need to take a look if we could send in mail without updating it, also send ID as it will probably be needed onward.
            //TODO: Check if we will forward objectId from FE mobile, or collect it here
            //var userObjectId = GetUserObjectId();

            var userToUpdate = _mapper.Map<UpdateUserRequest, User>(user);
            await _userManager.UpdateUserAsync(userToUpdate, "User");

            return Ok("User is updated");
        }

        // To call through API Gateway use [DELETE] https://localhost:44375/user
        /// <summary>
        /// Deletes the logged in user.
        /// </summary>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete("api/v1/user/")]
        public async Task<IActionResult> DeleteUserAsync()
        {
            var userObjectId = GetUserObjectId();
            await _userManager.DeleteUserAsync(userObjectId);
            return NoContent();
        }

        private string GetUserObjectId()
        {
            return User.Claims.Where(oid => oid.Type.Equals("http://schemas.microsoft.com/identity/claims/objectidentifier")).FirstOrDefault().Value;
        }

        private string GetUserEmail()
        {
            return User.Claims.Where(oid => oid.Type.Equals(ADConstants.ADUserResponse.Emails)).FirstOrDefault().Value;
        }
    }
}
